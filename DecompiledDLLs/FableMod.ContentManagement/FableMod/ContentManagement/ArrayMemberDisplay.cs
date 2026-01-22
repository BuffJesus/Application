// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.ArrayMemberDisplay
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

public class ArrayMemberDisplay : UserControl
{
  private Label lblName;
  private ArrayMember m_Array;
  private ContentManager m_ContentMgr;
  private Button btnAddNewElement;
  private ToolTip ttComments;
  private Panel panelSort;
  private TextBox textBoxElement;
  private Button buttonSort;
  private IContainer components;

  public ArrayMemberDisplay(ArrayMember arr, ContentManager contentmgr)
  {
    // ISSUE: fault handler
    try
    {
      this.InitializeComponent();
      this.m_ContentMgr = contentmgr;
      this.m_Array = arr;
      this.BuildLayout();
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
      if (this.Controls[index].GetType() == typeof (MemberCollection))
        ((MemberCollectionDisplay) this.Controls[index]).ApplyChanges();
      ++index;
    }
    while (index < this.Controls.Count);
  }

  public void DoLayout()
  {
    this.lblName.Text = this.m_Array.Name + "[" + this.m_Array.Elements.Count.ToString() + "]";
    this.btnAddNewElement.Left = this.lblName.Right + 3;
    this.panelSort.Left = this.btnAddNewElement.Right + 3;
    this.SuspendLayout();
    int y = this.btnAddNewElement.Size.Height + this.btnAddNewElement.Location.Y + 3;
    int num = 0;
    int index1 = 0;
    if (0 < this.Controls.Count)
    {
      do
      {
        if (this.Controls[index1].GetType() == typeof (MemberCollectionDisplay))
        {
          MemberCollectionDisplay control = (MemberCollectionDisplay) this.Controls[index1];
          Point point = new Point(0, y);
          control.Location = point;
          if (control.Width > num)
            num = control.Width;
          Size size = control.Size;
          y += size.Height - 1;
        }
        ++index1;
      }
      while (index1 < this.Controls.Count);
    }
    int index2 = 0;
    if (0 < this.Controls.Count)
    {
      do
      {
        if (this.Controls[index2].GetType() == typeof (MemberCollectionDisplay))
          this.Controls[index2].Width = num;
        ++index2;
      }
      while (index2 < this.Controls.Count);
    }
    this.ResumeLayout();
  }

  public ArrayMember Array => this.m_Array;

  private void \u007EArrayMemberDisplay() => this.components?.Dispose();

  private void BuildLayout()
  {
    this.SuspendLayout();
    int index = 0;
    if (0 < this.m_Array.Elements.Count)
    {
      do
      {
        MemberCollectionDisplay collectionDisplay = new MemberCollectionDisplay(this.m_Array.Elements[index], this.m_ContentMgr);
        collectionDisplay.RemoveElement += new RemoveElementEventHandler(this.OnRemoveElement);
        collectionDisplay.ElementUp += new MoveElementEventHandler(this.OnElementUp);
        collectionDisplay.ElementDown += new MoveElementEventHandler(this.OnElementDown);
        collectionDisplay.EditableElement = true;
        collectionDisplay.SizeChanged += new EventHandler(this.MemberSizeChanged);
        this.Controls.Add((System.Windows.Forms.Control) collectionDisplay);
        ++index;
      }
      while (index < this.m_Array.Elements.Count);
    }
    ArrayMember array1 = this.m_Array;
    if (array1.Comments != null && array1.Comments != "")
    {
      ArrayMember array2 = this.m_Array;
      this.ttComments.SetToolTip((System.Windows.Forms.Control) this.lblName, "Array " + array2.Name + " - " + array2.Comments);
    }
    this.ResumeLayout();
    this.DoLayout();
  }

  private void OnRemoveElement(MemberCollectionDisplay element)
  {
    this.m_Array.Elements.Remove(element.Members);
    this.m_Array.UpdateCount();
    this.Controls.Remove((System.Windows.Forms.Control) element);
    this.DoLayout();
  }

  private void OnElementUp(MemberCollectionDisplay element)
  {
    int index = this.m_Array.Elements.IndexOf(element.Members);
    if (index > 0)
    {
      MemberCollection element1 = this.m_Array.Elements[index - 1];
      this.m_Array.Elements[index - 1] = this.m_Array.Elements[index];
      this.m_Array.Elements[index] = element1;
      int childIndex = this.Controls.GetChildIndex((System.Windows.Forms.Control) element);
      this.Controls.SetChildIndex((System.Windows.Forms.Control) element, childIndex - 1);
      this.Controls.GetChildIndex((System.Windows.Forms.Control) element);
    }
    this.DoLayout();
  }

