// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormSelectBINEntry
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIN;
using FableMod.ContentManagement;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormSelectBINEntry : FormSelectEntry
{
  private BINFile myBIN;
  private string myCDef;
  private IContainer components;

  public FormSelectBINEntry(BINFile binfile, string current)
    : base(current)
  {
    this.myBIN = binfile;
    this.FindEntries();
  }

  public new BINEntry Selected => (BINEntry) base.Selected;

  public string CDefType
  {
    get => this.myCDef;
    set => this.myCDef = value;
  }

  protected override void AddEntries(Regex regex, string name, ProgressBar progressBar)
  {
    progressBar.Maximum = this.myBIN.EntryCount;
    List<string> stringList = (List<string>) null;
    if (!string.IsNullOrEmpty(this.myCDef))
      stringList = new List<string>((IEnumerable<string>) this.myCDef.Split(';'));
    for (int index = 0; index < this.myBIN.EntryCount; ++index)
    {
      BINEntry entry = this.myBIN.get_Entries(index);
      if ((regex != null && regex.IsMatch(entry.Name) || name == entry.Name) && (stringList == null || stringList.IndexOf(entry.Definition) >= 0))
        this.AddEntry(entry.Name, "", (object) entry);
      progressBar.Value = index;
      progressBar.Update();
    }
  }

  protected override void ShowSelectedEntry()
  {
    ContentManager instance = ContentManager.Instance;
    if (instance.FindEntry(LinkDestination.GameBINEntryName, (object) this.Selected.Name) == null)
      return;
    DefinitionType definition = instance.Definitions.GetDefinition(this.Selected.Definition);
    if (definition == null)
      return;
    definition.ReadIn(this.Selected);
    FableMod.ContentManagement.Control control = definition.FindControl(3361958702U);
    if (control != null && control.Members.Count == 5)
    {
      Member member = (Member) control.Members[1];
      instance.ShowEntry(LinkDestination.ModelID, member.Value, true);
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(488, 403);
    this.Name = nameof (FormSelectBINEntry);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Select Object";
    this.ResumeLayout(false);
  }
}
