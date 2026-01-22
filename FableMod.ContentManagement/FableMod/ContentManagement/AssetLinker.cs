// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.AssetLinker
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using System;
using System.Collections.Specialized;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.ContentManagement;

public class AssetLinker : IDisposable
{
  private void \u007EAssetLinker()
  {
  }

  public virtual NameValueCollection CreateLinks() => (NameValueCollection) null;

  public virtual void ApplyLinks(NameValueCollection c)
  {
  }

  protected AssetLinker()
  {
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EAssetLinker();
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
