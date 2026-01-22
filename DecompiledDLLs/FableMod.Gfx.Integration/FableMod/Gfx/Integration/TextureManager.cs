// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.TextureManager
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using FableMod.BIG;
using FableMod.ContentManagement;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.Gfx.Integration;

public class TextureManager : IDisposable
{
  protected Dictionary<uint, GfxTexture> m_Textures = new Dictionary<uint, GfxTexture>();

  private void \u007ETextureManager() => this.Clear();

  public void Clear()
  {
    Dictionary<uint, GfxTexture>.Enumerator enumerator = this.m_Textures.GetEnumerator();
    if (enumerator.MoveNext())
    {
      do
      {
        enumerator.Current.Value.Dispose();
      }
      while (enumerator.MoveNext());
    }
    this.m_Textures.Clear();
  }

  public GfxTexture Get(uint id)
  {
    GfxTexture gfxTexture = (GfxTexture) null;
    if (this.m_Textures.TryGetValue(id, out gfxTexture))
      return gfxTexture;
    ContentObject entry = ContentManager.Instance.FindEntry(LinkDestination.MainTextureID, (object) id);
    if (entry != null)
    {
      gfxTexture = new GfxTexture((AssetEntry) entry.Object);
      this.m_Textures[id] = gfxTexture;
    }
    return gfxTexture;
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007ETextureManager();
    }
    else
    {
      // ISSUE: explicit finalizer call
      this.Finalize();
    }
  }

  public virtual void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }
}
