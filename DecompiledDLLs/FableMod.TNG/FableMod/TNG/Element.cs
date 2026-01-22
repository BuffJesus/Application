// Decompiled with JetBrains decompiler
// Type: FableMod.TNG.Element
// Assembly: FableMod.TNG, Version=1.0.4918.434, Culture=neutral, PublicKeyToken=null
// MVID: D9131661-A628-42D1-B5F7-4150ACB2CB8F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.TNG.dll

using System;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace FableMod.TNG;

public abstract class Element : IDisposable
{
  protected string m_Name;
  protected string m_Comments;
  protected bool m_SaveDefault = true;

  private void \u007EElement()
  {
    this.m_Name = (string) null;
    this.m_Comments = (string) null;
    this.Destroy();
  }

  private void \u0021Element() => this.Destroy();

  public virtual void Destroy()
  {
  }

  public virtual Element Duplicate()
  {
    Element element = this.Factory();
    this.CopyTo(element);
    return element;
  }

  public virtual void Load(TNGDefinitions definitions, XmlNode node)
  {
    XmlAttribute attribute1 = node.Attributes["inherits"];
    if (attribute1 != null)
    {
      string innerText = attribute1.InnerText;
      definitions.Find(innerText)?.CopyTo(this);
    }
    this.m_Name = node.Attributes["name"].InnerText;
    XmlAttribute attribute2 = node.Attributes["comments"];
    if (attribute2 != null)
      this.m_Comments = attribute2.InnerText;
    XmlAttribute attribute3 = node.Attributes["savedefault"];
    if (attribute3 == null)
      return;
    this.m_SaveDefault = bool.Parse(attribute3.InnerText);
  }

  public virtual void Save(TextWriter writer)
  {
  }

  public virtual void CopyTo(Element element)
  {
    element.m_Name = this.m_Name;
    element.m_Comments = this.m_Comments;
    element.m_SaveDefault = this.m_SaveDefault;
  }

  public string Name
  {
    get => this.m_Name;
    set => this.m_Name = value;
  }

  public string Comments => this.m_Comments;

  public virtual bool Modified
  {
    [return: MarshalAs(UnmanagedType.U1)] get => false;
    [param: MarshalAs(UnmanagedType.U1)] set
    {
    }
  }

  public virtual void ToDefault()
  {
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public virtual bool IsDefault() => false;

  public virtual bool HasDefault
  {
    [return: MarshalAs(UnmanagedType.U1)] get => false;
  }

  public virtual bool SaveDefault
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_SaveDefault;
  }

  public event ElementChangedHandler Changed;

  [SpecialName]
  protected void raise_Changed(Element value0)
  {
    ElementChangedHandler backingStoreChanged = this.\u003Cbacking_store\u003EChanged;
    if (backingStoreChanged == null)
      return;
    backingStoreChanged(value0);
  }

  protected void OnChanged(Element element)
  {
    ElementChangedHandler backingStoreChanged = this.\u003Cbacking_store\u003EChanged;
    if (backingStoreChanged == null)
      return;
    backingStoreChanged(element);
  }

  protected void HandleInheritance(TNGDefinitions definitions, string name)
  {
    definitions.Find(name)?.CopyTo(this);
  }

  protected virtual Element Factory()
  {
    throw new Exception("FableMod::TNG: Element factory called");
  }

  [HandleProcessCorruptedStateExceptions]
  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EElement();
    }
    else
    {
      try
      {
        this.\u0021Element();
      }
      finally
      {
        // ISSUE: explicit finalizer call
        base.Finalize();
      }
    }
  }

  public virtual void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }

  ~Element() => this.Dispose(false);
}
