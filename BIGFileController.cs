// Decompiled with JetBrains decompiler
// Type: ChocolateBox.BIGFileController
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIG;
using FableMod.CLRCore;
using System;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class BIGFileController(FileDatabase database, string fileName) : FileController(database, fileName)
{
  private BIGFile myBIG;

  public override bool Close()
  {
    this.State &= -3;
    this.myBIG.Destroy();
    this.myBIG = (BIGFile) null;
    return true;
  }

  protected override bool OnLoad(Progress progress)
  {
    try
    {
      this.myBIG = new BIGFile();
      this.myBIG.Load(this.FileName, (ProgressInterface) progress);
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
    if (fileName != this.FileName)
    {
      this.myBIG.Save(fileName, (ProgressInterface) progress);
    }
    else
    {
      FileInfo fileInfo = new FileInfo(fileName);
      string str = $"{fileInfo.DirectoryName}\\TMP_{fileInfo.Name}";
      this.myBIG.Save(str, (ProgressInterface) progress);
      this.myBIG.Destroy();
      try
      {
        File.Delete(fileName);
        File.Move(str, fileName);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.ToString());
      }
      this.State = 1;
    }
    return false;
  }

  protected override bool OnForm(Progress progress)
  {
    FormBIG form = this.myBIG != FileDatabase.Instance.Textures ? new FormBIG() : (FormBIG) new FormTextureBIG();
    form.Build(this.myBIG, progress);
    this.CreateForm((FormFileController) form);
    return true;
  }

  public BIGFile BIG => this.myBIG;

  public override bool Modified
  {
    get => this.FileLoaded ? this.myBIG.Modified : base.Modified;
    set => base.Modified = value;
  }
}
