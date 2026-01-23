// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormFileController
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormFileController : Form
{
  private IContainer components;
  private FileController myController;

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
    this.ClientSize = new Size(292, 266);
    this.Name = nameof (FormFileController);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = nameof (FormFileController);
    this.FormClosing += new FormClosingEventHandler(this.FormFileController_FormClosing);
    this.ResumeLayout(false);
  }

  public FormFileController()
  {
    this.InitializeComponent();
    ThemeManager.ApplyTheme(this);
  }

  public FileController Controller
  {
    get => this.myController;
    set => this.myController = value;
  }

  protected void SaveChanges()
  {
  }

  private void FormFileController_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (e.CloseReason != CloseReason.UserClosing)
      return;
    if (this.Modified)
    {
      switch (MessageBox.Show("Save Changes?", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
      {
        case DialogResult.Cancel:
          e.Cancel = true;
          break;
        case DialogResult.Yes:
          this.SaveChanges();
          break;
      }
    }
    this.Controller.CloseForm((Form) this);
  }

  protected override void OnLoad(EventArgs e)
  {
    if (this.Controller != null)
      this.Text = $"{this.Text} - {this.Controller.RelativeFileName}";
    base.OnLoad(e);
  }

  protected virtual bool Modified => false;
}
