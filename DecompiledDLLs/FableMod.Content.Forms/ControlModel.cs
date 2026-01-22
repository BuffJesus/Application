// Decompiled with JetBrains decompiler
// Type: FableMod.Content.Forms.ControlModel
// Assembly: FableMod.Content.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A575F982-23D4-4DAE-A571-3D769462C59B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Content.Forms.dll

using FableMod.BIG;
using FableMod.ContentManagement;
using FableMod.Gfx.Integration;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Content.Forms;

public class ControlModel : ControlBIGEntry
{
  private IContainer components;
  private Button btnSave;
  private GfxModel m_Model;
  private GfxModelLOD m_ModelLOD;
  private TabControl tcModelLOD;
  private TabPage tabPreview;
  private TabPage tabSubMeshes;
  private TabPage tabMaterials;
  private Label lblSubMeshes;
  private ListView lvSubMeshes;
  private Panel pnlSubMeshDisplay;
  private Label lblSubMeshMaterialID;
  private TextBox txtSubMeshMaterialID;
  private Button btnSubMeshApply;
  private Button buttonApply;
  private TextBox txtMaterialID;
  private Label lblMaterialID;
  private Label lblMaterials;
  private ListView lvMaterials;
  private TextBox txtMaterialName;
  private Label lblMaterialName;
  private Panel pnlMaterialDisplay;
  private TextBox txtMaterialBaseTexture;
  private Label lblMaterialBaseTexture;
  private TextBox txtMaterialBumpMap;
  private Label lblMaterialBumpMap;
  private TextBox txtMaterialSpecularMap;
  private Label lblMaterialSpecularMap;
  private TextBox txtMaterialTextureFlags;
  private Label lblMaterialTextureLayers;
  private TextBox txtMaterialGlow;
  private Label lblMaterialGlow;
  private Label lblMaterialUnknown4;
  private Label lblMaterialUnknown3;
  private Label lblMaterialUnknown2;
  private TextBox txtMaterialAlphaTexture;
  private Label lblMaterialUnknown1;
  private TextBox txtMaterialUnknown4;
  private TextBox txtMaterialUnknown3;
  private TextBox txtMaterialUnknown2;
  private Label lblSelectLOD;
  private ComboBox cbSelectLOD;
  private Button btnLoad;
  private Button buttonSelectTexture;
  private Label labelFaces;
  private Label label2;
  private Label labelVertices;
  private Label label1;
  private GfxController myController;
  private Label label3;
  private TextBox textBoxPhysics;
  private Button buttonBrowse;
  private Button buttonExportImage;
  private GfxView myView;
  private Button buttonSelectBumpTexture;
  private CheckBox checkBoxAlpha;
  private bool myBlockEvents;

  public ControlModel()
  {
    this.InitializeComponent();
    this.myController = new GfxController();
    this.myView = new GfxView();
    this.myController.AddView(this.myView);
    this.tabPreview.Controls.Add((System.Windows.Forms.Control) this.myView);
    this.myView.Dock = DockStyle.Fill;
    this.myView.BringToFront();
  }

  public override void OnActivate() => base.OnActivate();

  public override void OnDeactivate() => base.OnDeactivate();

  private void RefreshSubMeshes()
  {
    this.textBoxPhysics.Text = this.m_Model.Physics.ToString();
    this.lvSubMeshes.Clear();
    if (this.m_ModelLOD == null)
      return;
    for (uint index = 0; (long) index < (long) this.m_ModelLOD.SubMeshCount; ++index)
    {
      SubMesh subMesh = this.m_ModelLOD.get_SubMeshes(index);
      this.lvSubMeshes.Items.Add(new ListViewItem(index.ToString())
      {
        Tag = (object) subMesh
      });
    }
  }

  private void RefreshMaterials()
  {
    this.lvMaterials.Clear();
    if (this.m_ModelLOD != null)
    {
      for (uint index = 0; (long) index < (long) this.m_ModelLOD.MaterialCount; ++index)
      {
        Material material = this.m_ModelLOD.get_Materials(index);
        this.lvMaterials.Items.Add(new ListViewItem(material.Name)
        {
          Tag = (object) material
        });
      }
    }
    this.pnlMaterialDisplay.Visible = false;
  }

