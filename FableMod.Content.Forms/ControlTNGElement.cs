// Decompiled with JetBrains decompiler
// Type: FableMod.Content.Forms.ControlTNGElement
// Assembly: FableMod.Content.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A575F982-23D4-4DAE-A571-3D769462C59B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Content.Forms.dll

using System.ComponentModel;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Content.Forms;

public class ControlTNGElement : UserControl
{
  private IContainer components;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.AutoScaleMode = AutoScaleMode.Font;
  }

  public ControlTNGElement() => this.InitializeComponent();

  public virtual int TotalHeight => 0;

  public virtual void ApplySelectUID(SelectUIDHandler handler)
  {
  }
}
