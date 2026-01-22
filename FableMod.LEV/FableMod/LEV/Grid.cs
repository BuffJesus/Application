// Decompiled with JetBrains decompiler
// Type: FableMod.LEV.Grid
// Assembly: FableMod.LEV, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 267777C9-1C4D-4C46-87D1-7AC25AEC038B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.LEV.dll

using \u003CCppImplementationDetails\u003E;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.LEV;

public class Grid : IDisposable
{
  protected GridCell[] m_Cells;
  protected int m_Width;
  protected int m_Height;

  public Grid(int iWidth, int iHeight)
  {
    this.m_Width = iWidth;
    this.m_Height = iHeight;
    // ISSUE: explicit constructor call
    base.\u002Ector();
    GridCell[] gridCellArray = new GridCell[this.m_Height * this.m_Width];
    this.m_Cells = gridCellArray;
    int index = 0;
    if (0 >= gridCellArray.Length)
      return;
    do
    {
      this.m_Cells[index] = new GridCell();
      ++index;
    }
    while (index < this.m_Cells.Length);
  }

  private void \u007EGrid()
  {
    int index = 0;
    if (0 < this.m_Cells.Length)
    {
      do
      {
        this.m_Cells[index]?.Dispose();
        ++index;
      }
      while (index < this.m_Cells.Length);
    }
    this.m_Cells = (GridCell[]) null;
  }

  public void SetUIDList(UIDList List, int x, int y, int size)
  {
    int num1 = y;
    int num2 = y + size;
    if (y >= num2)
      return;
    int num3 = x + size;
    do
    {
      int num4 = x;
      if (x < num3)
      {
        do
        {
          this.m_Cells[this.m_Width * num1 + num4].UIDList.Copy(List);
          ++num4;
        }
        while (num4 < num3);
      }
      ++num1;
    }
    while (num1 < num2);
  }

  public void AddToUIDList(UIDList listOut, int x, int y, int size)
  {
    int num1 = y;
    int num2 = y + size;
    if (y >= num2)
      return;
    int num3 = x + size;
    do
    {
      int num4 = x;
      if (x < num3)
      {
        do
        {
          listOut.Merge(this.m_Cells[this.m_Width * num1 + num4].UIDList);
          ++num4;
        }
        while (num4 < num3);
      }
      ++num1;
    }
    while (num1 < num2);
  }

  public void SetValueAt(int iX, int iY, byte ucValue)
  {
    this.m_Cells[this.m_Width * iY + iX].Value = ucValue;
  }

  public byte GetValueAt(int iX, int iY) => this.m_Cells[this.m_Width * iY + iX].Value;

  public GridCell GetCellAt(int iX, int iY) => this.m_Cells[this.m_Width * iY + iX];

  public unsafe int GetNavigationState(int iX, int iY, int iSize)
  {
    \u0024ArrayType\u0024\u0024\u0024BY03H arrayTypeBy03H;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ref arrayTypeBy03H = 0;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &arrayTypeBy03H + 4) = 0;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &arrayTypeBy03H + 8) = 0;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &arrayTypeBy03H + 12) = 0;
    int navigationState = 0;
    int num1 = (int) this.GetCellAt(iX, iY).Level;
    int iY1 = iY;
    int num2 = iY + iSize;
    if (iY < num2)
    {
      int num3 = iX + iSize;
      do
      {
        int iX1 = iX;
        if (iX < num3)
        {
          do
          {
            GridCell cellAt = this.GetCellAt(iX1, iY1);
            byte num4 = cellAt.Value;
            if ((byte) ((uint) num4 & 1U) != (byte) 0)
            {
              navigationState |= 1;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(int&) ref arrayTypeBy03H = ^(int&) ref arrayTypeBy03H + 1;
            }
            else if ((byte) ((uint) num4 & 2U) != (byte) 0)
            {
              navigationState |= 1;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(int&) ((IntPtr) &arrayTypeBy03H + 4) = ^(int&) ((IntPtr) &arrayTypeBy03H + 4) + 1;
            }
            if ((byte) ((uint) num4 & 8U) != (byte) 0)
            {
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(int&) ((IntPtr) &arrayTypeBy03H + 8) = ^(int&) ((IntPtr) &arrayTypeBy03H + 8) + 1;
            }
            if ((byte) ((uint) num4 & 16U /*0x10*/) != (byte) 0)
            {
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(int&) ((IntPtr) &arrayTypeBy03H + 12) = ^(int&) ((IntPtr) &arrayTypeBy03H + 12) + 1;
            }
            num1 = (int) cellAt.Level != num1 ? -1 : num1;
            ++iX1;
          }
          while (iX1 < num3);
        }
        ++iY1;
      }
      while (iY1 < num2);
    }
    int num5 = iSize;
    int num6 = num5 * num5;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    if (^(int&) ((IntPtr) &arrayTypeBy03H + 4) == num6)
    {
      navigationState |= 4;
    }
    else
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      if (^(int&) ref arrayTypeBy03H == num6)
        navigationState |= 2;
    }
    if ((navigationState & 1) != 0)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      if (^(int&) ((IntPtr) &arrayTypeBy03H + 8) == num6)
      {
        navigationState |= 32 /*0x20*/;
      }
      else
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if (^(int&) ((IntPtr) &arrayTypeBy03H + 12) == num6)
        {
          navigationState |= 64 /*0x40*/;
        }
        else
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          if (^(int&) ((IntPtr) &arrayTypeBy03H + 8) > 0 || ^(int&) ((IntPtr) &arrayTypeBy03H + 12) > 0)
            navigationState |= 16 /*0x10*/;
        }
      }
      if (num1 < 0)
        navigationState |= 8;
    }
    return navigationState;
  }

  public int Width => this.m_Width;

  public int Height => this.m_Height;

  internal unsafe void ToData(byte* aucData)
  {
    int num1 = 0;
    long num2 = 0;
    if (0 >= this.m_Width)
      return;
    int height = this.m_Height;
    do
    {
      int num3 = 0;
      long num4 = 0;
      if (0 < height)
      {
        do
        {
          int width = this.m_Width;
          *(sbyte*) ((long) width * num4 + num2 + (IntPtr) aucData) = (sbyte) this.m_Cells[width * num3 + num1].Value;
          ++num3;
          ++num4;
          height = this.m_Height;
        }
        while (num3 < height);
      }
      ++num1;
      ++num2;
    }
    while (num1 < this.m_Width);
  }

  internal unsafe void FromData(byte* aucData)
  {
    int num1 = 0;
    long num2 = 0;
    if (0 >= this.m_Width)
      return;
    int height = this.m_Height;
    do
    {
      int num3 = 0;
      long num4 = 0;
      if (0 < height)
      {
        do
        {
          int width = this.m_Width;
          this.m_Cells[width * num3 + num1].Value = *(byte*) ((long) width * num4 + num2 + (IntPtr) aucData);
          ++num3;
          ++num4;
          height = this.m_Height;
        }
        while (num3 < height);
      }
      ++num1;
      ++num2;
    }
    while (num1 < this.m_Width);
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EGrid();
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
