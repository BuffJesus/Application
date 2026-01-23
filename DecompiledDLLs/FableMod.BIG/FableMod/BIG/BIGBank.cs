// Decompiled with JetBrains decompiler
// Type: FableMod.BIG.BIGBank
// Assembly: FableMod.BIG, Version=1.0.4918.425, Culture=neutral, PublicKeyToken=null
// MVID: 88942552-073F-4D63-ADC6-04A8B51D93E5
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.BIG.dll

using \u003CCppImplementationDetails\u003E;
using FableMod.CLRCore;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.BIG;

public class BIGBank : AssetBank
{
  private string m_Name;
  private unsafe BIGBankHeader* m_SourceHeader;
  private unsafe BIGBankHeader* m_NewHeader;
  private unsafe BIGBankHeader* m_RecompileHeader;

  internal unsafe BIGBank(AssetArchive archive, uint startpos, ProgressInterface progress)
  {
    this.Load(archive, startpos, progress, true);
  }

  public unsafe BIGBank()
  {
  }

  internal unsafe void Load(AssetArchive archive, uint startpos, ProgressInterface progress, bool loadEntries)
  {
    // ISSUE: fault handler
    try
    {
      FileStream archiveFile = archive.ArchiveFile;
      this.m_Archive = archive;
      this.m_Entries = new Collection<AssetEntry>();
      archiveFile.Position = (long) startpos;
      this.m_SourceStartOffset = startpos;
      this.m_Name = "";
      List<byte> nameBytes = new List<byte>();
      int nextByte = archiveFile.ReadByte();
      while (nextByte != 0 && nextByte != 255 && nextByte != -1) 
      {
          nameBytes.Add((byte)nextByte);
          nextByte = archiveFile.ReadByte();
      }
      this.m_Name = System.Text.Encoding.ASCII.GetString(nameBytes.ToArray());

      BIGBankHeader* bigBankHeaderPtr = (BIGBankHeader*) \u003CModule\u003E.@new(20UL);
      BIGBankHeader* pBuffer;
      // ISSUE: fault handler
      try
      {
        if ((IntPtr) bigBankHeaderPtr != IntPtr.Zero)
        {
          // ISSUE: initblk instruction
          __memset((IntPtr) bigBankHeaderPtr, 0, 20);
          pBuffer = bigBankHeaderPtr;
        }
        else
          pBuffer = (BIGBankHeader*) 0L;
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) bigBankHeaderPtr);
      }
      this.m_SourceHeader = pBuffer;
      
      byte[] bankHeaderBytes = new byte[20];
      archiveFile.Read(bankHeaderBytes, 0, 20);
      Marshal.Copy(bankHeaderBytes, 0, (IntPtr)this.m_SourceHeader, 20);

      int position = (int) archiveFile.Position;
      this.m_SourceLength = (uint) position - startpos;

      if (loadEntries)
      {
          this.LoadEntries(archiveFile, progress);
      }
      
