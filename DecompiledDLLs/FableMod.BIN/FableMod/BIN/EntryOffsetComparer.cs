// Decompiled with JetBrains decompiler
// Type: FableMod.BIN.EntryOffsetComparer
// Assembly: FableMod.BIN, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 7B343E30-1A4D-49C7-A3B2-33514A983F5F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.BIN.dll

using System.Collections;

#nullable disable
namespace FableMod.BIN;

internal class EntryOffsetComparer : IComparer
{
  public virtual int Compare(object x, object y)
  {
    NamesBINEntry namesBinEntry1 = (NamesBINEntry) x;
    NamesBINEntry namesBinEntry2 = (NamesBINEntry) y;
    if (namesBinEntry1.Offset < namesBinEntry2.Offset)
      return -1;
    return (int) namesBinEntry1.Offset != (int) namesBinEntry2.Offset ? 1 : 0;
  }
}
