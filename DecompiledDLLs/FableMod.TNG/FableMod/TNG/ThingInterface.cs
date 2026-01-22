// Decompiled with JetBrains decompiler
// Type: FableMod.TNG.ThingInterface
// Assembly: FableMod.TNG, Version=1.0.4918.434, Culture=neutral, PublicKeyToken=null
// MVID: D9131661-A628-42D1-B5F7-4150ACB2CB8F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.TNG.dll

using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.TNG;

public abstract class ThingInterface : IDisposable
{
  protected Thing m_Thing;

  public ThingInterface(Thing thing)
  {
    this.m_Thing = thing;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  private void \u007EThingInterface()
  {
  }

  private void \u0021ThingInterface() => this.Destroy();

  public virtual void Destroy()
  {
  }

  public abstract void Update();

  public abstract void UpdateThing();

  public Thing Thing => this.m_Thing;

  [HandleProcessCorruptedStateExceptions]
  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
      return;
    try
    {
      this.Destroy();
    }
    finally
    {
      // ISSUE: explicit finalizer call
      base.Finalize();
    }
  }

  public virtual void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }

  ~ThingInterface() => this.Dispose(false);
}
