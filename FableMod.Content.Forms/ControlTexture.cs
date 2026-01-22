// Decompiled with JetBrains decompiler
// Type: FableMod.Content.Forms.ControlTexture
// Assembly: FableMod.Content.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A575F982-23D4-4DAE-A571-3D769462C59B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Content.Forms.dll

using FableMod.BIG;
using FableMod.ContentManagement;
using FableMod.Gfx.Integration;
using System;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Content.Forms;

public class ControlTexture : ControlBIGEntry
{
  private Label lblWidth;
  private TextBox txtWidth;
  private TextBox txtHeight;
  private Label lblHeight;
  private TextBox txtDepth;
  private Label lblDepth;
  private TextBox txtFrameWidth;
  private Label lblFrameWidth;
  private TextBox txtFrameHeight;
  private Label lblFrameHeight;
  private TextBox txtFrameCount;
  private Label lblFrameCount;
  private Label lblPreview;
  private System.ComponentModel.Container components;
  private Button btnSave;
  private Label lblCurrentFrame;
  private NumericUpDown nudCurrentFrame;
  private Button btnOpenNew;
  private Button btnAddFrame;
  private Label lblAlpha;
  private NumericUpDown nudAlpha;
  private OpenFileDialog openFileDialog;
  private SaveFileDialog saveFileDialog;
  private Panel panelTexture;
  private GfxTexture m_Texture;

  public ControlTexture()
  {
    this.InitializeComponent();
    this.SetStyle(ControlStyles.ResizeRedraw, true);
    this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
    this.SetStyle(ControlStyles.UserPaint, true);
    this.openFileDialog.Filter = GfxTexture.LOAD_FILE_FILTER;
    this.saveFileDialog.Filter = GfxTexture.SAVE_FILE_FILTER;
  }

