// Decompiled with JetBrains decompiler
// Type: FableMod.Content.Forms.ControlPhysicsModel
// Assembly: FableMod.Content.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A575F982-23D4-4DAE-A571-3D769462C59B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Content.Forms.dll

using FableMod.BIG;
using FableMod.Gfx.Integration;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Content.Forms;

public class ControlPhysicsModel : ControlBIGEntry
{
  private GfxController myController;
  private GfxView myView;
  private GfxTagModel myModel;
  private IContainer components;
  private Button buttonImport;
  private Button buttonExport;
  private Panel panelView;
  private OpenFileDialog openFileDialog;
  private SaveFileDialog saveFileDialog;
  private Button buttonClear;

  public ControlPhysicsModel()
  {
    this.InitializeComponent();
    this.myController = new GfxController();
    this.myView = new GfxView();
    this.myController.AddView(this.myView);
    this.panelView.Controls.Add((Control) this.myView);
    this.myView.Dock = DockStyle.Fill;
    this.myView.BringToFront();
  }

  public override void ApplyChanges() => this.myModel.CompileToEntry(this.BIGEntry);

  public override AssetEntry BIGEntry
  {
    get => base.BIGEntry;
    set
    {
      base.BIGEntry = value;
      if (this.myModel != null)
        this.myModel.Dispose();
      this.buttonExport.Enabled = false;
      this.myModel = new GfxTagModel(value);
      this.myController.ResetObjects();
      this.myController.AddModel((GfxBaseModel) this.myModel);
      this.myView.FrontCamera();
      this.myView.Activate(true);
      this.buttonExport.Enabled = true;
    }
  }

  private void buttonExport_Click(object sender, EventArgs e)
  {
    if (this.saveFileDialog.ShowDialog((IWin32Window) this) != DialogResult.OK)
      return;
    this.myModel.ExportX(this.saveFileDialog.FileName);
  }

  private void buttonImport_Click(object sender, EventArgs e)
  {
    if (this.openFileDialog.ShowDialog((IWin32Window) this) != DialogResult.OK)
      return;
    try
    {
      this.myModel.ImportX(this.openFileDialog.FileName);
      this.myController.ResetObjects();
      this.myController.AddModel((GfxBaseModel) this.myModel);
      this.myView.FrontCamera();
      this.myView.Activate(true);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, ex.Message);
    }
  }

  private void buttonClear_Click(object sender, EventArgs e)
  {
    this.myModel.Clear();
    this.myController.ResetObjects();
    this.myView.FrontCamera();
    this.myView.Activate(true);
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.buttonImport = new Button();
    this.buttonExport = new Button();
    this.panelView = new Panel();
    this.openFileDialog = new OpenFileDialog();
    this.saveFileDialog = new SaveFileDialog();
    this.buttonClear = new Button();
    this.SuspendLayout();
    this.lblEntryType.Size = new Size(75, 13);
    this.lblEntryType.Text = "Physics Model";
    this.buttonImport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonImport.Location = new Point(446, 8);
    this.buttonImport.Name = "buttonImport";
    this.buttonImport.Size = new Size(75, 23);
    this.buttonImport.TabIndex = 2;
    this.buttonImport.Text = "Import";
    this.buttonImport.UseVisualStyleBackColor = true;
    this.buttonImport.Click += new EventHandler(this.buttonImport_Click);
    this.buttonExport.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonExport.Enabled = false;
    this.buttonExport.Location = new Point(365, 8);
    this.buttonExport.Name = "buttonExport";
    this.buttonExport.Size = new Size(75, 23);
    this.buttonExport.TabIndex = 3;
    this.buttonExport.Text = "Export";
    this.buttonExport.UseVisualStyleBackColor = true;
    this.buttonExport.Click += new EventHandler(this.buttonExport_Click);
    this.panelView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.panelView.Location = new Point(11, 37);
    this.panelView.Name = "panelView";
    this.panelView.Size = new Size(510, 334);
    this.panelView.TabIndex = 4;
    this.openFileDialog.DefaultExt = "X";
    this.openFileDialog.Filter = "DirectX X-Files (*.X)|*.X||";
    this.saveFileDialog.DefaultExt = "X";
    this.saveFileDialog.Filter = "DirectX X-Files (*.X)|*.X||";
    this.buttonClear.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonClear.Enabled = false;
    this.buttonClear.Location = new Point(284, 8);
    this.buttonClear.Name = "buttonClear";
    this.buttonClear.Size = new Size(75, 23);
    this.buttonClear.TabIndex = 5;
    this.buttonClear.Text = "Clear";
    this.buttonClear.UseVisualStyleBackColor = true;
    this.buttonClear.Click += new EventHandler(this.buttonClear_Click);
    this.AutoScaleMode = AutoScaleMode.Inherit;
    this.Controls.Add((Control) this.buttonClear);
    this.Controls.Add((Control) this.buttonImport);
    this.Controls.Add((Control) this.buttonExport);
    this.Controls.Add((Control) this.panelView);
    this.EntryType = "Physics Model";
    this.Name = nameof (ControlPhysicsModel);
    this.Size = new Size(535, 388);
    this.Controls.SetChildIndex((Control) this.lblEntryType, 0);
    this.Controls.SetChildIndex((Control) this.panelView, 0);
    this.Controls.SetChildIndex((Control) this.buttonExport, 0);
    this.Controls.SetChildIndex((Control) this.buttonImport, 0);
    this.Controls.SetChildIndex((Control) this.buttonClear, 0);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
