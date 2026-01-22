// Decompiled with JetBrains decompiler
// Type: FableMod.Content.Forms.ControlElementArray
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

public class ControlElementArray : ControlComplexBlock
{
  private IContainer components;
  private Button buttonAdd;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.buttonAdd = new Button();
    this.panelControlsBase.SuspendLayout();
    this.panelTop.SuspendLayout();
    this.panelExpansion.SuspendLayout();
    this.SuspendLayout();
    this.panelTop.Controls.Add((Control) this.buttonAdd);
    this.panelTop.Controls.SetChildIndex((Control) this.buttonAdd, 0);
    this.panelTop.Controls.SetChildIndex((Control) this.buttonExpand, 0);
    this.panelTop.Controls.SetChildIndex((Control) this.buttonDefault, 0);
    this.panelTop.Controls.SetChildIndex((Control) this.labelName, 0);
    this.buttonDefault.Text = "Clr";
    this.buttonAdd.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonAdd.Location = new Point(226, 1);
    this.buttonAdd.Name = "buttonAdd";
    this.buttonAdd.Size = new Size(35, 21);
    this.buttonAdd.TabIndex = 4;
    this.buttonAdd.Text = "Add";
    this.buttonAdd.UseVisualStyleBackColor = true;
    this.buttonAdd.Click += new EventHandler(this.buttonAdd_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (ControlElementArray);
    this.panelControlsBase.ResumeLayout(false);
    this.panelTop.ResumeLayout(false);
    this.panelTop.PerformLayout();
    this.panelExpansion.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  public ControlElementArray() => this.InitializeComponent();

  protected override void ToDefault()
  {
    this.panelControls.Controls.Clear();
    this.myBlock.Clear();
    this.buttonDefault.Enabled = false;
    this.buttonExpand.Enabled = false;
    this.buttonExpand.Text = "+";
    this.myCollapsed = true;
    this.UpdateHeight();
  }

  private void buttonAdd_Click(object sender, EventArgs e)
  {
    this.AddElement(((ElementArray) this.myBlock).Add());
    this.panelControls.SuspendLayout();
    this.panelControls.Controls[this.panelControls.Controls.Count - 1].BringToFront();
    this.panelControls.ResumeLayout(true);
    bool flag = this.panelControls.Controls.Count > 0;
    this.buttonExpand.Enabled = flag;
    this.buttonDefault.Enabled = flag;
    this.array_ControlsUpdated();
  }
}
