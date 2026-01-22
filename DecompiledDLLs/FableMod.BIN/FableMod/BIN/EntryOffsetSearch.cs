// Decompiled with JetBrains decompiler
// Type: FableMod.BIN.EntryOffsetSearch
// Assembly: FableMod.BIN, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 7B343E30-1A4D-49C7-A3B2-33514A983F5F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.BIN.dll

using System.Collections;

#nullable disable
namespace FableMod.BIN;

internal class EntryOffsetSearch : IComparer
{
  public virtual int Compare(object x, object y)
  {
    NamesBINEntry namesBinEntry = (NamesBINEntry) x;
    uint num = (uint) y;
    if (namesBinEntry.Offset < num)
      return -1;
    return (int) namesBinEntry.Offset != (int) num ? 1 : 0;
  }
}
