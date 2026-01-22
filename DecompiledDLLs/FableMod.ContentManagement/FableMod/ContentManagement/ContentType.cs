// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.ContentType
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

#nullable disable
namespace FableMod.ContentManagement;

public enum ContentType : uint
{
  Unknown = 0,
  Names = 1,
  BIG = 2,
  BIN = 4,
  Graphics = 1026, // 0x00000402
  Text = 2050, // 0x00000802
  MainTextures = 4098, // 0x00001002
  GUITextures = 8194, // 0x00002002
  FrontEndTextures = 16386, // 0x00004002
  Objects = 1048580, // 0x00100004
  Scripts = 2097156, // 0x00200004
}
