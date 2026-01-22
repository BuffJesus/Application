// Decompiled with JetBrains decompiler
// Type: FableMod.Content.Forms.FormOpenTexture
// Assembly: FableMod.Content.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A575F982-23D4-4DAE-A571-3D769462C59B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Content.Forms.dll

using FableMod.Gfx.Integration;
using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Content.Forms;

public class FormOpenTexture : Form
{
  private ComboBox cbFormat;
  private Label lblFormat;
  private Label lblFile;
  private Button btnBrowse;
  private Button btnOpen;
  private Button btnCancel;
  public string FileName;
  public TextureFormat Format;
  private OpenFileDialog openFileDialog;
  private TextBox textBoxFileName;
  private System.ComponentModel.Container components;

  public FormOpenTexture()
  {
    this.InitializeComponent();
    this.cbFormat.Items.Clear();
    this.cbFormat.Items.AddRange((object[]) Enum.GetNames(typeof (TextureFormat)));
    this.cbFormat.SelectedIndex = 0;
    this.openFileDialog.Filter = GfxTexture.LOAD_FILE_FILTER;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.lblFormat = new Label();
    this.cbFormat = new ComboBox();
    this.lblFile = new Label();
    this.btnBrowse = new Button();
    this.btnOpen = new Button();
    this.btnCancel = new Button();
    this.openFileDialog = new OpenFileDialog();
    this.textBoxFileName = new TextBox();
    this.SuspendLayout();
    this.lblFormat.AutoSize = true;
    this.lblFormat.Location = new Point(16 /*0x10*/, 16 /*0x10*/);
    this.lblFormat.Name = "lblFormat";
    this.lblFormat.Size = new Size(74, 13);
    this.lblFormat.TabIndex = 0;
    this.lblFormat.Text = "Import Format:";
    this.cbFormat.DropDownStyle = ComboBoxStyle.DropDownList;
    this.cbFormat.Location = new Point(96 /*0x60*/, 12);
    this.cbFormat.Name = "cbFormat";
    this.cbFormat.Size = new Size(66, 21);
    this.cbFormat.TabIndex = 1;
    this.lblFile.AutoSize = true;
    this.lblFile.Location = new Point(16 /*0x10*/, 44);
    this.lblFile.Name = "lblFile";
    this.lblFile.Size = new Size(52, 13);
    this.lblFile.TabIndex = 2;
    this.lblFile.Text = "Filename:";
    this.btnBrowse.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnBrowse.Location = new Point(476, 40);
    this.btnBrowse.Name = "btnBrowse";
    this.btnBrowse.Size = new Size(32 /*0x20*/, 21);
    this.btnBrowse.TabIndex = 4;
    this.btnBrowse.Text = "...";
    this.btnBrowse.Click += new EventHandler(this.btnBrowse_Click);
    this.btnOpen.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnOpen.Enabled = false;
    this.btnOpen.Location = new Point(350, 73);
    this.btnOpen.Name = "btnOpen";
    this.btnOpen.Size = new Size(76, 24);
    this.btnOpen.TabIndex = 5;
    this.btnOpen.Text = "Open";
    this.btnOpen.Click += new EventHandler(this.btnOpen_Click);
    this.btnCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnCancel.Location = new Point(432, 73);
    this.btnCancel.Name = "btnCancel";
    this.btnCancel.Size = new Size(76, 24);
    this.btnCancel.TabIndex = 6;
    this.btnCancel.Text = "Cancel";
    this.btnCancel.Click += new EventHandler(this.btnCancel_Click);
    this.textBoxFileName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.textBoxFileName.Location = new Point(96 /*0x60*/, 41);
    this.textBoxFileName.Name = "textBoxFileName";
    this.textBoxFileName.ReadOnly = true;
    this.textBoxFileName.Size = new Size(374, 20);
    this.textBoxFileName.TabIndex = 7;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.ClientSize = new Size(520, 109);
    this.Controls.Add((Control) this.textBoxFileName);
    this.Controls.Add((Control) this.btnCancel);
    this.Controls.Add((Control) this.btnOpen);
    this.Controls.Add((Control) this.btnBrowse);
    this.Controls.Add((Control) this.lblFile);
    this.Controls.Add((Control) this.cbFormat);
    this.Controls.Add((Control) this.lblFormat);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FormOpenTexture);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Open Texture";
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void btnOpen_Click(object sender, EventArgs e)
  {
    this.Format = (TextureFormat) Enum.Parse(typeof (TextureFormat), this.cbFormat.SelectedItem.ToString(), true);
    this.DialogResult = DialogResult.OK;
    this.Close();
  }

  private void btnBrowse_Click(object sender, EventArgs e)
  {
    if (this.openFileDialog.ShowDialog((IWin32Window) this) != DialogResult.OK)
      return;
    this.FileName = this.openFileDialog.FileName;
    this.textBoxFileName.Text = this.FileName;
    this.btnOpen.Enabled = true;
  }

  private void btnCancel_Click(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.Cancel;
    this.Close();
  }
}
