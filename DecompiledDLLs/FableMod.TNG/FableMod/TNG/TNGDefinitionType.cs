// Decompiled with JetBrains decompiler
// Type: FableMod.TNG.TNGDefinitionType
// Assembly: FableMod.TNG, Version=1.0.4918.434, Culture=neutral, PublicKeyToken=null
// MVID: D9131661-A628-42D1-B5F7-4150ACB2CB8F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.TNG.dll

using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace FableMod.TNG;

public class TNGDefinitionType : ComplexBlock
{
  protected string m_Graphic;

  private void \u007ETNGDefinitionType()
  {
  }

  public override void CopyTo(Element element)
  {
    base.CopyTo(element);
    ((TNGDefinitionType) element).m_Graphic = this.m_Graphic;
  }

  public override void Load(TNGDefinitions definitions, XmlNode node)
  {
    XmlAttribute attribute = node.Attributes["graphic"];
    base.Load(definitions, node);
    if (attribute == null)
      return;
    this.m_Graphic = attribute.InnerText;
  }

  public string GraphicOverride => this.m_Graphic;

  protected override Element Factory() => (Element) new TNGDefinitionType();

  [return: MarshalAs(UnmanagedType.U1)]
  protected override bool LoadElement(TNGDefinitions definitions, XmlNode node)
  {
    if (base.LoadElement(definitions, node))
      return true;
    if (node.Name == "ctc")
    {
      CTCBlock ctcBlock = (CTCBlock) this.Find(node.Attributes["name"].InnerText);
      if (ctcBlock == null)
      {
        CTCBlock type = new CTCBlock();
        type.Load(definitions, node);
        this.Add((Element) type);
      }
      else
        ctcBlock.Load(definitions, node);
      return true;
    }
    if (!(node.Name == "ctcref"))
      return false;
    CTCBlock ctcBlock1 = (CTCBlock) definitions.Find(node.Attributes["name"].InnerText);
    if (ctcBlock1 != null)
      this.Add(ctcBlock1.Duplicate());
    return true;
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007ETNGDefinitionType();
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
