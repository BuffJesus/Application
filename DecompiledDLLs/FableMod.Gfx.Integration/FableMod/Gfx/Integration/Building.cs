// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.Building
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.Gfx.Integration;

public class Building : IDisposable
{
  private GfxModel m_Interior;
  private GfxModel m_Exterior;

  private Building()
  {
  }

  private void \u007EBuilding()
  {
    this.m_Interior = (GfxModel) null;
    this.m_Exterior = (GfxModel) null;
  }

  private GfxModel Interior
  {
    get => this.m_Interior;
    set => this.m_Interior = value;
  }

  private GfxModel Exterior
  {
    get => this.m_Exterior;
    set => this.m_Exterior = value;
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.m_Interior = (GfxModel) null;
      this.m_Exterior = (GfxModel) null;
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
