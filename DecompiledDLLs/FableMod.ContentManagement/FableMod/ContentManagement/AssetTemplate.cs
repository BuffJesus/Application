// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.AssetTemplate
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.ContentManagement;

public class AssetTemplate : BaseTemplate
{
  private uint m_Control;
  private int m_Element;
  private ContentType m_Type;

  private void \u007EAssetTemplate()
  {
  }

  public uint ControlID
  {
    get => this.m_Control;
    set => this.m_Control = value;
  }

  public int Element
  {
    get => this.m_Element;
    set => this.m_Element = value;
  }

  public ContentType Type
  {
    get => this.m_Type;
    set => this.m_Type = value;
  }

  public string Prefix
  {
    get
    {
      switch (this.m_Type)
      {
        case ContentType.Graphics:
          return "MESH";
        case ContentType.Text:
          return "TEXT";
        case ContentType.MainTextures:
          return "TEXTURE";
        default:
          return "PREFIX";
      }
    }
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
