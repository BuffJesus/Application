// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormModPackage
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.CLRCore;
using FableMod.ContentManagement;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormModPackage : Form
{
  private IContainer components;
  private ListView listViewEntries;
  private Button buttonSaveAs;
  private Button buttonCancel;
  private SaveFileDialog saveFileDialog;
  private ColumnHeader columnHeader0;
  private ColumnHeader columnHeader1;
  private Label label1;
  private TextBox textBoxDesc;
  private Button buttonApply;
  private Panel panelButtons;
  private Panel panel1;
  private Splitter splitter;
  private Button buttonModified;
  private Button buttonMerge;
  private OpenFileDialog openFileDialogFMP;
  private ModPackage myPackage;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.listViewEntries = new ListView();
    this.columnHeader0 = new ColumnHeader();
    this.columnHeader1 = new ColumnHeader();
    this.buttonSaveAs = new Button();
    this.buttonCancel = new Button();
    this.saveFileDialog = new SaveFileDialog();
    this.label1 = new Label();
    this.textBoxDesc = new TextBox();
    this.buttonApply = new Button();
    this.panelButtons = new Panel();
    this.buttonMerge = new Button();
    this.buttonModified = new Button();
    this.panel1 = new Panel();
    this.splitter = new Splitter();
    this.openFileDialogFMP = new OpenFileDialog();
    this.panelButtons.SuspendLayout();
    this.panel1.SuspendLayout();
    this.SuspendLayout();
    this.listViewEntries.AllowDrop = true;
    this.listViewEntries.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader0,
      this.columnHeader1
    });
    this.listViewEntries.Dock = DockStyle.Fill;
    this.listViewEntries.Location = new Point(8, 107);
    this.listViewEntries.Name = "listViewEntries";
    this.listViewEntries.Size = new Size(460, 172);
    this.listViewEntries.Sorting = SortOrder.Ascending;
    this.listViewEntries.TabIndex = 0;
    this.listViewEntries.UseCompatibleStateImageBehavior = false;
    this.listViewEntries.View = View.Details;
    this.listViewEntries.DragDrop += new DragEventHandler(this.listViewEntries_DragDrop);
    this.listViewEntries.DragEnter += new DragEventHandler(this.listViewEntries_DragEnter);
    this.listViewEntries.KeyDown += new KeyEventHandler(this.listViewEntries_KeyDown);
    this.columnHeader0.Text = "Name";
    this.columnHeader0.Width = 220;
    this.columnHeader1.Text = "Type";
    this.columnHeader1.Width = 150;
    this.buttonSaveAs.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonSaveAs.Location = new Point(304, 9);
    this.buttonSaveAs.Name = "buttonSaveAs";
    this.buttonSaveAs.Size = new Size(75, 23);
    this.buttonSaveAs.TabIndex = 2;
    this.buttonSaveAs.Text = "Save As...";
    this.buttonSaveAs.UseVisualStyleBackColor = true;
    this.buttonSaveAs.Click += new EventHandler(this.buttonSaveAs_Click);
    this.buttonCancel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonCancel.Location = new Point(385, 9);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(75, 23);
    this.buttonCancel.TabIndex = 3;
    this.buttonCancel.Text = "Close";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
    this.saveFileDialog.DefaultExt = "fmp";
    this.saveFileDialog.Filter = "Fable Mod Packages (*.fmp)|*.fmp|All Files (*.*)|*.*||";
    this.label1.AutoSize = true;
    this.label1.Location = new Point(-3, 0);
    this.label1.Name = "label1";
    this.label1.Size = new Size(63 /*0x3F*/, 13);
    this.label1.TabIndex = 4;
    this.label1.Text = "Description:";
    this.textBoxDesc.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.textBoxDesc.Location = new Point(0, 16 /*0x10*/);
    this.textBoxDesc.Multiline = true;
    this.textBoxDesc.Name = "textBoxDesc";
    this.textBoxDesc.Size = new Size(460, 77);
    this.textBoxDesc.TabIndex = 5;
    this.buttonApply.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonApply.Location = new Point(41, 9);
    this.buttonApply.Name = "buttonApply";
    this.buttonApply.Size = new Size(75, 23);
    this.buttonApply.TabIndex = 6;
    this.buttonApply.Text = "Apply";
    this.buttonApply.UseVisualStyleBackColor = true;
    this.buttonApply.Visible = false;
    this.buttonApply.Click += new EventHandler(this.buttonApply_Click);
    this.panelButtons.Controls.Add((System.Windows.Forms.Control) this.buttonMerge);
    this.panelButtons.Controls.Add((System.Windows.Forms.Control) this.buttonModified);
    this.panelButtons.Controls.Add((System.Windows.Forms.Control) this.buttonCancel);
    this.panelButtons.Controls.Add((System.Windows.Forms.Control) this.buttonApply);
    this.panelButtons.Controls.Add((System.Windows.Forms.Control) this.buttonSaveAs);
    this.panelButtons.Dock = DockStyle.Bottom;
    this.panelButtons.Location = new Point(8, 279);
    this.panelButtons.Name = "panelButtons";
    this.panelButtons.Size = new Size(460, 32 /*0x20*/);
    this.panelButtons.TabIndex = 7;
    this.buttonMerge.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonMerge.Location = new Point(223, 9);
    this.buttonMerge.Name = "buttonMerge";
    this.buttonMerge.Size = new Size(75, 23);
    this.buttonMerge.TabIndex = 8;
    this.buttonMerge.Text = "Merge...";
    this.buttonMerge.UseVisualStyleBackColor = true;
    this.buttonMerge.Click += new EventHandler(this.buttonMerge_Click);
    this.buttonModified.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonModified.Location = new Point(122, 9);
    this.buttonModified.Name = "buttonModified";
    this.buttonModified.Size = new Size(95, 23);
    this.buttonModified.TabIndex = 7;
    this.buttonModified.Text = "Get Modified";
    this.buttonModified.UseVisualStyleBackColor = true;
    this.buttonModified.Click += new EventHandler(this.buttonModified_Click);
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.textBoxDesc);
    this.panel1.Controls.Add((System.Windows.Forms.Control) this.label1);
    this.panel1.Dock = DockStyle.Top;
    this.panel1.Location = new Point(8, 8);
    this.panel1.Name = "panel1";
    this.panel1.Size = new Size(460, 93);
    this.panel1.TabIndex = 8;
    this.splitter.Cursor = Cursors.HSplit;
    this.splitter.Dock = DockStyle.Top;
    this.splitter.Location = new Point(8, 101);
    this.splitter.Name = "splitter";
    this.splitter.Size = new Size(460, 6);
    this.splitter.TabIndex = 9;
    this.splitter.TabStop = false;
    this.openFileDialogFMP.DefaultExt = "fmp";
    this.openFileDialogFMP.Filter = "Fable Mod Packages (*.fmp)|*.fmp||";
    this.AllowDrop = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(476, 319);
    this.Controls.Add((System.Windows.Forms.Control) this.listViewEntries);
    this.Controls.Add((System.Windows.Forms.Control) this.splitter);
    this.Controls.Add((System.Windows.Forms.Control) this.panel1);
    this.Controls.Add((System.Windows.Forms.Control) this.panelButtons);
    this.Name = nameof (FormModPackage);
    this.Padding = new Padding(8);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Show;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Mod Package";
    this.FormClosing += new FormClosingEventHandler(this.FormModPackage_FormClosing);
    this.panelButtons.ResumeLayout(false);
    this.panel1.ResumeLayout(false);
    this.panel1.PerformLayout();
    this.ResumeLayout(false);
  }

  public FormModPackage()
  {
    this.InitializeComponent();
    ThemeManager.ApplyTheme(this);
    this.myPackage = new ModPackage();
    this.Text = "New Mod Package";
    this.buttonApply.Visible = false;
  }

  private void FillList()
  {
    this.listViewEntries.Items.Clear();
    foreach (ContentObject contentObject in this.myPackage.Items)
      this.listViewEntries.Items.Add(new ListViewItem(contentObject.Name)
      {
        SubItems = {
          contentObject.Type.ToString()
        },
        Tag = (object) contentObject
      });
  }

  public FormModPackage(string fileName)
  {
    this.InitializeComponent();
    ThemeManager.ApplyTheme(this);
    this.myPackage = new ModPackage();
    this.myPackage.Load(fileName, (ProgressInterface) null);
    this.FillList();
    this.Text = "Mod Package - " + fileName;
    this.textBoxDesc.Text = this.myPackage.Description;
    this.buttonApply.Visible = true;
  }

  private void buttonCancel_Click(object sender, EventArgs e) => this.Close();

  private void AddObject(ContentObject o)
  {
    try
    {
      if (!this.myPackage.Add(o.Type, o.Object))
      {
        int num = (int) MessageBox.Show((IWin32Window) this, $"Object {o.Name} not supported.");
      }
      else
        this.FillList();
    }
    catch (Exception ex)
    {
    }
  }

  private void listViewEntries_DragDrop(object sender, DragEventArgs e)
  {
    this.AddObject((ContentObject) e.Data.GetData(typeof (ContentObject)));
  }

  private void listViewEntries_DragEnter(object sender, DragEventArgs e)
  {
    e.Effect = DragDropEffects.None;
    if (e.Data.GetDataPresent(typeof (ContentObject)) && ((ContentObject) e.Data.GetData(typeof (ContentObject))).Type != ContentType.Unknown)
      e.Effect = DragDropEffects.Copy;
    else
      e.Effect = DragDropEffects.None;
  }

  private void buttonSaveAs_Click(object sender, EventArgs e)
  {
    if (this.saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    this.myPackage.Description = this.textBoxDesc.Text;
    try
    {
      this.myPackage.Save(this.saveFileDialog.FileName, (ProgressInterface) null);
    }
    catch (Exception ex)
    {
      int num = (int) FormMain.Instance.ErrorMessage(ex.Message);
    }
  }

  private void buttonApply_Click(object sender, EventArgs e)
  {
    try
    {
      if (MessageBox.Show("Other editor forms need to be closed before the mod can be applied.\r\nAre you sure you want to continue?", FormMain.Instance.Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        return;
      FormMain.Instance.CloseChildren(typeof (FormModPackage));
      this.myPackage.Apply();
      int num = (int) FormMain.Instance.InfoMessage("Package applied successfully.");
    }
    catch (Exception ex)
    {
      int num = (int) FormMain.Instance.ErrorMessage(ex.Message);
    }
  }

  private void buttonModified_Click(object sender, EventArgs e)
  {
    GetModifiedProcessor modifiedProcessor = new GetModifiedProcessor();
    FormProcess formProcess = new FormProcess((Processor) modifiedProcessor);
    int num = (int) formProcess.ShowDialog();
    formProcess.Dispose();
    foreach (ContentObject o in modifiedProcessor.Objects)
      this.AddObject(o);
  }

  private void listViewEntries_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Delete)
      return;
    foreach (ListViewItem selectedItem in this.listViewEntries.SelectedItems)
    {
      ContentObject tag = (ContentObject) selectedItem.Tag;
      if (!this.myPackage.Remove(tag.Type, tag.Name))
      {
        int num = (int) MessageBox.Show($"Failed to remove {tag.Name}.");
      }
    }
    this.FillList();
  }

  private void buttonMerge_Click(object sender, EventArgs e)
  {
    if (this.openFileDialogFMP.ShowDialog() != DialogResult.OK)
      return;
    ModPackage package = new ModPackage();
    package.Load(this.openFileDialogFMP.FileName, (ProgressInterface) null);
    this.myPackage.Merge(package);
    this.FillList();
    package.Dispose();
    this.textBoxDesc.Text = this.myPackage.Description;
  }

  private void FormModPackage_FormClosing(object sender, FormClosingEventArgs e)
  {
    this.myPackage.Dispose();
  }
}
