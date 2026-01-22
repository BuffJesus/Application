// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.Theme
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

#nullable disable
namespace FableMod.Gfx.Integration;

public class Theme
{
  private GfxTexture m_Texture;
  private GfxModel m_Model;

  public GfxTexture Texture
  {
    get => this.m_Texture;
    set => this.m_Texture = value;
  }

  public GfxModel Model
  {
    get => this.m_Model;
    set => this.m_Model = value;
  }
}
