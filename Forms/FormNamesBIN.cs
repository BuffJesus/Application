// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormNamesBIN
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIN;
using FableMod.Forms;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormNamesBIN : FormFileController
{
  private IContainer components;
  private MenuStrip menuStripMain;
  private SaveFileDialog saveFileDialog;
  private ToolStripMenuItem fileToolStripMenuItem;
  private ToolStripMenuItem exitToolStripMenuItem;
  private ListView listViewEntries;
  private ColumnHeader columnHeader1;
  private ColumnHeader columnHeader2;
  private ColumnHeader columnHeader3;
  private ToolStripMenuItem editToolStripMenuItem;
  private ToolStripMenuItem addToolStripMenuItem;
  private ToolStripMenuItem saveAsToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem1;
  private NamesBINFile myNames;

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
    this.exitToolStripMenuItem = new ToolStripMenuItem();
    this.editToolStripMenuItem = new ToolStripMenuItem();
    this.addToolStripMenuItem = new ToolStripMenuItem();
    this.saveFileDialog = new SaveFileDialog();
    this.listViewEntries = new ListView();
    this.columnHeader1 = new ColumnHeader();
    this.columnHeader2 = new ColumnHeader();
    this.columnHeader3 = new ColumnHeader();
    this.toolStripMenuItem1 = new ToolStripSeparator();
    this.saveAsToolStripMenuItem = new ToolStripMenuItem();
    this.menuStripMain.SuspendLayout();
    this.SuspendLayout();
    this.menuStripMain.Items.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.fileToolStripMenuItem,
      (ToolStripItem) this.editToolStripMenuItem
    });
    this.menuStripMain.Location = new Point(0, 0);
    this.menuStripMain.Name = "menuStripMain";
    this.menuStripMain.RenderMode = ToolStripRenderMode.System;
    this.menuStripMain.Size = new Size(504, 24);
    this.menuStripMain.TabIndex = 0;
    this.menuStripMain.Text = "menuStrip1";
    this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.saveAsToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem1,
      (ToolStripItem) this.exitToolStripMenuItem
    });
    this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
    this.fileToolStripMenuItem.Size = new Size(35, 20);
    this.fileToolStripMenuItem.Text = "&File";
    this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
    this.exitToolStripMenuItem.Size = new Size(152, 22);
    this.exitToolStripMenuItem.Text = "&Close";
    this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
    this.editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.addToolStripMenuItem
    });
    this.editToolStripMenuItem.Name = "editToolStripMenuItem";
    this.editToolStripMenuItem.Size = new Size(37, 20);
    this.editToolStripMenuItem.Text = "&Edit";
    this.addToolStripMenuItem.Name = "addToolStripMenuItem";
    this.addToolStripMenuItem.Size = new Size(152, 22);
    this.addToolStripMenuItem.Text = "&Add";
    this.addToolStripMenuItem.Click += new EventHandler(this.addToolStripMenuItem_Click);
    this.saveFileDialog.DefaultExt = "bin";
    this.saveFileDialog.FileName = "Names.bin";
    this.saveFileDialog.Filter = "BIN Files (*.bin)|*.bin||";
    this.listViewEntries.Columns.AddRange(new ColumnHeader[3]
    {
      this.columnHeader1,
      this.columnHeader2,
      this.columnHeader3
    });
    this.listViewEntries.Cursor = Cursors.Default;
    this.listViewEntries.Dock = DockStyle.Fill;
    this.listViewEntries.GridLines = true;
    this.listViewEntries.HeaderStyle = ColumnHeaderStyle.Nonclickable;
    this.listViewEntries.HideSelection = false;
    this.listViewEntries.Location = new Point(0, 24);
    this.listViewEntries.Name = "listViewEntries";
    this.listViewEntries.Size = new Size(504, 383);
    this.listViewEntries.Sorting = SortOrder.Ascending;
    this.listViewEntries.TabIndex = 1;
    this.listViewEntries.UseCompatibleStateImageBehavior = false;
    this.listViewEntries.View = View.Details;
    this.columnHeader1.Text = "Name";
    this.columnHeader1.Width = 175;
    this.columnHeader2.Text = "Enum";
    this.columnHeader2.Width = 150;
    this.columnHeader3.Text = "Offset";
    this.columnHeader3.Width = 171;
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    this.toolStripMenuItem1.Size = new Size(149, 6);
    this.saveAsToolStripMenuItem.Name = "saveAsToolStripMenuItem";
    this.saveAsToolStripMenuItem.Size = new Size(152, 22);
    this.saveAsToolStripMenuItem.Text = "Save As...";
    this.saveAsToolStripMenuItem.Click += new EventHandler(this.saveAsToolStripMenuItem_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(504, 407);
    this.Controls.Add((Control) this.listViewEntries);
    this.Controls.Add((Control) this.menuStripMain);
    this.MainMenuStrip = this.menuStripMain;
    this.Name = nameof (FormNamesBIN);
    this.Text = "Names";
    this.menuStripMain.ResumeLayout(false);
    this.menuStripMain.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public FormNamesBIN()
  {
    this.InitializeComponent();
    ThemeManager.ApplyTheme(this);
  }

  public void Build(NamesBINFile bin, Progress progress)
  {
    this.myNames = bin;
    progress.Begin(bin.EntryCount);
    for (int index = 0; index < bin.EntryCount; ++index)
    {
      NamesBINEntry namesBinEntry = bin.get_Entries(index);
      this.listViewEntries.Items.Add(new ListViewItem()
      {
        Text = namesBinEntry.Name,
        SubItems = {
          namesBinEntry.Enum.ToString(),
          namesBinEntry.Offset.ToString()
        }
      });
      progress.Update();
    }
    progress.End();
  }

  private void addToolStripMenuItem_Click(object sender, EventArgs e)
  {
    FormTextBox formTextBox = new FormTextBox();
    formTextBox.labelInput.Text = "Input name:";
    formTextBox.Text = "Names.BIN: New Entry";
    if (formTextBox.ShowDialog() != DialogResult.OK)
      return;
    NamesBINEntry namesBinEntry = this.myNames.AddEntry(formTextBox.textBoxInput.Text);
    this.listViewEntries.Items.Add(new ListViewItem()
    {
      Text = namesBinEntry.Name,
      SubItems = {
        namesBinEntry.Enum.ToString(),
        namesBinEntry.Offset.ToString()
      }
    });
    this.listViewEntries.Sort();
  }

  private void exitToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

  private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.saveFileDialog.InitialDirectory = this.Controller.Directory;
    this.saveFileDialog.FileName = this.myNames.OriginalFileName;
    if (this.saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    this.myNames.Save(this.saveFileDialog.FileName);
    int num = (int) FormMain.Instance.InfoMessage("OK.");
  }
}
