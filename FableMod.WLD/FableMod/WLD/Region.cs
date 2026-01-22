// Decompiled with JetBrains decompiler
// Type: FableMod.WLD.Region
// Assembly: FableMod.WLD, Version=1.0.4918.436, Culture=neutral, PublicKeyToken=null
// MVID: C116F1D2-4A42-43FB-9046-16C428742204
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.WLD.dll

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.WLD;

public class Region : IDisposable
{
  protected int m_ID;
  protected bool m_AppearOnWorldMap;
  protected string m_RegionName;
  protected string m_NewDisplayName;
  protected string m_RegionDef;
  protected string m_MiniMap;
  protected float m_MiniMapScale;
  protected float m_MiniMapOffsetX;
  protected float m_MiniMapOffsetY;
  protected float m_WorldMapOffsetX;
  protected float m_WorldMapOffsetY;
  protected float m_NameGraphicOffsetX;
  protected float m_NameGraphicOffsetY;
  protected Collection<Map> m_Contains = new Collection<Map>();
  protected Collection<Map> m_Sees = new Collection<Map>();

  private void \u007ERegion()
  {
  }

  public void Save(TextWriter writer)
  {
  }

  public int ID
  {
    get => this.m_ID;
    set => this.m_ID = value;
  }

  public float MiniMapScale
  {
    get => this.m_MiniMapScale;
    set => this.m_MiniMapScale = value;
  }

  public bool AppearOnWorldMap
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_AppearOnWorldMap;
    [param: MarshalAs(UnmanagedType.U1)] set => this.m_AppearOnWorldMap = value;
  }

  public string RegionName
  {
    get => this.m_RegionName;
    set => this.m_RegionName = value;
  }

  public string NewDisplayName
  {
    get => this.m_NewDisplayName;
    set => this.m_NewDisplayName = value;
  }

  public string RegionDef
  {
    get => this.m_RegionDef;
    set => this.m_RegionDef = value;
  }

  public string MiniMap
  {
    get => this.m_MiniMap;
    set => this.m_MiniMap = value;
  }

  public float MiniMapOffsetX
  {
    get => this.m_MiniMapOffsetX;
    set => this.m_MiniMapOffsetX = value;
  }

  public float MiniMapOffsetY
  {
    get => this.m_MiniMapOffsetY;
    set => this.m_MiniMapOffsetY = value;
  }

  public float WorldMapOffsetX
  {
    get => this.m_WorldMapOffsetX;
    set => this.m_WorldMapOffsetX = value;
  }

  public float WorldMapOffsetY
  {
    get => this.m_WorldMapOffsetY;
    set => this.m_WorldMapOffsetY = value;
  }

  public float NameGraphicOffsetX
  {
    get => this.m_NameGraphicOffsetX;
    set => this.m_NameGraphicOffsetX = value;
  }

  public float NameGraphicOffsetY
  {
    get => this.m_NameGraphicOffsetY;
    set => this.m_NameGraphicOffsetY = value;
  }

  public Collection<Map> SeesMaps => this.m_Sees;

  public Collection<Map> ContainsMaps => this.m_Contains;

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
      return;
    // ISSUE: explicit finalizer call
    this.Finalize();
  }

  public virtual void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }
}
