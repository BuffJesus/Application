// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.MemberDisplay
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace FableMod.ContentManagement;

public class MemberDisplay : UserControl
{
  private Label lblLinksTo;
  private TextBox txtMemberValue;
  private Member m_Member;
  private ContentManager m_ContentMgr;
  private Button btnSelectEntry;
  private ToolTip ttComments;
  private Label labelName;
  private IContainer components;

  public MemberDisplay(Member member, ContentManager contentmgr)
  {
    // ISSUE: fault handler
    try
    {
      this.InitializeComponent();
      this.m_ContentMgr = contentmgr;
      this.m_Member = member;
      this.labelName.Text = $"{this.m_Member.Name} [{member.Type.ToString()}]";
      this.DoLinksTo();
      this.SuspendLayout();
      this.txtMemberValue.Left = this.labelName.Right + 3;
      this.txtMemberValue.Text = this.m_Member.Value.ToString();
      if (this.m_Member.Type == MemberType.STRING)
        this.txtMemberValue.Width = 250;
      this.btnSelectEntry.Left = this.txtMemberValue.Right + 3;
      if (!string.IsNullOrEmpty(this.m_Member.Comments))
        this.ttComments.SetToolTip((System.Windows.Forms.Control) this.labelName, this.m_Member.Comments);
      this.ResumeLayout();
    }
    __fault
    {
      base.Dispose(true);
    }
  }

  public Member Member => this.m_Member;

  public void ApplyChanges() => this.m_Member.Value = (object) this.txtMemberValue.Text;

  private void \u007EMemberDisplay() => this.components?.Dispose();

  private void lblLinksTo_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
  {
    this.m_ContentMgr.ShowEntry(this.lblLinksTo.Tag, false);
  }

  private void DoLinkLabel(LinkDestination dst, ContentObject o)
  {
    if (this.Controls.Contains((System.Windows.Forms.Control) this.lblLinksTo))
      this.Controls.Remove((System.Windows.Forms.Control) this.lblLinksTo);
    if (!this.Controls.Contains((System.Windows.Forms.Control) this.btnSelectEntry))
      this.Controls.Add((System.Windows.Forms.Control) this.btnSelectEntry);
    LinkLabel linkLabel = new LinkLabel();
    this.lblLinksTo = (Label) linkLabel;
    linkLabel.Tag = o.Object;
    linkLabel.AutoSize = true;
    Point point = new Point(3, 29);
    linkLabel.Location = point;
    linkLabel.Name = "lblLinksTo";
    linkLabel.TabIndex = 2;
    linkLabel.Text = "Links to (" + dst.ToString() + "): " + o.Name;
    linkLabel.LinkClicked += new LinkLabelLinkClickedEventHandler(this.lblLinksTo_LinkClicked);
    this.Controls.Add((System.Windows.Forms.Control) this.lblLinksTo);
  }

  private void DoNoLinkLabel(LinkDestination dst)
  {
    if (this.Controls.Contains((System.Windows.Forms.Control) this.lblLinksTo))
      this.Controls.Remove((System.Windows.Forms.Control) this.lblLinksTo);
    if (!this.Controls.Contains((System.Windows.Forms.Control) this.btnSelectEntry))
      this.Controls.Add((System.Windows.Forms.Control) this.btnSelectEntry);
    Label label = new Label();
    this.lblLinksTo = label;
    label.AutoSize = true;
    this.lblLinksTo.Location = new Point(3, 29);
    this.lblLinksTo.Name = "lblLinksTo";
    this.lblLinksTo.TabIndex = 2;
    this.lblLinksTo.Text = "Links to (" + this.m_Member.Link.To.ToString() + ") : (Error Finding Entry)";
    this.Controls.Add((System.Windows.Forms.Control) this.lblLinksTo);
  }

