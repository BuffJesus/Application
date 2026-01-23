using FableMod.BIG;
using FableMod.CLRCore;
using System;
using System.IO;
using System.Windows.Forms;

namespace ChocolateBox
{
    /// <summary>
    /// Controller for managing BIG archive files.
    /// BIG files are Fable's archive format containing multiple game assets (textures, models, etc.) organized into banks.
    /// </summary>
    /// <remarks>
    /// <para><b>BIG File Structure:</b></para>
    /// <code>
    /// BIG File
    /// ├── Bank 1 (e.g., "GBANK_MAIN_PC")
    /// │   ├── Asset Entry 1
    /// │   ├── Asset Entry 2
    /// │   └── ...
    /// ├── Bank 2 (e.g., "GBANK_GUI_PC")
    /// │   └── ...
    /// </code>
    ///
    /// <para><b>Common BIG Files:</b></para>
    /// <list type="bullet">
    /// <item><b>textures.big:</b> Game textures (GBANK_MAIN_PC, GBANK_GUI_PC)</item>
    /// <item><b>graphics.big:</b> 3D models and meshes</item>
    /// <item><b>levels.big:</b> Level/world data</item>
    /// </list>
    ///
    /// <para><b>Threading:</b></para>
    /// Form creation is marshaled to the UI thread to prevent cross-thread exceptions
    /// when loading large files (e.g., textures.big with 6000+ entries).
    /// </remarks>
    public class BIGFileController : FileController
    {
        private BIGFile myBIG;

        /// <summary>
        /// Initializes a new BIG file controller.
        /// </summary>
        /// <param name="database">The file database this controller belongs to</param>
        /// <param name="fileName">Path to the BIG file</param>
        public BIGFileController(FileDatabase database, string fileName)
            : base(database, fileName)
        {
        }

        /// <summary>
        /// Gets the underlying BIGFile instance.
        /// </summary>
        public BIGFile BIG => myBIG;

        /// <summary>
        /// Gets or sets whether the BIG file has been modified.
        /// </summary>
        public override bool Modified
        {
            get => FileLoaded ? myBIG.Modified : base.Modified;
            set => base.Modified = value;
        }

        /// <summary>
        /// Closes the BIG file and releases resources.
        /// </summary>
        /// <returns>True if successfully closed</returns>
        public override bool Close()
        {
            State &= ~(int)FileState.FileOK; // Clear file OK flag (bit 2)
            myBIG.Destroy();
            myBIG = null;
            return true;
        }

        /// <summary>
        /// Loads the BIG file from disk.
        /// </summary>
        /// <param name="progress">Progress tracker for long operations</param>
        /// <returns>True if successfully loaded, false on error</returns>
        protected override bool OnLoad(Progress progress)
        {
            try
            {
                Console.WriteLine($"[DEBUG] BIGFileController.OnLoad: Loading {FileName}");
                myBIG = new BIGFile();
                myBIG.Load(FileName, (ProgressInterface)progress);
                Console.WriteLine($"[DEBUG] BIGFileController.OnLoad: Load completed, BankCount={myBIG.BankCount}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[DEBUG] BIGFileController.OnLoad: Load failed: {ex.Message}");
                FormMain.Instance.ErrorMessage(ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// Saves the BIG file to disk.
        /// If saving to a different file, saves directly.
        /// If overwriting the same file, uses a temporary file to avoid corruption on failure.
        /// </summary>
        /// <param name="fileName">Target file path</param>
        /// <param name="progress">Progress tracker for long operations</param>
        /// <returns>False to indicate save completed (base class handles file state updates)</returns>
        protected override bool OnSave(string fileName, Progress progress)
        {
            if (fileName != FileName)
            {
                // Saving to a different file - save directly
                myBIG.Save(fileName, (ProgressInterface)progress);
            }
            else
            {
                // Overwriting same file - use temporary file for safety
                FileInfo fileInfo = new FileInfo(fileName);
                string tempPath = $"{fileInfo.DirectoryName}\\TMP_{fileInfo.Name}";

                myBIG.Save(tempPath, (ProgressInterface)progress);
                myBIG.Destroy();

                try
                {
                    File.Delete(fileName);
                    File.Move(tempPath, fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error replacing original file: {ex.Message}",
                        "Save Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                State = (int)FileState.InSystem;
            }
            return false;
        }

        /// <summary>
        /// Creates and displays the UI form for this BIG file.
        /// For texture files, creates a specialized FormTextureBIG.
        /// Form creation and tree building are done on the UI thread to prevent cross-thread exceptions.
        /// </summary>
        /// <param name="progress">Progress tracker for tree building</param>
        /// <returns>True if form created successfully</returns>
        protected override bool OnForm(Progress progress)
        {
            Console.WriteLine($"[DEBUG] BIGFileController.OnForm: Creating form");
            bool isTextures = myBIG == FileDatabase.Instance.Textures;

            // Create form and build on UI thread to avoid cross-thread issues
            // This is critical for large files like textures.big (6000+ entries)
            FormBIG form = null;
            FormMain.Instance.Invoke(new Action(() =>
            {
                form = isTextures ? (FormBIG)new FormTextureBIG() : new FormBIG();
                Console.WriteLine($"[DEBUG] BIGFileController.OnForm: Form created, calling Build()");
                form.Build(myBIG, progress);
                Console.WriteLine($"[DEBUG] BIGFileController.OnForm: Build completed");
            }));

            Console.WriteLine($"[DEBUG] BIGFileController.OnForm: Calling CreateForm()");
            CreateForm((FormFileController)form);
            Console.WriteLine($"[DEBUG] BIGFileController.OnForm: CreateForm completed");
            return true;
        }
    }
}
