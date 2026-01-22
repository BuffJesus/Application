// Decompiled with JetBrains decompiler
// Type: <CrtImplementationDetails>.Exception
// Assembly: FableMod.Data, Version=1.0.4918.427, Culture=neutral, PublicKeyToken=null
// MVID: 4E10122A-6FC9-49D4-9087-7FA415462296
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Data.dll

using System;
using System.Runtime.Serialization;

#nullable disable
namespace \u003CCrtImplementationDetails\u003E;

[Serializable]
internal class Exception : System.Exception
{
  protected Exception(SerializationInfo info, StreamingContext context)
    : base(info, context)
  {
  }

  public Exception(string message, System.Exception innerException)
    : base(message, innerException)
  {
  }

  public Exception(string message)
    : base(message)
  {
  }
}
