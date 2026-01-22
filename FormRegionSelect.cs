// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormRegionSelect
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormRegionSelect : Form
{
  private IContainer components;
  private Panel panel1;
  private Button buttonOK;
  private Button buttonCancel;
  public ListBox listBoxRegions;

  public FormRegionSelect() => this.InitializeComponent();

  private void listBoxRegions_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.buttonOK.Enabled = this.listBoxRegions.SelectedIndex >= 0;
  }

  private void listBoxRegions_DoubleClick(object sender, EventArgs e)
  {
    if (this.listBoxRegions.SelectedIndex < 0)
      return;
    this.DialogResult = DialogResult.OK;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.panel1 = new Panel();
    this.buttonOK = new Button();
    this.buttonCancel = new Button();
    this.listBoxRegions = new ListBox();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.panel1.Controls.Add((Control) this.buttonOK);
    this.panel1.Controls.Add((Control) this.buttonCancel);
    this.panel1.Dock = DockStyle.Bottom;
    this.panel1.Location = new Point(0, 369);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(298, 44);
    this.panel1.TabIndex = 0;
    this.buttonOK.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonOK.DialogResult = DialogResult.OK;
    this.buttonOK.Enabled = false;
    this.buttonOK.Location = new Point(131, 9);
    this.buttonOK.Name = "buttonOK";
    this.buttonOK.Size = new Size(75, 23);
    this.buttonOK.TabIndex = 1;
    this.buttonOK.Text = "&OK";
    this.buttonOK.UseVisualStyleBackColor = true;
    this.buttonCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Location = new Point(212, 9);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(75, 23);
    this.buttonCancel.TabIndex = 0;
    this.buttonCancel.Text = "&Cancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.listBoxRegions.Dock = DockStyle.Fill;
    this.listBoxRegions.FormattingEnabled = true;
    this.listBoxRegions.Location = new Point(0, 0);
    this.listBoxRegions.Name = "listBoxRegions";
    this.listBoxRegions.Size = new Size(298, 368);
    this.listBoxRegions.Sorted = true;
    this.listBoxRegions.TabIndex = 1;
    this.listBoxRegions.SelectedIndexChanged += new EventHandler(this.listBoxRegions_SelectedIndexChanged);
    this.listBoxRegions.DoubleClick += new EventHandler(this.listBoxRegions_DoubleClick);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(298, 413);
    this.Controls.Add((Control) this.listBoxRegions);
    this.Controls.Add((Control) this.panel1);
    this.MinimumSize = new Size(185, 34);
    this.Name = nameof (FormRegionSelect);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Select Region";
    this.panel1.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
