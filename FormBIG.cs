// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormBIG
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIG;
using FableMod.Content.Forms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormBIG : FormTreeFileController
{
  private BIGFile myBIG;
  private IContainer components;
  private ToolStripMenuItem fileToolStripMenuItem;
  private ToolStripMenuItem saveAsToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem1;
  private ToolStripMenuItem closeToolStripMenuItem;
  private SaveFileDialog saveFileDialog;
  private ToolStripMenuItem findByIDToolStripMenuItem;
  private ToolStripMenuItem findByNameToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem2;
  private ToolStripMenuItem findModifiedToolStripMenuItem;
  protected internal MenuStrip menuStripMain;
  protected ToolStripMenuItem editToolStripMenuItem;
  protected ToolStripMenuItem addEntryToolStripMenuItem;

  public FormBIG()
  {
    this.InitializeComponent();
    try
    {
      this.treeView.ImageList.Images.Add(Image.FromFile(Settings.DataDirectory + "icons\\bank.bmp"));
    }
    catch (Exception ex)
    {
    }
  }

  protected override int GetObjectID(object o)
  {
    return o.GetType() == typeof (AssetEntry) ? (int) ((AssetEntry) o).ID : -1;
  }

  protected override void ShowObject(object o)
  {
    if (!(o.GetType() == typeof (AssetEntry)))
      return;
    FormMain.Instance.AddMDI((Form) new FormBIGEntry((AssetEntry) o));
  }

  protected override bool DeleteObject(object o)
  {
    if (o.GetType() == typeof (AssetEntry))
    {
      ((AssetEntry) o).Detach();
      return true;
    }
    int num = (int) MessageBox.Show("not yet implemented");
    return false;
  }

  public virtual void Build(BIGFile big, Progress progress)
  {
    this.myBIG = big;
    int steps = 0;
    for (int index = 0; index < this.myBIG.BankCount; ++index)
      steps += this.myBIG.get_Banks(index).EntryCount;
    progress.Begin(steps);
    for (int index1 = 0; index1 < this.myBIG.BankCount; ++index1)
    {
      TreeNode treeNode = new TreeNode();
      treeNode.Text = this.myBIG.get_Banks(index1).Name;
      treeNode.Tag = (object) this.myBIG.get_Banks(index1);
      treeNode.ImageIndex = 2;
      treeNode.SelectedImageIndex = 2;
      this.AddNode((TreeNode) null, treeNode);
      for (int index2 = 0; index2 < big.get_Banks(index1).EntryCount; ++index2)
      {
        AssetEntry o = this.myBIG.get_Banks(index1).get_Entries(index2);
        this.AddToTree(treeNode, o.DevSymbolName, (object) o);
        progress.Update();
      }
    }
    progress.End();
    if (this.myBIG != FileDatabase.Instance.Textures)
      return;
    this.addEntryToolStripMenuItem.Text = "Add Texture";
  }

  private void closeToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

  private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.saveFileDialog.InitialDirectory = this.Controller.Directory;
    this.saveFileDialog.FileName = this.myBIG.OriginalFileName;
    if (this.saveFileDialog.ShowDialog((IWin32Window) this) != DialogResult.OK)
      return;
    FormProcess formProcess = new FormProcess((Processor) new FileProcessor(this.Controller, this.saveFileDialog.FileName));
    int num = (int) formProcess.ShowDialog();
    formProcess.Dispose();
    if (this.Controller.FileLoaded)
      return;
    this.Close();
  }

  private void findByIDToolStripMenuItem_Click(object sender, EventArgs e) => this.FindByID();

  private void findByNameToolStripMenuItem_Click(object sender, EventArgs e) => this.FindByName();

  protected virtual AssetEntry CreateEntry(BIGBank bank)
  {
    FormNewBIGEntry formNewBigEntry = new FormNewBIGEntry(bank);
    AssetEntry entry = (AssetEntry) null;
    if (formNewBigEntry.ShowDialog() == DialogResult.OK)
    {
      entry = new AssetEntry(formNewBigEntry.SymbolName, bank.GetNewID(), formNewBigEntry.Template.Type, (AssetBank) bank);
      entry.SubHeader = formNewBigEntry.Template.SubHeader;
      entry.Data = formNewBigEntry.Template.Data;
    }
    formNewBigEntry.Dispose();
    return entry;
  }

  private void addEntryToolStripMenuItem_Click(object sender, EventArgs e)
  {
    BIGBank tag = (BIGBank) this.treeView.SelectedNode.Tag;
    AssetEntry entry = this.CreateEntry(tag);
    if (entry == null)
      return;
    tag.AddEntry(entry);
    this.treeView.SelectedNode = this.AddToTree(this.treeView.SelectedNode, entry.DevSymbolName, (object) entry);
  }

  private void editToolStripMenuItem_Paint(object sender, PaintEventArgs e)
  {
    object tag = this.treeView.SelectedNode != null ? this.treeView.SelectedNode.Tag : (object) null;
    this.addEntryToolStripMenuItem.Enabled = tag != null && tag.GetType() == typeof (BIGBank);
  }

  protected override bool ObjectIsModified(object o)
  {
    return o.GetType() == typeof (AssetEntry) && ((AssetEntry) o).Modified;
  }

  private void findModifiedToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.FindModified();
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
    this.fileToolStripMenuItem = new ToolStripMenuItem();
    this.saveAsToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem1 = new ToolStripSeparator();
    this.closeToolStripMenuItem = new ToolStripMenuItem();
    this.editToolStripMenuItem = new ToolStripMenuItem();
    this.findByIDToolStripMenuItem = new ToolStripMenuItem();
    this.findByNameToolStripMenuItem = new ToolStripMenuItem();
    this.findModifiedToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem2 = new ToolStripSeparator();
    this.addEntryToolStripMenuItem = new ToolStripMenuItem();
    this.saveFileDialog = new SaveFileDialog();
    this.menuStripMain.SuspendLayout();
    this.SuspendLayout();
    this.treeView.LineColor = Color.Black;
    this.treeView.Location = new Point(0, 24);
    this.treeView.Size = new Size(514, 370);
    this.menuStripMain.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.fileToolStripMenuItem,
      (ToolStripItem) this.editToolStripMenuItem
    });
    this.menuStripMain.Location = new Point(0, 0);
    this.menuStripMain.Name = "menuStripMain";
    this.menuStripMain.RenderMode = ToolStripRenderMode.System;
    this.menuStripMain.Size = new Size(514, 24);
    this.menuStripMain.TabIndex = 1;
    this.menuStripMain.Text = "Main Menu";
    this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.saveAsToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem1,
      (ToolStripItem) this.closeToolStripMenuItem
    });
    this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
    this.fileToolStripMenuItem.Size = new Size(35, 20);
    this.fileToolStripMenuItem.Text = "&File";
    this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
    this.saveAsToolStripMenuItem.Size = new Size(152, 22);
    this.saveAsToolStripMenuItem.Text = "Save &As...";
    this.saveAsToolStripMenuItem.Click += new EventHandler(this.saveAsToolStripMenuItem_Click);
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    this.toolStripMenuItem1.Size = new Size(149, 6);
    this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
    this.closeToolStripMenuItem.ShortcutKeys = Keys.X | Keys.Control;
    this.closeToolStripMenuItem.Size = new Size(152, 22);
    this.closeToolStripMenuItem.Text = "&Close";
    this.closeToolStripMenuItem.Click += new EventHandler(this.closeToolStripMenuItem_Click);
    this.editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this.findByIDToolStripMenuItem,
      (ToolStripItem) this.findByNameToolStripMenuItem,
      (ToolStripItem) this.findModifiedToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem2,
      (ToolStripItem) this.addEntryToolStripMenuItem
    });
    this.editToolStripMenuItem.Name = "editToolStripMenuItem";
    this.editToolStripMenuItem.Size = new Size(37, 20);
    this.editToolStripMenuItem.Text = "&Edit";
    this.editToolStripMenuItem.Paint += new PaintEventHandler(this.editToolStripMenuItem_Paint);
    this.findByIDToolStripMenuItem.Name = "findByIDToolStripMenuItem";
    this.findByIDToolStripMenuItem.ShortcutKeys = Keys.F | Keys.Shift | Keys.Control;
    this.findByIDToolStripMenuItem.Size = new Size(191, 22);
    this.findByIDToolStripMenuItem.Text = "Find By &ID";
    this.findByIDToolStripMenuItem.Click += new EventHandler(this.findByIDToolStripMenuItem_Click);
    this.findByNameToolStripMenuItem.Name = "findByNameToolStripMenuItem";
    this.findByNameToolStripMenuItem.ShortcutKeys = Keys.F | Keys.Control;
    this.findByNameToolStripMenuItem.Size = new Size(191, 22);
    this.findByNameToolStripMenuItem.Text = "Find By &Name";
    this.findByNameToolStripMenuItem.Click += new EventHandler(this.findByNameToolStripMenuItem_Click);
    this.findModifiedToolStripMenuItem.Name = "findModifiedToolStripMenuItem";
    this.findModifiedToolStripMenuItem.Size = new Size(191, 22);
    this.findModifiedToolStripMenuItem.Text = "Find &Modified";
    this.findModifiedToolStripMenuItem.Click += new EventHandler(this.findModifiedToolStripMenuItem_Click);
    this.toolStripMenuItem2.Name = "toolStripMenuItem2";
    this.toolStripMenuItem2.Size = new Size(188, 6);
    this.addEntryToolStripMenuItem.Name = "addEntryToolStripMenuItem";
    this.addEntryToolStripMenuItem.Size = new Size(191, 22);
    this.addEntryToolStripMenuItem.Text = "&Add Entry";
    this.addEntryToolStripMenuItem.Click += new EventHandler(this.addEntryToolStripMenuItem_Click);
    this.saveFileDialog.DefaultExt = "big";
    this.saveFileDialog.Filter = "BIG Files (*.big)|*.big||";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(514, 394);
    this.Controls.Add((Control) this.menuStripMain);
    this.KeyPreview = true;
    this.MainMenuStrip = this.menuStripMain;
    this.Name = nameof (FormBIG);
    this.Text = "BIG";
    this.Controls.SetChildIndex((Control) this.menuStripMain, 0);
    this.Controls.SetChildIndex((Control) this.treeView, 0);
    this.menuStripMain.ResumeLayout(false);
    this.menuStripMain.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
