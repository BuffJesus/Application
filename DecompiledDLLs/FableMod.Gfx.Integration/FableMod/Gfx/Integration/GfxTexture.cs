// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.GfxTexture
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using FableMod.BIG;
using FableMod.CLRCore;
using FableMod.Data;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Gfx.Integration;

public class GfxTexture : IDisposable
{
  public static string SAVE_FILE_FILTER = "BMP Files (*.bmp)|*.bmp|" + "JPG Files (*.jpg)|*.jpg|" + "TGA Files (*.tga)|*.tga|" + "PNG Files (*.png)|*.png|" + "DDS Files (*.dds)|*.dds||";
  public static string LOAD_FILE_FILTER = "All Image Files|*.bmp;*.jpg;*.tga;*.png;*.dds|" + GfxTexture.SAVE_FILE_FILTER;
  protected unsafe ID3DXSprite* m_ISprite;
  protected unsafe IDirect3DTexture9** m_ITextures;
  protected unsafe TextureSubHeader* m_SubHeader;
  protected unsafe SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E* m_pBaseTexture;

  public unsafe GfxTexture(string filename, TextureFormat format)
  {
    FileStream File = File.OpenRead(filename);
    uint uiCount = File.CanRead ? (uint) (int) File.Length : throw new System.Exception("failed to read file");
    sbyte* pBuffer = (sbyte*) \u003CModule\u003E.new\u005B\u005D((ulong) uiCount);
    int num1 = (int) FileControl.Read(File, (void*) pBuffer, uiCount);
    File.Close();
    TextureSubHeader* textureSubHeaderPtr = (TextureSubHeader*) \u003CModule\u003E.@new(34UL);
    this.m_SubHeader = textureSubHeaderPtr;
    // ISSUE: initblk instruction
    __memset((IntPtr) textureSubHeaderPtr, 0, 34);
    *(short*) ((IntPtr) textureSubHeaderPtr + 10L) = (short) 1;
    switch (format)
    {
      case TextureFormat.DXT1:
        *(short*) ((IntPtr) textureSubHeaderPtr + 12L) = (short) 31 /*0x1F*/;
        *(short*) ((IntPtr) textureSubHeaderPtr + 28L) = (short) 1027;
        *(int*) ((IntPtr) textureSubHeaderPtr + 30L) = 0;
        *(sbyte*) ((IntPtr) textureSubHeaderPtr + 16L /*0x10*/) = (sbyte) 0;
        break;
      case TextureFormat.DXT3:
        *(short*) ((IntPtr) textureSubHeaderPtr + 12L) = (short) 32 /*0x20*/;
        *(short*) ((IntPtr) textureSubHeaderPtr + 28L) = (short) 2050;
        *(int*) ((IntPtr) textureSubHeaderPtr + 30L) = 0;
        *(sbyte*) ((IntPtr) textureSubHeaderPtr + 16L /*0x10*/) = (sbyte) 1;
        break;
    }
    this.m_ITextures = (IDirect3DTexture9**) \u003CModule\u003E.new\u005B\u005D((ulong) *(ushort*) ((IntPtr) textureSubHeaderPtr + 10L) * 8UL);
    IDirect3DDevice9* idirect3Ddevice9Ptr = (IDirect3DDevice9*) *(long*) ((IntPtr) \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA + 8L);
    IDirect3DTexture9* idirect3Dtexture9Ptr1 = (IDirect3DTexture9*) 0L;
    if (\u003CModule\u003E.D3DXCreateTextureFromFileInMemoryEx(idirect3Ddevice9Ptr, (void*) pBuffer, uiCount, 0U, 0U, 0U, 0U, (_D3DFORMAT) format, (_D3DPOOL) 1, uint.MaxValue, uint.MaxValue, 0U, (_D3DXIMAGE_INFO*) 0L, (tagPALETTEENTRY*) 0L, &idirect3Dtexture9Ptr1) < 0)
      throw new System.Exception("failed to create texture");
    *(long*) this.m_ITextures = (long) idirect3Dtexture9Ptr1;
    IDirect3DTexture9* idirect3Dtexture9Ptr2 = idirect3Dtexture9Ptr1;
    _D3DSURFACE_DESC d3DsurfaceDesc;
    ref _D3DSURFACE_DESC local = ref d3DsurfaceDesc;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num2 = __calli((__FnPtr<int (IntPtr, uint, _D3DSURFACE_DESC*)>) *(long*) (*(long*) idirect3Dtexture9Ptr1 + 136L))((IntPtr) idirect3Dtexture9Ptr2, 0U, (_D3DSURFACE_DESC*) ref local);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ushort num3 = (ushort) ^(int&) ((IntPtr) &d3DsurfaceDesc + 28);
    TextureSubHeader* subHeader = this.m_SubHeader;
    *(short*) ((IntPtr) subHeader + 8L) = (short) num3;
    *(short*) ((IntPtr) subHeader + 2L) = (short) num3;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ushort num4 = (ushort) ^(int&) ((IntPtr) &d3DsurfaceDesc + 24);
    *(short*) ((IntPtr) subHeader + 6L) = (short) num4;
    *(short*) subHeader = (short) num4;
    *(short*) ((IntPtr) subHeader + 4L) = (short) 0;
    ID3DXSprite* id3DxSpritePtr = (ID3DXSprite*) 0L;
    this.m_ISprite = \u003CModule\u003E.D3DXCreateSprite(idirect3Ddevice9Ptr, &id3DxSpritePtr) >= 0 ? id3DxSpritePtr : throw new System.Exception("failed to create sprite");
    \u003CModule\u003E.delete\u005B\u005D((void*) pBuffer);
  }

