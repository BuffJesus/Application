// Decompiled with JetBrains decompiler
// Type: FableMod.BIG.AssetBank
// Assembly: FableMod.BIG, Version=1.0.4918.425, Culture=neutral, PublicKeyToken=null
// MVID: 88942552-073F-4D63-ADC6-04A8B51D93E5
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.BIG.dll

using FableMod.CLRCore;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.BIG;

public abstract class AssetBank : AssetArchiveItem, IDisposable
{
  protected bool m_Modified = false;
  protected Collection<AssetEntry> m_Entries;

  private void \u007EAssetBank()
  {
    Collection<AssetEntry> entries = this.m_Entries;
    if (entries == null)
      return;
    int index = 0;
    if (0 < entries.Count)
    {
      do
      {
        this.m_Entries[index]?.Dispose();
        ++index;
      }
      while (index < this.m_Entries.Count);
    }
    this.m_Entries.Clear();
    this.m_Entries = (Collection<AssetEntry>) null;
  }

  public uint GetNewID()
  {
    uint newId = 1;
    int index = 0;
    if (0 < this.m_Entries.Count)
    {
      do
      {
        if (this.m_Entries[index].ID >= newId)
          newId = this.m_Entries[index].ID + 1U;
        ++index;
      }
      while (index < this.m_Entries.Count);
    }
    return newId;
  }

  public AssetEntry FindEntryByID(uint id)
  {
    int index = 0;
    if (0 < this.m_Entries.Count)
    {
      while ((int) this.m_Entries[index].ID != (int) id)
      {
        ++index;
        if (index >= this.m_Entries.Count)
          goto label_4;
      }
      return this.m_Entries[index];
    }
label_4:
    return (AssetEntry) null;
  }

  public AssetEntry FindEntryBySymbolName(string name)
  {
    int index = 0;
    if (0 < this.m_Entries.Count)
    {
      while (!(this.m_Entries[index].DevSymbolName == name))
      {
        ++index;
        if (index >= this.m_Entries.Count)
          goto label_4;
      }
      return this.m_Entries[index];
    }
label_4:
    return (AssetEntry) null;
  }

  public AssetEntry NewEntry(string symbolName, uint type)
  {
    AssetEntry entry = new AssetEntry(symbolName, this.GetNewID(), type, this);
    this.AddEntry(entry);
    return entry;
  }

  public virtual int AddEntry(AssetEntry entry)
  {
    this.m_Modified = true;
    this.m_Entries.Add(entry);
    return this.m_Entries.IndexOf(entry);
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public virtual bool RemoveEntry(AssetEntry entry)
  {
    int index = this.m_Entries.IndexOf(entry);
    if (index < 0)
      return false;
    this.m_Modified = true;
    this.m_Entries.RemoveAt(index);
    return true;
  }

  public AssetEntry get_Entries(int index) => this.m_Entries[index];

  public int EntryCount => this.m_Entries.Count;

  public bool Modified
  {
    [return: MarshalAs(UnmanagedType.U1)] get
    {
      if (this.m_Modified)
        return true;
      Collection<AssetEntry> entries = this.m_Entries;
      if (entries != null)
      {
        int index = 0;
        if (0 < entries.Count)
        {
          while (!this.m_Entries[index].Modified)
          {
            ++index;
            if (index >= this.m_Entries.Count)
              goto label_7;
          }
          return true;
        }
      }
label_7:
      return false;
    }
  }

  public new AssetArchive Archive => this.m_Archive;

  public abstract void RecompileWriteContent(FileStream @out, ProgressInterface progress);

  public abstract void RecompileWriteEntries(FileStream @out, ProgressInterface progress);

  public abstract void RecompileWriteHeader(FileStream @out, ProgressInterface progress);

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EAssetBank();
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
