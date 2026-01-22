// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.Link
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

#nullable disable
namespace FableMod.ContentManagement;

public class Link
{
  private LinkDestination m_To;
  private string m_Restriction;

  public Link(LinkDestination to, string restriction)
  {
    this.m_To = to;
    this.m_Restriction = restriction;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public Link(LinkDestination to)
  {
    this.m_To = to;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public LinkDestination To => this.m_To;

  public string Restriction => this.m_Restriction;
}
