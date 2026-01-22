// Decompiled with JetBrains decompiler
// Type: FableMod.LEV.UIDList
// Assembly: FableMod.LEV, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 267777C9-1C4D-4C46-87D1-7AC25AEC038B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.LEV.dll

using FableMod.CLRCore;
using System.Collections.Generic;
using System.IO;

#nullable disable
namespace FableMod.LEV;

public class UIDList
{
  protected static ulong UID_BASE = 18446741874686296064 /*0xFFFFFE0000000000*/;
  protected readonly List<ulong> m_UIDs = new List<ulong>();

  public void Add(ulong uod)
  {
    if (this.m_UIDs.IndexOf(uod) >= 0)
      return;
    this.m_UIDs.Add(uod);
  }

  public void Copy(UIDList list)
  {
    this.m_UIDs.Clear();
    this.Merge(list);
  }

  public void Merge(UIDList list)
  {
    int index = 0;
    if (0 >= list.m_UIDs.Count)
      return;
    do
    {
      ulong uiD = list.m_UIDs[index];
      if (this.m_UIDs.IndexOf(uiD) < 0)
        this.m_UIDs.Add(uiD);
      ++index;
    }
    while (index < list.m_UIDs.Count);
  }

  public unsafe void Load(BufferStream fileIn)
  {
    uint num1 = 0;
    fileIn.Read((void*) &num1, 4);
    this.m_UIDs.Clear();
    uint num2 = 0;
    if (0U >= num1)
      return;
    do
    {
      ulong num3 = 0;
      fileIn.Read((void*) &num3, 8);
      ulong num4 = UIDList.UID_BASE + num3;
      if (this.m_UIDs.IndexOf(num4) < 0)
        this.m_UIDs.Add(num4);
      ++num2;
    }
    while (num2 < num1);
  }

  public unsafe void Save(FileStream fileOut)
  {
    uint count = (uint) this.m_UIDs.Count;
    int num1 = (int) FileControl.Write(fileOut, (void*) &count, 4U);
    int index = 0;
    if (0 >= this.m_UIDs.Count)
      return;
    do
    {
      ulong num2 = this.m_UIDs[index] - UIDList.UID_BASE;
      int num3 = (int) FileControl.Write(fileOut, (void*) &num2, 8U);
      ++index;
    }
    while (index < this.m_UIDs.Count);
  }

  public int Count => this.m_UIDs.Count;

  public ulong get_Items(int index) => this.m_UIDs[index];
}
