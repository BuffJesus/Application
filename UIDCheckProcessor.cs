// Decompiled with JetBrains decompiler
// Type: ChocolateBox.UIDCheckProcessor
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.TNG;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace ChocolateBox;

internal class UIDCheckProcessor : Processor
{
  private List<string> myUIDs = new List<string>(20000);
  private List<string> mySaves = new List<string>(128 /*0x80*/);
  private int myCount;
  private int mySaved;

  public int ChangedCount => this.myCount;

  public int SavedCount => this.mySaved;

  protected bool IsValid(Thing thing)
  {
    if (!UIDManager.IsNormal(thing.UID))
      return false;
    return thing.Name == "Building" || thing.Name == "Object";
  }

  protected List<CTCBlock> GetOwned(TNGFile tng)
  {
    List<CTCBlock> owned = new List<CTCBlock>(100);
    for (int index = 0; index < tng.SectionCount; ++index)
    {
      foreach (Thing thing in tng.get_Sections(index).Things)
      {
        CTCBlock ctcBlock = thing.get_CTCs("CTCOwnedEntity");
        if (ctcBlock != null && UIDManager.IsNormal(ctcBlock.get_Variables("OwnerUID").StringValue))
          owned.Add(ctcBlock);
      }
    }
    return owned;
  }

  protected void UpdateThing(Thing thing, List<CTCBlock> ctcs)
  {
    string str = UIDManager.Generate();
    foreach (VariableBlock ctc in ctcs)
    {
      Variable variable = ctc.get_Variables("OwnerUID");
      if (variable.StringValue == thing.UID)
        variable.Value = (object) str;
    }
    thing.UID = str;
  }

  protected void CheckUIDs(string[] files, Progress progress)
  {
    string str = "Checking ";
    int num1 = 0;
    for (int index1 = 0; index1 < files.Length; ++index1)
    {
      progress.Info = $"{str}{Path.GetFileName(files[index1])}...";
      TNGFile tng = new TNGFile(FileDatabase.Instance.TNGDefinitions);
      tng.Load(files[index1]);
      List<CTCBlock> owned = this.GetOwned(tng);
      for (int index2 = 0; index2 < tng.SectionCount; ++index2)
      {
        foreach (Thing thing in tng.get_Sections(index2).Things)
        {
          if (this.IsValid(thing))
          {
            int num2 = this.myUIDs.IndexOf(thing.UID);
            if (num2 >= 0 && num2 != num1)
            {
              this.UpdateThing(thing, owned);
              ++this.myCount;
            }
            else
              this.myUIDs.Add(thing.UID);
            ++num1;
          }
        }
      }
      if (tng.Modified)
      {
        if (this.mySaves.IndexOf(files[index1]) < 0)
          this.mySaves.Add(files[index1]);
        tng.Save(files[index1]);
      }
      tng.Dispose();
      progress.Update();
    }
  }

  public override void Run(Progress progress)
  {
    string path = Settings.FableDirectory + "data\\Levels";
    string[] files1 = Directory.GetFiles(path, "*.tng");
    string[] files2 = Directory.GetFiles(path + "\\FinalAlbion", "*.tng");
    string[] strArray = new string[files1.Length + files2.Length];
    Array.Copy((Array) files1, (Array) strArray, files1.Length);
    Array.Copy((Array) files2, 0, (Array) strArray, files1.Length, files2.Length);
    progress.Begin(2);
    progress.Begin(strArray.Length);
    this.CheckUIDs(strArray, progress);
    progress.End();
    progress.Begin(strArray.Length);
    this.CheckUIDs(strArray, progress);
    progress.End();
    progress.End();
    this.myUIDs.Clear();
    this.myUIDs = (List<string>) null;
    this.mySaved = this.mySaves.Count;
    this.mySaves.Clear();
    this.mySaves = (List<string>) null;
  }
}
