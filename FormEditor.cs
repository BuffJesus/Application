// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormEditor
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIN;
using FableMod.Content.Forms;
using FableMod.ContentManagement;
using FableMod.Forms;
using FableMod.Gfx.Integration;
using FableMod.LEV;
using FableMod.STB;
using FableMod.TNG;
using FableMod.WLD;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormEditor : Form
{
  private IContainer components;
  private MenuStrip menuStripMain;
  private ToolStripMenuItem fileToolStripMenuItem;
  private ToolStripMenuItem loadRegionToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem1;
  private ToolStripMenuItem closeToolStripMenuItem;
  private Panel panelLeft;
  private Panel panelThing;
  private Splitter splitterMain;
  private Panel panelView;
  private ToolStripMenuItem viewToolStripMenuItem;
  private ToolStripMenuItem cameraToolStripMenuItem;
  private ToolStripMenuItem topToolStripMenuItem;
  private ToolStripMenuItem frontToolStripMenuItem;
  private ToolStripMenuItem rightToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem2;
  private ToolStripMenuItem showDirectionAxesToolStripMenuItem;
  private Panel panelChange;
  private ComboBox comboBoxSection;
  private ComboBox comboBoxMap;
  private Button buttonChangeSection;
  private Button buttonChangeMap;
  private Label label2;
  private Label label1;
  private ToolStripMenuItem thingsToolStripMenuItem;
  private ToolStripMenuItem newToolStripMenuItem;
  private ToolStripMenuItem importToolStripMenuItem;
  private ToolStripMenuItem saveRegionToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem3;
  private ToolStrip toolStripMain;
  private ToolStripButton toolStripButtonWorld;
  private ToolStripButton toolStripButtonWalk;
  private ToolStripButton toolStripButtonNav;
  private Panel panelNormalMode;
  private Panel panelNavMode;
  private RadioButton radioButtonDynamic;
  private RadioButton radioButtonNav;
  private GroupBox groupBoxNavMode;
  private Panel panelWalkNav;
  private NumericUpDown numericUpDownBrush;
  private Label label3;
  private ComboBox comboBoxEditMap;
  private Label label4;
  private Panel panelEditNav;
  private Label label6;
  private Label label5;
  private NumericUpDown numericUpDownSubset;
  private ComboBox comboBoxEditSection;
  private ToolStripSeparator toolStripMenuItem4;
  private ToolStripMenuItem editorToolsToolStripMenuItem;
  private Panel panelGeneral;
  private Label label7;
  private TrackBar trackBarDrawDistance;
  private Button buttonAddSubset;
  private Button buttonNewSection;
  private Button buttonResetNav;
  private ControlThing controlThing;
  private ToolStripMenuItem fullScreenToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem5;
  private ToolStripMenuItem saveScreenshotAsToolStripMenuItem;
  private ToolStripMenuItem teleporterToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem6;
  private ToolStripMenuItem editToolStripMenuItem;
  private ToolStripMenuItem findByUIDToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem7;
  private ToolStripMenuItem unfreezeAllToolStripMenuItem;
  private ToolStripSeparator toolStripMenuItem8;
  private ToolStripMenuItem markersToolStripMenuItem;
  private ToolStripMenuItem creaturesToolStripMenuItem;
  private ToolStripMenuItem objectsToolStripMenuItem;
  private GfxThingController myController;
  private GfxThingView myView;
  private Collection<ThingMap> myMaps;
  private Thing myCreatedThing;
  private List<Thing> myImports = new List<Thing>();
  private bool myUpdateBlock;
  private Thing[] mySelectedThings;
  private string myLastNewEntry = "";
  private Thing myTeleporter;
  private ThingMap myTeleporterMap;
  private string myRegion = "";
  private Variable myUIDVariable;
  private FormEditor.CreateMode myCreateMode;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormEditor));
    this.menuStripMain = new MenuStrip();
    this.fileToolStripMenuItem = new ToolStripMenuItem();
    this.loadRegionToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem1 = new ToolStripSeparator();
    this.saveRegionToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem5 = new ToolStripSeparator();
    this.saveScreenshotAsToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem3 = new ToolStripSeparator();
    this.closeToolStripMenuItem = new ToolStripMenuItem();
    this.editToolStripMenuItem = new ToolStripMenuItem();
    this.findByUIDToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem7 = new ToolStripSeparator();
    this.unfreezeAllToolStripMenuItem = new ToolStripMenuItem();
    this.viewToolStripMenuItem = new ToolStripMenuItem();
    this.cameraToolStripMenuItem = new ToolStripMenuItem();
    this.topToolStripMenuItem = new ToolStripMenuItem();
    this.frontToolStripMenuItem = new ToolStripMenuItem();
    this.rightToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem2 = new ToolStripSeparator();
    this.showDirectionAxesToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem4 = new ToolStripSeparator();
    this.editorToolsToolStripMenuItem = new ToolStripMenuItem();
    this.fullScreenToolStripMenuItem = new ToolStripMenuItem();
    this.thingsToolStripMenuItem = new ToolStripMenuItem();
    this.newToolStripMenuItem = new ToolStripMenuItem();
    this.teleporterToolStripMenuItem = new ToolStripMenuItem();
    this.toolStripMenuItem6 = new ToolStripSeparator();
    this.importToolStripMenuItem = new ToolStripMenuItem();
    this.panelLeft = new Panel();
    this.panelNormalMode = new Panel();
    this.panelThing = new Panel();
    this.panelChange = new Panel();
    this.buttonChangeSection = new Button();
    this.buttonChangeMap = new Button();
    this.label2 = new Label();
    this.label1 = new Label();
    this.comboBoxSection = new ComboBox();
    this.comboBoxMap = new ComboBox();
    this.panelEditNav = new Panel();
    this.buttonAddSubset = new Button();
    this.buttonNewSection = new Button();
    this.buttonResetNav = new Button();
    this.label6 = new Label();
    this.label5 = new Label();
    this.numericUpDownSubset = new NumericUpDown();
    this.comboBoxEditSection = new ComboBox();
    this.panelWalkNav = new Panel();
    this.numericUpDownBrush = new NumericUpDown();
    this.label3 = new Label();
    this.comboBoxEditMap = new ComboBox();
    this.label4 = new Label();
    this.panelNavMode = new Panel();
    this.groupBoxNavMode = new GroupBox();
    this.radioButtonNav = new RadioButton();
    this.radioButtonDynamic = new RadioButton();
    this.panelGeneral = new Panel();
    this.label7 = new Label();
    this.trackBarDrawDistance = new TrackBar();
    this.splitterMain = new Splitter();
    this.panelView = new Panel();
    this.toolStripMain = new ToolStrip();
    this.toolStripButtonWorld = new ToolStripButton();
    this.toolStripButtonWalk = new ToolStripButton();
    this.toolStripButtonNav = new ToolStripButton();
    this.toolStripMenuItem8 = new ToolStripSeparator();
    this.markersToolStripMenuItem = new ToolStripMenuItem();
    this.creaturesToolStripMenuItem = new ToolStripMenuItem();
    this.controlThing = new ControlThing();
    this.objectsToolStripMenuItem = new ToolStripMenuItem();
    this.menuStripMain.SuspendLayout();
    this.panelLeft.SuspendLayout();
    this.panelNormalMode.SuspendLayout();
    this.panelThing.SuspendLayout();
    this.panelChange.SuspendLayout();
    this.panelEditNav.SuspendLayout();
    this.numericUpDownSubset.BeginInit();
    this.panelWalkNav.SuspendLayout();
    this.numericUpDownBrush.BeginInit();
    this.panelNavMode.SuspendLayout();
    this.groupBoxNavMode.SuspendLayout();
    this.panelGeneral.SuspendLayout();
    this.trackBarDrawDistance.BeginInit();
    this.toolStripMain.SuspendLayout();
    this.SuspendLayout();
    this.menuStripMain.AllowMerge = false;
    this.menuStripMain.Items.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.fileToolStripMenuItem,
      (ToolStripItem) this.editToolStripMenuItem,
      (ToolStripItem) this.viewToolStripMenuItem,
      (ToolStripItem) this.thingsToolStripMenuItem
    });
    this.menuStripMain.Location = new Point(0, 0);
    this.menuStripMain.Name = "menuStripMain";
    this.menuStripMain.RenderMode = ToolStripRenderMode.System;
    this.menuStripMain.Size = new Size(676, 24);
    this.menuStripMain.TabIndex = 2;
    this.menuStripMain.Text = "menuStrip1";
    this.fileToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[7]
    {
      (ToolStripItem) this.loadRegionToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem1,
      (ToolStripItem) this.saveRegionToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem5,
      (ToolStripItem) this.saveScreenshotAsToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem3,
      (ToolStripItem) this.closeToolStripMenuItem
    });
    this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
    this.fileToolStripMenuItem.Size = new Size(35, 20);
    this.fileToolStripMenuItem.Text = "&File";
    this.fileToolStripMenuItem.Paint += new PaintEventHandler(this.fileToolStripMenuItem_Paint);
    this.loadRegionToolStripMenuItem.Name = "loadRegionToolStripMenuItem";
    this.loadRegionToolStripMenuItem.Size = new Size(178, 22);
    this.loadRegionToolStripMenuItem.Text = "&Load Region...";
    this.loadRegionToolStripMenuItem.Click += new EventHandler(this.loadRegionToolStripMenuItem_Click);
    this.toolStripMenuItem1.Name = "toolStripMenuItem1";
    this.toolStripMenuItem1.Size = new Size(175, 6);
    this.saveRegionToolStripMenuItem.Enabled = false;
    this.saveRegionToolStripMenuItem.Name = "saveRegionToolStripMenuItem";
    this.saveRegionToolStripMenuItem.ShortcutKeys = Keys.S | Keys.Control;
    this.saveRegionToolStripMenuItem.Size = new Size(178, 22);
    this.saveRegionToolStripMenuItem.Text = "Save Region";
    this.saveRegionToolStripMenuItem.Click += new EventHandler(this.saveRegionToolStripMenuItem_Click);
    this.toolStripMenuItem5.Name = "toolStripMenuItem5";
    this.toolStripMenuItem5.Size = new Size(175, 6);
    this.saveScreenshotAsToolStripMenuItem.Enabled = false;
    this.saveScreenshotAsToolStripMenuItem.Name = "saveScreenshotAsToolStripMenuItem";
    this.saveScreenshotAsToolStripMenuItem.Size = new Size(178, 22);
    this.saveScreenshotAsToolStripMenuItem.Text = "Save Screenshot As..";
    this.saveScreenshotAsToolStripMenuItem.Click += new EventHandler(this.saveScreenshotAsToolStripMenuItem_Click);
    this.toolStripMenuItem3.Name = "toolStripMenuItem3";
    this.toolStripMenuItem3.Size = new Size(175, 6);
    this.closeToolStripMenuItem.Name = "closeToolStripMenuItem";
    this.closeToolStripMenuItem.Size = new Size(178, 22);
    this.closeToolStripMenuItem.Text = "&Close";
    this.closeToolStripMenuItem.Click += new EventHandler(this.closeToolStripMenuItem_Click);
    this.editToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.findByUIDToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem7,
      (ToolStripItem) this.unfreezeAllToolStripMenuItem
    });
    this.editToolStripMenuItem.Enabled = false;
    this.editToolStripMenuItem.Name = "editToolStripMenuItem";
    this.editToolStripMenuItem.Size = new Size(37, 20);
    this.editToolStripMenuItem.Text = "&Edit";
    this.findByUIDToolStripMenuItem.Name = "findByUIDToolStripMenuItem";
    this.findByUIDToolStripMenuItem.Size = new Size(132, 22);
    this.findByUIDToolStripMenuItem.Text = "Find by UID";
    this.findByUIDToolStripMenuItem.Click += new EventHandler(this.findByUIDToolStripMenuItem_Click);
    this.toolStripMenuItem7.Name = "toolStripMenuItem7";
    this.toolStripMenuItem7.Size = new Size(129, 6);
    this.unfreezeAllToolStripMenuItem.Name = "unfreezeAllToolStripMenuItem";
    this.unfreezeAllToolStripMenuItem.Size = new Size(132, 22);
    this.unfreezeAllToolStripMenuItem.Text = "Unfreeze All";
    this.unfreezeAllToolStripMenuItem.Click += new EventHandler(this.unfreezeAllToolStripMenuItem_Click);
    this.viewToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[10]
    {
      (ToolStripItem) this.cameraToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem2,
      (ToolStripItem) this.showDirectionAxesToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem4,
      (ToolStripItem) this.editorToolsToolStripMenuItem,
      (ToolStripItem) this.fullScreenToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem8,
      (ToolStripItem) this.markersToolStripMenuItem,
      (ToolStripItem) this.creaturesToolStripMenuItem,
      (ToolStripItem) this.objectsToolStripMenuItem
    });
    this.viewToolStripMenuItem.Enabled = false;
    this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
    this.viewToolStripMenuItem.Size = new Size(41, 20);
    this.viewToolStripMenuItem.Text = "&View";
    this.viewToolStripMenuItem.Paint += new PaintEventHandler(this.viewToolStripMenuItem_Paint);
    this.cameraToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.topToolStripMenuItem,
      (ToolStripItem) this.frontToolStripMenuItem,
      (ToolStripItem) this.rightToolStripMenuItem
    });
    this.cameraToolStripMenuItem.Name = "cameraToolStripMenuItem";
    this.cameraToolStripMenuItem.Size = new Size(172, 22);
    this.cameraToolStripMenuItem.Text = "Camera";
    this.topToolStripMenuItem.Name = "topToolStripMenuItem";
    this.topToolStripMenuItem.Size = new Size(100, 22);
    this.topToolStripMenuItem.Text = "&Top";
    this.topToolStripMenuItem.Click += new EventHandler(this.topToolStripMenuItem_Click);
    this.frontToolStripMenuItem.Name = "frontToolStripMenuItem";
    this.frontToolStripMenuItem.Size = new Size(100, 22);
    this.frontToolStripMenuItem.Text = "&Front";
    this.frontToolStripMenuItem.Click += new EventHandler(this.frontToolStripMenuItem_Click);
    this.rightToolStripMenuItem.Name = "rightToolStripMenuItem";
    this.rightToolStripMenuItem.Size = new Size(100, 22);
    this.rightToolStripMenuItem.Text = "&Right";
    this.rightToolStripMenuItem.Click += new EventHandler(this.rightToolStripMenuItem_Click);
    this.toolStripMenuItem2.Name = "toolStripMenuItem2";
    this.toolStripMenuItem2.Size = new Size(169, 6);
    this.showDirectionAxesToolStripMenuItem.Name = "showDirectionAxesToolStripMenuItem";
    this.showDirectionAxesToolStripMenuItem.Size = new Size(172, 22);
    this.showDirectionAxesToolStripMenuItem.Text = "Show Direction Axes";
    this.showDirectionAxesToolStripMenuItem.Click += new EventHandler(this.showDirectionAxesToolStripMenuItem_Click);
    this.toolStripMenuItem4.Name = "toolStripMenuItem4";
    this.toolStripMenuItem4.Size = new Size(169, 6);
    this.editorToolsToolStripMenuItem.Checked = true;
    this.editorToolsToolStripMenuItem.CheckState = CheckState.Checked;
    this.editorToolsToolStripMenuItem.Name = "editorToolsToolStripMenuItem";
    this.editorToolsToolStripMenuItem.Size = new Size(172, 22);
    this.editorToolsToolStripMenuItem.Text = "Editor Tools";
    this.editorToolsToolStripMenuItem.Click += new EventHandler(this.editorToolsToolStripMenuItem_Click);
    this.fullScreenToolStripMenuItem.Name = "fullScreenToolStripMenuItem";
    this.fullScreenToolStripMenuItem.ShortcutKeys = Keys.F11;
    this.fullScreenToolStripMenuItem.Size = new Size(172, 22);
    this.fullScreenToolStripMenuItem.Text = "Full Screen";
    this.fullScreenToolStripMenuItem.Click += new EventHandler(this.fullScreenToolStripMenuItem_Click);
    this.thingsToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[4]
    {
      (ToolStripItem) this.newToolStripMenuItem,
      (ToolStripItem) this.teleporterToolStripMenuItem,
      (ToolStripItem) this.toolStripMenuItem6,
      (ToolStripItem) this.importToolStripMenuItem
    });
    this.thingsToolStripMenuItem.Enabled = false;
    this.thingsToolStripMenuItem.Name = "thingsToolStripMenuItem";
    this.thingsToolStripMenuItem.Size = new Size(50, 20);
    this.thingsToolStripMenuItem.Text = "Things";
    this.newToolStripMenuItem.Name = "newToolStripMenuItem";
    this.newToolStripMenuItem.Size = new Size(148, 22);
    this.newToolStripMenuItem.Text = "&New";
    this.newToolStripMenuItem.Click += new EventHandler(this.newToolStripMenuItem_Click);
    this.teleporterToolStripMenuItem.Name = "teleporterToolStripMenuItem";
    this.teleporterToolStripMenuItem.Size = new Size(148, 22);
    this.teleporterToolStripMenuItem.Text = "New Teleporter";
    this.teleporterToolStripMenuItem.ToolTipText = "Create a new teleporter";
    this.teleporterToolStripMenuItem.Click += new EventHandler(this.teleporterToolStripMenuItem_Click);
    this.toolStripMenuItem6.Name = "toolStripMenuItem6";
    this.toolStripMenuItem6.Size = new Size(145, 6);
    this.importToolStripMenuItem.Name = "importToolStripMenuItem";
    this.importToolStripMenuItem.Size = new Size(148, 22);
    this.importToolStripMenuItem.Text = "Import...";
    this.importToolStripMenuItem.ToolTipText = "Import a separate TNG file";
    this.importToolStripMenuItem.Click += new EventHandler(this.importToolStripMenuItem_Click);
    this.panelLeft.Controls.Add((System.Windows.Forms.Control) this.panelNormalMode);
    this.panelLeft.Controls.Add((System.Windows.Forms.Control) this.panelEditNav);
    this.panelLeft.Controls.Add((System.Windows.Forms.Control) this.panelWalkNav);
    this.panelLeft.Controls.Add((System.Windows.Forms.Control) this.panelNavMode);
    this.panelLeft.Controls.Add((System.Windows.Forms.Control) this.panelGeneral);
    this.panelLeft.Dock = DockStyle.Left;
    this.panelLeft.Location = new Point(0, 24);
    this.panelLeft.Name = "panelLeft";
    this.panelLeft.Size = new Size(323, 420);
    this.panelLeft.TabIndex = 3;
    this.panelNormalMode.Controls.Add((System.Windows.Forms.Control) this.panelThing);
    this.panelNormalMode.Controls.Add((System.Windows.Forms.Control) this.panelChange);
    this.panelNormalMode.Dock = DockStyle.Fill;
    this.panelNormalMode.Location = new Point(0, 288);
    this.panelNormalMode.Name = "panelNormalMode";
    this.panelNormalMode.Size = new Size(323, 132);
    this.panelNormalMode.TabIndex = 4;
    this.panelNormalMode.Visible = false;
    this.panelThing.BorderStyle = BorderStyle.Fixed3D;
    this.panelThing.Controls.Add((System.Windows.Forms.Control) this.controlThing);
    this.panelThing.Dock = DockStyle.Fill;
    this.panelThing.Location = new Point(0, 72);
    this.panelThing.Name = "panelThing";
    this.panelThing.Padding = new Padding(4);
    this.panelThing.Size = new Size(323, 60);
    this.panelThing.TabIndex = 2;
    this.panelChange.Controls.Add((System.Windows.Forms.Control) this.buttonChangeSection);
    this.panelChange.Controls.Add((System.Windows.Forms.Control) this.buttonChangeMap);
    this.panelChange.Controls.Add((System.Windows.Forms.Control) this.label2);
    this.panelChange.Controls.Add((System.Windows.Forms.Control) this.label1);
    this.panelChange.Controls.Add((System.Windows.Forms.Control) this.comboBoxSection);
    this.panelChange.Controls.Add((System.Windows.Forms.Control) this.comboBoxMap);
    this.panelChange.Dock = DockStyle.Top;
    this.panelChange.Location = new Point(0, 0);
    this.panelChange.Name = "panelChange";
    this.panelChange.Size = new Size(323, 72);
    this.panelChange.TabIndex = 3;
    this.buttonChangeSection.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonChangeSection.Enabled = false;
    this.buttonChangeSection.Location = new Point(252, 40);
    this.buttonChangeSection.Name = "buttonChangeSection";
    this.buttonChangeSection.Size = new Size(65, 23);
    this.buttonChangeSection.TabIndex = 4;
    this.buttonChangeSection.Text = "Change";
    this.buttonChangeSection.UseVisualStyleBackColor = true;
    this.buttonChangeSection.Click += new EventHandler(this.buttonChangeSection_Click);
    this.buttonChangeMap.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonChangeMap.Enabled = false;
    this.buttonChangeMap.Location = new Point(252, 13);
    this.buttonChangeMap.Name = "buttonChangeMap";
    this.buttonChangeMap.Size = new Size(65, 23);
    this.buttonChangeMap.TabIndex = 4;
    this.buttonChangeMap.Text = "Change";
    this.buttonChangeMap.UseVisualStyleBackColor = true;
    this.buttonChangeMap.Click += new EventHandler(this.buttonChangeMap_Click);
    this.label2.AutoSize = true;
    this.label2.Location = new Point(9, 45);
    this.label2.Name = "label2";
    this.label2.Size = new Size(46, 13);
    this.label2.TabIndex = 3;
    this.label2.Text = "Section:";
    this.label1.AutoSize = true;
    this.label1.Location = new Point(9, 18);
    this.label1.Name = "label1";
    this.label1.Size = new Size(31 /*0x1F*/, 13);
    this.label1.TabIndex = 2;
    this.label1.Text = "Map:";
    this.comboBoxSection.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.comboBoxSection.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBoxSection.Enabled = false;
    this.comboBoxSection.FormattingEnabled = true;
    this.comboBoxSection.Location = new Point(75, 42);
    this.comboBoxSection.Name = "comboBoxSection";
    this.comboBoxSection.Size = new Size(171, 21);
    this.comboBoxSection.Sorted = true;
    this.comboBoxSection.TabIndex = 1;
    this.comboBoxMap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.comboBoxMap.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBoxMap.Enabled = false;
    this.comboBoxMap.FormattingEnabled = true;
    this.comboBoxMap.Location = new Point(75, 15);
    this.comboBoxMap.Name = "comboBoxMap";
    this.comboBoxMap.Size = new Size(171, 21);
    this.comboBoxMap.Sorted = true;
    this.comboBoxMap.TabIndex = 0;
    this.panelEditNav.Controls.Add((System.Windows.Forms.Control) this.buttonAddSubset);
    this.panelEditNav.Controls.Add((System.Windows.Forms.Control) this.buttonNewSection);
    this.panelEditNav.Controls.Add((System.Windows.Forms.Control) this.buttonResetNav);
    this.panelEditNav.Controls.Add((System.Windows.Forms.Control) this.label6);
    this.panelEditNav.Controls.Add((System.Windows.Forms.Control) this.label5);
    this.panelEditNav.Controls.Add((System.Windows.Forms.Control) this.numericUpDownSubset);
    this.panelEditNav.Controls.Add((System.Windows.Forms.Control) this.comboBoxEditSection);
    this.panelEditNav.Dock = DockStyle.Top;
    this.panelEditNav.Location = new Point(0, 178);
    this.panelEditNav.Name = "panelEditNav";
    this.panelEditNav.Size = new Size(323, 110);
    this.panelEditNav.TabIndex = 7;
    this.panelEditNav.Visible = false;
    this.buttonAddSubset.Location = new Point(122, 37);
    this.buttonAddSubset.Name = "buttonAddSubset";
    this.buttonAddSubset.Size = new Size(52, 20);
    this.buttonAddSubset.TabIndex = 13;
    this.buttonAddSubset.Text = "Add";
    this.buttonAddSubset.UseVisualStyleBackColor = true;
    this.buttonAddSubset.Click += new EventHandler(this.buttonAddSubset_Click);
    this.buttonNewSection.Location = new Point(93, 81);
    this.buttonNewSection.Name = "buttonNewSection";
    this.buttonNewSection.Size = new Size(90, 23);
    this.buttonNewSection.TabIndex = 12;
    this.buttonNewSection.Text = "New Section";
    this.buttonNewSection.UseVisualStyleBackColor = true;
    this.buttonNewSection.Click += new EventHandler(this.buttonNewSection_Click);
    this.buttonResetNav.Location = new Point(12, 81);
    this.buttonResetNav.Name = "buttonResetNav";
    this.buttonResetNav.Size = new Size(75, 23);
    this.buttonResetNav.TabIndex = 11;
    this.buttonResetNav.Text = "Reset";
    this.buttonResetNav.UseVisualStyleBackColor = true;
    this.buttonResetNav.Click += new EventHandler(this.buttonResetNav_Click);
    this.label6.AutoSize = true;
    this.label6.Location = new Point(9, 39);
    this.label6.Name = "label6";
    this.label6.Size = new Size(43, 13);
    this.label6.TabIndex = 10;
    this.label6.Text = "Subset:";
    this.label5.AutoSize = true;
    this.label5.Location = new Point(9, 13);
    this.label5.Name = "label5";
    this.label5.Size = new Size(46, 13);
    this.label5.TabIndex = 9;
    this.label5.Text = "Section:";
    this.numericUpDownSubset.Location = new Point(77, 37);
    this.numericUpDownSubset.Maximum = new Decimal(new int[4]
    {
      6,
      0,
      0,
      0
    });
    this.numericUpDownSubset.Name = "numericUpDownSubset";
    this.numericUpDownSubset.Size = new Size(39, 20);
    this.numericUpDownSubset.TabIndex = 8;
    this.numericUpDownSubset.ValueChanged += new EventHandler(this.numericUpDownSubset_ValueChanged);
    this.comboBoxEditSection.FormattingEnabled = true;
    this.comboBoxEditSection.Location = new Point(77, 10);
    this.comboBoxEditSection.Name = "comboBoxEditSection";
    this.comboBoxEditSection.Size = new Size(231, 21);
    this.comboBoxEditSection.TabIndex = 7;
    this.comboBoxEditSection.SelectedIndexChanged += new EventHandler(this.comboBoxEditSection_SelectedIndexChanged);
    this.panelWalkNav.Controls.Add((System.Windows.Forms.Control) this.numericUpDownBrush);
    this.panelWalkNav.Controls.Add((System.Windows.Forms.Control) this.label3);
    this.panelWalkNav.Controls.Add((System.Windows.Forms.Control) this.comboBoxEditMap);
    this.panelWalkNav.Controls.Add((System.Windows.Forms.Control) this.label4);
    this.panelWalkNav.Dock = DockStyle.Top;
    this.panelWalkNav.Location = new Point(0, 114);
    this.panelWalkNav.Name = "panelWalkNav";
    this.panelWalkNav.Size = new Size(323, 64 /*0x40*/);
    this.panelWalkNav.TabIndex = 6;
    this.panelWalkNav.Visible = false;
    this.panelWalkNav.Paint += new PaintEventHandler(this.panelBrush_Paint);
    this.numericUpDownBrush.Location = new Point(75, 10);
    this.numericUpDownBrush.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numericUpDownBrush.Name = "numericUpDownBrush";
    this.numericUpDownBrush.Size = new Size(39, 20);
    this.numericUpDownBrush.TabIndex = 1;
    this.numericUpDownBrush.Value = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.numericUpDownBrush.ValueChanged += new EventHandler(this.numericUpDownBrush_ValueChanged);
    this.label3.AutoSize = true;
    this.label3.Location = new Point(9, 12);
    this.label3.Name = "label3";
    this.label3.Size = new Size(60, 13);
    this.label3.TabIndex = 0;
    this.label3.Text = "Brush Size:";
    this.comboBoxEditMap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.comboBoxEditMap.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBoxEditMap.FormattingEnabled = true;
    this.comboBoxEditMap.Location = new Point(75, 36);
    this.comboBoxEditMap.Name = "comboBoxEditMap";
    this.comboBoxEditMap.Size = new Size(231, 21);
    this.comboBoxEditMap.Sorted = true;
    this.comboBoxEditMap.TabIndex = 0;
    this.comboBoxEditMap.SelectedIndexChanged += new EventHandler(this.comboBoxEditMap_SelectedIndexChanged);
    this.label4.AutoSize = true;
    this.label4.Location = new Point(9, 39);
    this.label4.Name = "label4";
    this.label4.Size = new Size(31 /*0x1F*/, 13);
    this.label4.TabIndex = 2;
    this.label4.Text = "Map:";
    this.panelNavMode.Controls.Add((System.Windows.Forms.Control) this.groupBoxNavMode);
    this.panelNavMode.Dock = DockStyle.Top;
    this.panelNavMode.Location = new Point(0, 32 /*0x20*/);
    this.panelNavMode.Name = "panelNavMode";
    this.panelNavMode.Size = new Size(323, 82);
    this.panelNavMode.TabIndex = 5;
    this.panelNavMode.Visible = false;
    this.groupBoxNavMode.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.groupBoxNavMode.Controls.Add((System.Windows.Forms.Control) this.radioButtonNav);
    this.groupBoxNavMode.Controls.Add((System.Windows.Forms.Control) this.radioButtonDynamic);
    this.groupBoxNavMode.Location = new Point(12, 9);
    this.groupBoxNavMode.Name = "groupBoxNavMode";
    this.groupBoxNavMode.Size = new Size(133, 68);
    this.groupBoxNavMode.TabIndex = 3;
    this.groupBoxNavMode.TabStop = false;
    this.groupBoxNavMode.Text = "Navigation Edit Mode";
    this.radioButtonNav.AutoSize = true;
    this.radioButtonNav.Checked = true;
    this.radioButtonNav.Location = new Point(6, 19);
    this.radioButtonNav.Name = "radioButtonNav";
    this.radioButtonNav.Size = new Size(76, 17);
    this.radioButtonNav.TabIndex = 0;
    this.radioButtonNav.TabStop = true;
    this.radioButtonNav.Text = "Navigation";
    this.radioButtonNav.UseVisualStyleBackColor = true;
    this.radioButtonNav.CheckedChanged += new EventHandler(this.radioButtonNav_CheckedChanged);
    this.radioButtonDynamic.AutoSize = true;
    this.radioButtonDynamic.Location = new Point(6, 40);
    this.radioButtonDynamic.Name = "radioButtonDynamic";
    this.radioButtonDynamic.Size = new Size(66, 17);
    this.radioButtonDynamic.TabIndex = 1;
    this.radioButtonDynamic.Text = "Dynamic";
    this.radioButtonDynamic.UseVisualStyleBackColor = true;
    this.radioButtonDynamic.CheckedChanged += new EventHandler(this.radioButtonDynamic_CheckedChanged);
    this.panelGeneral.Controls.Add((System.Windows.Forms.Control) this.label7);
    this.panelGeneral.Controls.Add((System.Windows.Forms.Control) this.trackBarDrawDistance);
    this.panelGeneral.Dock = DockStyle.Top;
    this.panelGeneral.Location = new Point(0, 0);
    this.panelGeneral.Name = "panelGeneral";
    this.panelGeneral.Size = new Size(323, 32 /*0x20*/);
    this.panelGeneral.TabIndex = 8;
    this.panelGeneral.Visible = false;
    this.label7.AutoSize = true;
    this.label7.Location = new Point(8, 9);
    this.label7.Name = "label7";
    this.label7.Size = new Size(80 /*0x50*/, 13);
    this.label7.TabIndex = 1;
    this.label7.Text = "Draw Distance:";
    this.trackBarDrawDistance.AutoSize = false;
    this.trackBarDrawDistance.Location = new Point(94, 3);
    this.trackBarDrawDistance.Maximum = 0;
    this.trackBarDrawDistance.Name = "trackBarDrawDistance";
    this.trackBarDrawDistance.Size = new Size(80 /*0x50*/, 28);
    this.trackBarDrawDistance.TabIndex = 0;
    this.trackBarDrawDistance.TickStyle = TickStyle.None;
    this.trackBarDrawDistance.ValueChanged += new EventHandler(this.trackBarDrawDistance_ValueChanged);
    this.splitterMain.Location = new Point(323, 24);
    this.splitterMain.Name = "splitterMain";
    this.splitterMain.Size = new Size(6, 420);
    this.splitterMain.TabIndex = 4;
    this.splitterMain.TabStop = false;
    this.panelView.BackColor = Color.Black;
    this.panelView.Dock = DockStyle.Fill;
    this.panelView.Location = new Point(329, 49);
    this.panelView.Name = "panelView";
    this.panelView.Size = new Size(347, 395);
    this.panelView.TabIndex = 5;
    this.toolStripMain.Enabled = false;
    this.toolStripMain.Items.AddRange(new ToolStripItem[3]
    {
      (ToolStripItem) this.toolStripButtonWorld,
      (ToolStripItem) this.toolStripButtonWalk,
      (ToolStripItem) this.toolStripButtonNav
    });
    this.toolStripMain.Location = new Point(329, 24);
    this.toolStripMain.Name = "toolStripMain";
    this.toolStripMain.RenderMode = ToolStripRenderMode.System;
    this.toolStripMain.Size = new Size(347, 25);
    this.toolStripMain.TabIndex = 6;
    this.toolStripButtonWorld.Checked = true;
    this.toolStripButtonWorld.CheckState = CheckState.Checked;
    this.toolStripButtonWorld.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this.toolStripButtonWorld.Image = (Image) componentResourceManager.GetObject("toolStripButtonWorld.Image");
    this.toolStripButtonWorld.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonWorld.Name = "toolStripButtonWorld";
    this.toolStripButtonWorld.Size = new Size(39, 22);
    this.toolStripButtonWorld.Text = "World";
    this.toolStripButtonWorld.Click += new EventHandler(this.toolStripButtonWorld_Click);
    this.toolStripButtonWalk.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this.toolStripButtonWalk.Image = (Image) componentResourceManager.GetObject("toolStripButtonWalk.Image");
    this.toolStripButtonWalk.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonWalk.Name = "toolStripButtonWalk";
    this.toolStripButtonWalk.Size = new Size(34, 22);
    this.toolStripButtonWalk.Text = "Walk";
    this.toolStripButtonWalk.Click += new EventHandler(this.toolStripButtonWalk_Click);
    this.toolStripButtonNav.DisplayStyle = ToolStripItemDisplayStyle.Text;
    this.toolStripButtonNav.Image = (Image) componentResourceManager.GetObject("toolStripButtonNav.Image");
    this.toolStripButtonNav.ImageTransparentColor = Color.Magenta;
    this.toolStripButtonNav.Name = "toolStripButtonNav";
    this.toolStripButtonNav.Size = new Size(62, 22);
    this.toolStripButtonNav.Text = "Navigation";
    this.toolStripButtonNav.Click += new EventHandler(this.toolStripButtonNav_Click);
    this.toolStripMenuItem8.Name = "toolStripMenuItem8";
    this.toolStripMenuItem8.Size = new Size(169, 6);
    this.markersToolStripMenuItem.Checked = true;
    this.markersToolStripMenuItem.CheckOnClick = true;
    this.markersToolStripMenuItem.CheckState = CheckState.Checked;
    this.markersToolStripMenuItem.Name = "markersToolStripMenuItem";
    this.markersToolStripMenuItem.Size = new Size(172, 22);
    this.markersToolStripMenuItem.Text = "Markers";
    this.markersToolStripMenuItem.Click += new EventHandler(this.markersToolStripMenuItem_Click);
    this.creaturesToolStripMenuItem.Checked = true;
    this.creaturesToolStripMenuItem.CheckOnClick = true;
    this.creaturesToolStripMenuItem.CheckState = CheckState.Checked;
    this.creaturesToolStripMenuItem.Name = "creaturesToolStripMenuItem";
    this.creaturesToolStripMenuItem.Size = new Size(172, 22);
    this.creaturesToolStripMenuItem.Text = "Creatures";
    this.creaturesToolStripMenuItem.Click += new EventHandler(this.creaturesToolStripMenuItem_Click);
    this.controlThing.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.controlThing.Dock = DockStyle.Fill;
    this.controlThing.Location = new Point(4, 4);
    this.controlThing.Name = "controlThing";
    this.controlThing.Size = new Size(311, 48 /*0x30*/);
    this.controlThing.TabIndex = 0;
    this.controlThing.Thing = (Thing) null;
    this.controlThing.ThingChanged += new ThingChangedHandler(this.thing_Changed);
    this.objectsToolStripMenuItem.Checked = true;
    this.objectsToolStripMenuItem.CheckOnClick = true;
    this.objectsToolStripMenuItem.CheckState = CheckState.Checked;
    this.objectsToolStripMenuItem.Name = "objectsToolStripMenuItem";
    this.objectsToolStripMenuItem.Size = new Size(172, 22);
    this.objectsToolStripMenuItem.Text = "Objects";
    this.objectsToolStripMenuItem.Click += new EventHandler(this.objectsToolStripMenuItem_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(676, 444);
    this.Controls.Add((System.Windows.Forms.Control) this.panelView);
    this.Controls.Add((System.Windows.Forms.Control) this.toolStripMain);
    this.Controls.Add((System.Windows.Forms.Control) this.splitterMain);
    this.Controls.Add((System.Windows.Forms.Control) this.panelLeft);
    this.Controls.Add((System.Windows.Forms.Control) this.menuStripMain);
    this.Name = nameof (FormEditor);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Region Editor";
    this.Deactivate += new EventHandler(this.FormEditor_Deactivate);
    this.Load += new EventHandler(this.FormEditor_Load);
    this.Shown += new EventHandler(this.FormEditor_Shown);
    this.Activated += new EventHandler(this.FormEditor_Activated);
    this.FormClosing += new FormClosingEventHandler(this.FormEditor_FormClosing);
    this.menuStripMain.ResumeLayout(false);
    this.menuStripMain.PerformLayout();
    this.panelLeft.ResumeLayout(false);
    this.panelNormalMode.ResumeLayout(false);
    this.panelThing.ResumeLayout(false);
    this.panelChange.ResumeLayout(false);
    this.panelChange.PerformLayout();
    this.panelEditNav.ResumeLayout(false);
    this.panelEditNav.PerformLayout();
    this.numericUpDownSubset.EndInit();
    this.panelWalkNav.ResumeLayout(false);
    this.panelWalkNav.PerformLayout();
    this.numericUpDownBrush.EndInit();
    this.panelNavMode.ResumeLayout(false);
    this.groupBoxNavMode.ResumeLayout(false);
    this.groupBoxNavMode.PerformLayout();
    this.panelGeneral.ResumeLayout(false);
    this.panelGeneral.PerformLayout();
    this.trackBarDrawDistance.EndInit();
    this.toolStripMain.ResumeLayout(false);
    this.toolStripMain.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void Init()
  {
    this.myController = new GfxThingController();
    this.myView = new GfxThingView();
    this.myController.AddView((GfxView) this.myView);
    this.panelView.Controls.Add((System.Windows.Forms.Control) this.myView);
    this.myView.Dock = DockStyle.Fill;
    this.toolStripButtonNav.Visible = Settings.GetBool("Editor", "NavModeOn", false);
    this.myView.ThingSelected += new ThingSelectedHandler(this.myView_ThingSelected);
    this.myView.ThingCreated += new ThingCreateHandler(this.myView_ThingCreated);
    this.myView.ThingPicked += new ThingPickHandler(this.myView_ThingPicked);
  }

  private void Thing_SelectUID(Variable variable)
  {
    this.myView.Mode = EditorMode.Pick;
    this.myCreateMode = FormEditor.CreateMode.SelectObject;
    this.myUIDVariable = variable;
    this.controlThing.Enabled = false;
  }

  public FormEditor()
  {
    this.InitializeComponent();
    this.Init();
  }

  public FormEditor(FableMod.WLD.Region region, Thing thing, Thing teleporter, ThingMap map)
  {
    this.InitializeComponent();
    this.Init();
    this.SelectTeleporterDestination(thing, teleporter, map);
    this.LoadRegion(region);
  }

  private void SelectTeleporterDestination(Thing thing, Thing teleporter, ThingMap map)
  {
    this.myTeleporter = teleporter;
    this.myTeleporterMap = map;
    this.myCreatedThing = thing;
    this.myView.Mode = EditorMode.Create;
    this.myCreateMode = FormEditor.CreateMode.SelectTeleporterDst;
  }

  private void Reset()
  {
    this.myView.Reset();
    if (this.myMaps != null)
    {
      for (int index = 0; index < this.myMaps.Count; ++index)
        this.myMaps[index].Destroy();
      this.myMaps.Clear();
    }
    this.myController.ResetObjects();
    this.comboBoxEditMap.Items.Clear();
    this.comboBoxEditSection.Items.Clear();
    this.comboBoxMap.Items.Clear();
    this.mySelectedThings = (Thing[]) null;
  }

  private void LoadRegion(FableMod.WLD.Region region)
  {
    this.myView.Activate(false);
    WLDFileController wld1 = FileDatabase.Instance.GetWLD();
    WLDFile wld2 = wld1.WLD;
    RegionLoader regionLoader = new RegionLoader(Path.GetDirectoryName(wld1.FileName) + "\\", this.myController, region, (STBFile) null, Settings.GetBool("Editor", "SeesMaps", false));
    FormProcess formProcess = new FormProcess((Processor) regionLoader);
    int num = (int) formProcess.ShowDialog();
    formProcess.Dispose();
    this.myMaps = regionLoader.Maps;
    this.myView.Name = region.RegionName;
    formProcess.Dispose();
    this.myView.IsometricCamera();
    this.comboBoxMap.Items.Clear();
    if (this.myMaps != null)
    {
      this.FillMapComboBox(this.comboBoxMap);
      this.FillMapComboBox(this.comboBoxEditMap);
    }
    this.Text = "Region Editor - " + region.RegionName;
    this.myView.Maps = this.myMaps;
    this.myView.EditSection = "NULL";
    this.myView.EditMap = this.myMaps[0];
    this.panelGeneral.Visible = true;
    if (this.trackBarDrawDistance.Maximum == 0)
    {
      this.trackBarDrawDistance.Minimum = 1;
      this.trackBarDrawDistance.Maximum = (int) this.myView.DrawDistance;
    }
    this.trackBarDrawDistance.Value = (int) this.myView.DrawDistance;
    this.toolStripMain.Enabled = true;
    this.thingsToolStripMenuItem.Enabled = true;
    this.editToolStripMenuItem.Enabled = true;
    this.viewToolStripMenuItem.Enabled = true;
    this.saveScreenshotAsToolStripMenuItem.Enabled = true;
    this.myRegion = region.RegionName;
    this.myController.ShowOfType("Marker", this.markersToolStripMenuItem.Checked);
    this.myController.ShowOfType("AICreature", this.creaturesToolStripMenuItem.Checked);
    this.myController.ShowOfType("Object", this.objectsToolStripMenuItem.Checked);
    this.myView.Activate(true);
  }

  private void loadRegionToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (!this.CheckChanges())
      return;
    WLDFile wld = FileDatabase.Instance.GetWLD().WLD;
    FormRegionSelect formRegionSelect = new FormRegionSelect();
    for (int index = 0; index < wld.RegionCount; ++index)
      formRegionSelect.listBoxRegions.Items.Add((object) wld.get_Regions(index).RegionName);
    if (formRegionSelect.ShowDialog() != DialogResult.OK)
      return;
    formRegionSelect.Dispose();
    this.Reset();
    this.LoadRegion(wld.get_Regions(formRegionSelect.listBoxRegions.Items[formRegionSelect.listBoxRegions.SelectedIndex].ToString()));
  }

  private void closeToolStripMenuItem_Click(object sender, EventArgs e) => this.Close();

  private void FillMapComboBox(ComboBox comboBox)
  {
    for (int index = 0; index < this.myMaps.Count; ++index)
    {
      FormEditor.MapComboBoxItem mapComboBoxItem = new FormEditor.MapComboBoxItem(this.myMaps[index]);
      comboBox.Items.Add((object) mapComboBoxItem);
    }
  }

  private void UpdateComboBoxes(Thing[] things)
  {
    this.comboBoxSection.Items.Clear();
    if (things == null)
    {
      this.comboBoxMap.Enabled = false;
      this.comboBoxSection.Enabled = false;
      this.buttonChangeMap.Enabled = false;
      this.buttonChangeSection.Enabled = false;
    }
    else
    {
      this.comboBoxMap.Enabled = true;
      this.comboBoxSection.Enabled = true;
      this.buttonChangeMap.Enabled = this.myMaps.Count > 1;
      this.buttonChangeSection.Enabled = true;
      TNGFile tngFile = things[0].Section.TNGFile;
      for (int index = 1; index < things.Length; ++index)
      {
        if (tngFile != things[index].Section.TNGFile)
          tngFile = (TNGFile) null;
      }
      this.comboBoxMap.SelectedIndex = -1;
      for (int index = 0; index < this.comboBoxMap.Items.Count; ++index)
      {
        if (((FormEditor.MapComboBoxItem) this.comboBoxMap.Items[index]).myMap.TNG == tngFile)
        {
          this.comboBoxMap.SelectedIndex = index;
          break;
        }
      }
      this.comboBoxSection.Items.Clear();
      if (tngFile == null)
        return;
      Section section = things[0].Section;
      for (int index = 1; index < things.Length; ++index)
      {
        if (section != things[index].Section)
          section = (Section) null;
      }
      for (int index = 0; index < tngFile.SectionCount; ++index)
      {
        FormEditor.SectionComboBoxItem sectionComboBoxItem = new FormEditor.SectionComboBoxItem(tngFile.get_Sections(index));
        int num = this.comboBoxSection.Items.Add((object) sectionComboBoxItem);
        if (sectionComboBoxItem.mySection == section)
          this.comboBoxSection.SelectedIndex = num;
      }
    }
  }

  private void myView_ThingSelected(Thing[] things)
  {
    if (this.myView.Mode != EditorMode.Normal)
      return;
    this.panelNormalMode.Visible = true;
    if (things != null && things.Length == 1)
    {
      this.controlThing.Thing = things[0];
      this.controlThing.ApplySelectUID(new SelectUIDHandler(this.Thing_SelectUID));
      this.controlThing.Visible = true;
    }
    else
      this.controlThing.Visible = false;
    this.UpdateComboBoxes(things);
    this.mySelectedThings = things;
  }

  private void myView_ThingPicked(Thing thing)
  {
    if (!string.IsNullOrEmpty(this.myUIDVariable.Restriction) && thing.Name != this.myUIDVariable.Restriction)
    {
      int num1 = (int) MessageBox.Show($"Invalid object. Select \"{this.myUIDVariable.Restriction}\", please.");
    }
    else
    {
      int num2 = (int) MessageBox.Show($"Selected {thing.Name}:{thing.DefinitionType}:{thing.UID}.");
      this.myUIDVariable.Value = (object) thing.UID;
      this.myUIDVariable = (Variable) null;
      this.myCreateMode = FormEditor.CreateMode.None;
      this.myView.Mode = EditorMode.Normal;
      this.controlThing.Enabled = true;
    }
  }

  private void myView_ThingCreated(float x, float y, float z, float nx, float ny, float nz)
  {
    ThingMap map = (ThingMap) null;
    for (int index = 0; index < this.myMaps.Count; ++index)
    {
      if (this.myMaps[index].IsOver(x, y, z))
      {
        map = this.myMaps[index];
        break;
      }
    }
    if (map == null)
    {
      int num1 = (int) FormMain.Instance.ErrorMessage("Please click over a map");
    }
    else if (this.myCreateMode == FormEditor.CreateMode.SelectTeleporterDst)
    {
      float num2 = x - this.myTeleporterMap.X;
      float num3 = y - this.myTeleporterMap.Y;
      CTCBlock ctcBlock1 = this.myTeleporter.get_CTCs("CTCPhysicsStandard");
      if (MessageBox.Show("Create a two-way teleporter?", "ChocolateBox", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        Thing createdThing = this.myCreatedThing;
        if (this.SetupForTeleporter())
        {
          map.AddThing(this.myController, this.myCreatedThing, "NULL", x, y, z, nx, ny, nz);
          CTCBlock ctcBlock2 = createdThing.get_CTCs("CTCPhysicsStandard");
          CTCBlock ctcBlock3 = this.myTeleporter.get_CTCs("CTCPhysicsStandard");
          ctcBlock3.get_Variables("PositionX").Value = (object) (float) ((double) this.myTeleporterMap.X + (double) (float) ctcBlock2.get_Variables("PositionX").Value - (double) map.X);
          ctcBlock3.get_Variables("PositionY").Value = (object) (float) ((double) this.myTeleporterMap.Y + (double) (float) ctcBlock2.get_Variables("PositionY").Value - (double) map.Y);
          ctcBlock3.get_Variables("PositionZ").Value = (object) (float) ((double) (float) ctcBlock2.get_Variables("PositionZ").Value + 0.5);
          map.AddThing(this.myController, this.myTeleporter, "NULL");
          z += 0.5f;
        }
      }
      else if (MessageBox.Show("Create a teleportation platform?\r\nThis object is a visual que only.", "ChocolateBox", MessageBoxButtons.YesNo) == DialogResult.Yes)
      {
        Thing thing = new Thing("Object");
        thing.Create(FileDatabase.Instance.TNGDefinitions, "OBJECT_GUILD_PEDESTAL_TELEPORT_01");
        thing.UID = UIDManager.Generate();
        thing.Player = 4;
        map.AddThing(this.myController, thing, "NULL", x, y, z, nx, ny, nz);
        z += 0.5f;
      }
      ctcBlock1.get_Variables("PositionX").Value = (object) num2;
      ctcBlock1.get_Variables("PositionY").Value = (object) num3;
      ctcBlock1.get_Variables("PositionZ").Value = (object) z;
      int num4 = (int) FormMain.Instance.InfoMessage($"Teleporter created from map \"{this.myTeleporterMap.Name}\" to map \"{map.Name}\".");
      this.myCreateMode = FormEditor.CreateMode.None;
      this.myView.Mode = EditorMode.Normal;
    }
    else
    {
      if (this.myCreateMode == FormEditor.CreateMode.SelectTeleporterSrc)
      {
        try
        {
          map.AddThing(this.myController, this.myCreatedThing, "NULL", x, y, z, nx, ny, nz);
          map.AddThing(this.myController, this.myTeleporter, "NULL");
        }
        catch (Exception ex)
        {
          int num5 = (int) FormMain.Instance.ErrorMessage(ex.Message);
          return;
        }
        WLDFile wld = FileDatabase.Instance.GetWLD().WLD;
        FormRegionSelect formRegionSelect = new FormRegionSelect();
        formRegionSelect.Text = "Select Destination Region";
        for (int index = 0; index < wld.RegionCount; ++index)
          formRegionSelect.listBoxRegions.Items.Add((object) wld.get_Regions(index).RegionName);
        if (formRegionSelect.ShowDialog() == DialogResult.OK)
        {
          FableMod.WLD.Region region = wld.get_Regions(formRegionSelect.listBoxRegions.Items[formRegionSelect.listBoxRegions.SelectedIndex].ToString());
          if (region.RegionName != this.myRegion)
          {
            FormMain.Instance.AddMDI((Form) new FormEditor(region, this.myCreatedThing, this.myTeleporter, map));
          }
          else
          {
            this.SelectTeleporterDestination(this.myCreatedThing, this.myTeleporter, map);
            return;
          }
        }
        else
        {
          this.myController.RemoveThing(this.myCreatedThing);
          this.myController.RemoveThing(this.myTeleporter);
        }
        formRegionSelect.Dispose();
        this.myCreatedThing = (Thing) null;
        this.myTeleporter = (Thing) null;
      }
      else if (this.myCreateMode == FormEditor.CreateMode.CreateNew)
      {
        try
        {
          map.AddThing(this.myController, this.myCreatedThing, "NULL", x, y, z, nx, ny, nz);
        }
        catch (Exception ex)
        {
          int num6 = (int) FormMain.Instance.ErrorMessage(ex.Message);
        }
        this.myCreatedThing = (Thing) null;
      }
      else if (this.myCreateMode == FormEditor.CreateMode.Import)
      {
        for (int index = 0; index < this.myImports.Count; ++index)
        {
          Thing import = this.myImports[index];
          map.AddThing(this.myController, import, "NULL");
        }
      }
      this.myImports.Clear();
      this.myCreateMode = FormEditor.CreateMode.None;
      this.myView.Mode = EditorMode.Normal;
    }
  }

  private void thing_Changed(Thing thing)
  {
    if (this.myView.IsDoing)
      return;
    this.myView.UpdateAll();
  }

  private void topToolStripMenuItem_Click(object sender, EventArgs e) => this.myView.TopCamera();

  private void frontToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.myView.FrontCamera();
  }

  private void rightToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.myView.RightCamera();
  }

  private bool CheckChanges()
  {
    if (this.myMaps == null)
      return true;
    this.toolStripButtonWorld_Click((object) null, (EventArgs) null);
    List<Processor> processors = new List<Processor>();
    for (int index = 0; index < this.myMaps.Count; ++index)
    {
      ThingMap map = this.myMaps[index];
      if (map.Modified)
      {
        switch (MessageBox.Show($"Save changes to map {map.Name}?", FormMain.Instance.Title, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation))
        {
          case DialogResult.Cancel:
            return false;
          case DialogResult.Yes:
            processors.Add((Processor) new MapSaveProcessor(map.TNG, map.LEV));
            continue;
          default:
            continue;
        }
      }
    }
    if (processors.Count > 0)
    {
      FormProcess formProcess = new FormProcess(processors);
      int num = (int) formProcess.ShowDialog();
      formProcess.Dispose();
    }
    return true;
  }

  private void FormEditor_FormClosing(object sender, FormClosingEventArgs e)
  {
    if (!this.CheckChanges())
    {
      e.Cancel = true;
    }
    else
    {
      this.Reset();
      GfxManager.GetModelManager().Clear();
      GfxManager.GetTextureManager().Clear();
      GfxManager.GetThemeManager().Clear();
      this.myView.Destroy();
      this.myController.Destroy();
      this.myController = (GfxThingController) null;
      this.myMaps = (Collection<ThingMap>) null;
      this.myView = (GfxThingView) null;
    }
  }

  private void showDirectionAxesToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.myView.DirectionAxes = !this.myView.DirectionAxes;
  }

  private void viewToolStripMenuItem_Paint(object sender, PaintEventArgs e)
  {
    this.showDirectionAxesToolStripMenuItem.Checked = this.myView.DirectionAxes;
  }

  private void newToolStripMenuItem_Click(object sender, EventArgs e)
  {
    object @object = ContentManager.Instance.SelectEntry(new FableMod.ContentManagement.Link(LinkDestination.GameBINEntryName, (string) null), (object) this.myLastNewEntry);
    if (@object == null)
      return;
    ContentObject entry = ContentManager.Instance.FindEntry(LinkDestination.GameBINEntryName, @object);
    if (entry == null)
    {
      int num = (int) FormMain.Instance.ErrorMessage("Object could not be retrieved");
    }
    else
    {
      BINEntry binEntry = (BINEntry) entry.Object;
      string name = "Object";
      if (binEntry.Definition == "BUILDING")
        name = "Building";
      else if (binEntry.Definition == "CREATURE")
        name = "AICreature";
      else if (binEntry.Definition == "MARKER")
        name = "Marker";
      else if (binEntry.Definition == "HOLY_SITE")
        name = "Holy Site";
      else if (binEntry.Definition == "VILLAGE")
        name = "Village";
      this.myCreatedThing = new Thing(name);
      this.myCreatedThing.Create(FileDatabase.Instance.TNGDefinitions, binEntry.Name);
      this.myLastNewEntry = binEntry.Name;
      this.myCreatedThing.UID = UIDManager.Generate();
      this.myCreatedThing.Player = 4;
      this.myView.Mode = EditorMode.Create;
      this.myCreateMode = FormEditor.CreateMode.CreateNew;
    }
  }

  private void ImportTNG(string fileName)
  {
    TNGFile tngFile = new TNGFile(FileDatabase.Instance.TNGDefinitions);
    tngFile.Load(fileName);
    for (int index1 = 0; index1 < tngFile.SectionCount; ++index1)
    {
      for (int index2 = 0; index2 < tngFile.get_Sections(index1).Things.Count; ++index2)
        this.myImports.Add(tngFile.get_Sections(index1).Things[index2]);
    }
    for (int index = 0; index < this.myImports.Count; ++index)
    {
      Thing import = this.myImports[index];
      import.UID = UIDManager.Generate();
      import.Detach();
    }
    tngFile.Destroy();
    if (this.myImports.Count == 0)
    {
      int num = (int) FormMain.Instance.ErrorMessage("No things");
    }
    else
    {
      this.myView.Mode = EditorMode.Create;
      this.myCreateMode = FormEditor.CreateMode.Import;
    }
  }

  private void importToolStripMenuItem_Click(object sender, EventArgs e)
  {
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.InitialDirectory = Settings.FableDirectory;
    openFileDialog.Filter = "TNG Files (*.tng)|*.tng|All Files (*.*)|*.*||";
    openFileDialog.DefaultExt = "tng";
    if (openFileDialog.ShowDialog() == DialogResult.OK)
      this.ImportTNG(openFileDialog.FileName);
    openFileDialog.Dispose();
  }

  public void SaveChanges(bool quiet)
  {
    StringBuilder stringBuilder = new StringBuilder();
    if (this.myMaps != null)
    {
      this.myView.SaveChanges();
      stringBuilder.AppendFormat("Saved files:\r\n");
      for (int index = 0; index < this.myMaps.Count; ++index)
      {
        if (this.myMaps[index].Modified)
        {
          stringBuilder.AppendLine("\t" + this.myMaps[index].TNG.FileName);
          this.myMaps[index].TNG.Save(this.myMaps[index].TNG.FileName);
          this.myMaps[index].TNG.Modified = false;
        }
      }
      if (!quiet)
      {
        int num = (int) FormMain.Instance.InfoMessage(stringBuilder.ToString());
      }
    }
  }

  private void saveRegionToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.SaveChanges(false);
  }

  private void fileToolStripMenuItem_Paint(object sender, PaintEventArgs e)
  {
    if (this.myMaps != null)
    {
      for (int index = 0; index < this.myMaps.Count; ++index)
      {
        if (this.myMaps[index].Modified)
        {
          this.saveRegionToolStripMenuItem.Enabled = true;
          return;
        }
      }
    }
    this.saveRegionToolStripMenuItem.Enabled = false;
  }

  private void toolStripButtonWalk_Click(object sender, EventArgs e)
  {
    this.comboBoxEditMap.SelectedIndex = this.comboBoxEditMap.FindStringExact(this.myView.EditMap.Name);
    this.toolStripButtonWalk.Checked = true;
    this.toolStripButtonWorld.Checked = false;
    this.toolStripButtonNav.Checked = false;
    this.thingsToolStripMenuItem.Enabled = false;
    this.panelNormalMode.Visible = false;
    this.panelNavMode.Visible = false;
    this.panelEditNav.Visible = false;
    this.panelWalkNav.Visible = true;
    this.myView.Mode = EditorMode.Walk;
  }

  private void toolStripButtonWorld_Click(object sender, EventArgs e)
  {
    this.thingsToolStripMenuItem.Enabled = true;
    this.myView.Mode = EditorMode.Normal;
    this.toolStripButtonWorld.Checked = true;
    this.toolStripButtonWalk.Checked = false;
    this.toolStripButtonNav.Checked = false;
    this.panelWalkNav.Visible = false;
    this.panelNavMode.Visible = false;
    this.panelEditNav.Visible = false;
  }

  private void toolStripButtonNav_Click(object sender, EventArgs e)
  {
    this.myView.Mode = EditorMode.Navigation;
    this.toolStripButtonNav.Checked = true;
    this.toolStripButtonWalk.Checked = false;
    this.toolStripButtonWorld.Checked = false;
    this.thingsToolStripMenuItem.Enabled = false;
    this.panelNormalMode.Visible = false;
    this.panelEditNav.Visible = true;
    this.panelWalkNav.Visible = true;
    this.panelNavMode.Visible = true;
    this.comboBoxEditMap.SelectedIndex = this.comboBoxEditMap.FindStringExact(this.myView.EditMap.Name);
    this.myUpdateBlock = true;
    this.comboBoxEditSection.SelectedIndex = this.comboBoxEditSection.FindStringExact(this.myView.EditSection);
    this.numericUpDownSubset.Value = (Decimal) this.myView.EditSubset;
    this.myUpdateBlock = false;
  }

  private void numericUpDownBrush_ValueChanged(object sender, EventArgs e)
  {
    this.myView.BrushSize = (int) this.numericUpDownBrush.Value;
  }

  private void panelBrush_Paint(object sender, PaintEventArgs e)
  {
    this.numericUpDownBrush.Value = (Decimal) this.myView.BrushSize;
  }

  private void FillEditSectionComboBox(ThingMap map)
  {
    this.comboBoxEditSection.Items.Clear();
    LEVFile lev = map.LEV;
    for (int index = 0; index < lev.SectionCount; ++index)
      this.comboBoxEditSection.Items.Add((object) lev.get_Sections(index).Name);
  }

  private void comboBoxEditMap_SelectedIndexChanged(object sender, EventArgs e)
  {
    FormEditor.MapComboBoxItem selectedItem = (FormEditor.MapComboBoxItem) this.comboBoxEditMap.SelectedItem;
    this.myView.EditMap = selectedItem.myMap;
    this.FillEditSectionComboBox(selectedItem.myMap);
    this.myView.Focus();
  }

  private void FormEditor_Load(object sender, EventArgs e)
  {
  }

  private void comboBoxEditSection_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.myUpdateBlock)
      return;
    this.myView.EditSection = this.comboBoxEditSection.Items[this.comboBoxEditSection.SelectedIndex].ToString();
  }

  private void numericUpDownSubset_ValueChanged(object sender, EventArgs e)
  {
    if (this.myUpdateBlock)
      return;
    this.myView.EditSubset = (int) this.numericUpDownSubset.Value;
  }

  private void ShowEditorTools(bool show)
  {
    this.editorToolsToolStripMenuItem.Checked = show;
    this.panelLeft.Visible = show;
    this.splitterMain.Visible = show;
  }

  private void editorToolsToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.ShowEditorTools(!this.editorToolsToolStripMenuItem.Checked);
  }

  private void buttonChangeSection_Click(object sender, EventArgs e)
  {
    FormEditor.SectionComboBoxItem sectionComboBoxItem = (FormEditor.SectionComboBoxItem) this.comboBoxSection.Items[this.comboBoxSection.SelectedIndex];
    if (sectionComboBoxItem == null)
      return;
    this.buttonChangeSection.Enabled = false;
    this.buttonChangeSection.Update();
    foreach (Thing selectedThing in this.mySelectedThings)
    {
      if (selectedThing.Section != sectionComboBoxItem.mySection)
      {
        selectedThing.Detach();
        sectionComboBoxItem.mySection.AddThing(selectedThing);
      }
    }
    this.buttonChangeSection.Enabled = true;
  }

  private void radioButtonNav_CheckedChanged(object sender, EventArgs e)
  {
    this.myView.NavEditMode = NavigationEditMode.Navigation;
  }

  private void radioButtonDynamic_CheckedChanged(object sender, EventArgs e)
  {
    this.myView.NavEditMode = NavigationEditMode.Dynamic;
  }

  private void trackBarDrawDistance_ValueChanged(object sender, EventArgs e)
  {
    this.myView.DrawDistance = (float) this.trackBarDrawDistance.Value;
  }

  private void buttonResetNav_Click(object sender, EventArgs e)
  {
    this.myView.ResetNavSections();
    this.FillEditSectionComboBox(this.myView.EditMap);
  }

  private void buttonNewSection_Click(object sender, EventArgs e)
  {
    int num = (int) MessageBox.Show((IWin32Window) this, "not yet implemented");
  }

  private void buttonAddSubset_Click(object sender, EventArgs e)
  {
    int num = (int) MessageBox.Show((IWin32Window) this, "not yet implemented");
  }

  private void buttonChangeMap_Click(object sender, EventArgs e)
  {
    FormEditor.MapComboBoxItem mapComboBoxItem = (FormEditor.MapComboBoxItem) this.comboBoxMap.Items[this.comboBoxMap.SelectedIndex];
    if (mapComboBoxItem == null)
      return;
    Section section = mapComboBoxItem.myMap.TNG.get_Sections("NULL");
    if (section == null)
    {
      section = new Section("NULL");
      mapComboBoxItem.myMap.TNG.AddSection(section);
    }
    this.buttonChangeMap.Enabled = false;
    this.buttonChangeMap.Update();
    foreach (Thing selectedThing in this.mySelectedThings)
    {
      selectedThing.Detach();
      section.AddThing(selectedThing);
    }
    this.buttonChangeMap.Enabled = true;
  }

  private void fullScreenToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.fullScreenToolStripMenuItem.Checked = !this.fullScreenToolStripMenuItem.Checked;
    if (this.fullScreenToolStripMenuItem.Checked)
    {
      this.WindowState = FormWindowState.Maximized;
      this.ShowEditorTools(false);
      FormMain.Instance.ShowFileList(false);
    }
    else
    {
      this.WindowState = FormWindowState.Normal;
      this.ShowEditorTools(true);
      FormMain.Instance.ShowFileList(true);
    }
  }

  private void saveScreenshotAsToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.myView.SaveScreenshot();
  }

  private bool SetupForTeleporter()
  {
    object @object = ContentManager.Instance.SelectEntry(new FableMod.ContentManagement.Link(LinkDestination.GameBINEntryName, "OBJECT"), (object) "OBJECT_GUILD_PEDESTAL_TELEPORT_01");
    if (@object == null)
      return false;
    BINEntry binEntry = (BINEntry) ContentManager.Instance.FindEntry(LinkDestination.GameBINEntryName, @object).Object;
    if (binEntry.Definition != "OBJECT")
    {
      int num = (int) FormMain.Instance.ErrorMessage("Invalid object selected.");
      return false;
    }
    this.myCreatedThing = new Thing("Object");
    this.myCreatedThing.Create(FileDatabase.Instance.TNGDefinitions, binEntry.Name);
    this.myCreatedThing.UID = UIDManager.Generate();
    this.myCreatedThing.Player = 4;
    this.myCreatedThing.get_Variables("ScriptData").Value = (object) "Teleport?";
    CTCBlock ctcBlock = this.myCreatedThing.ApplyCTC(FileDatabase.Instance.TNGDefinitions, "CTCActionUseScriptedHook");
    ctcBlock.get_Variables("Usable").Value = (object) true;
    ctcBlock.get_Variables("ForceConfirmation").Value = (object) true;
    ctcBlock.get_Variables("TeleportToRegionEntrance").Value = (object) true;
    ((Variable) ((ElementArray) this.myCreatedThing.Find("CreateTC")).Add()).Value = (object) "CTCActionUseScriptedHook";
    Thing thing = new Thing("Marker");
    thing.Create(FileDatabase.Instance.TNGDefinitions, "MARKER_BASIC");
    thing.UID = UIDManager.Generate();
    thing.Player = 4;
    ctcBlock.get_Variables("EntranceConnectedToUID").Value = (object) thing.UID;
    this.myTeleporter = thing;
    return true;
  }

  private void teleporterToolStripMenuItem_Click(object sender, EventArgs e)
  {
    if (!this.SetupForTeleporter())
      return;
    this.myView.Mode = EditorMode.Create;
    this.myCreateMode = FormEditor.CreateMode.SelectTeleporterSrc;
  }

  private void FormEditor_Shown(object sender, EventArgs e)
  {
    if (this.myCreateMode != FormEditor.CreateMode.SelectTeleporterDst)
      return;
    this.myController.UpdateObjects();
    int num = (int) FormMain.Instance.InfoMessage("Next, select the teleporter destination.");
    this.myView.Focus();
  }

  private void findByUIDToolStripMenuItem_Click(object sender, EventArgs e)
  {
    FormTextBox formTextBox = new FormTextBox();
    formTextBox.Text = "Find by UID";
    formTextBox.labelInput.Text = "UID:";
    if (formTextBox.ShowDialog() != DialogResult.OK)
      return;
    this.myView.FindByUID(formTextBox.textBoxInput.Text);
  }

  private void FormEditor_Activated(object sender, EventArgs e) => this.myView.Activate(true);

  private void FormEditor_Deactivate(object sender, EventArgs e)
  {
    if (this.myView == null)
      return;
    this.myView.Activate(false);
  }

  private void unfreezeAllToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.myController.FreezeAll(false);
  }

  private void markersToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.myController.ShowOfType("Marker", this.markersToolStripMenuItem.Checked);
    this.myView.ClearSelection();
    this.myView.Render();
  }

  private void creaturesToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.myController.ShowOfType("AICreature", this.creaturesToolStripMenuItem.Checked);
    this.myView.ClearSelection();
    this.myView.Render();
  }

  private void objectsToolStripMenuItem_Click(object sender, EventArgs e)
  {
    this.myController.ShowOfType("Object", this.objectsToolStripMenuItem.Checked);
    this.myView.ClearSelection();
    this.myView.Render();
  }

  private enum CreateMode
  {
    None,
    CreateNew,
    Import,
    SelectTeleporterSrc,
    SelectTeleporterDst,
    SelectObject,
  }

  private class MapComboBoxItem
  {
    public ThingMap myMap;

    public MapComboBoxItem(ThingMap map) => this.myMap = map;

    public override string ToString() => this.myMap.Name;
  }

  private class SectionComboBoxItem
  {
    public Section mySection;

    public SectionComboBoxItem(Section section) => this.mySection = section;

    public override string ToString() => this.mySection.Name;
  }
}
