// Decompiled with JetBrains decompiler
// Type: FableMod.BIN.BINEntry
// Assembly: FableMod.BIN, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 7B343E30-1A4D-49C7-A3B2-33514A983F5F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.BIN.dll

using FableMod.Data;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.BIN;

public class BINEntry : IDisposable
{
  private bool m_Modified;
  private uint m_NameEnum;
  private unsafe sbyte* m_Data;
  private int m_Length;
  private string m_Definition;
  private string m_Name;
  private BINFile m_BIN;

  private unsafe void \u007EBINEntry()
  {
    \u003CModule\u003E.delete((void*) this.m_Data);
    this.m_Length = 0;
    this.m_Data = (sbyte*) 0L;
    this.m_Definition = (string) null;
    this.m_Name = (string) null;
  }

  public bool Modified
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_Modified;
    [param: MarshalAs(UnmanagedType.U1)] set => this.m_Modified = value;
  }

  public int Length => this.m_Length;

  public bool IsXBox
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_BIN.IsXBox;
  }

  public uint NameEnum => this.m_NameEnum;

  public int ID => this.m_BIN.GetEntryIndex(this);

  public string Definition
  {
    get => this.m_Definition;
    set => this.m_Definition = value;
  }

  public unsafe string Name
  {
    get => this.m_Name;
    set
    {
      this.m_Name = value;
      IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi(value);
      this.m_NameEnum = uint.MaxValue - ZLib.CRC32(uint.MaxValue, hglobalAnsi.ToPointer(), this.m_Name.Length);
      Marshal.FreeHGlobal(hglobalAnsi);
    }
  }

  public unsafe byte[] Data
  {
    get
    {
      int length = this.m_Length;
      byte[] data = new byte[length];
      int index1 = 0;
      long index2 = 0;
      if (0 < length)
      {
        do
        {
          data[index1] = (byte) this.m_Data[index2];
          ++index1;
          ++index2;
        }
        while (index1 < this.m_Length);
      }
      return data;
    }
    set
    {
      fixed (byte* data = &value[0])
        this.SetData((sbyte*) data, (uint) value.Length);
    }
  }

  public unsafe void SetData(sbyte* data, uint length)
  {
    this.m_Modified = true;
    \u003CModule\u003E.delete((void*) this.m_Data);
    ulong num = (ulong) length;
    void* voidPtr = \u003CModule\u003E.@new(num);
    this.m_Data = (sbyte*) voidPtr;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) voidPtr, (IntPtr) data, (long) num);
    this.m_Length = (int) length;
  }

  public unsafe sbyte* GetData() => this.m_Data;

  internal unsafe BINEntry(BINFile bin, string definition, string name, sbyte* data, int datalen)
  {
    this.m_BIN = bin;
    this.m_Modified = false;
    this.Name = name;
    this.m_Definition = definition;
    ulong num = (ulong) datalen;
    void* voidPtr = \u003CModule\u003E.@new(num);
    this.m_Data = (sbyte*) voidPtr;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) voidPtr, (IntPtr) data, (long) num);
    this.m_Length = datalen;
  }

  internal unsafe BINEntry(
    BINFile bin,
    string definition,
    string name,
    uint nameEnum,
    sbyte* data,
    int datalen)
  {
    this.m_BIN = bin;
    this.m_Modified = false;
    this.m_Name = name;
    this.m_NameEnum = nameEnum;
    this.m_Definition = definition;
    ulong num = (ulong) datalen;
    void* voidPtr = \u003CModule\u003E.@new(num);
    this.m_Data = (sbyte*) voidPtr;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) voidPtr, (IntPtr) data, (long) num);
    this.m_Length = datalen;
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EBINEntry();
    }
    else
    {
      // ISSUE: explicit finalizer call
      this.Finalize();
    }
  }

  public virtual void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }
}
