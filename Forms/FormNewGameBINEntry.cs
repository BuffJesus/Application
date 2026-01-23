// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormNewGameBINEntry
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIN;
using FableMod.ContentManagement;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormNewGameBINEntry : Form
{
  private BINFile myBIN;
  private bool myRenewTemplates = true;
  private IContainer components;
  private Label lblSymbolName;
  private Label lblDefinitionType;
  private TextBox textBoxName;
  private Button buttonCreate;
  private Button buttonCancel;
  private Label label1;
  private ComboBox comboBoxTemplates;
  private ComboBox comboBoxDefs;

  public FormNewGameBINEntry(BINFile bin)
  {
    this.InitializeComponent();
    ThemeManager.ApplyTheme(this);
    this.myBIN = bin;
    foreach (DefinitionType definition in FileDatabase.Instance.Definitions.GetDefinitions())
      this.comboBoxDefs.Items.Add((object) definition.Name);
  }

  public string Template => this.comboBoxTemplates.Text;

  public string Definition => this.comboBoxDefs.Text;

  public string SymbolName => this.textBoxName.Text;

  private void buttonCreate_Click(object sender, EventArgs e)
  {
    if (this.SymbolName == "" || this.Definition == "" || this.Template == "")
    {
      int num1 = (int) FormMain.Instance.ErrorMessage("Missing information.");
    }
    else if (this.myBIN.GetEntryByName(this.SymbolName) != null)
    {
      int num2 = (int) FormMain.Instance.ErrorMessage("Entry already exists.");
    }
    else
      this.DialogResult = DialogResult.OK;
  }

  private void buttonCancel_Click(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.Cancel;
  }

  private void comboBoxTemplates_DropDown(object sender, EventArgs e)
  {
    if (!this.myRenewTemplates)
      return;
    this.myRenewTemplates = false;
    BINEntry[] entriesByDefinition = this.myBIN.GetEntriesByDefinition(this.Definition);
    this.comboBoxTemplates.Items.Clear();
    foreach (BINEntry binEntry in entriesByDefinition)
    {
      if (binEntry.Name != "")
        this.comboBoxTemplates.Items.Add((object) binEntry.Name);
    }
  }

  private void comboBoxDefs_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.comboBoxTemplates.Enabled = this.comboBoxDefs.SelectedIndex >= 0;
    this.myRenewTemplates = true;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.lblSymbolName = new Label();
    this.lblDefinitionType = new Label();
    this.textBoxName = new TextBox();
    this.buttonCreate = new Button();
    this.buttonCancel = new Button();
    this.label1 = new Label();
    this.comboBoxTemplates = new ComboBox();
    this.comboBoxDefs = new ComboBox();
    this.SuspendLayout();
    this.lblSymbolName.AutoSize = true;
    this.lblSymbolName.Location = new Point(12, 14);
    this.lblSymbolName.Name = "lblSymbolName";
    this.lblSymbolName.Size = new Size(75, 13);
    this.lblSymbolName.TabIndex = 2;
    this.lblSymbolName.Text = "Symbol Name:";
    this.lblDefinitionType.AutoSize = true;
    this.lblDefinitionType.Location = new Point(12, 39);
    this.lblDefinitionType.Name = "lblDefinitionType";
    this.lblDefinitionType.Size = new Size(81, 13);
    this.lblDefinitionType.TabIndex = 3;
    this.lblDefinitionType.Text = "Definition Type:";
    this.textBoxName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.textBoxName.Location = new Point(99, 11);
    this.textBoxName.Name = "textBoxName";
    this.textBoxName.Size = new Size(296, 20);
    this.textBoxName.TabIndex = 0;
    this.buttonCreate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonCreate.Location = new Point(239, 93);
    this.buttonCreate.Name = "buttonCreate";
    this.buttonCreate.Size = new Size(75, 23);
    this.buttonCreate.TabIndex = 3;
    this.buttonCreate.Text = "Create";
    this.buttonCreate.UseVisualStyleBackColor = true;
    this.buttonCreate.Click += new EventHandler(this.buttonCreate_Click);
    this.buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Location = new Point(320, 93);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(75, 23);
    this.buttonCancel.TabIndex = 4;
    this.buttonCancel.Text = "Cancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.buttonCancel.Click += new EventHandler(this.buttonCancel_Click);
    this.label1.AutoSize = true;
    this.label1.Location = new Point(12, 64 /*0x40*/);
    this.label1.Name = "label1";
    this.label1.Size = new Size(54, 13);
    this.label1.TabIndex = 7;
    this.label1.Text = "Template:";
    this.comboBoxTemplates.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    this.comboBoxTemplates.AutoCompleteSource = AutoCompleteSource.ListItems;
    this.comboBoxTemplates.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBoxTemplates.Enabled = false;
    this.comboBoxTemplates.FormattingEnabled = true;
    this.comboBoxTemplates.Location = new Point(99, 61);
    this.comboBoxTemplates.MaxDropDownItems = 16 /*0x10*/;
    this.comboBoxTemplates.Name = "comboBoxTemplates";
    this.comboBoxTemplates.Size = new Size(296, 21);
    this.comboBoxTemplates.Sorted = true;
    this.comboBoxTemplates.TabIndex = 2;
    this.comboBoxTemplates.DropDown += new EventHandler(this.comboBoxTemplates_DropDown);
    this.comboBoxDefs.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    this.comboBoxDefs.AutoCompleteSource = AutoCompleteSource.ListItems;
    this.comboBoxDefs.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBoxDefs.FormattingEnabled = true;
    this.comboBoxDefs.Location = new Point(99, 36);
    this.comboBoxDefs.MaxDropDownItems = 16 /*0x10*/;
    this.comboBoxDefs.Name = "comboBoxDefs";
    this.comboBoxDefs.Size = new Size(296, 21);
    this.comboBoxDefs.Sorted = true;
    this.comboBoxDefs.TabIndex = 1;
    this.comboBoxDefs.SelectedIndexChanged += new EventHandler(this.comboBoxDefs_SelectedIndexChanged);
    this.AcceptButton = (IButtonControl) this.buttonCreate;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.ClientSize = new Size(407, 128 /*0x80*/);
    this.Controls.Add((System.Windows.Forms.Control) this.comboBoxDefs);
    this.Controls.Add((System.Windows.Forms.Control) this.comboBoxTemplates);
    this.Controls.Add((System.Windows.Forms.Control) this.label1);
    this.Controls.Add((System.Windows.Forms.Control) this.buttonCancel);
    this.Controls.Add((System.Windows.Forms.Control) this.buttonCreate);
    this.Controls.Add((System.Windows.Forms.Control) this.textBoxName);
    this.Controls.Add((System.Windows.Forms.Control) this.lblDefinitionType);
    this.Controls.Add((System.Windows.Forms.Control) this.lblSymbolName);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FormNewGameBINEntry);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "BIN: New Entry";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
