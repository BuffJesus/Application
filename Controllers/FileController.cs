// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FileController
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FileController
{
  private string myFileName;
  private int myState;
  private FormFileController myForm;
  private FileDatabase myDatabase;

  public FileController(FileDatabase database, string fileName)
  {
    this.myState = 1;
    this.myFileName = fileName;
    this.myDatabase = database;
  }

  public virtual bool Close() => false;

  public void LoadProcess(List<Processor> processors) => this.LoadProcess(processors, false);

  public virtual void LoadProcess(List<Processor> processors, bool form)
  {
    if (!this.FileLoaded)
      processors.Add((Processor) new FileProcessor(this, FileProcessorMode.LoadFile));
    if (this.FormLoaded || !form)
      return;
    processors.Add((Processor) new FileProcessor(this, FileProcessorMode.LoadForm));
  }

  protected virtual bool OnLoad(Progress progress) => false;

  protected virtual bool OnForm(Progress progress) => false;

  protected virtual bool OnSave(string fileName, Progress progress) => false;

  public bool LoadFile(Progress progress)
  {
    if (!this.FileLoaded)
    {
      FileInfo fileInfo = new FileInfo(this.FileName);
      progress.Info = $"Loading {fileInfo.Name}";
      if (!this.OnLoad(progress))
        return false;
      this.State |= 2;
      FileDatabase.Instance.Interface.OnLoadFile(this);
      return true;
    }
    progress.Update();
    return true;
  }

  public bool LoadForm(Progress progress)
  {
    if (!this.FormLoaded)
    {
      progress.Info = "Building interface";
      if (!this.OnForm(progress))
        return false;
      this.State |= 4;
      return true;
    }
    progress.Update();
    return true;
  }

  public void SaveFile(string fileName, Progress progress)
  {
    if (string.IsNullOrEmpty(fileName))
      fileName = this.FileName;
    FileInfo fileInfo = new FileInfo(fileName);
    progress.Info = $"Saving {fileInfo.Name}";
    if (!this.OnSave(fileName, progress))
      return;
    this.Modified = false;
  }

  protected void CreateForm(FormFileController form)
  {
    this.myForm = form;
    this.myForm.Controller = this;
    FormMain.Instance.AddMDI((Form) this.myForm);
  }

  public virtual void CloseForm(Form form)
  {
    this.myForm = (FormFileController) null;
    this.State &= -5;
  }

  public string FileName => this.myFileName;

  public int State
  {
    get => this.myState;
    set => this.myState = value;
  }

  public bool FileLoaded => (this.State & 2) != 0;

  public bool FormLoaded => (this.State & 4) != 0;

  public FileDatabase Database => this.myDatabase;

  public virtual bool UserAccess => true;

  public string RelativeFileName => this.Database.GetRelativeFileName(this.FileName);

  public string Directory
  {
    get
    {
      int length = this.FileName.LastIndexOf("\\");
      return length >= 0 ? this.FileName.Substring(0, length) : this.FileName;
    }
  }

  public virtual bool Modified
  {
    get => false;
    set
    {
    }
  }
}
