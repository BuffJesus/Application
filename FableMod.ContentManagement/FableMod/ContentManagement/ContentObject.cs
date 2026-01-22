// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.ContentObject
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.ContentManagement;

public class ContentObject : IDisposable
{
  protected string m_Name;
  protected object m_Object;
  protected ContentType m_Type;

  public ContentObject(string name, object @object, ContentType contentType)
  {
    this.m_Name = name;
    this.m_Object = @object;
    this.m_Type = contentType;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public ContentObject(string name, object @object)
  {
    this.m_Name = name;
    this.m_Object = @object;
    this.m_Type = ContentType.Unknown;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  private void \u007EContentObject()
  {
    this.m_Name = (string) null;
    this.m_Object = (object) null;
  }

  public string Name => this.m_Name;

  public object Object => this.m_Object;

  public ContentType Type => this.m_Type;

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.m_Name = (string) null;
      this.m_Object = (object) null;
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
