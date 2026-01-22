// Decompiled with JetBrains decompiler
// Type: ChocolateBox.NamesBINController
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIN;
using FableMod.CLRCore;
using System;

#nullable disable
namespace ChocolateBox;

internal class NamesBINController(FileDatabase database, string fileName) : FileController(database, fileName)
{
  private NamesBINFile myBIN;

  protected override bool OnLoad(Progress progress)
  {
    try
    {
      this.myBIN = new NamesBINFile();
      this.myBIN.Load(this.FileName, (ProgressInterface) progress);
    }
    catch (Exception ex)
    {
      int num = (int) FormMain.Instance.ErrorMessage(ex.Message);
      return false;
    }
    return true;
  }

  protected override bool OnSave(string fileName, Progress progress)
  {
    this.myBIN.Save(fileName);
    return true;
  }

  protected override bool OnForm(Progress progress)
  {
    FormNamesBIN form = new FormNamesBIN();
    form.Build(this.myBIN, progress);
    this.CreateForm((FormFileController) form);
    return true;
  }

  public NamesBINFile Names => this.myBIN;

  public override bool Modified
  {
    get => this.myBIN.Modified;
    set => this.myBIN.Modified = value;
  }
}
