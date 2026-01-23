// Decompiled with JetBrains decompiler
// Type: ChocolateBox.Progress
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.CLRCore;

#nullable disable
namespace ChocolateBox;

public class Progress : ProgressInterface
{
  private string myInfo;
  private string myStepInfo;
  private float myValue;

  protected override void SetValue(float value) => this.myValue = value;

  public string Info
  {
    get => this.myInfo;
    set => this.myInfo = value;
  }

  public string StepInfo
  {
    get => this.myStepInfo;
    set => this.myStepInfo = value;
  }

  public float Value
  {
    get => this.myValue;
    set => this.SetValue(value);
  }
}
