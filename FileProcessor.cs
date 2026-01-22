// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FileProcessor
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

#nullable disable
namespace ChocolateBox;

internal class FileProcessor : Processor
{
  private FileProcessorMode myMode;
  private FileController myController;
  private string myFileName;

  public FileProcessor(FileController c, FileProcessorMode mode)
  {
    this.myController = c;
    this.myMode = mode;
  }

  public FileProcessor(FileController c, string fileName)
  {
    this.myController = c;
    this.myMode = FileProcessorMode.SaveFile;
    this.myFileName = fileName;
  }

  public override void Run(Progress progress)
  {
    if (this.myMode == FileProcessorMode.LoadFile)
      this.myController.LoadFile(progress);
    else if (this.myMode == FileProcessorMode.LoadForm)
    {
      this.myController.LoadForm(progress);
    }
    else
    {
      if (this.myMode != FileProcessorMode.SaveFile)
        return;
      this.myController.SaveFile(this.myFileName, progress);
    }
  }
}
