using EasyCSharp.Core;
using Microsoft.Win32;
using System;
using System.IO;
using System.Security;
using System.Windows.Forms;

namespace ChocolateBox
{
    /// <summary>
    /// Manages application settings loaded from ChocolateBox.ini and the Windows registry.
    /// Handles Fable directory location and provides access to configuration values.
    /// </summary>
    internal static class Settings
    {
        private static string myFableDirectory;
        private static IniFile myIni;

        /// <summary>
        /// Gets the application's base directory (where the executable is located).
        /// </summary>
        public static string Directory =>
            AppDomain.CurrentDomain.BaseDirectory ?? Environment.CurrentDirectory;

        /// <summary>
        /// Gets the data directory path (typically "data\" under the application directory).
        /// </summary>
        public static string DataDirectory => Path.Combine(Directory, "data\\");

        /// <summary>
        /// Gets the Fable game installation directory path.
        /// This is loaded from the INI file or Windows registry on startup.
        /// </summary>
        public static string FableDirectory => myFableDirectory;

        /// <summary>
        /// Reads a string value from the INI file.
        /// </summary>
        /// <param name="section">The INI section name</param>
        /// <param name="key">The key name within the section</param>
        /// <returns>The string value, or empty string if not found</returns>
        public static string GetString(string section, string key)
        {
            return myIni?.ReadString(section, key) ?? string.Empty;
        }

        /// <summary>
        /// Reads a string value from the INI file with a default fallback.
        /// </summary>
        /// <param name="section">The INI section name</param>
        /// <param name="key">The key name within the section</param>
        /// <param name="defaultValue">Default value if key is not found</param>
        /// <returns>The string value, or the default if not found</returns>
        public static string GetString(string section, string key, string defaultValue)
        {
            return myIni?.ReadString(section, key, defaultValue) ?? defaultValue;
        }

        /// <summary>
        /// Reads a boolean value from the INI file with a default fallback.
        /// </summary>
        /// <param name="section">The INI section name</param>
        /// <param name="key">The key name within the section</param>
        /// <param name="defaultValue">Default value if key is not found</param>
        /// <returns>The boolean value, or the default if not found</returns>
        public static bool GetBool(string section, string key, bool defaultValue = false)
        {
            return myIni?.ReadBool(section, key, defaultValue) ?? defaultValue;
        }

        /// <summary>
        /// Reads a float value from the INI file.
        /// </summary>
        /// <param name="section">The INI section name</param>
        /// <param name="key">The key name within the section</param>
        /// <param name="defaultValue">Default value if key is not found (defaults to 0.0f)</param>
        /// <returns>The float value, or the default if not found</returns>
        public static float GetFloat(string section, string key, float defaultValue = 0.0f)
        {
            return myIni?.ReadFloat(section, key, defaultValue) ?? defaultValue;
        }

        /// <summary>
        /// Reads an integer value from the INI file.
        /// </summary>
        /// <param name="section">The INI section name</param>
        /// <param name="key">The key name within the section</param>
        /// <param name="defaultValue">Default value if key is not found (defaults to 0)</param>
        /// <returns>The integer value, or the default if not found</returns>
        public static int GetInt(string section, string key, int defaultValue = 0)
        {
            return myIni?.ReadInt(section, key, defaultValue) ?? defaultValue;
        }

        /// <summary>
        /// Loads settings from ChocolateBox.ini and locates the Fable installation directory.
        /// This method must be called during application startup before accessing any settings.
        ///
        /// The Fable directory is located using the following priority:
        /// 1. Path specified in the INI file under [Settings]/MyPath
        /// 2. Path from Windows registry (key specified in INI file under [Settings]/FableRegistry)
        /// 3. User selection via folder browser dialog
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if user cancels the folder selection dialog</exception>
        public static void Load()
        {
            // Try to locate ChocolateBox.ini
            string iniPath = Path.Combine(Directory, "ChocolateBox.ini");
            if (!File.Exists(iniPath))
            {
                // Try one level up if not found in BaseDirectory (might be in bin/Debug during development)
                string altPath = Path.Combine(
                    Path.GetDirectoryName(Directory.TrimEnd('\\')),
                    "ChocolateBox.ini");
                if (File.Exists(altPath))
                {
                    iniPath = altPath;
                }
            }

            // Load INI file
            try
            {
                myIni = new IniFile(iniPath);
            }
            catch
            {
                myIni = null;
            }

            // Try to load Fable directory from INI file
            try
            {
                myFableDirectory = GetString("Settings", "MyPath", string.Empty);
            }
            catch
            {
                myFableDirectory = string.Empty;
            }

            // If Fable directory not found or doesn't exist, try other methods
            if (string.IsNullOrEmpty(myFableDirectory) || !System.IO.Directory.Exists(myFableDirectory))
            {
                // Try to get Fable directory from Windows registry
                string registryPath = GetString("Settings", "FableRegistry", string.Empty);
                bool foundInRegistry = TryLoadFromRegistry(registryPath);

                // If not found in registry, prompt user to locate it
                if (!foundInRegistry)
                {
                    PromptUserForFableDirectory(registryPath);
                }
            }

            // Ensure directory path ends with backslash
            if (!myFableDirectory.EndsWith("\\"))
            {
                myFableDirectory += "\\";
            }
        }

        /// <summary>
        /// Attempts to load the Fable directory from the Windows registry.
        /// </summary>
        /// <param name="registryPath">Registry key path (e.g., "SOFTWARE\\Microsoft\\Fable")</param>
        /// <returns>True if successfully loaded from registry, false otherwise</returns>
        private static bool TryLoadFromRegistry(string registryPath)
        {
            if (string.IsNullOrEmpty(registryPath))
            {
                return false;
            }

            try
            {
                using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(registryPath, false))
                {
                    if (registryKey != null)
                    {
                        object value = registryKey.GetValue("SetupPath");
                        if (value != null)
                        {
                            myFableDirectory = value.ToString();
                            if (System.IO.Directory.Exists(myFableDirectory))
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            catch (SecurityException)
            {
                // Registry access denied - this is expected in some environments
            }

            return false;
        }

        /// <summary>
        /// Prompts the user to manually select the Fable installation directory.
        /// If a valid registry path is provided, the selection will be saved to the registry.
        /// </summary>
        /// <param name="registryPath">Optional registry path to save the selection</param>
        /// <exception cref="InvalidOperationException">Thrown if user cancels the dialog</exception>
        private static void PromptUserForFableDirectory(string registryPath)
        {
            using (FolderBrowserDialog folderDialog = new FolderBrowserDialog())
            {
                folderDialog.Description = "Locate Fable installation directory";

                if (folderDialog.ShowDialog() == DialogResult.OK)
                {
                    myFableDirectory = folderDialog.SelectedPath;

                    // Try to save to registry if registry path is specified
                    if (!string.IsNullOrEmpty(registryPath))
                    {
                        try
                        {
                            using (RegistryKey key = Registry.LocalMachine.CreateSubKey(registryPath))
                            {
                                key?.SetValue("SetupPath", myFableDirectory);
                            }
                        }
                        catch (SecurityException)
                        {
                            // Registry write failed - this is non-critical, user selection is still valid
                        }
                    }
                }
                else
                {
                    throw new InvalidOperationException("Unable to locate Fable directory. " +
                        "Please ensure Fable is installed or manually specify the installation directory.");
                }
            }
        }
    }
}
