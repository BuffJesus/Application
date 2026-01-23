// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormBIG
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIG;
using FableMod.Content.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using FableMod.Gfx.Integration;

#nullable disable
namespace ChocolateBox;

public class FormBIG : FormTreeFileController
{
  private BIGFile myBIG;
  private IContainer components;
  private MenuStrip menuStripMain;
  private ToolStripMenuItem fileToolStripMenuItem;
  private ToolStripMenuItem saveAsToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem1;
  private ToolStripMenuItem closeToolStripMenuItem;
  private SaveFileDialog saveFileDialog;
  private ToolStripMenuItem findByIDToolStripMenuItem;
  private ToolStripMenuItem findByNameToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem2;
  private ToolStripMenuItem findModifiedToolStripMenuItem;
  protected internal MenuStrip menuStripMain_protected;
  protected ToolStripMenuItem editToolStripMenuItem;
  protected ToolStripMenuItem addEntryToolStripMenuItem;

  private ToolStripMenuItem bulkExportToolStripMenuItem;
  private ToolStripMenuItem exportMeshToolStripMenuItem;
  private ContextMenuStrip contextMenuStripTree;

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

    this.btnExportSelected.Visible = true;

    this.contextMenuStripTree = new ContextMenuStrip();
    this.bulkExportToolStripMenuItem = new ToolStripMenuItem();
    this.exportMeshToolStripMenuItem = new ToolStripMenuItem();
    
    this.bulkExportToolStripMenuItem.Text = "Bulk Export Selected...";
    this.bulkExportToolStripMenuItem.Click += new EventHandler(this.bulkExportToolStripMenuItem_Click);
    
    this.exportMeshToolStripMenuItem.Text = "Export Mesh as...";
    this.exportMeshToolStripMenuItem.Click += new EventHandler((s, e) => this.ExportMesh());
    