  public unsafe GfxTexture(AssetEntry entry)
  {
    if (entry.GetSubHeaderLength() != 34U)
      throw new System.Exception("FableMod::Gfx::Integration: Invalid subheader length");
    this.m_SubHeader = (TextureSubHeader*) \u003CModule\u003E.@new(34UL);
    sbyte* subHeader1 = entry.GetSubHeader();
    TextureSubHeader* subHeader2 = this.m_SubHeader;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) subHeader2, (IntPtr) subHeader1, 34);
    uint num1 = 0;
    int _Y = 0;
    if ((byte) 0 < *(byte*) ((IntPtr) subHeader2 + 17L))
    {
      double num2 = (double) (uint) *(int*) ((IntPtr) subHeader2 + 20L);
      byte num3 = *(byte*) ((IntPtr) this.m_SubHeader + 17L);
      do
      {
        double num4 = \u003CModule\u003E._Pow_int\u003Cdouble\u003E(4.0, _Y);
        num1 = (uint) (num2 / num4 + (double) num1);
        ++_Y;
      }
      while (_Y < (int) num3);
    }
    sbyte* numPtr = (sbyte*) \u003CModule\u003E.new\u005B\u005D((ulong) (uint) ((int) *(ushort*) ((IntPtr) subHeader2 + 10L) * (int) num1 + 128 /*0x80*/));
    *(int*) numPtr = 542327876;
    _DDSURFACEDESC2_32* ddsurfacedesC232Ptr = (_DDSURFACEDESC2_32*) (numPtr + 4L);
    // ISSUE: initblk instruction
    __memset((IntPtr) ddsurfacedesC232Ptr, 0, 124);
    *(int*) ddsurfacedesC232Ptr = 124;
    *(int*) ((IntPtr) ddsurfacedesC232Ptr + 4L) = 659463;
    TextureSubHeader* subHeader3 = this.m_SubHeader;
    *(int*) ((IntPtr) ddsurfacedesC232Ptr + 8L) = (int) *(ushort*) ((IntPtr) subHeader3 + 2L);
    *(int*) ((IntPtr) ddsurfacedesC232Ptr + 12L) = (int) *(ushort*) subHeader3;
    *(int*) ((IntPtr) ddsurfacedesC232Ptr + 20L) = (int) *(ushort*) ((IntPtr) subHeader3 + 4L);
    *(int*) ((IntPtr) ddsurfacedesC232Ptr + 24L) = (int) *(byte*) ((IntPtr) subHeader3 + 17L);
    *(int*) ((IntPtr) ddsurfacedesC232Ptr + 16L /*0x10*/) = *(int*) ((IntPtr) subHeader3 + 20L);
    *(int*) ((IntPtr) ddsurfacedesC232Ptr + 72L) = 32 /*0x20*/;
    *(int*) ((IntPtr) ddsurfacedesC232Ptr + 76L) = 4;
    ushort num5 = *(ushort*) ((IntPtr) subHeader3 + 12L);
    switch (num5)
    {
      case 31 /*0x1F*/:
        *(int*) ((IntPtr) ddsurfacedesC232Ptr + 80L /*0x50*/) = 827611204;
        break;
      case 32 /*0x20*/:
      case 35:
        *(int*) ((IntPtr) ddsurfacedesC232Ptr + 80L /*0x50*/) = 861165636;
        break;
      default:
        throw new System.Exception($"Invalid texture format: {num5:X2}");
    }
    *(int*) ((IntPtr) ddsurfacedesC232Ptr + 104L) = 4198408;
    *(int*) ((IntPtr) ddsurfacedesC232Ptr + 108L) = 2097152 /*0x200000*/;
    IDirect3DTexture9** idirect3Dtexture9Ptr = (IDirect3DTexture9**) \u003CModule\u003E.new\u005B\u005D((ulong) *(ushort*) ((IntPtr) subHeader3 + 10L) * 8UL);
    this.m_ITextures = idirect3Dtexture9Ptr;
    TextureSubHeader* textureSubHeaderPtr = (TextureSubHeader*) ((IntPtr) this.m_SubHeader + 10L);
    // ISSUE: initblk instruction
    __memset((IntPtr) idirect3Dtexture9Ptr, 0, (long) *(ushort*) textureSubHeaderPtr * 8L);
    IDirect3DDevice9* idirect3Ddevice9Ptr = (IDirect3DDevice9*) *(long*) ((IntPtr) \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA + 8L);
    uint num6 = 0;
    int num7 = 0;
    if ((ushort) 0 < *(ushort*) textureSubHeaderPtr)
    {
      uint num8 = num1 + 128U /*0x80*/;
      do
      {
        TextureSubHeader* subHeader4 = this.m_SubHeader;
        if (*(int*) ((IntPtr) subHeader4 + 24L) != 0)
        {
          uint num9 = (uint) *(int*) ((IntPtr) subHeader4 + 20L);
          long num10 = (long) num6;
          LZO.DecompressChunk((byte*) (entry.GetData() + num10), (uint) *(int*) ((IntPtr) subHeader4 + 24L), (byte*) (numPtr + 128L /*0x80*/), &num9);
          TextureSubHeader* subHeader5 = this.m_SubHeader;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) (numPtr + (long) (uint) *(int*) ((IntPtr) subHeader5 + 20L) + 128L /*0x80*/), (long) (uint) *(int*) ((IntPtr) subHeader5 + 24L) + (num10 + (IntPtr) entry.GetData()), (long) (num1 - (uint) *(int*) ((IntPtr) this.m_SubHeader + 20L)));
          num6 += (uint) (*(int*) ((IntPtr) subHeader5 + 24L) - *(int*) ((IntPtr) subHeader5 + 20L)) + num1;
        }
        else
        {
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) (numPtr + 128L /*0x80*/), (IntPtr) (entry.GetData() + (long) num6), (long) num1);
          num6 += num1;
        }
        if (\u003CModule\u003E.D3DXCreateTextureFromFileInMemory(idirect3Ddevice9Ptr, (void*) numPtr, num8, (IDirect3DTexture9**) ((long) num7 * 8L + (IntPtr) this.m_ITextures)) >= 0)
          ++num7;
        else
          goto label_16;
      }
      while (num7 < (int) *(ushort*) ((IntPtr) this.m_SubHeader + 10L));
      goto label_17;
