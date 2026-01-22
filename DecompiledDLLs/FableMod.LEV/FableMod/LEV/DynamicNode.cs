// Decompiled with JetBrains decompiler
// Type: FableMod.LEV.DynamicNode
// Assembly: FableMod.LEV, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 267777C9-1C4D-4C46-87D1-7AC25AEC038B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.LEV.dll

using FableMod.CLRCore;
using System;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace FableMod.LEV;

public class DynamicNode : NavigationNode
{
  protected UIDList m_UIDList = new UIDList();

  public DynamicNode()
  {
    // ISSUE: fault handler
    try
    {
      this.m_Type = NavNodeType.Dynamic;
    }
    __fault
    {
      base.Dispose(true);
    }
  }

  private void \u007EDynamicNode()
  {
    if (!(this.m_UIDList is IDisposable uidList))
      return;
    uidList.Dispose();
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public override bool Load(BufferStream fileIn)
  {
    base.Load(fileIn);
    this.m_UIDList.Load(fileIn);
    return true;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public override bool Save(FileStream fileOut)
  {
    base.Save(fileOut);
    this.m_UIDList.Save(fileOut);
    return true;
  }

  public override void Print(StringBuilder sb)
  {
    base.Print(sb);
    UIDList uidList = this.m_UIDList;
    sb.AppendFormat("UIDCount: {0}; ", (object) uidList.Count);
  }

  public UIDList UIDList => this.m_UIDList;

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EDynamicNode();
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
