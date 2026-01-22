// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormFind
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormFind : Form
{
  private IContainer components;
  private Button buttonCancel;
  private Button buttonOK;
  internal TextBox textBoxName;
  public Label labelInfo;
  private Label label1;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.labelInfo = new Label();
    this.textBoxName = new TextBox();
    this.buttonCancel = new Button();
    this.buttonOK = new Button();
    this.label1 = new Label();
    this.SuspendLayout();
    this.labelInfo.AutoSize = true;
    this.labelInfo.Location = new Point(12, 9);
    this.labelInfo.Name = "labelInfo";
    this.labelInfo.Size = new Size(44, 13);
    this.labelInfo.TabIndex = 0;
    this.labelInfo.Text = "Pattern:";
    this.textBoxName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.textBoxName.Location = new Point(62, 6);
    this.textBoxName.Name = "textBoxName";
    this.textBoxName.Size = new Size(292, 20);
    this.textBoxName.TabIndex = 0;
    this.textBoxName.TextChanged += new EventHandler(this.textBoxName_TextChanged);
    this.buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Location = new Point(279, 60);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(75, 23);
    this.buttonCancel.TabIndex = 2;
    this.buttonCancel.Text = "Cancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonOK.DialogResult = DialogResult.OK;
    this.buttonOK.Enabled = false;
    this.buttonOK.Location = new Point(198, 60);
    this.buttonOK.Name = "buttonOK";
    this.buttonOK.Size = new Size(75, 23);
    this.buttonOK.TabIndex = 1;
    this.buttonOK.Text = "OK";
    this.buttonOK.UseVisualStyleBackColor = true;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(59, 29);
    this.label1.Name = "label1";
    this.label1.Size = new Size(174, 13);
    this.label1.TabIndex = 3;
    this.label1.Text = "(Regex based patterns are allowed)";
    this.AcceptButton = (IButtonControl) this.buttonOK;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.ClientSize = new Size(366, 95);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.buttonOK);
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.textBoxName);
    this.Controls.Add((Control) this.labelInfo);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FormFind);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Find";
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public FormFind() => this.InitializeComponent();

  private void textBoxName_TextChanged(object sender, EventArgs e)
  {
    this.buttonOK.Enabled = this.textBoxName.Text.Length > 0;
  }
}
