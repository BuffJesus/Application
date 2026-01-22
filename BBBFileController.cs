// Decompiled with JetBrains decompiler
// Type: ChocolateBox.BBBFileController
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BBB;
using FableMod.CLRCore;
using System;
using System.Collections.Generic;

#nullable disable
namespace ChocolateBox;

public class BBBFileController : FileController
{
  private BBBFile myBBB;
  private List<FileController> myFiles = new List<FileController>();

  public BBBFileController(FileDatabase database, BBBFile bbb, string fileName)
    : base(database, fileName)
  {
    this.myBBB = bbb;
  }

  protected override bool OnLoad(Progress progress)
  {
    try
    {
      this.myBBB.Open(this.FileName, (ProgressInterface) progress);
    }
    catch (Exception ex)
    {
      int num = (int) FormMain.Instance.ErrorMessage(ex.Message);
    }
    return true;
  }

  protected override bool OnForm(Progress progress)
  {
    FormBBB form = new FormBBB();
    form.Build(this.myBBB, progress);
    this.CreateForm((FormFileController) form);
    return true;
  }

  public bool HasFile(string fileName)
  {
    return this.myBBB.get_Entries(this.Database.GetRelativeFileName(fileName)) != null;
  }

  public override bool Modified => false;

  public BBBFile BBB => this.myBBB;
}
