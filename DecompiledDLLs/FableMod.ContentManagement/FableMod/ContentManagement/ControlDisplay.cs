// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.ControlDisplay
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace FableMod.ContentManagement;

public class ControlDisplay : UserControl
{
  private Label lblName;
  private ContentManager m_ContentMgr;
  private Control m_Control;
  private System.ComponentModel.Container components;

  public ControlDisplay(Control control, ContentManager contentmgr)
  {
    // ISSUE: fault handler
    try
    {
      this.InitializeComponent();
      this.m_ContentMgr = contentmgr;
      this.m_Control = control;
      this.lblName.Text = control.ToString();
      MemberCollectionDisplay collectionDisplay = new MemberCollectionDisplay(this.m_Control.Members, this.m_ContentMgr);
      Point point = new Point(0, this.lblName.Size.Height + this.lblName.Location.Y + 3);
      collectionDisplay.Location = point;
      this.Controls.Add((System.Windows.Forms.Control) collectionDisplay);
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
      if (this.Controls[index].GetType() == typeof (MemberCollectionDisplay))
        ((MemberCollectionDisplay) this.Controls[index]).ApplyChanges();
      ++index;
    }
    while (index < this.Controls.Count);
  }

  private void \u007EControlDisplay() => this.components?.Dispose();

  private void InitializeComponent()
  {
    this.lblName = new Label();
    this.SuspendLayout();
    this.lblName.AutoSize = true;
    this.lblName.Location = new Point(3, 3);
    this.lblName.Margin = new Padding(3);
    this.lblName.MaximumSize = new Size(190, 13);
    this.lblName.Name = "lblName";
    this.lblName.Size = new Size(188, 13);
    this.lblName.TabIndex = 0;
    this.lblName.Text = "FFFFFFFF - ControlNameXXXXXXXXX";
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.AutoSize = true;
    this.AutoSizeMode = AutoSizeMode.GrowAndShrink;
    this.Controls.Add((System.Windows.Forms.Control) this.lblName);
    this.Name = nameof (ControlDisplay);
    this.Size = new Size(194, 19);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EControlDisplay();
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
