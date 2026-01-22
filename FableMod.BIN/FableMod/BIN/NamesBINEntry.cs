// Decompiled with JetBrains decompiler
// Type: FableMod.BIN.NamesBINEntry
// Assembly: FableMod.BIN, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 7B343E30-1A4D-49C7-A3B2-33514A983F5F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.BIN.dll

using FableMod.Data;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.BIN;

public class NamesBINEntry
{
  private uint m_Offset;
  private uint m_Enum;
  private string m_Name;

  public uint Offset => this.m_Offset;

  public uint Enum => this.m_Enum;

  public string Name => this.m_Name;

  internal unsafe NamesBINEntry(uint offset, string name)
  {
    this.m_Offset = offset;
    IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi(name);
    this.m_Enum = uint.MaxValue - ZLib.CRC32(uint.MaxValue, hglobalAnsi.ToPointer(), name.Length);
    Marshal.FreeHGlobal(hglobalAnsi);
    this.m_Name = name;
  }

  internal unsafe NamesBINEntry(uint offset, uint enumval, sbyte* name)
  {
    this.m_Offset = offset;
    this.m_Enum = enumval;
    this.m_Name = new string(name);
  }

  internal unsafe NamesBINEntry(uint offset, sbyte* name)
  {
    this.m_Offset = offset;
    sbyte* numPtr = name;
    if (*name != (sbyte) 0)
    {
      do
      {
        ++numPtr;
      }
      while (*numPtr != (sbyte) 0);
    }
    long count = (long) ((IntPtr) numPtr - (IntPtr) name);
    this.m_Enum = uint.MaxValue - ZLib.CRC32(uint.MaxValue, (void*) name, (int) count);
    this.m_Name = new string(name);
  }
}
