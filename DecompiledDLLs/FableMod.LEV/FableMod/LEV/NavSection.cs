// Decompiled with JetBrains decompiler
// Type: FableMod.LEV.NavSection
// Assembly: FableMod.LEV, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 267777C9-1C4D-4C46-87D1-7AC25AEC038B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.LEV.dll

using FableMod.CLRCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.LEV;

public class NavSection : IDisposable
{
  protected List<Grid> m_Grids = new List<Grid>();
  protected List<NavNode> m_Nodes = new List<NavNode>();
  protected float m_Width;
  protected float m_Height;
  protected string m_Name;

  private void \u007ENavSection()
  {
    this.DisposeGrids();
    this.DisposeNodes();
    if (this.m_Nodes is IDisposable nodes)
      nodes.Dispose();
    if (!(this.m_Grids is IDisposable grids))
      return;
    grids.Dispose();
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public unsafe bool Load(BufferStream fileIn)
  {
    LEVNavSectionHeader navSectionHeader;
    fileIn.Read((void*) &navSectionHeader, 20);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    this.m_Width = ^(float&) ((IntPtr) &navSectionHeader + 8);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    this.m_Height = ^(float&) ((IntPtr) &navSectionHeader + 12);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    Log.WriteLine("Section FileSize: {0}", (object) (uint) ^(int&) ref navSectionHeader);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    Log.WriteLine("Section Version: {0}", (object) (uint) ^(int&) ((IntPtr) &navSectionHeader + 4));
    Log.WriteLine("Section Size: {0}x{1}", (object) this.m_Width, (object) this.m_Height);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    Log.WriteLine("Section NumLevels: {0}", (object) (uint) ^(int&) ((IntPtr) &navSectionHeader + 16 /*0x10*/));
    uint num1 = 0;
    fileIn.Read((void*) &num1, 4);
    Log.WriteLine("NumInteractives: {0}", (object) num1);
    BufferStream bufferStream = fileIn;
    bufferStream.Seek((int) ((long) bufferStream.Tell() + (long) num1 * 12L));
    uint num2 = 0;
    fileIn.Read((void*) &num2, 4);
    uint num3 = 0;
    fileIn.Read((void*) &num3, 4);
    Log.WriteLine("NumSubsets: {0}", (object) num2);
    Log.WriteLine("NumNavNodes: {0}", (object) num3);
    uint num4 = 0;
    if (0U < num3)
    {
      do
      {
        NavNode navNode = \u003CModule\u003E.FableMod\u002ELEV\u002E\u003FA0xe3ba95e2\u002EReadNode(fileIn);
        if (navNode != null)
          this.m_Nodes.Add(navNode);
        ++num4;
      }
      while (num4 < num3);
    }
    this.CreateGrids();
    return true;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public unsafe bool Save(FileStream fileOut)
  {
    uint num1 = 0;
    int num2 = (int) FileControl.Write(fileOut, (void*) &num1, 4U);
    int position1 = (int) fileOut.Position;
    uint num3 = 0;
    int num4 = (int) FileControl.Write(fileOut, (void*) &num3, 4U);
    uint count = (uint) this.m_Nodes.Count;
    int num5 = (int) FileControl.Write(fileOut, (void*) &count, 4U);
    int index = 0;
    if (0 < this.m_Nodes.Count)
    {
      do
      {
        NavNode node = this.m_Nodes[index];
        if (num3 < node.Subset + 1U)
          num3 = node.Subset + 1U;
        if (node.Save(fileOut))
          ++index;
        else
          goto label_5;
      }
      while (index < this.m_Nodes.Count);
      goto label_6;
label_5:
      return false;
    }
label_6:
    if (count > 0U)
    {
      int position2 = (int) fileOut.Position;
      fileOut.Position = (long) position1;
      int num6 = (int) FileControl.Write(fileOut, (void*) &num3, 4U);
      fileOut.Position = (long) position2;
    }
    return true;
  }

  public void AddGrid()
  {
    this.m_Grids.Add(new Grid((int) ((double) this.m_Width * 2.0), (int) ((double) this.m_Height * 2.0)));
  }

  public void CreateGrids()
  {
    this.DisposeGrids();
    this.AddGrid();
    int index = 0;
    if (0 < this.m_Nodes.Count)
    {
      do
      {
        NavSection navSection = this;
        navSection.AddNodeToGrid(navSection.m_Nodes[index]);
        ++index;
      }
      while (index < this.m_Nodes.Count);
    }
    this.DisposeNodes();
  }

  public unsafe void CreateNodes()
  {
    byte num1 = 0;
    uint num2 = 1;
    int index1 = 0;
    if (0 < this.m_Grids.Count)
    {
      do
      {
        byte subset = num1;
        ++num1;
        Grid grid = index1 < this.m_Grids.Count ? this.m_Grids[index1] : (Grid) null;
        NavNode.GenerateNodes(this.m_Nodes, subset, grid, &num2);
        ++index1;
      }
      while (index1 < this.m_Grids.Count);
    }
    Console.WriteLine("Generated {0} nodes.", (object) this.m_Nodes.Count);
    int index2 = 0;
    if (0 < this.m_Nodes.Count)
    {
      while (this.m_Nodes[index2].Type == NavNodeType.Blank)
      {
        ++index2;
        if (index2 >= this.m_Nodes.Count)
          goto label_6;
      }
      NavNode.GenerateLinks(this.m_Nodes);
      return;
    }
label_6:
    this.DisposeNodes();
  }

  public Grid GetGrid(int subset)
  {
    if (subset >= this.m_Grids.Count)
    {
      do
      {
        this.m_Grids.Add(new Grid((int) ((double) this.m_Width * 2.0), (int) ((double) this.m_Height * 2.0)));
      }
      while (subset >= this.m_Grids.Count);
    }
    return subset >= this.m_Grids.Count ? (Grid) null : this.m_Grids[subset];
  }

  public int NodeCount => this.m_Nodes.Count;

  public NavNode get_Nodes(int index) => this.m_Nodes[index];

  public int GridCount => this.m_Grids.Count;

  public Grid get_Grids(int index)
  {
    return index >= this.m_Grids.Count ? (Grid) null : this.m_Grids[index];
  }

  public float Width
  {
    get => this.m_Width;
    set => this.m_Width = value;
  }

  public float Height
  {
    get => this.m_Height;
    set => this.m_Height = value;
  }

  public string Name
  {
    get => this.m_Name;
    set => this.m_Name = value;
  }

  protected void DisposeNodes()
  {
    int index = 0;
    if (0 < this.m_Nodes.Count)
    {
      do
      {
        this.m_Nodes[index]?.Dispose();
        ++index;
      }
      while (index < this.m_Nodes.Count);
    }
    this.m_Nodes.Clear();
  }

  protected void DisposeGrids()
  {
    int index = 0;
    if (0 < this.m_Grids.Count)
    {
      do
      {
        this.m_Grids[index]?.Dispose();
        ++index;
      }
      while (index < this.m_Grids.Count);
    }
    this.m_Grids.Clear();
  }

  protected void AddNodeToGrid(NavNode node)
  {
    if (node.Type == NavNodeType.Blank)
      return;
    if (node.Subset >= (uint) this.m_Grids.Count && node.Subset >= (uint) this.m_Grids.Count)
    {
      do
      {
        this.m_Grids.Add(new Grid((int) ((double) this.m_Width * 2.0), (int) ((double) this.m_Height * 2.0)));
      }
      while (node.Subset >= (uint) this.m_Grids.Count);
    }
    Grid grid = this.m_Grids[(int) node.Subset];
    if (node.Type == NavNodeType.Navigation)
    {
      byte num = 1;
      NavigationNode navigationNode = (NavigationNode) node;
      byte add = navigationNode.Special != (byte) 64 /*0x40*/ ? (navigationNode.Special == (byte) 128 /*0x80*/ ? (byte) 17 : num) : (byte) 9;
      this.BlitNodeToGrid(grid, node, add, (byte) navigationNode.Level);
    }
    else if (node.Type == NavNodeType.Dynamic)
    {
      byte num1 = 2;
      DynamicNode dynamicNode = (DynamicNode) node;
      byte add = dynamicNode.Special != (byte) 64 /*0x40*/ ? (dynamicNode.Special == (byte) 128 /*0x80*/ ? (byte) 18 : num1) : (byte) 10;
      this.BlitNodeToGrid(grid, node, add, (byte) dynamicNode.Level);
      float num2 = dynamicNode.Size * 0.5f + dynamicNode.X;
      float num3 = dynamicNode.Y - dynamicNode.Size * 0.5f;
      int x = (int) ((double) ((float) (grid.Width / 2) - num2) * 2.0);
      grid.SetUIDList(dynamicNode.UIDList, x, (int) ((double) num3 * 2.0), (int) ((double) dynamicNode.Size * 2.0));
    }
    node.RemoveAdjacents();
  }

  protected void BlitNodeToGrid(Grid grid, NavNode node, byte add, byte level)
  {
    float num1 = node.Size * 0.5f + node.X;
    float num2 = node.Y - node.Size * 0.5f;
    float num3 = num1 - node.Size;
    float num4 = node.Size + num2;
    int num5 = grid.Width / 2;
    int num6 = (int) ((double) num2 * 2.0);
    float num7 = (float) num5;
    int num8 = (int) ((double) (num7 - num3) * 2.0);
    int num9 = (int) ((double) num4 * 2.0);
    int iX = (int) ((double) (num7 - num1) * 2.0);
    if (iX >= num8)
      return;
    do
    {
      int iY = num6;
      if (num6 < num9)
      {
        do
        {
          grid.SetValueAt(iX, iY, add);
          grid.GetCellAt(iX, iY).Level = level;
          ++iY;
        }
        while (iY < num9);
      }
      ++iX;
    }
    while (iX < num8);
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected unsafe bool WriteInteractives(FileStream fileOut)
  {
    uint num1 = 0;
    int num2 = (int) FileControl.Write(fileOut, (void*) &num1, 4U);
    return true;
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007ENavSection();
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
