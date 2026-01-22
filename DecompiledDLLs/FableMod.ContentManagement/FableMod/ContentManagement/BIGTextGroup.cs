// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.BIGTextGroup
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using FableMod.BIG;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.ContentManagement;

public class BIGTextGroup : IDisposable
{
  private unsafe uint* m_Entries;
  private uint m_EntryCount;

  public unsafe BIGTextGroup(AssetEntry ent)
  {
    sbyte* data = ent.GetData();
    uint num;
    // ISSUE: cpblk instruction
    __memcpy(ref num, (IntPtr) data, 4);
    this.m_EntryCount = num;
    this.m_Entries = (uint*) \u003CModule\u003E.@new((ulong) num * 4UL);
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) this.m_Entries, (IntPtr) (ent.GetData() + 4L), (long) this.m_EntryCount * 4L);
  }

  public unsafe BIGTextGroup()
  {
    this.m_Entries = (uint*) 0L;
    this.m_EntryCount = 0U;
  }

  private unsafe void \u007EBIGTextGroup()
  {
    uint* entries = this.m_Entries;
    if ((IntPtr) entries == IntPtr.Zero)
      return;
    \u003CModule\u003E.delete((void*) entries);
  }

  public unsafe void ApplyToEntry(AssetEntry entry)
  {
    uint entryCount = this.m_EntryCount;
    uint num = entryCount;
    sbyte* data = (sbyte*) \u003CModule\u003E.@new(((ulong) entryCount + 1UL) * 4UL);
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) data, ref num, 4);
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) (data + 4L), (IntPtr) this.m_Entries, (long) this.m_EntryCount * 4L);
    entry.SetData(data, (uint) (((ulong) this.m_EntryCount + 1UL) * 4UL));
  }

  public unsafe uint[] Entries
  {
    get
    {
      uint entryCount = this.m_EntryCount;
      uint[] entries = new uint[(int) entryCount];
      int index = 0;
      if (0U < entryCount)
      {
        long num = 0;
        do
        {
          entries[index] = (uint) *(int*) ((IntPtr) this.m_Entries + num);
          ++index;
          num += 4L;
        }
        while ((uint) index < this.m_EntryCount);
      }
      return entries;
    }
    set
    {
      \u003CModule\u003E.delete((void*) this.m_Entries);
      int length = value.Length;
      this.m_EntryCount = (uint) length;
      this.m_Entries = (uint*) \u003CModule\u003E.@new((ulong) (uint) length * 4UL);
      int index = 0;
      if (0U >= this.m_EntryCount)
        return;
      long num = 0;
      do
      {
        *(int*) ((IntPtr) this.m_Entries + num) = (int) value[index];
        ++index;
        num += 4L;
      }
      while ((uint) index < this.m_EntryCount);
    }
  }

  protected virtual unsafe void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      uint* entries = this.m_Entries;
      if ((IntPtr) entries == IntPtr.Zero)
        return;
      \u003CModule\u003E.delete((void*) entries);
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
