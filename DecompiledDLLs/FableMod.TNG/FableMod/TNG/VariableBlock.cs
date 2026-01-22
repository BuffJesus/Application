// Decompiled with JetBrains decompiler
// Type: FableMod.TNG.VariableBlock
// Assembly: FableMod.TNG, Version=1.0.4918.434, Culture=neutral, PublicKeyToken=null
// MVID: D9131661-A628-42D1-B5F7-4150ACB2CB8F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.TNG.dll

using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace FableMod.TNG;

public abstract class VariableBlock : Block
{
  private void \u007EVariableBlock()
  {
  }

  public override void Save(TextWriter writer)
  {
    int index = 0;
    if (0 >= this.ElementCount)
      return;
    do
    {
      this.get_Elements(index).Save(writer);
      ++index;
    }
    while (index < this.ElementCount);
  }

  public Variable get_Variables(string name)
  {
    int index = 0;
    if (0 < this.ElementCount)
    {
      while (!(this.get_Elements(index).GetType() == typeof (Variable)) || !(this.get_Elements(index).m_Name == name))
      {
        ++index;
        if (index >= this.ElementCount)
          goto label_4;
      }
      return (Variable) this.get_Elements(index);
    }
label_4:
    return (Variable) null;
  }

  public Collection<Variable> Variables
  {
    get
    {
      Collection<Variable> variables = new Collection<Variable>();
      int index = 0;
      if (0 < this.ElementCount)
      {
        do
        {
          if (this.get_Elements(index).GetType() == typeof (Variable))
            variables.Add((Variable) this.get_Elements(index));
          ++index;
        }
        while (index < this.ElementCount);
      }
      return variables;
    }
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected override bool LoadElement(TNGDefinitions definitions, XmlNode node)
  {
    if (base.LoadElement(definitions, node))
      return true;
    if (node.Name == "variable")
    {
      Variable variable = (Variable) this.Find(node.Attributes["name"].InnerText);
      if (variable == null)
      {
        Variable type = new Variable();
        type.Load(definitions, node);
        this.Add((Element) type);
      }
      else
        variable.Load(definitions, node);
      return true;
    }
    if (!(node.Name == "varref"))
      return false;
    Variable variable1 = (Variable) definitions.Find(node.Attributes["name"].InnerText);
    if (variable1 != null)
      this.Add(variable1.Duplicate());
    return true;
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EVariableBlock();
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
