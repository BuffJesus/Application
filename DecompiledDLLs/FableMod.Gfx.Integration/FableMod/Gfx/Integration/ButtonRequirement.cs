// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.ButtonRequirement
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using System;

#nullable disable
namespace FableMod.Gfx.Integration;

[Flags]
public enum ButtonRequirement : uint
{
  Modifier = 2,
  Mouse = 1,
  Key = 0,
}