  private void DoLinksTo()
  {
    this.SuspendLayout();
    Member member = this.m_Member;
    if (member.Link != null)
    {
      ContentManager contentMgr = this.m_ContentMgr;
      if (contentMgr != null)
      {
        ContentObject entry = contentMgr.FindEntry(member.Link.To, member.Value);
        if (entry != null)
        {
          MemberDisplay memberDisplay = this;
          memberDisplay.DoLinkLabel(memberDisplay.m_Member.Link.To, entry);
        }
        else
        {
          MemberDisplay memberDisplay = this;
          memberDisplay.DoNoLinkLabel(memberDisplay.m_Member.Link.To);
        }
        this.ResumeLayout();
        return;
      }
      this.DoNoLinkLabel(member.Link.To);
    }
    else
    {
      if (this.Controls.Contains((System.Windows.Forms.Control) this.lblLinksTo))
        this.Controls.Remove((System.Windows.Forms.Control) this.lblLinksTo);
      if (this.Controls.Contains((System.Windows.Forms.Control) this.btnSelectEntry))
        this.Controls.Remove((System.Windows.Forms.Control) this.btnSelectEntry);
    }
    this.ResumeLayout();
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.txtMemberValue = new TextBox();
    this.lblLinksTo = new Label();
    this.btnSelectEntry = new Button();
    MemberDisplay memberDisplay = this;
    memberDisplay.ttComments = new ToolTip(memberDisplay.components);
    this.labelName = new Label();
    this.SuspendLayout();
    this.txtMemberValue.Location = new Point(131, 3);
    this.txtMemberValue.Name = "txtMemberValue";
    this.txtMemberValue.Size = new Size(80 /*0x50*/, 20);
    this.txtMemberValue.TabIndex = 1;
    this.txtMemberValue.Validating += new CancelEventHandler(this.txtMemberValue_Validating);
    this.lblLinksTo.AutoSize = true;
    this.lblLinksTo.Location = new Point(3, 29);
    this.lblLinksTo.Name = "lblLinksTo";
    this.lblLinksTo.Size = new Size(137, 13);
    this.lblLinksTo.TabIndex = 2;
    this.lblLinksTo.Text = "Links To: (Entry Not Found)";
    this.btnSelectEntry.Location = new Point(217, 2);
    this.btnSelectEntry.Name = "btnSelectEntry";
    this.btnSelectEntry.Size = new Size(25, 21);
    this.btnSelectEntry.TabIndex = 3;
    this.btnSelectEntry.Text = "...";
    this.btnSelectEntry.UseVisualStyleBackColor = true;
    this.btnSelectEntry.Click += new EventHandler(this.btnSelectEntry_Click);
    this.labelName.AutoSize = true;
    this.labelName.Location = new Point(3, 6);
    this.labelName.MinimumSize = new Size(60, 0);
    this.labelName.Name = "labelName";
    this.labelName.Size = new Size(97, 13);
    this.labelName.TabIndex = 4;
    this.labelName.Text = "LongMemberName";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoSize = true;
    this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.Controls.Add((System.Windows.Forms.Control) this.labelName);
    this.Controls.Add((System.Windows.Forms.Control) this.btnSelectEntry);
    this.Controls.Add((System.Windows.Forms.Control) this.lblLinksTo);
    this.Controls.Add((System.Windows.Forms.Control) this.txtMemberValue);
    this.Name = nameof (MemberDisplay);
    this.Size = new Size(245, 42);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void btnSelectEntry_Click(object sender, EventArgs e)
  {
    Member member = this.m_Member;
    object obj = this.m_ContentMgr.SelectEntry(member.Link, member.Value);
    if (obj == null)
      return;
    this.m_Member.Value = (object) obj.ToString();
    this.DoLinksTo();
    this.txtMemberValue.Text = this.m_Member.Value.ToString();
  }

  private void txtMemberValue_Validating(object sender, CancelEventArgs e)
  {
    try
    {
      this.m_Member.Value = (object) this.txtMemberValue.Text;
      this.DoLinksTo();
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, ex.Message, "Error Validating New Value");
    }
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EMemberDisplay();
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