      archiveFile.Position = (long) position;
    }
    __fault
    {
      this.Dispose(true);
    }
  }

  internal unsafe void LoadEntries(FileStream archiveFile, ProgressInterface progress)
  {
      long originalPosition = archiveFile.Position;
      archiveFile.Position = (long) (uint) *(int*) ((IntPtr) this.m_SourceHeader + 8L);
      
      byte[] num2Bytes = new byte[4];
      archiveFile.Read(num2Bytes, 0, 4);
      uint num2 = BitConverter.ToUInt32(num2Bytes, 0);

      archiveFile.Position += (long) (num2 * 8U);
      int entryCount = *(int*) ((IntPtr) this.m_SourceHeader + 4L);
      progress?.Begin(entryCount);
      uint num4 = 0;
      if (0U < (uint) entryCount)
      {
        int progressStep = entryCount / 100;
        if (progressStep == 0) progressStep = 1;

        do
        {
          AssetEntry entry = new AssetEntry(this.m_Archive, (AssetBank) this, (uint) (int) archiveFile.Position);
          this.m_Entries.Add(entry);
          if (num4 % (uint)progressStep == 0)
          {
            if (progress is Progress p)
                p.StepInfo = $"(Entry {num4 + 1} of {entryCount}: {entry.DevSymbolName})";
            progress?.Update();
          }
          ++num4;
        }
        while (num4 < (uint) entryCount);
      }
      if (progress is Progress pFinal)
          pFinal.StepInfo = "";
      progress?.End();
      archiveFile.Position = originalPosition;
  }

  public unsafe BIGBank(string name, uint id, uint blocksize, AssetArchive archive)
  {
    // ISSUE: fault handler
    try
    {
      BIGBankHeader* bigBankHeaderPtr1 = (BIGBankHeader*) \u003CModule\u003E.@new(20UL);
      BIGBankHeader* bigBankHeaderPtr2;
      // ISSUE: fault handler
      try
      {
        if ((IntPtr) bigBankHeaderPtr1 != IntPtr.Zero)
        {
          // ISSUE: initblk instruction
          __memset((IntPtr) bigBankHeaderPtr1, 0, 20);
          bigBankHeaderPtr2 = bigBankHeaderPtr1;
        }
        else
          bigBankHeaderPtr2 = (BIGBankHeader*) 0L;
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) bigBankHeaderPtr1);
      }
      this.m_NewHeader = bigBankHeaderPtr2;
      *(int*) this.GetNewHeader() = (int) id;
      *(int*) ((IntPtr) this.GetNewHeader() + 16L /*0x10*/) = (int) blocksize;
      this.m_Name = name;
      this.m_Archive = archive;
      this.m_Entries = new Collection<AssetEntry>();
    }
    __fault
    {
      this.Dispose(true);
    }
  }

  public string Name
  {
    get => this.m_Name;
    set => this.m_Name = value;
  }

  public unsafe uint ID
  {
    get
    {
      BIGBankHeader* newHeader = this.m_NewHeader;
      return (IntPtr) newHeader != IntPtr.Zero ? (uint) *(int*) newHeader : (uint) *(int*) this.m_SourceHeader;
    }
    set => *(int*) this.GetNewHeader() = (int) value;
  }

  public unsafe uint BlockSize
  {
    get
    {
      BIGBankHeader* newHeader = this.m_NewHeader;
      return (IntPtr) newHeader != IntPtr.Zero ? (uint) *(int*) ((IntPtr) newHeader + 16L /*0x10*/) : (uint) *(int*) ((IntPtr) this.m_SourceHeader + 16L /*0x10*/);
    }
    set => *(int*) ((IntPtr) this.GetNewHeader() + 16L /*0x10*/) = (int) value;
  }

  public unsafe uint Length
  {
    get
    {
      BIGBankHeader* newHeader = this.m_NewHeader;
      return (IntPtr) newHeader != IntPtr.Zero ? (uint) *(int*) ((IntPtr) newHeader + 12L) : (uint) *(int*) ((IntPtr) this.m_SourceHeader + 12L);
    }
  }

  public unsafe uint EntryStartOffset
  {
    get
    {
      BIGBankHeader* newHeader = this.m_NewHeader;
      return (IntPtr) newHeader != IntPtr.Zero ? (uint) *(int*) ((IntPtr) newHeader + 8L) : (uint) *(int*) ((IntPtr) this.m_SourceHeader + 8L);
    }
  }

  public override unsafe int AddEntry(AssetEntry entry)
  {
    this.m_Modified = true;
    this.m_Entries.Add(entry);
    *(int*) ((IntPtr) this.GetNewHeader() + 4L) = this.m_Entries.Count;
    return this.m_Entries.IndexOf(entry);
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public override unsafe bool RemoveEntry(AssetEntry entry)
  {
    if (!base.RemoveEntry(entry))
      return false;
    *(int*) ((IntPtr) this.GetNewHeader() + 4L) = this.m_Entries.Count;
    return true;
  }

  public override void RecompileWriteContent(FileStream @out, ProgressInterface progress)
  {
    progress?.Begin(this.EntryCount);
    int index = 0;
    if (0 < this.EntryCount)
    {
      do
      {
        AssetEntry assetEntry = this.get_Entries(index);
        assetEntry.RecompileWriteData(@out);
        int num1 = (int) this.BlockSize - (int) (assetEntry.Length % this.BlockSize);
        if ((uint) num1 < this.BlockSize && 0 < num1)
        {
          int num2 = num1;
          do
          {
            @out.WriteByte((byte) 0);
            num2 += -1;
          }
          while (num2 != 0);
        }
        progress?.Update();
        ++index;
      }
      while (index < this.EntryCount);
    }
    progress?.End();
  }

  public override unsafe void RecompileWriteEntries(FileStream @out, ProgressInterface progress)
  {
    BIGBankHeader* recompileHeader1 = this.m_RecompileHeader;
    if ((IntPtr) recompileHeader1 != IntPtr.Zero)
      \u003CModule\u003E.delete((void*) recompileHeader1);
    if ((IntPtr) this.m_NewHeader != IntPtr.Zero)
    {
      BIGBankHeader* bigBankHeaderPtr1 = (BIGBankHeader*) \u003CModule\u003E.@new(20UL);
      BIGBankHeader* bigBankHeaderPtr2;
      if ((IntPtr) bigBankHeaderPtr1 != IntPtr.Zero)
      {
        // ISSUE: cpblk instruction
        __memcpy((IntPtr) bigBankHeaderPtr1, (IntPtr) this.m_NewHeader, 20);
        bigBankHeaderPtr2 = bigBankHeaderPtr1;
      }
      else
        bigBankHeaderPtr2 = (BIGBankHeader*) 0L;
      this.m_RecompileHeader = bigBankHeaderPtr2;
    }
    else
    {
      BIGBankHeader* bigBankHeaderPtr3 = (BIGBankHeader*) \u003CModule\u003E.@new(20UL);
      BIGBankHeader* bigBankHeaderPtr4;
      if ((IntPtr) bigBankHeaderPtr3 != IntPtr.Zero)
      {
        // ISSUE: cpblk instruction
        __memcpy((IntPtr) bigBankHeaderPtr3, (IntPtr) this.m_SourceHeader, 20);
        bigBankHeaderPtr4 = bigBankHeaderPtr3;
      }
      else
        bigBankHeaderPtr4 = (BIGBankHeader*) 0L;
      this.m_RecompileHeader = bigBankHeaderPtr4;
    }
    *(int*) ((IntPtr) this.m_RecompileHeader + 8L) = (int) @out.Position;
    if ((IntPtr) this.m_SourceHeader != IntPtr.Zero)
    {
      uint num1 = 0;
      \u0024ArrayType\u0024\u0024\u0024BY0EAA\u0040UTypeCountEntry\u0040BIG\u0040FableMod\u0040\u0040 entryBigFableMod;
      // ISSUE: initblk instruction
      __memset(ref entryBigFableMod, 0, 4096 /*0x1000*/);
      int index = 0;
      if (0 < this.EntryCount)
      {
        do
        {
          uint type = this.get_Entries(index).Type;
          int num2 = 0;
          long num3 = 0;
          if (0U < num1)
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            while (^(int&) (num3 * 8L + ref entryBigFableMod) != (int) type)
            {
              ++num2;
              ++num3;
              if ((uint) num2 >= num1)
                goto label_17;
            }
            // ISSUE: cast to a reference type
            ref \u0024ArrayType\u0024\u0024\u0024BY0EAA\u0040UTypeCountEntry\u0040BIG\u0040FableMod\u0040\u0040 local = num3 * 8L + (\u0024ArrayType\u0024\u0024\u0024BY0EAA\u0040UTypeCountEntry\u0040BIG\u0040FableMod\u0040\u0040&) ((IntPtr) &entryBigFableMod + 4);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ref local = ^(int&) ref local + 1;
            goto label_18;
          }
label_17:
          long num4 = (long) num1 * 8L;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) (num4 + ref entryBigFableMod) = (int) type;
          // ISSUE: cast to a reference type
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) (num4 + (\u0024ArrayType\u0024\u0024\u0024BY0EAA\u0040UTypeCountEntry\u0040BIG\u0040FableMod\u0040\u0040&) ((IntPtr) &entryBigFableMod + 4)) = ^(int&) (num4 + (\u0024ArrayType\u0024\u0024\u0024BY0EAA\u0040UTypeCountEntry\u0040BIG\u0040FableMod\u0040\u0040&) ((IntPtr) &entryBigFableMod + 4)) + 1;
          ++num1;
label_18:
          ++index;
        }
        while (index < this.EntryCount);
      }
      int num5 = (int) FileControl.Write(@out, (void*) &num1, 4U);
      int num6 = (int) FileControl.Write(@out, (void*) &entryBigFableMod, (uint) ((ulong) num1 * 8UL));
    }
    else
    {
      uint num7 = 0;
      int num8 = (int) FileControl.Write(@out, (void*) &num7, 4U);
    }
    progress?.Begin(this.EntryCount);
    int index1 = 0;
    if (0 < this.EntryCount)
    {
      do
      {
        this.get_Entries(index1).RecompileWriteHeader(@out);
        progress?.Update();
        ++index1;
      }
      while (index1 < this.EntryCount);
    }
    progress?.End();
    BIGBankHeader* recompileHeader2 = this.m_RecompileHeader;
    *(int*) ((IntPtr) recompileHeader2 + 12L) = (int) (@out.Position - (long) (uint) *(int*) ((IntPtr) recompileHeader2 + 8L));
  }

  public override unsafe void RecompileWriteHeader(FileStream @out, ProgressInterface progress)
  {
    if ((IntPtr) this.m_RecompileHeader == IntPtr.Zero)
    {
      if ((IntPtr) this.m_NewHeader != IntPtr.Zero)
      {
        BIGBankHeader* bigBankHeaderPtr1 = (BIGBankHeader*) \u003CModule\u003E.@new(20UL);
        BIGBankHeader* bigBankHeaderPtr2;
        if ((IntPtr) bigBankHeaderPtr1 != IntPtr.Zero)
        {
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) bigBankHeaderPtr1, (IntPtr) this.m_NewHeader, 20);
          bigBankHeaderPtr2 = bigBankHeaderPtr1;
        }
        else
          bigBankHeaderPtr2 = (BIGBankHeader*) 0L;
        this.m_RecompileHeader = bigBankHeaderPtr2;
      }
      else
      {
        BIGBankHeader* bigBankHeaderPtr3 = (BIGBankHeader*) \u003CModule\u003E.@new(20UL);
        BIGBankHeader* bigBankHeaderPtr4;
        if ((IntPtr) bigBankHeaderPtr3 != IntPtr.Zero)
        {
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) bigBankHeaderPtr3, (IntPtr) this.m_SourceHeader, 20);
          bigBankHeaderPtr4 = bigBankHeaderPtr3;
        }
        else
          bigBankHeaderPtr4 = (BIGBankHeader*) 0L;
        this.m_RecompileHeader = bigBankHeaderPtr4;
      }
    }
    IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi(this.m_Name);
    int num1 = (int) FileControl.Write(@out, hglobalAnsi.ToPointer(), (uint) (this.m_Name.Length + 1));
    Marshal.FreeHGlobal(hglobalAnsi);
    int num2 = (int) FileControl.Write(@out, (void*) this.m_RecompileHeader, 20U);
  }

  private unsafe BIGBankHeader* GetNewHeader()
  {
    if ((IntPtr) this.m_NewHeader == IntPtr.Zero)
    {
      BIGBankHeader* bigBankHeaderPtr1 = (BIGBankHeader*) \u003CModule\u003E.@new(20UL);
      BIGBankHeader* bigBankHeaderPtr2;
      if ((IntPtr) bigBankHeaderPtr1 != IntPtr.Zero)
      {
        // ISSUE: cpblk instruction
        __memcpy((IntPtr) bigBankHeaderPtr1, (IntPtr) this.m_SourceHeader, 20);
        bigBankHeaderPtr2 = bigBankHeaderPtr1;
      }
      else
        bigBankHeaderPtr2 = (BIGBankHeader*) 0L;
      this.m_NewHeader = bigBankHeaderPtr2;
    }
    return this.m_NewHeader;
  }
}
