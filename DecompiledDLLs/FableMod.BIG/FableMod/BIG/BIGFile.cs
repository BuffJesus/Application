// Decompiled with JetBrains decompiler
// Type: FableMod.BIG.BIGFile
// Assembly: FableMod.BIG, Version=1.0.4918.425, Culture=neutral, PublicKeyToken=null
// MVID: 88942552-073F-4D63-ADC6-04A8B51D93E5
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.BIG.dll

using FableMod.CLRCore;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Threading.Tasks;

#nullable disable
namespace FableMod.BIG;

public class BIGFile : AssetArchive
{
  private unsafe BIGHeader* m_SourceHeader;
  private unsafe BIGHeader* m_RecompileHeader;

  public unsafe BIGFile(int type)
  {
    // ISSUE: fault handler
    try
    {
      BIGHeader* bigHeaderPtr1 = (BIGHeader*) \u003CModule\u003E.@new(16UL /*0x10*/);
      BIGHeader* bigHeaderPtr2;
      // ISSUE: fault handler
      try
      {
        if ((IntPtr) bigHeaderPtr1 != IntPtr.Zero)
        {
          // ISSUE: initblk instruction
          __memset((IntPtr) bigHeaderPtr1, 0, 16 /*0x10*/);
          bigHeaderPtr2 = bigHeaderPtr1;
        }
        else
          bigHeaderPtr2 = (BIGHeader*) 0L;
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) bigHeaderPtr1);
      }
      this.m_SourceHeader = bigHeaderPtr2;
      *(sbyte*) bigHeaderPtr2 = (sbyte) 66;
      *(sbyte*) this.m_SourceHeader = (sbyte) 73;
      *(sbyte*) this.m_SourceHeader = (sbyte) 71;
      *(sbyte*) this.m_SourceHeader = (sbyte) 66;
      *(int*) ((IntPtr) this.m_SourceHeader + 4L) = 101;
      *(int*) ((IntPtr) this.m_SourceHeader + 12L) = type;
      this.m_Banks = new Collection<AssetBank>();
    }
    __fault
    {
      base.Dispose(true);
    }
  }

  public BIGFile()
  {
  }

  public unsafe void Load(string filename, ProgressInterface progress)
  {
    this.m_OriginalFileName = filename;
    BIGHeader* bigHeaderPtr1 = (BIGHeader*) \u003CModule\u003E.@new(16UL /*0x10*/);
    BIGHeader* bigHeaderPtr2;
    // ISSUE: fault handler
    try
    {
      if ((IntPtr) bigHeaderPtr1 != IntPtr.Zero)
      {
        // ISSUE: initblk instruction
        __memset((IntPtr) bigHeaderPtr1, 0, 16 /*0x10*/);
        bigHeaderPtr2 = bigHeaderPtr1;
      }
      else
        bigHeaderPtr2 = (BIGHeader*) 0L;
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) bigHeaderPtr1);
    }
    this.m_SourceHeader = bigHeaderPtr2;
    this.m_Banks = new Collection<AssetBank>();
    FileStream File = File.Open(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
    this.m_ArchiveFile = File;
    int num1 = (int) FileControl.Read(File, (void*) this.m_SourceHeader, 16U /*0x10*/);
    this.m_ArchiveFile.Position = (long) (uint) *(int*) ((IntPtr) this.m_SourceHeader + 8L);
    uint steps = 0;
    
    byte[] stepsBytes = new byte[4];
    this.m_ArchiveFile.Read(stepsBytes, 0, 4);
    steps = BitConverter.ToUInt32(stepsBytes, 0);

    progress?.Begin((int) steps);
    uint num3 = 0;
    if (0U < steps)
    {
      int progressStep = (int)steps / 100;
      if (progressStep == 0) progressStep = 1;

      // Pass 1: Sequentially read bank headers (fast)
      List<BIGBank> bankList = new List<BIGBank>();
      for (uint i = 0; i < steps; i++)
      {
          BIGBank bigBank = new BIGBank();
          bigBank.Load(this, (uint)this.m_ArchiveFile.Position, null, false);
          bankList.Add(bigBank);
          this.m_Banks.Add(bigBank);
      }

      // Pass 2: Multi-threaded load of entries (slow I/O)
      int banksDone = 0;
      object syncLock = new object();
      Parallel.ForEach(bankList, new ParallelOptions { MaxDegreeOfParallelism = Environment.ProcessorCount }, bank => 
      {
          using (FileStream bankFile = File.Open(this.m_OriginalFileName, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
          {
              bank.LoadEntries(bankFile, null);
          }
          
          int done = System.Threading.Interlocked.Increment(ref banksDone);
          if (done % progressStep == 0)
          {
              lock (syncLock)
              {
                  if (progress is Progress p)
                      p.StepInfo = $"(Loaded bank {done} of {steps}: {bank.Name})";
                  progress?.Update();
              }
          }
      });
    }
    if (progress is Progress pFinal)
        pFinal.StepInfo = "";
    progress?.End();
  }

  private unsafe void \u007EBIGFile()
  {
    this.CloseFile();
    BIGHeader* sourceHeader = this.m_SourceHeader;
    if ((IntPtr) sourceHeader != IntPtr.Zero)
      \u003CModule\u003E.delete((void*) sourceHeader);
    BIGHeader* recompileHeader = this.m_RecompileHeader;
    if ((IntPtr) recompileHeader == IntPtr.Zero)
      return;
    \u003CModule\u003E.delete((void*) recompileHeader);
  }

  public override unsafe void Save(string newfile, ProgressInterface progress)
  {
    FileStream fileStream = !(this.m_OriginalFileName == newfile) ? File.Create(newfile) : throw new Exception("Use a different filename");
    int blockSize = (int) ((BIGBank) base.get_Banks(0)).BlockSize;
    ulong num1 = (ulong) blockSize;
    int count = (int) (16UL /*0x10*/ / num1);
    if (16UL /*0x10*/ % num1 != 0UL)
      count += blockSize;
    FileControl.WriteNull(fileStream, count);
    progress?.Begin(this.BankCount);
    int index1 = 0;
    if (0 < this.BankCount)
    {
      do
      {
        ((BIGBank) base.get_Banks(index1)).RecompileWriteContent(fileStream, progress);
        ++index1;
      }
      while (index1 < this.BankCount);
    }
    progress?.End();
    int index2 = 0;
    if (0 < this.BankCount)
    {
      do
      {
        ((BIGBank) base.get_Banks(index2)).RecompileWriteEntries(fileStream, (ProgressInterface) null);
        ++index2;
      }
      while (index2 < this.BankCount);
    }
    BIGHeader* recompileHeader = this.m_RecompileHeader;
    if ((IntPtr) recompileHeader != IntPtr.Zero)
      \u003CModule\u003E.delete((void*) recompileHeader);
    BIGHeader* bigHeaderPtr1 = (BIGHeader*) \u003CModule\u003E.@new(16UL /*0x10*/);
    BIGHeader* bigHeaderPtr2;
    if ((IntPtr) bigHeaderPtr1 != IntPtr.Zero)
    {
      // ISSUE: cpblk instruction
      __memcpy((IntPtr) bigHeaderPtr1, (IntPtr) this.m_SourceHeader, 16 /*0x10*/);
      bigHeaderPtr2 = bigHeaderPtr1;
    }
    else
      bigHeaderPtr2 = (BIGHeader*) 0L;
    this.m_RecompileHeader = bigHeaderPtr2;
    *(int*) ((IntPtr) this.m_RecompileHeader + 8L) = (int) fileStream.Position;
    uint bankCount = (uint) this.BankCount;
    int num2 = (int) FileControl.Write(fileStream, (void*) &bankCount, 4U);
    int index3 = 0;
    if (0 < this.BankCount)
    {
      do
      {
        ((BIGBank) base.get_Banks(index3)).RecompileWriteHeader(fileStream, (ProgressInterface) null);
        ++index3;
      }
      while (index3 < this.BankCount);
    }
    fileStream.Position = 0L;
    int num3 = (int) FileControl.Write(fileStream, (void*) this.m_RecompileHeader, 16U /*0x10*/);
    fileStream.Close();
  }

  public override unsafe int ContentType
  {
    get => *(int*) ((IntPtr) this.m_SourceHeader + 12L);
    set => *(int*) ((IntPtr) this.m_SourceHeader + 12L) = value;
  }

  public BIGBank get_Banks(string name)
  {
    int index = 0;
    if (0 < this.BankCount)
    {
      while (!(((BIGBank) base.get_Banks(index)).Name == name))
      {
        ++index;
        if (index >= this.BankCount)
          goto label_4;
      }
      return (BIGBank) base.get_Banks(index);
    }
label_4:
    return (BIGBank) null;
  }

  public BIGBank get_Banks(int index) => (BIGBank) base.get_Banks(index);

  public BIGBank FindBankByID(uint id)
  {
    int index = 0;
    if (0 < this.BankCount)
    {
      BIGBank bankById;
      do
      {
        bankById = (BIGBank) base.get_Banks(index);
        if ((int) bankById.ID != (int) id)
          ++index;
        else
          goto label_3;
      }
      while (index < this.BankCount);
      goto label_4;
label_3:
      return bankById;
    }
label_4:
    return (BIGBank) null;
  }

  public BIGBank FindBankByName(string name) => this.get_Banks(name);

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EBIGFile();
      }
      finally
      {
        base.Dispose(true);
      }
    }
    else
      base.Dispose(false);
  }
}