  private void OnElementDown(MemberCollectionDisplay element)
  {
    int index = this.m_Array.Elements.IndexOf(element.Members);
    if (index < this.m_Array.Elements.Count - 1)
    {
      MemberCollection element1 = this.m_Array.Elements[index + 1];
      this.m_Array.Elements[index + 1] = this.m_Array.Elements[index];
      this.m_Array.Elements[index] = element1;
      int childIndex = this.Controls.GetChildIndex((System.Windows.Forms.Control) element);
      this.Controls.SetChildIndex((System.Windows.Forms.Control) element, childIndex + 1);
      this.Controls.GetChildIndex((System.Windows.Forms.Control) element);
    }
    this.DoLayout();
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.lblName = new Label();
    this.btnAddNewElement = new Button();
    ArrayMemberDisplay arrayMemberDisplay = this;
    arrayMemberDisplay.ttComments = new ToolTip(arrayMemberDisplay.components);
    this.panelSort = new Panel();
    this.buttonSort = new Button();
    this.textBoxElement = new TextBox();
    this.panelSort.SuspendLayout();
    this.SuspendLayout();
    this.lblName.AutoSize = true;
    this.lblName.Location = new Point(3, 8);
    this.lblName.Margin = new Padding(3);
    this.lblName.MaximumSize = new Size(0, 13);
    this.lblName.Name = "lblName";
    this.lblName.Size = new Size(97, 13);
    this.lblName.TabIndex = 0;
    this.lblName.Text = "ArrayMemberName";
    this.btnAddNewElement.Location = new Point(157, 3);
    this.btnAddNewElement.Name = "btnAddNewElement";
    this.btnAddNewElement.Size = new Size(41, 23);
    this.btnAddNewElement.TabIndex = 1;
    this.btnAddNewElement.Text = "Add Element";
    this.btnAddNewElement.UseVisualStyleBackColor = true;
    this.btnAddNewElement.Click += new EventHandler(this.btnAddNewElement_Click);
    this.panelSort.Controls.Add((System.Windows.Forms.Control) this.textBoxElement);
    this.panelSort.Controls.Add((System.Windows.Forms.Control) this.buttonSort);
    this.panelSort.Location = new Point(204, 0);
    this.panelSort.Name = "panelSort";
    this.panelSort.Size = new Size(113, 29);
    this.panelSort.TabIndex = 2;
    this.buttonSort.Location = new Point(3, 3);
    this.buttonSort.Name = "buttonSort";
    this.buttonSort.Size = new Size(54, 23);
    this.buttonSort.TabIndex = 0;
    this.buttonSort.Text = "Sort";
    this.buttonSort.UseVisualStyleBackColor = true;
    this.buttonSort.Click += new EventHandler(this.buttonSort_Click);
    this.textBoxElement.Location = new Point(63 /*0x3F*/, 5);
    this.textBoxElement.Name = "textBoxElement";
    this.textBoxElement.Size = new Size(44, 20);
    this.textBoxElement.TabIndex = 1;
    this.textBoxElement.Text = "0";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoSize = true;
    this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.Controls.Add((System.Windows.Forms.Control) this.panelSort);
    this.Controls.Add((System.Windows.Forms.Control) this.btnAddNewElement);
    this.Controls.Add((System.Windows.Forms.Control) this.lblName);
    this.Name = nameof (ArrayMemberDisplay);
    this.Size = new Size(320, 32 /*0x20*/);
    this.panelSort.ResumeLayout(false);
    this.panelSort.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void btnAddNewElement_Click(object sender, EventArgs e)
  {
    MemberCollection element = this.m_Array.CreateElement();
    MemberCollectionDisplay collectionDisplay = new MemberCollectionDisplay(element, this.m_ContentMgr);
    collectionDisplay.EditableElement = true;
    collectionDisplay.RemoveElement += new RemoveElementEventHandler(this.OnRemoveElement);
    collectionDisplay.ElementUp += new MoveElementEventHandler(this.OnElementUp);
    collectionDisplay.ElementDown += new MoveElementEventHandler(this.OnElementDown);
    this.m_Array.Elements.Add(element);
    this.m_Array.UpdateCount();
    this.Controls.Add((System.Windows.Forms.Control) collectionDisplay);
    this.DoLayout();
  }

  private void MemberSizeChanged(object sender, EventArgs e) => this.DoLayout();

  private void buttonSort_Click(object sender, EventArgs e)
  {
    int index1 = 0;
    int index2 = 0;
    if (0 < this.Controls.Count)
    {
      while (!(this.Controls[index2].GetType() == typeof (MemberCollectionDisplay)))
      {
        ++index2;
        if (index2 >= this.Controls.Count)
          goto label_4;
      }
      index1 = index2;
    }
label_4:
    this.m_Array.Sort(int.Parse(this.textBoxElement.Text));
    this.SuspendLayout();
    int num1 = this.Controls.Count - index1;
    if (0 < num1)
    {
      int num2 = num1;
      do
      {
        this.Controls.RemoveAt(index1);
        num2 += -1;
      }
      while (num2 != 0);
    }
    this.ResumeLayout();
    this.BuildLayout();
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EArrayMemberDisplay();
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
