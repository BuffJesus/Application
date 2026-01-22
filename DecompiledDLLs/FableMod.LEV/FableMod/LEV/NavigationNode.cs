// Decompiled with JetBrains decompiler
// Type: FableMod.LEV.NavigationNode
// Assembly: FableMod.LEV, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 267777C9-1C4D-4C46-87D1-7AC25AEC038B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.LEV.dll

using FableMod.CLRCore;
using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace FableMod.LEV;

public class NavigationNode : NavNode
{
  protected byte m_Special;
  protected uint m_Level;

  public NavigationNode()
  {
    // ISSUE: fault handler
    try
    {
      this.m_Type = NavNodeType.Navigation;
      this.m_Special = (byte) 0;
      this.m_Level = 1U;
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
    uint num1 = 0;
    fileIn.Read((void*) &num1, 4);
    byte num2 = 0;
    fileIn.Read((void*) &num2, 1);
    this.m_Level = num1;
    this.m_Special = num2;
    Debug.Assert(num2 == (byte) 0 || num2 == (byte) 64 /*0x40*/ || num2 == (byte) 128 /*0x80*/);
    this.m_Level = 0U;
    this.m_Special = (byte) 0;
    uint num3 = 0;
    fileIn.Read((void*) &num3, 4);
    uint num4 = 0;
    if (0U < num3)
    {
      do
      {
        uint id = 0;
        fileIn.Read((void*) &id, 4);
        this.AddAdjacent(id);
        ++num4;
      }
      while (num4 < num3);
    }
    return true;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public override unsafe bool Save(FileStream fileOut)
  {
    base.Save(fileOut);
    uint level = this.m_Level;
    int num1 = (int) FileControl.Write(fileOut, (void*) &level, 4U);
    byte num2 = 0;
    if (this.m_Special > (byte) 0)
      num2 = *(byte*) ((IntPtr) this.m_Header + 1L) > (byte) 0 ? (byte) 64 /*0x40*/ : (byte) 128 /*0x80*/;
    int num3 = (int) FileControl.Write(fileOut, (void*) &num2, 1U);
    uint adjacentCount = (uint) this.AdjacentCount;
    int num4 = (int) FileControl.Write(fileOut, (void*) &adjacentCount, 4U);
    this.SaveAdjacents(fileOut);
    return true;
  }

  public override void Print(StringBuilder sb) => base.Print(sb);

  public byte Special
  {
    get => this.m_Special;
    set => this.m_Special = value;
  }

  public uint Level
  {
    get => this.m_Level;
    set => this.m_Level = value;
  }
}
