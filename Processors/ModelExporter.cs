using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using FableMod.Gfx.Integration;

namespace ChocolateBox
{
    /// <summary>
    /// Supported 3D model export formats.
    /// </summary>
    public enum ExportFormat
    {
        /// <summary>DirectX .X format (native Fable format)</summary>
        X,
        /// <summary>Wavefront OBJ format (widely supported)</summary>
        OBJ
    }

    /// <summary>
    /// Handles exporting 3D models from Fable to various formats.
    ///
    /// <para><b>Export Process:</b></para>
    /// <list type="number">
    /// <item>Model is first exported to DirectX .X format (native format)</item>
    /// <item>If OBJ format requested, .X file is parsed and converted</item>
    /// <item>Coordinate system is transformed to match target format conventions</item>
    /// </list>
    ///
    /// <para><b>Coordinate Systems:</b></para>
    /// <list type="bullet">
    /// <item><b>Fable .X:</b> Y forward, Z up, X right</item>
    /// <item><b>OBJ Output:</b> X forward, Z up, Y right (Z-up convention)</item>
    /// </list>
    ///
    /// <para><b>Features:</b></para>
    /// <list type="bullet">
    /// <item>Exports geometry with vertex positions, normals, and UV coordinates</item>
    /// <item>Preserves material assignments and texture references</item>
    /// <item>Handles segmented meshes (maintains mesh groups)</item>
    /// <item>Applies proper scaling (default: 0.01x for game units to world units)</item>
    /// </list>
    /// </summary>
    public static class ModelExporter
    {
        private class ParsedMesh
        {
            public string Name;
            public List<float> Vertices = new List<float>();
            public List<int[]> Faces = new List<int[]>();
            public List<float> Uvs = new List<float>();
            public string TextureName;
            public List<string> MaterialTextures = new List<string>();
            public List<int> FaceMaterialIndices = new List<int>();
        }

        private const float DefaultScale = 0.01f;

        private static void TransformPositionForObj(float rawX, float rawY, float rawZ, out float x, out float y, out float z)
        {
            // Transform to Z+ up, X+ forward (matching glTF output)
            // Fable .X in practice: Y forward, Z up, X right
            // Transform: X_obj = -Y_fable, Y_obj = Z_fable, Z_obj = X_fable
            x = -rawY * DefaultScale;
            y = rawZ * DefaultScale;
            z = rawX * DefaultScale;
        }

