// Decompiled with JetBrains decompiler
// Type: FableMod.Content.Forms.ControlTextGroup
// Assembly: FableMod.Content.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A575F982-23D4-4DAE-A571-3D769462C59B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Content.Forms.dll

using FableMod.BIG;
using FableMod.ContentManagement;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Content.Forms;

public class ControlTextGroup : ControlBIGEntry
{
  private Label lblEntries;
  private Button btnAdd;
  private Button btnRemove;
  private Label lblID;
  private TextBox txtID;
  private IContainer components;
  private ListView lvEntries;
  private ColumnHeader chID;
  private ColumnHeader chContent;
  private Button btnMoveUp;
  private Button btnMoveDown;
  private Button btnRefresh;
  private BIGTextGroup m_TextGroup;

  public ControlTextGroup() => this.InitializeComponent();

  public override void ApplyChanges()
  {
    if (this.m_TextGroup == null)
      return;
    this.SaveListViewToTextGroup();
    this.m_TextGroup.ApplyToEntry(this.m_Entry);
  }

  private void RefreshDisplay()
  {
    this.lvEntries.Items.Clear();
    uint[] entries = this.m_TextGroup.Entries;
    for (int index = 0; index < entries.Length; ++index)
    {
      AssetEntry entryById = this.m_Entry.Bank.FindEntryByID(entries[index]);
      string[] items = new string[2]
      {
        entries[index].ToString(),
        null
      };
      if (entryById != null)
        items[1] = new BIGText(entryById).Content;
      this.lvEntries.Items.Add(new ListViewItem(items));
    }
  }

  private void SaveListViewToTextGroup()
  {
    uint[] numArray = new uint[this.lvEntries.Items.Count];
    for (int index = 0; index < this.lvEntries.Items.Count; ++index)
    {
      try
      {
        numArray[index] = uint.Parse(this.lvEntries.Items[index].SubItems[0].Text);
      }
      catch (Exception ex)
      {
      }
    }
    this.m_TextGroup.Entries = numArray;
  }

