// Decompiled with JetBrains decompiler
// Type: FableMod.LEV.NavNode
// Assembly: FableMod.LEV, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 267777C9-1C4D-4C46-87D1-7AC25AEC038B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.LEV.dll

using \u003CCppImplementationDetails\u003E;
using FableMod.CLRCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace FableMod.LEV;

public class NavNode : IDisposable
{
  protected NavNodeType m_Type;
  protected unsafe LEVNodeHeader* m_Header;
  protected List<uint> m_Adjacents;

  public unsafe NavNode()
  {
    LEVNodeHeader* levNodeHeaderPtr1 = (LEVNodeHeader*) \u003CModule\u003E.@new(14UL);
    LEVNodeHeader* levNodeHeaderPtr2;
    if ((IntPtr) levNodeHeaderPtr1 != IntPtr.Zero)
    {
      // ISSUE: initblk instruction
      __memset((IntPtr) levNodeHeaderPtr1, 0, 14);
      levNodeHeaderPtr2 = levNodeHeaderPtr1;
    }
    else
      levNodeHeaderPtr2 = (LEVNodeHeader*) 0L;
    this.m_Header = levNodeHeaderPtr2;
    this.m_Adjacents = new List<uint>();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  private unsafe void \u007ENavNode() => \u003CModule\u003E.delete((void*) this.m_Header);

  [return: MarshalAs(UnmanagedType.U1)]
  public virtual unsafe bool Load(BufferStream fileIn)
  {
    fileIn.Read((void*) this.m_Header, 14);
    Debug.Assert(*(byte*) this.m_Header < (byte) 7);
    return true;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public virtual unsafe bool Save(FileStream fileOut)
  {
    NavNodeType type = this.m_Type;
    if (type == NavNodeType.Blank)
    {
      \u0024ArrayType\u0024\u0024\u0024BY02E arrayTypeBy02E;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ref arrayTypeBy02E = (sbyte) 0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy02E + 1) = (sbyte) 1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy02E + 2) = (sbyte) 1;
      int num = (int) FileControl.Write(fileOut, (void*) &arrayTypeBy02E, 3U);
      return true;
    }
    \u0024ArrayType\u0024\u0024\u0024BY03E arrayTypeBy03E;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(sbyte&) ((IntPtr) &arrayTypeBy03E + 2) = (sbyte) 0;
    if (type == NavNodeType.Standard)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ref arrayTypeBy03E = (sbyte) 0;
      int num = *(byte*) this.m_Header == (byte) 0 ? 1 : 0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy03E + 1) = (sbyte) (byte) num;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy03E + 3) = (sbyte) 0;
    }
    else if (type == NavNodeType.Navigation)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ref arrayTypeBy03E = (sbyte) 0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy03E + 1) = (sbyte) 0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy03E + 3) = (sbyte) 1;
    }
    else if (type == NavNodeType.Dynamic)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ref arrayTypeBy03E = (sbyte) 1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy03E + 1) = (sbyte) 0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(sbyte&) ((IntPtr) &arrayTypeBy03E + 3) = (sbyte) 1;
    }
    int num1 = (int) FileControl.Write(fileOut, (void*) &arrayTypeBy03E, 4U);
    int num2 = (int) FileControl.Write(fileOut, (void*) this.m_Header, 14U);
    return true;
  }

  public virtual unsafe void Print(StringBuilder sb)
  {
    NavNodeType type = this.m_Type;
    sb.AppendFormat("Type: {0}; ", (object) type.ToString());
    if (this.m_Type == NavNodeType.Blank)
      return;
    int count = this.m_Adjacents.Count;
    LEVNodeHeader* header = this.m_Header;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    float num = ^(float&) ((long) *(byte*) header * 4L + ref \u003CModule\u003E.FableMod\u002ELEV\u002E\u003FA0xe83841bc\u002ESIZES);
    sb.AppendFormat("ID: {0}; Size: {1}; Adj: {2}; ", (object) (uint) *(int*) ((IntPtr) header + 10L), (object) num, (object) count);
  }

  public void Print(TextWriter writer)
  {
    StringBuilder sb = new StringBuilder();
    this.Print(sb);
    writer.Write(sb.ToString());
  }

  public unsafe void SaveAdjacents(FileStream fileOut)
  {
    uint index = 0;
    if (0U >= (uint) this.m_Adjacents.Count)
      return;
    do
    {
      uint adjacent = this.m_Adjacents[(int) index];
      int num = (int) FileControl.Write(fileOut, (void*) &adjacent, 4U);
      ++index;
    }
    while (index < (uint) this.m_Adjacents.Count);
  }

  public void AddAdjacent(uint id) => this.m_Adjacents.Add(id);

  [return: MarshalAs(UnmanagedType.U1)]
  public bool HasAdjacent(uint id) => this.m_Adjacents.IndexOf(id) >= 0;

  public void RemoveAdjacents() => this.m_Adjacents.Clear();

  public int AdjacentCount => this.m_Adjacents.Count;

  public uint get_Adjacents(int index) => this.m_Adjacents[index];

  public unsafe uint ID
  {
    get => (uint) *(int*) ((IntPtr) this.m_Header + 10L);
    set => *(int*) ((IntPtr) this.m_Header + 10L) = (int) value;
  }

  public unsafe byte Layer
  {
    get => *(byte*) this.m_Header;
    set => *(sbyte*) this.m_Header = (sbyte) value;
  }

  public unsafe float Size
  {
    get
    {
      return ^(float&) ((long) *(byte*) this.m_Header * 4L + ref \u003CModule\u003E.FableMod\u002ELEV\u002E\u003FA0xe83841bc\u002ESIZES);
    }
  }

  public unsafe float X
  {
    get => *(float*) ((IntPtr) this.m_Header + 2L);
    set => *(float*) ((IntPtr) this.m_Header + 2L) = value;
  }

  public unsafe float Y
  {
    get => *(float*) ((IntPtr) this.m_Header + 6L);
    set => *(float*) ((IntPtr) this.m_Header + 6L) = value;
  }

  public unsafe uint Subset
  {
    get => (uint) *(byte*) ((IntPtr) this.m_Header + 1L);
    set => *(sbyte*) ((IntPtr) this.m_Header + 1L) = (sbyte) value;
  }

  public NavNodeType Type
  {
    get => this.m_Type;
    set => this.m_Type = value;
  }

  public static unsafe void GenerateLinks(List<NavNode> nodes)
  {
    int count1 = nodes.Count;
    int index1 = 0;
    if (0 >= count1)
      return;
    do
    {
      NavNode node1 = nodes[index1];
      switch (node1.m_Type)
      {
        case NavNodeType.Navigation:
        case NavNodeType.Dynamic:
          LEVNodeHeader* header1 = node1.m_Header;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          float num1 = ^(float&) ((long) *(byte*) header1 * 4L + ref \u003CModule\u003E.FableMod\u002ELEV\u002E\u003FA0xe83841bc\u002ESIZES) * 0.5f;
          float num2 = *(float*) ((IntPtr) header1 + 2L);
          float x3 = num2 + num1;
          float x4 = num2 - num1;
          float num3 = *(float*) ((IntPtr) header1 + 6L);
          float y3 = num3 - num1;
          float y4 = num3 + num1;
          int count2 = nodes.Count;
          int index2 = 0;
          if (0 < count2)
          {
            do
            {
              NavNode node2 = nodes[index2];
              if (node2 != node1)
              {
                switch (node2.m_Type)
                {
                  case NavNodeType.Blank:
                  case NavNodeType.Standard:
                    break;
                  default:
                    uint num4 = (uint) *(int*) ((IntPtr) node2.m_Header + 10L);
                    if ((node1.m_Adjacents.IndexOf(num4) >= 0 ? 1 : 0) == 0 && Math.Abs((int) *(byte*) ((IntPtr) node1.m_Header + 1L) - (int) *(byte*) ((IntPtr) node2.m_Header + 1L)) <= 1)
                    {
                      LEVNodeHeader* header2 = node2.m_Header;
                      // ISSUE: cast to a reference type
                      // ISSUE: explicit reference operation
                      float num5 = ^(float&) ((long) *(byte*) header2 * 4L + ref \u003CModule\u003E.FableMod\u002ELEV\u002E\u003FA0xe83841bc\u002ESIZES) * 0.5f;
                      float num6 = *(float*) ((IntPtr) header2 + 2L);
                      float x1 = num6 + num5;
                      float x2 = num6 - num5;
                      float num7 = *(float*) ((IntPtr) header2 + 6L);
                      float y1 = num7 - num5;
                      float y2 = num7 + num5;
                      bool flag1 = \u003CModule\u003E.FableMod\u002ELEV\u002E\u003FA0xe83841bc\u002EHitTestX(x1, x2, x3, x4);
                      bool flag2 = \u003CModule\u003E.FableMod\u002ELEV\u002E\u003FA0xe83841bc\u002EHitTestY(y1, y2, y3, y4);
                      bool flag3 = (double) x3 == (double) x2 && flag2;
                      bool flag4 = (double) x4 == (double) x1 && flag2;
                      bool flag5 = (double) y3 == (double) y2 && flag1;
                      int num8 = (double) y4 != (double) y1 || !flag1 ? 0 : 1;
                      if ((flag3 || flag4 || flag5 || (byte) num8 != (byte) 0 ? 1 : 0) != 0)
                      {
                        node1.m_Adjacents.Add((uint) *(int*) ((IntPtr) header2 + 10L));
                        node2.m_Adjacents.Add((uint) *(int*) ((IntPtr) node1.m_Header + 10L));
                        break;
                      }
                      break;
                    }
                    break;
                }
              }
              ++index2;
            }
            while (index2 < count2);
            break;
          }
          break;
      }
      ++index1;
    }
    while (index1 < count1);
  }

  public static unsafe void GenerateNodes(
    List<NavNode> nodes,
    byte subset,
    Grid grid,
    uint* puiID)
  {
    uint num1 = (uint) (grid.Width / 64 /*0x40*/);
    uint num2 = (uint) (grid.Height / 64 /*0x40*/);
    int y = 0;
    if (0U >= num2)
      return;
    uint num3 = num2;
    do
    {
      int x = 0;
      if (0U < num1)
      {
        uint num4 = num1;
        do
        {
          if (NavNode.Generate(nodes, subset, (byte) 0, grid, x, y, puiID) == null)
          {
            nodes.Add(new NavNode()
            {
              m_Type = NavNodeType.Blank
            });
            uint* numPtr = puiID;
            int num5 = (int) *numPtr + 1;
            *numPtr = (uint) num5;
          }
          x += 64 /*0x40*/;
          num4 += uint.MaxValue;
        }
        while (num4 > 0U);
      }
      y += 64 /*0x40*/;
      num3 += uint.MaxValue;
    }
    while (num3 > 0U);
  }

  public static unsafe NavNode Generate(
    List<NavNode> nodes,
    byte subset,
    byte layer,
    Grid grid,
    int x,
    int y,
    uint* puiID)
  {
    long num1 = (long) layer * 4L;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    int num2 = ^(int&) (num1 + ref \u003CModule\u003E.FableMod\u002ELEV\u002E\u003FA0xe83841bc\u002ECELL_SIZES);
    uint navigationState = (uint) grid.GetNavigationState(x, y, num2);
    if (navigationState == 0U)
      return (NavNode) null;
    float num3 = (float) ((double) grid.Width * 0.5 - (double) x * 0.5);
    float num4 = (float) y * 0.5f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    float num5 = ^(float&) (num1 + ref \u003CModule\u003E.FableMod\u002ELEV\u002E\u003FA0xe83841bc\u002ESIZES);
    float num6 = (float) (((double) (num3 - num5) + (double) num3) * 0.5);
    float num7 = (float) (((double) (num5 + num4) + (double) num4) * 0.5);
    if (((int) navigationState & 4) != 0 && ((int) navigationState & 16 /*0x10*/) == 0 && ((int) navigationState & 8) == 0)
    {
      DynamicNode dynamicNode = new DynamicNode();
      nodes.Add((NavNode) dynamicNode);
      uint num8 = *puiID;
      uint num9 = num8;
      *puiID = num8 + 1U;
      *(int*) ((IntPtr) dynamicNode.m_Header + 10L) = (int) num9;
      *(sbyte*) dynamicNode.m_Header = (sbyte) layer;
      *(sbyte*) ((IntPtr) dynamicNode.m_Header + 1L) = (sbyte) subset;
      *(float*) ((IntPtr) dynamicNode.m_Header + 2L) = num6;
      *(float*) ((IntPtr) dynamicNode.m_Header + 6L) = num7;
      dynamicNode.Level = (uint) grid.GetCellAt(x, y).Level;
      if (((int) navigationState & 32 /*0x20*/) != 0)
        dynamicNode.Special = (byte) 64 /*0x40*/;
      else if (((int) navigationState & 64 /*0x40*/) != 0)
        dynamicNode.Special = (byte) 128 /*0x80*/;
      grid.AddToUIDList(dynamicNode.UIDList, x, y, num2);
      return (NavNode) dynamicNode;
    }
    if (((int) navigationState & 2) != 0 && ((int) navigationState & 16 /*0x10*/) == 0 && ((int) navigationState & 8) == 0)
    {
      NavigationNode navigationNode = new NavigationNode();
      nodes.Add((NavNode) navigationNode);
      uint num10 = *puiID;
      uint num11 = num10;
      *puiID = num10 + 1U;
      *(int*) ((IntPtr) navigationNode.m_Header + 10L) = (int) num11;
      *(sbyte*) navigationNode.m_Header = (sbyte) layer;
      *(sbyte*) ((IntPtr) navigationNode.m_Header + 1L) = (sbyte) subset;
      *(float*) ((IntPtr) navigationNode.m_Header + 2L) = num6;
      *(float*) ((IntPtr) navigationNode.m_Header + 6L) = num7;
      navigationNode.Level = (uint) grid.GetCellAt(x, y).Level;
      if (((int) navigationState & 32 /*0x20*/) != 0)
        navigationNode.Special = (byte) 64 /*0x40*/;
      else if (((int) navigationState & 64 /*0x40*/) != 0)
        navigationNode.Special = (byte) 128 /*0x80*/;
      return (NavNode) navigationNode;
    }
    StandardNode standardNode = new StandardNode();
    nodes.Add((NavNode) standardNode);
    *(sbyte*) standardNode.m_Header = (sbyte) layer;
    *(sbyte*) ((IntPtr) standardNode.m_Header + 1L) = (sbyte) subset;
    *(float*) ((IntPtr) standardNode.m_Header + 2L) = num6;
    *(float*) ((IntPtr) standardNode.m_Header + 6L) = num7;
    int num12 = num2 / 2;
    byte layer1 = (byte) ((int) layer + 1);
    NavNode navNode1 = NavNode.Generate(nodes, subset, layer1, grid, num12 + x, y, puiID);
    uint id1 = navNode1 == null ? 0U : (uint) *(int*) ((IntPtr) navNode1.m_Header + 10L);
    standardNode.AddAdjacent(id1);
    NavNode navNode2 = NavNode.Generate(nodes, subset, layer1, grid, x, y, puiID);
    uint id2 = navNode2 == null ? 0U : navNode2.ID;
    standardNode.AddAdjacent(id2);
    int y1 = num12 + y;
    NavNode navNode3 = NavNode.Generate(nodes, subset, layer1, grid, num12 + x, y1, puiID);
    uint id3 = navNode3 == null ? 0U : navNode3.ID;
    standardNode.AddAdjacent(id3);
    NavNode navNode4 = NavNode.Generate(nodes, subset, layer1, grid, x, y1, puiID);
    uint id4 = navNode4 == null ? 0U : navNode4.ID;
    standardNode.AddAdjacent(id4);
    uint num13 = *puiID;
    uint num14 = num13;
    *puiID = num13 + 1U;
    standardNode.ID = num14;
    return (NavNode) standardNode;
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007ENavNode();
    }
    else
    {
      // ISSUE: explicit finalizer call
      this.Finalize();
    }
  }

  public virtual void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }
}
