// Decompiled with JetBrains decompiler
// Type: FableMod.Content.Forms.ControlComplexBlock
// Assembly: FableMod.Content.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A575F982-23D4-4DAE-A571-3D769462C59B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Content.Forms.dll

using FableMod.TNG;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Content.Forms;

public class ControlComplexBlock : ControlTNGElement
{
  protected bool myCollapsed = true;
  protected ComplexBlock myBlock;
  private IContainer components;
  private Panel panelTab;
  protected internal Panel panelControls;
  protected internal Label labelName;
  protected internal Panel panelControlsBase;
  protected internal Panel panelTop;
  protected internal Button buttonDefault;
  protected internal Panel panelExpansion;
  protected internal Button buttonExpand;

  public event ControlsUpdatedHandler ControlsUpdated;

  public ControlComplexBlock() => this.InitializeComponent();

  public override void ApplySelectUID(SelectUIDHandler handler)
  {
    Control.ControlCollection controls = this.panelControls.Controls;
    for (int index = 0; index < controls.Count; ++index)
      ((ControlTNGElement) controls[index]).ApplySelectUID(handler);
  }

  protected void AddElement(Element element)
  {
    ControlTNGElement controlTngElement1 = (ControlTNGElement) null;
    ControlTNGElement controlTngElement2;
    if (element.GetType() == typeof (Variable))
      controlTngElement2 = (ControlTNGElement) new ControlThingVariable((Variable) element);
    else if (element.GetType() == typeof (ElementArray))
    {
      ControlElementArray controlElementArray = new ControlElementArray();
      controlElementArray.Setup((ComplexBlock) element);
      controlElementArray.ControlsUpdated += new ControlsUpdatedHandler(this.array_ControlsUpdated);
      controlTngElement2 = (ControlTNGElement) controlElementArray;
    }
    else
    {
      ControlComplexBlock controlComplexBlock = new ControlComplexBlock();
      this.panelControls.Controls.Add((Control) controlTngElement1);
      controlComplexBlock.Setup((ComplexBlock) element);
      controlComplexBlock.ControlsUpdated += new ControlsUpdatedHandler(this.array_ControlsUpdated);
      controlTngElement2 = (ControlTNGElement) controlComplexBlock;
    }
    this.panelControls.Controls.Add((Control) controlTngElement2);
    controlTngElement2.Dock = DockStyle.Top;
    controlTngElement2.BringToFront();
  }

  protected void UpdateLabelWidths()
  {
    Control.ControlCollection controls = this.panelControls.Controls;
    int num = 0;
    int count = controls.Count;
    for (int index = 0; index < count; ++index)
    {
      if (controls[index].GetType() == typeof (ControlThingVariable))
      {
        ControlThingVariable controlThingVariable = (ControlThingVariable) controls[index];
        if (num < controlThingVariable.LabelWidth)
          num = controlThingVariable.LabelWidth;
      }
    }
    for (int index = 0; index < count; ++index)
    {
      if (controls[index].GetType() == typeof (ControlThingVariable))
        ((ControlThingVariable) controls[index]).LabelWidth = num;
    }
  }

  public void Setup(ComplexBlock block)
  {
    this.labelName.Text = block.Name;
    this.SuspendLayout();
    for (int index = 0; index < block.ElementCount; ++index)
      this.AddElement(block.get_Elements(index));
    this.UpdateLabelWidths();
    this.myBlock = block;
    this.Height = this.panelTop.Height;
    this.panelExpansion.Visible = false;
    this.buttonExpand.Enabled = this.panelControls.Controls.Count > 0;
    this.buttonDefault.Enabled = block.HasDefault;
    this.UpdateFont();
    this.myBlock.Changed += new ElementChangedHandler(this.element_Changed);
    this.ResumeLayout(true);
  }

  protected virtual int GetControlsHeight()
  {
    int controlsHeight = 0;
    if (this.myCollapsed)
      return controlsHeight;
    Control.ControlCollection controls = this.panelControls.Controls;
    int count = controls.Count;
    for (int index = 0; index < count; ++index)
      controlsHeight += ((ControlTNGElement) controls[index]).TotalHeight;
    return controlsHeight;
  }

  private void UpdateFont()
  {
    if (!this.myBlock.HasDefault)
      return;
    if (this.myBlock.IsDefault() && this.labelName.Font.Bold)
    {
      this.labelName.Font = new Font(this.labelName.Font, FontStyle.Regular);
    }
    else
    {
      if (this.myBlock.IsDefault() || this.labelName.Font.Bold)
        return;
      this.labelName.Font = new Font(this.labelName.Font, FontStyle.Bold);
    }
  }

  public override int TotalHeight => this.GetControlsHeight() + this.panelTop.Height;

  public bool Collapsed => this.myCollapsed;

  protected virtual void OnBlockChanged()
  {
  }

  protected void element_Changed(Element element)
  {
    this.UpdateFont();
    this.OnBlockChanged();
  }

  protected void OnControlsUpdated()
  {
    if (this.ControlsUpdated == null)
      return;
    this.ControlsUpdated();
  }

  protected void array_ControlsUpdated()
  {
    this.UpdateHeight();
    this.OnControlsUpdated();
  }

