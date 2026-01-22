// Decompiled with JetBrains decompiler
// Type: FableMod.BIG.AssetEntry
// Assembly: FableMod.BIG, Version=1.0.4918.425, Culture=neutral, PublicKeyToken=null
// MVID: 88942552-073F-4D63-ADC6-04A8B51D93E5
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.BIG.dll

using FableMod.CLRCore;
using System;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.BIG;

public class AssetEntry : AssetArchiveItem, IDisposable
{
  protected AssetBank m_Bank;
  protected bool m_Modified;
  protected unsafe AssetEntryHeader* m_SourceHeader;
  protected unsafe AssetEntryHeader* m_NewHeader;
  protected unsafe AssetEntryHeader* m_RecompileHeader;
  protected unsafe sbyte* m_Data;

  public unsafe AssetEntry(AssetArchive archive, AssetBank bank, uint startpos)
  {
    this.m_NewHeader = (AssetEntryHeader*) 0L;
    if (archive != bank.Archive)
      throw new Exception();
    this.m_Bank = bank;
    this.m_Archive = archive;
    this.m_SourceStartOffset = startpos;
    this.LoadFromArchive();
    this.m_Modified = false;
  }

  public AssetEntry(string symbolname, uint id, uint type, AssetBank bank)
  {
    this.New(symbolname, id, type, bank);
  }

