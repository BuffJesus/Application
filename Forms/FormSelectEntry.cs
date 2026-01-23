// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormSelectEntry
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using System;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormSelectEntry : Form
{
  private object mySelected;
  private IContainer components;
  private Button buttonOK;
  private Button buttonCancel;
  private ProgressBar progressBar;
  private Button buttonFind;
  private ColumnHeader columnHeader2;
  private TextBox textBoxName;
  private Label label1;
  private ColumnHeader columnHeader1;
  protected ListView listViewEntries;
  private ToolTip toolTip;

  public FormSelectEntry()
  {
    this.InitializeComponent();
    ThemeManager.ApplyTheme(this);
  }

  public FormSelectEntry(string current)
  {
    this.InitializeComponent();
    ThemeManager.ApplyTheme(this);
    if (current != null)
      this.textBoxName.Text = current;
    else
      this.textBoxName.Text = "";
  }

  protected void AddEntry(string name, string type, object entry)
  {
    ListViewItem listViewItem = new ListViewItem(name);
    if (!string.IsNullOrEmpty(type))
      listViewItem.SubItems.Add(type);
    listViewItem.Tag = entry;
    this.listViewEntries.Items.Add(listViewItem);
    if (!(name == this.textBoxName.Text))
      return;
    listViewItem.Selected = true;
  }

  protected virtual void AddEntries(Regex regex, string name, ProgressBar progressBar)
  {
  }

  protected void FindEntries()
  {
    this.buttonOK.Enabled = false;
    this.listViewEntries.Items.Clear();
    if (this.textBoxName.Text.Length <= 0)
      return;
    string text = this.textBoxName.Text;
    Regex regex = (Regex) null;
    if (text.IndexOf("[\\DEV\\") < 0)
    {
      try
      {
        regex = new Regex(text, RegexOptions.IgnoreCase);
      }
      catch (Exception ex)
      {
        int num = (int) FormMain.Instance.ErrorMessage(ex.ToString());
        return;
      }
    }
    try
    {
      this.AddEntries(regex, text, this.progressBar);
    }
    catch (Exception ex)
    {
      this.AddEntries((Regex) null, text, this.progressBar);
    }
    if (this.listViewEntries.Items.Count <= 0)
      return;
    this.listViewEntries.Items[0].Selected = true;
    this.listViewEntries.Focus();
  }

  private void buttonFind_Click(object sender, EventArgs e) => this.FindEntries();

  private void listViewEntries_SelectedIndexChanged(object sender, EventArgs e)
  {
    if (this.listViewEntries.SelectedItems.Count == 0)
      return;
    this.mySelected = this.listViewEntries.SelectedItems[0].Tag;
    this.buttonOK.Enabled = true;
  }

  protected virtual void ShowSelectedEntry()
  {
  }

  public object Selected => this.mySelected;

  private void listViewEntries_MouseDoubleClick(object sender, MouseEventArgs e)
  {
    if (this.listViewEntries.SelectedItems.Count != 0)
    {
      this.mySelected = this.listViewEntries.SelectedItems[0].Tag;
      this.buttonOK.Enabled = true;
    }
    if (this.mySelected == null)
      return;
    this.ShowSelectedEntry();
  }

  private void textBoxName_TextChanged(object sender, EventArgs e)
  {
    this.buttonFind.Enabled = this.textBoxName.Text.Length > 0;
  }

  private void textBoxName_KeyDown(object sender, KeyEventArgs e)
  {
    if (e.KeyCode != Keys.Return || !this.buttonFind.Enabled)
      return;
    e.Handled = true;
    this.FindEntries();
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.buttonOK = new Button();
    this.buttonCancel = new Button();
    this.progressBar = new ProgressBar();
    this.buttonFind = new Button();
    this.columnHeader2 = new ColumnHeader();
    this.textBoxName = new TextBox();
    this.label1 = new Label();
    this.columnHeader1 = new ColumnHeader();
    this.listViewEntries = new ListView();
    this.toolTip = new ToolTip(this.components);
    this.SuspendLayout();
    this.buttonOK.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonOK.DialogResult = DialogResult.OK;
    this.buttonOK.Enabled = false;
    this.buttonOK.Location = new Point(320, 363);
    this.buttonOK.Name = "buttonOK";
    this.buttonOK.Size = new Size(75, 23);
    this.buttonOK.TabIndex = 13;
    this.buttonOK.Text = "OK";
    this.buttonOK.UseVisualStyleBackColor = true;
    this.buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonCancel.DialogResult = DialogResult.Cancel;
    this.buttonCancel.Location = new Point(401, 363);
    this.buttonCancel.Name = "buttonCancel";
    this.buttonCancel.Size = new Size(75, 23);
    this.buttonCancel.TabIndex = 12;
    this.buttonCancel.Text = "Cancel";
    this.buttonCancel.UseVisualStyleBackColor = true;
    this.progressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.progressBar.Location = new Point(12, 340);
    this.progressBar.Name = "progressBar";
    this.progressBar.Size = new Size(464, 17);
    this.progressBar.TabIndex = 11;
    this.buttonFind.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonFind.Enabled = false;
    this.buttonFind.Location = new Point(422, 8);
    this.buttonFind.Name = "buttonFind";
    this.buttonFind.Size = new Size(54, 20);
    this.buttonFind.TabIndex = 10;
    this.buttonFind.Text = "Find";
    this.buttonFind.UseVisualStyleBackColor = true;
    this.buttonFind.Click += new EventHandler(this.buttonFind_Click);
    this.columnHeader2.Text = "Type";
    this.columnHeader2.Width = 200;
    this.textBoxName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.textBoxName.Location = new Point(90, 8);
    this.textBoxName.Name = "textBoxName";
    this.textBoxName.Size = new Size(326, 20);
    this.textBoxName.TabIndex = 8;
    this.textBoxName.TextChanged += new EventHandler(this.textBoxName_TextChanged);
    this.textBoxName.KeyDown += new KeyEventHandler(this.textBoxName_KeyDown);
    this.label1.AutoSize = true;
    this.label1.Location = new Point(12, 11);
    this.label1.Name = "label1";
    this.label1.Size = new Size(72, 13);
    this.label1.TabIndex = 7;
    this.label1.Text = "Regex Name:";
    this.columnHeader1.Text = "Name";
    this.columnHeader1.Width = 256 /*0x0100*/;
    this.listViewEntries.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.listViewEntries.Columns.AddRange(new ColumnHeader[2]
    {
      this.columnHeader1,
      this.columnHeader2
    });
    this.listViewEntries.HideSelection = false;
    this.listViewEntries.LabelWrap = false;
    this.listViewEntries.Location = new Point(12, 34);
    this.listViewEntries.MultiSelect = false;
    this.listViewEntries.Name = "listViewEntries";
    this.listViewEntries.ShowGroups = false;
    this.listViewEntries.Size = new Size(464, 300);
    this.listViewEntries.Sorting = SortOrder.Ascending;
    this.listViewEntries.TabIndex = 9;
    this.toolTip.SetToolTip((Control) this.listViewEntries, "Double click items to view");
    this.listViewEntries.UseCompatibleStateImageBehavior = false;
    this.listViewEntries.View = View.Details;
    this.listViewEntries.MouseDoubleClick += new MouseEventHandler(this.listViewEntries_MouseDoubleClick);
    this.listViewEntries.SelectedIndexChanged += new EventHandler(this.listViewEntries_SelectedIndexChanged);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(488, 398);
    this.Controls.Add((Control) this.buttonOK);
    this.Controls.Add((Control) this.buttonCancel);
    this.Controls.Add((Control) this.progressBar);
    this.Controls.Add((Control) this.buttonFind);
    this.Controls.Add((Control) this.textBoxName);
    this.Controls.Add((Control) this.label1);
    this.Controls.Add((Control) this.listViewEntries);
    this.Name = nameof (FormSelectEntry);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Select Object";
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
