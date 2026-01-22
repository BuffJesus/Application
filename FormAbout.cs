// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormAbout
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormAbout : Form
{
  private IContainer components;
  private Button buttonOK;
  private Label labelInfo;
  private Panel panelInfo;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.buttonOK = new Button();
    this.labelInfo = new Label();
    this.panelInfo = new Panel();
    this.panelInfo.SuspendLayout();
    this.SuspendLayout();
    this.buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonOK.DialogResult = DialogResult.Cancel;
    this.buttonOK.Location = new Point(233, 274);
    this.buttonOK.Name = "buttonOK";
    this.buttonOK.Size = new Size(75, 23);
    this.buttonOK.TabIndex = 1;
    this.buttonOK.Text = "OK";
    this.buttonOK.UseVisualStyleBackColor = true;
    this.labelInfo.BackColor = Color.White;
    this.labelInfo.Dock = DockStyle.Fill;
    this.labelInfo.FlatStyle = FlatStyle.Flat;
    this.labelInfo.ForeColor = Color.Black;
    this.labelInfo.Location = new Point(0, 0);
    this.labelInfo.Name = "labelInfo";
    this.labelInfo.Size = new Size(292, 241);
    this.labelInfo.TabIndex = 2;
    this.labelInfo.Text = "ChocolateBox\r\n\r\nVersion  [Version]\r\nDate [Date]\r\n\r\nBrought to you by\r\n(in alphabetical order)\r\n\r\nAilia\r\nBayStone\r\nchaos\r\nHunter\r\nJohnDoe\r\nKeshire\r\nMarcopolo\r\nmorerunes\r\nSatan\r\nSilverback";
    this.labelInfo.TextAlign = ContentAlignment.MiddleCenter;
    this.panelInfo.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.panelInfo.BackColor = Color.White;
    this.panelInfo.BorderStyle = BorderStyle.Fixed3D;
    this.panelInfo.Controls.Add((Control) this.labelInfo);
    this.panelInfo.Location = new Point(12, 12);
    this.panelInfo.Name = "panelInfo";
    this.panelInfo.Size = new Size(296, 245);
    this.panelInfo.TabIndex = 3;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(320, 309);
    this.Controls.Add((Control) this.panelInfo);
    this.Controls.Add((Control) this.buttonOK);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FormAbout);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "About ChocolateBox";
    this.panelInfo.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public FormAbout()
  {
    this.InitializeComponent();
    this.labelInfo.Text = this.labelInfo.Text.Replace("[Version]", Assembly.GetExecutingAssembly().GetName().Version.ToString()).Replace("[Date]", File.GetLastWriteTime(Application.ExecutablePath).ToString());
  }
}