  public override AssetEntry BIGEntry
  {
    get => base.BIGEntry;
    set
    {
      base.BIGEntry = value;
      this.m_TextGroup = new BIGTextGroup(this.m_Entry);
      this.RefreshDisplay();
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
    this.lblEntries = new Label();
    this.lvEntries = new ListView();
    this.chID = new ColumnHeader();
    this.chContent = new ColumnHeader();
    this.btnAdd = new Button();
    this.btnRemove = new Button();
    this.lblID = new Label();
    this.txtID = new TextBox();
    this.btnMoveUp = new Button();
    this.btnMoveDown = new Button();
    this.btnRefresh = new Button();
    this.SuspendLayout();
    this.lblEntries.AutoSize = true;
    this.lblEntries.Location = new Point(1, 40);
    this.lblEntries.Name = "lblEntries";
    this.lblEntries.Size = new Size(42, 13);
    this.lblEntries.TabIndex = 2;
    this.lblEntries.Text = "Entries:";
    this.lvEntries.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.lvEntries.Columns.AddRange(new ColumnHeader[2]
    {
      this.chID,
      this.chContent
    });
    this.lvEntries.GridLines = true;
    this.lvEntries.HideSelection = false;
    this.lvEntries.Location = new Point(58, 40);
    this.lvEntries.Name = "lvEntries";
    this.lvEntries.Size = new Size(495, 208 /*0xD0*/);
    this.lvEntries.TabIndex = 3;
    this.lvEntries.UseCompatibleStateImageBehavior = false;
    this.lvEntries.View = View.Details;
    this.lvEntries.DoubleClick += new EventHandler(this.lvEntries_DoubleClick);
    this.chID.Text = "ID";
    this.chContent.Text = "Content";
    this.chContent.Width = 384;
    this.btnAdd.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.btnAdd.Location = new Point(140, 253);
    this.btnAdd.Name = "btnAdd";
    this.btnAdd.Size = new Size(56, 21);
    this.btnAdd.TabIndex = 4;
    this.btnAdd.Text = "Add";
    this.btnAdd.Click += new EventHandler(this.btnAdd_Click);
    this.btnRemove.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnRemove.Location = new Point(432, 254);
    this.btnRemove.Name = "btnRemove";
    this.btnRemove.Size = new Size(121, 20);
    this.btnRemove.TabIndex = 5;
    this.btnRemove.Text = "Remove Selected";
    this.btnRemove.Click += new EventHandler(this.btnRemove_Click);
    this.lblID.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.lblID.AutoSize = true;
    this.lblID.Location = new Point(59, 259);
    this.lblID.Name = "lblID";
    this.lblID.Size = new Size(21, 13);
    this.lblID.TabIndex = 6;
    this.lblID.Text = "ID:";
    this.txtID.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
    this.txtID.Location = new Point(86, 254);
    this.txtID.MaxLength = 5;
    this.txtID.Name = "txtID";
    this.txtID.Size = new Size(48 /*0x30*/, 20);
    this.txtID.TabIndex = 7;
    this.txtID.Text = "0";
    this.btnMoveUp.Location = new Point(4, 116);
    this.btnMoveUp.Name = "btnMoveUp";
    this.btnMoveUp.Size = new Size(48 /*0x30*/, 23);
    this.btnMoveUp.TabIndex = 8;
    this.btnMoveUp.Text = "Up";
    this.btnMoveUp.Click += new EventHandler(this.btnMoveUp_Click);
    this.btnMoveDown.Location = new Point(4, 145);
    this.btnMoveDown.Name = "btnMoveDown";
    this.btnMoveDown.Size = new Size(48 /*0x30*/, 23);
    this.btnMoveDown.TabIndex = 9;
    this.btnMoveDown.Text = "Down";
    this.btnMoveDown.Click += new EventHandler(this.btnMoveDown_Click);
    this.btnRefresh.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.btnRefresh.Location = new Point(305, 254);
    this.btnRefresh.Name = "btnRefresh";
    this.btnRefresh.Size = new Size(121, 20);
    this.btnRefresh.TabIndex = 10;
    this.btnRefresh.Text = "Refresh Display";
    this.btnRefresh.Click += new EventHandler(this.btnRefresh_Click);
    this.Controls.Add((System.Windows.Forms.Control) this.btnRefresh);
    this.Controls.Add((System.Windows.Forms.Control) this.btnMoveDown);
    this.Controls.Add((System.Windows.Forms.Control) this.btnMoveUp);
    this.Controls.Add((System.Windows.Forms.Control) this.txtID);
    this.Controls.Add((System.Windows.Forms.Control) this.lblID);
    this.Controls.Add((System.Windows.Forms.Control) this.btnRemove);
    this.Controls.Add((System.Windows.Forms.Control) this.btnAdd);
    this.Controls.Add((System.Windows.Forms.Control) this.lvEntries);
    this.Controls.Add((System.Windows.Forms.Control) this.lblEntries);
    this.EntryType = "Text Group";
    this.Name = "ControlConversation";
    this.Size = new Size(565, 284);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblEntries, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lvEntries, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.btnAdd, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.btnRemove, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblID, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.txtID, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.btnMoveUp, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.btnMoveDown, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.btnRefresh, 0);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void btnAdd_Click(object sender, EventArgs e)
  {
    try
    {
      uint num = uint.Parse(this.txtID.Text);
      this.SaveListViewToTextGroup();
      uint[] entries = this.m_TextGroup.Entries;
      uint[] numArray = new uint[entries.Length + 1];
      entries.CopyTo((Array) numArray, 0);
      numArray[entries.Length] = num;
      this.m_TextGroup.Entries = numArray;
      this.RefreshDisplay();
    }
    catch (Exception ex)
    {
    }
  }

  private void btnRemove_Click(object sender, EventArgs e)
  {
    for (int index = 0; index < this.lvEntries.SelectedItems.Count; ++index)
      this.lvEntries.Items.Remove(this.lvEntries.SelectedItems[index]);
  }

  private void btnMoveUp_Click(object sender, EventArgs e)
  {
    if (this.lvEntries.SelectedItems.Count == 0)
      return;
    ListViewItem selectedItem = this.lvEntries.SelectedItems[0];
    int num = this.lvEntries.Items.IndexOf(selectedItem);
    if (num == 0)
      return;
    this.lvEntries.Items.Remove(selectedItem);
    this.lvEntries.Items.Insert(num - 1, selectedItem);
    selectedItem.Selected = true;
  }

  private void btnMoveDown_Click(object sender, EventArgs e)
  {
    if (this.lvEntries.SelectedItems.Count == 0)
      return;
    ListViewItem selectedItem = this.lvEntries.SelectedItems[0];
    int num = this.lvEntries.Items.IndexOf(selectedItem);
    if (num == this.lvEntries.Items.Count - 1)
      return;
    this.lvEntries.Items.Remove(selectedItem);
    this.lvEntries.Items.Insert(num + 1, selectedItem);
    selectedItem.Selected = true;
  }

  private void lvEntries_DoubleClick(object sender, EventArgs e)
  {
    if (this.lvEntries.SelectedItems.Count == 0)
      return;
    try
    {
      ContentManager.Instance.ShowEntry(LinkDestination.TextID, (object) uint.Parse(this.lvEntries.SelectedItems[0].SubItems[0].Text), false);
    }
    catch (Exception ex)
    {
    }
  }

  private void btnRefresh_Click(object sender, EventArgs e)
  {
    this.SaveListViewToTextGroup();
    this.RefreshDisplay();
  }
}
