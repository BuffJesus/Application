// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormBBB
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BBB;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormBBB : FormTreeFileController
{
  private BBBFile myBBB;
  private IContainer components;
  private MenuStrip menuStripMain;
  private ToolStripMenuItem fileToolStripMenuItem;
  private ToolStripMenuItem extractToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem1;
  private ToolStripMenuItem saveToolStripMenuItem;
  private ToolStripMenuItem saveAsToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem2;
  private ToolStripMenuItem closeToolStripMenuItem;
  private FolderBrowserDialog folderBrowserDialog;

  public FormBBB() => this.InitializeComponent();

  public void Build(BBBFile bbb, Progress progress)
  {
    this.myBBB = bbb;
    TreeNode treeNode = new TreeNode();
    treeNode.Text = "Files";
    this.treeView.Nodes.Add(treeNode);
    progress.Begin(this.myBBB.EntryCount);
    for (int index = 0; index < this.myBBB.EntryCount; ++index)
    {
      string fileName = this.myBBB.get_Entries(index).FileName;
      this.AddToTree(treeNode, fileName, (object) fileName);
      progress.Update();
    }
    progress.End();
  }

  private void closeToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

  private void extractToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.folderBrowserDialog.SelectedPath = Settings.FableDirectory;
    if (this.folderBrowserDialog.ShowDialog() != DialogResult.OK)
      return;
    int num = (int) new FormProcess((Processor) new BBBExtractor(this.myBBB, this.folderBrowserDialog.SelectedPath)).ShowDialog();
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
    this.extractToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem1 = new ToolStripSeparator();
    this.saveToolStripMenuItem = new ToolStripMenuItem();
    this.saveAsToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem2 = new ToolStripSeparator();
    this.closeToolStripMenuItem = new ToolStripMenuItem();
    this.folderBrowserDialog = new FolderBrowserDialog();
    this.menuStripMain.SuspendLayout();
    this.SuspendLayout();
    this.treeView.LineColor = Color.Black;
    this.treeView.Location = new Point(0, 24);
    this.treeView.Size = new Size(292, 242);
    this.menuStripMain.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.fileToolStripMenuItem
    });
    this.menuStripMain.Location = new Point(0, 0);
    this.menuStripMain.Name = "menuStripMain";
    this.menuStripMain.RenderMode = ToolStripRenderMode.System;
    this.menuStripMain.Size = new Size(292, 24);
    this.menuStripMain.TabIndex = 1;
    this.menuStripMain.Text = "menuStrip1";
    this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[6]
    {
      (ToolStripItem) this.extractToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem1,
      (ToolStripItem) this.saveToolStripMenuItem,
      (ToolStripItem) this.saveAsToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem2,
      (ToolStripItem) this.closeToolStripMenuItem
    });
    this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
    this.fileToolStripMenuItem.Size = new Size(35, 20);
    this.fileToolStripMenuItem.Text = "&File";
    this.extractToolStripMenuItem.Name = "extractToolStripMenuItem";
    this.extractToolStripMenuItem.Size = new Size(125, 22);
    this.extractToolStripMenuItem.Text = "&Extract";
    this.extractToolStripMenuItem.Click += new EventHandler(this.extractToolStripMenuItem_Click);
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    this.toolStripMenuItem1.Size = new Size(122, 6);
    this.saveToolStripMenuItem.Name = "saveToolStripMenuItem";
    this.saveToolStripMenuItem.Size = new Size(125, 22);
    this.saveToolStripMenuItem.Text = "&Save";
    this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
    this.saveAsToolStripMenuItem.Size = new Size(125, 22);
    this.saveAsToolStripMenuItem.Text = "Save &As...";
    this.toolStripMenuItem2.Name = "toolStripMenuItem2";
    this.toolStripMenuItem2.Size = new Size(122, 6);
    this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
    this.closeToolStripMenuItem.Size = new Size(125, 22);
    this.closeToolStripMenuItem.Text = "&Close";
    this.closeToolStripMenuItem.Click += new EventHandler(this.closeToolStripMenuItem_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(292, 266);
    this.Controls.Add((Control) this.menuStripMain);
    this.MainMenuStrip = this.menuStripMain;
    this.Name = nameof (FormBBB);
    this.Text = "BBB";
    this.UnderscoreSeparator = false;
    this.Controls.SetChildIndex((Control) this.menuStripMain, 0);
    this.Controls.SetChildIndex((Control) this.treeView, 0);
    this.menuStripMain.ResumeLayout(false);
    this.menuStripMain.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
