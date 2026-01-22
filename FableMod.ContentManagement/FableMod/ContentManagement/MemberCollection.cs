// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.MemberCollection
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.ContentManagement;

public class MemberCollection : IDisposable
{
  private Collection<BaseMember> m_Members = new Collection<BaseMember>();

  public MemberCollection(MemberCollection source)
  {
    int index = 0;
    if (0 >= source.m_Members.Count)
      return;
    do
    {
      Debug.Assert(index < source.m_Members.Count);
      if (source.m_Members[index].GetType() == typeof (Member))
      {
        Debug.Assert(index < source.m_Members.Count);
        this.m_Members.Add((BaseMember) new Member((Member) source.m_Members[index]));
      }
      else
      {
        Debug.Assert(index < source.m_Members.Count);
        if (source.m_Members[index].GetType() == typeof (ArrayMember))
        {
          Debug.Assert(index < source.m_Members.Count);
          this.m_Members.Add((BaseMember) new ArrayMember((ArrayMember) source.m_Members[index], this));
        }
      }
      ++index;
    }
    while (index < source.m_Members.Count);
  }

  public MemberCollection()
  {
  }

  private void \u007EMemberCollection()
  {
    this.m_Members.Clear();
    this.m_Members = (Collection<BaseMember>) null;
  }

  public void Add(BaseMember member) => this.m_Members.Add(member);

  public unsafe int ReadIn(sbyte* data, int length)
  {
    int num = 0;
    int index = 0;
    if (0 < this.m_Members.Count)
    {
      do
      {
        num += this.m_Members[index].ReadIn((sbyte*) ((long) num + (IntPtr) data), length - num);
        ++index;
      }
      while (index < this.m_Members.Count);
    }
    return num;
  }

  public unsafe int Write(sbyte* data, int length)
  {
    int num = 0;
    int index1 = 0;
    if (0 < this.m_Members.Count)
    {
      do
      {
        if (this.m_Members[index1].GetType() == typeof (ArrayMember))
          ((ArrayMember) this.m_Members[index1]).UpdateCount();
        ++index1;
      }
      while (index1 < this.m_Members.Count);
    }
    int index2 = 0;
    if (0 < this.m_Members.Count)
    {
      do
      {
        num += this.m_Members[index2].Write((sbyte*) ((long) num + (IntPtr) data), length - num);
        ++index2;
      }
      while (index2 < this.m_Members.Count);
    }
    return num;
  }

  public int Count => this.m_Members.Count;

  public BaseMember this[string name]
  {
    get
    {
      int index = 0;
      if (0 < this.m_Members.Count)
      {
        while (!(this.m_Members[index].Name == name))
        {
          ++index;
          if (index >= this.m_Members.Count)
            goto label_4;
        }
        return this.m_Members[index];
      }
label_4:
      return (BaseMember) null;
    }
  }

  public BaseMember this[int index]
  {
    get
    {
      Debug.Assert(index < this.m_Members.Count);
      return this.m_Members[index];
    }
  }

  public Member GetMemberByName(string name)
  {
    int index = 0;
    if (0 < this.m_Members.Count)
    {
      while (!(this.m_Members[index].GetType() == typeof (Member)) || !(this.m_Members[index].Name == name))
      {
        ++index;
        if (index >= this.m_Members.Count)
          goto label_4;
      }
      return (Member) this.m_Members[index];
    }
label_4:
    return (Member) null;
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EMemberCollection();
    }
    else
    {
      // ISSUE: explicit finalizer call
      this.Finalize();
    }
  }

  public virtual void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }
}
