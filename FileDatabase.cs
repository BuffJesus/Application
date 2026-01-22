// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FileDatabase
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BBB;
using FableMod.BIG;
using FableMod.BIN;
using FableMod.Content.Forms;
using FableMod.ContentManagement;
using FableMod.Gfx.Integration;
using FableMod.STB;
using FableMod.TNG;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FileDatabase : ContentManager
{
  private List<FileController> myFiles = new List<FileController>();
  private FileInterface myInterface;
  private DefinitionDB myDefinitions;
  private TNGDefinitions myTNGDefinitions;

  public FileDatabase(
    FileInterface fileInterface,
    DefinitionDB definitions,
    TNGDefinitions tngdefinitions)
  {
    this.myInterface = fileInterface;
    this.myDefinitions = definitions;
    this.myTNGDefinitions = tngdefinitions;
    ContentManager.SetInstance((ContentManager) this);
  }

  public void CloseFiles()
  {
    foreach (FileController file in this.myFiles)
      file.Close();
    this.ResetBIG();
  }

  public FileController AutoLoad(string fileName)
  {
    FileController fileController = this.Get(fileName);
    if (fileController != null && !fileController.FileLoaded)
    {
      List<Processor> processors = new List<Processor>();
      fileController.LoadProcess(processors);
      FormProcess formProcess = new FormProcess(processors);
      int num = (int) formProcess.ShowDialog();
      formProcess.Dispose();
    }
    return fileController;
  }

  protected override NamesBINFile LoadNames()
  {
    return ((NamesBINController) this.AutoLoad(Settings.GetString("Files", "Names")))?.Names;
  }

  protected override BINFile LoadObjects()
  {
    return ((BINFileController) this.AutoLoad(Settings.GetString("Files", "Objects")))?.BIN;
  }

  protected override BINFile LoadScripts()
  {
    return ((BINFileController) this.AutoLoad(Settings.GetString("Files", "Scripts")))?.BIN;
  }

  protected override BIGBank LoadGraphics()
  {
    return ((BIGFileController) this.AutoLoad(Settings.GetString("Files", "Graphics")))?.BIG.FindBankByName(Settings.GetString("Banks", "Graphics"));
  }

  protected override BIGFile LoadTextures()
  {
    return ((BIGFileController) this.AutoLoad(Settings.GetString("Files", "Textures")))?.BIG;
  }

  protected override BIGBank LoadFrontEndTextures()
  {
    return ((BIGFileController) this.AutoLoad(Settings.GetString("Files", "FrontEndTextures"))).BIG.FindBankByName(Settings.GetString("Banks", "FrontEndTextures"));
  }

  protected override BIGBank GetMainTextureBank(BIGFile textures)
  {
    return textures.FindBankByName(Settings.GetString("Banks", "Textures"));
  }

  protected override BIGBank GetGUITextureBank(BIGFile textures)
  {
    return textures.FindBankByName(Settings.GetString("Banks", "GUITextures"));
  }

  protected override BIGBank LoadText()
  {
    return ((BIGFileController) this.AutoLoad(Settings.GetString("Files", "Text")))?.BIG.FindBankByName(Settings.GetString("Banks", "Text"));
  }

  public override void ShowEntry(object o, bool dialog)
  {
    Form form = (Form) null;
    if (o.GetType() == typeof (BINEntry))
      form = (Form) new FormBINEntry((BINEntry) o);
    else if (o.GetType() == typeof (AssetEntry))
      form = (Form) new FormBIGEntry((AssetEntry) o);
    if (form == null)
      return;
    if (dialog)
    {
      int num = (int) form.ShowDialog();
      form.Dispose();
    }
    else
      FormMain.Instance.AddMDI(form);
  }

  public override object SelectEntry(FableMod.ContentManagement.Link link, object current)
  {
    if (link.To == LinkDestination.NamesBINOffset)
    {
      if (this.AutoLoadNames())
      {
        NamesBINEntry entryByOffset = this.Names.GetEntryByOffset(uint.Parse(current.ToString()));
        string current1 = "";
        if (entryByOffset != null)
          current1 = entryByOffset.Name;
        FormSelectNamesEntry selectNamesEntry = new FormSelectNamesEntry(this.Names, current1, link.Restriction);
        if (selectNamesEntry.ShowDialog() == DialogResult.OK)
        {
          NamesBINEntry selected = selectNamesEntry.Selected;
          selectNamesEntry.Dispose();
          return (object) selected.Offset;
        }
        selectNamesEntry.Dispose();
      }
    }
    else if (link.To == LinkDestination.NamesBINEnum)
    {
      if (this.AutoLoadNames())
      {
        NamesBINEntry entryByEnum = this.Names.GetEntryByEnum(uint.Parse(current.ToString()));
        string current2 = "";
        if (entryByEnum != null)
          current2 = entryByEnum.Name;
        FormSelectNamesEntry selectNamesEntry = new FormSelectNamesEntry(this.Names, current2, link.Restriction);
        if (selectNamesEntry.ShowDialog() == DialogResult.OK)
        {
          NamesBINEntry selected = selectNamesEntry.Selected;
          selectNamesEntry.Dispose();
          return (object) selected.Enum;
        }
        selectNamesEntry.Dispose();
      }
    }
    else if (link.To == LinkDestination.GameBINEntryName)
    {
      if (this.AutoLoadObjects())
      {
        FormSelectBINEntry formSelectBinEntry = new FormSelectBINEntry(this.Objects, current.ToString());
        formSelectBinEntry.CDefType = link.Restriction;
        if (formSelectBinEntry.ShowDialog() == DialogResult.OK)
        {
          BINEntry selected = formSelectBinEntry.Selected;
          formSelectBinEntry.Dispose();
          return (object) selected.Name;
        }
        formSelectBinEntry.Dispose();
      }
    }
    else if (link.To == LinkDestination.GameBINEntryID)
    {
      if (this.AutoLoadObjects())
      {
        FormSelectBINEntry formSelectBinEntry = new FormSelectBINEntry(this.Objects, this.FindEntry(link.To, (object) uint.Parse(current.ToString())).Name);
        formSelectBinEntry.CDefType = link.Restriction;
        if (formSelectBinEntry.ShowDialog() == DialogResult.OK)
        {
          BINEntry selected = formSelectBinEntry.Selected;
          formSelectBinEntry.Dispose();
          return (object) selected.ID;
        }
        formSelectBinEntry.Dispose();
      }
    }
    else if (link.To == LinkDestination.ModelID)
    {
      if (this.AutoLoadGraphics())
      {
        ContentObject entry = this.FindEntry(link.To, (object) uint.Parse(current.ToString()));
        FormSelectBIGEntry formSelectBigEntry = entry == null ? new FormSelectBIGEntry((AssetBank) this.GraphicsBank, link.To, (string) null) : new FormSelectBIGEntry((AssetBank) this.GraphicsBank, link.To, entry.Name);
        if (formSelectBigEntry.ShowDialog() == DialogResult.OK)
        {
          AssetEntry selected = formSelectBigEntry.Selected;
          formSelectBigEntry.Dispose();
          return (object) selected.ID;
        }
        formSelectBigEntry.Dispose();
      }
    }
    else if (link.To == LinkDestination.MainTextureID)
    {
      if (this.AutoLoadTextures())
      {
        ContentObject entry = this.FindEntry(link.To, (object) uint.Parse(current.ToString()));
        FormSelectBIGEntry formSelectBigEntry = entry == null ? new FormSelectBIGEntry((AssetBank) this.MainTextureBank, link.To, (string) null) : new FormSelectBIGEntry((AssetBank) this.MainTextureBank, link.To, entry.Name);
        if (formSelectBigEntry.ShowDialog() == DialogResult.OK)
        {
          AssetEntry selected = formSelectBigEntry.Selected;
          formSelectBigEntry.Dispose();
          return (object) selected.ID;
        }
        formSelectBigEntry.Dispose();
      }
    }
    else if (link.To == LinkDestination.GUITextureID)
    {
      if (this.AutoLoadTextures())
      {
        ContentObject entry = this.FindEntry(link.To, (object) uint.Parse(current.ToString()));
        FormSelectBIGEntry formSelectBigEntry = entry == null ? new FormSelectBIGEntry((AssetBank) this.GUITextureBank, link.To, (string) null) : new FormSelectBIGEntry((AssetBank) this.GUITextureBank, link.To, entry.Name);
        if (formSelectBigEntry.ShowDialog() == DialogResult.OK)
        {
          AssetEntry selected = formSelectBigEntry.Selected;
          formSelectBigEntry.Dispose();
          return (object) selected.ID;
        }
        formSelectBigEntry.Dispose();
      }
    }
    else if (link.To == LinkDestination.FrontEndTextureID)
    {
      if (this.AutoLoadFrontEndTextures())
      {
        ContentObject entry = this.FindEntry(link.To, (object) uint.Parse(current.ToString()));
        FormSelectBIGEntry formSelectBigEntry = entry == null ? new FormSelectBIGEntry((AssetBank) this.FrontEndTextureBank, link.To, (string) null) : new FormSelectBIGEntry((AssetBank) this.FrontEndTextureBank, link.To, entry.Name);
        if (formSelectBigEntry.ShowDialog() == DialogResult.OK)
        {
          AssetEntry selected = formSelectBigEntry.Selected;
          formSelectBigEntry.Dispose();
          return (object) selected.ID;
        }
        formSelectBigEntry.Dispose();
      }
    }
    else if (link.To == LinkDestination.ModelName && this.AutoLoadGraphics())
    {
      FormSelectBIGEntry formSelectBigEntry = new FormSelectBIGEntry((AssetBank) this.GraphicsBank, link.To, current.ToString());
      if (formSelectBigEntry.ShowDialog() == DialogResult.OK)
      {
        AssetEntry selected = formSelectBigEntry.Selected;
        formSelectBigEntry.Dispose();
        return (object) selected.DevSymbolName;
      }
      formSelectBigEntry.Dispose();
    }
    return (object) null;
  }

  public ContentObject GetContentObject(object o)
  {
    if (o.GetType() == typeof (AssetEntry))
    {
      AssetEntry assetEntry = (AssetEntry) o;
      ContentType contentType = ContentType.Unknown;
      if (assetEntry.Bank.GetType() == typeof (BIGBank))
      {
        BIGBank bank = (BIGBank) assetEntry.Bank;
        if (bank.Name == Settings.GetString("Banks", "Graphics"))
          contentType = ContentType.Graphics;
        else if (bank.Name == Settings.GetString("Banks", "Text"))
          contentType = ContentType.Text;
        else if (bank.Name == Settings.GetString("Banks", "Textures"))
          contentType = ContentType.MainTextures;
        else if (bank.Name == Settings.GetString("Banks", "GUITextures"))
          contentType = ContentType.GUITextures;
        else if (bank.Name == Settings.GetString("Banks", "FrontEndTextures"))
          contentType = ContentType.FrontEndTextures;
      }
      return new ContentObject(assetEntry.DevSymbolName, (object) assetEntry, contentType);
    }
    if (!(o.GetType() == typeof (BINEntry)))
      return (ContentObject) null;
    if (!this.AutoLoadObjects())
      return (ContentObject) null;
    BINEntry binEntry = (BINEntry) o;
    ContentType contentType1 = ContentType.Objects;
    if (this.Objects.GetEntryByName(binEntry.Name) != null)
      contentType1 = ContentType.Objects;
    else if (binEntry.Definition == "CCutsceneDef")
      contentType1 = ContentType.Scripts;
    return new ContentObject(binEntry.Name, (object) binEntry, contentType1);
  }

  public override AssetLinker CreateAssetLinker(ContentType contentType, AssetEntry entry)
  {
    return ((BIGBank) entry.Bank).Name == Settings.GetString("Banks", "Graphics") && (entry.Type == 1U || entry.Type == 2U || entry.Type == 4U || entry.Type == 5U) ? (AssetLinker) new GfxModel(entry) : (AssetLinker) null;
  }

  public FileController Find(string fileName)
  {
    for (int index = 0; index < this.FileCount; ++index)
    {
      if (string.Compare(this.GetFileAt(index).FileName, fileName, true) == 0)
        return this.GetFileAt(index);
    }
    return (FileController) null;
  }

  private string GetAbsoluteFileName(string fileName)
  {
    string fableDirectory = Settings.FableDirectory;
    if (fileName.IndexOf(":\\") < 0)
      fileName = fableDirectory + fileName;
    return fileName;
  }

  public string GetRelativeFileName(string fileName)
  {
    string fableDirectory = Settings.FableDirectory;
    if (fileName.IndexOf(fableDirectory) == 0)
      fileName = fileName.Substring(fableDirectory.Length);
    return fileName;
  }

  public WLDFileController GetWLD()
  {
    return (WLDFileController) this.AutoLoad(Settings.GetString("Files", "World"));
  }

  public BBBFileController GetSTB()
  {
    return (BBBFileController) this.AutoLoad(Settings.GetString("Files", "STB"));
  }

  public FileController Get(string fileName)
  {
    fileName = this.GetAbsoluteFileName(fileName);
    FileController fileController = this.Find(fileName);
    if (fileController != null)
      return fileController;
    FileInfo fileInfo = new FileInfo(fileName);
    switch (fileInfo.Extension.ToUpper())
    {
      case ".BIG":
        fileController = (FileController) new BIGFileController(this, fileName);
        break;
      case ".BIN":
        fileController = !(fileInfo.Name.ToUpper() == "NAMES.BIN") ? (FileController) new BINFileController(this, fileName) : (FileController) new NamesBINController(this, fileName);
        break;
      case ".STB":
        fileController = (FileController) new BBBFileController(this, (BBBFile) new STBFile(), fileName);
        break;
      case ".WAD":
        fileController = (FileController) new BBBFileController(this, new BBBFile(), fileName);
        break;
      case ".WLD":
        fileController = (FileController) new WLDFileController(this, fileName);
        break;
    }
    if (fileController != null)
      this.myFiles.Add(fileController);
    return fileController;
  }

  public static FileDatabase Instance => (FileDatabase) ContentManager.Instance;

  public int FileCount => this.myFiles.Count;

  public FileController GetFileAt(int index) => this.myFiles[index];

  public override DefinitionDB Definitions => this.myDefinitions;

  public TNGDefinitions TNGDefinitions => this.myTNGDefinitions;

  public FileInterface Interface => this.myInterface;

  public override string DataDirectory => Settings.FableDirectory;
}
