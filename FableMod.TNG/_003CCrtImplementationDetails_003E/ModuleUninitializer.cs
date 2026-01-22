// Decompiled with JetBrains decompiler
// Type: <CrtImplementationDetails>.ModuleUninitializer
// Assembly: FableMod.TNG, Version=1.0.4918.434, Culture=neutral, PublicKeyToken=null
// MVID: D9131661-A628-42D1-B5F7-4150ACB2CB8F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.TNG.dll

using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Threading;

#nullable disable
namespace \u003CCrtImplementationDetails\u003E;

internal class ModuleUninitializer : Stack
{
  private static object @lock = new object();
  internal static ModuleUninitializer _ModuleUninitializer = new ModuleUninitializer();

  [SecuritySafeCritical]
  internal void AddHandler(EventHandler handler)
  {
    bool lockTaken = false;
    RuntimeHelpers.PrepareConstrainedRegions();
    try
    {
      RuntimeHelpers.PrepareConstrainedRegions();
      Monitor.Enter(ModuleUninitializer.@lock, ref lockTaken);
      RuntimeHelpers.PrepareDelegate((Delegate) handler);
      this.Push((object) handler);
    }
    finally
    {
      if (lockTaken)
        Monitor.Exit(ModuleUninitializer.@lock);
    }
  }

  [SecurityCritical]
  static ModuleUninitializer()
  {
  }

  [SecuritySafeCritical]
  private ModuleUninitializer()
  {
    EventHandler eventHandler = new EventHandler(this.SingletonDomainUnload);
    AppDomain.CurrentDomain.DomainUnload += eventHandler;
    AppDomain.CurrentDomain.ProcessExit += eventHandler;
  }

  [PrePrepareMethod]
  [SecurityCritical]
  private void SingletonDomainUnload(object source, EventArgs arguments)
  {
    bool lockTaken = false;
    RuntimeHelpers.PrepareConstrainedRegions();
    try
    {
      RuntimeHelpers.PrepareConstrainedRegions();
      Monitor.Enter(ModuleUninitializer.@lock, ref lockTaken);
      foreach (EventHandler eventHandler in (Stack) this)
        eventHandler(source, arguments);
    }
    finally
    {
      if (lockTaken)
        Monitor.Exit(ModuleUninitializer.@lock);
    }
  }
}
