// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.ModelManager
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

public class ModelManager : IDisposable
{
  protected Dictionary<uint, GfxModel> m_Models = new Dictionary<uint, GfxModel>();
  protected Dictionary<string, GfxModel> m_NameModels = new Dictionary<string, GfxModel>();
  protected Dictionary<string, Building> m_Buildings;

  private void \u007EModelManager() => this.Clear();

  public void Clear()
  {
    Dictionary<uint, GfxModel>.Enumerator enumerator = this.m_Models.GetEnumerator();
    if (enumerator.MoveNext())
    {
      do
      {
        enumerator.Current.Value.Dispose();
      }
      while (enumerator.MoveNext());
    }
    this.m_Models.Clear();
    this.m_NameModels.Clear();
  }

  public GfxModel Get(string name)
  {
    GfxModel gfxModel = (GfxModel) null;
    if (this.m_NameModels.TryGetValue(name, out gfxModel))
      return gfxModel;
    ContentObject entry = ContentManager.Instance.FindEntry(LinkDestination.ModelName, (object) name);
    return entry != null ? this.AddModel((AssetEntry) entry.Object) : (GfxModel) null;
  }

  public GfxModel Get(uint id)
  {
    GfxModel gfxModel = (GfxModel) null;
    if (this.m_Models.TryGetValue(id, out gfxModel))
      return gfxModel;
    ContentObject entry = ContentManager.Instance.FindEntry(LinkDestination.ModelID, (object) id);
    return entry != null ? this.AddModel((AssetEntry) entry.Object) : (GfxModel) null;
  }

  protected GfxModel AddModel(AssetEntry entry)
  {
    GfxModel gfxModel = new GfxModel(entry);
    this.m_Models[entry.ID] = gfxModel;
    this.m_NameModels[entry.DevSymbolName] = gfxModel;
    return gfxModel;
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EModelManager();
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
