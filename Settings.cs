// Decompiled with JetBrains decompiler
// Type: ChocolateBox.Settings
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using EasyCSharp.Core;
using Microsoft.Win32;
using System;
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
    Settings.myIni = new IniFile(Settings.Directory + "ChocolateBox.ini");
    Settings.myFableDirectory = Settings.GetString(nameof (Settings), "MyPath", "");
    if (string.IsNullOrEmpty(Settings.myFableDirectory))
    {
      string str = Settings.GetString(nameof (Settings), "FableRegistry", "");
      if (str == "")
        throw new Exception("Registry information missing from the settings file.");
      try
      {
        RegistryKey registryKey = Registry.LocalMachine.OpenSubKey(str, false);
        if (registryKey != null)
        {
          Settings.myFableDirectory = registryKey.GetValue("SetupPath").ToString();
        }
        else
        {
          FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
          folderBrowserDialog.Description = "Locate Fable directory";
          Settings.myFableDirectory = folderBrowserDialog.ShowDialog() == DialogResult.OK ? folderBrowserDialog.SelectedPath : throw new InvalidOperationException("Unable to locate registry information.");
          Registry.LocalMachine.CreateSubKey(str).SetValue("SetupPath", (object) Settings.myFableDirectory);
        }
      }
      catch (SecurityException ex)
      {
        throw new InvalidOperationException("Registry not available for the current user.");
      }
    }
    if (Settings.myFableDirectory.EndsWith("\\"))
      return;
    Settings.myFableDirectory += "\\";
  }
}
