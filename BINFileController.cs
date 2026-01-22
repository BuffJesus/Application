// Decompiled with JetBrains decompiler
// Type: ChocolateBox.BINFileController
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIN;
using FableMod.CLRCore;
using System;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace ChocolateBox;

internal class BINFileController(FileDatabase database, string fileName) : FileController(database, fileName)
{
  private BINFile myBIN;

  protected NamesBINController GetNamesController()
  {
    return (NamesBINController) this.Database.Get(Settings.FableDirectory + Settings.GetString("Files", "Names"));
  }

  public override void LoadProcess(List<Processor> processors, bool form)
  {
    this.GetNamesController().LoadProcess(processors, false);
    base.LoadProcess(processors, form);
  }

  protected override bool OnLoad(Progress progress)
  {
    try
    {
      this.myBIN = new BINFile(this.GetNamesController().Names);
      this.myBIN.Load(this.FileName, (ProgressInterface) progress);
    }
    catch (Exception ex)
    {
      int num = (int) FormMain.Instance.ErrorMessage(ex.Message);
      return false;
    }
    return true;
  }

  protected override bool OnForm(Progress progress)
  {
    FormBIN form = new FormBIN();
    form.Build(this.myBIN, progress);
    this.CreateForm((FormFileController) form);
    return true;
  }

  protected override bool OnSave(string fileName, Progress progress)
  {
    try
    {
      this.myBIN.Save(fileName, (ProgressInterface) progress);
      NamesBINController namesController = this.GetNamesController();
      if (namesController.Modified)
      {
        string str = Path.GetDirectoryName(fileName) + "\\names.bin";
        namesController.Names.Save(namesController.FileName);
        if (str == namesController.FileName)
          namesController.Modified = false;
      }
    }
    catch (Exception ex)
    {
      int num = (int) FormMain.Instance.ErrorMessage(ex.Message);
      return false;
    }
    return true;
  }

  public BINFile BIN => this.myBIN;

  public override bool Modified
  {
    get => this.FileLoaded ? this.myBIN.Modified : base.Modified;
    set => base.Modified = value;
  }
}
