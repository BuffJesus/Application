// Decompiled with JetBrains decompiler
// Type: FableMod.Content.Forms.ControlThing
// Assembly: FableMod.Content.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A575F982-23D4-4DAE-A571-3D769462C59B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Content.Forms.dll

using FableMod.ContentManagement;
using FableMod.TNG;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Content.Forms;

public class ControlThing : ControlComplexBlock
{
  private IContainer components;
  private GroupBox groupBoxBase;
  private Button buttonGenerate;
  private NumericUpDown numericUpDownPlayer;
  private Label label3;
  private Button buttonOpen;
  private TextBox textBoxUID;
  private Label label2;
  private Label label1;
  private TextBox textBoxDefType;
  private Panel panelBase;
  private Thing myThing;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.groupBoxBase = new GroupBox();
    this.buttonGenerate = new Button();
    this.numericUpDownPlayer = new NumericUpDown();
    this.label3 = new Label();
    this.buttonOpen = new Button();
    this.textBoxUID = new TextBox();
    this.label2 = new Label();
    this.label1 = new Label();
    this.textBoxDefType = new TextBox();
    this.panelBase = new Panel();
    this.panelControlsBase.SuspendLayout();
    this.panelTop.SuspendLayout();
    this.panelExpansion.SuspendLayout();
    this.groupBoxBase.SuspendLayout();
    this.numericUpDownPlayer.BeginInit();
    this.panelBase.SuspendLayout();
    this.SuspendLayout();
    this.panelControls.Location = new Point(0, 128 /*0x80*/);
    this.panelControls.Size = new Size(442, 244);
    this.panelControlsBase.Controls.Add((System.Windows.Forms.Control) this.panelBase);
    this.panelControlsBase.Size = new Size(442, 372);
    this.panelControlsBase.Controls.SetChildIndex((System.Windows.Forms.Control) this.panelBase, 0);
    this.panelControlsBase.Controls.SetChildIndex((System.Windows.Forms.Control) this.panelControls, 0);
    this.panelTop.Size = new Size(466, 22);
    this.buttonDefault.Location = new Point(1238, 0);
    this.panelExpansion.Size = new Size(466, 372);
    this.groupBoxBase.Controls.Add((System.Windows.Forms.Control) this.buttonGenerate);
    this.groupBoxBase.Controls.Add((System.Windows.Forms.Control) this.numericUpDownPlayer);
    this.groupBoxBase.Controls.Add((System.Windows.Forms.Control) this.label3);
    this.groupBoxBase.Controls.Add((System.Windows.Forms.Control) this.buttonOpen);
    this.groupBoxBase.Controls.Add((System.Windows.Forms.Control) this.textBoxUID);
    this.groupBoxBase.Controls.Add((System.Windows.Forms.Control) this.label2);
    this.groupBoxBase.Controls.Add((System.Windows.Forms.Control) this.label1);
    this.groupBoxBase.Controls.Add((System.Windows.Forms.Control) this.textBoxDefType);
    this.groupBoxBase.Dock = DockStyle.Fill;
    this.groupBoxBase.Location = new Point(10, 10);
    this.groupBoxBase.Name = "groupBoxBase";
    this.groupBoxBase.Size = new Size(422, 108);
    this.groupBoxBase.TabIndex = 1;
    this.groupBoxBase.TabStop = false;
    this.groupBoxBase.Text = "Base Information";
    this.buttonGenerate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonGenerate.Location = new Point(354, 47);
    this.buttonGenerate.Name = "buttonGenerate";
    this.buttonGenerate.Size = new Size(62, 21);
    this.buttonGenerate.TabIndex = 8;
    this.buttonGenerate.Text = "Generate";
    this.buttonGenerate.UseVisualStyleBackColor = true;
    this.buttonGenerate.Click += new EventHandler(this.buttonGenerate_Click);
    this.numericUpDownPlayer.Location = new Point(91, 76);
    this.numericUpDownPlayer.Maximum = new Decimal(new int[4]
    {
      4,
      0,
      0,
      0
    });
    this.numericUpDownPlayer.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      int.MinValue
    });
    this.numericUpDownPlayer.Name = "numericUpDownPlayer";
    this.numericUpDownPlayer.Size = new Size(38, 20);
    this.numericUpDownPlayer.TabIndex = 6;
    this.label3.AutoSize = true;
    this.label3.Location = new Point(6, 78);
    this.label3.Name = "label3";
    this.label3.Size = new Size(39, 13);
    this.label3.TabIndex = 5;
    this.label3.Text = "Player:";
    this.buttonOpen.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonOpen.Location = new Point(354, 21);
    this.buttonOpen.Name = "buttonOpen";
    this.buttonOpen.Size = new Size(62, 21);
    this.buttonOpen.TabIndex = 4;
    this.buttonOpen.Text = "Open";
    this.buttonOpen.UseVisualStyleBackColor = true;
    this.buttonOpen.Click += new EventHandler(this.buttonOpen_Click);
    this.textBoxUID.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.textBoxUID.Location = new Point(91, 48 /*0x30*/);
    this.textBoxUID.Name = "textBoxUID";
    this.textBoxUID.Size = new Size(257, 20);
    this.textBoxUID.TabIndex = 3;
    this.label2.AutoSize = true;
    this.label2.Location = new Point(6, 51);
    this.label2.Name = "label2";
    this.label2.Size = new Size(29, 13);
    this.label2.TabIndex = 2;
    this.label2.Text = "UID:";
    this.label1.AutoSize = true;
    this.label1.Location = new Point(6, 25);
    this.label1.Name = "label1";
    this.label1.Size = new Size(81, 13);
    this.label1.TabIndex = 1;
    this.label1.Text = "Definition Type:";
    this.textBoxDefType.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.textBoxDefType.Location = new Point(91, 22);
    this.textBoxDefType.Name = "textBoxDefType";
    this.textBoxDefType.ReadOnly = true;
    this.textBoxDefType.Size = new Size(257, 20);
    this.textBoxDefType.TabIndex = 0;
    this.panelBase.Controls.Add((System.Windows.Forms.Control) this.groupBoxBase);
    this.panelBase.Dock = DockStyle.Top;
    this.panelBase.Location = new Point(0, 0);
    this.panelBase.Name = "panelBase";
    this.panelBase.Padding = new Padding(10);
    this.panelBase.Size = new Size(442, 128 /*0x80*/);
    this.panelBase.TabIndex = 3;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (ControlThing);
    this.Size = new Size(466, 394);
    this.panelControlsBase.ResumeLayout(false);
    this.panelTop.ResumeLayout(false);
    this.panelTop.PerformLayout();
    this.panelExpansion.ResumeLayout(false);
    this.groupBoxBase.ResumeLayout(false);
    this.groupBoxBase.PerformLayout();
    this.numericUpDownPlayer.EndInit();
    this.panelBase.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public event ThingChangedHandler ThingChanged;

  public ControlThing() => this.InitializeComponent();

  public ControlThing(Thing thing)
  {
    this.InitializeComponent();
    this.Thing = thing;
  }

  public Thing Thing
  {
    get => this.myThing;
    set
    {
      if (this.myThing == value)
        return;
      this.panelControls.Controls.Clear();
      this.Collapse();
      this.myThing = value;
      this.SuspendLayout();
      this.Setup((ComplexBlock) this.myThing);
      this.labelName.Text = $"{this.myThing.Name} [{this.myThing.DefinitionType}]";
      this.textBoxDefType.Text = this.myThing.DefinitionType;
      this.textBoxUID.Text = this.myThing.UID;
      this.numericUpDownPlayer.Value = (Decimal) this.myThing.Player;
      this.Expand();
      this.ResumeLayout(true);
    }
  }

  protected override void OnBlockChanged()
  {
    if (this.ThingChanged != null)
      this.ThingChanged(this.myThing);
    base.OnBlockChanged();
  }

  protected override int GetControlsHeight()
  {
    int controlsHeight = base.GetControlsHeight();
    return this.Collapsed ? controlsHeight : controlsHeight + this.panelBase.Height;
  }

  private void buttonOpen_Click(object sender, EventArgs e)
  {
    ContentManager instance = ContentManager.Instance;
    ContentObject entry = instance.FindEntry(LinkDestination.GameBINEntryName, (object) this.myThing.DefinitionType);
    if (entry == null)
      return;
    instance.ShowEntry(entry.Object, false);
  }

  private void buttonGenerate_Click(object sender, EventArgs e)
  {
    this.myThing.UID = UIDManager.Generate();
    this.textBoxUID.Text = this.myThing.UID;
  }
}
