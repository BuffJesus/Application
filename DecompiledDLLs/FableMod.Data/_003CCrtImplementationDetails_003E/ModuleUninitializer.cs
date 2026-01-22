// Decompiled with JetBrains decompiler
// Type: <CrtImplementationDetails>.ModuleUninitializer
// Assembly: FableMod.Data, Version=1.0.4918.427, Culture=neutral, PublicKeyToken=null
// MVID: 4E10122A-6FC9-49D4-9087-7FA415462296
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Data.dll

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

  [SecurityCritical]
  [PrePrepareMethod]
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
