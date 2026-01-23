// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormNewBIGEntry
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIG;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormNewBIGEntry : Form
{
  private BIGBank myBank;
  private IContainer components;
  private Button buttonCancel;
  private Button buttonCreate;
  private TextBox txtName;
  private Label lblSymbolName;
  private ComboBox comboBoxTemplates;
  private Label label1;

  public FormNewBIGEntry(BIGBank bank)
  {
    this.InitializeComponent();
    ThemeManager.ApplyTheme(this);
    this.myBank = bank;
    for (int index = 0; index < this.myBank.EntryCount; ++index)
      this.comboBoxTemplates.Items.Add((object) this.myBank.get_Entries(index).DevSymbolName);
    this.comboBoxTemplates.SelectedIndex = 0;
  }

  public string SymbolName => this.txtName.Text;

  public AssetEntry Template => this.myBank.FindEntryBySymbolName(this.comboBoxTemplates.Text);

  private void buttonCreate_Click(object sender, EventArgs e)
  {
    if (this.SymbolName == "" || this.Template == null)
    {
      int num1 = (int) FormMain.Instance.ErrorMessage("Missing information.");
    }
    else if (this.myBank.FindEntryBySymbolName(this.SymbolName) != null)
    {
      int num2 = (int) FormMain.Instance.ErrorMessage("Entry with the same name already exists.");
    }
    else
      this.DialogResult = DialogResult.OK;
  }

  private void buttonCancel_Click(object sender, EventArgs e)
  {
    this.DialogResult = DialogResult.Cancel;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.buttonCancel = new Button();
    this.buttonCreate = new Button();
    this.txtName = new TextBox();
    this.lblSymbolName = new Label();
    this.comboBoxTemplates = new ComboBox();
    this.label1 = new Label();
    this.SuspendLayout();
    this.buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Location = new Point(317, 71);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(75, 23);
    this.buttonCancel.TabIndex = 3;
    this.buttonCancel.Text = "Cancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.buttonCreate.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonCreate.Location = new Point(236, 71);
    this.buttonCreate.Name = "buttonCreate";
    this.buttonCreate.Size = new Size(75, 23);
    this.buttonCreate.TabIndex = 2;
    this.buttonCreate.Text = "Create";
    this.buttonCreate.UseVisualStyleBackColor = true;
    this.buttonCreate.Click += new EventHandler(this.buttonCreate_Click);
    this.txtName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtName.Location = new Point(94, 12);
    this.txtName.Name = "txtName";
    this.txtName.Size = new Size(298, 20);
    this.txtName.TabIndex = 0;
    this.lblSymbolName.AutoSize = true;
    this.lblSymbolName.Location = new Point(7, 15);
    this.lblSymbolName.Name = "lblSymbolName";
    this.lblSymbolName.Size = new Size(75, 13);
    this.lblSymbolName.TabIndex = 10;
    this.lblSymbolName.Text = "Symbol Name:";
    this.comboBoxTemplates.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.comboBoxTemplates.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    this.comboBoxTemplates.AutoCompleteSource = AutoCompleteSource.ListItems;
    this.comboBoxTemplates.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBoxTemplates.FormattingEnabled = true;
    this.comboBoxTemplates.Location = new Point(94, 38);
    this.comboBoxTemplates.Name = "comboBoxTemplates";
    this.comboBoxTemplates.Size = new Size(298, 21);
    this.comboBoxTemplates.TabIndex = 1;
    this.label1.AutoSize = true;
    this.label1.Location = new Point(7, 41);
    this.label1.Name = "label1";
    this.label1.Size = new Size(54, 13);
    this.label1.TabIndex = 18;
    this.label1.Text = "Template:";
    this.AcceptButton = (IButtonControl) this.buttonCreate;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.CancelButton = (IButtonControl) this.buttonCancel;
    this.ClientSize = new Size(404, 106);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.comboBoxTemplates);
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.buttonCreate);
    this.Controls.Add((Control) this.txtName);
    this.Controls.Add((Control) this.lblSymbolName);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FormNewBIGEntry);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "BIG: New Entry";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
