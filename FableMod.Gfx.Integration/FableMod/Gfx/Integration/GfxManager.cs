// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.GfxManager
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Gfx.Integration;

public class GfxManager
{
  private static TextureManager m_TextureManager;
  private static ModelManager m_ModelManager;
  private static ThemeManager m_ThemeManager;
  private static unsafe Node* m_pRoot;

  public static unsafe void Initialize(Form form, int width, int height)
  {
    // ISSUE: untyped stack allocation
    long num1 = (long) __untypedstackalloc(\u003CModule\u003E.__CxxQueryExceptionSize());
    FableMod.Gfx.Exception* exceptionPtr;
    try
    {
      \u003CModule\u003E.FableMod\u002EGfx\u002EManager\u002EInitialize((HWND__*) form.Handle.ToPointer(), (uint) width, (uint) height);
    }
    catch (System.Exception ex1) when (\u003CModule\u003E.__CxxExceptionFilter((void*) Marshal.GetExceptionPointers(), (void*) &\u003CModule\u003E.\u003F\u003F_R0\u003FAVException\u0040Gfx\u0040FableMod\u0040\u0040\u00408, 8, (void*) &exceptionPtr) != 0)
    {
      uint num2 = 0;
      \u003CModule\u003E.__CxxRegisterExceptionObject((void*) Marshal.GetExceptionPointers(), (void*) num1);
      try
      {
        try
        {
          throw new System.Exception(new string(\u003CModule\u003E.FableMod\u002EGfx\u002EException\u002EGetMsg(exceptionPtr)));
        }
        catch (System.Exception ex2) when (
        {
          // ISSUE: unable to correctly present filter
          num2 = (uint) \u003CModule\u003E.__CxxDetectRethrow((void*) Marshal.GetExceptionPointers());
          if (num2 != 0U)
          {
            SuccessfulFiltering;
          }
          else
            throw;
        }
        )
        {
        }
        if (num2 != 0U)
          throw;
      }
      finally
      {
        \u003CModule\u003E.__CxxUnregisterExceptionObject((void*) num1, (int) num2);
      }
    }
    GfxManager.m_ModelManager = new ModelManager();
    GfxManager.m_TextureManager = new TextureManager();
    GfxManager.m_ThemeManager = new ThemeManager(GfxManager.m_TextureManager, GfxManager.m_ModelManager);
  }

  public static unsafe void SaveScreenToFile(string fileName)
  {
    Device* mod1PeavDevice23Ea = \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA;
    \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002ELock(mod1PeavDevice23Ea);
    long num1 = *(long*) ((IntPtr) mod1PeavDevice23Ea + 8L);
    long num2 = num1;
    IDirect3DSurface9* idirect3Dsurface9Ptr1;
    ref IDirect3DSurface9* local = ref idirect3Dsurface9Ptr1;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    if (__calli((__FnPtr<int (IntPtr, uint, uint, _D3DBACKBUFFER_TYPE, IDirect3DSurface9**)>) *(long*) (*(long*) num1 + 144L /*0x90*/))((IntPtr) num2, 0U, 0U, (_D3DBACKBUFFER_TYPE) 0, (IDirect3DSurface9**) ref local) < 0)
    {
      \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EUnlock(mod1PeavDevice23Ea);
      throw new System.Exception("Failed to create surface.");
    }
    IntPtr hglobalUni = Marshal.StringToHGlobalUni(fileName);
    string lower = Path.GetExtension(fileName).ToLower();
    if (!(lower == "png") && !(lower == "DDS"))
    {
      int num3 = lower == "BMP" ? 1 : 0;
    }
    if (\u003CModule\u003E.D3DXSaveSurfaceToFileW((char*) hglobalUni.ToPointer(), (_D3DXIMAGE_FILEFORMAT) 1, idirect3Dsurface9Ptr1, (tagPALETTEENTRY*) 0L, (tagRECT*) 0L) < 0)
    {
      \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EUnlock(mod1PeavDevice23Ea);
      IDirect3DSurface9* idirect3Dsurface9Ptr2 = idirect3Dsurface9Ptr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int num4 = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) idirect3Dsurface9Ptr2 + 16L /*0x10*/))((IntPtr) idirect3Dsurface9Ptr2);
      Marshal.FreeHGlobal(hglobalUni);
      throw new System.Exception("Failed to save surface.");
    }
    Marshal.FreeHGlobal(hglobalUni);
    IDirect3DSurface9* idirect3Dsurface9Ptr3 = idirect3Dsurface9Ptr1;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num5 = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) idirect3Dsurface9Ptr3 + 16L /*0x10*/))((IntPtr) idirect3Dsurface9Ptr3);
    \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EUnlock(mod1PeavDevice23Ea);
  }

  public static void Destroy()
  {
    if (GfxManager.m_TextureManager != null)
    {
      GfxManager.m_TextureManager?.Dispose();
      GfxManager.m_TextureManager = (TextureManager) null;
    }
    if (GfxManager.m_ModelManager != null)
    {
      GfxManager.m_ModelManager?.Dispose();
      GfxManager.m_ModelManager = (ModelManager) null;
    }
    if (GfxManager.m_ThemeManager != null)
    {
      GfxManager.m_ThemeManager?.Dispose();
      GfxManager.m_ThemeManager = (ThemeManager) null;
    }
    \u003CModule\u003E.FableMod\u002EGfx\u002EManager\u002EDispose();
  }

  public static unsafe void SetDirectory(string directory)
  {
    if (!directory.EndsWith("\\"))
      directory += "\\";
    IntPtr hglobalUni = Marshal.StringToHGlobalUni(directory);
    \u003CModule\u003E.FableMod\u002EGfx\u002EManager\u002ESetDirectory((char*) hglobalUni.ToPointer());
    Marshal.FreeHGlobal(hglobalUni);
    directory += "FableMod.Gfx.Log";
    hglobalUni = Marshal.StringToHGlobalUni(directory);
    \u003CModule\u003E.FableMod\u002ELogFile\u002EOpen(\u003CModule\u003E.FableMod\u002ELogFile\u002EGetInstance(), (char*) hglobalUni.ToPointer());
    Marshal.FreeHGlobal(hglobalUni);
  }

  public static TextureManager GetTextureManager() => GfxManager.m_TextureManager;

  public static ModelManager GetModelManager() => GfxManager.m_ModelManager;

  public static ThemeManager GetThemeManager() => GfxManager.m_ThemeManager;

  private GfxManager()
  {
  }
}