    this.contextMenuStripTree.Items.Add(this.exportMeshToolStripMenuItem);
    this.contextMenuStripTree.Items.Add(this.bulkExportToolStripMenuItem);
    this.treeView.ContextMenuStrip = this.contextMenuStripTree;
  }

  protected override void OnExportSelected()
  {
      this.ExportSelected();
  }

  private void ExportMesh()
  {
      if (!(this.treeView.SelectedNode.Tag is AssetEntry entry))
          return;

      // Meshes are typically type 1, 2, 4, or 5 in the Graphics bank
      if (((BIGBank)entry.Bank).Name != Settings.GetString("Banks", "Graphics") ||
          (entry.Type != 1U && entry.Type != 2U && entry.Type != 4U && entry.Type != 5U))
      {
          MessageBox.Show("This asset is not a mesh and cannot be exported as a model.", "Not a Mesh", MessageBoxButtons.OK, MessageBoxIcon.Warning);
          return;
      }

      SaveFileDialog saveFileDialog = new SaveFileDialog();
      saveFileDialog.FileName = entry.DevSymbolName;
      saveFileDialog.Filter = "Legacy .X (*.x)|*.x|OBJ (*.obj)|*.obj";

      if (saveFileDialog.ShowDialog() != DialogResult.OK)
          return;

      ExportFormat format = ExportFormat.X;
      string extension = Path.GetExtension(saveFileDialog.FileName)?.ToLowerInvariant();
      if (extension == ".obj") format = ExportFormat.OBJ;
      else if (saveFileDialog.FilterIndex == 2) format = ExportFormat.OBJ;

      string directory = Path.GetDirectoryName(saveFileDialog.FileName);
      string fileName = Path.GetFileNameWithoutExtension(saveFileDialog.FileName);

      try
      {
          GfxModel model = new GfxModel(entry);
          if (model.LODCount > 0)
          {
              ModelExporter.Export(model.get_LODs(0), directory, fileName, format);
              MessageBox.Show("Mesh exported successfully.", "Export Successful", MessageBoxButtons.OK, MessageBoxIcon.Information);
          }
      }
      catch (Exception ex)
      {
          MessageBox.Show("Error exporting mesh: " + ex.Message, "Export Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
      }
  }

  private void bulkExportToolStripMenuItem_Click(object sender, EventArgs e)
  {
      this.ExportSelected();
  }

  private void ExportSelected()
  {
    List<AssetEntry> selectedEntries = new List<AssetEntry>();
    this.GetCheckedEntries(this.treeView.Nodes, selectedEntries);

    if (selectedEntries.Count == 0)
    {
        MessageBox.Show("No assets selected for export. Please check the boxes next to the assets you want to export.", "No Assets Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
        return;
    }

    FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
    folderBrowserDialog.Description = "Select the parent folder for the bulk export";
    if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
        return;

    string parentFolder = folderBrowserDialog.SelectedPath;

    FormExportOptions exportOptions = new FormExportOptions();
    if (exportOptions.ShowDialog() != DialogResult.OK)
        return;

    ExportFormat format = exportOptions.SelectedFormat;
    
    FormProcess formProcess = new FormProcess(new BulkExportProcessor(selectedEntries, parentFolder, format));
    formProcess.ShowDialog();
    formProcess.Dispose();
  }


  private void GetCheckedEntries(TreeNodeCollection nodes, List<AssetEntry> selectedEntries)
  {
    foreach (TreeNode node in nodes)
    {
      if (node.Checked && node.Tag is AssetEntry entry)
      {
        selectedEntries.Add(entry);
      }
      if (node.Nodes.Count > 0)
      {
        GetCheckedEntries(node.Nodes, selectedEntries);
      }
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
    if (this.treeView.InvokeRequired)
    {
        this.treeView.Invoke(new Action(() => this.Build(big, progress)));
        return;
    }

    this.myBIG = big;
    int steps = 0;
    int bankCount = this.myBIG.BankCount;
    for (int index = 0; index < bankCount; ++index)
      steps += this.myBIG.get_Banks(index).EntryCount;

    Console.WriteLine($"[DEBUG] Building BIG tree with {steps} total entries across {bankCount} banks");
    progress.Begin(steps);
    this.treeView.BeginUpdate();
    this.ClearNodeCache();
    try
    {
        // Update progress every 10 entries (was every 1%)
        int progressStep = 10;
        int currentStep = 0;
        System.DateTime startTime = System.DateTime.Now;

        for (int index1 = 0; index1 < bankCount; ++index1)
        {
          BIGBank bank = this.myBIG.get_Banks(index1);
          int entryCount = bank.EntryCount;
          progress.StepInfo = $"(Bank {index1 + 1} of {bankCount}: {bank.Name} - {entryCount} entries)";
          Console.WriteLine($"[DEBUG] Processing bank {index1 + 1}/{bankCount}: {bank.Name} ({entryCount} entries)");

          TreeNode treeNode = new TreeNode();
          treeNode.Text = bank.Name;
          treeNode.Tag = (object) bank;
          treeNode.ImageIndex = 2;
          treeNode.SelectedImageIndex = 2;

          for (int index2 = 0; index2 < entryCount; ++index2)
          {
            AssetEntry o = bank.get_Entries(index2);
            this.AddToTree(treeNode, o.DevSymbolName, (object) o);
            currentStep++;
            if (currentStep % progressStep == 0)
                progress.Update();
          }

          // Add the fully populated bank node in one go
          this.AddNode(null, treeNode);
          // Collapse the node to prevent expensive layout calculations
          treeNode.Collapse();
          Console.WriteLine($"[DEBUG] Completed bank {index1 + 1}/{bankCount} in {(System.DateTime.Now - startTime).TotalSeconds:F1}s");
        }

        Console.WriteLine($"[DEBUG] Total tree build time: {(System.DateTime.Now - startTime).TotalSeconds:F1}s");
    }
    finally
    {
        progress.StepInfo = "";
        Console.WriteLine($"[DEBUG] Calling TreeView.EndUpdate()");
        this.treeView.EndUpdate();
        Console.WriteLine($"[DEBUG] TreeView.EndUpdate() completed");
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
    this.editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.toolStripMenuItem2,
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
    this.saveFileDialog.DefaultExt = "big";
    this.saveFileDialog.Filter = "BIG Files (*.big)|*.big||";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(514, 394);
    this.Controls.Add((Control) this.menuStripMain);
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
