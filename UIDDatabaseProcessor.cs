// Decompiled with JetBrains decompiler
// Type: ChocolateBox.UIDDatabaseProcessor
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.TNG;
using System.IO;

#nullable disable
namespace ChocolateBox;

internal class UIDDatabaseProcessor : Processor
{
  protected void ProcessFiles(string[] files, Progress progress)
  {
    string str = "Reading UIDs from ";
    for (int index1 = 0; index1 < files.Length; ++index1)
    {
      progress.Info = $"{str}{Path.GetFileName(files[index1])}...";
      TNGFile tngFile = new TNGFile(FileDatabase.Instance.TNGDefinitions);
      tngFile.Load(files[index1]);
      for (int index2 = 0; index2 < tngFile.SectionCount; ++index2)
      {
        foreach (Thing thing in tngFile.get_Sections(index2).Things)
          UIDManager.Add(thing.UID);
      }
      tngFile.Dispose();
      progress.Update();
    }
  }

  public override void Run(Progress progress)
  {
    string path = Settings.FableDirectory + "data\\Levels";
    string[] files1 = Directory.GetFiles(path, "*.tng");
    string[] files2 = Directory.GetFiles(path + "\\FinalAlbion", "*.tng");
    progress.Begin(files1.Length + files2.Length);
    this.ProcessFiles(files1, progress);
    this.ProcessFiles(files2, progress);
    progress.End();
  }
}
