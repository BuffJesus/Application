// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormBIN
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIN;
using FableMod.Content.Forms;
using FableMod.ContentManagement;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormBIN : FormTreeFileController
{
  private BINFile myBIN;
  private IContainer components;
  private MenuStrip menuStripMain;
  private ToolStripMenuItem toolStripMenuItem1;
  private ToolStripMenuItem saveAsToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem2;
  private ToolStripMenuItem closeToolStripMenuItem;
  private SaveFileDialog saveFileDialog;
  private ToolStripMenuItem editToolStripMenuItem;
  private ToolStripMenuItem findByNameToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem3;
  private ToolStripMenuItem addEntryToolStripMenuItem;
  private ToolStripMenuItem findModifiedToolStripMenuItem;
  private ToolStripMenuItem findByIDToolStripMenuItem;
  private ToolStripMenuItem dumpToFolderToolStripMenuItem;
  private FolderBrowserDialog folderBrowserDialog;

  public FormBIN()
  {
    this.InitializeComponent();
    ThemeManager.ApplyTheme(this);
    this.treeView.ImageList.Images.Add(Image.FromFile(Settings.DataDirectory + "icons\\" + "definitiontype.bmp"));
    this.dumpToFolderToolStripMenuItem.Visible = Settings.GetBool("BIN", "Dump", false);
  }

  protected override int GetObjectID(object o) => ((BINEntry) o).ID;

  protected override bool ObjectIsModified(object o)
  {
    return o.GetType() == typeof (BINEntry) && ((BINEntry) o).Modified;
  }

  protected override void ShowObject(object o)
  {
    BINEntry binEntry = (BINEntry) o;
    FormMain.Instance.AddMDI(!(binEntry.Definition == "CCutsceneDef") ? (Form) new FormBINEntry(binEntry) : (Form) new FormCutscene(binEntry));
  }

  protected override bool DeleteObject(object o) => this.myBIN.RemoveEntry((BINEntry) o);

  protected TreeNode AddEntry(BINEntry entry)
  {
    TreeNode treeNode = (TreeNode) null;
    for (int index = 0; index < this.treeView.Nodes.Count; ++index)
    {
      if (this.treeView.Nodes[index].Text == entry.Definition)
      {
        treeNode = this.treeView.Nodes[index];
        break;
      }
    }
    if (treeNode == null)
    {
      treeNode = new TreeNode();
      treeNode.Text = entry.Definition;
      treeNode.ImageIndex = 2;
      treeNode.SelectedImageIndex = 2;
      this.AddNode(null, treeNode);
    }
    return this.AddToTree(treeNode, entry.Name, (object) entry);
  }

  public void Build(BINFile bin, Progress progress)
  {
    if (this.treeView.InvokeRequired)
    {
        this.treeView.Invoke(new Action(() => this.Build(bin, progress)));
        return;
    }

    this.myBIN = bin;
    progress.Begin(bin.EntryCount);
    this.treeView.BeginUpdate();
    this.ClearNodeCache();
    try
    {
        for (int index = 0; index < bin.EntryCount; ++index)
        {
          this.AddEntry(bin.get_Entries(index));
          progress.Update();
        }
    }
    finally
    {
        this.treeView.EndUpdate();
    }
    progress.End();
  }

  private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.saveFileDialog.InitialDirectory = this.Controller.Directory;
    this.saveFileDialog.FileName = this.myBIN.OriginalFileName;
    if (this.saveFileDialog.ShowDialog((IWin32Window) this) != DialogResult.OK)
      return;
    FormProcess formProcess = new FormProcess((Processor) new FileProcessor(this.Controller, this.saveFileDialog.FileName));
    int num = (int) formProcess.ShowDialog();
    formProcess.Dispose();
  }

  private void closeToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

  private void findToolStripMenuItem_Click(object sender, EventArgs e) => this.FindByName();

  private void addEntryToolStripMenuItem_Click(object sender, EventArgs e)
  {
    FormNewGameBINEntry formNewGameBinEntry1 = new FormNewGameBINEntry(this.myBIN);
    FormNewGameBINEntry formNewGameBinEntry2;
    if (formNewGameBinEntry1.ShowDialog((IWin32Window) this) == DialogResult.OK)
    {
      byte[] numArray = (byte[]) null;
      BINEntry entryByName = this.myBIN.GetEntryByName(formNewGameBinEntry1.Template);
      if (entryByName == null)
      {
        formNewGameBinEntry1.Dispose();
        formNewGameBinEntry2 = (FormNewGameBINEntry) null;
        int num = (int) MessageBox.Show((IWin32Window) this, "Default entry not found.");
        return;
      }
      byte[] data = entryByName.Data;
      BINEntry entry = this.myBIN.AddEntry(formNewGameBinEntry1.SymbolName, formNewGameBinEntry1.Definition, data);
      DefinitionType definition = ContentManager.Instance.Definitions.GetDefinition(formNewGameBinEntry1.Definition);
      definition.ReadIn(entry);
      definition.FixLinks(LinkDestination.GameBINEntryID, (object) entryByName.ID, (object) entry.ID);
      definition.Write(entry);
      this.treeView.SelectedNode = this.AddEntry(entry);
      numArray = (byte[]) null;
    }
    formNewGameBinEntry1.Dispose();
    formNewGameBinEntry2 = (FormNewGameBINEntry) null;
  }

  private void editToolStripMenuItem_Paint(object sender, PaintEventArgs e)
  {
  }

  private void findModifiedToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.FindModified();
  }

  private void findByIDToolStripMenuItem_Click(object sender, EventArgs e) => this.FindByID();

  private void dumpToFolderToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (this.folderBrowserDialog.ShowDialog() != DialogResult.OK)
      return;
    FormProcess formProcess = new FormProcess((Processor) new BINDumber(this.myBIN, this.folderBrowserDialog.SelectedPath));
    formProcess.Text = $"Dumping BIN: {this.myBIN.OriginalFileName}...";
    int num = (int) formProcess.ShowDialog();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.menuStripMain = new MenuStrip();
    this.toolStripMenuItem1 = new ToolStripMenuItem();
    this.saveAsToolStripMenuItem = new ToolStripMenuItem();
    this.dumpToFolderToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem2 = new ToolStripSeparator();
    this.closeToolStripMenuItem = new ToolStripMenuItem();
    this.editToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem3 = new ToolStripSeparator();
    this.addEntryToolStripMenuItem = new ToolStripMenuItem();
    this.saveFileDialog = new SaveFileDialog();
    this.folderBrowserDialog = new FolderBrowserDialog();
    this.menuStripMain.SuspendLayout();
    this.SuspendLayout();
    this.treeView.LineColor = Color.Black;
    this.treeView.Location = new Point(0, 24);
    this.treeView.Size = new Size(387, 386);
    this.menuStripMain.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.toolStripMenuItem1,
      (ToolStripItem) this.editToolStripMenuItem
    });
    this.menuStripMain.Location = new Point(0, 0);
    this.menuStripMain.Name = "menuStripMain";
    this.menuStripMain.RenderMode = ToolStripRenderMode.System;
    this.menuStripMain.Size = new Size(387, 24);
    this.menuStripMain.TabIndex = 1;
    this.menuStripMain.Text = "menuStrip1";
    this.toolStripMenuItem1.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.saveAsToolStripMenuItem,
      (ToolStripItem) this.dumpToFolderToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem2,
      (ToolStripItem) this.closeToolStripMenuItem
    });
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    this.toolStripMenuItem1.Size = new Size(35, 20);
    this.toolStripMenuItem1.Text = "&File";
    this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
    this.saveAsToolStripMenuItem.Size = new Size(152, 22);
    this.saveAsToolStripMenuItem.Text = "Save &As...";
    this.saveAsToolStripMenuItem.Click += new EventHandler(this.saveAsToolStripMenuItem_Click);
    this.dumpToFolderToolStripMenuItem.Name = "dumpToFolderToolStripMenuItem";
    this.dumpToFolderToolStripMenuItem.Size = new Size(152, 22);
    this.dumpToFolderToolStripMenuItem.Text = "Dump Entries...";
    this.dumpToFolderToolStripMenuItem.Click += new EventHandler(this.dumpToFolderToolStripMenuItem_Click);
    this.toolStripMenuItem2.Name = "toolStripMenuItem2";
    this.toolStripMenuItem2.Size = new Size(149, 6);
    this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
    this.closeToolStripMenuItem.Size = new Size(152, 22);
    this.closeToolStripMenuItem.Text = "&Close";
    this.closeToolStripMenuItem.Click += new EventHandler(this.closeToolStripMenuItem_Click);
    this.editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.toolStripMenuItem3,
      (ToolStripItem) this.addEntryToolStripMenuItem
    });
    this.editToolStripMenuItem.Name = "editToolStripMenuItem";
    this.editToolStripMenuItem.Size = new Size(37, 20);
    this.editToolStripMenuItem.Text = "&Edit";
    this.editToolStripMenuItem.Paint += new PaintEventHandler(this.editToolStripMenuItem_Paint);
    this.addEntryToolStripMenuItem.Name = "addEntryToolStripMenuItem";
    this.addEntryToolStripMenuItem.Size = new Size(191, 22);
    this.addEntryToolStripMenuItem.Text = "&Add Entry";
    this.addEntryToolStripMenuItem.Click += new EventHandler(this.addEntryToolStripMenuItem_Click);
    this.saveFileDialog.DefaultExt = "bin";
    this.saveFileDialog.Filter = "BIN Files (*.bin)|*.bin||";
    this.folderBrowserDialog.Description = "Select the BIN dump destination folder";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(387, 410);
    this.Controls.Add((System.Windows.Forms.Control) this.menuStripMain);
    this.MainMenuStrip = this.menuStripMain;
    this.Name = nameof (FormBIN);
    this.Text = "BIN";
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.menuStripMain, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.treeView, 0);
    this.menuStripMain.ResumeLayout(false);
    this.menuStripMain.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
