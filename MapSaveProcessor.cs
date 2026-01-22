// Decompiled with JetBrains decompiler
// Type: ChocolateBox.MapSaveProcessor
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.CLRCore;
using FableMod.LEV;
using FableMod.TNG;
using System.IO;

#nullable disable
namespace ChocolateBox;

internal class MapSaveProcessor : Processor
{
  private LEVFile myLEV;
  private TNGFile myTNG;

  public MapSaveProcessor(TNGFile tng, LEVFile lev)
  {
    this.myTNG = tng;
    this.myLEV = lev;
  }

  private string GetName(string pathName) => new FileInfo(pathName).Name;

  public override void Run(Progress progress)
  {
    int steps = 0;
    if (this.myTNG.Modified)
      ++steps;
    bool flag = Settings.GetBool("Settings", "SaveLEV", false) && this.myLEV.Modified;
    if (flag)
      ++steps;
    progress.Begin(steps);
    if (this.myTNG.Modified)
    {
      progress.Info = $"Saving {this.GetName(this.myTNG.FileName)}...";
      this.myTNG.Save(this.myTNG.FileName);
      progress.Update();
    }
    if (flag)
    {
      progress.Info = $"Saving {this.GetName(this.myLEV.FileName)}...";
      this.myLEV.Save(this.myLEV.FileName, (ProgressInterface) progress);
    }
    progress.End();
    base.Run(progress);
  }
}
