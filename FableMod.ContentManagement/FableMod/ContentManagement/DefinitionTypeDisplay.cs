// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.DefinitionTypeDisplay
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using System;
using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace FableMod.ContentManagement;

public class DefinitionTypeDisplay : UserControl
{
  private ContentManager m_ContentMgr;
  private DefinitionType m_Definition;
  private System.Windows.Forms.Control m_Display;
  private TreeView tvMembers;
  private Splitter splitter1;
  private Panel pnlMemberDisplay;
  private System.ComponentModel.Container components;

  public DefinitionTypeDisplay(DefinitionType definition, ContentManager contentmgr)
  {
    // ISSUE: fault handler
    try
    {
      this.InitializeComponent();
      this.m_ContentMgr = contentmgr;
      this.m_Definition = definition;
      TreeNode node1 = new TreeNode(definition.Name);
      node1.Tag = (object) definition;
      DefinitionType definition1 = this.m_Definition;
      if (definition1.HasCDefListing)
        node1.Nodes.Add(new TreeNode(definition1.CDefs.Name)
        {
          Tag = (object) this.m_Definition.CDefs
        });
      bool flag = !DefinitionDB.DeveloperModeEnabled();
      int index1 = 0;
      if (0 < definition.Controls.Count)
      {
        do
        {
          // ISSUE: explicit non-virtual call
          Control control = __nonvirtual (definition.Controls[index1]);
          TreeNode node2 = new TreeNode(control.ToString());
          node2.Tag = (object) control;
          int index2 = 0;
          if (0 < control.Members.Count)
          {
            do
            {
              BaseMember member = control.Members[index2];
              if (!flag || !(member.Name == "unknown"))
                node2.Nodes.Add(new TreeNode(member.Name)
                {
                  Tag = (object) member
                });
              ++index2;
            }
            while (index2 < control.Members.Count);
          }
          if (node2.Nodes.Count > 0)
            node1.Nodes.Add(node2);
          ++index1;
        }
        while (index1 < definition.Controls.Count);
      }
      this.tvMembers.Nodes.Add(node1);
    }
    __fault
    {
      base.Dispose(true);
    }
  }

  public void ApplyChanges()
  {
    int index = 0;
    if (0 >= this.Controls.Count)
      return;
    do
    {
      if (this.Controls[index].GetType() == typeof (ControlDisplay))
        ((ControlDisplay) this.Controls[index]).ApplyChanges();
      else if (this.Controls[index].GetType() == typeof (ArrayMemberDisplay))
        ((ArrayMemberDisplay) this.Controls[index]).ApplyChanges();
      ++index;
    }
    while (index < this.Controls.Count);
  }

  public void DoLayout()
  {
    int y = 0;
    this.SuspendLayout();
    int index = 0;
    if (0 < this.pnlMemberDisplay.Controls.Count)
    {
      do
      {
        if (this.pnlMemberDisplay.Controls[index].GetType() == typeof (ControlDisplay))
        {
          ControlDisplay control = (ControlDisplay) this.pnlMemberDisplay.Controls[index];
          Point point = new Point(0, y);
          control.Location = point;
          Size size = control.Size;
          y += size.Height;
        }
        else if (this.pnlMemberDisplay.Controls[index].GetType() == typeof (ArrayMemberDisplay))
        {
          ArrayMemberDisplay control = (ArrayMemberDisplay) this.pnlMemberDisplay.Controls[index];
          Point point = new Point(0, y);
          control.Location = point;
          Size size = control.Size;
          y += size.Height;
        }
        ++index;
      }
      while (index < this.pnlMemberDisplay.Controls.Count);
    }
    this.ResumeLayout();
  }

  private void \u007EDefinitionTypeDisplay() => this.components?.Dispose();

