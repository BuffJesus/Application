// Decompiled with JetBrains decompiler
// Type: <CrtImplementationDetails>.ModuleLoadException
// Assembly: FableMod.LEV, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 267777C9-1C4D-4C46-87D1-7AC25AEC038B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.LEV.dll

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
