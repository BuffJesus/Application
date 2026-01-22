// Decompiled with JetBrains decompiler
// Type: FableMod.LEV.GridCell
// Assembly: FableMod.LEV, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 267777C9-1C4D-4C46-87D1-7AC25AEC038B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.LEV.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.LEV;

public class GridCell : IDisposable
{
  protected UIDList m_UIDList = new UIDList();
  protected byte m_Value = 0;
  protected byte m_Level = 0;

  private void \u007EGridCell()
  {
    if (!(this.m_UIDList is IDisposable uidList))
      return;
    uidList.Dispose();
  }

  public byte Value
  {
    get => this.m_Value;
    set => this.m_Value = value;
  }

  public byte Level
  {
    get => this.m_Level;
    set => this.m_Level = value;
  }

  public UIDList UIDList => this.m_UIDList;

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EGridCell();
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
