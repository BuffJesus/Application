// Decompiled with JetBrains decompiler
// Type: FableMod.Content.Forms.FormBINEntry
// Assembly: FableMod.Content.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A575F982-23D4-4DAE-A571-3D769462C59B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Content.Forms.dll

using FableMod.BIN;
using FableMod.ContentManagement;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Content.Forms;

public class FormBINEntry : Form
{
  private IContainer components;
  private Label lblSymbolName;
  private Label lblDefinitionType;
  private TextBox txtSymbolName;
  private TextBox txtDefinitionType;
  private Button btnExport;
  private Button btnViewData;
  private Button btnImport;
  private Button btnApplyChanges;
  private SplitContainer spltContainerMain;
  private SaveFileDialog saveFileDialog;
  private OpenFileDialog openFileDialog;
  private TextBox textBoxId;
  private Label label1;
  private BINEntry m_Entry;
  private DefinitionType m_DefType;
  private DefinitionTypeDisplay m_DefDisplay;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.lblSymbolName = new Label();
    this.lblDefinitionType = new Label();
    this.txtSymbolName = new TextBox();
    this.txtDefinitionType = new TextBox();
    this.btnExport = new Button();
    this.btnViewData = new Button();
    this.btnImport = new Button();
    this.btnApplyChanges = new Button();
    this.spltContainerMain = new SplitContainer();
    this.textBoxId = new TextBox();
    this.label1 = new Label();
    this.saveFileDialog = new SaveFileDialog();
    this.openFileDialog = new OpenFileDialog();
    this.spltContainerMain.Panel1.SuspendLayout();
    this.spltContainerMain.SuspendLayout();
    this.SuspendLayout();
    this.lblSymbolName.AutoSize = true;
    this.lblSymbolName.Location = new Point(9, 12);
    this.lblSymbolName.Name = "lblSymbolName";
    this.lblSymbolName.Size = new Size(75, 13);
    this.lblSymbolName.TabIndex = 0;
    this.lblSymbolName.Text = "Symbol Name:";
    this.lblDefinitionType.AutoSize = true;
    this.lblDefinitionType.Location = new Point(9, 36);
    this.lblDefinitionType.Name = "lblDefinitionType";
    this.lblDefinitionType.Size = new Size(81, 13);
    this.lblDefinitionType.TabIndex = 1;
    this.lblDefinitionType.Text = "Definition Type:";
    this.txtSymbolName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtSymbolName.Location = new Point(97, 9);
    this.txtSymbolName.Name = "txtSymbolName";
    this.txtSymbolName.Size = new Size(300, 20);
    this.txtSymbolName.TabIndex = 2;
    this.txtDefinitionType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtDefinitionType.Location = new Point(97, 33);
    this.txtDefinitionType.Name = "txtDefinitionType";
    this.txtDefinitionType.Size = new Size(300, 20);
    this.txtDefinitionType.TabIndex = 3;
    this.btnExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnExport.Location = new Point(109, 85);
    this.btnExport.Name = "btnExport";
    this.btnExport.Size = new Size(92, 24);
    this.btnExport.TabIndex = 4;
    this.btnExport.Text = "Export";
    this.btnExport.UseVisualStyleBackColor = true;
    this.btnExport.Click += new EventHandler(this.btnExport_Click);
    this.btnViewData.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnViewData.Location = new Point(11, 85);
    this.btnViewData.Name = "btnViewData";
    this.btnViewData.Size = new Size(92, 24);
    this.btnViewData.TabIndex = 5;
    this.btnViewData.Text = "View Data";
    this.btnViewData.UseVisualStyleBackColor = true;
    this.btnViewData.Click += new EventHandler(this.btnViewData_Click);
    this.btnImport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnImport.Location = new Point(207, 85);
    this.btnImport.Name = "btnImport";
    this.btnImport.Size = new Size(92, 24);
    this.btnImport.TabIndex = 6;
    this.btnImport.Text = "Import";
    this.btnImport.UseVisualStyleBackColor = true;
    this.btnImport.Click += new EventHandler(this.btnImport_Click);
    this.btnApplyChanges.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnApplyChanges.Location = new Point(305, 85);
    this.btnApplyChanges.Name = "btnApplyChanges";
    this.btnApplyChanges.Size = new Size(92, 24);
    this.btnApplyChanges.TabIndex = 7;
    this.btnApplyChanges.Text = "Apply Changes";
    this.btnApplyChanges.UseVisualStyleBackColor = true;
    this.btnApplyChanges.Click += new EventHandler(this.btnApplyChanges_Click);
    this.spltContainerMain.Dock = DockStyle.Fill;
    this.spltContainerMain.FixedPanel = FixedPanel.Panel1;
    this.spltContainerMain.IsSplitterFixed = true;
    this.spltContainerMain.Location = new Point(0, 0);
    this.spltContainerMain.Name = "spltContainerMain";
    this.spltContainerMain.Orientation = Orientation.Horizontal;
    this.spltContainerMain.Panel1.Controls.Add((System.Windows.Forms.Control) this.textBoxId);
    this.spltContainerMain.Panel1.Controls.Add((System.Windows.Forms.Control) this.label1);
    this.spltContainerMain.Panel1.Controls.Add((System.Windows.Forms.Control) this.txtSymbolName);
    this.spltContainerMain.Panel1.Controls.Add((System.Windows.Forms.Control) this.btnApplyChanges);
    this.spltContainerMain.Panel1.Controls.Add((System.Windows.Forms.Control) this.lblSymbolName);
    this.spltContainerMain.Panel1.Controls.Add((System.Windows.Forms.Control) this.btnImport);
    this.spltContainerMain.Panel1.Controls.Add((System.Windows.Forms.Control) this.lblDefinitionType);
    this.spltContainerMain.Panel1.Controls.Add((System.Windows.Forms.Control) this.btnViewData);
    this.spltContainerMain.Panel1.Controls.Add((System.Windows.Forms.Control) this.txtDefinitionType);
    this.spltContainerMain.Panel1.Controls.Add((System.Windows.Forms.Control) this.btnExport);
    this.spltContainerMain.Panel1MinSize = 85;
    this.spltContainerMain.Size = new Size(409, 174);
    this.spltContainerMain.SplitterDistance = 120;
    this.spltContainerMain.TabIndex = 8;
    this.textBoxId.Location = new Point(97, 59);
    this.textBoxId.Name = "textBoxId";
    this.textBoxId.ReadOnly = true;
    this.textBoxId.Size = new Size(68, 20);
    this.textBoxId.TabIndex = 9;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(9, 62);
    this.label1.Name = "label1";
    this.label1.Size = new Size(21, 13);
    this.label1.TabIndex = 8;
    this.label1.Text = "ID:";
    this.saveFileDialog.DefaultExt = "fbf";
    this.saveFileDialog.Filter = "Fable BIN Files (*.fbf)|*.fbf|All Files (*.*)|*.*||";
    this.openFileDialog.DefaultExt = "fbf";
    this.openFileDialog.Filter = "Fable BIN Files (*.fbf)|*.fbf|All Files (*.*)|*.*||";
    this.openFileDialog.FileOk += new CancelEventHandler(this.openFileDialog_FileOk);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoScroll = true;
    this.CausesValidation = false;
    this.ClientSize = new Size(409, 174);
    this.Controls.Add((System.Windows.Forms.Control) this.spltContainerMain);
    this.MinimumSize = new Size(417, 34);
    this.Name = nameof (FormBINEntry);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "BIN Entry";
    this.spltContainerMain.Panel1.ResumeLayout(false);
    this.spltContainerMain.Panel1.PerformLayout();
    this.spltContainerMain.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public FormBINEntry(BINEntry entry)
  {
    this.InitializeComponent();
    this.m_Entry = entry;
    this.Text = this.m_Entry.Name;
    this.txtDefinitionType.Text = this.m_Entry.Definition;
    this.txtSymbolName.Text = this.m_Entry.Name;
    this.textBoxId.Text = this.m_Entry.ID.ToString();
    this.RefreshDefinitionDisplay();
  }