  public override void ApplyChanges()
  {
    try
    {
      this.m_Texture.FrameWidth = int.Parse(this.txtFrameWidth.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Frame Width entered was not a valid frame width.", "Invalid Frame Width");
    }
    try
    {
      this.m_Texture.FrameHeight = int.Parse(this.txtFrameHeight.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Frame Height entered was not a valid frame height.", "Invalid Frame Height");
    }
    this.m_Texture.AlphaChannels = (byte) this.nudAlpha.Value;
    this.m_Texture.ApplyToEntry(this.m_Entry);
  }

  private void RefreshDisplay()
  {
    if (this.m_Texture != null)
    {
      this.txtWidth.Text = this.m_Texture.Width.ToString();
      this.txtHeight.Text = this.m_Texture.Height.ToString();
      this.txtDepth.Text = this.m_Texture.Depth.ToString();
      this.txtFrameCount.Text = this.m_Texture.FrameCount.ToString();
      this.txtFrameWidth.Text = this.m_Texture.FrameWidth.ToString();
      this.txtFrameHeight.Text = this.m_Texture.FrameHeight.ToString();
      this.nudAlpha.Value = (Decimal) this.m_Texture.AlphaChannels;
      this.EntryType = "Texture - " + this.m_Texture.Format.ToString();
      this.nudCurrentFrame.Maximum = (Decimal) (this.m_Texture.FrameCount - 1);
    }
    this.Invalidate();
  }

  public override AssetEntry BIGEntry
  {
    set
    {
      this.m_Entry = value;
      this.m_Texture = new GfxTexture(this.m_Entry);
      this.RefreshDisplay();
      this.Invalidate(true);
    }
  }

  protected override void OnPaintBackground(PaintEventArgs pevent)
  {
    base.OnPaintBackground(pevent);
    if (this.m_Texture == null)
      return;
    this.m_Texture.Draw((System.Windows.Forms.Control) this.panelTexture, this.panelTexture.ClientRectangle, (int) this.nudCurrentFrame.Value);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.lblWidth = new Label();
    this.txtWidth = new TextBox();
    this.txtHeight = new TextBox();
    this.lblHeight = new Label();
    this.txtDepth = new TextBox();
    this.lblDepth = new Label();
    this.txtFrameWidth = new TextBox();
    this.lblFrameWidth = new Label();
    this.txtFrameHeight = new TextBox();
    this.lblFrameHeight = new Label();
    this.txtFrameCount = new TextBox();
    this.lblFrameCount = new Label();
    this.lblPreview = new Label();
    this.btnSave = new Button();
    this.nudCurrentFrame = new NumericUpDown();
    this.lblCurrentFrame = new Label();
    this.btnOpenNew = new Button();
    this.btnAddFrame = new Button();
    this.lblAlpha = new Label();
    this.nudAlpha = new NumericUpDown();
    this.openFileDialog = new OpenFileDialog();
    this.saveFileDialog = new SaveFileDialog();
    this.panelTexture = new Panel();
    this.nudCurrentFrame.BeginInit();
    this.nudAlpha.BeginInit();
    this.SuspendLayout();
    this.lblWidth.Location = new Point(12, 32 /*0x20*/);
    this.lblWidth.Name = "lblWidth";
    this.lblWidth.Size = new Size(40, 16 /*0x10*/);
    this.lblWidth.TabIndex = 2;
    this.lblWidth.Text = "Width:";
    this.txtWidth.Location = new Point(48 /*0x30*/, 28);
    this.txtWidth.Name = "txtWidth";
    this.txtWidth.ReadOnly = true;
    this.txtWidth.Size = new Size(44, 20);
    this.txtWidth.TabIndex = 3;
    this.txtHeight.Location = new Point(48 /*0x30*/, 52);
    this.txtHeight.Name = "txtHeight";
    this.txtHeight.ReadOnly = true;
    this.txtHeight.Size = new Size(44, 20);
    this.txtHeight.TabIndex = 5;
    this.lblHeight.Location = new Point(12, 56);
    this.lblHeight.Name = "lblHeight";
    this.lblHeight.Size = new Size(40, 16 /*0x10*/);
    this.lblHeight.TabIndex = 4;
    this.lblHeight.Text = "Height:";
    this.txtDepth.Location = new Point(48 /*0x30*/, 76);
    this.txtDepth.Name = "txtDepth";
    this.txtDepth.ReadOnly = true;
    this.txtDepth.Size = new Size(44, 20);
    this.txtDepth.TabIndex = 7;
    this.lblDepth.Location = new Point(12, 80 /*0x50*/);
    this.lblDepth.Name = "lblDepth";
    this.lblDepth.Size = new Size(40, 16 /*0x10*/);
    this.lblDepth.TabIndex = 6;
    this.lblDepth.Text = "Depth:";
    this.txtFrameWidth.Location = new Point(168, 28);
    this.txtFrameWidth.Name = "txtFrameWidth";
    this.txtFrameWidth.Size = new Size(44, 20);
    this.txtFrameWidth.TabIndex = 9;
    this.lblFrameWidth.Location = new Point(100, 32 /*0x20*/);
    this.lblFrameWidth.Name = "lblFrameWidth";
    this.lblFrameWidth.Size = new Size(72, 16 /*0x10*/);
    this.lblFrameWidth.TabIndex = 8;
    this.lblFrameWidth.Text = "Frame Width:";
    this.txtFrameHeight.Location = new Point(168, 52);
    this.txtFrameHeight.Name = "txtFrameHeight";
    this.txtFrameHeight.Size = new Size(44, 20);
    this.txtFrameHeight.TabIndex = 11;
    this.lblFrameHeight.Location = new Point(96 /*0x60*/, 56);
    this.lblFrameHeight.Name = "lblFrameHeight";
    this.lblFrameHeight.Size = new Size(76, 16 /*0x10*/);
    this.lblFrameHeight.TabIndex = 10;
    this.lblFrameHeight.Text = "Frame Height:";
    this.txtFrameCount.Location = new Point(168, 76);
    this.txtFrameCount.Name = "txtFrameCount";
    this.txtFrameCount.ReadOnly = true;
    this.txtFrameCount.Size = new Size(44, 20);
    this.txtFrameCount.TabIndex = 13;
    this.lblFrameCount.Location = new Point(100, 80 /*0x50*/);
    this.lblFrameCount.Name = "lblFrameCount";
    this.lblFrameCount.Size = new Size(76, 16 /*0x10*/);
    this.lblFrameCount.TabIndex = 14;
    this.lblFrameCount.Text = "Frame Count:";
    this.lblPreview.Location = new Point(12, 104);
    this.lblPreview.Name = "lblPreview";
    this.lblPreview.Size = new Size(48 /*0x30*/, 16 /*0x10*/);
    this.lblPreview.TabIndex = 15;
    this.lblPreview.Text = "Preview:";
    this.btnSave.Location = new Point(312, 52);
    this.btnSave.Name = "btnSave";
    this.btnSave.Size = new Size(92, 26);
    this.btnSave.TabIndex = 16 /*0x10*/;
    this.btnSave.Text = "Save As...";
    this.btnSave.Click += new EventHandler(this.btnSave_Click);
    this.nudCurrentFrame.Location = new Point(128 /*0x80*/, 100);
    this.nudCurrentFrame.Maximum = new Decimal(new int[4]);
    this.nudCurrentFrame.Name = "nudCurrentFrame";
    this.nudCurrentFrame.Size = new Size(36, 20);
    this.nudCurrentFrame.TabIndex = 19;
    this.nudCurrentFrame.ValueChanged += new EventHandler(this.nudCurrentFrame_ValueChanged);
    this.lblCurrentFrame.Location = new Point(88, 104);
    this.lblCurrentFrame.Name = "lblCurrentFrame";
    this.lblCurrentFrame.Size = new Size(44, 16 /*0x10*/);
    this.lblCurrentFrame.TabIndex = 18;
    this.lblCurrentFrame.Text = "Frame:";
    this.btnOpenNew.Location = new Point(312, 20);
    this.btnOpenNew.Name = "btnOpenNew";
    this.btnOpenNew.Size = new Size(92, 26);
    this.btnOpenNew.TabIndex = 20;
    this.btnOpenNew.Text = "Open...";
    this.btnOpenNew.Click += new EventHandler(this.btnOpenNew_Click);
    this.btnAddFrame.Location = new Point(312, 84);
    this.btnAddFrame.Name = "btnAddFrame";
    this.btnAddFrame.Size = new Size(92, 26);
    this.btnAddFrame.TabIndex = 21;
    this.btnAddFrame.Text = "Add Frame...";
    this.btnAddFrame.Click += new EventHandler(this.btnAddFrame_Click);
    this.lblAlpha.Location = new Point(220, 32 /*0x20*/);
    this.lblAlpha.Name = "lblAlpha";
    this.lblAlpha.Size = new Size(40, 16 /*0x10*/);
    this.lblAlpha.TabIndex = 22;
    this.lblAlpha.Text = "Alpha:";
    this.nudAlpha.Location = new Point(264, 28);
    this.nudAlpha.Maximum = new Decimal(new int[4]
    {
      4,
      0,
      0,
      0
    });
    this.nudAlpha.Name = "nudAlpha";
    this.nudAlpha.Size = new Size(36, 20);
    this.nudAlpha.TabIndex = 23;
    this.nudAlpha.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.panelTexture.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.panelTexture.BackColor = SystemColors.Control;
    this.panelTexture.Location = new Point(11, 126);
    this.panelTexture.Name = "panelTexture";
    this.panelTexture.Size = new Size(393, 201);
    this.panelTexture.TabIndex = 24;
    this.Controls.Add((System.Windows.Forms.Control) this.panelTexture);
    this.Controls.Add((System.Windows.Forms.Control) this.nudAlpha);
    this.Controls.Add((System.Windows.Forms.Control) this.lblAlpha);
    this.Controls.Add((System.Windows.Forms.Control) this.btnAddFrame);
    this.Controls.Add((System.Windows.Forms.Control) this.btnOpenNew);
    this.Controls.Add((System.Windows.Forms.Control) this.nudCurrentFrame);
    this.Controls.Add((System.Windows.Forms.Control) this.lblCurrentFrame);
    this.Controls.Add((System.Windows.Forms.Control) this.btnSave);
    this.Controls.Add((System.Windows.Forms.Control) this.lblPreview);
    this.Controls.Add((System.Windows.Forms.Control) this.txtFrameCount);
    this.Controls.Add((System.Windows.Forms.Control) this.lblFrameCount);
    this.Controls.Add((System.Windows.Forms.Control) this.txtFrameHeight);
    this.Controls.Add((System.Windows.Forms.Control) this.lblFrameHeight);
    this.Controls.Add((System.Windows.Forms.Control) this.txtFrameWidth);
    this.Controls.Add((System.Windows.Forms.Control) this.lblFrameWidth);
    this.Controls.Add((System.Windows.Forms.Control) this.txtDepth);
    this.Controls.Add((System.Windows.Forms.Control) this.lblDepth);
    this.Controls.Add((System.Windows.Forms.Control) this.txtHeight);
    this.Controls.Add((System.Windows.Forms.Control) this.lblHeight);
    this.Controls.Add((System.Windows.Forms.Control) this.txtWidth);
    this.Controls.Add((System.Windows.Forms.Control) this.lblWidth);
    this.EntryType = "Texture";
    this.Name = nameof (ControlTexture);
    this.Size = new Size(416, 330);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblWidth, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.txtWidth, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblHeight, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.txtHeight, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblDepth, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.txtDepth, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblFrameWidth, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.txtFrameWidth, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblFrameHeight, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.txtFrameHeight, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblFrameCount, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.txtFrameCount, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblPreview, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.btnSave, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblCurrentFrame, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.nudCurrentFrame, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.btnOpenNew, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.btnAddFrame, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblAlpha, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.nudAlpha, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.panelTexture, 0);
    this.nudCurrentFrame.EndInit();
    this.nudAlpha.EndInit();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void btnSave_Click(object sender, EventArgs e)
  {
    if (this.m_Texture == null || this.saveFileDialog.ShowDialog((IWin32Window) this) != DialogResult.OK)
      return;
    this.m_Texture.Save(this.saveFileDialog.FileName, (int) this.nudCurrentFrame.Value);
  }

  private void nudCurrentFrame_ValueChanged(object sender, EventArgs e) => this.Invalidate();

  private void btnOpenNew_Click(object sender, EventArgs e)
  {
    FormOpenTexture formOpenTexture = new FormOpenTexture();
    if (formOpenTexture.ShowDialog((IWin32Window) this) == DialogResult.OK)
    {
      GfxTexture gfxTexture;
      try
      {
        gfxTexture = new GfxTexture(formOpenTexture.FileName, formOpenTexture.Format);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show((IWin32Window) this, ex.Message);
        return;
      }
      this.m_Texture = gfxTexture;
      this.RefreshDisplay();
    }
    formOpenTexture.Dispose();
  }

  private void btnAddFrame_Click(object sender, EventArgs e)
  {
    this.openFileDialog.InitialDirectory = ContentManager.Instance.DataDirectory;
    if (this.openFileDialog.ShowDialog((IWin32Window) this) != DialogResult.OK)
      return;
    try
    {
      this.m_Texture.AddFrame(this.openFileDialog.FileName);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, ex.Message);
    }
    this.RefreshDisplay();
  }
}
