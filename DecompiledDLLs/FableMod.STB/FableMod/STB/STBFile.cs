// Decompiled with JetBrains decompiler
// Type: FableMod.STB.STBFile
// Assembly: FableMod.STB, Version=1.0.4918.435, Culture=neutral, PublicKeyToken=null
// MVID: 2266CC66-C206-4DE0-80D4-5C3E14C1F606
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.STB.dll

using \u003CCppImplementationDetails\u003E;
using FableMod.BBB;
using FableMod.CLRCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.STB;

public class STBFile : BBBFile
{
  protected Dictionary<string, uint> m_Offsets = (Dictionary<string, uint>) null;

  private void \u007ESTBFile()
  {
  }

  public unsafe Level GetLevel(string name)
  {
    this.CheckOffsets();
    uint num1 = 0;
    if (!this.m_Offsets.TryGetValue(name.ToLower(), out num1))
    {
      Console.WriteLine("STB: Level {0} not found", (object) name);
      return (Level) null;
    }
    this.File.Position = (long) num1;
    STBLevBaseHeader stbLevBaseHeader;
    int num2 = (int) FileControl.Read(this.File, (void*) &stbLevBaseHeader, 40U);
    STBLevSectionHeader levSectionHeader;
    int num3 = (int) FileControl.Read(this.File, (void*) &levSectionHeader, 56U);
    STBLevOffsetHeader stbLevOffsetHeader;
    int num4 = (int) FileControl.Read(this.File, (void*) &stbLevOffsetHeader, 21U);
    uint num5 = 0;
    int num6 = (int) FileControl.Read(this.File, (void*) &num5, 4U);
    int num7 = (int) FileControl.Read(this.File, (void*) &num5, 4U);
    float num8;
    int num9 = (int) FileControl.Read(this.File, (void*) &num8, 4U);
    int num10 = (int) FileControl.Read(this.File, (void*) &num8, 4U);
    int num11 = (int) FileControl.Read(this.File, (void*) &num8, 4U);
    int num12 = (int) FileControl.Read(this.File, (void*) &num8, 4U);
    int num13 = (int) FileControl.Read(this.File, (void*) &num8, 4U);
    int num14 = (int) FileControl.Read(this.File, (void*) &num5, 4U);
    int num15 = (int) FileControl.Read(this.File, (void*) &num5, 4U);
    Console.WriteLine("3rd: {0}", (object) num5);
    int num16 = (int) FileControl.Read(this.File, (void*) &num5, 4U);
    Console.WriteLine("3rdsize: {0}", (object) num5);
    uint num17;
    int num18 = (int) FileControl.Read(this.File, (void*) &num17, 4U);
    Console.WriteLine("offset 3rd: {0}", (object) num5);
    int num19 = (int) FileControl.Read(this.File, (void*) &num5, 4U);
    ushort num20;
    int num21 = (int) FileControl.Read(this.File, (void*) &num20, 2U);
    int num22 = (int) FileControl.Read(this.File, (void*) &num5, 2U);
    byte num23;
    int num24 = (int) FileControl.Read(this.File, (void*) &num23, 1U);
    int num25 = (int) FileControl.Read(this.File, (void*) &num5, 4U);
    Level level = new Level();
    Console.WriteLine("STB Level: {0}", (object) name);
    int num26 = 0;
    if (0U < num5)
    {
      do
      {
        Flora flora = new Flora();
        flora.Read(this.File);
        level.Add(flora);
        ++num26;
      }
      while ((uint) num26 < num5);
    }
    return level;
  }

  protected unsafe void CheckOffsets()
  {
    if (this.m_Offsets != null)
      return;
    this.m_Offsets = new Dictionary<string, uint>();
    BBBEntry bbbEntry = this.get_Entries("__STATIC_MAP_COMMON_HEADER__");
    this.File.Position = (long) bbbEntry.FileOffset;
    BinaryReader binaryReader = new BinaryReader((Stream) this.File);
    uint num1 = binaryReader.ReadUInt32();
    if (0U >= num1)
      return;
    uint num2 = num1;
    do
    {
      \u0024ArrayType\u0024\u0024\u0024BY0BAA\u0040D arrayTypeBy0BaaD;
      sbyte* numPtr = (sbyte*) &arrayTypeBy0BaaD;
      sbyte num3;
      do
      {
        num3 = (sbyte) binaryReader.ReadByte();
        *numPtr = num3;
        ++numPtr;
      }
      while (num3 != (sbyte) 0);
      this.m_Offsets[new string((sbyte*) &arrayTypeBy0BaaD).ToLower()] = binaryReader.ReadUInt32() + bbbEntry.FileOffset;
      num2 += uint.MaxValue;
    }
    while (num2 > 0U);
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
      }
      finally
      {
        base.Dispose(true);
      }
    }
    else
      base.Dispose(false);
  }
}