  public override void ApplyChanges()
  {
    try
    {
      this.m_Model.Physics = uint.Parse(this.textBoxPhysics.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Invalid physics object.");
      return;
    }
    this.m_Model.CompileToEntry(this.m_Entry);
    this.m_Model = new GfxModel(this.m_Entry);
    this.m_ModelLOD = this.cbSelectLOD.SelectedIndex < 0 ? this.m_Model.get_LODs(0) : this.m_Model.get_LODs(this.cbSelectLOD.SelectedIndex);
    this.myController.ResetObjects();
    this.myController.AddModel(this.m_ModelLOD);
    this.myView.FrontCamera();
    this.myView.Activate(true);
    this.RefreshSubMeshes();
    this.RefreshMaterials();
  }

  public override AssetEntry BIGEntry
  {
    get => base.BIGEntry;
    set
    {
      base.BIGEntry = value;
      this.myController.ResetObjects();
      this.cbSelectLOD.Items.Clear();
      if (this.m_Model != null)
        this.m_Model.Dispose();
      this.m_Model = new GfxModel(this.m_Entry);
      for (int index = 0; (long) index < (long) this.m_Model.LODCount; ++index)
        this.cbSelectLOD.Items.Add((object) ("LOD " + index.ToString()));
      this.cbSelectLOD.SelectedIndex = 0;
      this.myController.AddModel((GfxBaseModel) this.m_Model);
      this.myView.FrontCamera();
      this.myView.Activate(true);
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.btnSave = new Button();
    this.tcModelLOD = new TabControl();
    this.tabPreview = new TabPage();
    this.tabSubMeshes = new TabPage();
    this.pnlSubMeshDisplay = new Panel();
    this.labelFaces = new Label();
    this.label2 = new Label();
    this.labelVertices = new Label();
    this.label1 = new Label();
    this.btnSubMeshApply = new Button();
    this.txtSubMeshMaterialID = new TextBox();
    this.lblSubMeshMaterialID = new Label();
    this.lblSubMeshes = new Label();
    this.lvSubMeshes = new ListView();
    this.tabMaterials = new TabPage();
    this.pnlMaterialDisplay = new Panel();
    this.checkBoxAlpha = new CheckBox();
    this.buttonSelectBumpTexture = new Button();
    this.buttonSelectTexture = new Button();
    this.txtMaterialUnknown4 = new TextBox();
    this.txtMaterialUnknown3 = new TextBox();
    this.txtMaterialUnknown2 = new TextBox();
    this.lblMaterialUnknown4 = new Label();
    this.lblMaterialUnknown3 = new Label();
    this.lblMaterialUnknown2 = new Label();
    this.txtMaterialAlphaTexture = new TextBox();
    this.lblMaterialUnknown1 = new Label();
    this.buttonApply = new Button();
    this.txtMaterialGlow = new TextBox();
    this.lblMaterialGlow = new Label();
    this.txtMaterialBaseTexture = new TextBox();
    this.lblMaterialBaseTexture = new Label();
    this.txtMaterialBumpMap = new TextBox();
    this.lblMaterialBumpMap = new Label();
    this.txtMaterialSpecularMap = new TextBox();
    this.lblMaterialSpecularMap = new Label();
    this.txtMaterialTextureFlags = new TextBox();
    this.lblMaterialTextureLayers = new Label();
    this.txtMaterialName = new TextBox();
    this.lblMaterialName = new Label();
    this.txtMaterialID = new TextBox();
    this.lblMaterialID = new Label();
    this.lblMaterials = new Label();
    this.lvMaterials = new ListView();
    this.lblSelectLOD = new Label();
    this.cbSelectLOD = new ComboBox();
    this.btnLoad = new Button();
    this.label3 = new Label();
    this.textBoxPhysics = new TextBox();
    this.buttonBrowse = new Button();
    this.buttonExportImage = new Button();
    this.tcModelLOD.SuspendLayout();
    this.tabSubMeshes.SuspendLayout();
    this.pnlSubMeshDisplay.SuspendLayout();
    this.tabMaterials.SuspendLayout();
    this.pnlMaterialDisplay.SuspendLayout();
    this.SuspendLayout();
    this.lblEntryType.Size = new Size(36, 13);
    this.lblEntryType.Text = "Model";
    this.btnSave.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnSave.Location = new Point(322, 3);
    this.btnSave.Name = "btnSave";
    this.btnSave.Size = new Size(109, 23);
    this.btnSave.TabIndex = 2;
    this.btnSave.Text = "Export LOD Model";
    this.btnSave.Click += new EventHandler(this.btnSave_Click);
    this.tcModelLOD.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.tcModelLOD.Controls.Add((System.Windows.Forms.Control) this.tabPreview);
    this.tcModelLOD.Controls.Add((System.Windows.Forms.Control) this.tabSubMeshes);
    this.tcModelLOD.Controls.Add((System.Windows.Forms.Control) this.tabMaterials);
    this.tcModelLOD.Location = new Point(11, 57);
    this.tcModelLOD.Name = "tcModelLOD";
    this.tcModelLOD.SelectedIndex = 0;
    this.tcModelLOD.Size = new Size(539, 403);
    this.tcModelLOD.TabIndex = 4;
    this.tcModelLOD.Selected += new TabControlEventHandler(this.tcModelLOD_Selected);
    this.tabPreview.Location = new Point(4, 22);
    this.tabPreview.Name = "tabPreview";
    this.tabPreview.Padding = new Padding(3);
    this.tabPreview.Size = new Size(531, 377);
    this.tabPreview.TabIndex = 0;
    this.tabPreview.Text = "Preview";
    this.tabPreview.UseVisualStyleBackColor = true;
    this.tabPreview.Leave += new EventHandler(this.tabPreview_Leave);
    this.tabPreview.Enter += new EventHandler(this.tabPreview_Enter);
    this.tabSubMeshes.Controls.Add((System.Windows.Forms.Control) this.pnlSubMeshDisplay);
    this.tabSubMeshes.Controls.Add((System.Windows.Forms.Control) this.lblSubMeshes);
    this.tabSubMeshes.Controls.Add((System.Windows.Forms.Control) this.lvSubMeshes);
    this.tabSubMeshes.Location = new Point(4, 22);
    this.tabSubMeshes.Name = "tabSubMeshes";
    this.tabSubMeshes.Padding = new Padding(3);
    this.tabSubMeshes.Size = new Size(531, 377);
    this.tabSubMeshes.TabIndex = 1;
    this.tabSubMeshes.Text = "SubMeshes";
    this.tabSubMeshes.UseVisualStyleBackColor = true;
    this.pnlSubMeshDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.pnlSubMeshDisplay.Controls.Add((System.Windows.Forms.Control) this.labelFaces);
    this.pnlSubMeshDisplay.Controls.Add((System.Windows.Forms.Control) this.label2);
    this.pnlSubMeshDisplay.Controls.Add((System.Windows.Forms.Control) this.labelVertices);
    this.pnlSubMeshDisplay.Controls.Add((System.Windows.Forms.Control) this.label1);
    this.pnlSubMeshDisplay.Controls.Add((System.Windows.Forms.Control) this.btnSubMeshApply);
    this.pnlSubMeshDisplay.Controls.Add((System.Windows.Forms.Control) this.txtSubMeshMaterialID);
    this.pnlSubMeshDisplay.Controls.Add((System.Windows.Forms.Control) this.lblSubMeshMaterialID);
    this.pnlSubMeshDisplay.Location = new Point(189, 24);
    this.pnlSubMeshDisplay.Name = "pnlSubMeshDisplay";
    this.pnlSubMeshDisplay.Size = new Size(336, 347);
    this.pnlSubMeshDisplay.TabIndex = 2;
    this.pnlSubMeshDisplay.Visible = false;
    this.labelFaces.AutoSize = true;
    this.labelFaces.Location = new Point((int) sbyte.MaxValue, 65);
    this.labelFaces.Name = "labelFaces";
    this.labelFaces.Size = new Size(10, 13);
    this.labelFaces.TabIndex = 6;
    this.labelFaces.Text = "-";
    this.label2.AutoSize = true;
    this.label2.Location = new Point(4, 65);
    this.label2.Name = "label2";
    this.label2.Size = new Size(39, 13);
    this.label2.TabIndex = 5;
    this.label2.Text = "Faces:";
    this.labelVertices.AutoSize = true;
    this.labelVertices.Location = new Point((int) sbyte.MaxValue, 41);
    this.labelVertices.Name = "labelVertices";
    this.labelVertices.Size = new Size(10, 13);
    this.labelVertices.TabIndex = 4;
    this.labelVertices.Text = "-";
    this.label1.AutoSize = true;
    this.label1.Location = new Point(4, 41);
    this.label1.Name = "label1";
    this.label1.Size = new Size(48 /*0x30*/, 13);
    this.label1.TabIndex = 3;
    this.label1.Text = "Vertices:";
    this.btnSubMeshApply.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnSubMeshApply.Location = new Point(206, 316);
    this.btnSubMeshApply.Name = "btnSubMeshApply";
    this.btnSubMeshApply.Size = new Size(111, 23);
    this.btnSubMeshApply.TabIndex = 2;
    this.btnSubMeshApply.Text = "Apply Changes";
    this.btnSubMeshApply.UseVisualStyleBackColor = true;
    this.btnSubMeshApply.Click += new EventHandler(this.btnSubMeshApply_Click);
    this.txtSubMeshMaterialID.Location = new Point(130, 9);
    this.txtSubMeshMaterialID.Name = "txtSubMeshMaterialID";
    this.txtSubMeshMaterialID.Size = new Size(168, 20);
    this.txtSubMeshMaterialID.TabIndex = 1;
    this.lblSubMeshMaterialID.AutoSize = true;
    this.lblSubMeshMaterialID.Location = new Point(4, 12);
    this.lblSubMeshMaterialID.Name = "lblSubMeshMaterialID";
    this.lblSubMeshMaterialID.Size = new Size(61, 13);
    this.lblSubMeshMaterialID.TabIndex = 0;
    this.lblSubMeshMaterialID.Text = "Material ID:";
    this.lblSubMeshes.AutoSize = true;
    this.lblSubMeshes.Location = new Point(6, 7);
    this.lblSubMeshes.Name = "lblSubMeshes";
    this.lblSubMeshes.Size = new Size(66, 13);
    this.lblSubMeshes.TabIndex = 1;
    this.lblSubMeshes.Text = "SubMeshes:";
    this.lvSubMeshes.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lvSubMeshes.HideSelection = false;
    this.lvSubMeshes.Location = new Point(6, 24);
    this.lvSubMeshes.MultiSelect = false;
    this.lvSubMeshes.Name = "lvSubMeshes";
    this.lvSubMeshes.Size = new Size(177, 347);
    this.lvSubMeshes.TabIndex = 0;
    this.lvSubMeshes.UseCompatibleStateImageBehavior = false;
    this.lvSubMeshes.View = View.List;
    this.lvSubMeshes.SelectedIndexChanged += new EventHandler(this.lvSubMeshes_SelectedIndexChanged);
    this.tabMaterials.Controls.Add((System.Windows.Forms.Control) this.pnlMaterialDisplay);
    this.tabMaterials.Controls.Add((System.Windows.Forms.Control) this.lblMaterials);
    this.tabMaterials.Controls.Add((System.Windows.Forms.Control) this.lvMaterials);
    this.tabMaterials.Location = new Point(4, 22);
    this.tabMaterials.Name = "tabMaterials";
    this.tabMaterials.Padding = new Padding(3);
    this.tabMaterials.Size = new Size(531, 377);
    this.tabMaterials.TabIndex = 2;
    this.tabMaterials.Text = "Materials";
    this.tabMaterials.UseVisualStyleBackColor = true;
    this.pnlMaterialDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.checkBoxAlpha);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.buttonSelectBumpTexture);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.buttonSelectTexture);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.txtMaterialUnknown4);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.txtMaterialUnknown3);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.txtMaterialUnknown2);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.lblMaterialUnknown4);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.lblMaterialUnknown3);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.lblMaterialUnknown2);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.txtMaterialAlphaTexture);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.lblMaterialUnknown1);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.buttonApply);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.txtMaterialGlow);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.lblMaterialGlow);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.txtMaterialBaseTexture);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.lblMaterialBaseTexture);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.txtMaterialBumpMap);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.lblMaterialBumpMap);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.txtMaterialSpecularMap);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.lblMaterialSpecularMap);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.txtMaterialTextureFlags);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.lblMaterialTextureLayers);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.txtMaterialName);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.lblMaterialName);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.txtMaterialID);
    this.pnlMaterialDisplay.Controls.Add((System.Windows.Forms.Control) this.lblMaterialID);
    this.pnlMaterialDisplay.Location = new Point(189, 24);
    this.pnlMaterialDisplay.Name = "pnlMaterialDisplay";
    this.pnlMaterialDisplay.Size = new Size(333, 345);
    this.pnlMaterialDisplay.TabIndex = 5;
    this.pnlMaterialDisplay.Visible = false;
    this.checkBoxAlpha.AutoSize = true;
    this.checkBoxAlpha.Location = new Point(130, 183);
    this.checkBoxAlpha.Name = "checkBoxAlpha";
    this.checkBoxAlpha.Size = new Size(95, 17);
    this.checkBoxAlpha.TabIndex = 27;
    this.checkBoxAlpha.Text = "Alpha Enabled";
    this.checkBoxAlpha.UseVisualStyleBackColor = true;
    this.buttonSelectBumpTexture.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonSelectBumpTexture.Location = new Point(290, 81);
    this.buttonSelectBumpTexture.Name = "buttonSelectBumpTexture";
    this.buttonSelectBumpTexture.Size = new Size(24, 20);
    this.buttonSelectBumpTexture.TabIndex = 26;
    this.buttonSelectBumpTexture.Text = "...";
    this.buttonSelectBumpTexture.UseVisualStyleBackColor = true;
    this.buttonSelectBumpTexture.Click += new EventHandler(this.buttonSelectBumpTexture_Click);
    this.buttonSelectTexture.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonSelectTexture.Location = new Point(290, 57);
    this.buttonSelectTexture.Name = "buttonSelectTexture";
    this.buttonSelectTexture.Size = new Size(24, 20);
    this.buttonSelectTexture.TabIndex = 25;
    this.buttonSelectTexture.Text = "...";
    this.buttonSelectTexture.UseVisualStyleBackColor = true;
    this.buttonSelectTexture.Click += new EventHandler(this.buttonSelectTexture_Click);
    this.txtMaterialUnknown4.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtMaterialUnknown4.Location = new Point(130, 283);
    this.txtMaterialUnknown4.Name = "txtMaterialUnknown4";
    this.txtMaterialUnknown4.Size = new Size(184, 20);
    this.txtMaterialUnknown4.TabIndex = 24;
    this.txtMaterialUnknown3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtMaterialUnknown3.Location = new Point(130, 257);
    this.txtMaterialUnknown3.Name = "txtMaterialUnknown3";
    this.txtMaterialUnknown3.Size = new Size(184, 20);
    this.txtMaterialUnknown3.TabIndex = 23;
    this.txtMaterialUnknown2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtMaterialUnknown2.Location = new Point(130, 231);
    this.txtMaterialUnknown2.Name = "txtMaterialUnknown2";
    this.txtMaterialUnknown2.Size = new Size(184, 20);
    this.txtMaterialUnknown2.TabIndex = 22;
    this.lblMaterialUnknown4.AutoSize = true;
    this.lblMaterialUnknown4.Location = new Point(5, 286);
    this.lblMaterialUnknown4.Name = "lblMaterialUnknown4";
    this.lblMaterialUnknown4.Size = new Size(62, 13);
    this.lblMaterialUnknown4.TabIndex = 21;
    this.lblMaterialUnknown4.Text = "Unknown4:";
    this.lblMaterialUnknown3.AutoSize = true;
    this.lblMaterialUnknown3.Location = new Point(4, 260);
    this.lblMaterialUnknown3.Name = "lblMaterialUnknown3";
    this.lblMaterialUnknown3.Size = new Size(62, 13);
    this.lblMaterialUnknown3.TabIndex = 20;
    this.lblMaterialUnknown3.Text = "Unknown3:";
    this.lblMaterialUnknown2.AutoSize = true;
    this.lblMaterialUnknown2.Location = new Point(4, 234);
    this.lblMaterialUnknown2.Name = "lblMaterialUnknown2";
    this.lblMaterialUnknown2.Size = new Size(62, 13);
    this.lblMaterialUnknown2.TabIndex = 19;
    this.lblMaterialUnknown2.Text = "Unknown2:";
    this.txtMaterialAlphaTexture.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtMaterialAlphaTexture.Location = new Point(130, 131);
    this.txtMaterialAlphaTexture.Name = "txtMaterialAlphaTexture";
    this.txtMaterialAlphaTexture.Size = new Size(184, 20);
    this.txtMaterialAlphaTexture.TabIndex = 18;
    this.txtMaterialAlphaTexture.TextChanged += new EventHandler(this.txtMaterialAlphaTexture_TextChanged);
    this.lblMaterialUnknown1.AutoSize = true;
    this.lblMaterialUnknown1.Location = new Point(3, 134);
    this.lblMaterialUnknown1.Name = "lblMaterialUnknown1";
    this.lblMaterialUnknown1.Size = new Size(90, 13);
    this.lblMaterialUnknown1.TabIndex = 17;
    this.lblMaterialUnknown1.Text = "Alpha Texture ID:";
    this.buttonApply.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonApply.Location = new Point(205, 309);
    this.buttonApply.Name = "buttonApply";
    this.buttonApply.Size = new Size(109, 23);
    this.buttonApply.TabIndex = 2;
    this.buttonApply.Text = "Apply Changes";
    this.buttonApply.UseVisualStyleBackColor = true;
    this.buttonApply.Click += new EventHandler(this.btnMaterialApply_Click);
    this.txtMaterialGlow.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtMaterialGlow.Location = new Point(130, 205);
    this.txtMaterialGlow.Name = "txtMaterialGlow";
    this.txtMaterialGlow.Size = new Size(184, 20);
    this.txtMaterialGlow.TabIndex = 16 /*0x10*/;
    this.lblMaterialGlow.AutoSize = true;
    this.lblMaterialGlow.Location = new Point(4, 208 /*0xD0*/);
    this.lblMaterialGlow.Name = "lblMaterialGlow";
    this.lblMaterialGlow.Size = new Size(77, 13);
    this.lblMaterialGlow.TabIndex = 15;
    this.lblMaterialGlow.Text = "Glow Strength:";
    this.txtMaterialBaseTexture.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtMaterialBaseTexture.Location = new Point(130, 57);
    this.txtMaterialBaseTexture.Name = "txtMaterialBaseTexture";
    this.txtMaterialBaseTexture.Size = new Size(154, 20);
    this.txtMaterialBaseTexture.TabIndex = 14;
    this.txtMaterialBaseTexture.TextChanged += new EventHandler(this.txtMaterialBaseTexture_TextChanged);
    this.lblMaterialBaseTexture.AutoSize = true;
    this.lblMaterialBaseTexture.Location = new Point(4, 60);
    this.lblMaterialBaseTexture.Name = "lblMaterialBaseTexture";
    this.lblMaterialBaseTexture.Size = new Size(87, 13);
    this.lblMaterialBaseTexture.TabIndex = 13;
    this.lblMaterialBaseTexture.Text = "Base Texture ID:";
    this.txtMaterialBumpMap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtMaterialBumpMap.Location = new Point(130, 81);
    this.txtMaterialBumpMap.Name = "txtMaterialBumpMap";
    this.txtMaterialBumpMap.Size = new Size(154, 20);
    this.txtMaterialBumpMap.TabIndex = 12;
    this.txtMaterialBumpMap.TextChanged += new EventHandler(this.txtMaterialBumpMap_TextChanged);
    this.lblMaterialBumpMap.AutoSize = true;
    this.lblMaterialBumpMap.Location = new Point(4, 84);
    this.lblMaterialBumpMap.Name = "lblMaterialBumpMap";
    this.lblMaterialBumpMap.Size = new Size(75, 13);
    this.lblMaterialBumpMap.TabIndex = 11;
    this.lblMaterialBumpMap.Text = "Bump Map ID:";
    this.txtMaterialSpecularMap.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtMaterialSpecularMap.Location = new Point(130, 105);
    this.txtMaterialSpecularMap.Name = "txtMaterialSpecularMap";
    this.txtMaterialSpecularMap.Size = new Size(184, 20);
    this.txtMaterialSpecularMap.TabIndex = 10;
    this.txtMaterialSpecularMap.TextChanged += new EventHandler(this.txtMaterialSpecularMap_TextChanged);
    this.lblMaterialSpecularMap.AutoSize = true;
    this.lblMaterialSpecularMap.Location = new Point(4, 108);
    this.lblMaterialSpecularMap.Name = "lblMaterialSpecularMap";
    this.lblMaterialSpecularMap.Size = new Size(90, 13);
    this.lblMaterialSpecularMap.TabIndex = 9;
    this.lblMaterialSpecularMap.Text = "Specular Map ID:";
    this.txtMaterialTextureFlags.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtMaterialTextureFlags.Location = new Point(130, 157);
    this.txtMaterialTextureFlags.Name = "txtMaterialTextureFlags";
    this.txtMaterialTextureFlags.Size = new Size(184, 20);
    this.txtMaterialTextureFlags.TabIndex = 8;
    this.lblMaterialTextureLayers.AutoSize = true;
    this.lblMaterialTextureLayers.Location = new Point(5, 160 /*0xA0*/);
    this.lblMaterialTextureLayers.Name = "lblMaterialTextureLayers";
    this.lblMaterialTextureLayers.Size = new Size(74, 13);
    this.lblMaterialTextureLayers.TabIndex = 7;
    this.lblMaterialTextureLayers.Text = "Texture Flags:";
    this.txtMaterialName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtMaterialName.Location = new Point(130, 9);
    this.txtMaterialName.Name = "txtMaterialName";
    this.txtMaterialName.Size = new Size(184, 20);
    this.txtMaterialName.TabIndex = 4;
    this.lblMaterialName.AutoSize = true;
    this.lblMaterialName.Location = new Point(4, 12);
    this.lblMaterialName.Name = "lblMaterialName";
    this.lblMaterialName.Size = new Size(78, 13);
    this.lblMaterialName.TabIndex = 3;
    this.lblMaterialName.Text = "Material Name:";
    this.txtMaterialID.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtMaterialID.Location = new Point(130, 33);
    this.txtMaterialID.Name = "txtMaterialID";
    this.txtMaterialID.Size = new Size(184, 20);
    this.txtMaterialID.TabIndex = 1;
    this.lblMaterialID.AutoSize = true;
    this.lblMaterialID.Location = new Point(4, 36);
    this.lblMaterialID.Name = "lblMaterialID";
    this.lblMaterialID.Size = new Size(61, 13);
    this.lblMaterialID.TabIndex = 0;
    this.lblMaterialID.Text = "Material ID:";
    this.lblMaterials.AutoSize = true;
    this.lblMaterials.Location = new Point(6, 7);
    this.lblMaterials.Name = "lblMaterials";
    this.lblMaterials.Size = new Size(52, 13);
    this.lblMaterials.TabIndex = 4;
    this.lblMaterials.Text = "Materials:";
    this.lvMaterials.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
    this.lvMaterials.HideSelection = false;
    this.lvMaterials.Location = new Point(6, 23);
    this.lvMaterials.MultiSelect = false;
    this.lvMaterials.Name = "lvMaterials";
    this.lvMaterials.Size = new Size(177, 346);
    this.lvMaterials.TabIndex = 3;
    this.lvMaterials.UseCompatibleStateImageBehavior = false;
    this.lvMaterials.View = View.List;
    this.lvMaterials.SelectedIndexChanged += new EventHandler(this.lvMaterials_SelectedIndexChanged);
    this.lblSelectLOD.AutoSize = true;
    this.lblSelectLOD.Location = new Point(137, 8);
    this.lblSelectLOD.Name = "lblSelectLOD";
    this.lblSelectLOD.Size = new Size(65, 13);
    this.lblSelectLOD.TabIndex = 5;
    this.lblSelectLOD.Text = "Select LOD:";
    this.cbSelectLOD.FormattingEnabled = true;
    this.cbSelectLOD.Location = new Point(208 /*0xD0*/, 5);
    this.cbSelectLOD.Name = "cbSelectLOD";
    this.cbSelectLOD.Size = new Size(80 /*0x50*/, 21);
    this.cbSelectLOD.TabIndex = 6;
    this.cbSelectLOD.SelectedIndexChanged += new EventHandler(this.cbSelectLOD_SelectedIndexChanged);
    this.btnLoad.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.btnLoad.Location = new Point(437, 3);
    this.btnLoad.Name = "btnLoad";
    this.btnLoad.Size = new Size(109, 23);
    this.btnLoad.TabIndex = 7;
    this.btnLoad.Text = "Import LOD Model";
    this.btnLoad.UseVisualStyleBackColor = true;
    this.btnLoad.Click += new EventHandler(this.btnLoad_Click);
    this.label3.AutoSize = true;
    this.label3.Location = new Point(8, 34);
    this.label3.Name = "label3";
    this.label3.Size = new Size(46, 13);
    this.label3.TabIndex = 8;
    this.label3.Text = "Physics:";
    this.textBoxPhysics.Location = new Point(60, 31 /*0x1F*/);
    this.textBoxPhysics.MaxLength = 6;
    this.textBoxPhysics.Name = "textBoxPhysics";
    this.textBoxPhysics.Size = new Size(62, 20);
    this.textBoxPhysics.TabIndex = 9;
    this.buttonBrowse.Location = new Point(128 /*0x80*/, 31 /*0x1F*/);
    this.buttonBrowse.Name = "buttonBrowse";
    this.buttonBrowse.Size = new Size(25, 20);
    this.buttonBrowse.TabIndex = 10;
    this.buttonBrowse.Text = "...";
    this.buttonBrowse.UseVisualStyleBackColor = true;
    this.buttonBrowse.Click += new EventHandler(this.buttonBrowse_Click);
    this.buttonExportImage.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonExportImage.Location = new Point(322, 29);
    this.buttonExportImage.Name = "buttonExportImage";
    this.buttonExportImage.Size = new Size(224 /*0xE0*/, 23);
    this.buttonExportImage.TabIndex = 11;
    this.buttonExportImage.Text = "Save screenshot as...";
    this.buttonExportImage.UseVisualStyleBackColor = true;
    this.buttonExportImage.Click += new EventHandler(this.buttonExportImage_Click);
    this.Controls.Add((System.Windows.Forms.Control) this.buttonExportImage);
    this.Controls.Add((System.Windows.Forms.Control) this.buttonBrowse);
    this.Controls.Add((System.Windows.Forms.Control) this.textBoxPhysics);
    this.Controls.Add((System.Windows.Forms.Control) this.label3);
    this.Controls.Add((System.Windows.Forms.Control) this.btnLoad);
    this.Controls.Add((System.Windows.Forms.Control) this.btnSave);
    this.Controls.Add((System.Windows.Forms.Control) this.cbSelectLOD);
    this.Controls.Add((System.Windows.Forms.Control) this.lblSelectLOD);
    this.Controls.Add((System.Windows.Forms.Control) this.tcModelLOD);
    this.EntryType = "Model";
    this.Name = nameof (ControlModel);
    this.Size = new Size(561, 463);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblEntryType, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.tcModelLOD, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblSelectLOD, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.cbSelectLOD, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.btnSave, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.btnLoad, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.label3, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.textBoxPhysics, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.buttonBrowse, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.buttonExportImage, 0);
    this.tcModelLOD.ResumeLayout(false);
    this.tabSubMeshes.ResumeLayout(false);
    this.tabSubMeshes.PerformLayout();
    this.pnlSubMeshDisplay.ResumeLayout(false);
    this.pnlSubMeshDisplay.PerformLayout();
    this.tabMaterials.ResumeLayout(false);
    this.tabMaterials.PerformLayout();
    this.pnlMaterialDisplay.ResumeLayout(false);
    this.pnlMaterialDisplay.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void btnSave_Click(object sender, EventArgs e)
  {
    if (this.m_ModelLOD == null)
      return;
    SaveFileDialog saveFileDialog = new SaveFileDialog();
    saveFileDialog.Filter = "DirectX X-File (*.X)|*.X||";
    if (saveFileDialog.ShowDialog((IWin32Window) this) != DialogResult.OK)
      return;
    this.m_ModelLOD.ExportX(saveFileDialog.FileName);
  }

  private void btnLoad_Click(object sender, EventArgs e)
  {
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.Filter = "DirectX X-File (*.x)|*.X|Wavefront (*.obj)|*.OBJ||";
    if (openFileDialog.ShowDialog((IWin32Window) this) == DialogResult.OK)
    {
      try
      {
        if (openFileDialog.FilterIndex == 1)
          this.m_ModelLOD.ImportX(openFileDialog.FileName);
        else
          this.m_ModelLOD.ImportObj(openFileDialog.FileName);
      }
      catch (Exception ex)
      {
        int num = (int) MessageBox.Show(ex.ToString());
      }
      this.m_ModelLOD = this.m_Model.get_LODs(this.cbSelectLOD.SelectedIndex);
      this.RefreshSubMeshes();
      this.RefreshMaterials();
      this.myController.ResetObjects();
      this.myController.AddModel(this.m_ModelLOD);
      this.myView.FrontCamera();
    }
    openFileDialog.Dispose();
  }

  private void tcModelLOD_Selected(object sender, TabControlEventArgs e)
  {
    TabPage tabPage = e.TabPage;
    TabPage tabPreview = this.tabPreview;
  }

  private void lvSubMeshes_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvSubMeshes.SelectedItems.Count != 0)
    {
      this.pnlSubMeshDisplay.Visible = true;
      SubMesh tag = (SubMesh) this.lvSubMeshes.SelectedItems[0].Tag;
      this.txtSubMeshMaterialID.Text = tag.MaterialID.ToString();
      this.labelVertices.Text = tag.VertexCount.ToString();
      this.labelFaces.Text = tag.FaceCount.ToString();
    }
    else
      this.pnlSubMeshDisplay.Visible = false;
  }

  private void btnSubMeshApply_Click(object sender, EventArgs e)
  {
    if (this.lvSubMeshes.SelectedItems.Count == 0)
      return;
    SubMesh tag = (SubMesh) this.lvSubMeshes.SelectedItems[0].Tag;
    try
    {
      tag.MaterialID = uint.Parse(this.txtSubMeshMaterialID.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Invalid material ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private void lvMaterials_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.lvMaterials.SelectedItems.Count != 0)
    {
      Material tag = (Material) this.lvMaterials.SelectedItems[0].Tag;
      this.pnlMaterialDisplay.Visible = true;
      this.txtMaterialID.Text = tag.ID.ToString();
      this.txtMaterialName.Text = tag.Name;
      this.myBlockEvents = true;
      this.txtMaterialBaseTexture.Text = tag.BaseTextureID.ToString();
      this.txtMaterialBumpMap.Text = tag.BumpMapTextureID.ToString();
      this.txtMaterialSpecularMap.Text = tag.ReflectionTextureID.ToString();
      this.txtMaterialTextureFlags.Text = tag.TextureFlags.ToString();
      this.checkBoxAlpha.Checked = tag.AlphaEnabled != (byte) 0;
      this.txtMaterialGlow.Text = tag.GlowStrength.ToString();
      this.myBlockEvents = false;
      this.txtMaterialAlphaTexture.Text = tag.AlphaMapTextureID.ToString();
      this.txtMaterialUnknown2.Text = tag.Unknown2.ToString();
      this.txtMaterialUnknown3.Text = tag.Unknown3.ToString();
      this.txtMaterialUnknown4.Text = tag.Unknown4.ToString();
    }
    else
      this.pnlMaterialDisplay.Visible = false;
  }

  private void btnMaterialApply_Click(object sender, EventArgs e)
  {
    if (this.lvMaterials.SelectedItems.Count == 0)
      return;
    Material tag = (Material) this.lvMaterials.SelectedItems[0].Tag;
    tag.Name = this.txtMaterialName.Text;
    try
    {
      tag.ID = uint.Parse(this.txtMaterialID.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Invalid material ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    try
    {
      tag.BaseTextureID = uint.Parse(this.txtMaterialBaseTexture.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Invalid base texture ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    try
    {
      tag.BumpMapTextureID = uint.Parse(this.txtMaterialBumpMap.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Invalid bump map ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    try
    {
      tag.ReflectionTextureID = uint.Parse(this.txtMaterialSpecularMap.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Invalid specular map ID.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    try
    {
      tag.TextureFlags = uint.Parse(this.txtMaterialTextureFlags.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Invalid max texture layers count.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    try
    {
      tag.AlphaEnabled = this.checkBoxAlpha.Checked ? (byte) 1 : (byte) 0;
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Invalid alpha enabled value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    try
    {
      tag.GlowStrength = uint.Parse(this.txtMaterialGlow.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Invalid glow strength.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    try
    {
      tag.AlphaMapTextureID = uint.Parse(this.txtMaterialAlphaTexture.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Invalid unknown1 value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    try
    {
      tag.Unknown2 = byte.Parse(this.txtMaterialUnknown2.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Invalid unknown2 value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    try
    {
      tag.Unknown3 = byte.Parse(this.txtMaterialUnknown3.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Invalid unknown3 value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
    try
    {
      tag.Unknown4 = ushort.Parse(this.txtMaterialUnknown4.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Invalid unknown4 value.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Hand);
    }
  }

  private void cbSelectLOD_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.m_ModelLOD = this.m_Model.get_LODs(this.cbSelectLOD.SelectedIndex);
    this.RefreshSubMeshes();
    this.RefreshMaterials();
    this.myController.ResetObjects();
    this.myController.AddModel(this.m_ModelLOD);
  }

  private void SelectTexture(TextBox textBox, uint textureId)
  {
    Material tag = (Material) this.lvMaterials.SelectedItems[0].Tag;
    object obj = ContentManager.Instance.SelectEntry(new FableMod.ContentManagement.Link(LinkDestination.MainTextureID, (string) null), (object) textureId);
    if (obj == null)
      return;
    textBox.Text = obj.ToString();
  }

  private void buttonSelectTexture_Click(object sender, EventArgs e)
  {
    if (this.lvMaterials.SelectedItems.Count <= 0)
      return;
    this.SelectTexture(this.txtMaterialBaseTexture, ((Material) this.lvMaterials.SelectedItems[0].Tag).BaseTextureID);
  }

  private void buttonBrowse_Click(object sender, EventArgs e)
  {
    object obj = ContentManager.Instance.SelectEntry(new FableMod.ContentManagement.Link(LinkDestination.ModelID, (string) null), (object) uint.Parse(this.textBoxPhysics.Text));
    if (obj == null)
      return;
    this.textBoxPhysics.Text = obj.ToString();
  }

  private void buttonExportImage_Click(object sender, EventArgs e) => this.myView.SaveScreenshot();

  private void tabPreview_Enter(object sender, EventArgs e) => this.myView.Activate(true);

  private void tabPreview_Leave(object sender, EventArgs e) => this.myView.Activate(false);

  private void SetTextureFlags(TextBox textBox, int flag)
  {
    if (this.myBlockEvents)
      return;
    int num = int.Parse(this.txtMaterialTextureFlags.Text);
    this.txtMaterialTextureFlags.Text = (!(textBox.Text == "0") ? num | flag : num & ~flag).ToString();
  }

  private void txtMaterialBaseTexture_TextChanged(object sender, EventArgs e)
  {
    this.SetTextureFlags(this.txtMaterialBaseTexture, 1);
  }

  private void txtMaterialBumpMap_TextChanged(object sender, EventArgs e)
  {
    this.SetTextureFlags(this.txtMaterialBumpMap, 2);
  }

  private void txtMaterialSpecularMap_TextChanged(object sender, EventArgs e)
  {
    this.SetTextureFlags(this.txtMaterialSpecularMap, 4);
  }

  private void buttonSelectBumpTexture_Click(object sender, EventArgs e)
  {
    if (this.lvMaterials.SelectedItems.Count <= 0)
      return;
    this.SelectTexture(this.txtMaterialBaseTexture, ((Material) this.lvMaterials.SelectedItems[0].Tag).BumpMapTextureID);
  }

  private void txtMaterialAlphaTexture_TextChanged(object sender, EventArgs e)
  {
    this.SetTextureFlags(this.txtMaterialAlphaTexture, 8);
    this.checkBoxAlpha.Checked = this.txtMaterialAlphaTexture.Text != "0";
  }
}
