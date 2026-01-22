// Decompiled with JetBrains decompiler
// Type: FableMod.BIN.BINFile
// Assembly: FableMod.BIN, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 7B343E30-1A4D-49C7-A3B2-33514A983F5F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.BIN.dll

using \u003CCppImplementationDetails\u003E;
using FableMod.CLRCore;
using FableMod.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.BIN;

public class BINFile(NamesBINFile names) : IDisposable
{
  private bool m_Modified;
  private string m_OriginalFileName;
  private NamesBINFile m_Names = names;
  private unsafe byte* m_Header;
  private ArrayList m_Entries;
  private int m_Count;
  private Dictionary<string, BINEntry> m_EntryDictionary;
  private IComparer m_SearchComparer = (IComparer) new EntrySearch();

  private unsafe void \u007EBINFile()
  {
    this.m_Entries.Clear();
    this.m_Entries = (ArrayList) null;
    \u003CModule\u003E.delete((void*) this.m_Header);
    Dictionary<string, BINEntry> entryDictionary = this.m_EntryDictionary;
    if (entryDictionary == null)
      return;
    entryDictionary.Clear();
    this.m_EntryDictionary = (Dictionary<string, BINEntry>) null;
  }

  public unsafe void Load(string filename, ProgressInterface progress)
  {
    this.m_OriginalFileName = filename;
    FileStream File = File.Open(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
    sbyte* pBuffer = (sbyte*) \u003CModule\u003E.@new((ulong) File.Length);
    this.m_Header = (byte*) \u003CModule\u003E.@new(9UL);
    int num1 = (int) FileControl.Read(File, (void*) pBuffer, (uint) (int) File.Length);
    File.Close();
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) this.m_Header, (IntPtr) pBuffer, 9);
    uint capacity;
    // ISSUE: cpblk instruction
    __memcpy(ref capacity, (IntPtr) (pBuffer + 9L), 4);
    ulong num2 = (ulong) capacity * 12UL;
    BINMainTableEntry* binMainTableEntryPtr1 = (BINMainTableEntry*) \u003CModule\u003E.@new(num2);
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) binMainTableEntryPtr1, (IntPtr) (pBuffer + 13L), (long) num2);
    int num3 = (int) ((long) num2 + 13L);
    uint num4;
    // ISSUE: cpblk instruction
    __memcpy(ref num4, (long) num3 + (IntPtr) pBuffer, 4);
    int num5 = (int) ((long) num3 + 4L);
    ulong num6 = (ulong) num4 * 8UL;
    BINSecondTableEntry* secondTableEntryPtr1 = (BINSecondTableEntry*) \u003CModule\u003E.@new(num6);
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) secondTableEntryPtr1, (long) num5 + (IntPtr) pBuffer, (long) num6);
    int num7 = (int) ((long) (int) ((long) num5 + (long) num6) + 4L);
    this.m_Entries = new ArrayList((int) capacity);
    this.m_Count = (int) capacity;
    sbyte* dest = (sbyte*) \u003CModule\u003E.@new(65536UL /*0x010000*/);
    progress?.Begin((int) num4 - 1);
    uint num8 = num4 - 1U;
    if (0U < num8)
    {
      long num9 = (long) num7;
      BINSecondTableEntry* secondTableEntryPtr2 = (BINSecondTableEntry*) ((IntPtr) secondTableEntryPtr1 + 4L);
      uint num10 = num8;
      do
      {
        BINSecondTableEntry* secondTableEntryPtr3 = (BINSecondTableEntry*) ((IntPtr) secondTableEntryPtr2 + 8L);
        int num11 = *(int*) secondTableEntryPtr2;
        int num12 = ZLib.Uncompress((void*) dest, 65536 /*0x010000*/, (void*) ((long) (uint) num11 + num9 + (IntPtr) pBuffer), *(int*) secondTableEntryPtr3 - num11);
        int num13 = *(int*) ((IntPtr) secondTableEntryPtr2 - 4L);
        long num14 = (long) num13;
        if ((uint) num13 < (uint) (*(int*) ((IntPtr) secondTableEntryPtr2 + 4L) - 1))
        {
          BINMainTableEntry* binMainTableEntryPtr2 = (BINMainTableEntry*) ((IntPtr) binMainTableEntryPtr1 + num14 * 12L + 4L);
          do
          {
            int num15 = num13 - *(int*) ((IntPtr) secondTableEntryPtr2 - 4L);
            int num16 = (int) *(ushort*) ((long) (uint) num15 * 2L + (IntPtr) dest);
            int datalen = (int) *(ushort*) ((long) (uint) (num15 + 1) * 2L + (IntPtr) dest) - num16;
            string name1 = this.m_Names.GetEntryByOffset((uint) *(int*) ((IntPtr) binMainTableEntryPtr2 - 4L)).Name;
            string name2 = "";
            uint nameEnum = 0;
            NamesBINEntry namesBinEntry = !this.IsXBox ? this.m_Names.GetEntryByOffset((uint) *(int*) binMainTableEntryPtr2) : this.m_Names.GetEntryByEnum((uint) *(int*) binMainTableEntryPtr2);
            if (this.IsXBox)
            {
              nameEnum = (uint) *(int*) binMainTableEntryPtr2;
              sbyte* numPtr1 = (sbyte*) ((long) num16 + (IntPtr) dest);
              name2 = new string(numPtr1);
              sbyte* numPtr2 = numPtr1;
              sbyte* numPtr3 = numPtr2;
              if (*numPtr2 != (sbyte) 0)
              {
                do
                {
                  ++numPtr3;
                }
                while (*numPtr3 != (sbyte) 0);
              }
              long num17 = (long) ((IntPtr) numPtr3 - (IntPtr) numPtr2);
              datalen = (int) ((long) datalen - num17 - 1L);
              sbyte* numPtr4 = numPtr1;
              sbyte* numPtr5 = numPtr4;
              if (*numPtr4 != (sbyte) 0)
              {
                do
                {
                  ++numPtr5;
                }
                while (*numPtr5 != (sbyte) 0);
              }
              num16 = (int) ((IntPtr) numPtr5 - (IntPtr) numPtr4 + (long) num16 + 1L);
            }
            else if (namesBinEntry != null)
            {
              nameEnum = namesBinEntry.Enum;
              name2 = namesBinEntry.Name;
            }
            this.m_Entries.Add((object) new BINEntry(this, name1, name2, nameEnum, (sbyte*) ((long) num16 + (IntPtr) dest), datalen));
            ++num13;
            binMainTableEntryPtr2 += 12L;
          }
          while ((uint) num13 < (uint) (*(int*) ((IntPtr) secondTableEntryPtr2 + 4L) - 1));
        }
        int num18 = (int) *(ushort*) ((long) (uint) (num13 - *(int*) ((IntPtr) secondTableEntryPtr2 - 4L)) * 2L + (IntPtr) dest);
        int datalen1 = num12 - num18;
        BINMainTableEntry* binMainTableEntryPtr3 = (BINMainTableEntry*) ((long) num13 * 12L + (IntPtr) binMainTableEntryPtr1);
        string name3 = this.m_Names.GetEntryByOffset((uint) *(int*) binMainTableEntryPtr3).Name;
        string name4 = "";
        uint nameEnum1 = 0;
        BINMainTableEntry* binMainTableEntryPtr4;
        NamesBINEntry namesBinEntry1;
        if (this.IsXBox)
        {
          binMainTableEntryPtr4 = (BINMainTableEntry*) ((IntPtr) binMainTableEntryPtr3 + 4L);
          namesBinEntry1 = this.m_Names.GetEntryByEnum((uint) *(int*) binMainTableEntryPtr4);
        }
        else
        {
          binMainTableEntryPtr4 = (BINMainTableEntry*) ((IntPtr) binMainTableEntryPtr3 + 4L);
          namesBinEntry1 = this.m_Names.GetEntryByOffset((uint) *(int*) binMainTableEntryPtr4);
        }
        if (this.IsXBox)
        {
          nameEnum1 = (uint) *(int*) binMainTableEntryPtr4;
          sbyte* numPtr6 = (sbyte*) ((long) num18 + (IntPtr) dest);
          name4 = new string(numPtr6);
          sbyte* numPtr7 = numPtr6;
          sbyte* numPtr8 = numPtr7;
          if (*numPtr7 != (sbyte) 0)
          {
            do
            {
              ++numPtr8;
            }
            while (*numPtr8 != (sbyte) 0);
          }
          long num19 = (long) ((IntPtr) numPtr8 - (IntPtr) numPtr7);
          datalen1 = (int) ((long) datalen1 - num19 - 1L);
          sbyte* numPtr9 = numPtr6;
          sbyte* numPtr10 = numPtr9;
          if (*numPtr9 != (sbyte) 0)
          {
            do
            {
              ++numPtr10;
            }
            while (*numPtr10 != (sbyte) 0);
          }
          num18 = (int) ((IntPtr) numPtr10 - (IntPtr) numPtr9 + (long) num18 + 1L);
        }
        else if (namesBinEntry1 != null)
        {
          nameEnum1 = namesBinEntry1.Enum;
          name4 = namesBinEntry1.Name;
        }
        this.m_Entries.Add((object) new BINEntry(this, name3, name4, nameEnum1, (sbyte*) ((long) num18 + (IntPtr) dest), datalen1));
        progress?.Update();
        secondTableEntryPtr2 = secondTableEntryPtr3;
        num10 += uint.MaxValue;
      }
      while (num10 > 0U);
    }
    progress?.End();
    \u003CModule\u003E.delete((void*) binMainTableEntryPtr1);
    \u003CModule\u003E.delete((void*) secondTableEntryPtr1);
    \u003CModule\u003E.delete((void*) dest);
    \u003CModule\u003E.delete((void*) pBuffer);
  }

  public unsafe void Save(string filename, NamesBINFile names, ProgressInterface progress)
  {
    ulong count1 = (ulong) this.m_Count;
    BINMainTableEntry* binMainTableEntryPtr1 = (BINMainTableEntry*) \u003CModule\u003E.@new(count1 > 1537228672809129301UL ? ulong.MaxValue : count1 * 12UL);
    // ISSUE: initblk instruction
    __memset((IntPtr) binMainTableEntryPtr1, 0, (long) this.m_Count * 12L);
    uint num1 = 0;
    BINSecondTableEntry* secondTableEntryPtr = (BINSecondTableEntry*) \u003CModule\u003E.@new(8192UL /*0x2000*/);
    sbyte** numPtr1 = (sbyte**) \u003CModule\u003E.@new(8192UL /*0x2000*/);
    ushort* numPtr2 = (ushort*) \u003CModule\u003E.@new(4096UL /*0x1000*/);
    sbyte* src = (sbyte*) \u003CModule\u003E.@new(32768UL /*0x8000*/);
    sbyte* dest = (sbyte*) \u003CModule\u003E.@new(32768UL /*0x8000*/);
    int num2 = 0;
    int num3 = 0;
    int num4 = 0;
    SortedList<uint, int> sortedList = new SortedList<uint, int>();
    progress?.Begin(this.EntryCount);
    int index = 0;
    long num5 = 0;
    \u0024ArrayType\u0024\u0024\u0024BY0EAA\u0040H arrayTypeBy0EaaH;
    if (0 < this.EntryCount)
    {
      BINMainTableEntry* binMainTableEntryPtr2 = (BINMainTableEntry*) ((IntPtr) binMainTableEntryPtr1 + 4L);
      do
      {
        BINEntry binEntry = this.get_Entries(index);
        if (this.IsXBox)
          *(int*) binMainTableEntryPtr2 = (int) binEntry.NameEnum;
        else if (binEntry.Name != "")
        {
          NamesBINEntry namesBinEntry = names.GetEntryByName(binEntry.Name) ?? names.AddEntry(binEntry.Name);
          *(int*) binMainTableEntryPtr2 = (int) namesBinEntry.Offset;
        }
        else
          *(int*) binMainTableEntryPtr2 = -1;
        NamesBINEntry namesBinEntry1 = names.GetEntryByName(binEntry.Definition) ?? names.AddEntry(binEntry.Definition);
        *(int*) ((IntPtr) binMainTableEntryPtr2 - 4L) = (int) namesBinEntry1.Offset;
        int num6 = 0;
        if (sortedList.TryGetValue(namesBinEntry1.Offset, out num6))
        {
          *(int*) ((IntPtr) binMainTableEntryPtr2 + 4L) = num6;
          int num7 = num6 + 1;
          sortedList[namesBinEntry1.Offset] = num7;
        }
        else
        {
          *(int*) ((IntPtr) binMainTableEntryPtr2 + 4L) = 0;
          sortedList[namesBinEntry1.Offset] = 1;
        }
        int length = binEntry.Length;
        if (this.IsXBox && binEntry.NameEnum != 0U)
          length += binEntry.Name.Length + 1;
        int num8 = num3 * 2;
        if (num8 + length + num2 + 2 > 32768 /*0x8000*/)
        {
          long num9 = 0;
          if (0L < num5)
          {
            do
            {
              IntPtr num10 = num9 * 2L + (IntPtr) numPtr2;
              *(short*) num10 = (short) ((int) *(ushort*) num10 + num8);
              ++num9;
            }
            while (num9 < num5);
          }
          \u003CModule\u003E.memmove((void*) ((long) *numPtr2 + (IntPtr) src), (void*) src, (ulong) num2);
          ushort num11 = *numPtr2;
          int count2 = num2 + (int) num11;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) src, (IntPtr) numPtr2, (long) num11);
          uint num12 = (uint) ZLib.Compress((void*) dest, 32768 /*0x8000*/, (void*) src, count2, 1);
          ulong num13 = (ulong) num12;
          void* voidPtr = \u003CModule\u003E.@new(num13);
          long num14 = (long) num1;
          long num15 = num14 * 8L;
          *(long*) (num15 + (IntPtr) numPtr1) = (long) voidPtr;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) voidPtr, (IntPtr) dest, (long) num13);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) (num14 * 4L + ref arrayTypeBy0EaaH) = (int) num12;
          *(int*) (num15 + (IntPtr) secondTableEntryPtr) = index - num3;
          *(int*) (num15 + (IntPtr) secondTableEntryPtr + 4L) = num4;
          num4 += (int) num12;
          ++num1;
          num2 = 0;
          num3 = 0;
          num5 = 0L;
        }
        *(short*) (num5 * 2L + (IntPtr) numPtr2) = (short) num2;
        ++num3;
        ++num5;
        if (this.IsXBox)
        {
          IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi(binEntry.Name);
          void* pointer = hglobalAnsi.ToPointer();
          long num16 = (long) ((long) num2 - (IntPtr) pointer + (IntPtr) src);
          sbyte num17;
          do
          {
            num17 = *(sbyte*) pointer;
            *(sbyte*) ((IntPtr) pointer + num16) = num17;
            ++pointer;
          }
          while (num17 != (sbyte) 0);
          num2 += binEntry.Name.Length + 1;
          Marshal.FreeHGlobal(hglobalAnsi);
        }
        // ISSUE: cpblk instruction
        __memcpy((long) num2 + (IntPtr) src, (IntPtr) binEntry.GetData(), (long) binEntry.Length);
        num2 += binEntry.Length;
        progress?.Update();
        ++index;
        binMainTableEntryPtr2 += 12L;
      }
      while (index < this.EntryCount);
    }
    progress?.End();
    if (num3 != 0)
    {
      long num18 = 0;
      long num19 = (long) num3;
      if (0L < num19)
      {
        int num20 = num3 * 2;
        do
        {
          IntPtr num21 = num18 * 2L + (IntPtr) numPtr2;
          *(short*) num21 = (short) ((int) *(ushort*) num21 + num20);
          ++num18;
        }
        while (num18 < num19);
      }
      \u003CModule\u003E.memmove((void*) ((long) *numPtr2 + (IntPtr) src), (void*) src, (ulong) num2);
      ushort num22 = *numPtr2;
      int count3 = num2 + (int) num22;
      // ISSUE: cpblk instruction
      __memcpy((IntPtr) src, (IntPtr) numPtr2, (long) num22);
      int num23 = ZLib.Compress((void*) dest, 32768 /*0x8000*/, (void*) src, count3, 1);
      ulong num24 = (ulong) num23;
      void* voidPtr = \u003CModule\u003E.@new(num24);
      long num25 = (long) num1;
      long num26 = num25 * 8L;
      *(long*) (num26 + (IntPtr) numPtr1) = (long) voidPtr;
      // ISSUE: cpblk instruction
      __memcpy((IntPtr) voidPtr, (IntPtr) dest, (long) num24);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) (num25 * 4L + ref arrayTypeBy0EaaH) = num23;
      *(int*) (num26 + (IntPtr) secondTableEntryPtr) = this.m_Count - num3;
      *(int*) (num26 + (IntPtr) secondTableEntryPtr + 4L) = num4;
      num4 += num23;
      ++num1;
    }
    if (sortedList is IDisposable disposable)
      disposable.Dispose();
    long num27 = (long) num1;
    long num28 = num27 * 8L;
    *(int*) (num28 + (IntPtr) secondTableEntryPtr) = this.m_Count;
    *(int*) (num28 + (IntPtr) secondTableEntryPtr + 4L) = num4;
    *(long*) (num28 + (IntPtr) numPtr1) = (long) \u003CModule\u003E.@new(0UL);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) (num27 * 4L + ref arrayTypeBy0EaaH) = 0;
    uint num29 = num1 + 1U;
    ulong num30 = (ulong) num29 * 8UL;
    sbyte* pBuffer = (sbyte*) \u003CModule\u003E.@new(num30 + (ulong) this.m_Count * 12UL + (ulong) num4 + 21UL);
    uint count4 = (uint) this.m_Count;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) pBuffer, (IntPtr) this.m_Header, 9);
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) (pBuffer + 9L), ref count4, 4);
    ulong num31 = (ulong) count4 * 12UL;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) (pBuffer + 13L), (IntPtr) binMainTableEntryPtr1, (long) num31);
    int num32 = (int) ((long) num31 + 13L);
    // ISSUE: cpblk instruction
    __memcpy((long) num32 + (IntPtr) pBuffer, ref num29, 4);
    int num33 = (int) ((long) num32 + 4L);
    // ISSUE: cpblk instruction
    __memcpy((long) num33 + (IntPtr) pBuffer, (IntPtr) secondTableEntryPtr, (long) num30);
    int num34 = (int) ((long) num33 + (long) num30);
    // ISSUE: cpblk instruction
    __memcpy((long) num34 + (IntPtr) pBuffer, ref num4, 4);
    int uiCount = (int) ((long) num34 + 4L);
    if (0U < num29)
    {
      sbyte** numPtr3 = numPtr1;
      \u0024ArrayType\u0024\u0024\u0024BY0EAA\u0040H* arrayTypeBy0EaaHPtr = &arrayTypeBy0EaaH;
      uint num35 = num29;
      do
      {
        int num36 = *(int*) arrayTypeBy0EaaHPtr;
        // ISSUE: cpblk instruction
        __memcpy((long) uiCount + (IntPtr) pBuffer, *(long*) numPtr3, (long) num36);
        uiCount += num36;
        arrayTypeBy0EaaHPtr += 4L;
        numPtr3 += 8L;
        num35 += uint.MaxValue;
      }
      while (num35 > 0U);
    }
    FileStream File = File.Create(filename);
    int num37 = (int) FileControl.Write(File, (void*) pBuffer, (uint) uiCount);
    File.Close();
    \u003CModule\u003E.delete((void*) pBuffer);
    \u003CModule\u003E.delete((void*) src);
    \u003CModule\u003E.delete((void*) dest);
    \u003CModule\u003E.delete((void*) numPtr2);
    if (0U < num29)
    {
      sbyte** numPtr4 = numPtr1;
      uint num38 = num29;
      do
      {
        \u003CModule\u003E.delete((void*) *(long*) numPtr4);
        numPtr4 += 8L;
        num38 += uint.MaxValue;
      }
      while (num38 > 0U);
    }
    \u003CModule\u003E.delete((void*) numPtr1);
  }

  public void Save(string filename, ProgressInterface progress)
  {
    this.Save(filename, this.m_Names, progress);
  }

  public BINEntry get_Entries(int index)
  {
    return index > this.m_Entries.Count ? (BINEntry) null : (BINEntry) this.m_Entries[index];
  }

  public string OriginalFileName => this.m_OriginalFileName;

  public int EntryCount => this.m_Count;

  public bool Modified
  {
    [return: MarshalAs(UnmanagedType.U1)] get
    {
      if (this.m_Modified)
        return true;
      int index = 0;
      if (0 < this.m_Count)
      {
        while (!(index <= this.m_Entries.Count ? (BINEntry) this.m_Entries[index] : (BINEntry) null).Modified)
        {
          ++index;
          if (index >= this.m_Count)
            goto label_6;
        }
        return true;
      }
label_6:
      return false;
    }
  }

  public unsafe bool IsXBox
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_Header[1L] == (byte) 170;
  }

  public int GetEntryIndex(BINEntry entry) => this.m_Entries.IndexOf((object) entry);

  public BINEntry GetEntryByEnum(uint enumval)
  {
    int index = 0;
    if (0 < this.m_Count)
    {
      while ((int) (index <= this.m_Entries.Count ? (BINEntry) this.m_Entries[index] : (BINEntry) null).NameEnum != (int) enumval)
      {
        ++index;
        if (index >= this.m_Count)
          goto label_6;
      }
      return index > this.m_Entries.Count ? (BINEntry) null : (BINEntry) this.m_Entries[index];
    }
label_6:
    return (BINEntry) null;
  }

  public BINEntry GetEntryByName(string name)
  {
    if (this.m_EntryDictionary == null)
    {
      this.m_EntryDictionary = new Dictionary<string, BINEntry>();
      int index = 0;
      if (0 < this.m_Count)
      {
        do
        {
          BINEntry entry = index <= this.m_Entries.Count ? (BINEntry) this.m_Entries[index] : (BINEntry) null;
          this.m_EntryDictionary[(index <= this.m_Entries.Count ? (BINEntry) this.m_Entries[index] : (BINEntry) null).Name] = entry;
          ++index;
        }
        while (index < this.m_Count);
      }
    }
    BINEntry binEntry = (BINEntry) null;
    return this.m_EntryDictionary.TryGetValue(name, out binEntry) ? binEntry : (BINEntry) null;
  }

  public BINEntry[] GetEntriesByDefinition(string Definition)
  {
    ArrayList arrayList = new ArrayList();
    int index1 = 0;
    if (0 < this.m_Count)
    {
      do
      {
        if ((index1 <= this.m_Entries.Count ? (BINEntry) this.m_Entries[index1] : (BINEntry) null).Definition == Definition)
        {
          BINEntry entry = index1 <= this.m_Entries.Count ? (BINEntry) this.m_Entries[index1] : (BINEntry) null;
          arrayList.Add((object) entry);
        }
        ++index1;
      }
      while (index1 < this.m_Count);
    }
    BINEntry[] entriesByDefinition = new BINEntry[arrayList.Count];
    int index2 = 0;
    if (0 < arrayList.Count)
    {
      do
      {
        entriesByDefinition[index2] = (BINEntry) arrayList[index2];
        ++index2;
      }
      while (index2 < arrayList.Count);
    }
    arrayList.Clear();
    return entriesByDefinition;
  }

  public unsafe BINEntry AddEntry(string Name, string Definition, sbyte* data, uint datalen)
  {
    this.m_Modified = true;
    BINEntry binEntry = new BINEntry(this, Definition, Name, data, (int) datalen);
    this.m_Entries.Add((object) binEntry);
    BINFile binFile = this;
    binFile.m_Count = binFile.m_Entries.Count;
    this.m_EntryDictionary = (Dictionary<string, BINEntry>) null;
    return binEntry;
  }

  public unsafe BINEntry AddEntry(string Name, string Definition, byte[] data)
  {
    fixed (byte* data1 = &data[0])
      return this.AddEntry(Name, Definition, (sbyte*) data1, (uint) data.Length);
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public bool RemoveEntry(BINEntry entry)
  {
    int index = this.m_Entries.IndexOf((object) entry);
    if (index < 0)
      return false;
    this.m_Modified = true;
    this.m_Entries.RemoveAt(index);
    BINFile binFile = this;
    binFile.m_Count = binFile.m_Entries.Count;
    return true;
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EBINFile();
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
