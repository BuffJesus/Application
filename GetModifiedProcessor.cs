// Decompiled with JetBrains decompiler
// Type: ChocolateBox.GetModifiedProcessor
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIG;
using FableMod.BIN;
using FableMod.ContentManagement;
using System.Collections.ObjectModel;

#nullable disable
namespace ChocolateBox;

internal class GetModifiedProcessor : Processor
{
  private Collection<ContentObject> myObjects;

  public GetModifiedProcessor() => this.myObjects = new Collection<ContentObject>();

  public Collection<ContentObject> Objects => this.myObjects;

  private void FindModified(BIGFile big, Progress progress)
  {
    progress.Begin(big.BankCount);
    for (int index = 0; index < big.BankCount; ++index)
      this.FindModified(big.get_Banks(index), progress);
    progress.End();
  }

  private void FindModified(BIGBank bank, Progress progress)
  {
    progress.Begin(bank.EntryCount);
    for (int index = 0; index < bank.EntryCount; ++index)
    {
      AssetEntry o = bank.get_Entries(index);
      if (o.Modified)
        this.myObjects.Add(FileDatabase.Instance.GetContentObject((object) o));
      progress.Update();
    }
    progress.End();
  }

  private void FindModified(BINFile bin, Progress progress)
  {
    progress.Begin(bin.EntryCount);
    for (int index = 0; index < bin.EntryCount; ++index)
    {
      BINEntry o = bin.get_Entries(index);
      if (o.Modified)
        this.myObjects.Add(FileDatabase.Instance.GetContentObject((object) o));
      progress.Update();
    }
    progress.End();
  }

  public override void Run(Progress progress)
  {
    progress.Begin(4);
    progress.Info = "Checking graphics...";
    this.FindModified(ContentManager.Instance.GraphicsBank, progress);
    progress.Info = "Checking textures...";
    this.FindModified(ContentManager.Instance.Textures, progress);
    progress.Info = "Checking front end textures...";
    this.FindModified(ContentManager.Instance.FrontEndTextureBank, progress);
    progress.Info = "Checking text...";
    this.FindModified(ContentManager.Instance.TextBank, progress);
    progress.Info = "Checking objects...";
    this.FindModified(ContentManager.Instance.Objects, progress);
    progress.Info = "Checking scripts...";
    this.FindModified(ContentManager.Instance.Scripts, progress);
    progress.End();
  }
}