  public unsafe AssetEntry(AssetEntry entry, AssetBank bank)
  {
    if ((IntPtr) entry.m_NewHeader != IntPtr.Zero)
    {
      AssetEntryHeader* assetEntryHeaderPtr1 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
      AssetEntryHeader* assetEntryHeaderPtr2;
      // ISSUE: fault handler
      try
      {
        assetEntryHeaderPtr2 = (IntPtr) assetEntryHeaderPtr1 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr1, entry.m_NewHeader);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) assetEntryHeaderPtr1);
      }
      this.m_NewHeader = assetEntryHeaderPtr2;
    }
    else
    {
      AssetEntryHeader* assetEntryHeaderPtr3 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
      AssetEntryHeader* assetEntryHeaderPtr4;
      // ISSUE: fault handler
      try
      {
        assetEntryHeaderPtr4 = (IntPtr) assetEntryHeaderPtr3 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr3, entry.m_SourceHeader);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) assetEntryHeaderPtr3);
      }
      this.m_NewHeader = assetEntryHeaderPtr4;
    }
    AssetEntryHeader* newHeader = entry.m_NewHeader;
    uint len = (IntPtr) newHeader == IntPtr.Zero ? (uint) *(int*) ((IntPtr) entry.m_SourceHeader + 12L) : (uint) *(int*) ((IntPtr) newHeader + 12L);
    this.SetData(entry.GetData(), len);
    this.m_Bank = bank;
    this.m_Archive = bank.Archive;
    this.m_Modified = false;
  }

  private unsafe void \u007EAssetEntry()
  {
    AssetEntryHeader* newHeader = this.m_NewHeader;
    if ((IntPtr) newHeader != IntPtr.Zero)
    {
      AssetEntryHeader* assetEntryHeaderPtr = newHeader;
      \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bdtor\u007D(assetEntryHeaderPtr);
      \u003CModule\u003E.delete((void*) assetEntryHeaderPtr);
      this.m_NewHeader = (AssetEntryHeader*) 0L;
    }
    AssetEntryHeader* recompileHeader = this.m_RecompileHeader;
    if ((IntPtr) recompileHeader != IntPtr.Zero)
    {
      AssetEntryHeader* assetEntryHeaderPtr = recompileHeader;
      \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bdtor\u007D(assetEntryHeaderPtr);
      \u003CModule\u003E.delete((void*) assetEntryHeaderPtr);
      this.m_RecompileHeader = (AssetEntryHeader*) 0L;
    }
    AssetEntryHeader* sourceHeader = this.m_SourceHeader;
    if ((IntPtr) sourceHeader != IntPtr.Zero)
    {
      AssetEntryHeader* assetEntryHeaderPtr = sourceHeader;
      \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bdtor\u007D(assetEntryHeaderPtr);
      \u003CModule\u003E.delete((void*) assetEntryHeaderPtr);
      this.m_SourceHeader = (AssetEntryHeader*) 0L;
    }
    sbyte* data = this.m_Data;
    if ((IntPtr) data == IntPtr.Zero)
      return;
    \u003CModule\u003E.delete((void*) data);
    this.m_Data = (sbyte*) 0L;
  }

  public unsafe void New(string symbolname, uint id, uint type, AssetBank bank)
  {
    AssetEntryHeader* assetEntryHeaderPtr1 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
    AssetEntryHeader* assetEntryHeaderPtr2;
    // ISSUE: fault handler
    try
    {
      assetEntryHeaderPtr2 = (IntPtr) assetEntryHeaderPtr1 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr1);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) assetEntryHeaderPtr1);
    }
    this.m_NewHeader = assetEntryHeaderPtr2;
    this.ID = id;
    this.Type = type;
    this.DevSymbolName = symbolname;
    byte[] numArray = new byte[0];
    if (numArray.Length > 0)
    {
      fixed (byte* data = &numArray[0])
      {
        // ISSUE: variable of a reference type
        byte* local;
        // ISSUE: fault handler
        try
        {
          this.SetData((sbyte*) data, (uint) numArray.Length);
        }
        __fault
        {
          // ISSUE: cast to a reference type
          local = (byte*) 0L;
        }
        // ISSUE: cast to a reference type
        local = (byte*) 0L;
      }
    }
    this.m_Bank = bank;
    this.m_Archive = bank.Archive;
    this.m_Modified = true;
  }

  public void Detach() => this.m_Bank?.RemoveEntry(this);

  public unsafe void Purge()
  {
    if (this.m_Modified)
      return;
    sbyte* data = this.m_Data;
    if ((IntPtr) data == IntPtr.Zero)
      return;
    \u003CModule\u003E.delete((void*) data);
    this.m_Data = (sbyte*) 0L;
  }

  public unsafe sbyte* GetData()
  {
    if ((IntPtr) this.m_Data == IntPtr.Zero)
    {
      AssetEntryHeader* newHeader1 = this.m_NewHeader;
      this.m_Data = (sbyte*) \u003CModule\u003E.@new((IntPtr) newHeader1 == IntPtr.Zero ? (ulong) (uint) *(int*) ((IntPtr) this.m_SourceHeader + 12L) : (ulong) (uint) *(int*) ((IntPtr) newHeader1 + 12L));
      AssetEntryHeader* newHeader2 = this.m_NewHeader;
      this.m_Archive.ArchiveFile.Position = (IntPtr) newHeader2 == IntPtr.Zero ? (long) (uint) *(int*) ((IntPtr) this.m_SourceHeader + 16L /*0x10*/) : (long) (uint) *(int*) ((IntPtr) newHeader2 + 16L /*0x10*/);
      AssetEntryHeader* newHeader3 = this.m_NewHeader;
      int num = (int) FileControl.Read(this.m_Archive.ArchiveFile, (void*) this.m_Data, (IntPtr) newHeader3 == IntPtr.Zero ? (uint) *(int*) ((IntPtr) this.m_SourceHeader + 12L) : (uint) *(int*) ((IntPtr) newHeader3 + 12L));
      this.m_Modified = false;
    }
    return this.m_Data;
  }

  public unsafe byte[] Data
  {
    get
    {
      sbyte* data1 = this.GetData();
      AssetEntryHeader* newHeader = this.m_NewHeader;
      byte[] data2 = new byte[(IntPtr) newHeader == IntPtr.Zero ? *(int*) ((IntPtr) this.m_SourceHeader + 12L) : *(int*) ((IntPtr) newHeader + 12L)];
      int index = 0;
      if (0U < this.Length)
      {
        sbyte* numPtr = data1;
        do
        {
          data2[index] = (byte) *numPtr;
          ++index;
          ++numPtr;
        }
        while ((uint) index < this.Length);
      }
      return data2;
    }
    set
    {
      if (value.Length <= 0)
        return;
      fixed (byte* data1 = &value[0])
      {
        byte* local;
        try
        {
          this.SetData((sbyte*) data1, (uint) value.Length);
        }
        __fault
        {
          local = (byte*) 0L;
        }
        local = (byte*) 0L;
      }
    }
  }

  public unsafe void Copy(AssetEntry srcEntry)
  {
    AssetEntryHeader* newHeader1 = srcEntry.m_NewHeader;
    this.DevSymbolName = (IntPtr) newHeader1 == IntPtr.Zero ? new string((sbyte*) *(long*) ((IntPtr) srcEntry.m_SourceHeader + 28L)) : new string((sbyte*) *(long*) ((IntPtr) newHeader1 + 28L));
    AssetEntryHeader* newHeader2 = srcEntry.m_NewHeader;
    this.DevCRC = (IntPtr) newHeader2 == IntPtr.Zero ? (uint) *(int*) ((IntPtr) srcEntry.m_SourceHeader + 36L) : (uint) *(int*) ((IntPtr) newHeader2 + 36L);
    AssetEntryHeader* newHeader3 = srcEntry.m_NewHeader;
    this.DevFileType = (IntPtr) newHeader3 == IntPtr.Zero ? (uint) *(int*) ((IntPtr) srcEntry.m_SourceHeader + 20L) : (uint) *(int*) ((IntPtr) newHeader3 + 20L);
    this.DevSources = srcEntry.DevSources;
    AssetEntryHeader* newHeader4 = srcEntry.m_NewHeader;
    this.Type = (IntPtr) newHeader4 == IntPtr.Zero ? (uint) *(int*) ((IntPtr) srcEntry.m_SourceHeader + 8L) : (uint) *(int*) ((IntPtr) newHeader4 + 8L);
    AssetEntryHeader* newHeader5 = srcEntry.m_NewHeader;
    uint len = (IntPtr) newHeader5 == IntPtr.Zero ? (uint) *(int*) ((IntPtr) srcEntry.m_SourceHeader + 12L) : (uint) *(int*) ((IntPtr) newHeader5 + 12L);
    this.SetData(srcEntry.GetData(), len);
  }

  public unsafe void SetData(sbyte* data, uint len)
  {
    \u003CModule\u003E.delete((void*) this.m_Data);
    ulong num = (ulong) len;
    sbyte* numPtr = (sbyte*) \u003CModule\u003E.@new(num);
    this.m_Data = numPtr;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) numPtr, (IntPtr) data, (long) num);
    if ((IntPtr) this.m_NewHeader == IntPtr.Zero)
    {
      AssetEntryHeader* assetEntryHeaderPtr1 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
      AssetEntryHeader* assetEntryHeaderPtr2;
      // ISSUE: fault handler
      try
      {
        assetEntryHeaderPtr2 = (IntPtr) assetEntryHeaderPtr1 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr1, this.m_SourceHeader);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) assetEntryHeaderPtr1);
      }
      this.m_NewHeader = assetEntryHeaderPtr2;
    }
    *(int*) ((IntPtr) this.m_NewHeader + 12L) = (int) len;
    this.m_Modified = true;
  }

  public unsafe sbyte* GetSubHeader()
  {
    AssetEntryHeader* newHeader = this.m_NewHeader;
    return (IntPtr) newHeader != IntPtr.Zero ? (sbyte*) *(long*) ((IntPtr) newHeader + 56L) : (sbyte*) *(long*) ((IntPtr) this.m_SourceHeader + 56L);
  }

  public unsafe uint GetSubHeaderLength()
  {
    AssetEntryHeader* newHeader = this.m_NewHeader;
    return (IntPtr) newHeader != IntPtr.Zero ? (uint) *(int*) ((IntPtr) newHeader + 52L) : (uint) *(int*) ((IntPtr) this.m_SourceHeader + 52L);
  }

  public unsafe void SetSubHeader(sbyte* data, uint len)
  {
    if ((IntPtr) this.m_NewHeader == IntPtr.Zero)
    {
      AssetEntryHeader* assetEntryHeaderPtr1 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
      AssetEntryHeader* assetEntryHeaderPtr2;
      // ISSUE: fault handler
      try
      {
        assetEntryHeaderPtr2 = (IntPtr) assetEntryHeaderPtr1 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr1, this.m_SourceHeader);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) assetEntryHeaderPtr1);
      }
      this.m_NewHeader = assetEntryHeaderPtr2;
    }
    \u003CModule\u003E.delete((void*) *(long*) ((IntPtr) this.m_NewHeader + 56L));
    if (len > 0U)
    {
      ulong num = (ulong) len;
      *(long*) ((IntPtr) this.m_NewHeader + 56L) = (long) \u003CModule\u003E.@new(num);
      // ISSUE: cpblk instruction
      __memcpy(*(long*) ((IntPtr) this.m_NewHeader + 56L), (IntPtr) data, (long) num);
      *(int*) ((IntPtr) this.m_NewHeader + 52L) = (int) len;
    }
    else
    {
      *(long*) ((IntPtr) this.m_NewHeader + 56L) = 0L;
      *(int*) ((IntPtr) this.m_NewHeader + 52L) = 0;
    }
  }

  public unsafe byte[] SubHeader
  {
    get
    {
      AssetEntryHeader* newHeader = this.m_NewHeader;
      uint length = (IntPtr) newHeader == IntPtr.Zero ? (uint) *(int*) ((IntPtr) this.m_SourceHeader + 52L) : (uint) *(int*) ((IntPtr) newHeader + 52L);
      if (length <= 0U)
        return (byte[]) null;
      sbyte* numPtr1 = (IntPtr) newHeader == IntPtr.Zero ? (sbyte*) *(long*) ((IntPtr) this.m_SourceHeader + 56L) : (sbyte*) *(long*) ((IntPtr) newHeader + 56L);
      byte[] subHeader = new byte[(int) length];
      int index = 0;
      if (0U < length)
      {
        sbyte* numPtr2 = numPtr1;
        do
        {
          subHeader[index] = (byte) *numPtr2;
          ++index;
          ++numPtr2;
        }
        while ((uint) index < length);
      }
      return subHeader;
    }
    set
    {
      if (value != null && value.Length != 0)
      {
        fixed (byte* data1 = &value[0])
        {
          byte* local;
          try
          {
            this.SetSubHeader((sbyte*) data1, (uint) value.Length);
          }
          __fault
          {
            local = (byte*) 0L;
          }
          local = (byte*) 0L;
        }
      }
      else
        this.SetSubHeader((sbyte*) 0L, 0U);
    }
  }

  public unsafe uint MagicNumber
  {
    get
    {
      AssetEntryHeader* newHeader = this.m_NewHeader;
      return (IntPtr) newHeader != IntPtr.Zero ? (uint) *(int*) newHeader : (uint) *(int*) this.m_SourceHeader;
    }
  }

  public unsafe uint ID
  {
    get
    {
      AssetEntryHeader* newHeader = this.m_NewHeader;
      return (IntPtr) newHeader != IntPtr.Zero ? (uint) *(int*) ((IntPtr) newHeader + 4L) : (uint) *(int*) ((IntPtr) this.m_SourceHeader + 4L);
    }
    set
    {
      if ((IntPtr) this.m_NewHeader == IntPtr.Zero)
      {
        AssetEntryHeader* assetEntryHeaderPtr1 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
        AssetEntryHeader* assetEntryHeaderPtr2;
        try
        {
          assetEntryHeaderPtr2 = (IntPtr) assetEntryHeaderPtr1 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr1, this.m_SourceHeader);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) assetEntryHeaderPtr1);
        }
        this.m_NewHeader = assetEntryHeaderPtr2;
      }
      *(int*) ((IntPtr) this.m_NewHeader + 4L) = (int) value;
    }
  }

  public unsafe uint Type
  {
    get
    {
      AssetEntryHeader* newHeader = this.m_NewHeader;
      return (IntPtr) newHeader != IntPtr.Zero ? (uint) *(int*) ((IntPtr) newHeader + 8L) : (uint) *(int*) ((IntPtr) this.m_SourceHeader + 8L);
    }
    set
    {
      if ((IntPtr) this.m_NewHeader == IntPtr.Zero)
      {
        AssetEntryHeader* assetEntryHeaderPtr1 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
        AssetEntryHeader* assetEntryHeaderPtr2;
        try
        {
          assetEntryHeaderPtr2 = (IntPtr) assetEntryHeaderPtr1 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr1, this.m_SourceHeader);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) assetEntryHeaderPtr1);
        }
        this.m_NewHeader = assetEntryHeaderPtr2;
      }
      *(int*) ((IntPtr) this.m_NewHeader + 8L) = (int) value;
    }
  }

  public unsafe uint Length
  {
    get
    {
      AssetEntryHeader* newHeader = this.m_NewHeader;
      return (IntPtr) newHeader != IntPtr.Zero ? (uint) *(int*) ((IntPtr) newHeader + 12L) : (uint) *(int*) ((IntPtr) this.m_SourceHeader + 12L);
    }
    set
    {
      if ((IntPtr) this.m_NewHeader == IntPtr.Zero)
      {
        AssetEntryHeader* assetEntryHeaderPtr1 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
        AssetEntryHeader* assetEntryHeaderPtr2;
        try
        {
          assetEntryHeaderPtr2 = (IntPtr) assetEntryHeaderPtr1 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr1, this.m_SourceHeader);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) assetEntryHeaderPtr1);
        }
        this.m_NewHeader = assetEntryHeaderPtr2;
      }
      *(int*) ((IntPtr) this.m_NewHeader + 12L) = (int) value;
    }
  }

  public unsafe uint StartOffset
  {
    get
    {
      AssetEntryHeader* newHeader = this.m_NewHeader;
      return (IntPtr) newHeader != IntPtr.Zero ? (uint) *(int*) ((IntPtr) newHeader + 16L /*0x10*/) : (uint) *(int*) ((IntPtr) this.m_SourceHeader + 16L /*0x10*/);
    }
  }

  public unsafe uint DevFileType
  {
    get
    {
      AssetEntryHeader* newHeader = this.m_NewHeader;
      return (IntPtr) newHeader != IntPtr.Zero ? (uint) *(int*) ((IntPtr) newHeader + 20L) : (uint) *(int*) ((IntPtr) this.m_SourceHeader + 20L);
    }
    set
    {
      if ((IntPtr) this.m_NewHeader == IntPtr.Zero)
      {
        AssetEntryHeader* assetEntryHeaderPtr1 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
        AssetEntryHeader* assetEntryHeaderPtr2;
        try
        {
          assetEntryHeaderPtr2 = (IntPtr) assetEntryHeaderPtr1 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr1, this.m_SourceHeader);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) assetEntryHeaderPtr1);
        }
        this.m_NewHeader = assetEntryHeaderPtr2;
      }
      *(int*) ((IntPtr) this.m_NewHeader + 20L) = (int) value;
    }
  }

  public unsafe string DevSymbolName
  {
    get
    {
      AssetEntryHeader* newHeader = this.m_NewHeader;
      return (IntPtr) newHeader != IntPtr.Zero ? new string((sbyte*) *(long*) ((IntPtr) newHeader + 28L)) : new string((sbyte*) *(long*) ((IntPtr) this.m_SourceHeader + 28L));
    }
    set
    {
      if ((IntPtr) this.m_NewHeader == IntPtr.Zero)
      {
        AssetEntryHeader* assetEntryHeaderPtr1 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
        AssetEntryHeader* assetEntryHeaderPtr2;
        try
        {
          assetEntryHeaderPtr2 = (IntPtr) assetEntryHeaderPtr1 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr1, this.m_SourceHeader);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) assetEntryHeaderPtr1);
        }
        this.m_NewHeader = assetEntryHeaderPtr2;
      }
      \u003CModule\u003E.delete((void*) *(long*) ((IntPtr) this.m_NewHeader + 28L));
      IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi(value);
      *(long*) ((IntPtr) this.m_NewHeader + 28L) = (long) \u003CModule\u003E.@new((ulong) (value.Length + 1));
      void* pointer = hglobalAnsi.ToPointer();
      long num1 = *(long*) ((IntPtr) this.m_NewHeader + 28L);
      sbyte num2;
      do
      {
        num2 = *(sbyte*) pointer;
        *(sbyte*) num1 = num2;
        ++pointer;
        ++num1;
      }
      while (num2 != (sbyte) 0);
      Marshal.FreeHGlobal(hglobalAnsi);
      *(int*) ((IntPtr) this.m_NewHeader + 24L) = value.Length;
    }
  }

  public unsafe uint DevCRC
  {
    get
    {
      AssetEntryHeader* newHeader = this.m_NewHeader;
      return (IntPtr) newHeader != IntPtr.Zero ? (uint) *(int*) ((IntPtr) newHeader + 36L) : (uint) *(int*) ((IntPtr) this.m_SourceHeader + 36L);
    }
    set
    {
      if ((IntPtr) this.m_NewHeader == IntPtr.Zero)
      {
        AssetEntryHeader* assetEntryHeaderPtr1 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
        AssetEntryHeader* assetEntryHeaderPtr2;
        try
        {
          assetEntryHeaderPtr2 = (IntPtr) assetEntryHeaderPtr1 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr1, this.m_SourceHeader);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) assetEntryHeaderPtr1);
        }
        this.m_NewHeader = assetEntryHeaderPtr2;
      }
      *(int*) ((IntPtr) this.m_NewHeader + 36L) = (int) value;
    }
  }

  public unsafe string[] DevSources
  {
    get
    {
      AssetEntryHeader* newHeader = this.m_NewHeader;
      AssetEntryHeader* assetEntryHeaderPtr1 = (IntPtr) newHeader == IntPtr.Zero ? this.m_SourceHeader : newHeader;
      int length = *(int*) ((IntPtr) assetEntryHeaderPtr1 + 40L);
      string[] devSources = new string[length];
      int index = 0;
      if (0U < (uint) length)
      {
        AssetEntryHeader* assetEntryHeaderPtr2 = (AssetEntryHeader*) ((IntPtr) assetEntryHeaderPtr1 + 44L);
        long num = 0;
        do
        {
          IntPtr ptr = new IntPtr((void*) *(long*) (num + *(long*) assetEntryHeaderPtr2 + 4L));
          devSources[index] = Marshal.PtrToStringAnsi(ptr);
          ++index;
          num += 12L;
        }
        while ((uint) index < (uint) *(int*) ((IntPtr) assetEntryHeaderPtr1 + 40L));
      }
      return devSources;
    }
    set
    {
      if ((IntPtr) this.m_NewHeader == IntPtr.Zero)
      {
        AssetEntryHeader* assetEntryHeaderPtr1 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
        AssetEntryHeader* assetEntryHeaderPtr2;
        try
        {
          assetEntryHeaderPtr2 = (IntPtr) assetEntryHeaderPtr1 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr1, this.m_SourceHeader);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) assetEntryHeaderPtr1);
        }
        this.m_NewHeader = assetEntryHeaderPtr2;
      }
      uint num1 = (uint) *(int*) ((IntPtr) this.m_NewHeader + 40L);
      if (num1 != 0U)
      {
        int num2 = 0;
        if (0U < num1)
        {
          long num3 = 0;
          do
          {
            \u003CModule\u003E.delete((void*) *(long*) (num3 + *(long*) ((IntPtr) this.m_NewHeader + 44L) + 4L));
            ++num2;
            num3 += 12L;
          }
          while ((uint) num2 < (uint) *(int*) ((IntPtr) this.m_NewHeader + 40L));
        }
        \u003CModule\u003E.delete((void*) *(long*) ((IntPtr) this.m_NewHeader + 44L));
      }
      *(int*) ((IntPtr) this.m_NewHeader + 40L) = value.Length;
      ulong length = (ulong) value.Length;
      *(long*) ((IntPtr) this.m_NewHeader + 44L) = (long) \u003CModule\u003E.@new(length > 1537228672809129301UL ? ulong.MaxValue : length * 12UL);
      int index = 0;
      if (0U >= (uint) *(int*) ((IntPtr) this.m_NewHeader + 40L))
        return;
      long num4 = 0;
      do
      {
        *(int*) (*(long*) ((IntPtr) this.m_NewHeader + 44L) + num4) = value[index].Length;
        IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi(value[index]);
        sbyte* numPtr = (sbyte*) \u003CModule\u003E.@new((ulong) (value[index].Length + 1));
        *(long*) (num4 + *(long*) ((IntPtr) this.m_NewHeader + 44L) + 4L) = (long) numPtr;
        void* pointer = hglobalAnsi.ToPointer();
        long num5 = *(long*) (num4 + *(long*) ((IntPtr) this.m_NewHeader + 44L) + 4L);
        sbyte num6;
        do
        {
          num6 = *(sbyte*) pointer;
          *(sbyte*) num5 = num6;
          ++pointer;
          ++num5;
        }
        while (num6 != (sbyte) 0);
        Marshal.FreeHGlobal(hglobalAnsi);
        ++index;
        num4 += 12L;
      }
      while ((uint) index < (uint) *(int*) ((IntPtr) this.m_NewHeader + 40L));
    }
  }

  public unsafe bool Modified
  {
    [return: MarshalAs(UnmanagedType.U1)] get
    {
      return (IntPtr) this.m_NewHeader != IntPtr.Zero || this.m_Modified;
    }
  }

  public AssetBank Bank => this.m_Bank;

  public override unsafe string ToString()
  {
    if (this.m_Bank.GetType() == typeof (BIGBank))
    {
      AssetEntryHeader* newHeader1 = this.m_NewHeader;
      string str = (IntPtr) newHeader1 == IntPtr.Zero ? new string((sbyte*) *(long*) ((IntPtr) this.m_SourceHeader + 28L)) : new string((sbyte*) *(long*) ((IntPtr) newHeader1 + 28L));
      AssetBank bank = this.m_Bank;
      AssetEntryHeader* newHeader2 = this.m_NewHeader;
      return $"{(ValueType) (uint) ((IntPtr) newHeader2 == IntPtr.Zero ? *(int*) ((IntPtr) this.m_SourceHeader + 4L) : *(int*) ((IntPtr) newHeader2 + 4L))}::{((BIGBank) bank).Name}::{str}";
    }
    AssetEntryHeader* newHeader3 = this.m_NewHeader;
    string str1 = (IntPtr) newHeader3 == IntPtr.Zero ? new string((sbyte*) *(long*) ((IntPtr) this.m_SourceHeader + 28L)) : new string((sbyte*) *(long*) ((IntPtr) newHeader3 + 28L));
    AssetEntryHeader* newHeader4 = this.m_NewHeader;
    return $"{(ValueType) (uint) ((IntPtr) newHeader4 == IntPtr.Zero ? *(int*) ((IntPtr) this.m_SourceHeader + 4L) : *(int*) ((IntPtr) newHeader4 + 4L))}::{"UNKNOWN"}::{str1}";
  }

  public unsafe void RecompileWriteHeader(FileStream @out)
  {
    if ((IntPtr) this.m_RecompileHeader == IntPtr.Zero)
    {
      if ((IntPtr) this.m_NewHeader != IntPtr.Zero)
      {
        AssetEntryHeader* assetEntryHeaderPtr1 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
        AssetEntryHeader* assetEntryHeaderPtr2;
        // ISSUE: fault handler
        try
        {
          assetEntryHeaderPtr2 = (IntPtr) assetEntryHeaderPtr1 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr1, this.m_NewHeader);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) assetEntryHeaderPtr1);
        }
        this.m_RecompileHeader = assetEntryHeaderPtr2;
      }
      else
      {
        AssetEntryHeader* assetEntryHeaderPtr3 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
        AssetEntryHeader* assetEntryHeaderPtr4;
        // ISSUE: fault handler
        try
        {
          assetEntryHeaderPtr4 = (IntPtr) assetEntryHeaderPtr3 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr3, this.m_SourceHeader);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) assetEntryHeaderPtr3);
        }
        this.m_RecompileHeader = assetEntryHeaderPtr4;
      }
    }
    AssetEntryHeader* recompileHeader1 = this.m_RecompileHeader;
    int num1 = *(int*) ((IntPtr) recompileHeader1 + 24L) + 40;
    uint num2 = (uint) *(int*) ((IntPtr) recompileHeader1 + 40L);
    if (0U < num2)
    {
      long num3 = *(long*) ((IntPtr) recompileHeader1 + 44L);
      uint num4 = num2;
      do
      {
        num1 += *(int*) num3 + 4;
        num3 += 12L;
        num4 += uint.MaxValue;
      }
      while (num4 > 0U);
    }
    sbyte* pBuffer = (sbyte*) \u003CModule\u003E.@new((ulong) (num1 + *(int*) ((IntPtr) recompileHeader1 + 52L)));
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) pBuffer, (IntPtr) this.m_RecompileHeader, 28);
    AssetEntryHeader* recompileHeader2 = this.m_RecompileHeader;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) (pBuffer + 28L), *(long*) ((IntPtr) recompileHeader2 + 28L), (long) (uint) *(int*) ((IntPtr) recompileHeader2 + 24L));
    AssetEntryHeader* recompileHeader3 = this.m_RecompileHeader;
    int num5 = *(int*) ((IntPtr) recompileHeader3 + 24L) + 28;
    // ISSUE: cpblk instruction
    __memcpy((long) num5 + (IntPtr) pBuffer, (IntPtr) recompileHeader3 + 36L, 8);
    int num6 = num5 + 8;
    int num7 = 0;
    AssetEntryHeader* recompileHeader4 = this.m_RecompileHeader;
    if (0U < (uint) *(int*) ((IntPtr) recompileHeader4 + 40L))
    {
      long num8 = *(long*) ((IntPtr) recompileHeader4 + 44L);
      long num9 = 0;
      do
      {
        // ISSUE: cpblk instruction
        __memcpy((long) num6 + (IntPtr) pBuffer, num9 + num8, 4);
        int num10 = num6 + 4;
        long num11 = *(long*) ((IntPtr) this.m_RecompileHeader + 44L) + num9;
        // ISSUE: cpblk instruction
        __memcpy((long) num10 + (IntPtr) pBuffer, *(long*) (num11 + 4L), (long) (uint) *(int*) num11);
        num8 = *(long*) ((IntPtr) this.m_RecompileHeader + 44L);
        num6 = num10 + *(int*) (num9 + num8);
        ++num7;
        num9 += 12L;
      }
      while ((uint) num7 < (uint) *(int*) ((IntPtr) this.m_RecompileHeader + 40L));
    }
    // ISSUE: cpblk instruction
    __memcpy((long) num6 + (IntPtr) pBuffer, (IntPtr) this.m_RecompileHeader + 52L, 4);
    int num12 = num6 + 4;
    AssetEntryHeader* recompileHeader5 = this.m_RecompileHeader;
    // ISSUE: cpblk instruction
    __memcpy((long) num12 + (IntPtr) pBuffer, *(long*) ((IntPtr) recompileHeader5 + 56L), (long) (uint) *(int*) ((IntPtr) recompileHeader5 + 52L));
    int num13 = (int) FileControl.Write(@out, (void*) pBuffer, (uint) (*(int*) ((IntPtr) this.m_RecompileHeader + 52L) + num12));
    \u003CModule\u003E.delete((void*) pBuffer);
  }

  public unsafe void RecompileWriteData(FileStream @out)
  {
    AssetEntryHeader* recompileHeader = this.m_RecompileHeader;
    if ((IntPtr) recompileHeader != IntPtr.Zero)
    {
      AssetEntryHeader* assetEntryHeaderPtr = recompileHeader;
      \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bdtor\u007D(assetEntryHeaderPtr);
      \u003CModule\u003E.delete((void*) assetEntryHeaderPtr);
    }
    if ((IntPtr) this.m_NewHeader != IntPtr.Zero)
    {
      AssetEntryHeader* assetEntryHeaderPtr1 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
      AssetEntryHeader* assetEntryHeaderPtr2;
      // ISSUE: fault handler
      try
      {
        assetEntryHeaderPtr2 = (IntPtr) assetEntryHeaderPtr1 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr1, this.m_NewHeader);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) assetEntryHeaderPtr1);
      }
      this.m_RecompileHeader = assetEntryHeaderPtr2;
    }
    else
    {
      AssetEntryHeader* assetEntryHeaderPtr3 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
      AssetEntryHeader* assetEntryHeaderPtr4;
      // ISSUE: fault handler
      try
      {
        assetEntryHeaderPtr4 = (IntPtr) assetEntryHeaderPtr3 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr3, this.m_SourceHeader);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) assetEntryHeaderPtr3);
      }
      this.m_RecompileHeader = assetEntryHeaderPtr4;
    }
    *(int*) ((IntPtr) this.m_RecompileHeader + 16L /*0x10*/) = (int) @out.Position;
    AssetEntryHeader* newHeader = this.m_NewHeader;
    uint uiCount = (IntPtr) newHeader == IntPtr.Zero ? (uint) *(int*) ((IntPtr) this.m_SourceHeader + 12L) : (uint) *(int*) ((IntPtr) newHeader + 12L);
    int num = (int) FileControl.Write(@out, (void*) this.GetData(), uiCount);
    if (((IntPtr) this.m_NewHeader != IntPtr.Zero || this.m_Modified ? 1 : 0) != 0)
      return;
    \u003CModule\u003E.delete((void*) this.m_Data);
    this.m_Data = (sbyte*) 0L;
  }

  protected unsafe void LoadFromArchive()
  {
    uint sourceStartOffset = this.m_SourceStartOffset;
    if (sourceStartOffset == 0U)
      return;
    this.m_Archive.ArchiveFile.Position = (long) sourceStartOffset;
    AssetEntryHeader* assetEntryHeaderPtr1 = (AssetEntryHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
    AssetEntryHeader* assetEntryHeaderPtr2;
    // ISSUE: fault handler
    try
    {
      assetEntryHeaderPtr2 = (IntPtr) assetEntryHeaderPtr1 == IntPtr.Zero ? (AssetEntryHeader*) 0L : \u003CModule\u003E.FableMod\u002EBIG\u002EAssetEntryHeader\u002E\u007Bctor\u007D(assetEntryHeaderPtr1);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) assetEntryHeaderPtr1);
    }
    this.m_SourceHeader = assetEntryHeaderPtr2;
    FileStream archiveFile = this.m_Archive.ArchiveFile;
    int num1 = (int) FileControl.Read(archiveFile, (void*) this.m_SourceHeader, 28U);
    *(long*) ((IntPtr) this.m_SourceHeader + 28L) = (long) \u003CModule\u003E.@new((ulong) (uint) (*(int*) ((IntPtr) this.m_SourceHeader + 24L) + 1));
    AssetEntryHeader* sourceHeader1 = this.m_SourceHeader;
    int num2 = (int) FileControl.Read(archiveFile, (void*) *(long*) ((IntPtr) sourceHeader1 + 28L), (uint) *(int*) ((IntPtr) sourceHeader1 + 24L));
    AssetEntryHeader* sourceHeader2 = this.m_SourceHeader;
    *(sbyte*) ((long) (uint) *(int*) ((IntPtr) sourceHeader2 + 24L) + *(long*) ((IntPtr) sourceHeader2 + 28L)) = (sbyte) 0;
    int num3 = (int) FileControl.Read(archiveFile, (void*) ((IntPtr) this.m_SourceHeader + 36L), 8U);
    *(long*) ((IntPtr) this.m_SourceHeader + 44L) = (long) \u003CModule\u003E.@new((ulong) (uint) *(int*) ((IntPtr) this.m_SourceHeader + 40L) * 12UL);
    uint num4 = 0;
    if (0U < (uint) *(int*) ((IntPtr) this.m_SourceHeader + 40L))
    {
      long num5 = 0;
      do
      {
        DevSourceEntry* pBuffer1 = (DevSourceEntry*) (*(long*) ((IntPtr) this.m_SourceHeader + 44L) + num5);
        int num6 = (int) FileControl.Read(archiveFile, (void*) pBuffer1, 4U);
        void* pBuffer2 = \u003CModule\u003E.@new((ulong) (uint) (*(int*) pBuffer1 + 1));
        *(long*) ((IntPtr) pBuffer1 + 4L) = (long) pBuffer2;
        int num7 = (int) FileControl.Read(archiveFile, pBuffer2, (uint) *(int*) pBuffer1);
        *(sbyte*) ((long) (uint) *(int*) pBuffer1 + *(long*) ((IntPtr) pBuffer1 + 4L)) = (sbyte) 0;
        ++num4;
        num5 += 12L;
      }
      while (num4 < (uint) *(int*) ((IntPtr) this.m_SourceHeader + 40L));
    }
    int num8 = (int) FileControl.Read(archiveFile, (void*) ((IntPtr) this.m_SourceHeader + 52L), 4U);
    *(long*) ((IntPtr) this.m_SourceHeader + 56L) = (long) \u003CModule\u003E.@new((ulong) (uint) *(int*) ((IntPtr) this.m_SourceHeader + 52L));
    AssetEntryHeader* sourceHeader3 = this.m_SourceHeader;
    int num9 = (int) FileControl.Read(archiveFile, (void*) *(long*) ((IntPtr) sourceHeader3 + 56L), (uint) *(int*) ((IntPtr) sourceHeader3 + 52L));
    this.m_SourceLength = (uint) (int) (archiveFile.Position - (long) this.m_SourceStartOffset);
  }

  protected unsafe int GetHeaderLength(AssetEntryHeader* header)
  {
    int num1 = *(int*) ((IntPtr) header + 24L) + 40;
    uint num2 = (uint) *(int*) ((IntPtr) header + 40L);
    if (0U < num2)
    {
      long num3 = *(long*) ((IntPtr) header + 44L);
      uint num4 = num2;
      do
      {
        num1 += *(int*) num3 + 4;
        num3 += 12L;
        num4 += uint.MaxValue;
      }
      while (num4 > 0U);
    }
    return *(int*) ((IntPtr) header + 52L) + num1;
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EAssetEntry();
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
