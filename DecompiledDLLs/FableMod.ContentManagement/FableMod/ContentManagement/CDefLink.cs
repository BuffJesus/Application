// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.CDefLink
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

#nullable disable
namespace FableMod.ContentManagement;

public class CDefLink
{
  private Member m_Data;
  private Member m_Inherited;

  public object DataID
  {
    get => this.m_Data.Value;
    set => this.m_Data.Value = value;
  }

  public object InheritedID
  {
    get => this.m_Inherited.Value;
    set => this.m_Inherited.Value = value;
  }

  internal CDefLink(Member data, Member inherited)
  {
    this.m_Data = data;
    this.m_Inherited = inherited;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }
}
