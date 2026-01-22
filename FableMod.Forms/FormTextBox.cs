// Decompiled with JetBrains decompiler
// Type: FableMod.Forms.FormTextBox
// Assembly: FableMod.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 910E5594-6600-4712-BB52-1327761AD253
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Forms.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Forms;

public class FormTextBox : Form
{
  private IContainer components;
  private Button buttonCancel;
  private Button buttonOk;
  public Label labelInput;
  public TextBox textBoxInput;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.labelInput = new Label();
    this.textBoxInput = new TextBox();
    this.buttonCancel = new Button();
    this.buttonOk = new Button();
    this.SuspendLayout();
    this.labelInput.AutoSize = true;
    this.labelInput.Location = new Point(9, 9);
    this.labelInput.Name = "labelInput";
    this.labelInput.Size = new Size(34, 13);
    this.labelInput.TabIndex = 0;
    this.labelInput.Text = "Input:";
    this.textBoxInput.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.textBoxInput.Location = new Point(12, 25);
    this.textBoxInput.Name = "textBoxInput";
    this.textBoxInput.Size = new Size(375, 20);
    this.textBoxInput.TabIndex = 1;
    this.textBoxInput.TextChanged += new EventHandler(this.textBoxInput_TextChanged);
    this.buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Location = new Point(312, 57);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(75, 23);
    this.buttonCancel.TabIndex = 2;
    this.buttonCancel.Text = "Cancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.buttonOk.DialogResult = DialogResult.OK;
    this.buttonOk.Enabled = false;
    this.buttonOk.Location = new Point(231, 57);
    this.buttonOk.Name = "buttonOk";
    this.buttonOk.Size = new Size(75, 23);
    this.buttonOk.TabIndex = 3;
    this.buttonOk.Text = "OK";
    this.buttonOk.UseVisualStyleBackColor = true;
    this.AcceptButton = (IButtonControl) this.buttonOk;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.ClientSize = new Size(399, 92);
    this.Controls.Add((Control) this.buttonOk);
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.textBoxInput);
    this.Controls.Add((Control) this.labelInput);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FormTextBox);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Input Text";
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public FormTextBox() => this.InitializeComponent();

  private void textBoxInput_TextChanged(object sender, EventArgs e)
  {
    if (this.textBoxInput.Text.Length <= 0)
      return;
    this.buttonOk.Enabled = true;
  }
}
