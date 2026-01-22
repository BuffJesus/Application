// Decompiled with JetBrains decompiler
// Type: <CrtImplementationDetails>.ModuleLoadException
// Assembly: FableMod.CLRCore, Version=1.0.4918.423, Culture=neutral, PublicKeyToken=null
// MVID: 9BFDF4CA-2166-4C71-B7DE-FD9072E9B599
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.CLRCore.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace \u003CCrtImplementationDetails\u003E;

[Serializable]
internal class ModuleLoadException : Exception
{
  public const string Nested = "A nested exception occurred after the primary exception that caused the C++ module to fail to load.\n";

  protected ModuleLoadException(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public ModuleLoadException(string message, Exception innerException)
    : base(message, innerException)
  {
  }

  public ModuleLoadException(string message)
    : base(message)
  {
  }
}
