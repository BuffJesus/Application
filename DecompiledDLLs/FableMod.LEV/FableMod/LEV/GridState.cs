// Decompiled with JetBrains decompiler
// Type: FableMod.LEV.GridState
// Assembly: FableMod.LEV, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 267777C9-1C4D-4C46-87D1-7AC25AEC038B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.LEV.dll

#nullable disable
namespace FableMod.LEV;

public enum GridState : uint
{
  Node = 0,
  NotEmpty = 1,
  FullNavigation = 2,
  FullDynamic = 4,
  MultipleLevels = 8,
  PartialSpecial = 16, // 0x00000010
  FullSpecial1 = 32, // 0x00000020
  FullSpecial2 = 64, // 0x00000040
}