  private void InitializeComponent()
  {
    this.tvMembers = new TreeView();
    this.splitter1 = new Splitter();
    this.pnlMemberDisplay = new Panel();
    this.SuspendLayout();
    this.tvMembers.Dock = DockStyle.Left;
    this.tvMembers.HideSelection = false;
    this.tvMembers.Location = new Point(0, 0);
    this.tvMembers.Name = "tvMembers";
    this.tvMembers.Size = new Size(200, 449);
    this.tvMembers.TabIndex = 0;
    this.tvMembers.AfterSelect += new TreeViewEventHandler(this.tvMembers_AfterSelect);
    this.splitter1.Location = new Point(200, 0);
    this.splitter1.Name = "splitter1";
    this.splitter1.Size = new Size(5, 449);
    this.splitter1.TabIndex = 1;
    this.splitter1.TabStop = false;
    this.pnlMemberDisplay.AutoScroll = true;
    this.pnlMemberDisplay.Dock = DockStyle.Fill;
    this.pnlMemberDisplay.Location = new Point(205, 0);
    this.pnlMemberDisplay.Name = "pnlMemberDisplay";
    this.pnlMemberDisplay.Size = new Size(348, 449);
    this.pnlMemberDisplay.TabIndex = 2;
    this.pnlMemberDisplay.TabStop = true;
    this.pnlMemberDisplay.MouseClick += new MouseEventHandler(this.pnlMemberDisplay_MouseClick);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoSize = true;
    this.Controls.Add((System.Windows.Forms.Control) this.pnlMemberDisplay);
    this.Controls.Add((System.Windows.Forms.Control) this.splitter1);
    this.Controls.Add((System.Windows.Forms.Control) this.tvMembers);
    this.Name = nameof (DefinitionTypeDisplay);
    this.Size = new Size(553, 449);
    this.ResumeLayout(false);
  }

  private void MemberSizeChanged(object sender, EventArgs e) => this.DoLayout();

  private void tvMembers_AfterSelect(object sender, TreeViewEventArgs e)
  {
    this.pnlMemberDisplay.Controls.Clear();
    if (e.Node == null)
      return;
    object tag = e.Node.Tag;
    if (tag == null)
      return;
    Type type = tag.GetType();
    if (type == typeof (Member))
    {
      MemberDisplay memberDisplay = new MemberDisplay((Member) tag, this.m_ContentMgr);
      this.m_Display = (System.Windows.Forms.Control) memberDisplay;
      this.pnlMemberDisplay.Controls.Add((System.Windows.Forms.Control) memberDisplay);
    }
    else if (type == typeof (ArrayMember))
    {
      ArrayMemberDisplay arrayMemberDisplay = new ArrayMemberDisplay((ArrayMember) tag, this.m_ContentMgr);
      this.m_Display = (System.Windows.Forms.Control) arrayMemberDisplay;
      this.pnlMemberDisplay.Controls.Add((System.Windows.Forms.Control) arrayMemberDisplay);
    }
    else if (type == typeof (Control))
    {
      ControlDisplay controlDisplay = new ControlDisplay((Control) tag, this.m_ContentMgr);
      this.m_Display = (System.Windows.Forms.Control) controlDisplay;
      this.pnlMemberDisplay.Controls.Add((System.Windows.Forms.Control) controlDisplay);
    }
    else
    {
      if (!(type == typeof (DefinitionType)))
        return;
      this.pnlMemberDisplay.SuspendLayout();
      DefinitionType definition = this.m_Definition;
      if (definition.HasCDefListing)
      {
        ArrayMemberDisplay arrayMemberDisplay = new ArrayMemberDisplay(definition.CDefs, this.m_ContentMgr);
        arrayMemberDisplay.SizeChanged += new EventHandler(this.MemberSizeChanged);
        this.pnlMemberDisplay.Controls.Add((System.Windows.Forms.Control) arrayMemberDisplay);
      }
      int index = 0;
      if (0 < this.m_Definition.Controls.Count)
      {
        do
        {
          // ISSUE: explicit non-virtual call
          ControlDisplay controlDisplay = new ControlDisplay(__nonvirtual (this.m_Definition.Controls[index]), this.m_ContentMgr);
          controlDisplay.SizeChanged += new EventHandler(this.MemberSizeChanged);
          this.pnlMemberDisplay.Controls.Add((System.Windows.Forms.Control) controlDisplay);
          ++index;
        }
        while (index < this.m_Definition.Controls.Count);
      }
      this.pnlMemberDisplay.ResumeLayout();
      this.DoLayout();
      this.pnlMemberDisplay.Focus();
    }
  }

  private void pnlMemberDisplay_MouseClick(object sender, MouseEventArgs e)
  {
    this.pnlMemberDisplay.Focus();
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EDefinitionTypeDisplay();
      }
      finally
      {
        base.Dispose(true);
      }
    }
    else
      base.Dispose(false);
  }
}
