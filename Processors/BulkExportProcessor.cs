using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FableMod.BIG;
using FableMod.Gfx.Integration;
using FableMod.CLRCore;

namespace ChocolateBox
{
    /// <summary>
    /// Processes bulk export of assets from BIG archives.
    /// Supports exporting both meshes (3D models) and textures in parallel for performance.
    /// </summary>
    internal class BulkExportProcessor : Processor
    {
        private List<AssetEntry> m_SelectedEntries;
        private string m_ParentFolder;
        private ExportFormat m_Format;
        private int m_MeshCount = 0;
        private int m_TextureCount = 0;
        private int m_SkippedCount = 0;

        public BulkExportProcessor(List<AssetEntry> selectedEntries, string parentFolder, ExportFormat format)
        {
            m_SelectedEntries = selectedEntries;
            m_ParentFolder = parentFolder;
            m_Format = format;
        }

        public BulkExportProcessor(List<AssetEntry> selectedEntries, string parentFolder) : this(selectedEntries, parentFolder, ExportFormat.X)
        {
        }

        public override void Run(Progress progress)
        {
            progress.Info = "Bulk Exporting Assets...";
            progress.Begin(m_SelectedEntries.Count);

            int processedCount = 0;
            int totalCount = m_SelectedEntries.Count;

            Parallel.ForEach(m_SelectedEntries, entry =>
            {
                string bankName = ((BIGBank)entry.Bank).Name;
                string graphicsBankName = Settings.GetString("Banks", "Graphics");
                string texturesBankName = Settings.GetString("Banks", "Textures");
                string guiTexturesBankName = Settings.GetString("Banks", "GUITextures");

                // Check if this is a mesh entry
                bool isMesh = bankName == graphicsBankName &&
                              (entry.Type == 1U || entry.Type == 2U || entry.Type == 4U || entry.Type == 5U);

                // Check if this is a texture entry
                bool isTexture = (bankName == texturesBankName || bankName == guiTexturesBankName) &&
                                 (entry.Type == 0U || entry.Type == 1U || entry.Type == 2U);

                if (isMesh)
                {
                    try
                    {
                        // Each thread needs its own GfxModel instance which usually loads data from the archive
                        // AssetEntry and BIGFile should be thread-safe for reading since we added concurrent FileStreams in previous tasks
                        GfxModel model = null;
                        lock (this)
                        {
                            try { model = new GfxModel(entry); }
                            catch (Exception ex)
                            {
                                Console.WriteLine("[DEBUG_LOG] Error creating GfxModel for {0}: {1}", entry.DevSymbolName, ex.Message);
                                return; // Continue to next entry
                            }
                        }
                        
                        if (model != null && model.LODCount > 0)
                        {
                            string meshName = entry.DevSymbolName;
                            string meshSubFolder = Path.Combine(m_ParentFolder, "Meshes", meshName);

                            // Directory.CreateDirectory is thread-safe
                            if (!Directory.Exists(meshSubFolder)) Directory.CreateDirectory(meshSubFolder);

                            GfxModelLOD lod = model.get_LODs(0);
                            
                            // Synchronize export to prevent potential race conditions in native Gfx libs
                            lock (this)
                            {
                                ModelExporter.Export(lod, meshSubFolder, meshName, m_Format);
                            }
                            Interlocked.Increment(ref m_MeshCount);

                            // Track textures exported for THIS mesh to avoid redundant exports within the same mesh folder
                            HashSet<uint> meshExportedTextures = new HashSet<uint>();

                            // Export textures for all LODs
                            for (int i = 0; i < model.LODCount; i++)
                            {
                                GfxModelLOD currentLod = model.get_LODs(i);
                                for (int j = 0; j < currentLod.MaterialCount; j++)
                                {
                                    Material mat = currentLod.get_Materials((uint)j);
                                    ExportTexture(mat.BaseTextureID, meshSubFolder, meshExportedTextures);
                                    ExportTexture(mat.BumpMapTextureID, meshSubFolder, meshExportedTextures);
                                    ExportTexture(mat.ReflectionTextureID, meshSubFolder, meshExportedTextures);
                                    ExportTexture(mat.AlphaMapTextureID, meshSubFolder, meshExportedTextures);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        string errorMsg = string.Format("[DEBUG_LOG] Error exporting mesh {0}: {1}", entry.DevSymbolName, ex.Message);
                        Console.WriteLine(errorMsg);
                        if (ex.InnerException != null)
                            Console.WriteLine("[DEBUG_LOG] Inner Exception: {0}", ex.InnerException.Message);
                        Interlocked.Increment(ref m_SkippedCount);
                    }
                }
                else if (isTexture)
                {
                    // Export standalone texture
                    try
                    {
                        string textureFolder = Path.Combine(m_ParentFolder, "Textures");
                        if (!Directory.Exists(textureFolder))
                            Directory.CreateDirectory(textureFolder);

                        GfxTexture texture = null;
                        lock (this)
                        {
                            try
                            {
                                texture = new GfxTexture(entry);
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("[DEBUG_LOG] Error creating GfxTexture for {0}: {1}", entry.DevSymbolName, ex.Message);
                                Interlocked.Increment(ref m_SkippedCount);
                                return;
                            }
                        }

                        if (texture != null)
                        {
                            string texPath = Path.Combine(textureFolder, entry.DevSymbolName + ".dds");
                            lock (this)
                            {
                                texture.Save(texPath, 0);
                            }
                            Interlocked.Increment(ref m_TextureCount);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("[DEBUG_LOG] Error exporting standalone texture {0}: {1}", entry.DevSymbolName, ex.Message);
                        if (ex.InnerException != null)
                            Console.WriteLine("[DEBUG_LOG] Inner Exception: {0}", ex.InnerException.Message);
                        Interlocked.Increment(ref m_SkippedCount);
                    }
                }
                else
                {
                    // Unsupported asset type
                    Interlocked.Increment(ref m_SkippedCount);
                    Console.WriteLine("[DEBUG_LOG] Skipping unsupported asset: {0} (Bank: {1}, Type: {2})",
                        entry.DevSymbolName, bankName, entry.Type);
                }

                int currentProcessed = Interlocked.Increment(ref processedCount);
                if (currentProcessed % Math.Max(1, totalCount / 100) == 0)
                {
                    progress.Update();
                }
            });

            progress.End();
            
            FormMain.Instance.Invoke(new Action(() =>
            {
                string message = string.Format(
                    "Bulk export completed.\n\n" +
                    "Meshes exported: {0}\n" +
                    "Textures exported: {1}\n" +
                    "Skipped (unsupported or errors): {2}",
                    m_MeshCount, m_TextureCount, m_SkippedCount);

                System.Windows.Forms.MessageBox.Show(
                    message,
                    "Export Complete",
                    System.Windows.Forms.MessageBoxButtons.OK,
                    System.Windows.Forms.MessageBoxIcon.Information);
            }));
        }

        private void ExportTexture(uint textureID, string textureFolder, HashSet<uint> exportedTextures)
        {
            if (textureID == 0 || textureID == 0xFFFFFFFF || exportedTextures.Contains(textureID))
                return;

            AssetEntry texEntry = FindEntryInBank(FileDatabase.Instance.Textures, Settings.GetString("Banks", "Textures"), textureID);
            if (texEntry == null)
            {
                texEntry = FindEntryInBank(FileDatabase.Instance.Textures, Settings.GetString("Banks", "GUITextures"), textureID);
            }

            if (texEntry != null)
            {
                    try
                    {
                        GfxTexture texture = null;
                        lock (this)
                        {
                            try { texture = new GfxTexture(texEntry); }
                            catch (Exception ex)
                            {
                                Console.WriteLine("[DEBUG_LOG] Error creating GfxTexture for {0}: {1}", texEntry.DevSymbolName, ex.Message);
                                return;
                            }
                        }
                        
                        // Use .dds extension for native format export
                    string texPath = Path.Combine(textureFolder, texEntry.DevSymbolName + ".dds");
                    
                    // Synchronize export to prevent potential race conditions in native Gfx libs
                    lock (this)
                    {
                        texture.Save(texPath, 0);
                    }
                    
                    lock (exportedTextures)
                    {
                        exportedTextures.Add(textureID);
                    }
                    Interlocked.Increment(ref m_TextureCount);
                }
                catch (Exception ex)
                {
                    string errorMsg = string.Format("[DEBUG_LOG] Error exporting texture {0}: {1}", textureID, ex.Message);
                    Console.WriteLine(errorMsg);
                    if (ex.InnerException != null)
                        Console.WriteLine("[DEBUG_LOG] Inner Exception: {0}", ex.InnerException.Message);
                }
            }
        }

        private AssetEntry FindEntryInBank(BIGFile big, string bankName, uint id)
        {
            if (big == null) return null;
            for (int i = 0; i < big.BankCount; i++)
            {
                BIGBank bank = (BIGBank)big.get_Banks(i);
                if (bank.Name == bankName)
                {
                    return bank.FindEntryByID(id);
                }
            }
            return null;
        }
    }
}
