// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.ButtonComparer
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using System.Collections.Generic;

#nullable disable
namespace FableMod.Gfx.Integration;

internal class ButtonComparer : IComparer<CfgButton>
{
  public int GetModifiers(Buttons button)
  {
    int num = 0;
    int modifiers = (button & Buttons.Ctrl) == Buttons.Ctrl ? 1 : num;
    if ((button & Buttons.Alt) == Buttons.Alt)
      ++modifiers;
    if ((button & Buttons.Shift) == Buttons.Shift)
      ++modifiers;
    return modifiers;
  }

  public virtual int Compare(CfgButton a, CfgButton b)
  {
    Buttons button1 = a.Button;
    Buttons button2 = b.Button;
    if (button1 == button2)
      return 0;
    bool flag1 = (button1 & Buttons.Mouse) != Buttons.None;
    bool flag2 = (button2 & Buttons.Mouse) != Buttons.None;
    if (flag1)
    {
      if (!flag2)
        return -1;
      bool flag3 = (button1 & Buttons.Double) == Buttons.Double;
      bool flag4 = (button2 & Buttons.Double) == Buttons.Double;
      if (flag3)
      {
        if (!flag4)
          return -1;
      }
      else if (flag4)
        return 1;
      bool flag5 = (button1 & Buttons.Keys) != Buttons.None;
      bool flag6 = (button2 & Buttons.Keys) != Buttons.None;
      if (flag5)
      {
        if (!flag6)
          return -1;
      }
      else if (flag6)
        return 1;
      int modifiers1 = this.GetModifiers(button1);
      int modifiers2 = this.GetModifiers(button2);
      if (modifiers1 > modifiers2)
        return -1;
      if (modifiers2 > modifiers1)
        return 1;
    }
    else
    {
      if (flag2)
        return 1;
      int num1 = 0;
      int num2 = (button1 & Buttons.Ctrl) == Buttons.Ctrl ? 1 : num1;
      if ((button1 & Buttons.Alt) == Buttons.Alt)
        ++num2;
      if ((button1 & Buttons.Shift) == Buttons.Shift)
        ++num2;
      int num3 = 0;
      int num4 = (button2 & Buttons.Ctrl) == Buttons.Ctrl ? 1 : num3;
      if ((button2 & Buttons.Alt) == Buttons.Alt)
        ++num4;
      if ((button2 & Buttons.Shift) == Buttons.Shift)
        ++num4;
      if (num2 > num4)
        return -1;
      if (num4 > num2)
        return 1;
    }
    return 0;
  }
}
