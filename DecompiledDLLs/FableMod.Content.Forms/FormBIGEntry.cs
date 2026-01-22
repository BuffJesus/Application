// Decompiled with JetBrains decompiler
// Type: FableMod.Content.Forms.FormBIGEntry
// Assembly: FableMod.Content.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A575F982-23D4-4DAE-A571-3D769462C59B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Content.Forms.dll

using FableMod.BIG;
using FableMod.ContentManagement;
using FableMod.Data;
using FableMod.Forms;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Content.Forms;

public class FormBIGEntry : Form
{
  private Panel pnlStandardView;
  private Label lblSymbolName;
  private TextBox txtSymbolName;
  private Label lblID;
  private TextBox txtID;
  private Label lblType;
  private TextBox txtType;
  private Label lblDevSources;
  private TextBox txtDevSources;
  private AssetEntry m_Entry;
  private Button btnViewSubHeader;
  private Button btnExport;
  private ControlBIGEntry becAdvanced;
  private Button btnApply;
  private Button btnViewData;
  private Button btnImport;
  private GroupBox gbEntryData;
  private GroupBox gbSubHeader;
  private Button btnImportSubHeader;
  private Button btnExportSubHeader;
  private Splitter splitter;
  private System.ComponentModel.Container components;

  public FormBIGEntry(AssetEntry entry)
  {
    this.InitializeComponent();
    this.m_Entry = entry;
    this.Text = this.m_Entry.DevSymbolName;
    this.txtSymbolName.Text = this.m_Entry.DevSymbolName;
    this.txtID.Text = this.m_Entry.ID.ToString();
    this.txtType.Text = this.m_Entry.Type.ToString();
    this.txtDevSources.Lines = this.m_Entry.DevSources;
    try
    {
      AssetArchive archive = this.m_Entry.Archive;
      if (archive.GetType() == typeof (BIGFile))
      {
        uint type = this.m_Entry.Type;
        int contentType = archive.ContentType;
        if ((contentType == 71 || contentType == 43 || contentType == 75) && (type == 0U || type == 1U || type == 2U))
          this.becAdvanced = (ControlBIGEntry) new ControlTexture();
        else if (contentType == 73 && (type == 1U || type == 2U || type == 4U || type == 5U))
          this.becAdvanced = (ControlBIGEntry) new ControlModel();
        else if (contentType == 73 && type == 3U)
          this.becAdvanced = (ControlBIGEntry) new ControlPhysicsModel();
        else if (contentType == 42 && type == 0U)
          this.becAdvanced = (ControlBIGEntry) new ControlText();
        else if (contentType == 42 && type == 1U)
          this.becAdvanced = (ControlBIGEntry) new ControlTextGroup();
        if (this.becAdvanced == null)
          throw new Exception($"Unknown content type: {archive.OriginalFileName}:{contentType:X}:{type:X}");
        this.becAdvanced.BIGEntry = this.m_Entry;
        if (this.ClientSize.Width < this.becAdvanced.Width)
          this.ClientSize = new Size(this.becAdvanced.Width, this.pnlStandardView.Height + this.becAdvanced.Height);
        else
          this.ClientSize = new Size(this.ClientSize.Width, this.pnlStandardView.Height + this.becAdvanced.Height);
        this.becAdvanced.Dock = DockStyle.Fill;
        this.Controls.Add((System.Windows.Forms.Control) this.becAdvanced);
        this.becAdvanced.BringToFront();
      }
    }
    catch (Exception ex)
    {
      this.becAdvanced = (ControlBIGEntry) null;
      Messages.Error(ex);
      this.ClientSize = this.pnlStandardView.Size;
    }
    this.Invalidate(true);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.pnlStandardView = new Panel();
    this.gbEntryData = new GroupBox();
    this.btnExport = new Button();
    this.btnViewData = new Button();
    this.btnImport = new Button();
    this.gbSubHeader = new GroupBox();
    this.btnImportSubHeader = new Button();
    this.btnExportSubHeader = new Button();
    this.btnViewSubHeader = new Button();
    this.btnApply = new Button();
    this.txtDevSources = new TextBox();
    this.lblDevSources = new Label();
    this.txtType = new TextBox();
    this.lblType = new Label();
    this.txtID = new TextBox();
    this.lblID = new Label();
    this.txtSymbolName = new TextBox();
    this.lblSymbolName = new Label();
    this.splitter = new Splitter();
    this.pnlStandardView.SuspendLayout();
    this.gbEntryData.SuspendLayout();
    this.gbSubHeader.SuspendLayout();
    this.SuspendLayout();
    this.pnlStandardView.Controls.Add((System.Windows.Forms.Control) this.gbEntryData);
    this.pnlStandardView.Controls.Add((System.Windows.Forms.Control) this.gbSubHeader);
    this.pnlStandardView.Controls.Add((System.Windows.Forms.Control) this.btnApply);
    this.pnlStandardView.Controls.Add((System.Windows.Forms.Control) this.txtDevSources);
    this.pnlStandardView.Controls.Add((System.Windows.Forms.Control) this.lblDevSources);
    this.pnlStandardView.Controls.Add((System.Windows.Forms.Control) this.txtType);
    this.pnlStandardView.Controls.Add((System.Windows.Forms.Control) this.lblType);
    this.pnlStandardView.Controls.Add((System.Windows.Forms.Control) this.txtID);
    this.pnlStandardView.Controls.Add((System.Windows.Forms.Control) this.lblID);
    this.pnlStandardView.Controls.Add((System.Windows.Forms.Control) this.txtSymbolName);
    this.pnlStandardView.Controls.Add((System.Windows.Forms.Control) this.lblSymbolName);
    this.pnlStandardView.Dock = DockStyle.Top;
    this.pnlStandardView.Location = new Point(0, 0);
    this.pnlStandardView.Name = "pnlStandardView";
    this.pnlStandardView.Size = new Size(350, 219);
    this.pnlStandardView.TabIndex = 0;
    this.gbEntryData.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbEntryData.Controls.Add((System.Windows.Forms.Control) this.btnExport);
    this.gbEntryData.Controls.Add((System.Windows.Forms.Control) this.btnViewData);
    this.gbEntryData.Controls.Add((System.Windows.Forms.Control) this.btnImport);
    this.gbEntryData.Location = new Point(12, 138);
    this.gbEntryData.Name = "gbEntryData";
    this.gbEntryData.Size = new Size(326, 44);
    this.gbEntryData.TabIndex = 15;
    this.gbEntryData.TabStop = false;
    this.gbEntryData.Text = "Entry Data";
    this.btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnExport.Location = new Point(114, 14);
    this.btnExport.Name = "btnExport";
    this.btnExport.Size = new Size(100, 24);
    this.btnExport.TabIndex = 9;
    this.btnExport.Text = "Export";
    this.btnExport.Click += new EventHandler(this.btnExport_Click);
    this.btnViewData.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnViewData.Location = new Point(8, 14);
    this.btnViewData.Name = "btnViewData";
    this.btnViewData.Size = new Size(100, 24);
    this.btnViewData.TabIndex = 11;
    this.btnViewData.Text = "View Data";
    this.btnViewData.Click += new EventHandler(this.btnViewData_Click);
    this.btnImport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnImport.Location = new Point(220, 14);
    this.btnImport.Name = "btnImport";
    this.btnImport.Size = new Size(100, 24);
    this.btnImport.TabIndex = 12;
    this.btnImport.Text = "Import";
    this.btnImport.Click += new EventHandler(this.btnImport_Click);
    this.gbSubHeader.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.gbSubHeader.Controls.Add((System.Windows.Forms.Control) this.btnImportSubHeader);
    this.gbSubHeader.Controls.Add((System.Windows.Forms.Control) this.btnExportSubHeader);
    this.gbSubHeader.Controls.Add((System.Windows.Forms.Control) this.btnViewSubHeader);
    this.gbSubHeader.Location = new Point(12, 88);
    this.gbSubHeader.Name = "gbSubHeader";
    this.gbSubHeader.Size = new Size(326, 44);
    this.gbSubHeader.TabIndex = 13;
    this.gbSubHeader.TabStop = false;
    this.gbSubHeader.Text = "Sub Header";
    this.btnImportSubHeader.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnImportSubHeader.Location = new Point(220, 14);
    this.btnImportSubHeader.Name = "btnImportSubHeader";
    this.btnImportSubHeader.Size = new Size(100, 24);
    this.btnImportSubHeader.TabIndex = 14;
    this.btnImportSubHeader.Text = "Import";
    this.btnImportSubHeader.Click += new EventHandler(this.btnImportSubHeader_Click);
    this.btnExportSubHeader.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnExportSubHeader.Location = new Point(114, 14);
    this.btnExportSubHeader.Name = "btnExportSubHeader";
    this.btnExportSubHeader.Size = new Size(100, 24);
    this.btnExportSubHeader.TabIndex = 14;
    this.btnExportSubHeader.Text = "Export";
    this.btnExportSubHeader.Click += new EventHandler(this.btnExportSubHeader_Click);
    this.btnViewSubHeader.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnViewSubHeader.Location = new Point(8, 14);
    this.btnViewSubHeader.Name = "btnViewSubHeader";
    this.btnViewSubHeader.Size = new Size(100, 24);
    this.btnViewSubHeader.TabIndex = 8;
    this.btnViewSubHeader.Text = "View Sub Header";
    this.btnViewSubHeader.Click += new EventHandler(this.btnViewSubHeader_Click);
    this.btnApply.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnApply.Location = new Point(232, 188);
    this.btnApply.Name = "btnApply";
    this.btnApply.Size = new Size(100, 24);
    this.btnApply.TabIndex = 10;
    this.btnApply.Text = "Apply Changes";
    this.btnApply.Click += new EventHandler(this.btnApply_Click);
    this.txtDevSources.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtDevSources.Location = new Point(226, 36);
    this.txtDevSources.Multiline = true;
    this.txtDevSources.Name = "txtDevSources";
    this.txtDevSources.Size = new Size(112 /*0x70*/, 46);
    this.txtDevSources.TabIndex = 7;
    this.txtDevSources.WordWrap = false;
    this.lblDevSources.AutoSize = true;
    this.lblDevSources.Location = new Point(148, 36);
    this.lblDevSources.Name = "lblDevSources";
    this.lblDevSources.Size = new Size(72, 13);
    this.lblDevSources.TabIndex = 6;
    this.lblDevSources.Text = "Dev Sources:";
    this.txtType.Location = new Point(90, 62);
    this.txtType.Name = "txtType";
    this.txtType.Size = new Size(52, 20);
    this.txtType.TabIndex = 5;
    this.lblType.AutoSize = true;
    this.lblType.Location = new Point(50, 65);
    this.lblType.Name = "lblType";
    this.lblType.Size = new Size(34, 13);
    this.lblType.TabIndex = 4;
    this.lblType.Text = "Type:";
    this.txtID.Location = new Point(90, 36);
    this.txtID.Name = "txtID";
    this.txtID.Size = new Size(52, 20);
    this.txtID.TabIndex = 3;
    this.lblID.AutoSize = true;
    this.lblID.Location = new Point(63 /*0x3F*/, 38);
    this.lblID.Name = "lblID";
    this.lblID.Size = new Size(21, 13);
    this.lblID.TabIndex = 2;
    this.lblID.Text = "ID:";
    this.txtSymbolName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtSymbolName.Location = new Point(90, 8);
    this.txtSymbolName.Name = "txtSymbolName";
    this.txtSymbolName.Size = new Size(248, 20);
    this.txtSymbolName.TabIndex = 1;
    this.lblSymbolName.AutoSize = true;
    this.lblSymbolName.Location = new Point(9, 11);
    this.lblSymbolName.Name = "lblSymbolName";
    this.lblSymbolName.Size = new Size(75, 13);
    this.lblSymbolName.TabIndex = 0;
    this.lblSymbolName.Text = "Symbol Name:";
    this.splitter.BorderStyle = BorderStyle.Fixed3D;
    this.splitter.Dock = DockStyle.Top;
    this.splitter.Location = new Point(0, 219);
    this.splitter.Name = "splitter";
    this.splitter.Size = new Size(350, 8);
    this.splitter.TabIndex = 1;
    this.splitter.TabStop = false;
    this.AutoScaleBaseSize = new Size(5, 13);
    this.ClientSize = new Size(350, 280);
    this.Controls.Add((System.Windows.Forms.Control) this.splitter);
    this.Controls.Add((System.Windows.Forms.Control) this.pnlStandardView);
    this.MinimumSize = new Size(358, 0);
    this.Name = nameof (FormBIGEntry);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "BIG Entry";
    this.Deactivate += new EventHandler(this.FormBIGEntry_Deactivate);
    this.Activated += new EventHandler(this.FormBIGEntry_Activated);
    this.pnlStandardView.ResumeLayout(false);
    this.pnlStandardView.PerformLayout();
    this.gbEntryData.ResumeLayout(false);
    this.gbSubHeader.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private void btnViewSubHeader_Click(object sender, EventArgs e)
  {
    uint type = this.m_Entry.Type;
    if (this.m_Entry.Archive.ContentType == 73 && type == 3U)
      return;
    FormDataViewer formDataViewer = new FormDataViewer(this.m_Entry.SubHeader);
    formDataViewer.MdiParent = this.MdiParent;
    formDataViewer.Show();
  }

  private void btnExport_Click(object sender, EventArgs e)
  {
    SaveFileDialog saveFileDialog = new SaveFileDialog();
    if (saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    FileStream fileStream = File.Create(saveFileDialog.FileName);
    byte[] data = this.m_Entry.Data;
    if (data[data.Length - 3] == (byte) 17 && data[data.Length - 2] == (byte) 0)
    {
      if (data[data.Length - 1] == (byte) 0)
      {
        try
        {
          byte[] buffer = LZO.DecompressRaw(data, 4, data.Length - 4);
          if (MessageBox.Show((IWin32Window) this, "This file seems to be entirely lzo compressed.Would you like to decompress it before saving to file?", "LZO Compression Found", MessageBoxButtons.YesNo) == DialogResult.Yes)
          {
            fileStream.Write(buffer, 0, buffer.Length);
            fileStream.Close();
            return;
          }
        }
        catch (Exception ex)
        {
        }
      }
    }
    fileStream.Write(data, 0, data.Length);
    fileStream.Close();
  }

  private void btnApply_Click(object sender, EventArgs e)
  {
    this.m_Entry.DevSymbolName = this.txtSymbolName.Text;
    this.m_Entry.DevSources = this.txtDevSources.Lines;
    try
    {
      this.m_Entry.Type = uint.Parse(this.txtType.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Type entered was not a valid type number.", "Invalid Type");
    }
    try
    {
      this.m_Entry.ID = uint.Parse(this.txtID.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "ID entered was not a valid ID number.", "Invalid ID");
    }
    if (this.becAdvanced == null)
      return;
    this.becAdvanced.ApplyChanges();
  }

  private void btnViewData_Click(object sender, EventArgs e)
  {
    FormDataViewer formDataViewer = new FormDataViewer(this.m_Entry.Data);
    formDataViewer.MdiParent = this.MdiParent;
    formDataViewer.Show();
  }

  private void btnImport_Click(object sender, EventArgs e)
  {
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.InitialDirectory = ContentManager.Instance.DataDirectory;
    if (openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    FileStream fileStream = File.OpenRead(openFileDialog.FileName);
    byte[] buffer = new byte[fileStream.Length];
    fileStream.Read(buffer, 0, (int) fileStream.Length);
    this.m_Entry.Data = buffer;
  }

  private void btnExportSubHeader_Click(object sender, EventArgs e)
  {
    SaveFileDialog saveFileDialog = new SaveFileDialog();
    saveFileDialog.InitialDirectory = ContentManager.Instance.DataDirectory;
    if (saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    FileStream fileStream = File.Create(saveFileDialog.FileName);
    fileStream.Write(this.m_Entry.SubHeader, 0, this.m_Entry.SubHeader.Length);
    fileStream.Close();
  }

  private void btnImportSubHeader_Click(object sender, EventArgs e)
  {
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.InitialDirectory = ContentManager.Instance.DataDirectory;
    if (openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    FileStream fileStream = File.OpenRead(openFileDialog.FileName);
    byte[] buffer = new byte[fileStream.Length];
    fileStream.Read(buffer, 0, (int) fileStream.Length);
    this.m_Entry.SubHeader = buffer;
  }

  private void FormBIGEntry_Deactivate(object sender, EventArgs e)
  {
    if (this.becAdvanced == null)
      return;
    this.becAdvanced.OnDeactivate();
  }

  private void FormBIGEntry_Activated(object sender, EventArgs e)
  {
    if (this.becAdvanced == null)
      return;
    this.becAdvanced.OnActivate();
  }
}
