// Decompiled with JetBrains decompiler
// Type: FableMod.BBB.BBBFile
// Assembly: FableMod.BBB, Version=1.0.4918.427, Culture=neutral, PublicKeyToken=null
// MVID: E6F7EB8A-26AC-4E0A-8433-40351F83A480
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.BBB.dll

using FableMod.CLRCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.BBB;

public class BBBFile : IDisposable
{
  protected List<BBBEntry> m_Entries;
  protected unsafe BBBHeader* m_Header;
  protected FileStream m_File;

  public unsafe BBBFile()
  {
    this.m_Entries = new List<BBBEntry>();
    BBBHeader* bbbHeaderPtr1 = (BBBHeader*) \u003CModule\u003E.@new(32UL /*0x20*/);
    BBBHeader* bbbHeaderPtr2;
    if ((IntPtr) bbbHeaderPtr1 != IntPtr.Zero)
    {
      // ISSUE: initblk instruction
      __memset((IntPtr) bbbHeaderPtr1, 0, 32 /*0x20*/);
      bbbHeaderPtr2 = bbbHeaderPtr1;
    }
    else
      bbbHeaderPtr2 = (BBBHeader*) 0L;
    this.m_Header = bbbHeaderPtr2;
  }

  private unsafe void \u007EBBBFile()
  {
    FileStream file = this.m_File;
    if (file != null)
    {
      file.Close();
      this.m_File = (FileStream) null;
    }
    this.m_Entries.Clear();
    this.m_Entries.Clear();
    if (this.m_Entries is IDisposable entries)
      entries.Dispose();
    \u003CModule\u003E.delete((void*) this.m_Header);
  }

  public unsafe void Open(string fileName, ProgressInterface progress)
  {
    FileStream file = this.m_File;
    if (file != null)
    {
      file.Close();
      this.m_File = (FileStream) null;
    }
    this.m_Entries.Clear();
    FileStream File = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
    this.m_File = File;
    int num1 = (int) FileControl.Read(File, (void*) this.m_Header, 32U /*0x20*/);
    this.m_File.Position = (long) (uint) *(int*) ((IntPtr) this.m_Header + 28L);
    progress?.Begin(*(int*) ((IntPtr) this.m_Header + 20L));
    BBBDevHeader bbbDevHeader;
    int num2 = (int) FileControl.Read(this.m_File, (void*) &bbbDevHeader, 12U);
    uint num3 = 0;
    if (0U < (uint) *(int*) ((IntPtr) this.m_Header + 24L))
    {
      do
      {
        BBBEntry entry = this.CreateEntry();
        entry.ReadHeader(this.m_File);
        this.m_Entries.Add(entry);
        ++num3;
      }
      while (num3 < (uint) *(int*) ((IntPtr) this.m_Header + 24L));
    }
    progress?.End();
  }

  public void Close()
  {
    FileStream file = this.m_File;
    if (file != null)
    {
      file.Close();
      this.m_File = (FileStream) null;
    }
    this.m_Entries.Clear();
  }

  public unsafe void ExtractFile(string path, BBBEntry entry)
  {
    string path1 = Path.Combine(path, entry.FileName);
    Directory.CreateDirectory(Path.GetDirectoryName(path1));
    FileStream File = new FileStream(path1, FileMode.Create, FileAccess.Write, FileShare.None);
    this.m_File.Position = (long) entry.FileOffset;
    uint uiCount = (uint) *(int*) ((IntPtr) this.m_Header + 16L /*0x10*/);
    uint fileSize = entry.FileSize;
    byte* pBuffer = (byte*) \u003CModule\u003E.@new((ulong) uiCount);
    do
    {
      int num1 = (int) FileControl.Read(this.m_File, (void*) pBuffer, uiCount);
      if (fileSize >= uiCount)
      {
        int num2 = (int) FileControl.Write(File, (void*) pBuffer, uiCount);
        fileSize -= uiCount;
      }
      else
        goto label_3;
    }
    while (fileSize > 0U);
    goto label_4;
label_3:
    int num = (int) FileControl.Write(File, (void*) pBuffer, fileSize);
label_4:
    \u003CModule\u003E.delete((void*) pBuffer);
    File.Close();
  }

  public byte[] ExtractData(BBBEntry entry)
  {
    this.m_File.Position = (long) entry.FileOffset;
    byte[] array = new byte[(int) entry.FileSize];
    this.m_File.Read(array, 0, (int) entry.FileSize);
    return array;
  }

  public int EntryCount => this.m_Entries.Count;

  public BBBEntry get_Entries(string name)
  {
    int index = 0;
    if (0 < this.m_Entries.Count)
    {
      while (string.Compare(this.m_Entries[index].FileName, name, true) != 0)
      {
        ++index;
        if (index >= this.m_Entries.Count)
          goto label_4;
      }
      return this.m_Entries[index];
    }
label_4:
    return (BBBEntry) null;
  }

  public BBBEntry get_Entries(int index) => this.m_Entries[index];

  protected FileStream File => this.m_File;

  protected virtual BBBEntry CreateEntry() => new BBBEntry();

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EBBBFile();
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
