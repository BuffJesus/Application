// Decompiled with JetBrains decompiler
// Type: <CrtImplementationDetails>.ModuleLoadException
// Assembly: FableMod.BIG, Version=1.0.4918.425, Culture=neutral, PublicKeyToken=null
// MVID: 88942552-073F-4D63-ADC6-04A8B51D93E5
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.BIG.dll

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
