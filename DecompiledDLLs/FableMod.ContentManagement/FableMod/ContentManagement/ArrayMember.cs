// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.ArrayMember
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace FableMod.ContentManagement;

public class ArrayMember : BaseMember
{
  private new string m_Comments;
  private Member m_CountMember;
  private MemberCollection m_ElementMembers;
  private Collection<MemberCollection> m_Elements;

  public ArrayMember(ArrayMember arr, MemberCollection cont)
  {
    string name = arr.m_CountMember.m_Name;
    this.m_CountMember = cont.GetMemberByName(name);
    this.m_ElementMembers = new MemberCollection(arr.m_ElementMembers);
    this.m_Elements = new Collection<MemberCollection>();
    this.m_Name = arr.m_Name;
    base.m_Comments = ((BaseMember) arr).m_Comments;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public ArrayMember(string name, string comments, Member count)
  {
    this.m_CountMember = count;
    this.m_ElementMembers = new MemberCollection();
    this.m_Elements = new Collection<MemberCollection>();
    this.m_Name = name;
    base.m_Comments = comments;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  private void \u007EArrayMember()
  {
    this.m_ElementMembers = (MemberCollection) null;
    this.m_Elements = (Collection<MemberCollection>) null;
  }

  public void UpdateCount() => this.m_CountMember.Value = (object) this.m_Elements.Count.ToString();

  public override void FixLinks(LinkDestination link, object oldValue, object newValue)
  {
    int index1 = 0;
    if (0 >= this.m_Elements.Count)
      return;
    Collection<MemberCollection> elements1;
    do
    {
      int index2 = 0;
      if (0 < this.m_Elements[index1].Count)
      {
        Collection<MemberCollection> elements2;
        do
        {
          this.m_Elements[index1][index2].FixLinks(link, oldValue, newValue);
          ++index2;
          elements2 = this.m_Elements;
        }
        while (index2 < elements2[index1].Count);
      }
      ++index1;
      elements1 = this.m_Elements;
    }
    while (index1 < elements1.Count);
  }

  public override void Print(StringBuilder sb, string tab)
  {
    object[] objArray = new object[4]
    {
      (object) tab,
      (object) this.m_Name,
      null,
      null
    };
    Collection<MemberCollection> elements1 = this.m_Elements;
    objArray[2] = (object) elements1.Count;
    objArray[3] = (object) Environment.NewLine;
    sb.AppendFormat("{0}Array: {1}[{2}]{3}", objArray);
    string tab1 = tab + "    ";
    int index1 = 0;
    if (0 >= this.m_Elements.Count)
      return;
    Collection<MemberCollection> elements2;
    do
    {
      sb.AppendFormat("{0}  Element {1}{2}", (object) tab, (object) index1, (object) Environment.NewLine);
      int index2 = 0;
      if (0 < this.m_Elements[index1].Count)
      {
        Collection<MemberCollection> elements3;
        do
        {
          sb.AppendFormat("{0}   Index {1}{2}", (object) tab, (object) index2, (object) Environment.NewLine);
          this.m_Elements[index1][index2].Print(sb, tab1);
          ++index2;
          elements3 = this.m_Elements;
        }
        while (index2 < elements3[index1].Count);
      }
      ++index1;
      elements2 = this.m_Elements;
    }
    while (index1 < elements2.Count);
  }

  public override unsafe int ReadIn(sbyte* data, int length)
  {
    int num1 = 0;
    uint num2 = uint.Parse(this.m_CountMember.Value.ToString());
    this.m_Elements.Clear();
    if (0U < num2)
    {
      uint num3 = num2;
      do
      {
        MemberCollection memberCollection = new MemberCollection(this.m_ElementMembers);
        num1 += memberCollection.ReadIn((sbyte*) ((long) num1 + (IntPtr) data), length - num1);
        this.m_Elements.Add(memberCollection);
        num3 += uint.MaxValue;
      }
      while (num3 > 0U);
    }
    return num1;
  }

  public override unsafe int Write(sbyte* data, int length)
  {
    int num = 0;
    int index = 0;
    if (0 < this.m_Elements.Count)
    {
      do
      {
        num += this.m_Elements[index].Write((sbyte*) ((long) num + (IntPtr) data), length - num);
        ++index;
      }
      while (index < this.m_Elements.Count);
    }
    return num;
  }

  public void Sort(int sortElement)
  {
    if (this.m_Elements.Count <= 0)
      return;
    MemberCollection[] memberCollectionArray = new MemberCollection[this.m_Elements.Count];
    int index1 = 0;
    if (0 < this.m_Elements.Count)
    {
      do
      {
        memberCollectionArray[index1] = this.m_Elements[index1];
        ++index1;
      }
      while (index1 < this.m_Elements.Count);
    }
    Array.Sort((Array) memberCollectionArray, (IComparer) new ElementSort(sortElement));
    this.m_Elements.Clear();
    int index2 = 0;
    if (0 >= memberCollectionArray.Length)
      return;
    do
    {
      this.m_Elements.Add(memberCollectionArray[index2]);
      ++index2;
    }
    while (index2 < memberCollectionArray.Length);
  }

  public string ElementCount => this.m_CountMember.m_Name;

  public MemberCollection ElementMembers => this.m_ElementMembers;

  public Collection<MemberCollection> Elements => this.m_Elements;

  public MemberCollection CreateElement() => new MemberCollection(this.m_ElementMembers);

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder();
    Collection<MemberCollection> elements1 = this.m_Elements;
    string name = this.m_Name;
    stringBuilder.AppendFormat("Array: {0}[{1}]{2}", (object) name, (object) elements1.Count, (object) Environment.NewLine);
    int index1 = 0;
    if (0 < this.m_Elements.Count)
    {
      Collection<MemberCollection> elements2;
      do
      {
        stringBuilder.AppendFormat("    Element {0}{1}", (object) index1, (object) Environment.NewLine);
        int index2 = 0;
        if (0 < this.m_Elements[index1].Count)
        {
          Collection<MemberCollection> elements3;
          do
          {
            Collection<MemberCollection> elements4 = this.m_Elements;
            stringBuilder.AppendFormat("        {0}{1}", (object) elements4[index1][index2].ToString(), (object) Environment.NewLine);
            ++index2;
            elements3 = this.m_Elements;
          }
          while (index2 < elements3[index1].Count);
        }
        ++index1;
        elements2 = this.m_Elements;
      }
      while (index1 < elements2.Count);
    }
    return stringBuilder.ToString();
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.m_ElementMembers = (MemberCollection) null;
        this.m_Elements = (Collection<MemberCollection>) null;
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
