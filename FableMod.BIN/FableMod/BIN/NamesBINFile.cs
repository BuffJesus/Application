// Decompiled with JetBrains decompiler
// Type: FableMod.BIN.NamesBINFile
// Assembly: FableMod.BIN, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 7B343E30-1A4D-49C7-A3B2-33514A983F5F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.BIN.dll

using FableMod.CLRCore;
using System;
using System.Collections;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.BIN;

public class NamesBINFile : IDisposable
{
  private bool m_Modified;
  private string m_OriginalFileName;
  private unsafe NamesBINHeader* m_Header;
  private ArrayList m_Entries;
  private int m_Count;
  private IComparer m_SearchComparer;

  private unsafe void \u007ENamesBINFile()
  {
    this.m_Entries.Clear();
    this.m_Entries = (ArrayList) null;
    this.m_SearchComparer = (IComparer) null;
    \u003CModule\u003E.delete((void*) this.m_Header);
  }

  public unsafe void Load(string filename, ProgressInterface progress)
  {
    this.m_OriginalFileName = filename;
    FileStream File = File.Open(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
    sbyte* pBuffer = (sbyte*) \u003CModule\u003E.@new((ulong) File.Length);
    int num1 = (int) FileControl.Read(File, (void*) pBuffer, (uint) (int) File.Length);
    File.Close();
    void* voidPtr = \u003CModule\u003E.@new(20UL);
    this.m_Header = (NamesBINHeader*) voidPtr;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) voidPtr, (IntPtr) pBuffer, 20);
    int num2 = 20;
    int capacity = *(int*) ((IntPtr) this.m_Header + 8L);
    this.m_Count = capacity;
    this.m_Entries = new ArrayList(capacity);
    progress?.Begin(this.m_Count);
    int num3 = 0;
    if (0 < this.m_Count)
    {
      do
      {
        uint enumval;
        // ISSUE: cpblk instruction
        __memcpy(ref enumval, (long) num2 + (IntPtr) pBuffer, 4);
        int num4 = (int) ((long) num2 + 4L);
        sbyte* name = (sbyte*) ((long) num4 + (IntPtr) pBuffer);
        this.m_Entries.Add((object) new NamesBINEntry((uint) (num4 - 20), enumval, name));
        sbyte* numPtr1 = name;
        sbyte* numPtr2 = numPtr1;
        if (*numPtr1 != (sbyte) 0)
        {
          do
          {
            ++numPtr2;
          }
          while (*numPtr2 != (sbyte) 0);
        }
        num2 = (int) ((IntPtr) numPtr2 - (IntPtr) numPtr1 + (long) num4 + 1L);
        progress?.Update();
        ++num3;
      }
      while (num3 < this.m_Count);
    }
    if (*(int*) ((IntPtr) this.m_Header + 4L) == -1461490636)
    {
      this.m_Entries.Sort(0, this.m_Entries.Count, (IComparer) new EntryOffsetComparer());
      this.m_SearchComparer = (IComparer) new EntryOffsetSearch();
    }
    progress?.End();
    \u003CModule\u003E.delete((void*) pBuffer);
  }

  public unsafe void Save(string filename)
  {
    sbyte* pBuffer = (sbyte*) \u003CModule\u003E.@new((ulong) (uint) (((NamesBINEntry) this.m_Entries[this.m_Count - 1]).Name.Length + (int) ((NamesBINEntry) this.m_Entries[this.m_Count - 1]).Offset + 25));
    *(int*) ((IntPtr) this.m_Header + 8L) = this.m_Count;
    *(int*) ((IntPtr) this.m_Header + 12L) = ((NamesBINEntry) this.m_Entries[this.m_Count - 1]).Name.Length + (int) ((NamesBINEntry) this.m_Entries[this.m_Count - 1]).Offset + 5;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) pBuffer, (IntPtr) this.m_Header, 20);
    int uiCount = 20;
    int index = 0;
    if (0 < this.m_Count)
    {
      do
      {
        IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi(((NamesBINEntry) this.m_Entries[index]).Name);
        uint num1 = ((NamesBINEntry) this.m_Entries[index]).Enum;
        // ISSUE: cpblk instruction
        __memcpy((long) uiCount + (IntPtr) pBuffer, ref num1, 4);
        int num2 = uiCount + 4;
        void* pointer = hglobalAnsi.ToPointer();
        long num3 = (long) ((long) num2 - (IntPtr) pointer + (IntPtr) pBuffer);
        sbyte num4;
        do
        {
          num4 = *(sbyte*) pointer;
          *(sbyte*) (num3 + (IntPtr) pointer) = num4;
          ++pointer;
        }
        while (num4 != (sbyte) 0);
        NamesBINEntry entry = (NamesBINEntry) this.m_Entries[index];
        uiCount = num2 + (entry.Name.Length + 1);
        Marshal.FreeHGlobal(hglobalAnsi);
        ++index;
      }
      while (index < this.m_Count);
    }
    FileStream File = File.Create(filename);
    int num = (int) FileControl.Write(File, (void*) pBuffer, (uint) uiCount);
    File.Close();
    \u003CModule\u003E.delete((void*) pBuffer);
  }

  public NamesBINEntry get_Entries(int index) => (NamesBINEntry) this.m_Entries[index];

  public string OriginalFileName => this.m_OriginalFileName;

  public int EntryCount => this.m_Count;

  public bool Modified
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_Modified;
    [param: MarshalAs(UnmanagedType.U1)] set => this.m_Modified = value;
  }

  public unsafe bool IsPC
  {
    [return: MarshalAs(UnmanagedType.U1)] get
    {
      return *(int*) ((IntPtr) this.m_Header + 4L) == -1461490636;
    }
  }

  public NamesBINEntry AddEntry(string name)
  {
    this.m_Modified = true;
    NamesBINEntry namesBinEntry = new NamesBINEntry((uint) (((NamesBINEntry) this.m_Entries[this.m_Count - 1]).Name.Length + (int) ((NamesBINEntry) this.m_Entries[this.m_Count - 1]).Offset + 5), name);
    this.m_Entries.Add((object) namesBinEntry);
    ++this.m_Count;
    return namesBinEntry;
  }

  public NamesBINEntry GetEntryByEnum(uint enumval)
  {
    int index = 0;
    if (0 < this.m_Count)
    {
      while ((int) ((NamesBINEntry) this.m_Entries[index]).Enum != (int) enumval)
      {
        ++index;
        if (index >= this.m_Count)
          goto label_4;
      }
      return (NamesBINEntry) this.m_Entries[index];
    }
label_4:
    return (NamesBINEntry) null;
  }

  public NamesBINEntry GetEntryByOffset(uint offset)
  {
    int index = this.m_Entries.BinarySearch(0, this.m_Entries.Count, (object) offset, this.m_SearchComparer);
    return index >= 0 ? (NamesBINEntry) this.m_Entries[index] : (NamesBINEntry) null;
  }

  public NamesBINEntry GetEntryByName(string name)
  {
    int index = 0;
    if (0 < this.m_Count)
    {
      while (!(((NamesBINEntry) this.m_Entries[index]).Name == name))
      {
        ++index;
        if (index >= this.m_Count)
          goto label_4;
      }
      return (NamesBINEntry) this.m_Entries[index];
    }
label_4:
    return (NamesBINEntry) null;
  }

  public unsafe NamesBINEntry GetEntry(uint id)
  {
    return *(int*) ((IntPtr) this.m_Header + 4L) == -1461490636 ? this.GetEntryByOffset(id) : this.GetEntryByEnum(id);
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007ENamesBINFile();
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
