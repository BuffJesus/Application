// Decompiled with JetBrains decompiler
// Type: ChocolateBox.BBBExtractor
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BBB;

#nullable disable
namespace ChocolateBox;

public class BBBExtractor : Processor
{
  private BBBFile myBBB;
  private string myPath;

  public BBBExtractor(BBBFile bbb, string path)
  {
    this.myBBB = bbb;
    this.myPath = path;
  }

  public override void Run(Progress progress)
  {
    progress.Begin(this.myBBB.EntryCount);
    for (int index = 0; index < this.myBBB.EntryCount; ++index)
    {
      progress.Info = this.myBBB.get_Entries(index).FileName;
      this.myBBB.ExtractFile(this.myPath, this.myBBB.get_Entries(index));
      progress.Update();
    }
    progress.End();
  }
}
