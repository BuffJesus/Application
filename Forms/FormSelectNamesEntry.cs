// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormSelectNamesEntry
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIN;
using System.ComponentModel;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormSelectNamesEntry : FormSelectEntry
{
  private NamesBINFile myNames;
  private Regex myRestriction;
  private IContainer components;

  public FormSelectNamesEntry(NamesBINFile names, string current, string restriction)
    : base(current)
  {
    this.InitializeComponent();
    this.myNames = names;
    this.myRestriction = new Regex(restriction);
  }

  public NamesBINEntry Selected => (NamesBINEntry) base.Selected;

  protected override void AddEntries(Regex regex, string name, ProgressBar progressBar)
  {
    progressBar.Maximum = this.myNames.EntryCount;
    for (int index = 0; index < this.myNames.EntryCount; ++index)
    {
      NamesBINEntry entry = this.myNames.get_Entries(index);
      if (this.myRestriction.IsMatch(entry.Name))
      {
        if (regex != null && regex.IsMatch(entry.Name))
          this.AddEntry(entry.Name, "", (object) entry);
        else if (name == entry.Name)
          this.AddEntry(entry.Name, "", (object) entry);
      }
      progressBar.Value = index;
      progressBar.Update();
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
    this.components = (IContainer) new System.ComponentModel.Container();
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Text = nameof (FormSelectNamesEntry);
  }
}
