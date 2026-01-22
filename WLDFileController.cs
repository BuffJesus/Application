// Decompiled with JetBrains decompiler
// Type: ChocolateBox.WLDFileController
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.WLD;

#nullable disable
namespace ChocolateBox;

public class WLDFileController : FileController
{
  private WLDFile myWLD;

  public WLDFileController(FileDatabase database, string fileName)
    : base(database, fileName)
  {
    this.myWLD = new WLDFile();
  }

  protected override bool OnLoad(Progress progress)
  {
    progress.Begin(1);
    this.myWLD.Load(this.FileName);
    progress.End();
    return true;
  }

  public WLDFile WLD => this.myWLD;

  public override bool UserAccess => false;
}
