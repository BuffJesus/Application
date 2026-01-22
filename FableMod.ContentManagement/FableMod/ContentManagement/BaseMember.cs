// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.BaseMember
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace FableMod.ContentManagement;

public abstract class BaseMember : IDisposable
{
  protected string m_Name;
  protected string m_Comments;

  public BaseMember(BaseMember member)
  {
    this.m_Name = member.m_Name;
    this.m_Comments = member.m_Comments;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public BaseMember(string name, string comments)
  {
    this.m_Name = name;
    this.m_Comments = comments;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public BaseMember()
  {
  }

  public abstract void FixLinks(LinkDestination link, object oldValue, object newValue);

  public abstract void Print(StringBuilder sb, string tab);

  public unsafe int ReadIn(byte[] data, int offset)
  {
    fixed (byte* data1 = &data[offset])
      return this.ReadIn((sbyte*) data1, data.Length - offset);
  }

  public abstract unsafe int ReadIn(sbyte* data, int length);

  public abstract unsafe int Write(sbyte* data, int length);

  private void \u007EBaseMember()
  {
    this.m_Name = (string) null;
    this.m_Comments = (string) null;
  }

  public string Name => this.m_Name;

  public string Comments => this.m_Comments;

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.m_Name = (string) null;
      this.m_Comments = (string) null;
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
