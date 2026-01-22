// Decompiled with JetBrains decompiler
// Type: FableMod.TNG.ElementArray
// Assembly: FableMod.TNG, Version=1.0.4918.434, Culture=neutral, PublicKeyToken=null
// MVID: D9131661-A628-42D1-B5F7-4150ACB2CB8F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.TNG.dll

using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace FableMod.TNG;

public class ElementArray : ComplexBlock
{
  protected Element m_Base;

  private void \u007EElementArray() => this.m_Base = (Element) null;

  public Element Add()
  {
    Element type = this.m_Base.Duplicate();
    int elementCount = this.ElementCount;
    string name = type.m_Name;
    type.m_Name = name.Replace("*", elementCount.ToString());
    this.Add(type);
    this.Modified = true;
    return type;
  }

  public Element MatchAdd(string name)
  {
    int num1 = name.IndexOf("[");
    int startIndex1 = name.IndexOf("]", num1 + 1);
    string str1 = name.Substring(0, num1 + 1) + name.Substring(startIndex1);
    int num2 = this.m_Base.m_Name.IndexOf("[");
    int startIndex2 = this.m_Base.m_Name.IndexOf("]", num2 + 1);
    string name1 = this.m_Base.m_Name;
    string str2 = name1;
    string str3 = name1.Substring(0, num2 + 1) + str2.Substring(startIndex2);
    if (!(str1 == str3))
      return (Element) null;
    int index = int.Parse(name.Substring(num1 + 1, startIndex1 - num1 - 1));
    if (index >= this.m_Elements.Count)
    {
      do
      {
        this.Add();
      }
      while (index >= this.m_Elements.Count);
    }
    Element element = this.m_Elements[index];
    int length = name.Length;
    return element;
  }

  public override void CopyTo(Element element)
  {
    base.CopyTo(element);
    ((ElementArray) element).m_Base = this.m_Base.Duplicate();
  }

  public override void Load(TNGDefinitions definitions, XmlNode node)
  {
    base.Load(definitions, node);
    node = node.FirstChild;
    if (node.Name == "array")
    {
      ElementArray elementArray = (ElementArray) this.Find(node.Attributes["name"].InnerText);
      if (elementArray == null)
      {
        elementArray = new ElementArray();
        elementArray.Load(definitions, node);
      }
      else
        elementArray.Load(definitions, node);
      this.m_Base = (Element) elementArray;
    }
    else if (node.Name == "arrayref")
      this.m_Base = definitions.Find(node.Attributes["name"].InnerText).Duplicate();
    else if (node.Name == "variable")
    {
      Variable variable = (Variable) this.Find(node.Attributes["name"].InnerText);
      if (variable == null)
      {
        variable = new Variable();
        variable.Load(definitions, node);
      }
      else
        variable.Load(definitions, node);
      this.m_Base = (Element) variable;
    }
    else if (node.Name == "varref")
      this.m_Base = definitions.Find(node.Attributes["name"].InnerText).Duplicate();
    if (this.m_Base == null)
      throw new Exception("FableMod::TNG: Array base member not defined");
  }

  protected override Element Factory() => (Element) new ElementArray();

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EElementArray();
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
