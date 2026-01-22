// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.CfgButton
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.Gfx.Integration;

internal class CfgButton(string name, Buttons button)
{
  private string m_Name = name;
  private Buttons m_Button = button;
  private bool m_Pressed = false;
  private bool m_Enabled = true;

  public string Name => this.m_Name;

  public Buttons Button
  {
    get => this.m_Button;
    set => this.m_Button = value;
  }

  public bool Pressed
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_Pressed;
    [param: MarshalAs(UnmanagedType.U1)] set => this.m_Pressed = value;
  }

  public bool Enabled
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_Enabled;
    [param: MarshalAs(UnmanagedType.U1)] set => this.m_Enabled = value;
  }
}
