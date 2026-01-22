// Decompiled with JetBrains decompiler
// Type: FableMod.Content.Forms.ControlBIGEntry
// Assembly: FableMod.Content.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A575F982-23D4-4DAE-A571-3D769462C59B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Content.Forms.dll

using FableMod.BIG;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Content.Forms;

public class ControlBIGEntry : UserControl
{
  private System.ComponentModel.Container components;
  private Label lblEntryTypeLabel;
  protected internal Label lblEntryType;
  protected AssetEntry m_Entry;

  public ControlBIGEntry() => this.InitializeComponent();

  public virtual void OnActivate()
  {
  }

  public virtual void OnDeactivate()
  {
  }

  public string EntryType
  {
    get => this.lblEntryType.Text;
    set => this.lblEntryType.Text = value;
  }

  public virtual void ApplyChanges()
  {
  }

  public virtual AssetEntry BIGEntry
  {
    get => this.m_Entry;
    set => this.m_Entry = value;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.lblEntryTypeLabel = new Label();
    this.lblEntryType = new Label();
    this.SuspendLayout();
    this.lblEntryTypeLabel.Location = new Point(8, 8);
    this.lblEntryTypeLabel.Name = "lblEntryTypeLabel";
    this.lblEntryTypeLabel.Size = new Size(72, 16 /*0x10*/);
    this.lblEntryTypeLabel.TabIndex = 0;
    this.lblEntryTypeLabel.Text = "Entry Type :";
    this.lblEntryType.AutoSize = true;
    this.lblEntryType.Location = new Point(72, 8);
    this.lblEntryType.Name = "lblEntryType";
    this.lblEntryType.Size = new Size(53, 13);
    this.lblEntryType.TabIndex = 1;
    this.lblEntryType.Text = "Unknown";
    this.Controls.Add((Control) this.lblEntryType);
    this.Controls.Add((Control) this.lblEntryTypeLabel);
    this.Name = nameof (ControlBIGEntry);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
