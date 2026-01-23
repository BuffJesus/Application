// Decompiled with JetBrains decompiler
// Type: ChocolateBox.Settings
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using EasyCSharp.Core;
using Microsoft.Win32;
using System;
using System.IO;
using System.Security;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

internal class Settings
{
  private static string myFableDirectory;
  private static IniFile myIni;

  public static string Directory
  {
    get => AppDomain.CurrentDomain.BaseDirectory ?? Environment.CurrentDirectory;
  }

  public static string DataDirectory => Settings.Directory + "data\\";

  public static string FableDirectory => Settings.myFableDirectory;

  public static string GetString(string Section, string Key)
  {
    return Settings.myIni == null ? "" : Settings.myIni.ReadString(Section, Key);
  }

  public static string GetString(string section, string key, string def)
  {
    return Settings.myIni == null ? "" : Settings.myIni.ReadString(section, key, def);
  }

  public static bool GetBool(string Section, string Key, bool Default)
  {
    return Settings.myIni != null && Settings.myIni.ReadBool(Section, Key, Default);
  }

  public static bool GetBool(string Section, string Key) => Settings.GetBool(Section, Key, false);

  public static float GetFloat(string Section, string Key)
  {
    return Settings.myIni == null ? 0.0f : Settings.myIni.ReadFloat(Section, Key);
  }

  public static float GetFloat(string Section, string Key, float Default)
  {
    return Settings.myIni == null ? Default : Settings.myIni.ReadFloat(Section, Key, Default);
  }

  public static int GetInt(string Section, string Key)
  {
    return Settings.myIni == null ? 0 : Settings.myIni.ReadInt(Section, Key);
  }

  public static int GetInt(string Section, string Key, int Default)
  {
    return Settings.myIni == null ? Default : Settings.myIni.ReadInt(Section, Key, Default);
  }

  public static void Load()
  {
    string iniPath = Path.Combine(Settings.Directory, "ChocolateBox.ini");
    if (!File.Exists(iniPath))
    {
        // Try one level up if not found in BaseDirectory (might be in bin/Debug)
        string altPath = Path.Combine(Path.GetDirectoryName(Settings.Directory.TrimEnd('\\')), "ChocolateBox.ini");
        if (File.Exists(altPath)) iniPath = altPath;
    }
    try
    {
        Settings.myIni = new IniFile(iniPath);
    }
    catch { Settings.myIni = null; }

    try
    {
        Settings.myFableDirectory = Settings.GetString(nameof(Settings), "MyPath", "");
    }
    catch { Settings.myFableDirectory = ""; }

    if (string.IsNullOrEmpty(Settings.myFableDirectory) || !System.IO.Directory.Exists(Settings.myFableDirectory))
    {
      string str = "";
      try
      {
          str = Settings.GetString(nameof(Settings), "FableRegistry", "");
      }
      catch { }
      bool registryPathFound = false;
      if (!string.IsNullOrEmpty(str))
      {
          try
          {
            RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(str, false);
            if (registryKey != null)
            {
              object val = registryKey.GetValue("SetupPath");
              if (val != null)
              {
                  Settings.myFableDirectory = val.ToString();
                  if (System.IO.Directory.Exists(Settings.myFableDirectory))
                      registryPathFound = true;
              }
            }
          }
          catch (SecurityException)
          {
          }
      }

      if (!registryPathFound)
      {
        using (FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog())
        {
            folderBrowserDialog.Description = "Locate Fable directory";
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
              Settings.myFableDirectory = folderBrowserDialog.SelectedPath;
              if (!string.IsNullOrEmpty(str))
              {
                  try
                  {
                      Registry.LocalMachine.CreateSubKey(str).SetValue("SetupPath", (object) Settings.myFableDirectory);
                  }
                  catch (SecurityException) { }
              }
            }
            else
            {
              throw new InvalidOperationException("Unable to locate Fable directory.");
            }
        }
      }
    }
    if (Settings.myFableDirectory.EndsWith("\\"))
      return;
    Settings.myFableDirectory += "\\";
  }
}
