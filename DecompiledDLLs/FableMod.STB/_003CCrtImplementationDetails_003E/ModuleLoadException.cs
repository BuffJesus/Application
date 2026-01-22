// Decompiled with JetBrains decompiler
// Type: <CrtImplementationDetails>.ModuleLoadException
// Assembly: FableMod.STB, Version=1.0.4918.435, Culture=neutral, PublicKeyToken=null
// MVID: 2266CC66-C206-4DE0-80D4-5C3E14C1F606
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.STB.dll

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
