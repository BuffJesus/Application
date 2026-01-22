// Decompiled with JetBrains decompiler
// Type: FableMod.TNG.ComplexBlock
// Assembly: FableMod.TNG, Version=1.0.4918.434, Culture=neutral, PublicKeyToken=null
// MVID: D9131661-A628-42D1-B5F7-4150ACB2CB8F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.TNG.dll

using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace FableMod.TNG;

public abstract class ComplexBlock : VariableBlock
{
  private void \u007EComplexBlock()
  {
  }

  public override Element Find(string name)
  {
    int length = name.IndexOf(".");
    int num1 = name.IndexOf("[");
    int num2 = num1 < 0 ? -1 : name.IndexOf("]");
    bool flag = num1 >= 0 && num2 >= 0;
    if (length >= 0 && (!flag || length < num1 && length < num2))
    {
      string str = name.Substring(0, length);
      string name1 = name.Substring(length + 1);
      int index = 0;
      if (0 < this.ElementCount)
      {
        Element element;
        do
        {
          if (this.get_Elements(index).GetType() == typeof (ElementStruct) && this.get_Elements(index).m_Name == str)
          {
            element = ((ComplexBlock) this.get_Elements(index)).Find(name1);
            if (element != null)
              goto label_5;
          }
          ++index;
        }
        while (index < this.ElementCount);
        goto label_6;
label_5:
        return element;
      }
label_6:
      return (Element) null;
    }
    if (!flag)
      return base.Find(name);
    int index1 = 0;
    if (0 < this.ElementCount)
    {
      Element element;
      do
      {
        if (this.get_Elements(index1).GetType() == typeof (ElementArray))
        {
          element = ((ElementArray) this.get_Elements(index1)).MatchAdd(name);
          if (element != null)
            goto label_12;
        }
        ++index1;
      }
      while (index1 < this.ElementCount);
      goto label_13;
label_12:
      return element;
    }
label_13:
    return (Element) null;
  }

  public Collection<ElementArray> Arrays
  {
    get
    {
      Collection<ElementArray> arrays = new Collection<ElementArray>();
      int index = 0;
      if (0 < this.ElementCount)
      {
        do
        {
          if (this.get_Elements(index).GetType() == typeof (ElementArray))
            arrays.Add((ElementArray) this.get_Elements(index));
          ++index;
        }
        while (index < this.ElementCount);
      }
      return arrays;
    }
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected override bool LoadElement(TNGDefinitions definitions, XmlNode node)
  {
    if (base.LoadElement(definitions, node))
      return true;
    if (node.Name == "array")
    {
      ElementArray elementArray = (ElementArray) this.Find(node.Attributes["name"].InnerText);
      if (elementArray == null)
      {
        ElementArray type = new ElementArray();
        type.Load(definitions, node);
        this.Add((Element) type);
      }
      else
        elementArray.Load(definitions, node);
      return true;
    }
    if (node.Name == "arrayref")
    {
      ElementArray elementArray = (ElementArray) definitions.Find(node.Attributes["name"].InnerText);
      if (elementArray != null)
        this.Add(elementArray.Duplicate());
      return true;
    }
    if (node.Name == "struct")
    {
      ElementStruct elementStruct = (ElementStruct) this.Find(node.Attributes["name"].InnerText);
      if (elementStruct == null)
      {
        ElementStruct type = new ElementStruct();
        type.Load(definitions, node);
        this.Add((Element) type);
      }
      else
        elementStruct.Load(definitions, node);
      return true;
    }
    if (!(node.Name == "structref"))
      return false;
    ElementStruct elementStruct1 = (ElementStruct) definitions.Find(node.Attributes["name"].InnerText);
    if (elementStruct1 != null)
      this.Add(elementStruct1.Duplicate());
    return true;
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EComplexBlock();
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
