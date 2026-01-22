// Decompiled with JetBrains decompiler
// Type: FableMod.BIG.AssetArchive
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

public abstract class AssetArchive : IDisposable
{
  protected FileStream m_ArchiveFile;
  protected string m_OriginalFileName;
  protected Collection<AssetBank> m_Banks;

  private void \u007EAssetArchive() => this.Destroy();

  public void CloseFile()
  {
    FileStream archiveFile = this.m_ArchiveFile;
    if (archiveFile == null)
      return;
    archiveFile.Close();
    this.m_ArchiveFile = (FileStream) null;
  }

  public void Destroy()
  {
    FileStream archiveFile = this.m_ArchiveFile;
    if (archiveFile != null)
    {
      archiveFile.Close();
      this.m_ArchiveFile = (FileStream) null;
    }
    Collection<AssetBank> banks1 = this.m_Banks;
    if (banks1 == null || banks1.Count == 0)
      return;
    int index = 0;
    while (true)
    {
      Collection<AssetBank> banks2 = this.m_Banks;
      int count = banks2 != null ? banks2.Count : 0;
      if (index < count)
      {
        this.m_Banks[index]?.Dispose();
        ++index;
      }
      else
        break;
    }
    this.m_Banks.Clear();
  }

  public virtual int AddBank(AssetBank entry)
  {
    this.m_Banks.Add(entry);
    return this.m_Banks.IndexOf(entry);
  }

  public abstract void Save(string newfile, ProgressInterface progress);

  public AssetBank get_Banks(int index) => this.m_Banks[index];

  public string OriginalFileName => this.m_OriginalFileName;

  public int BankCount
  {
    get
    {
      Collection<AssetBank> banks = this.m_Banks;
      return banks == null ? 0 : banks.Count;
    }
  }

  public abstract int ContentType { get; }

  public bool Modified
  {
    [return: MarshalAs(UnmanagedType.U1)] get
    {
      int index = 0;
      while (true)
      {
        Collection<AssetBank> banks = this.m_Banks;
        int count = banks != null ? banks.Count : 0;
        if (index < count)
        {
          if (!this.m_Banks[index].Modified)
            ++index;
          else
            break;
        }
        else
          goto label_5;
      }
      return true;
label_5:
      return false;
    }
  }

  public FileStream ArchiveFile => this.m_ArchiveFile;

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.Destroy();
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
