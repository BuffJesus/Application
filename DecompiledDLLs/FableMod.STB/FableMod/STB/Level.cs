// Decompiled with JetBrains decompiler
// Type: FableMod.STB.Level
// Assembly: FableMod.STB, Version=1.0.4918.435, Culture=neutral, PublicKeyToken=null
// MVID: 2266CC66-C206-4DE0-80D4-5C3E14C1F606
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.STB.dll

using System;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.STB;

public class Level : IDisposable
{
  protected Collection<Flora> m_Flora = new Collection<Flora>();

  private void \u007ELevel() => this.m_Flora = (Collection<Flora>) null;

  public int FloraCount => this.m_Flora.Count;

  public Flora get_Flora(int index) => this.m_Flora[index];

  internal void Add(Flora flora) => this.m_Flora.Add(flora);

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.m_Flora = (Collection<Flora>) null;
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
