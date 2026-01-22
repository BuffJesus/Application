// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.DefTypeTemplate
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.ContentManagement;

public class DefTypeTemplate : BaseTemplate
{
  private string m_Type;
  private int m_OriginalId;

  private void \u007EDefTypeTemplate()
  {
  }

  public int OriginalID
  {
    get => this.m_OriginalId;
    set => this.m_OriginalId = value;
  }

  public string Type
  {
    get => this.m_Type;
    set => this.m_Type = value;
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
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
