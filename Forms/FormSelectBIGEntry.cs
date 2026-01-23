// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormSelectBIGEntry
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIG;
using FableMod.ContentManagement;
using System;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormSelectBIGEntry : FormSelectEntry
{
  private IContainer components;
  private AssetBank myBank;
  private LinkDestination myLink;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Text = nameof (FormSelectBIGEntry);
  }

  public FormSelectBIGEntry(AssetBank bank, LinkDestination link, string current)
    : base(current)
  {
    this.myBank = bank;
    this.myLink = link;
    this.FindEntries();
    this.listViewEntries.Columns.RemoveAt(1);
    this.listViewEntries.Columns[0].Width = this.listViewEntries.ClientSize.Width;
  }

  public new AssetEntry Selected => (AssetEntry) base.Selected;

  protected override void AddEntries(Regex regex, string name, ProgressBar progressBar)
  {
    progressBar.Maximum = this.myBank.EntryCount;
    for (int index = 0; index < this.myBank.EntryCount; ++index)
    {
      AssetEntry entry = this.myBank.get_Entries(index);
      try
      {
        if (regex != null && (regex.IsMatch(entry.DevSymbolName) || regex.IsMatch(entry.ID.ToString())))
        {
          this.AddEntry(entry.DevSymbolName, "", (object) entry);
        }
        else
        {
          if (!(name == entry.DevSymbolName))
          {
            if (!(name == entry.ID.ToString()))
              goto label_9;
          }
          this.AddEntry(entry.DevSymbolName, "", (object) entry);
        }
      }
      catch (Exception ex)
      {
      }
label_9:
      progressBar.Value = index;
      progressBar.Update();
    }
  }

  protected override void ShowSelectedEntry()
  {
    ContentManager instance = ContentManager.Instance;
    ContentObject contentObject = (ContentObject) null;
    if (this.myLink == LinkDestination.ModelName)
      contentObject = instance.FindEntry(LinkDestination.ModelName, (object) this.Selected.DevSymbolName);
    else if (this.myLink == LinkDestination.MainTextureID || this.myLink == LinkDestination.ModelID || this.myLink == LinkDestination.TextID || this.myLink == LinkDestination.TextGroupID)
      contentObject = instance.FindEntry(this.myLink, (object) this.Selected.ID);
    if (contentObject != null)
    {
      instance.ShowEntry(contentObject.Object, true);
    }
    else
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Object not found");
    }
  }
}
