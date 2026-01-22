// Decompiled with JetBrains decompiler
// Type: FableMod.LEV.StandardNode
// Assembly: FableMod.LEV, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 267777C9-1C4D-4C46-87D1-7AC25AEC038B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.LEV.dll

using \u003CCppImplementationDetails\u003E;
using FableMod.CLRCore;
using System;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.LEV;

public class StandardNode : NavNode
{
  public StandardNode()
  {
    // ISSUE: fault handler
    try
    {
      this.m_Type = NavNodeType.Standard;
    }
    __fault
    {
      this.Dispose(true);
    }
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public override unsafe bool Load(BufferStream fileIn)
  {
    base.Load(fileIn);
    \u0024ArrayType\u0024\u0024\u0024BY03I arrayTypeBy03I;
    fileIn.Read((void*) &arrayTypeBy03I, 16 /*0x10*/);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    this.AddAdjacent((uint) ^(int&) ref arrayTypeBy03I);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    this.AddAdjacent((uint) ^(int&) ((IntPtr) &arrayTypeBy03I + 4));
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    this.AddAdjacent((uint) ^(int&) ((IntPtr) &arrayTypeBy03I + 8));
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    this.AddAdjacent((uint) ^(int&) ((IntPtr) &arrayTypeBy03I + 12));
    return true;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public override bool Save(FileStream fileOut)
  {
    base.Save(fileOut);
    this.SaveAdjacents(fileOut);
    return true;
  }
}