label_16:
      throw new System.Exception("FableMod::Gfx::Integration: Failed to create texture");
    }
label_17:
    \u003CModule\u003E.delete\u005B\u005D((void*) numPtr);
    ID3DXSprite* id3DxSpritePtr = (ID3DXSprite*) 0L;
    this.m_ISprite = \u003CModule\u003E.D3DXCreateSprite(idirect3Ddevice9Ptr, &id3DxSpritePtr) >= 0 ? id3DxSpritePtr : throw new System.Exception("FableMod::Gfx::Integration: Failed to create sprite");
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E* fableModGfxTexturePtr1 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E* fableModGfxTexturePtr2;
    // ISSUE: fault handler
    try
    {
      if ((IntPtr) fableModGfxTexturePtr1 != IntPtr.Zero)
      {
        Texture* texturePtr1 = (Texture*) \u003CModule\u003E.@new(40UL);
        Texture* texturePtr2;
        // ISSUE: fault handler
        try
        {
          texturePtr2 = (IntPtr) texturePtr1 == IntPtr.Zero ? (Texture*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002ETexture\u002E\u007Bctor\u007D(texturePtr1);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) texturePtr1);
        }
        *(long*) fableModGfxTexturePtr1 = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VTexture\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
        *(long*) ((IntPtr) fableModGfxTexturePtr1 + 8L) = (long) texturePtr2;
        if ((IntPtr) texturePtr2 != IntPtr.Zero)
          *(int*) ((IntPtr) texturePtr2 + 8L) = *(int*) ((IntPtr) texturePtr2 + 8L) + 1;
        fableModGfxTexturePtr2 = fableModGfxTexturePtr1;
      }
      else
        fableModGfxTexturePtr2 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) 0L;
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) fableModGfxTexturePtr1);
    }
    this.m_pBaseTexture = fableModGfxTexturePtr2;
    byte num11 = (byte) (*(byte*) ((IntPtr) this.m_SubHeader + 16L /*0x10*/) > (byte) 0);
    \u003CModule\u003E.FableMod\u002EGfx\u002ETexture\u002ESetAlphaRequired((Texture*) *(long*) ((IntPtr) fableModGfxTexturePtr2 + 8L), (bool) num11);
    \u003CModule\u003E.FableMod\u002EGfx\u002ETexture\u002ESetD3DTexture((Texture*) *(long*) ((IntPtr) this.m_pBaseTexture + 8L), (IDirect3DTexture9*) *(long*) this.m_ITextures);
    IntPtr hglobalUni = Marshal.StringToHGlobalUni(entry.DevSymbolName);
    \u003CModule\u003E.FableMod\u002EGfx\u002ERootObject\u002ESetName((RootObject*) *(long*) ((IntPtr) this.m_pBaseTexture + 8L), (char*) hglobalUni.ToPointer());
    Marshal.FreeHGlobal(hglobalUni);
    entry.Purge();
  }

  private void \u007EGfxTexture() => this.\u0021GfxTexture();

  private unsafe void \u0021GfxTexture()
  {
    if ((IntPtr) this.m_ITextures != IntPtr.Zero)
    {
      int num1 = 0;
      if ((ushort) 0 < *(ushort*) ((IntPtr) this.m_SubHeader + 10L))
      {
        long num2 = 0;
        do
        {
          IDirect3DTexture9** idirect3Dtexture9Ptr = (IDirect3DTexture9**) ((IntPtr) this.m_ITextures + num2);
          if (*(long*) idirect3Dtexture9Ptr != 0L)
          {
            long num3 = *(long*) idirect3Dtexture9Ptr;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            int num4 = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) num3 + 16L /*0x10*/))((IntPtr) num3);
            *(long*) ((IntPtr) this.m_ITextures + num2) = 0L;
          }
          ++num1;
          num2 += 8L;
        }
        while (num1 < (int) *(ushort*) ((IntPtr) this.m_SubHeader + 10L));
      }
      \u003CModule\u003E.delete\u005B\u005D((void*) this.m_ITextures);
    }
    TextureSubHeader* subHeader = this.m_SubHeader;
    if ((IntPtr) subHeader != IntPtr.Zero)
      \u003CModule\u003E.delete\u005B\u005D((void*) subHeader);
    ID3DXSprite* isprite = this.m_ISprite;
    if ((IntPtr) isprite != IntPtr.Zero)
    {
      ID3DXSprite* id3DxSpritePtr = isprite;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int num = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxSpritePtr + 16L /*0x10*/))((IntPtr) id3DxSpritePtr);
      this.m_ISprite = (ID3DXSprite*) 0L;
    }
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E* pBaseTexture = this.m_pBaseTexture;
    if ((IntPtr) pBaseTexture == IntPtr.Zero)
      return;
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E* fableModGfxTexturePtr1 = pBaseTexture;
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E* fableModGfxTexturePtr2 = fableModGfxTexturePtr1;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) fableModGfxTexturePtr1)((IntPtr) fableModGfxTexturePtr2, 1U);
    this.m_pBaseTexture = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) 0L;
  }

  public unsafe void Save(string filename, int frameIndex)
  {
    if (frameIndex >= (int) *(ushort*) ((IntPtr) this.m_SubHeader + 10L) || frameIndex < 0)
      frameIndex = 0;
    IntPtr hglobalUni = Marshal.StringToHGlobalUni(filename);
    string upper = Path.GetExtension(filename).ToUpper();
    _D3DXIMAGE_FILEFORMAT dximageFileformat1 = (_D3DXIMAGE_FILEFORMAT) 1;
    _D3DXIMAGE_FILEFORMAT dximageFileformat2;
    int num;
    switch (upper)
    {
      case ".BMP":
        dximageFileformat2 = (_D3DXIMAGE_FILEFORMAT) 0;
        goto label_9;
      case ".PNG":
        dximageFileformat2 = (_D3DXIMAGE_FILEFORMAT) 3;
        goto label_9;
      case ".TGA":
        dximageFileformat2 = (_D3DXIMAGE_FILEFORMAT) 2;
        goto label_9;
      case ".DDS":
        num = 4;
        break;
      default:
        num = (int) dximageFileformat1;
        break;
    }
    dximageFileformat2 = (_D3DXIMAGE_FILEFORMAT) num;
label_9:
    \u003CModule\u003E.D3DXSaveTextureToFileW((char*) hglobalUni.ToPointer(), dximageFileformat2, (IDirect3DBaseTexture9*) *(long*) ((IntPtr) this.m_ITextures + (long) frameIndex * 8L), (tagPALETTEENTRY*) 0L);
    Marshal.FreeHGlobal(hglobalUni);
  }

  public unsafe void ApplyToEntry(AssetEntry entry)
  {
    TextureSubHeader* subHeader1 = this.m_SubHeader;
    switch (*(ushort*) ((IntPtr) subHeader1 + 12L))
    {
      case 31 /*0x1F*/:
        *(int*) ((IntPtr) subHeader1 + 20L) = (int) *(ushort*) ((IntPtr) subHeader1 + 2L) * (int) *(ushort*) subHeader1 / 2;
        break;
      case 32 /*0x20*/:
        *(int*) ((IntPtr) subHeader1 + 20L) = (int) *(ushort*) ((IntPtr) subHeader1 + 2L) * (int) *(ushort*) subHeader1;
        break;
      default:
        throw new InvalidOperationException("Invalid DXT");
    }
    TextureSubHeader* subHeader2 = this.m_SubHeader;
    ushort num1 = *(ushort*) ((IntPtr) subHeader2 + 2L);
    ushort num2 = *(ushort*) subHeader2;
    int num3 = (uint) num2 <= (uint) num1 ? (int) num2 : (int) num1;
    int num4 = 1;
    if (num3 > 4)
    {
      do
      {
        num3 >>= 1;
        ++num4;
      }
      while (num3 > 4);
    }
    *(sbyte*) ((IntPtr) subHeader2 + 17L) = (sbyte) num4;
    int num5 = 0;
    int _Y = 0;
    if (0 < num4)
    {
      double num6 = (double) (uint) *(int*) ((IntPtr) this.m_SubHeader + 20L);
      do
      {
        num5 = (int) (num6 / \u003CModule\u003E._Pow_int\u003Cdouble\u003E(4.0, _Y) + (double) num5);
        ++_Y;
      }
      while (_Y < num4);
    }
    ID3DXBuffer* id3DxBufferPtr1 = (ID3DXBuffer*) 0L;
    if (*(ushort*) ((IntPtr) this.m_SubHeader + 10L) == (ushort) 1)
    {
      long num7 = *(long*) this.m_ITextures;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      __calli((__FnPtr<void (IntPtr)>) *(long*) (*(long*) num7 + 128L /*0x80*/))((IntPtr) num7);
      \u003CModule\u003E.D3DXSaveTextureToFileInMemory(&id3DxBufferPtr1, (_D3DXIMAGE_FILEFORMAT) 4, (IDirect3DBaseTexture9*) *(long*) this.m_ITextures, (tagPALETTEENTRY*) 0L);
      ID3DXBuffer* id3DxBufferPtr2 = id3DxBufferPtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      byte* numPtr1 = (byte*) __calli((__FnPtr<void* (IntPtr)>) *(long*) (*(long*) id3DxBufferPtr2 + 24L))((IntPtr) id3DxBufferPtr2);
      ID3DXBuffer* id3DxBufferPtr3 = id3DxBufferPtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int num8 = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxBufferPtr3 + 32L /*0x20*/))((IntPtr) id3DxBufferPtr3);
      uint num9 = (uint) (num5 + 128 /*0x80*/);
      byte* numPtr2 = (byte*) \u003CModule\u003E.new\u005B\u005D((ulong) num9);
      uint num10 = num9;
      TextureSubHeader* subHeader3 = this.m_SubHeader;
      if (*(ushort*) ((IntPtr) subHeader3 + 12L) == (ushort) 32 /*0x20*/)
        LZO.CompressChunk(numPtr1 + 128L /*0x80*/, (uint) *(int*) ((IntPtr) subHeader3 + 20L), numPtr2, &num10, 3, true);
      else
        LZO.CompressChunk(numPtr1 + 128L /*0x80*/, (uint) *(int*) ((IntPtr) subHeader3 + 20L), numPtr2, &num10, 3);
      *(int*) ((IntPtr) this.m_SubHeader + 24L) = (int) num10;
      uint num11 = (uint) *(int*) ((IntPtr) this.m_SubHeader + 20L);
      // ISSUE: cpblk instruction
      __memcpy((long) num10 + (IntPtr) numPtr2, (IntPtr) (numPtr1 + (long) num11 + 128L /*0x80*/), (long) ((uint) num5 - num11));
      uint len = num10 + (uint) (num5 - *(int*) ((IntPtr) this.m_SubHeader + 20L));
      entry.SetData((sbyte*) numPtr2, len);
      \u003CModule\u003E.delete\u005B\u005D((void*) numPtr2);
      entry.SetSubHeader((sbyte*) this.m_SubHeader, 34U);
      ID3DXBuffer* id3DxBufferPtr4 = id3DxBufferPtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int num12 = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxBufferPtr4 + 16L /*0x10*/))((IntPtr) id3DxBufferPtr4);
    }
    else
    {
      long num13 = *(long*) this.m_ITextures;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      __calli((__FnPtr<void (IntPtr)>) *(long*) (*(long*) num13 + 128L /*0x80*/))((IntPtr) num13);
      \u003CModule\u003E.D3DXSaveTextureToFileInMemory(&id3DxBufferPtr1, (_D3DXIMAGE_FILEFORMAT) 4, (IDirect3DBaseTexture9*) *(long*) this.m_ITextures, (tagPALETTEENTRY*) 0L);
      ID3DXBuffer* id3DxBufferPtr5 = id3DxBufferPtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      byte* numPtr3 = (byte*) __calli((__FnPtr<void* (IntPtr)>) *(long*) (*(long*) id3DxBufferPtr5 + 24L))((IntPtr) id3DxBufferPtr5);
      byte* data = (byte*) \u003CModule\u003E.new\u005B\u005D((ulong) ((uint) *(ushort*) ((IntPtr) this.m_SubHeader + 10L) * (uint) num5));
      *(int*) ((IntPtr) this.m_SubHeader + 24L) = 0;
      ulong num14 = (ulong) (uint) num5;
      // ISSUE: cpblk instruction
      __memcpy((IntPtr) data, (IntPtr) (numPtr3 + 128L /*0x80*/), (long) num14);
      ID3DXBuffer* id3DxBufferPtr6 = id3DxBufferPtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int num15 = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxBufferPtr6 + 16L /*0x10*/))((IntPtr) id3DxBufferPtr6);
      int num16 = num5;
      int num17 = 1;
      if ((ushort) 1 < *(ushort*) ((IntPtr) this.m_SubHeader + 10L))
      {
        long num18 = 8;
        do
        {
          long num19 = *(long*) ((IntPtr) this.m_ITextures + num18);
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          __calli((__FnPtr<void (IntPtr)>) *(long*) (*(long*) num19 + 128L /*0x80*/))((IntPtr) num19);
          \u003CModule\u003E.D3DXSaveTextureToFileInMemory(&id3DxBufferPtr1, (_D3DXIMAGE_FILEFORMAT) 4, (IDirect3DBaseTexture9*) *(long*) ((IntPtr) this.m_ITextures + num18), (tagPALETTEENTRY*) 0L);
          ID3DXBuffer* id3DxBufferPtr7 = id3DxBufferPtr1;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          byte* numPtr4 = (byte*) __calli((__FnPtr<void* (IntPtr)>) *(long*) (*(long*) id3DxBufferPtr7 + 24L))((IntPtr) id3DxBufferPtr7);
          // ISSUE: cpblk instruction
          __memcpy((long) num16 + (IntPtr) data, (IntPtr) (numPtr4 + 128L /*0x80*/), (long) num14);
          ID3DXBuffer* id3DxBufferPtr8 = id3DxBufferPtr1;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          int num20 = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxBufferPtr8 + 16L /*0x10*/))((IntPtr) id3DxBufferPtr8);
          num16 += num5;
          ++num17;
          num18 += 8L;
        }
        while (num17 < (int) *(ushort*) ((IntPtr) this.m_SubHeader + 10L));
      }
      entry.SetData((sbyte*) data, (uint) *(ushort*) ((IntPtr) this.m_SubHeader + 10L) * (uint) num5);
      \u003CModule\u003E.delete\u005B\u005D((void*) data);
      entry.SetSubHeader((sbyte*) this.m_SubHeader, 34U);
    }
  }

  public unsafe void AddFrame(string filename)
  {
    FileStream File = File.OpenRead(filename);
    sbyte* pBuffer = File.CanRead ? (sbyte*) \u003CModule\u003E.new\u005B\u005D((ulong) File.Length) : throw new System.Exception("FableMod::Gfx::Integration:: Failed to read file");
    uint length = (uint) (int) File.Length;
    int num1 = (int) FileControl.Read(File, (void*) pBuffer, length);
    File.Close();
    TextureSubHeader* textureSubHeaderPtr = (TextureSubHeader*) ((IntPtr) this.m_SubHeader + 12L);
    TextureFormat textureFormat;
    if (*(ushort*) textureSubHeaderPtr == (ushort) 31 /*0x1F*/)
    {
      textureFormat = TextureFormat.DXT1;
    }
    else
    {
      *(short*) textureSubHeaderPtr = (short) 32 /*0x20*/;
      textureFormat = TextureFormat.DXT3;
    }
    long num2 = *(long*) ((IntPtr) \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA + 8L);
    IDirect3DTexture9* idirect3Dtexture9Ptr1 = (IDirect3DTexture9*) 0L;
    sbyte* numPtr = pBuffer;
    int num3 = (int) length;
    int num4 = (int) textureFormat;
    ref IDirect3DTexture9* local1 = ref idirect3Dtexture9Ptr1;
    if (\u003CModule\u003E.D3DXCreateTextureFromFileInMemoryEx((IDirect3DDevice9*) num2, (void*) numPtr, (uint) num3, 0U, 0U, 0U, 0U, (_D3DFORMAT) num4, (_D3DPOOL) 1, uint.MaxValue, uint.MaxValue, 0U, (_D3DXIMAGE_INFO*) 0L, (tagPALETTEENTRY*) 0L, (IDirect3DTexture9**) ref local1) < 0)
    {
      \u003CModule\u003E.delete\u005B\u005D((void*) pBuffer);
      throw new System.Exception("Failed to create texture");
    }
    \u003CModule\u003E.delete\u005B\u005D((void*) pBuffer);
    IDirect3DTexture9* idirect3Dtexture9Ptr2 = idirect3Dtexture9Ptr1;
    _D3DSURFACE_DESC d3DsurfaceDesc;
    ref _D3DSURFACE_DESC local2 = ref d3DsurfaceDesc;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num5 = __calli((__FnPtr<int (IntPtr, uint, _D3DSURFACE_DESC*)>) *(long*) (*(long*) idirect3Dtexture9Ptr1 + 136L))((IntPtr) idirect3Dtexture9Ptr2, 0U, (_D3DSURFACE_DESC*) ref local2);
    TextureSubHeader* subHeader = this.m_SubHeader;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    if ((int) *(ushort*) ((IntPtr) subHeader + 2L) == ^(int&) ((IntPtr) &d3DsurfaceDesc + 28) && (int) *(ushort*) subHeader == ^(int&) ((IntPtr) &d3DsurfaceDesc + 24))
    {
      *(short*) ((IntPtr) subHeader + 10L) = (short) ((int) *(ushort*) ((IntPtr) subHeader + 10L) + 1);
      IDirect3DTexture9** idirect3Dtexture9Ptr3 = (IDirect3DTexture9**) \u003CModule\u003E.new\u005B\u005D((ulong) *(ushort*) ((IntPtr) this.m_SubHeader + 10L) * 8UL);
      // ISSUE: cpblk instruction
      __memcpy((IntPtr) idirect3Dtexture9Ptr3, (IntPtr) this.m_ITextures, (long) ((int) *(ushort*) ((IntPtr) this.m_SubHeader + 10L) - 1) * 8L);
      \u003CModule\u003E.delete\u005B\u005D((void*) this.m_ITextures);
      this.m_ITextures = idirect3Dtexture9Ptr3;
      *(long*) ((IntPtr) idirect3Dtexture9Ptr3 + (long) *(ushort*) ((IntPtr) this.m_SubHeader + 10L) * 8L - 8L) = (long) idirect3Dtexture9Ptr1;
    }
    else
    {
      IDirect3DTexture9* idirect3Dtexture9Ptr4 = idirect3Dtexture9Ptr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int num6 = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) idirect3Dtexture9Ptr4 + 16L /*0x10*/))((IntPtr) idirect3Dtexture9Ptr4);
      throw new ArgumentException("Invalid texture size");
    }
  }

  public unsafe void Draw(Control control, Rectangle rect, int FrameIndex)
  {
    Device* mod1PeavDevice23Ea = \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA;
    \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002ELock(mod1PeavDevice23Ea);
    \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EBeginScene(mod1PeavDevice23Ea);
    \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EClearBuffers(mod1PeavDevice23Ea, 4294902015U);
    if (FrameIndex >= (int) *(ushort*) ((IntPtr) this.m_SubHeader + 10L) || FrameIndex < 0)
      FrameIndex = 0;
    long num1 = (long) FrameIndex * 8L;
    long num2 = *(long*) ((IntPtr) this.m_ITextures + num1);
    long num3 = num2;
    _D3DSURFACE_DESC d3DsurfaceDesc;
    ref _D3DSURFACE_DESC local1 = ref d3DsurfaceDesc;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num4 = __calli((__FnPtr<int (IntPtr, uint, _D3DSURFACE_DESC*)>) *(long*) (*(long*) num2 + 136L))((IntPtr) num3, 0U, (_D3DSURFACE_DESC*) ref local1);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    float num5 = (float) (uint) ^(int&) ((IntPtr) &d3DsurfaceDesc + 24);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    float num6 = (float) (uint) ^(int&) ((IntPtr) &d3DsurfaceDesc + 28);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    if ((uint) ^(int&) ((IntPtr) &d3DsurfaceDesc + 24) > (uint) rect.Width || (uint) ^(int&) ((IntPtr) &d3DsurfaceDesc + 28) > (uint) rect.Height)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      float num7 = (float) rect.Width / (float) (uint) ^(int&) ((IntPtr) &d3DsurfaceDesc + 24);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      float num8 = (float) (uint) ^(int&) ((IntPtr) &d3DsurfaceDesc + 28);
      float num9 = (float) rect.Height / num8;
      if ((double) num7 > 1.0)
        num7 = 1f;
      if ((double) num9 > 1.0)
        num9 = 1f;
      float num10 = (double) num7 >= (double) num9 ? num9 : num7;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      num5 = (float) (uint) ^(int&) ((IntPtr) &d3DsurfaceDesc + 24) * num10;
      num6 = num8 * num10;
    }
    D3DXMATRIX d3Dxmatrix;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    \u003CModule\u003E.D3DXMatrixScaling(&d3Dxmatrix, num5 / (float) (uint) ^(int&) ((IntPtr) &d3DsurfaceDesc + 24), num6 / (float) (uint) ^(int&) ((IntPtr) &d3DsurfaceDesc + 28), 1f);
    ID3DXSprite* isprite1 = this.m_ISprite;
    ID3DXSprite* id3DxSpritePtr1 = isprite1;
    ref D3DXMATRIX local2 = ref d3Dxmatrix;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num11 = __calli((__FnPtr<int (IntPtr, D3DXMATRIX*)>) *(long*) (*(long*) isprite1 + 40L))((IntPtr) id3DxSpritePtr1, (D3DXMATRIX*) ref local2);
    ID3DXSprite* isprite2 = this.m_ISprite;
    ID3DXSprite* id3DxSpritePtr2 = isprite2;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num12 = __calli((__FnPtr<int (IntPtr, uint)>) *(long*) (*(long*) isprite2 + 64L /*0x40*/))((IntPtr) id3DxSpritePtr2, 16U /*0x10*/);
    tagRECT tagRect1;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ref tagRect1 = 0;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &tagRect1 + 4) = 0;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &tagRect1 + 8) = rect.Width;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &tagRect1 + 12) = rect.Height;
    tagRECT tagRect2;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ref tagRect2 = rect.Left;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &tagRect2 + 4) = rect.Top;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &tagRect2 + 8) = rect.Width;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &tagRect2 + 12) = rect.Height;
    ID3DXSprite* isprite3 = this.m_ISprite;
    ID3DXSprite* id3DxSpritePtr3 = isprite3;
    long num13 = *(long*) ((IntPtr) this.m_ITextures + num1);
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num14 = __calli((__FnPtr<int (IntPtr, IDirect3DTexture9*, tagRECT*, D3DXVECTOR3*, D3DXVECTOR3*, uint)>) *(long*) (*(long*) isprite3 + 72L))((IntPtr) id3DxSpritePtr3, (IDirect3DTexture9*) num13, (tagRECT*) 0L, (D3DXVECTOR3*) 0L, (D3DXVECTOR3*) 0L, uint.MaxValue);
    ID3DXSprite* isprite4 = this.m_ISprite;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num15 = __calli((__FnPtr<int (IntPtr)>) *(long*) (*(long*) isprite4 + 88L))((IntPtr) isprite4);
    \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EEndScene(mod1PeavDevice23Ea);
    IntPtr handle = control.Handle;
    \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EPresent(mod1PeavDevice23Ea, (HWND__*) handle.ToPointer(), &tagRect1, &tagRect2);
    \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EUnlock(mod1PeavDevice23Ea);
  }

  public unsafe int Width => (int) *(ushort*) this.m_SubHeader;

  public unsafe int Height => (int) *(ushort*) ((IntPtr) this.m_SubHeader + 2L);

  public unsafe int Depth => (int) *(ushort*) ((IntPtr) this.m_SubHeader + 4L);

  public unsafe int FrameCount => (int) *(ushort*) ((IntPtr) this.m_SubHeader + 10L);

  public unsafe int FrameWidth
  {
    get => (int) *(ushort*) ((IntPtr) this.m_SubHeader + 6L);
    set
    {
      if (value <= 0)
        return;
      *(short*) ((IntPtr) this.m_SubHeader + 6L) = (short) value;
    }
  }

  public unsafe int FrameHeight
  {
    get => (int) *(ushort*) ((IntPtr) this.m_SubHeader + 8L);
    set
    {
      if (value <= 0)
        return;
      TextureSubHeader* subHeader = this.m_SubHeader;
      *(short*) ((IntPtr) subHeader + 8L) = (short) *(ushort*) ((IntPtr) subHeader + 2L);
    }
  }

  public unsafe byte AlphaChannels
  {
    get => *(byte*) ((IntPtr) this.m_SubHeader + 16L /*0x10*/);
    set => *(sbyte*) ((IntPtr) this.m_SubHeader + 16L /*0x10*/) = (sbyte) value;
  }

  public unsafe TextureFormat Format
  {
    get
    {
      return *(ushort*) ((IntPtr) this.m_SubHeader + 12L) != (ushort) 31 /*0x1F*/ ? TextureFormat.DXT3 : TextureFormat.DXT1;
    }
  }

  public unsafe Texture* GetBaseTexture()
  {
    return (Texture*) *(long*) ((IntPtr) this.m_pBaseTexture + 8L);
  }

  public unsafe IDirect3DTexture9* GetBaseD3DTexture()
  {
    return (IDirect3DTexture9*) *(long*) this.m_ITextures;
  }

  [HandleProcessCorruptedStateExceptions]
  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EGfxTexture();
    }
    else
    {
      try
      {
        this.\u0021GfxTexture();
      }
      finally
      {
        // ISSUE: explicit finalizer call
        base.Finalize();
      }
    }
  }

  public virtual void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }

  ~GfxTexture() => this.Dispose(false);
}