  protected void UpdateHeight()
  {
    if (this.myCollapsed)
    {
      this.panelExpansion.Visible = false;
      this.panelExpansion.Height = 0;
    }
    else
    {
      this.panelExpansion.Visible = true;
      this.panelExpansion.Height = this.GetControlsHeight();
    }
    this.Height = this.TotalHeight;
  }

  public void Expand()
  {
    if (!this.Collapsed)
      return;
    this.OnExpand();
  }

  public void Collapse()
  {
    if (this.Collapsed)
      return;
    this.OnCollapse();
  }

  protected virtual void OnExpand()
  {
    this.SuspendLayout();
    this.myCollapsed = false;
    this.buttonExpand.Text = "-";
    this.array_ControlsUpdated();
    this.ResumeLayout(true);
  }

  protected virtual void OnCollapse()
  {
    this.SuspendLayout();
    this.myCollapsed = true;
    this.buttonExpand.Text = "+";
    this.array_ControlsUpdated();
    this.ResumeLayout(true);
  }

  private void buttonExpand_Click(object sender, EventArgs e)
  {
    if (this.buttonExpand.Text == "+")
      this.OnExpand();
    else
      this.OnCollapse();
  }

  protected virtual void ToDefault() => this.myBlock.ToDefault();

  private void buttonDefault_Click(object sender, EventArgs e) => this.ToDefault();

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.panelTop = new Panel();
    this.labelName = new Label();
    this.buttonExpand = new Button();
    this.buttonDefault = new Button();
    this.panelTab = new Panel();
    this.panelControls = new Panel();
    this.panelExpansion = new Panel();
    this.panelControlsBase = new Panel();
    this.panelTop.SuspendLayout();
    this.panelExpansion.SuspendLayout();
    this.panelControlsBase.SuspendLayout();
    this.SuspendLayout();
    this.panelTop.BackColor = SystemColors.Control;
    this.panelTop.Controls.Add((Control) this.labelName);
    this.panelTop.Controls.Add((Control) this.buttonExpand);
    this.panelTop.Controls.Add((Control) this.buttonDefault);
    this.panelTop.Dock = DockStyle.Top;
    this.panelTop.Location = new Point(0, 0);
    this.panelTop.Name = "panelTop";
    this.panelTop.Padding = new Padding(0, 0, 11, 0);
    this.panelTop.Size = new Size(301, 22);
    this.panelTop.TabIndex = 0;
    this.labelName.AutoSize = true;
    this.labelName.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.labelName.Location = new Point(24, 4);
    this.labelName.Name = "labelName";
    this.labelName.Size = new Size(34, 13);
    this.labelName.TabIndex = 3;
    this.labelName.Text = "Name";
    this.buttonExpand.Location = new Point(0, 0);
    this.buttonExpand.Name = "buttonExpand";
    this.buttonExpand.Size = new Size(21, 21);
    this.buttonExpand.TabIndex = 2;
    this.buttonExpand.TabStop = false;
    this.buttonExpand.Text = "+";
    this.buttonExpand.UseVisualStyleBackColor = true;
    this.buttonExpand.Click += new EventHandler(this.buttonExpand_Click);
    this.buttonDefault.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonDefault.Location = new Point(266, 0);
    this.buttonDefault.Name = "buttonDefault";
    this.buttonDefault.Size = new Size(35, 21);
    this.buttonDefault.TabIndex = 2;
    this.buttonDefault.TabStop = false;
    this.buttonDefault.Text = "Def";
    this.buttonDefault.UseVisualStyleBackColor = true;
    this.buttonDefault.Click += new EventHandler(this.buttonDefault_Click);
    this.panelTab.Dock = DockStyle.Left;
    this.panelTab.Location = new Point(0, 0);
    this.panelTab.Name = "panelTab";
    this.panelTab.Size = new Size(24, 226);
    this.panelTab.TabIndex = 1;
    this.panelControls.AutoScroll = true;
    this.panelControls.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.panelControls.Dock = DockStyle.Fill;
    this.panelControls.Location = new Point(0, 0);
    this.panelControls.Name = "panelControls";
    this.panelControls.Size = new Size(277, 226);
    this.panelControls.TabIndex = 2;
    this.panelExpansion.Controls.Add((Control) this.panelControlsBase);
    this.panelExpansion.Controls.Add((Control) this.panelTab);
    this.panelExpansion.Dock = DockStyle.Fill;
    this.panelExpansion.Location = new Point(0, 22);
    this.panelExpansion.Name = "panelExpansion";
    this.panelExpansion.Size = new Size(301, 226);
    this.panelExpansion.TabIndex = 3;
    this.panelExpansion.Visible = false;
    this.panelControlsBase.Controls.Add((Control) this.panelControls);
    this.panelControlsBase.Dock = DockStyle.Fill;
    this.panelControlsBase.Location = new Point(24, 0);
    this.panelControlsBase.Name = "panelControlsBase";
    this.panelControlsBase.Size = new Size(277, 226);
    this.panelControlsBase.TabIndex = 3;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.Controls.Add((Control) this.panelExpansion);
    this.Controls.Add((Control) this.panelTop);
    this.Name = nameof (ControlComplexBlock);
    this.Size = new Size(301, 248);
    this.panelTop.ResumeLayout(false);
    this.panelTop.PerformLayout();
    this.panelExpansion.ResumeLayout(false);
    this.panelControlsBase.ResumeLayout(false);
    this.ResumeLayout(false);
  }
}
