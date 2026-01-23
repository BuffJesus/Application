// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormMain
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.ContentManagement;
using FableMod.Forms;
using FableMod.Gfx.Integration;
using FableMod.TNG;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace ChocolateBox;

public class FormMain : FormApp, FileInterface
{
  private IContainer components;
  private MenuStrip menuStripMain;
  private ToolStripMenuItem fileToolStripMenuItem;
  private ToolStripMenuItem exitToolStripMenuItem;
  private Panel panelLeft;
  private ListBox listBoxFiles;
  private Splitter splitterMain;
  private ToolStripMenuItem loadToolStripMenuItem;
  private OpenFileDialog openFileDialog;
  private StatusStrip statusStrip;
  private ToolStripStatusLabel toolStripStatusLabel;
  private ToolStripSeparator toolStripMenuItem1;
  private ToolStripMenuItem editorsToolStripMenuItem;
  private ToolStripMenuItem editorToolStripMenuItem;
  private ToolStripMenuItem viewToolStripMenuItem;
  private ToolStripMenuItem fileListToolStripMenuItem;
  private ToolStripMenuItem saveAndRunFableToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem2;
  private ToolStripMenuItem helpToolStripMenuItem;
  private ToolStripMenuItem aboutToolStripMenuItem;
  private ToolStripMenuItem newModPackageToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem4;
  private ToolStripMenuItem loadModPackageToolStripMenuItem;
  private OpenFileDialog openFileDialogMod;
  private Timer timerTip;
  private ToolStripMenuItem toolsToolStripMenuItem;
  private ToolStripMenuItem generateUIDDatabaseToolStripMenuItem;
  private ToolStripMenuItem completeUIDCheckToolStripMenuItem;
  private ContextMenuStrip contextMenuStripFile;
  private ToolStripMenuItem openToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem3;
  private ToolStripMenuItem backupToolStripMenuItem;
  private ToolStripMenuItem rollBackToolStripMenuItem;
  private ToolStripMenuItem objectBuilderToolStripMenuItem;
  private FileDatabase myFileDB;
  private DefinitionDB myDefinitionDB = new DefinitionDB();
  private TNGDefinitions myTNGDefinitions = new TNGDefinitions();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormMain));
    this.menuStripMain = new MenuStrip();
    this.fileToolStripMenuItem = new ToolStripMenuItem();
    this.loadToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem1 = new ToolStripSeparator();
    this.newModPackageToolStripMenuItem = new ToolStripMenuItem();
    this.loadModPackageToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem2 = new ToolStripSeparator();
    this.saveAndRunFableToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem4 = new ToolStripSeparator();
    this.exitToolStripMenuItem = new ToolStripMenuItem();
    this.viewToolStripMenuItem = new ToolStripMenuItem();
    this.fileListToolStripMenuItem = new ToolStripMenuItem();
    this.editorsToolStripMenuItem = new ToolStripMenuItem();
    this.editorToolStripMenuItem = new ToolStripMenuItem();
    this.objectBuilderToolStripMenuItem = new ToolStripMenuItem();
    this.toolsToolStripMenuItem = new ToolStripMenuItem();
    this.generateUIDDatabaseToolStripMenuItem = new ToolStripMenuItem();
    this.completeUIDCheckToolStripMenuItem = new ToolStripMenuItem();
    this.helpToolStripMenuItem = new ToolStripMenuItem();
    this.aboutToolStripMenuItem = new ToolStripMenuItem();
    this.panelLeft = new Panel();
    this.listBoxFiles = new ListBox();
    this.contextMenuStripFile = new ContextMenuStrip(this.components);
    this.openToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem3 = new ToolStripSeparator();
    this.backupToolStripMenuItem = new ToolStripMenuItem();
    this.rollBackToolStripMenuItem = new ToolStripMenuItem();
    this.splitterMain = new Splitter();
    this.openFileDialog = new OpenFileDialog();
    this.statusStrip = new StatusStrip();
    this.toolStripStatusLabel = new ToolStripStatusLabel();
    this.openFileDialogMod = new OpenFileDialog();
    this.timerTip = new Timer(this.components);
    this.menuStripMain.SuspendLayout();
    this.panelLeft.SuspendLayout();
    this.contextMenuStripFile.SuspendLayout();
    this.statusStrip.SuspendLayout();
    this.SuspendLayout();
    this.menuStripMain.Items.AddRange(new ToolStripItem[5]
    {
      (ToolStripItem) this.fileToolStripMenuItem,
      (ToolStripItem) this.viewToolStripMenuItem,
      (ToolStripItem) this.editorsToolStripMenuItem,
      (ToolStripItem) this.toolsToolStripMenuItem,
      (ToolStripItem) this.helpToolStripMenuItem
    });
    this.menuStripMain.Location = new Point(0, 0);
    this.menuStripMain.Name = "menuStripMain";
    this.menuStripMain.RenderMode = ToolStripRenderMode.System;
    this.menuStripMain.Size = new Size(560, 24);
    this.menuStripMain.TabIndex = 1;
    this.menuStripMain.Text = "menuStrip1";
    this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[8]
    {
      (ToolStripItem) this.loadToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem1,
      (ToolStripItem) this.newModPackageToolStripMenuItem,
      (ToolStripItem) this.loadModPackageToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem2,
      (ToolStripItem) this.saveAndRunFableToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem4,
      (ToolStripItem) this.exitToolStripMenuItem
    });
    this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
    this.fileToolStripMenuItem.Size = new Size(35, 20);
    this.fileToolStripMenuItem.Text = "&File";
    this.loadToolStripMenuItem.Name = "loadToolStripMenuItem";
    this.loadToolStripMenuItem.Size = new Size(175, 22);
    this.loadToolStripMenuItem.Text = "&Load...";
    this.loadToolStripMenuItem.ToolTipText = "Load a Fable resource file";
    this.loadToolStripMenuItem.Click += new EventHandler(this.loadToolStripMenuItem_Click);
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    this.toolStripMenuItem1.Size = new Size(172, 6);
    this.newModPackageToolStripMenuItem.Name = "newModPackageToolStripMenuItem";
    this.newModPackageToolStripMenuItem.Size = new Size(175, 22);
    this.newModPackageToolStripMenuItem.Text = "New Mod Package...";
    this.newModPackageToolStripMenuItem.ToolTipText = "Create a new Fable mod package";
    this.newModPackageToolStripMenuItem.Click += new EventHandler(this.newModPackageToolStripMenuItem_Click);
    this.loadModPackageToolStripMenuItem.Name = "loadModPackageToolStripMenuItem";
    this.loadModPackageToolStripMenuItem.Size = new Size(175, 22);
    this.loadModPackageToolStripMenuItem.Text = "Load Mod Package...";
    this.loadModPackageToolStripMenuItem.ToolTipText = "Load an existing Fable mod package";
    this.loadModPackageToolStripMenuItem.Click += new EventHandler(this.loadModPackageToolStripMenuItem_Click);
    this.toolStripMenuItem2.Name = "toolStripMenuItem2";
    this.toolStripMenuItem2.Size = new Size(172, 6);
    this.saveAndRunFableToolStripMenuItem.Name = "saveAndRunFableToolStripMenuItem";
    this.saveAndRunFableToolStripMenuItem.Size = new Size(175, 22);
    this.saveAndRunFableToolStripMenuItem.Text = "Save and Run Fable";
    this.saveAndRunFableToolStripMenuItem.ToolTipText = "Save everything, close windows and run Fable";
    this.saveAndRunFableToolStripMenuItem.Click += new EventHandler(this.saveAllAndRunFableToolStripMenuItem_Click);
    this.toolStripMenuItem4.Name = "toolStripMenuItem4";
    this.toolStripMenuItem4.Size = new Size(172, 6);
    this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
    this.exitToolStripMenuItem.Size = new Size(175, 22);
    this.exitToolStripMenuItem.Text = "&Exit";
    this.exitToolStripMenuItem.ToolTipText = "Exit application";
    this.exitToolStripMenuItem.Click += new EventHandler(this.exitToolStripMenuItem_Click);
    this.viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.fileListToolStripMenuItem
    });
    this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
    this.viewToolStripMenuItem.Size = new Size(41, 20);
    this.viewToolStripMenuItem.Text = "&View";
    this.fileListToolStripMenuItem.Checked = true;
    this.fileListToolStripMenuItem.CheckState = CheckState.Checked;
    this.fileListToolStripMenuItem.Name = "fileListToolStripMenuItem";
    this.fileListToolStripMenuItem.Size = new Size(152, 22);
    this.fileListToolStripMenuItem.Text = "File List";
    this.fileListToolStripMenuItem.Click += new EventHandler(this.fileListToolStripMenuItem_Click);
    this.editorsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.editorToolStripMenuItem,
      (ToolStripItem) this.objectBuilderToolStripMenuItem
    });
    this.editorsToolStripMenuItem.Name = "editorsToolStripMenuItem";
    this.editorsToolStripMenuItem.Size = new Size(52, 20);
    this.editorsToolStripMenuItem.Text = "&Editors";
    this.editorToolStripMenuItem.Name = "editorToolStripMenuItem";
    this.editorToolStripMenuItem.Size = new Size(152, 22);
    this.editorToolStripMenuItem.Text = "&Region Editor";
    this.editorToolStripMenuItem.ToolTipText = "3D Region Editor";
    this.editorToolStripMenuItem.Click += new EventHandler(this.editorToolStripMenuItem_Click);
    this.objectBuilderToolStripMenuItem.Name = "objectBuilderToolStripMenuItem";
    this.objectBuilderToolStripMenuItem.Size = new Size(152, 22);
    this.objectBuilderToolStripMenuItem.Text = "Object Builder";
    this.objectBuilderToolStripMenuItem.ToolTipText = "Create new objects";
    this.objectBuilderToolStripMenuItem.Click += new EventHandler(this.itemBuilderToolStripMenuItem_Click);
    this.toolsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[2]
    {
      (ToolStripItem) this.generateUIDDatabaseToolStripMenuItem,
      (ToolStripItem) this.completeUIDCheckToolStripMenuItem
    });
    this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
    this.toolsToolStripMenuItem.Size = new Size(44, 20);
    this.toolsToolStripMenuItem.Text = "Tools";
    this.generateUIDDatabaseToolStripMenuItem.Name = "generateUIDDatabaseToolStripMenuItem";
    this.generateUIDDatabaseToolStripMenuItem.Size = new Size(189, 22);
    this.generateUIDDatabaseToolStripMenuItem.Text = "Generate UID Database";
    this.generateUIDDatabaseToolStripMenuItem.ToolTipText = "Generate an UID database";
    this.generateUIDDatabaseToolStripMenuItem.Click += new EventHandler(this.generateUIDDatabaseToolStripMenuItem_Click);
    this.completeUIDCheckToolStripMenuItem.Name = "completeUIDCheckToolStripMenuItem";
    this.completeUIDCheckToolStripMenuItem.Size = new Size(189, 22);
    this.completeUIDCheckToolStripMenuItem.Text = "UID Check";
    this.completeUIDCheckToolStripMenuItem.ToolTipText = "Check all TNG files for duplicate UIDs";
    this.completeUIDCheckToolStripMenuItem.Click += new EventHandler(this.completeUIDCheckToolStripMenuItem_Click);
    this.helpToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.aboutToolStripMenuItem
    });
    this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
    this.helpToolStripMenuItem.Size = new Size(40, 20);
    this.helpToolStripMenuItem.Text = "Help";
    this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
    this.aboutToolStripMenuItem.Size = new Size(172, 22);
    this.aboutToolStripMenuItem.Text = "About ChocolateBox";
    this.aboutToolStripMenuItem.ToolTipText = "About ChocolateBox";
    this.aboutToolStripMenuItem.Click += new EventHandler(this.aboutToolStripMenuItem_Click);
    this.panelLeft.Controls.Add((System.Windows.Forms.Control) this.listBoxFiles);
    this.panelLeft.Dock = DockStyle.Left;
    this.panelLeft.Location = new Point(0, 24);
    this.panelLeft.Name = "panelLeft";
    this.panelLeft.Padding = new Padding(10);
    this.panelLeft.Size = new Size(180, 374);
    this.panelLeft.TabIndex = 2;
    this.listBoxFiles.BorderStyle = BorderStyle.None;
    this.listBoxFiles.ContextMenuStrip = this.contextMenuStripFile;
    this.listBoxFiles.Dock = DockStyle.Fill;
    this.listBoxFiles.FormattingEnabled = true;
    this.listBoxFiles.Location = new Point(10, 10);
    this.listBoxFiles.Name = "listBoxFiles";
    this.listBoxFiles.Size = new Size(160, 354);
    this.listBoxFiles.TabIndex = 0;
    this.listBoxFiles.DoubleClick += new EventHandler(this.listBoxFiles_DoubleClick);
    this.contextMenuStripFile.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.openToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem3,
      (ToolStripItem) this.backupToolStripMenuItem,
      (ToolStripItem) this.rollBackToolStripMenuItem
    });
    this.contextMenuStripFile.Name = "contextMenuStripFile";
    this.contextMenuStripFile.RenderMode = ToolStripRenderMode.System;
    this.contextMenuStripFile.Size = new Size(114, 76);
    this.openToolStripMenuItem.Name = "openToolStripMenuItem";
    this.openToolStripMenuItem.Size = new Size(113, 22);
    this.openToolStripMenuItem.Text = "Open";
    this.openToolStripMenuItem.Click += new EventHandler(this.openToolStripMenuItem_Click);
    this.toolStripMenuItem3.Name = "toolStripMenuItem3";
    this.toolStripMenuItem3.Size = new Size(110, 6);
    this.backupToolStripMenuItem.Name = "backupToolStripMenuItem";
    this.backupToolStripMenuItem.Size = new Size(113, 22);
    this.backupToolStripMenuItem.Text = "Backup";
    this.backupToolStripMenuItem.Click += new EventHandler(this.backupToolStripMenuItem_Click);
    this.rollBackToolStripMenuItem.Name = "rollBackToolStripMenuItem";
    this.rollBackToolStripMenuItem.Size = new Size(113, 22);
    this.rollBackToolStripMenuItem.Text = "Rollback";
    this.rollBackToolStripMenuItem.Click += new EventHandler(this.rollBackToolStripMenuItem_Click);
    this.splitterMain.Location = new Point(180, 24);
    this.splitterMain.Name = "splitterMain";
    this.splitterMain.Size = new Size(4, 374);
    this.splitterMain.TabIndex = 3;
    this.splitterMain.TabStop = false;
    this.openFileDialog.Filter = "Fable Files (*.big;*.bin;*.wad;*.wld; *.stb)|*.big;*.bin;*.wld;*.wad; *.tng;*.stb||";
    this.openFileDialog.Multiselect = true;
    this.openFileDialog.Title = "Open Fable File...";
    this.statusStrip.Items.AddRange(new ToolStripItem[1]
    {
      (ToolStripItem) this.toolStripStatusLabel
    });
    this.statusStrip.Location = new Point(0, 396);
    this.statusStrip.Name = "statusStrip";
    this.statusStrip.Size = new Size(560, 24);
    this.statusStrip.TabIndex = 5;
    this.statusStrip.Text = "Feel the Blaze...!!";
    this.toolStripStatusLabel.Name = "toolStripStatusLabel";
    this.toolStripStatusLabel.Size = new Size(94, 19);
    this.toolStripStatusLabel.Text = "Feel the Blaze...!!";
    this.openFileDialogMod.DefaultExt = "fmp";
    this.openFileDialogMod.Filter = "Fable Mod Packages (*.fmp)|*.fmp|All Files (*.*)|*.*||";
    this.timerTip.Tick += new EventHandler(this.timerTip_Tick);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(1024, 768);
    this.Controls.Add((System.Windows.Forms.Control) this.splitterMain);
    this.Controls.Add((System.Windows.Forms.Control) this.panelLeft);
    this.Controls.Add((System.Windows.Forms.Control) this.menuStripMain);
    this.Controls.Add((System.Windows.Forms.Control) this.statusStrip);
    this.Icon = null; // (Icon) componentResourceManager.GetObject("$this.Icon");
    this.IsMdiContainer = true;
    this.MainMenuStrip = this.menuStripMain;
    this.Name = nameof (FormMain);
    this.RegistryKey = "SOFTWARE\\FableMod\\ChocolateBox";
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "ChocolateBox";
    this.Title = "ChocolateBox";
    this.Load += new EventHandler(this.FormMain_Load);
    this.Shown += new EventHandler(this.FormMain_Shown);
    this.FormClosing += new FormClosingEventHandler(this.FormMain_FormClosing);
    this.menuStripMain.ResumeLayout(false);
    this.menuStripMain.PerformLayout();
    this.panelLeft.ResumeLayout(false);
    this.contextMenuStripFile.ResumeLayout(false);
    this.statusStrip.ResumeLayout(false);
    this.statusStrip.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public static FormMain Instance => (FormMain) FormApp.Instance;

  public FormMain() => this.InitializeComponent();

  public void AddToFileList(string fileName)
  {
    if (this.listBoxFiles.InvokeRequired)
    {
      this.Invoke((Delegate) new FormMain.AddToFileListDelegate(this.AddToFileList), (object) fileName);
    }
    else
    {
      if (this.listBoxFiles.Items.IndexOf((object) fileName) >= 0)
        return;
      this.listBoxFiles.Items.Add((object) fileName);
    }
  }

  public void OnLoadFile(FileController c)
  {
    if (!c.UserAccess)
      return;
    this.AddToFileList(c.RelativeFileName);
  }

  private void SaveChanges()
  {
    List<Processor> processors = new List<Processor>();
    lock (this.myFileDB)
    {
      for (int index = 0; index < this.myFileDB.FileCount; ++index)
      {
        FileController fileAt = this.myFileDB.GetFileAt(index);
        if (fileAt.Modified)
          processors.Add((Processor) new FileProcessor(fileAt, ""));
      }
    }
    int num = (int) new FormProcess(processors).ShowDialog();
    processors.Clear();
  }

  private void LoadStartupFiles()
  {
    string filename = Settings.Directory + Settings.GetString("Settings", "StartupFile");
    List<Processor> processors = new List<Processor>();
    try
    {
      XmlDocument xmlDocument = new XmlDocument();
      xmlDocument.Load(filename);
      for (XmlNode xmlNode = xmlDocument["files"].FirstChild; xmlNode != null; xmlNode = xmlNode.NextSibling)
      {
        if (xmlNode.Name == "file")
        {
          XmlAttribute attribute1 = xmlNode.Attributes["name"];
          XmlAttribute attribute2 = xmlNode.Attributes["form"];
          if (attribute1 != null)
          {
            FileController c = this.myFileDB.Get(attribute1.InnerText);
            processors.Add((Processor) new FileProcessor(c, FileProcessorMode.LoadFile));
            if (bool.Parse(attribute2.InnerText))
              processors.Add((Processor) new FileProcessor(c, FileProcessorMode.LoadForm));
          }
        }
      }
    }
    catch (Exception ex)
    {
      int num = (int) this.ErrorMessage(ex.Message);
      this.Close();
    }
    FormProcess formProcess = new FormProcess(processors);
    int num1 = (int) formProcess.ShowDialog();
    formProcess.Dispose();
  }

  public void AddMDI(Form form)
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new FormMain.AddMDIDelegate(this.AddMDI), (object) form);
    }
    else
    {
      form.MdiParent = (Form) this;
      ThemeManager.ApplyTheme(form);
      form.FormClosed += (s, e) => { this.BeginInvoke(new Action(() => this.SmartLayoutMdi())); };
      form.Resize += (s, e) => { this.OnMdiChildResize(form); };
      form.Show();
      this.SmartLayoutMdi();
    }
  }

  private bool _isLayouting = false;
  private float _splitRatioV = 0.5f; // For 2 forms: left/right
  private float _splitRatioH = 0.5f; // For 3 forms: top/bottom
  private float _splitRatioVBottom = 0.5f; // For 3 forms: bottom-left/bottom-right

  private void OnMdiChildResize(Form child)
  {
      if (_isLayouting || child.WindowState != FormWindowState.Normal) return;

      // Use BeginInvoke to defer layout until after the resize operation is complete
      this.BeginInvoke(new Action(() => 
      {
          if (_isLayouting) return;

          Form[] children = Array.FindAll(this.MdiChildren, f => f.Visible && f.WindowState != FormWindowState.Minimized);
          if (children.Length < 2) return;

          Rectangle clientArea = Rectangle.Empty;
          foreach (System.Windows.Forms.Control ctl in this.Controls)
          {
              if (ctl is MdiClient) { clientArea = ctl.ClientRectangle; break; }
          }
          if (clientArea.IsEmpty) return;

          if (children.Length == 2)
          {
              if (child == children[0])
                  _splitRatioV = (float)child.Width / clientArea.Width;
              else
                  _splitRatioV = (float)(clientArea.Width - child.Width) / clientArea.Width;
          }
          else if (children.Length == 3)
          {
              if (child == children[0])
              {
                  _splitRatioH = (float)child.Height / clientArea.Height;
              }
              else
              {
                  _splitRatioH = (float)(clientArea.Height - child.Height) / clientArea.Height;
                  if (child == children[1])
                      _splitRatioVBottom = (float)child.Width / clientArea.Width;
                  else
                      _splitRatioVBottom = (float)(clientArea.Width - child.Width) / clientArea.Width;
              }
          }

          // Clamp ratios to prevent windows from disappearing
          _splitRatioV = Math.Max(0.1f, Math.Min(0.9f, _splitRatioV));
          _splitRatioH = Math.Max(0.1f, Math.Min(0.9f, _splitRatioH));
          _splitRatioVBottom = Math.Max(0.1f, Math.Min(0.9f, _splitRatioVBottom));

          SmartLayoutMdi();
      }));
  }

  private void SmartLayoutMdi()
  {
      if (_isLayouting) return;
      _isLayouting = true;
      try
      {
          Form[] children = Array.FindAll(this.MdiChildren, f => f.Visible && f.WindowState != FormWindowState.Minimized);
          if (children.Length == 0) return;

          Rectangle clientArea = Rectangle.Empty;
          foreach (System.Windows.Forms.Control ctl in this.Controls)
          {
              if (ctl is MdiClient) { clientArea = ctl.ClientRectangle; break; }
          }
          if (clientArea.IsEmpty) return;

          if (children.Length == 1)
          {
              children[0].WindowState = FormWindowState.Normal;
              children[0].Bounds = clientArea;
          }
          else if (children.Length == 2)
          {
              int splitX = (int)(clientArea.Width * _splitRatioV);
              children[0].WindowState = FormWindowState.Normal;
              children[0].Bounds = new Rectangle(0, 0, splitX, clientArea.Height);
              children[1].WindowState = FormWindowState.Normal;
              children[1].Bounds = new Rectangle(splitX, 0, clientArea.Width - splitX, clientArea.Height);
          }
          else if (children.Length == 3)
          {
              int splitY = (int)(clientArea.Height * _splitRatioH);
              int splitXBottom = (int)(clientArea.Width * _splitRatioVBottom);
              
              children[0].WindowState = FormWindowState.Normal;
              children[0].Bounds = new Rectangle(0, 0, clientArea.Width, splitY);
              
              children[1].WindowState = FormWindowState.Normal;
              children[1].Bounds = new Rectangle(0, splitY, splitXBottom, clientArea.Height - splitY);
              children[2].WindowState = FormWindowState.Normal;
              children[2].Bounds = new Rectangle(splitXBottom, splitY, clientArea.Width - splitXBottom, clientArea.Height - splitY);
          }
          else
          {
              this.LayoutMdi(MdiLayout.TileVertical);
          }
      }
      finally
      {
          _isLayouting = false;
      }
  }

  private FileController LoadFile(string fileName)
  {
    List<Processor> processors = new List<Processor>();
    FileController fileController = this.myFileDB.Get(fileName);
    fileController.LoadProcess(processors, true);
    FormProcess formProcess = new FormProcess(processors);
    int num = (int) formProcess.ShowDialog();
    formProcess.Dispose();
    return fileController;
  }

  private void listBoxFiles_DoubleClick(object sender, EventArgs e)
  {
    int selectedIndex = this.listBoxFiles.SelectedIndex;
    if (selectedIndex < 0)
      return;
    this.LoadFile(this.listBoxFiles.Items[selectedIndex].ToString());
  }

  private void loadToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.openFileDialog.InitialDirectory = Settings.FableDirectory;
    if (this.openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    List<Processor> processors = new List<Processor>();
    for (int index = 0; index < this.openFileDialog.FileNames.Length; ++index)
    {
      FileController fileController = this.myFileDB.Get(this.openFileDialog.FileNames[index]);
      if (fileController == null)
      {
        int num = (int) this.ErrorMessage("Unknown file format");
        return;
      }
      fileController.LoadProcess(processors);
    }
    FormProcess formProcess = new FormProcess(processors);
    int num1 = (int) formProcess.ShowDialog();
    formProcess.Dispose();
  }

  private void LoadUIDs()
  {
    string str = Settings.GetString("Settings", "UIDFile");
    if (string.IsNullOrEmpty(str))
      return;
    string fileName = FormApp.Folder + str;
    try
    {
      UIDManager.LoadFromFile(fileName);
    }
    catch (IOException ex)
    {
      this.GenerateUIDDatabase();
    }
  }

  private void FormMain_Load(object sender, EventArgs e)
  {
    try
    {
      Settings.Load();
      this.myDefinitionDB.Load(FormApp.Folder + Settings.GetString("Settings", "DefinitionsFile"));
      DefinitionDB.EnableDeveloperMode(Settings.GetBool("DefTypes", "DevModeOn", false));
      this.myTNGDefinitions.Load(FormApp.Folder + Settings.GetString("Settings", "TNGDefinitionsFile"));
      this.myFileDB = new FileDatabase((FileInterface) this, this.myDefinitionDB, this.myTNGDefinitions);
      
      if (!new ModEnvironmentManager().Check())
      {
        int num = (int) this.ErrorMessage("Modding environment is not acceptable.\r\nApplication will now exit.");
        this.Close();
        return;
      }
      
      this.LoadStartupFiles();
      GfxManager.Initialize((Form) this, Settings.GetInt("Resolution", "Width", 1024 /*0x0400*/), Settings.GetInt("Resolution", "Height", 768 /*0x0300*/));
      GfxManager.SetDirectory(FormApp.Folder);
      this.LoadUIDs();
      this.newModPackageToolStripMenuItem.Enabled = Settings.GetBool("Settings", "ModPackages", false);
      this.loadModPackageToolStripMenuItem.Enabled = this.newModPackageToolStripMenuItem.Enabled;
      this.completeUIDCheckToolStripMenuItem.Enabled = Settings.GetBool("Settings", "UIDCheck", false);
      ThemeManager.ApplyTheme(this);
      this.timerTip.Enabled = true;
    }
    catch (Exception ex)
    {
      int num = (int) this.ErrorMessage($"{ex.Message}\r\n{ex.ToString()}");
      this.Close();
    }
  }

  private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
  {
    try
    {
      if (this.myFileDB != null)
        this.SaveUIDDatabase();
      bool flag = false;
      if (this.myFileDB != null)
      {
        lock (this.myFileDB)
        {
          for (int index = 0; index < this.myFileDB.FileCount; ++index)
          {
            if (this.myFileDB.GetFileAt(index).Modified)
            {
              flag = true;
              break;
            }
          }
        }
      }
      if (flag)
      {
        switch (MessageBox.Show("Save changes?\r\nWarning! You should always have backups of the original files.", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
        {
          case DialogResult.Cancel:
            e.Cancel = true;
            return;
          case DialogResult.Yes:
            this.SaveChanges();
            break;
        }
      }
      GfxManager.Destroy();
    }
    catch (Exception ex)
    {
      int num = (int) this.ErrorMessage($"{ex.Message}\r\n{ex.ToString()}");
      this.Close();
    }
  }

  private void exitToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

  private void editorToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.AddMDI((Form) new FormEditor());
  }

  public void ShowFileList(bool show)
  {
    this.panelLeft.Visible = show;
    this.splitterMain.Visible = show;
  }

  private void fileListToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.fileListToolStripMenuItem.Checked = !this.fileListToolStripMenuItem.Checked;
    this.ShowFileList(this.fileListToolStripMenuItem.Checked);
  }

  public void CloseChildren(Type exclude)
  {
    for (int index = 0; index < this.MdiChildren.Length; ++index)
    {
      if (this.MdiChildren[index].GetType() == typeof (FormEditor))
      {
        ((FormEditor) this.MdiChildren[index]).SaveChanges(true);
        break;
      }
    }
    int index1 = 0;
    while (index1 < this.MdiChildren.Length)
    {
      if (this.MdiChildren[index1].GetType() != exclude)
        this.MdiChildren[index1].Close();
      else
        ++index1;
    }
  }

  private void saveAllAndRunFableToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (Process.GetProcessesByName("Fable").Length != 0)
    {
      int num = (int) this.ErrorMessage("Fable is still running.");
    }
    else
    {
      for (int index = 0; index < this.MdiChildren.Length; ++index)
      {
        if (this.MdiChildren[index].GetType() == typeof (FormEditor))
        {
          ((FormEditor) this.MdiChildren[index]).SaveChanges(true);
          break;
        }
      }
      while (this.MdiChildren.Length > 0)
        this.MdiChildren[0].Close();
      this.SaveChanges();
      this.myFileDB.CloseFiles();
      Directory.SetCurrentDirectory(Settings.FableDirectory);
      GfxManager.Destroy();
      Process.Start(Settings.FableDirectory + "fable.exe");
      GfxManager.Initialize((Form) this, Settings.GetInt("Resolution", "Width", 1024 /*0x0400*/), Settings.GetInt("Resolution", "Height", 768 /*0x0300*/));
    }
  }

  private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
  {
    FormAbout formAbout = new FormAbout();
    int num = (int) formAbout.ShowDialog((IWin32Window) this);
    formAbout.Dispose();
  }

  private void newModPackageToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.AddMDI((Form) new FormModPackage());
  }

  private void loadModPackageToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (this.openFileDialogMod.ShowDialog() != DialogResult.OK)
      return;
    try
    {
      this.AddMDI((Form) new FormModPackage(this.openFileDialogMod.FileName));
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Failed to load FMP: " + ex.Message);
    }
  }

  private void FormMain_Shown(object sender, EventArgs e)
  {
    this.timerTip.Enabled = Settings.GetBool("Settings", "Tips", true);
  }

  private void timerTip_Tick(object sender, EventArgs e)
  {
    this.timerTip.Enabled = false;
    FormDayTip formDayTip = new FormDayTip();
    formDayTip.Owner = (Form) this;
    int num = (int) formDayTip.ShowDialog();
    formDayTip.Dispose();
  }

  private void SaveUIDDatabase()
  {
    string str = Settings.GetString("Settings", "UIDFile");
    if (string.IsNullOrEmpty(str))
      return;
    UIDManager.WriteToFile(FormApp.Folder + str);
  }

  private void GenerateUIDDatabase()
  {
    FormProcess formProcess = new FormProcess((Processor) new UIDDatabaseProcessor());
    int num = (int) formProcess.ShowDialog();
    formProcess.Dispose();
    this.SaveUIDDatabase();
  }

  private void generateUIDDatabaseToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show("Warning! Are you sure you want to do this?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    this.GenerateUIDDatabase();
  }

  private void completeUIDCheckToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (MessageBox.Show("Warning! Are you sure you want to do this?", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    UIDCheckProcessor uidCheckProcessor = new UIDCheckProcessor();
    FormProcess formProcess = new FormProcess((Processor) uidCheckProcessor);
    int num1 = (int) formProcess.ShowDialog();
    formProcess.Dispose();
    int num2 = (int) this.InfoMessage(string.Format("Changed {0} object(s) and saved {0} file(s).", (object) uidCheckProcessor.ChangedCount, (object) uidCheckProcessor.SavedCount));
  }

  private void openToolStripMenuItem_Click(object sender, EventArgs e)
  {
    int selectedIndex = this.listBoxFiles.SelectedIndex;
    if (selectedIndex < 0)
      return;
    this.LoadFile(this.listBoxFiles.Items[selectedIndex].ToString());
  }

  private void backupToolStripMenuItem_Click(object sender, EventArgs e)
  {
    int selectedIndex = this.listBoxFiles.SelectedIndex;
    if (selectedIndex < 0)
      return;
    string str1 = this.listBoxFiles.Items[selectedIndex].ToString();
    string str2 = $"{Settings.FableDirectory}Backups\\{str1}";
    if (File.Exists(str2) && MessageBox.Show("Backup already exists. Overwrite?", "ChocolateBox", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    Directory.CreateDirectory(Path.GetDirectoryName(str2));
    string text = this.statusStrip.Items[0].Text;
    this.statusStrip.Items[0].Text = $"Backing up {str1}, please wait...";
    this.statusStrip.Update();
    File.Copy(Settings.FableDirectory + str1, str2, true);
    this.statusStrip.Items[0].Text = text;
    int num = (int) this.InfoMessage($"Backup done for {str1}.");
  }

  private void rollBackToolStripMenuItem_Click(object sender, EventArgs e)
  {
    string str = this.listBoxFiles.Items[this.listBoxFiles.SelectedIndex].ToString();
    string sourceFileName = $"{Settings.FableDirectory}Backups\\{str}";
    if (MessageBox.Show("Are you sure you want to rollback?", "ChocolateBox", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) != DialogResult.Yes)
      return;
    string text = this.statusStrip.Items[0].Text;
    this.statusStrip.Items[0].Text = $"Rolling back {str}, please wait...";
    this.statusStrip.Update();
    File.Copy(sourceFileName, Settings.FableDirectory + str, true);
    this.statusStrip.Items[0].Text = text;
    int num = (int) this.InfoMessage($"Rollback done for {str}.");
  }

  private void itemBuilderToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.AddMDI((Form) new FormObjectBuilder());
  }

  private delegate void AddToFileListDelegate(string fileName);

  private delegate void AddMDIDelegate(Form form);
}
