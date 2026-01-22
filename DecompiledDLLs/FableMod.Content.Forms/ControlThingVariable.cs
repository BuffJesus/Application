// Decompiled with JetBrains decompiler
// Type: FableMod.Content.Forms.ControlThingVariable
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

public class ControlThingVariable : ControlTNGElement
{
  private IContainer components;
  private Label labelName;
  private Button buttonDefault;
  private Variable myVariable;
  private System.Windows.Forms.Control myValueControl;
  private Button myButton;
  private static int SPACE_LC = 5;
  private static int SPACE_LB = 5;
  public SelectUIDHandler SelectUID;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.labelName = new Label();
    this.buttonDefault = new Button();
    this.SuspendLayout();
    this.labelName.BackColor = SystemColors.Control;
    this.labelName.Font = new Font("Microsoft Sans Serif", 7f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.labelName.Location = new Point(0, 4);
    this.labelName.Name = "labelName";
    this.labelName.Size = new Size(165, 13);
    this.labelName.TabIndex = 0;
    this.labelName.Text = "Variable Name";
    this.buttonDefault.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonDefault.Location = new Point(335, 0);
    this.buttonDefault.Name = "buttonDefault";
    this.buttonDefault.Size = new Size(35, 21);
    this.buttonDefault.TabIndex = 1;
    this.buttonDefault.TabStop = false;
    this.buttonDefault.Text = "Def";
    this.buttonDefault.UseVisualStyleBackColor = true;
    this.buttonDefault.Click += new EventHandler(this.buttonDefault_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.Controls.Add((System.Windows.Forms.Control) this.buttonDefault);
    this.Controls.Add((System.Windows.Forms.Control) this.labelName);
    this.Name = nameof (ControlThingVariable);
    this.Size = new Size(370, 22);
    this.ResumeLayout(false);
  }

  public ControlThingVariable(Variable variable)
  {
    this.InitializeComponent();
    this.myVariable = variable;
    this.labelName.Text = this.myVariable.Name + ":";
    this.buttonDefault.Enabled = variable.HasDefault;
    this.SuspendLayout();
    int num = this.labelName.Location.X + this.labelName.Width + ControlThingVariable.SPACE_LC;
    int x = this.buttonDefault.Location.X;
    switch (this.myVariable.Type)
    {
      case VariableType.Unknown:
      case VariableType.String:
      case VariableType.QuoteString:
      case VariableType.Float:
      case VariableType.Integer:
        TextBox textBox1 = new TextBox();
        this.Controls.Add((System.Windows.Forms.Control) textBox1);
        textBox1.Left = num;
        textBox1.Width = x - num - ControlThingVariable.SPACE_LB;
        textBox1.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        textBox1.Text = this.myVariable.StringValue;
        textBox1.Leave += new EventHandler(this.textBox_Leave);
        this.myValueControl = (System.Windows.Forms.Control) textBox1;
        break;
      case VariableType.Boolean:
        CheckBox checkBox = new CheckBox();
        this.Controls.Add((System.Windows.Forms.Control) checkBox);
        checkBox.AutoSize = false;
        checkBox.Left = num;
        checkBox.Width = x - num - ControlThingVariable.SPACE_LB;
        checkBox.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        checkBox.Checked = (bool) this.myVariable.Value;
        checkBox.CheckedChanged += new EventHandler(this.checkBox_CheckedChanged);
        this.myValueControl = (System.Windows.Forms.Control) checkBox;
        break;
      case VariableType.UID:
        Button button1 = new Button();
        this.Controls.Add((System.Windows.Forms.Control) button1);
        button1.Text = "...";
        button1.Width = this.buttonDefault.Width;
        button1.Height = this.buttonDefault.Height;
        button1.Left = x - ControlThingVariable.SPACE_LB - button1.Width;
        button1.Click += new EventHandler(this.button_Click);
        TextBox textBox2 = new TextBox();
        this.Controls.Add((System.Windows.Forms.Control) textBox2);
        textBox2.Left = num;
        button1.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        textBox2.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        textBox2.Text = this.myVariable.StringValue;
        textBox2.Width = button1.Left - num - ControlThingVariable.SPACE_LB;
        textBox2.Leave += new EventHandler(this.textBox_Leave);
        this.myValueControl = (System.Windows.Forms.Control) textBox2;
        this.myButton = button1;
        break;
      case VariableType.GameEnum:
        Button button2 = new Button();
        this.Controls.Add((System.Windows.Forms.Control) button2);
        button2.Text = "...";
        button2.Width = this.buttonDefault.Width;
        button2.Height = this.buttonDefault.Height;
        button2.Left = x - ControlThingVariable.SPACE_LB - button2.Width;
        button2.Click += new EventHandler(this.button_Click);
        TextBox textBox3 = new TextBox();
        this.Controls.Add((System.Windows.Forms.Control) textBox3);
        textBox3.Left = num;
        button2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        textBox3.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
        textBox3.Text = this.myVariable.StringValue;
        textBox3.Width = button2.Left - num - ControlThingVariable.SPACE_LB;
        textBox3.Leave += new EventHandler(this.textBox_Leave);
        this.myValueControl = (System.Windows.Forms.Control) textBox3;
        this.myButton = button2;
        break;
    }
    this.ResumeLayout(true);
    this.UpdateFont();
    this.myVariable.Changed += new ElementChangedHandler(this.variable_Changed);
  }

  public override void ApplySelectUID(SelectUIDHandler handler)
  {
    this.SelectUID += handler;
    base.ApplySelectUID(handler);
  }

  public override int TotalHeight => this.Height;

  private void UpdateFont()
  {
    if (!this.myVariable.HasDefault)
      return;
    if (this.myVariable.IsDefault() && this.labelName.Font.Bold)
    {
      this.labelName.Font = new Font(this.labelName.Font, FontStyle.Regular);
    }
    else
    {
      if (this.myVariable.IsDefault() || this.labelName.Font.Bold)
        return;
      this.labelName.Font = new Font(this.labelName.Font, FontStyle.Bold);
    }
  }

  protected void variable_Changed(Element element)
  {
    this.UpdateFont();
    if (this.myVariable.Type == VariableType.Boolean)
      ((CheckBox) this.myValueControl).Checked = (bool) this.myVariable.Value;
    else
      this.myValueControl.Text = this.myVariable.StringValue;
  }

  private void textBox_Leave(object sender, EventArgs e)
  {
    TextBox textBox = (TextBox) sender;
    try
    {
      this.myVariable.Value = (object) textBox.Text;
    }
    catch (Exception ex)
    {
      textBox.Text = this.myVariable.StringValue;
    }
  }

  private void checkBox_CheckedChanged(object sender, EventArgs e)
  {
    this.myVariable.Value = ((CheckBox) sender).Checked ? (object) "TRUE" : (object) "FALSE";
  }

  private void button_Click(object sender, EventArgs e)
  {
    if (this.myVariable.Type == VariableType.GameEnum)
    {
      object obj = ContentManager.Instance.SelectEntry(new FableMod.ContentManagement.Link(LinkDestination.GameBINEntryName, this.myVariable.Restriction), this.myVariable.Value);
      if (obj == null)
        return;
      this.myVariable.Value = (object) obj.ToString();
    }
    else
    {
      if (this.SelectUID == null)
        return;
      this.SelectUID(this.myVariable);
    }
  }

  public int LabelWidth
  {
    get => this.labelName.Width;
    set
    {
      this.SuspendLayout();
      this.labelName.AutoSize = false;
      this.labelName.Width = value;
      int num = this.labelName.Location.X + this.labelName.Width + ControlThingVariable.SPACE_LC;
      this.myValueControl.Left = num;
      this.myValueControl.Width = this.myVariable.Type == VariableType.GameEnum || this.myVariable.Type == VariableType.UID ? this.myButton.Location.X - num - ControlThingVariable.SPACE_LB : this.buttonDefault.Location.X - num - ControlThingVariable.SPACE_LB;
      this.ResumeLayout(true);
    }
  }

  private void buttonDefault_Click(object sender, EventArgs e) => this.myVariable.ToDefault();
}