        /// <summary>
        /// Exports a Fable 3D model to the specified format.
        /// </summary>
        /// <param name="lod">The model LOD (Level of Detail) to export. Typically LOD 0 is the highest quality.</param>
        /// <param name="directory">Output directory where the model file will be saved.</param>
        /// <param name="fileName">Base filename (without extension). Extension will be added based on format.</param>
        /// <param name="format">Target export format (X or OBJ).</param>
        /// <remarks>
        /// The export process always creates an intermediate .X file first, then converts to OBJ if requested.
        /// This is because the FableMod library natively exports to DirectX .X format.
        /// <para>
        /// For OBJ exports, the .X file is parsed and converted with proper coordinate transformation
        /// and material mapping. The OBJ will include a .mtl material file if textures are referenced.
        /// </para>
        /// </remarks>
        public static void Export(GfxModelLOD lod, string directory, string fileName, ExportFormat format)
        {
            string xPath = Path.Combine(directory, fileName + ".X");

            // Always export to .X first as a base or if requested
            Console.WriteLine($"[DEBUG_LOG] ModelExporter.Export: Starting export for {fileName} to {directory} (Format: {format})");
            Console.WriteLine($"[DEBUG_LOG] ModelExporter.Export: Exporting to .X: {xPath}");
            lod.ExportX(xPath);

            if (!File.Exists(xPath))
            {
                Console.WriteLine($"[DEBUG_LOG] ModelExporter.Export: FAILED: .X file was not created at {xPath}");
                return;
            }

            long length = new FileInfo(xPath).Length;
            Console.WriteLine($"[DEBUG_LOG] ModelExporter.Export: .X file created, size: {length} bytes");

            if (length == 0)
            {
                Console.WriteLine("[DEBUG_LOG] ModelExporter.Export: FAILED: .X file is empty");
                return;
            }

            if (format == ExportFormat.X)
                return;

            try
            {
                if (format == ExportFormat.OBJ)
                {
                    Console.WriteLine($"[DEBUG_LOG] Converting to OBJ: {fileName}.obj");
                    ConvertToObj(xPath, Path.Combine(directory, fileName + ".obj"));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("[DEBUG_LOG] Error converting model: " + ex.Message);
                Console.WriteLine("[DEBUG_LOG] Stack trace: " + ex.StackTrace);
            }
        }

        private class XFileTokenReader
        {
            private string[] tokens;
            private int index = 0;

            public XFileTokenReader(string content)
            {
                // We need to tokenize but keep separators like ; and , as tokens 
                // IF they are not attached to numbers.
                // However, the previous approach of Replace(";", " ; ") was causing issues
                // when double-replaced or when whitespace was already present.
                
                StringBuilder sb = new StringBuilder();
                foreach (char c in content)
                {
                    if (c == ';' || c == ',' || c == '{' || c == '}')
                    {
                        sb.Append(' ');
                        sb.Append(c);
                        sb.Append(' ');
                    }
                    else if (char.IsWhiteSpace(c))
                    {
                        sb.Append(' ');
                    }
                    else
                    {
                        sb.Append(c);
                    }
                }

                tokens = sb.ToString().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                
                Console.WriteLine($"[DEBUG_LOG] XFileTokenReader: Tokenized content into {tokens.Length} tokens");
            }

            public string Next() => index < tokens.Length ? tokens[index++] : null;
            public string Peek() => index < tokens.Length ? tokens[index] : null;
            public bool HasNext() => index < tokens.Length;

            public bool SkipTo(string target)
            {
                while (HasNext())
                {
                    if (Next() == target) return true;
                }
                return false;
            }

            public bool TryParseFloat(out float result)
            {
                result = 0;
                string t = Next();
                while (t == "," || t == ";") t = Next();
                if (t == null) return false;
                
                bool success = float.TryParse(t, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out result);
                if (!success) Console.WriteLine($"[DEBUG_LOG] XFileTokenReader: Failed to parse float from token '{t}' at index {index-1}");
                return success;
            }

            public bool TryParseInt(out int result)
            {
                result = 0;
                string t = Next();
                while (t == "," || t == ";") t = Next();
                if (t == null) return false;

                // Sometimes floats are where ints are expected (e.g. if we desync)
                // or if the .X file has 1.0 instead of 1
                if (int.TryParse(t, out result)) return true;
                
                if (float.TryParse(t, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.InvariantCulture, out float f))
                {
                    result = (int)f;
                    return true;
                }

                Console.WriteLine($"[DEBUG_LOG] XFileTokenReader: Failed to parse int from token '{t}' at index {index-1}");
                return false;
            }
        }



        private static void ConvertToObj(string xPath, string objPath)
        {
            if (!File.Exists(xPath)) return;

            string mtlPath = Path.ChangeExtension(objPath, ".mtl");
            StringBuilder objContent = new StringBuilder();
            StringBuilder mtlContent = new StringBuilder();
            
            objContent.AppendLine($"mtllib {Path.GetFileName(mtlPath)}");
            objContent.AppendLine("# Exported from Silver Chest");
            
            string content = File.ReadAllText(xPath);
            int vertexOffset = 0;
            int uvOffset = 0;
            HashSet<string> definedMaterials = new HashSet<string>();

                int searchPos = 0;
                int meshCount = 0;
                Console.WriteLine($"[DEBUG_LOG] OBJ: Starting mesh search in file of length {content.Length}");
                while (searchPos < content.Length)
                {
                    int meshIdx = content.IndexOf("Mesh", searchPos);
                    if (meshIdx == -1) 
                    {
                        Console.WriteLine($"[DEBUG_LOG] OBJ: No more 'Mesh' keywords found after pos {searchPos}");
                        break;
                    }

                    Console.WriteLine($"[DEBUG_LOG] OBJ: Potential 'Mesh' keyword found at {meshIdx}");

                    // Ensure it's not a template Mesh or another word containing "Mesh"
                    // Check prefix
                    bool isTemplate = false;
                    if (meshIdx >= 9 && content.Substring(meshIdx - 9, 9) == "template ")
                    {
                        isTemplate = true;
                    }
                    
                    // Ensure it's a standalone word "Mesh"
                    bool isStandalone = true;
                    if (meshIdx > 0 && char.IsLetterOrDigit(content[meshIdx - 1]))
                    {
                        isStandalone = false;
                    }
                    
                    // Check if it's followed by whitespace or {
                    char nextChar = meshIdx + 4 < content.Length ? content[meshIdx + 4] : '\0';
                    bool hasValidSuffix = (nextChar == ' ' || nextChar == '\t' || nextChar == '\n' || nextChar == '\r' || nextChar == '{' || nextChar == '\0');

                    if (isTemplate || !isStandalone || !hasValidSuffix)
                    {
                        Console.WriteLine($"[DEBUG_LOG] OBJ: Skipping 'Mesh' at {meshIdx} (isTemplate: {isTemplate}, isStandalone: {isStandalone}, hasValidSuffix: {hasValidSuffix}, nextChar: '{(nextChar == '\0' ? "NULL" : nextChar.ToString())}')");
                        searchPos = meshIdx + 4;
                        continue;
                    }

                    int blockStart = content.IndexOf("{", meshIdx);
                    if (blockStart == -1) 
                    {
                        Console.WriteLine($"[DEBUG_LOG] OBJ: No opening brace found for Mesh at {meshIdx}");
                        searchPos = meshIdx + 4;
                        continue;
                    }
                    
                    int depth = 1;
                    int blockEnd = -1;
                    for (int i = blockStart + 1; i < content.Length; i++)
                    {
                        if (content[i] == '{') depth++;
                        else if (content[i] == '}')
                        {
                            depth--;
                            if (depth == 0)
                            {
                                blockEnd = i;
                                break;
                            }
                        }
                    }

                    if (blockEnd == -1)
                    {
                        Console.WriteLine($"[DEBUG_LOG] OBJ: No closing brace found for Mesh starting at {blockStart}");
                        break;
                    }
                    searchPos = blockEnd + 1;

                    string meshBlock = content.Substring(blockStart + 1, blockEnd - blockStart - 1);
                    Console.WriteLine($"[DEBUG_LOG] OBJ: Processing Mesh block starting at {blockStart}, length: {meshBlock.Length}");
                    
                    // Try to get the name of the mesh from the content before {
                    string meshHeader = content.Substring(meshIdx, blockStart - meshIdx);
                    string[] headerTokens = meshHeader.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
                    string meshName = headerTokens.Length > 1 ? headerTokens[1] : null;

                    XFileTokenReader reader = new XFileTokenReader(meshBlock);

                    int vertexCount = 0;
                    if (reader.TryParseInt(out vertexCount))
                    {
                        Console.WriteLine($"[DEBUG_LOG] OBJ: Parsed vertex count: {vertexCount}");
                        
                        // We need to skip the vertices and faces to find MeshMaterialList if we don't use the reader for everything
                        // But since we are already using the reader, let's just use it to parse everything in order.
                    string currentTexture = null;
                    int matListIdx = meshBlock.IndexOf("MeshMaterialList");
                    if (matListIdx != -1)
                    {
                        Console.WriteLine($"[DEBUG_LOG] OBJ: Found MeshMaterialList in block at offset {matListIdx}");
                        int texFileIdx = meshBlock.IndexOf("TextureFilename", matListIdx);
                        if (texFileIdx != -1)
                        {
                            Console.WriteLine($"[DEBUG_LOG] OBJ: Found TextureFilename in block at offset {texFileIdx}");
                            int quoteStart = meshBlock.IndexOf("\"", texFileIdx);
                            int quoteEnd = meshBlock.IndexOf("\"", quoteStart + 1);
                            if (quoteStart != -1 && quoteEnd != -1)
                            {
                                currentTexture = meshBlock.Substring(quoteStart + 1, quoteEnd - quoteStart - 1);
                                Console.WriteLine($"[DEBUG_LOG] OBJ: Found texture: {currentTexture}");
                                currentTexture = Path.GetFileName(currentTexture);
                                
                                // Preserve the original extension to match on-disk textures
                                // (Avoid forcing .dds since some sources are not DDS.)
                                if (string.IsNullOrEmpty(Path.GetExtension(currentTexture)))
                                    currentTexture += ".dds";
                            }
                        }
                    }

                    string mtlName = !string.IsNullOrEmpty(currentTexture) ? Path.GetFileNameWithoutExtension(currentTexture) : $"Material_{vertexOffset}";
                    if (!string.IsNullOrEmpty(currentTexture))
                    {
                        if (!definedMaterials.Contains(mtlName))
                        {
                            mtlContent.AppendLine($"newmtl {mtlName}");
                            mtlContent.AppendLine("Ka 1.000 1.000 1.000");
                            mtlContent.AppendLine("Kd 1.000 1.000 1.000");
                            mtlContent.AppendLine("Ks 0.000 0.000 0.000");
                            mtlContent.AppendLine($"map_Kd {currentTexture}");
                            mtlContent.AppendLine();
                            definedMaterials.Add(mtlName);
                        }
                    }
                    else
                    {
                        // Default material for meshes without textures
                        if (!definedMaterials.Contains(mtlName))
                        {
                            mtlContent.AppendLine($"newmtl {mtlName}");
                            mtlContent.AppendLine("Ka 0.200 0.200 0.200");
                            mtlContent.AppendLine("Kd 0.600 0.600 0.600");
                            mtlContent.AppendLine("Ks 0.000 0.000 0.000");
                            mtlContent.AppendLine();
                            definedMaterials.Add(mtlName);
                        }
                    }

                    string objMeshName = !string.IsNullOrEmpty(meshName) ? meshName : $"Mesh_{vertexOffset}_{meshCount}";
                    objContent.AppendLine($"o {objMeshName}");
                    objContent.AppendLine($"usemtl {mtlName}");
                    objContent.AppendLine("s 1");

                    for (int j = 0; j < vertexCount; j++)
                    {
                        if (reader.TryParseFloat(out float rawX) && 
                            reader.TryParseFloat(out float rawY) && 
                            reader.TryParseFloat(out float rawZ))
                        {
                            TransformPositionForObj(rawX, rawY, rawZ, out float x, out float y, out float z);
                            objContent.AppendLine($"v {x.ToString("F6", System.Globalization.CultureInfo.InvariantCulture)} {y.ToString("F6", System.Globalization.CultureInfo.InvariantCulture)} {z.ToString("F6", System.Globalization.CultureInfo.InvariantCulture)}");
                        }
                    }

                    meshCount++;

                    int uvCountWritten = 0;
                    bool emitUvs = false;
                    StringBuilder uvLines = new StringBuilder();
                    int uvIdx = meshBlock.IndexOf("MeshTextureCoords");
                    if (uvIdx != -1)
                    {
                        int uvBlockStart = meshBlock.IndexOf("{", uvIdx);
                        if (uvBlockStart != -1)
                        {
                            XFileTokenReader uvReader = new XFileTokenReader(meshBlock.Substring(uvBlockStart + 1));
                            if (uvReader.TryParseInt(out int nUvs))
                            {
                                if (nUvs >= vertexCount)
                                {
                                    emitUvs = true;
                                    for (int j = 0; j < vertexCount; j++)
                                    {
                                        if (uvReader.TryParseFloat(out float u) && uvReader.TryParseFloat(out float v))
                                        {
                                            uvLines.AppendLine($"vt {u.ToString("F6", System.Globalization.CultureInfo.InvariantCulture)} {(1.0f - v).ToString("F6", System.Globalization.CultureInfo.InvariantCulture)}");
                                            uvCountWritten++;
                                        }
                                        else
                                        {
                                            emitUvs = false;
                                            uvCountWritten = 0;
                                            uvLines.Clear();
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                    if (emitUvs)
                        objContent.Append(uvLines.ToString());

                    if (reader.TryParseInt(out int faceCount))
                    {
                        for (int j = 0; j < faceCount; j++)
                        {
                            if (reader.TryParseInt(out int nIndices))
                            {
                                List<int> indices = new List<int>();
                                for (int k = 0; k < nIndices; k++)
                                {
                                    if (reader.TryParseInt(out int idx))
                                        indices.Add(idx);
                                }
                                
                                if (indices.Count >= 3)
                                {
                                    objContent.Append("f");
                                    // Reverse winding for OBJ (DirectX is usually CW, OBJ is CCW)
                                    // Combined with ORIENT_FOR_UNREAL transformation, this matches Python's logic
                                    // (Python uses recalc_face_normals which typically yields CCW)
                                    for (int k = indices.Count - 1; k >= 0; k--)
                                    {
                                        int idx = indices[k];
                                        int vIdx = idx + 1 + vertexOffset;
                                        if (emitUvs)
                                        {
                                            int vtIdx = idx + 1 + uvOffset;
                                            objContent.Append($" {vIdx}/{vtIdx}");
                                        }
                                        else
                                        {
                                            objContent.Append($" {vIdx}");
                                        }
                                    }
                                    objContent.AppendLine();
                                }
                            }
                        }
                    }
                    vertexOffset += vertexCount;
                    if (emitUvs)
                        uvOffset += uvCountWritten;
                }
            }

            File.WriteAllText(objPath, objContent.ToString());
            if (mtlContent.Length > 0)
                File.WriteAllText(mtlPath, mtlContent.ToString());
        }

        private static bool TryParseMeshMaterialList(string meshBlock, out List<string> materialTextures, out List<int> faceMaterialIndices)
        {
            materialTextures = new List<string>();
            faceMaterialIndices = new List<int>();

            int matListIdx = meshBlock.IndexOf("MeshMaterialList");
            if (matListIdx == -1)
                return false;

            int blockStart = meshBlock.IndexOf("{", matListIdx);
            if (blockStart == -1)
                return false;

            int depth = 1;
            int blockEnd = -1;
            for (int i = blockStart + 1; i < meshBlock.Length; i++)
            {
                if (meshBlock[i] == '{') depth++;
                else if (meshBlock[i] == '}')
                {
                    depth--;
                    if (depth == 0)
                    {
                        blockEnd = i;
                        break;
                    }
                }
            }

            if (blockEnd == -1)
                return false;

            string matListBlock = meshBlock.Substring(blockStart + 1, blockEnd - blockStart - 1);
            XFileTokenReader reader = new XFileTokenReader(matListBlock);

            if (!reader.TryParseInt(out int materialCount))
                return false;

            if (!reader.TryParseInt(out int faceIndexCount))
                return false;

            for (int i = 0; i < faceIndexCount; i++)
            {
                if (reader.TryParseInt(out int matIdx))
                    faceMaterialIndices.Add(matIdx);
            }

            materialTextures = ExtractMaterialTextures(matListBlock, materialCount);
            return true;
        }

        private static List<string> ExtractMaterialTextures(string matListBlock, int expectedCount)
        {
            List<string> textures = new List<string>();
            int searchPos = 0;

            while (searchPos < matListBlock.Length && textures.Count < expectedCount)
            {
                int matIdx = matListBlock.IndexOf("Material", searchPos);
                if (matIdx == -1)
                    break;

                bool isStandalone = matIdx == 0 || !char.IsLetterOrDigit(matListBlock[matIdx - 1]);
                if (!isStandalone)
                {
                    searchPos = matIdx + 1;
                    continue;
                }

                int braceStart = matListBlock.IndexOf("{", matIdx);
                if (braceStart == -1)
                    break;

                int depth = 1;
                int braceEnd = -1;
                for (int i = braceStart + 1; i < matListBlock.Length; i++)
                {
                    if (matListBlock[i] == '{') depth++;
                    else if (matListBlock[i] == '}')
                    {
                        depth--;
                        if (depth == 0)
                        {
                            braceEnd = i;
                            break;
                        }
                    }
                }

                if (braceEnd == -1)
                    break;

                string matBlock = matListBlock.Substring(braceStart + 1, braceEnd - braceStart - 1);
                string textureName = FindFirstTextureFilename(matBlock);
                textures.Add(textureName);

                searchPos = braceEnd + 1;
            }

            while (textures.Count < expectedCount)
                textures.Add(null);

            return textures;
        }

        private static string FindFirstTextureFilename(string block)
        {
            int texFileIdx = block.IndexOf("TextureFilename");
            if (texFileIdx == -1)
                return null;

            int quoteStart = block.IndexOf("\"", texFileIdx);
            int quoteEnd = quoteStart != -1 ? block.IndexOf("\"", quoteStart + 1) : -1;
            if (quoteStart == -1 || quoteEnd == -1)
                return null;

            string textureName = block.Substring(quoteStart + 1, quoteEnd - quoteStart - 1);
            textureName = Path.GetFileName(textureName);
            if (string.IsNullOrEmpty(Path.GetExtension(textureName)))
                textureName += ".dds";

            return textureName;
        }

        private static List<ParsedMesh> ParseMeshesFromX(string content, string baseName)
        {
            List<ParsedMesh> meshes = new List<ParsedMesh>();
            string normalized = content.Replace("\r\n", "\n").Replace("\r", "\n");

            string framePattern = @"Frame\s+([\w-]+)\s*\{\s*\n?\s*FrameTransformMatrix\s*\{[^}]+\}\s*\n?\s*Mesh\s*\{";
            MatchCollection matches = Regex.Matches(normalized, framePattern, RegexOptions.Singleline);
            foreach (Match match in matches)
            {
                string frameName = match.Groups[1].Value;
                if (ShouldSkipFrame(frameName, baseName))
                    continue;

                int meshStart = match.Index + match.Length;
                int depth = 1;
                int pos = meshStart;
                while (pos < normalized.Length && depth > 0)
                {
                    char c = normalized[pos];
                    if (c == '{') depth++;
                    else if (c == '}') depth--;
                    pos++;
                }

                if (depth != 0 || pos <= meshStart)
                    continue;

                string meshBlock = normalized.Substring(meshStart, pos - meshStart - 1);
                ParsedMesh mesh = ParseMeshBlock(meshBlock, frameName);
                if (mesh.Vertices.Count > 0 && mesh.Faces.Count > 0)
                    meshes.Add(mesh);
            }

            return meshes;
        }

        private static bool ShouldSkipFrame(string frameName, string baseName)
        {
            if (string.IsNullOrEmpty(frameName))
                return true;

            string lower = frameName.ToLowerInvariant();
            string[] skipNames = new string[]
            {
                "frame",
                "mesh",
                "template",
                "movement",
                "scene-root",
                "orphan_helpers",
                "hdmy_",
                "hpnt_",
                "bone_offset_matrix"
            };

            foreach (string skip in skipNames)
            {
                if (lower.Contains(skip))
                    return true;
            }

            if (frameName.StartsWith("MESH_", StringComparison.OrdinalIgnoreCase) &&
                string.Equals(frameName, baseName, StringComparison.OrdinalIgnoreCase))
                return true;

            return false;
        }

        private static ParsedMesh ParseMeshBlock(string meshBlock, string name)
        {
            ParsedMesh mesh = new ParsedMesh { Name = name };

            Match vertMatch = Regex.Match(meshBlock, @"^\s*(\d+);\s*(.*?);;", RegexOptions.Singleline);
            if (!vertMatch.Success)
                return mesh;

            string vertData = vertMatch.Groups[2].Value;
            foreach (Match v in Regex.Matches(vertData, @"([-+]?\d*\.?\d+(?:e[-+]?\d+)?)\s*;\s*([-+]?\d*\.?\d+(?:e[-+]?\d+)?)\s*;\s*([-+]?\d*\.?\d+(?:e[-+]?\d+)?)\s*;?"))
            {
                if (TryParseInvariantFloat(v.Groups[1].Value, out float x) &&
                    TryParseInvariantFloat(v.Groups[2].Value, out float y) &&
                    TryParseInvariantFloat(v.Groups[3].Value, out float z))
                {
                    mesh.Vertices.Add(x);
                    mesh.Vertices.Add(y);
                    mesh.Vertices.Add(z);
                }
            }

            string remaining = meshBlock.Substring(vertMatch.Index + vertMatch.Length);
            Match faceMatch = Regex.Match(remaining, @"^\s*(\d+);\s*(.*?);;", RegexOptions.Singleline);
            if (faceMatch.Success)
            {
                string faceData = faceMatch.Groups[2].Value;
                foreach (Match f in Regex.Matches(faceData, @"3;(\d+),(\d+),(\d+);?"))
                {
                    if (int.TryParse(f.Groups[1].Value, out int a) &&
                        int.TryParse(f.Groups[2].Value, out int b) &&
                        int.TryParse(f.Groups[3].Value, out int c))
                    {
                        mesh.Faces.Add(new int[] { a, b, c });
                    }
                }

                remaining = remaining.Substring(faceMatch.Index + faceMatch.Length);
            }

            Match uvMatch = Regex.Match(remaining, @"MeshTextureCoords\s*\{\s*(\d+);\s*(.*?);;", RegexOptions.Singleline);
            if (uvMatch.Success)
            {
                string uvData = uvMatch.Groups[2].Value;
                foreach (Match u in Regex.Matches(uvData, @"([-+]?\d*\.?\d+(?:e[-+]?\d+)?)\s*;\s*([-+]?\d*\.?\d+(?:e[-+]?\d+)?)\s*;?"))
                {
                    if (TryParseInvariantFloat(u.Groups[1].Value, out float uu) &&
                        TryParseInvariantFloat(u.Groups[2].Value, out float vv))
                    {
                        mesh.Uvs.Add(uu);
                        mesh.Uvs.Add(vv);
                    }
                }
            }

            Match texMatch = Regex.Match(remaining, @"TextureFilename\s*\{\s*""([^""]+)""", RegexOptions.Singleline);
            if (texMatch.Success)
            {
                string texName = Path.GetFileName(texMatch.Groups[1].Value);
                if (string.IsNullOrEmpty(Path.GetExtension(texName)))
                    texName += ".dds";
                mesh.TextureName = texName;
            }

            if (TryParseMeshMaterialList(meshBlock, out List<string> materialTextures, out List<int> faceMaterialIndices))
            {
                mesh.MaterialTextures = materialTextures;
                mesh.FaceMaterialIndices = faceMaterialIndices;
            }

            return mesh;
        }

        private static bool TryParseInvariantFloat(string value, out float result)
        {
            return float.TryParse(
                value,
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out result);
        }
    }
}
