// Decompiled with JetBrains decompiler
// Type: FableMod.TNG.TNGDefinitions
// Assembly: FableMod.TNG, Version=1.0.4918.434, Culture=neutral, PublicKeyToken=null
// MVID: D9131661-A628-42D1-B5F7-4150ACB2CB8F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.TNG.dll

using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace FableMod.TNG;

public class TNGDefinitions : TNGDefinitionType
{
  private void \u007ETNGDefinitions()
  {
  }

  public void Load(string fileName)
  {
    this.Clear();
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(fileName);
    XmlNode node = xmlDocument["tngdefinitions"].FirstChild;
    if (node == null)
      return;
    do
    {
      TNGDefinitions definitions = this;
      definitions.LoadElement(definitions, node);
      node = node.NextSibling;
    }
    while (node != null);
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected override bool LoadElement(TNGDefinitions definitions, XmlNode node)
  {
    if (base.LoadElement(definitions, node))
      return true;
    if (!(node.Name == "deftype"))
      return false;
    TNGDefinitionType type = new TNGDefinitionType();
    type.Load(definitions, node);
    this.Add((Element) type);
    return true;
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007ETNGDefinitions();
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
