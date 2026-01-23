// Decompiled with JetBrains decompiler
// Type: EasyCSharp.Core.IniFile
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace EasyCSharp.Core;

public class IniFile
{
  private string myPath;

  [DllImport("kernel32")]
  private static extern long WritePrivateProfileString(
    string section,
    string key,
    string val,
    string filePath);

  [DllImport("kernel32")]
  private static extern int GetPrivateProfileString(
    string section,
    string key,
    string def,
    StringBuilder retVal,
    int size,
    string filePath);

  public IniFile(string inipath) => this.myPath = inipath;

  public void WriteString(string Section, string Key, string Value)
  {
    IniFile.WritePrivateProfileString(Section, Key, Value, this.myPath);
  }

  public void WriteInt(string Section, string Key, int Value)
  {
    this.WriteString(Section, Key, Value.ToString());
  }

  public void WriteFloat(string Section, string Key, float Value)
  {
    CultureInfo provider = new CultureInfo("en-US");
    this.WriteString(Section, Key, Value.ToString((IFormatProvider) provider));
  }

  public void WriteBool(string Section, string Key, bool Value)
  {
    string str = Value ? "true" : "false";
    this.WriteString(Section, Key, str);
  }

  public string ReadString(string Section, string Key) => this.ReadString(Section, Key, "");

  public string ReadString(string Section, string Key, string Default)
  {
    try
    {
      StringBuilder retVal = new StringBuilder(128 /*0x80*/);
      IniFile.GetPrivateProfileString(Section, Key, Default, retVal, 128 /*0x80*/, this.myPath);
      return retVal.ToString();
    }
    catch (Exception)
    {
      return Default;
    }
  }

  public int ReadInt(string Section, string Key, int Default)
  {
    string s = this.ReadString(Section, Key);
    if (s.Length == 0)
      return Default;
    try
    {
      return int.Parse(s);
    }
    catch (Exception ex)
    {
    }
    return Default;
  }

  public int ReadInt(string Section, string Key) => this.ReadInt(Section, Key, 0);

  public float ReadFloat(string Section, string Key, float Default)
  {
    if (this.ReadString(Section, Key).Length == 0)
      return Default;
    CultureInfo provider = new CultureInfo("en-US");
    try
    {
      return float.Parse(this.ReadString(Section, Key), (IFormatProvider) provider);
    }
    catch (Exception ex)
    {
    }
    return Default;
  }

  public float ReadFloat(string Section, string Key) => this.ReadFloat(Section, Key, 0.0f);

  public bool ReadBool(string Section, string Key, bool Default)
  {
    string str = this.ReadString(Section, Key);
    if (str.Length == 0)
      return Default;
    return str.ToUpper().Equals("TRUE") || str.Equals("1");
  }

  public bool ReadBool(string Section, string Key) => this.ReadBool(Section, Key, false);

  public string Path
  {
    get => this.myPath;
    set => this.myPath = value;
  }
}
