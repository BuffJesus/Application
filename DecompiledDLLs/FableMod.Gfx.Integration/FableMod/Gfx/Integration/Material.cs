// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.Material
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.Gfx.Integration;

public class Material(MTRL* mat)
{
  private unsafe MTRL* m_Material = mat;

  public unsafe uint ID
  {
    get => (uint) *(int*) this.m_Material;
    set => *(int*) this.m_Material = (int) value;
  }

  public unsafe string Name
  {
    get => new string((sbyte*) *(long*) ((IntPtr) this.m_Material + 4L));
    set
    {
      \u003CModule\u003E.delete\u005B\u005D((void*) *(long*) ((IntPtr) this.m_Material + 4L));
      *(long*) ((IntPtr) this.m_Material + 4L) = (long) \u003CModule\u003E.new\u005B\u005D((ulong) (value.Length + 1));
      void* pointer = Marshal.StringToHGlobalAnsi(value).ToPointer();
      long num1 = *(long*) ((IntPtr) this.m_Material + 4L);
      sbyte num2;
      do
      {
        num2 = *(sbyte*) pointer;
        *(sbyte*) num1 = num2;
        ++pointer;
        ++num1;
      }
      while (num2 != (sbyte) 0);
    }
  }

  public unsafe uint Padding => (uint) *(int*) ((IntPtr) this.m_Material + 12L);

  public unsafe uint BaseTextureID
  {
    get => (uint) *(int*) ((IntPtr) this.m_Material + 16L /*0x10*/);
    set => *(int*) ((IntPtr) this.m_Material + 16L /*0x10*/) = (int) value;
  }

  public unsafe uint BumpMapTextureID
  {
    get => (uint) *(int*) ((IntPtr) this.m_Material + 20L);
    set => *(int*) ((IntPtr) this.m_Material + 20L) = (int) value;
  }

  public unsafe uint ReflectionTextureID
  {
    get => (uint) *(int*) ((IntPtr) this.m_Material + 24L);
    set => *(int*) ((IntPtr) this.m_Material + 24L) = (int) value;
  }

  public unsafe uint AlphaMapTextureID
  {
    get => (uint) *(int*) ((IntPtr) this.m_Material + 28L);
    set => *(int*) ((IntPtr) this.m_Material + 28L) = (int) value;
  }

  public unsafe uint TextureFlags
  {
    get => (uint) *(int*) ((IntPtr) this.m_Material + 32L /*0x20*/);
    set => *(int*) ((IntPtr) this.m_Material + 32L /*0x20*/) = (int) value;
  }

  public unsafe uint GlowStrength
  {
    get => (uint) *(int*) ((IntPtr) this.m_Material + 36L);
    set => *(int*) ((IntPtr) this.m_Material + 36L) = (int) value;
  }

  public unsafe byte Unknown2
  {
    get => *(byte*) ((IntPtr) this.m_Material + 40L);
    set => *(sbyte*) ((IntPtr) this.m_Material + 40L) = (sbyte) value;
  }

  public unsafe byte AlphaEnabled
  {
    get => *(byte*) ((IntPtr) this.m_Material + 41L);
    set => *(sbyte*) ((IntPtr) this.m_Material + 41L) = (sbyte) value;
  }

  public unsafe byte Unknown3
  {
    get => *(byte*) ((IntPtr) this.m_Material + 42L);
    set => *(sbyte*) ((IntPtr) this.m_Material + 42L) = (sbyte) value;
  }

  public unsafe ushort Unknown4
  {
    get => *(ushort*) ((IntPtr) this.m_Material + 43L);
    set => *(short*) ((IntPtr) this.m_Material + 43L) = (short) value;
  }
}
