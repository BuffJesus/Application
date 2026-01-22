// Decompiled with JetBrains decompiler
// Type: <CrtImplementationDetails>.ModuleLoadException
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace \u003CCrtImplementationDetails\u003E;

[Serializable]
internal class ModuleLoadException : System.Exception
{
  public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";

  protected ModuleLoadException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public ModuleLoadException(string message, System.Exception innerException)
    : base(message, innerException)
  {
  }

  public ModuleLoadException(string message)
    : base(message)
  {
  }
}
