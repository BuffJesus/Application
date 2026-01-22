// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.ElementSort
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using System.Collections;
using System.Diagnostics;

#nullable disable
namespace FableMod.ContentManagement;

internal class ElementSort(int element) : IComparer
{
  private int m_Element = element;

  public virtual int Compare(object x, object y)
  {
    MemberCollection memberCollection1 = (MemberCollection) x;
    MemberCollection memberCollection2 = (MemberCollection) y;
    int element = this.m_Element;
    BaseMember baseMember1 = memberCollection1[element];
    BaseMember baseMember2 = memberCollection2[this.m_Element];
    if (baseMember1.GetType() == typeof (Member))
    {
      Member member1 = (Member) baseMember1;
      Member member2 = (Member) baseMember2;
      if (member1.Type == MemberType.INT)
      {
        int num1 = (int) member1.Value;
        int num2 = (int) member2.Value;
        if (num1 < num2)
          return -1;
        if (num1 == num2)
          return 0;
        if (num1 > num2)
          return 1;
      }
      else if (member1.Type == MemberType.UINT)
      {
        uint num3 = (uint) member1.Value;
        uint num4 = (uint) member2.Value;
        if (num3 < num4)
          return -1;
        if ((int) num3 == (int) num4)
          return 0;
        if (num3 > num4)
          return 1;
      }
      else
        Debug.Assert(false);
    }
    return -1;
  }
}
