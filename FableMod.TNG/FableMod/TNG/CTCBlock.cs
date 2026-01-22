// Decompiled with JetBrains decompiler
// Type: FableMod.TNG.CTCBlock
// Assembly: FableMod.TNG, Version=1.0.4918.434, Culture=neutral, PublicKeyToken=null
// MVID: D9131661-A628-42D1-B5F7-4150ACB2CB8F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.TNG.dll

using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.TNG;

public class CTCBlock : ComplexBlock
{
  private void \u007ECTCBlock()
  {
  }

  public override void Save(TextWriter writer)
  {
    if (this.IsDefault() && !this.SaveDefault)
      return;
    writer.WriteLine("Start{0};", (object) this.m_Name);
    int index = 0;
    if (0 < this.ElementCount)
    {
      do
      {
        this.get_Elements(index).Save(writer);
        ++index;
      }
      while (index < this.ElementCount);
    }
    writer.WriteLine("End{0};", (object) this.m_Name);
  }

  protected override Element Factory() => (Element) new CTCBlock();

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007ECTCBlock();
      }
      finally
      {
        base.Dispose(true);
      }
    }
    else
      base.Dispose(false);
  }
}
