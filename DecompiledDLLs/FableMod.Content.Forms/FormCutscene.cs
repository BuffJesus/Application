// Decompiled with JetBrains decompiler
// Type: FableMod.Content.Forms.FormCutscene
// Assembly: FableMod.Content.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A575F982-23D4-4DAE-A571-3D769462C59B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Content.Forms.dll

using FableMod.BIN;
using FableMod.ContentManagement;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Content.Forms;

public class FormCutscene : Form
{
  private IContainer components;
  private TabControl tabControl1;
  private TabPage tabPage1;
  private TabPage tabPage2;
  private TabPage tabPage3;
  private TextBox textBox1;
  private TextBox textBox2;
  private TextBox textBox3;
  private BINEntry myCutscene;
  private DefinitionType myDefType;
  private FableMod.ContentManagement.Control myMode1;
  private FableMod.ContentManagement.Control myMode2;
  private FableMod.ContentManagement.Control myMode3;
  private bool myModified;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.tabControl1 = new TabControl();
    this.tabPage1 = new TabPage();
    this.tabPage2 = new TabPage();
    this.tabPage3 = new TabPage();
    this.textBox1 = new TextBox();
    this.textBox2 = new TextBox();
    this.textBox3 = new TextBox();
    this.tabControl1.SuspendLayout();
    this.tabPage1.SuspendLayout();
    this.tabPage2.SuspendLayout();
    this.tabPage3.SuspendLayout();
    this.SuspendLayout();
    this.tabControl1.Controls.Add((System.Windows.Forms.Control) this.tabPage1);
    this.tabControl1.Controls.Add((System.Windows.Forms.Control) this.tabPage2);
    this.tabControl1.Controls.Add((System.Windows.Forms.Control) this.tabPage3);
    this.tabControl1.Dock = DockStyle.Fill;
    this.tabControl1.Location = new Point(0, 0);
    this.tabControl1.Name = "tabControl1";
    this.tabControl1.SelectedIndex = 0;
    this.tabControl1.Size = new Size(632, 393);
    this.tabControl1.TabIndex = 2;
    this.tabPage1.Controls.Add((System.Windows.Forms.Control) this.textBox1);
    this.tabPage1.Location = new Point(4, 22);
    this.tabPage1.Name = "tabPage1";
    this.tabPage1.Padding = new Padding(3);
    this.tabPage1.Size = new Size(624, 367);
    this.tabPage1.TabIndex = 0;
    this.tabPage1.Text = "Mode 1";
    this.tabPage1.UseVisualStyleBackColor = true;
    this.tabPage2.Controls.Add((System.Windows.Forms.Control) this.textBox2);
    this.tabPage2.Location = new Point(4, 22);
    this.tabPage2.Name = "tabPage2";
    this.tabPage2.Padding = new Padding(3);
    this.tabPage2.Size = new Size(497, 325);
    this.tabPage2.TabIndex = 1;
    this.tabPage2.Text = "Mode 2";
    this.tabPage2.UseVisualStyleBackColor = true;
    this.tabPage3.Controls.Add((System.Windows.Forms.Control) this.textBox3);
    this.tabPage3.Location = new Point(4, 22);
    this.tabPage3.Name = "tabPage3";
    this.tabPage3.Padding = new Padding(3);
    this.tabPage3.Size = new Size(497, 325);
    this.tabPage3.TabIndex = 2;
    this.tabPage3.Text = "Mode 3";
    this.tabPage3.UseVisualStyleBackColor = true;
    this.textBox1.Dock = DockStyle.Fill;
    this.textBox1.Font = new Font("Courier New", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.textBox1.Location = new Point(3, 3);
    this.textBox1.Multiline = true;
    this.textBox1.Name = "textBox1";
    this.textBox1.ScrollBars = ScrollBars.Vertical;
    this.textBox1.Size = new Size(618, 361);
    this.textBox1.TabIndex = 0;
    this.textBox1.WordWrap = false;
    this.textBox1.TextChanged += new EventHandler(this.textBox_Changed);
    this.textBox1.KeyDown += new KeyEventHandler(this.textBox_KeyDown);
    this.textBox2.Dock = DockStyle.Fill;
    this.textBox2.Font = new Font("Courier New", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.textBox2.Location = new Point(3, 3);
    this.textBox2.Multiline = true;
    this.textBox2.Name = "textBox2";
    this.textBox2.ScrollBars = ScrollBars.Vertical;
    this.textBox2.Size = new Size(491, 319);
    this.textBox2.TabIndex = 1;
    this.textBox2.WordWrap = false;
    this.textBox3.Dock = DockStyle.Fill;
    this.textBox3.Font = new Font("Courier New", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.textBox3.Location = new Point(3, 3);
    this.textBox3.Multiline = true;
    this.textBox3.Name = "textBox3";
    this.textBox3.ScrollBars = ScrollBars.Vertical;
    this.textBox3.Size = new Size(491, 319);
    this.textBox3.TabIndex = 1;
    this.textBox3.WordWrap = false;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(632, 393);
    this.Controls.Add((System.Windows.Forms.Control) this.tabControl1);
    this.Name = nameof (FormCutscene);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = nameof (FormCutscene);
    this.FormClosing += new FormClosingEventHandler(this.FormCutscene_FormClosing);
    this.tabControl1.ResumeLayout(false);
    this.tabPage1.ResumeLayout(false);
    this.tabPage1.PerformLayout();
    this.tabPage2.ResumeLayout(false);
    this.tabPage2.PerformLayout();
    this.tabPage3.ResumeLayout(false);
    this.tabPage3.PerformLayout();
    this.ResumeLayout(false);
  }

  public FormCutscene(BINEntry cutscene)
  {
    this.InitializeComponent();
    this.myCutscene = cutscene;
    this.Text = "Cutscene: " + this.myCutscene.Name;
    this.myDefType = ContentManager.Instance.Definitions.GetDefinition(this.myCutscene.Definition);
    this.myDefType.ReadIn(this.myCutscene);
    this.myMode1 = this.myDefType.FindControl(1511943337U);
    this.myMode2 = this.myDefType.FindControl(3251288831U);
    this.myMode3 = this.myDefType.FindControl(1338197266U);
    this.textBox1.Text = this.GetScript(this.myMode1);
    this.textBox2.Text = this.GetScript(this.myMode2);
    this.textBox3.Text = this.GetScript(this.myMode3);
    this.myModified = false;
  }

  private string GetScript(FableMod.ContentManagement.Control c)
  {
    if (c == null)
      return "";
    Member member1 = (Member) c.Members[0];
    ArrayMember member2 = (ArrayMember) c.Members[1];
    StringBuilder stringBuilder = new StringBuilder();
    uint num = (uint) member1.Value;
    for (int index = 0; (long) index < (long) num; ++index)
    {
      Member member3 = (Member) member2.Elements[index][0];
      stringBuilder.AppendLine(member3.Value.ToString());
    }
    return stringBuilder.ToString();
  }

  private void ApplyScript(FableMod.ContentManagement.Control c, string script)
  {
    string[] strArray = script.Split(new string[1]{ "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
    Member member1 = (Member) c.Members[0];
    ArrayMember member2 = (ArrayMember) c.Members[1];
    member1.Value = (object) (uint) strArray.Length;
    member2.Elements.Clear();
    for (int index = 0; index < strArray.Length; ++index)
    {
      MemberCollection element = member2.CreateElement();
      ((Member) element[0]).Value = (object) strArray[index];
      member2.Elements.Add(element);
    }
  }

  private void ApplyChanges()
  {
    if (!this.myModified)
      return;
    this.ApplyScript(this.myMode1, this.textBox1.Text);
    this.ApplyScript(this.myMode2, this.textBox2.Text);
    this.ApplyScript(this.myMode3, this.textBox3.Text);
    this.myDefType.Write(this.myCutscene);
    this.myModified = false;
  }

  private void textBox_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Return)
      return;
    this.ApplyChanges();
  }

  private void FormCutscene_FormClosing(object sender, FormClosingEventArgs e)
  {
    this.ApplyChanges();
  }

  private void textBox_Changed(object sender, EventArgs e) => this.myModified = true;
}
