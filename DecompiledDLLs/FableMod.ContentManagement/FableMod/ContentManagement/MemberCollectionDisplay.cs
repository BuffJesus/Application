// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.MemberCollectionDisplay
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using System;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace FableMod.ContentManagement;

public class MemberCollectionDisplay : UserControl
{
  private MemberCollection m_Members;
  private ContentManager m_ContentMgr;
  private Button btnDelete;
  private Button buttonUp;
  private Button buttonDown;
  private System.ComponentModel.Container components;

  public MemberCollectionDisplay(MemberCollection mc, ContentManager contentmgr)
  {
    // ISSUE: fault handler
    try
    {
      this.InitializeComponent();
      this.m_ContentMgr = contentmgr;
      this.m_Members = mc;
      this.SuspendLayout();
      int index = 0;
      if (0 < mc.Count)
      {
        do
        {
          if (mc[index].GetType() == typeof (Member))
            this.Controls.Add((System.Windows.Forms.Control) new MemberDisplay((Member) mc[index], this.m_ContentMgr));
          else if (mc[index].GetType() == typeof (ArrayMember))
          {
            ArrayMemberDisplay arrayMemberDisplay = new ArrayMemberDisplay((ArrayMember) mc[index], this.m_ContentMgr);
            arrayMemberDisplay.SizeChanged += new EventHandler(this.MemberSizeChanged);
            this.Controls.Add((System.Windows.Forms.Control) arrayMemberDisplay);
          }
          ++index;
        }
        while (index < mc.Count);
      }
      this.ResumeLayout();
      this.EditableElement = false;
    }
    __fault
    {
      base.Dispose(true);
    }
  }

  public event RemoveElementEventHandler RemoveElement;

  [SpecialName]
  protected void raise_RemoveElement(MemberCollectionDisplay value0)
  {
    RemoveElementEventHandler storeRemoveElement = this.\u003Cbacking_store\u003ERemoveElement;
    if (storeRemoveElement == null)
      return;
    storeRemoveElement(value0);
  }

  public event MoveElementEventHandler ElementUp;

  [SpecialName]
  protected void raise_ElementUp(MemberCollectionDisplay value0)
  {
    MoveElementEventHandler backingStoreElementUp = this.\u003Cbacking_store\u003EElementUp;
    if (backingStoreElementUp == null)
      return;
    backingStoreElementUp(value0);
  }

  public event MoveElementEventHandler ElementDown;

  [SpecialName]
  protected void raise_ElementDown(MemberCollectionDisplay value0)
  {
    MoveElementEventHandler storeElementDown = this.\u003Cbacking_store\u003EElementDown;
    if (storeElementDown == null)
      return;
    storeElementDown(value0);
  }

  public bool EditableElement
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.btnDelete.Visible;
    [param: MarshalAs(UnmanagedType.U1)] set
    {
      if (this.btnDelete.Visible == value)
        return;
      this.btnDelete.Visible = value;
      this.buttonUp.Visible = value;
      this.buttonDown.Visible = value;
      this.DoLayout();
    }
  }

  public MemberCollection Members => this.m_Members;

  public void ApplyChanges()
  {
    int index = 0;
    if (0 >= this.Controls.Count)
      return;
    do
    {
      if (this.Controls[index].GetType() == typeof (MemberDisplay))
        ((MemberDisplay) this.Controls[index]).ApplyChanges();
      else if (this.Controls[index].GetType() == typeof (ArrayMemberDisplay))
        ((ArrayMemberDisplay) this.Controls[index]).ApplyChanges();
      ++index;
    }
    while (index < this.Controls.Count);
  }

  public void DoLayout()
  {
    System.Windows.Forms.Control control1 = (System.Windows.Forms.Control) null;
    this.SuspendLayout();
    int y = 0;
    int x = 3;
    if (this.EditableElement)
      x = this.btnDelete.Size.Width + this.btnDelete.Location.X + 3;
    int index = 0;
    if (0 < this.Controls.Count)
    {
      do
      {
        if (this.Controls[index].GetType() == typeof (ArrayMemberDisplay))
        {
          ArrayMemberDisplay control2 = (ArrayMemberDisplay) this.Controls[index];
          control2.DoLayout();
          control1 = (System.Windows.Forms.Control) control2;
        }
        else if (this.Controls[index].GetType() == typeof (MemberDisplay))
          control1 = this.Controls[index];
        if (control1 != null)
        {
          Point point = new Point(x, y);
          control1.Location = point;
          Size size = control1.Size;
          y += size.Height - 1;
        }
        ++index;
      }
      while (index < this.Controls.Count);
    }
    this.ResumeLayout();
  }

  private void \u007EMemberCollectionDisplay() => this.components?.Dispose();

  private void InitializeComponent()
  {
    this.btnDelete = new Button();
    this.buttonUp = new Button();
    this.buttonDown = new Button();
    this.SuspendLayout();
    this.btnDelete.Location = new Point(3, 3);
    this.btnDelete.Name = "btnDelete";
    this.btnDelete.Size = new Size(44, 23);
    this.btnDelete.TabIndex = 0;
    this.btnDelete.Text = "Del";
    this.btnDelete.UseVisualStyleBackColor = true;
    this.btnDelete.Click += new EventHandler(this.btnDelete_Click);
    this.buttonUp.Location = new Point(3, 32 /*0x20*/);
    this.buttonUp.Name = "buttonUp";
    this.buttonUp.Size = new Size(44, 23);
    this.buttonUp.TabIndex = 1;
    this.buttonUp.Text = "Up";
    this.buttonUp.UseVisualStyleBackColor = true;
    this.buttonUp.Click += new EventHandler(this.buttonUp_Click);
    this.buttonDown.Location = new Point(3, 61);
    this.buttonDown.Name = "buttonDown";
    this.buttonDown.Size = new Size(44, 23);
    this.buttonDown.TabIndex = 2;
    this.buttonDown.Text = "Down";
    this.buttonDown.UseVisualStyleBackColor = true;
    this.buttonDown.Click += new EventHandler(this.buttonDown_Click);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoSize = true;
    this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.BorderStyle = BorderStyle.FixedSingle;
    this.Controls.Add((System.Windows.Forms.Control) this.buttonDown);
    this.Controls.Add((System.Windows.Forms.Control) this.buttonUp);
    this.Controls.Add((System.Windows.Forms.Control) this.btnDelete);
    this.Name = nameof (MemberCollectionDisplay);
    this.Size = new Size(50, 87);
    this.ResumeLayout(false);
  }

  private void btnDelete_Click(object sender, EventArgs e)
  {
    MemberCollectionDisplay collectionDisplay = this;
    collectionDisplay.raise_RemoveElement(collectionDisplay);
  }

  private void MemberSizeChanged(object sender, EventArgs e) => this.DoLayout();

  private void buttonUp_Click(object sender, EventArgs e)
  {
    MemberCollectionDisplay collectionDisplay = this;
    collectionDisplay.raise_ElementUp(collectionDisplay);
  }

  private void buttonDown_Click(object sender, EventArgs e)
  {
    MemberCollectionDisplay collectionDisplay = this;
    collectionDisplay.raise_ElementDown(collectionDisplay);
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EMemberCollectionDisplay();
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
