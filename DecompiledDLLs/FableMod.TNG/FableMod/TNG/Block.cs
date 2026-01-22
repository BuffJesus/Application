// Decompiled with JetBrains decompiler
// Type: FableMod.TNG.Block
// Assembly: FableMod.TNG, Version=1.0.4918.434, Culture=neutral, PublicKeyToken=null
// MVID: D9131661-A628-42D1-B5F7-4150ACB2CB8F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.TNG.dll

using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace FableMod.TNG;

public abstract class Block : Element
{
  protected Collection<Element> m_Elements = new Collection<Element>();

  private void \u007EBlock() => this.m_Elements = (Collection<Element>) null;

  public override void Load(TNGDefinitions definitions, XmlNode node)
  {
    base.Load(definitions, node);
    XmlNode node1 = node.FirstChild;
    if (node1 == null)
      return;
    do
    {
      this.LoadElement(definitions, node1);
      node1 = node1.NextSibling;
    }
    while (node1 != null);
  }

  public virtual Element Find(string name)
  {
    int index = 0;
    if (0 < this.m_Elements.Count)
    {
      while (!(this.m_Elements[index].m_Name == name))
      {
        ++index;
        if (index >= this.m_Elements.Count)
          goto label_4;
      }
      return this.m_Elements[index];
    }
label_4:
    return (Element) null;
  }

  public override void CopyTo(Element element)
  {
    base.CopyTo(element);
    Block block = (Block) element;
    foreach (Element element1 in this.m_Elements)
      block.Add(element1.Duplicate());
  }

  public void Add(Element type)
  {
    this.m_Elements.Add(type);
    type.Changed += new ElementChangedHandler(this.ChildChanged);
  }

  public void Clear() => this.m_Elements?.Clear();

  public override bool Modified
  {
    [return: MarshalAs(UnmanagedType.U1)] get
    {
      int index = 0;
      if (0 < this.m_Elements.Count)
      {
        while (!this.m_Elements[index].Modified)
        {
          ++index;
          if (index >= this.m_Elements.Count)
            goto label_4;
        }
        return true;
      }
label_4:
      return false;
    }
    [param: MarshalAs(UnmanagedType.U1)] set
    {
      int index = 0;
      if (0 >= this.m_Elements.Count)
        return;
      do
      {
        this.m_Elements[index].Modified = value;
        ++index;
      }
      while (index < this.m_Elements.Count);
    }
  }

  public override void ToDefault()
  {
    int index = 0;
    if (0 >= this.m_Elements.Count)
      return;
    do
    {
      this.m_Elements[index].ToDefault();
      ++index;
    }
    while (index < this.m_Elements.Count);
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public override bool IsDefault()
  {
    int index = 0;
    if (0 < this.m_Elements.Count)
    {
      while (!this.m_Elements[index].HasDefault || this.m_Elements[index].IsDefault())
      {
        ++index;
        if (index >= this.m_Elements.Count)
          goto label_4;
      }
      return false;
    }
label_4:
    return true;
  }

  public override bool HasDefault
  {
    [return: MarshalAs(UnmanagedType.U1)] get
    {
      int index = 0;
      if (0 < this.m_Elements.Count)
      {
        while (!this.m_Elements[index].HasDefault)
        {
          ++index;
          if (index >= this.m_Elements.Count)
            goto label_4;
        }
        return true;
      }
label_4:
      return false;
    }
  }

  public override bool SaveDefault
  {
    [return: MarshalAs(UnmanagedType.U1)] get
    {
      if (!this.m_SaveDefault)
        return false;
      int index = 0;
      if (0 < this.m_Elements.Count)
      {
        while (!this.m_Elements[index].SaveDefault)
        {
          ++index;
          if (index >= this.m_Elements.Count)
            goto label_6;
        }
        return true;
      }
label_6:
      return true;
    }
  }

  public int ElementCount => this.m_Elements.Count;

  public Element get_Elements(int index) => this.m_Elements[index];

  protected void ChildChanged(Element element)
  {
    this.HandleChange();
    Block block = this;
    block.OnChanged((Element) block);
  }

  protected virtual void HandleChange()
  {
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected virtual bool LoadElement(TNGDefinitions definitions, XmlNode node) => false;

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.m_Elements = (Collection<Element>) null;
      }
      finally
      {
        base.Dispose(true);
      }
    }
    else
      base.Dispose(false);
  }
}
