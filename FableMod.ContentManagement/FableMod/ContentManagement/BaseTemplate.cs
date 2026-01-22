// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.BaseTemplate
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.ContentManagement;

public class BaseTemplate : IDisposable
{
  private DefTypeTemplate m_Link;
  private string m_Name;
  private bool m_Named;
  private int m_Id;
  private bool m_Ref;

  private void \u007EBaseTemplate()
  {
  }

  public string Name
  {
    get => this.m_Name;
    set => this.m_Name = value;
  }

  public DefTypeTemplate LinkTo
  {
    get => this.m_Link;
    set => this.m_Link = value;
  }

  public bool Named
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_Named;
    [param: MarshalAs(UnmanagedType.U1)] set => this.m_Named = value;
  }

  public bool Ref
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_Ref;
    [param: MarshalAs(UnmanagedType.U1)] set => this.m_Ref = value;
  }

  public int ID
  {
    get => this.m_Id;
    set => this.m_Id = value;
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
      return;
    // ISSUE: explicit finalizer call
    this.Finalize();
  }

  public virtual void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }
}