  public void RefreshDefinitionDisplay()
  {
    this.m_DefType = ContentManager.Instance.Definitions.GetDefinition(this.m_Entry.Definition);
    if (this.m_DefType == null)
      return;
    try
    {
      if (this.m_DefDisplay != null)
      {
        this.spltContainerMain.Panel2.Controls.Remove((System.Windows.Forms.Control) this.m_DefDisplay);
        this.spltContainerMain.Panel2Collapsed = true;
        this.m_DefDisplay.Dispose();
      }
      this.m_DefType.ReadIn(this.m_Entry);
      this.m_DefDisplay = new DefinitionTypeDisplay(this.m_DefType, ContentManager.Instance);
      this.Size = new Size(this.Size.Width, 480);
      this.spltContainerMain.Panel2.Controls.Add((System.Windows.Forms.Control) this.m_DefDisplay);
      this.spltContainerMain.Panel2Collapsed = false;
      this.m_DefDisplay.Dock = DockStyle.Fill;
    }
    catch (Exception ex)
    {
      this.Size = new Size(this.Size.Width, 120);
      int num = (int) MessageBox.Show((IWin32Window) this, ex.Message, "Error Parsing Entry");
    }
  }

  private void btnExport_Click(object sender, EventArgs e)
  {
    this.saveFileDialog.InitialDirectory = ContentManager.Instance.DataDirectory;
    if (this.saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    FileStream fileStream = File.Create(this.saveFileDialog.FileName);
    byte[] data = this.m_Entry.Data;
    fileStream.Write(data, 0, data.Length);
    fileStream.Close();
  }

  private void btnViewData_Click(object sender, EventArgs e)
  {
    FormDataViewer formDataViewer = new FormDataViewer(this.m_Entry.Data);
    formDataViewer.MdiParent = this.MdiParent;
    formDataViewer.Show();
  }

  private void btnImport_Click(object sender, EventArgs e)
  {
    this.openFileDialog.InitialDirectory = ContentManager.Instance.DataDirectory;
    if (this.openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    FileStream fileStream = File.OpenRead(this.openFileDialog.FileName);
    byte[] buffer = new byte[fileStream.Length];
    fileStream.Read(buffer, 0, (int) fileStream.Length);
    this.m_Entry.Data = buffer;
    this.RefreshDefinitionDisplay();
  }

  private void btnApplyChanges_Click(object sender, EventArgs e)
  {
    this.m_Entry.Definition = this.txtDefinitionType.Text;
    this.m_Entry.Name = this.txtSymbolName.Text;
    if (this.m_DefDisplay == null)
      return;
    this.m_DefDisplay.ApplyChanges();
    this.m_DefType.Write(this.m_Entry);
  }

  private void openFileDialog_FileOk(object sender, CancelEventArgs e)
  {
  }
}
