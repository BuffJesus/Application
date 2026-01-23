// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.Map
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using FableMod.LEV;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.Gfx.Integration;

public class Map : IDisposable
{
  protected LEVFile m_Lev;
  protected string m_Name;
  protected unsafe SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* m_pTerrain = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E*) 0L;

  private void \u007EMap() => this.Destroy();

  public unsafe void Create(
    GfxController controller,
    string name,
    int mapX,
    int mapY,
    LEVFile lev)
  {
    this.m_Name = name;
    this.m_Lev = lev;
    if (lev == null)
      return;
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* fableModGfxTerrainPtr1 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* fableModGfxTerrainPtr2;
    // ISSUE: fault handler
    try
    {
      if ((IntPtr) fableModGfxTerrainPtr1 != IntPtr.Zero)
      {
        Terrain* terrainPtr1 = (Terrain*) \u003CModule\u003E.@new(552UL);
        Terrain* terrainPtr2;
        // ISSUE: fault handler
        try
        {
          terrainPtr2 = (IntPtr) terrainPtr1 == IntPtr.Zero ? (Terrain*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002ETerrain\u002E\u007Bctor\u007D(terrainPtr1);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) terrainPtr1);
        }
        *(long*) fableModGfxTerrainPtr1 = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VTerrain\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
        *(long*) ((IntPtr) fableModGfxTerrainPtr1 + 8L) = (long) terrainPtr2;
        if ((IntPtr) terrainPtr2 != IntPtr.Zero)
          *(int*) ((IntPtr) terrainPtr2 + 8L) = *(int*) ((IntPtr) terrainPtr2 + 8L) + 1;
        fableModGfxTerrainPtr2 = fableModGfxTerrainPtr1;
      }
      else
        fableModGfxTerrainPtr2 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E*) 0L;
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) fableModGfxTerrainPtr1);
    }
    this.m_pTerrain = fableModGfxTerrainPtr2;
    IntPtr hglobalUni = Marshal.StringToHGlobalUni(name);
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain1 = this.m_pTerrain;
    \u003CModule\u003E.FableMod\u002EGfx\u002ERootObject\u002ESetName((IntPtr) pTerrain1 == IntPtr.Zero ? (RootObject*) 0L : (RootObject*) *(long*) ((IntPtr) pTerrain1 + 8L), (char*) hglobalUni.ToPointer());
    Marshal.FreeHGlobal(hglobalUni);
    int num1 = lev.Width + 1;
    ulong num2 = (ulong) ((lev.Height + 1) * num1);
    float* numPtr = (float*) \u003CModule\u003E.new\u005B\u005D(num2 > 4611686018427387903UL /*0x3FFFFFFFFFFFFFFF*/ ? ulong.MaxValue : num2 * 4UL);
    int x = 0;
    long num3 = 0;
    if (0 <= lev.Width)
    {
      do
      {
        int y = 0;
        long num4 = 0;
        if (0 <= lev.Height)
        {
          do
          {
            *(float*) ((((long) lev.Width + 1L) * num4 + num3) * 4L + (IntPtr) numPtr) = lev.get_Heights(x, y);
            ++y;
            ++num4;
          }
          while (y <= lev.Height);
        }
        ++x;
        ++num3;
      }
      while (x <= lev.Width);
    }
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain2 = this.m_pTerrain;
    \u003CModule\u003E.FableMod\u002EGfx\u002ETerrain\u002ECreate((IntPtr) pTerrain2 == IntPtr.Zero ? (Terrain*) 0L : (Terrain*) *(long*) ((IntPtr) pTerrain2 + 8L), numPtr, (uint) lev.Width, (uint) lev.Height);
    \u003CModule\u003E.delete\u005B\u005D((void*) numPtr);
    Node* root = controller.GetRoot();
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain3 = this.m_pTerrain;
    Terrain* terrainPtr3 = (IntPtr) pTerrain3 == IntPtr.Zero ? (Terrain*) 0L : (Terrain*) *(long*) ((IntPtr) pTerrain3 + 8L);
    Node* nodePtr = root;
    Terrain* terrainPtr4 = terrainPtr3;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) root + 120L))((IntPtr) nodePtr, (Spatial*) terrainPtr4);
    float num5 = \u003CModule\u003E.FableMod\u002EGfx\u002ESettings\u002EGetFloat(\u003CModule\u003E.FableMod\u002EGfx\u002ESettings\u002EGetInstance(), (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1BA\u0040NFNMOFEE\u0040\u003F\u0024AAT\u003F\u0024AAe\u003F\u0024AAr\u003F\u0024AAr\u003F\u0024AAa\u003F\u0024AAi\u003F\u0024AAn\u003F\u0024AA\u003F\u0024AA\u0040, (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1BA\u0040MKDGPOGN\u0040\u003F\u0024AAS\u003F\u0024AAp\u003F\u0024AAa\u003F\u0024AAc\u003F\u0024AAi\u003F\u0024AAn\u003F\u0024AAg\u003F\u0024AA\u003F\u0024AA\u0040, 1f);
    D3DXVECTOR3 d3DxvectoR3;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3DxvectoR3 = (float) mapX * num5;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3 + 4) = (float) mapY * num5;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3 + 8) = 0.0f;
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain4 = this.m_pTerrain;
    Terrain* terrainPtr5 = (IntPtr) pTerrain4 == IntPtr.Zero ? (Terrain*) 0L : (Terrain*) *(long*) ((IntPtr) pTerrain4 + 8L);
    *(int*) ((IntPtr) terrainPtr5 + 32L /*0x20*/) = *(int*) ((IntPtr) terrainPtr5 + 32L /*0x20*/) | 1;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) terrainPtr5 + 100L, ref d3DxvectoR3, 12);
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain5 = this.m_pTerrain;
    long num6 = (IntPtr) pTerrain5 == IntPtr.Zero ? 0L : *(long*) ((IntPtr) pTerrain5 + 8L);
    long num7 = num6;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, byte)>) *(long*) (*(long*) num6 + 16L /*0x10*/))((IntPtr) num7, (byte) 1);
    this.CreateTerrainTexture(controller);
  }

  public virtual unsafe void Destroy()
  {
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain = this.m_pTerrain;
    if ((IntPtr) pTerrain != IntPtr.Zero)
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* fableModGfxTerrainPtr1 = pTerrain;
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* fableModGfxTerrainPtr2 = fableModGfxTerrainPtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) fableModGfxTerrainPtr1)((IntPtr) fableModGfxTerrainPtr2, 1U);
      this.m_pTerrain = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E*) 0L;
    }
    this.m_Name = (string) null;
    LEVFile lev = this.m_Lev;
    if (lev == null)
      return;
    lev.Destroy();
    this.m_Lev = (LEVFile) null;
  }

  public unsafe void Highlight([MarshalAs(UnmanagedType.U1)] bool bHighlight)
  {
    if (bHighlight)
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain = this.m_pTerrain;
      Terrain* terrainPtr = (IntPtr) pTerrain == IntPtr.Zero ? (Terrain*) 0L : (Terrain*) *(long*) ((IntPtr) pTerrain + 8L);
      *(int*) ((IntPtr) terrainPtr + 320L) = *(int*) ((IntPtr) terrainPtr + 320L) | 32 /*0x20*/;
    }
    else
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain = this.m_pTerrain;
      Terrain* terrainPtr = (IntPtr) pTerrain == IntPtr.Zero ? (Terrain*) 0L : (Terrain*) *(long*) ((IntPtr) pTerrain + 8L);
      *(int*) ((IntPtr) terrainPtr + 320L) = *(int*) ((IntPtr) terrainPtr + 320L) & -33;
    }
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public unsafe bool IsOver(float x, float y, float z)
  {
    D3DXVECTOR3 d3DxvectoR3;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3DxvectoR3 = x;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3 + 4) = y;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3 + 8) = z;
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain = this.m_pTerrain;
    return \u003CModule\u003E.FableMod\u002EGfx\u002ETerrain\u002EIsOver((IntPtr) pTerrain == IntPtr.Zero ? (Terrain*) 0L : (Terrain*) *(long*) ((IntPtr) pTerrain + 8L), &d3DxvectoR3);
  }

  public LEVFile LEV => this.m_Lev;

  public unsafe float X
  {
    get
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain = this.m_pTerrain;
      if ((IntPtr) pTerrain != IntPtr.Zero)
      {
        long num = *(long*) ((IntPtr) pTerrain + 8L);
        if (num != 0L)
          return *(float*) (num + 180L);
      }
      return 0.0f;
    }
  }

  public unsafe float Y
  {
    get
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain = this.m_pTerrain;
      if ((IntPtr) pTerrain != IntPtr.Zero)
      {
        long num = *(long*) ((IntPtr) pTerrain + 8L);
        if (num != 0L)
          return *(float*) (num + 184L);
      }
      return 0.0f;
    }
  }

  public string Name => this.m_Name;

  public virtual bool Modified
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_Lev.Modified;
  }

  public unsafe Terrain* GetTerrain()
  {
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain = this.m_pTerrain;
    return (IntPtr) pTerrain != IntPtr.Zero ? (Terrain*) *(long*) ((IntPtr) pTerrain + 8L) : (Terrain*) 0L;
  }

  public unsafe void CreateWalkTexture()
  {
    EditableTexture* editableTexturePtr1 = (EditableTexture*) \u003CModule\u003E.@new(96UL /*0x60*/);
    EditableTexture* editableTexturePtr2;
    // ISSUE: fault handler
    try
    {
      editableTexturePtr2 = (IntPtr) editableTexturePtr1 == IntPtr.Zero ? (EditableTexture*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002E\u007Bctor\u007D(editableTexturePtr1);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) editableTexturePtr1);
    }
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AEditableTexture\u003E gfxEditableTexture;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ref gfxEditableTexture = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VEditableTexture\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ((IntPtr) &gfxEditableTexture + 8) = (long) editableTexturePtr2;
    if ((IntPtr) editableTexturePtr2 != IntPtr.Zero)
      *(int*) ((IntPtr) editableTexturePtr2 + 8L) = *(int*) ((IntPtr) editableTexturePtr2 + 8L) + 1;
    // ISSUE: fault handler
    try
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain1 = this.m_pTerrain;
      Terrain* terrainPtr1 = (IntPtr) pTerrain1 == IntPtr.Zero ? (Terrain*) 0L : (Terrain*) *(long*) ((IntPtr) pTerrain1 + 8L);
      Terrain* terrainPtr2 = (IntPtr) pTerrain1 == IntPtr.Zero ? (Terrain*) 0L : (Terrain*) *(long*) ((IntPtr) pTerrain1 + 8L);
      \u003CModule\u003E.FableMod\u002EGfx\u002ETexture\u002ECreate((Texture*) editableTexturePtr2, \u003CModule\u003E.FableMod\u002EGfx\u002ETerrain\u002EGetTextureSize(terrainPtr2, (byte) 1), \u003CModule\u003E.FableMod\u002EGfx\u002ETerrain\u002EGetTextureSize(terrainPtr1, (byte) 1), 1U, false, false);
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain2 = this.m_pTerrain;
      \u003CModule\u003E.FableMod\u002EGfx\u002ETerrain\u002ESetTexture((IntPtr) pTerrain2 == IntPtr.Zero ? (Terrain*) 0L : (Terrain*) *(long*) ((IntPtr) pTerrain2 + 8L), 1U, editableTexturePtr2);
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain3 = this.m_pTerrain;
      uint num1 = (uint) *(int*) (((IntPtr) pTerrain3 == IntPtr.Zero ? 0L : *(long*) ((IntPtr) pTerrain3 + 8L)) + 396L);
      uint num2 = (uint) *(int*) (((IntPtr) pTerrain3 == IntPtr.Zero ? 0L : *(long*) ((IntPtr) pTerrain3 + 8L)) + 392L);
      D3DXVECTOR2 d3DxvectoR2;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ref d3DxvectoR2 = (float) num2;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3DxvectoR2 + 4) = (float) num1;
      \u003CModule\u003E.FableMod\u002EGfx\u002ETerrain\u002ESetTextureSpacing((IntPtr) pTerrain3 == IntPtr.Zero ? (Terrain*) 0L : (Terrain*) *(long*) ((IntPtr) pTerrain3 + 8L), (byte) 1, &d3DxvectoR2, editableTexturePtr2);
      \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002ELock(editableTexturePtr2);
      int x = 0;
      if (0 < this.m_Lev.Width)
      {
        do
        {
          int y = 0;
          if (0 < this.m_Lev.Height)
          {
            do
            {
              if (this.m_Lev.get_Walks(x, y))
                \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002EDrawRawPixel(editableTexturePtr2, x, y, 4286085250U);
              else
                \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002EDrawRawPixel(editableTexturePtr2, x, y, 4278190080U /*0xFF000000*/);
              ++y;
            }
            while (y < this.m_Lev.Height);
          }
          ++x;
        }
        while (x < this.m_Lev.Width);
      }
      \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002EUnlock(editableTexturePtr2);
    }
    __fault
    {
      // ISSUE: method pointer
      // ISSUE: cast to a function pointer type
      \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AEditableTexture\u003E\u002E\u007Bdtor\u007D), (void*) &gfxEditableTexture);
    }
    if ((IntPtr) editableTexturePtr2 == IntPtr.Zero)
      return;
    uint num = (uint) *(int*) ((IntPtr) editableTexturePtr2 + 8L);
    if (num > 0U)
      *(int*) ((IntPtr) editableTexturePtr2 + 8L) = (int) num - 1;
    if (*(int*) ((IntPtr) editableTexturePtr2 + 8L) != 0)
      return;
    EditableTexture* editableTexturePtr3 = editableTexturePtr2;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) editableTexturePtr2)((IntPtr) editableTexturePtr3, 1U);
  }

  public unsafe void FinishWalkTexture()
  {
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain1 = this.m_pTerrain;
    EditableTexture* editableTexturePtr1 = (EditableTexture*) *(long*) (((IntPtr) pTerrain1 == IntPtr.Zero ? 0L : *(long*) ((IntPtr) pTerrain1 + 8L)) + 384L);
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AEditableTexture\u003E gfxEditableTexture;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ref gfxEditableTexture = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VEditableTexture\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ((IntPtr) &gfxEditableTexture + 8) = (long) editableTexturePtr1;
    if ((IntPtr) editableTexturePtr1 != IntPtr.Zero)
      *(int*) ((IntPtr) editableTexturePtr1 + 8L) = *(int*) ((IntPtr) editableTexturePtr1 + 8L) + 1;
    // ISSUE: fault handler
    try
    {
      if ((IntPtr) editableTexturePtr1 != IntPtr.Zero)
      {
        \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002ELock(editableTexturePtr1);
        int x = 0;
        if (0 < this.m_Lev.Width)
        {
          do
          {
            int y = 0;
            if (0 < this.m_Lev.Height)
            {
              do
              {
                byte num = (byte) (\u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002EGetRawPixel(editableTexturePtr1, x, y) == 4286085250U);
                this.m_Lev.set_Walks(x, y, (bool) num);
                ++y;
              }
              while (y < this.m_Lev.Height);
            }
            ++x;
          }
          while (x < this.m_Lev.Width);
        }
        \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002EUnlock(editableTexturePtr1);
        SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain2 = this.m_pTerrain;
        \u003CModule\u003E.FableMod\u002EGfx\u002ETerrain\u002ESetTexture((IntPtr) pTerrain2 == IntPtr.Zero ? (Terrain*) 0L : (Terrain*) *(long*) ((IntPtr) pTerrain2 + 8L), 1U, (EditableTexture*) 0L);
      }
    }
    __fault
    {
      // ISSUE: method pointer
      // ISSUE: cast to a function pointer type
      \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AEditableTexture\u003E\u002E\u007Bdtor\u007D), (void*) &gfxEditableTexture);
    }
    if ((IntPtr) editableTexturePtr1 == IntPtr.Zero)
      return;
    uint num1 = (uint) *(int*) ((IntPtr) editableTexturePtr1 + 8L);
    if (num1 > 0U)
      *(int*) ((IntPtr) editableTexturePtr1 + 8L) = (int) num1 - 1;
    if (*(int*) ((IntPtr) editableTexturePtr1 + 8L) != 0)
      return;
    EditableTexture* editableTexturePtr2 = editableTexturePtr1;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) editableTexturePtr1)((IntPtr) editableTexturePtr2, 1U);
  }

  public unsafe void CreateNavTexture(string sectionName, int subset)
  {
    Console.WriteLine("Map::CreateNavTexture({0}, {1})", (object) sectionName, (object) subset);
    NavSection navSection = this.m_Lev.get_Sections(sectionName);
    if (navSection == null)
      return;
    Grid grid1 = navSection.GetGrid(subset);
    if (grid1 == null)
      return;
    EditableTexture* editableTexturePtr1 = (EditableTexture*) \u003CModule\u003E.@new(96UL /*0x60*/);
    EditableTexture* editableTexturePtr2;
    // ISSUE: fault handler
    try
    {
      editableTexturePtr2 = (IntPtr) editableTexturePtr1 == IntPtr.Zero ? (EditableTexture*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002E\u007Bctor\u007D(editableTexturePtr1);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) editableTexturePtr1);
    }
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AEditableTexture\u003E gfxEditableTexture;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ref gfxEditableTexture = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VEditableTexture\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ((IntPtr) &gfxEditableTexture + 8) = (long) editableTexturePtr2;
    if ((IntPtr) editableTexturePtr2 != IntPtr.Zero)
      *(int*) ((IntPtr) editableTexturePtr2 + 8L) = *(int*) ((IntPtr) editableTexturePtr2 + 8L) + 1;
    // ISSUE: fault handler
    try
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain1 = this.m_pTerrain;
      uint textureSize = \u003CModule\u003E.FableMod\u002EGfx\u002ETerrain\u002EGetTextureSize((IntPtr) pTerrain1 == IntPtr.Zero ? (Terrain*) 0L : (Terrain*) *(long*) ((IntPtr) pTerrain1 + 8L), (byte) 2);
      EditableTexture* editableTexturePtr3 = editableTexturePtr2;
      int num1 = (int) textureSize;
      \u003CModule\u003E.FableMod\u002EGfx\u002ETexture\u002ECreate((Texture*) editableTexturePtr3, (uint) num1, (uint) num1, 1U, false, true);
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain2 = this.m_pTerrain;
      \u003CModule\u003E.FableMod\u002EGfx\u002ETerrain\u002ESetTexture((IntPtr) pTerrain2 == IntPtr.Zero ? (Terrain*) 0L : (Terrain*) *(long*) ((IntPtr) pTerrain2 + 8L), 1U, editableTexturePtr2);
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain3 = this.m_pTerrain;
      uint num2 = (uint) *(int*) (((IntPtr) pTerrain3 == IntPtr.Zero ? 0L : *(long*) ((IntPtr) pTerrain3 + 8L)) + 396L);
      uint num3 = (uint) *(int*) (((IntPtr) pTerrain3 == IntPtr.Zero ? 0L : *(long*) ((IntPtr) pTerrain3 + 8L)) + 392L);
      D3DXVECTOR2 d3DxvectoR2;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ref d3DxvectoR2 = (float) (num3 * 2U);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3DxvectoR2 + 4) = (float) (num2 * 2U);
      \u003CModule\u003E.FableMod\u002EGfx\u002ETerrain\u002ESetTextureSpacing((IntPtr) pTerrain3 == IntPtr.Zero ? (Terrain*) 0L : (Terrain*) *(long*) ((IntPtr) pTerrain3 + 8L), (byte) 1, &d3DxvectoR2, editableTexturePtr2);
      \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002ELock(editableTexturePtr2);
      int num4 = 0;
      if (0 < grid1.Width)
      {
        int num5 = -1;
        do
        {
          int iY = 0;
          if (0 < grid1.Height)
          {
            do
            {
              Grid grid2 = grid1;
              GridCell cellAt = grid2.GetCellAt(grid2.Width + num5, iY);
              uint num6 = 4278190080 /*0xFF000000*/;
              byte num7 = cellAt.Value;
              uint num8 = (byte) ((uint) num7 & 4U) == (byte) 0 ? ((byte) ((uint) num7 & 1U) == (byte) 0 ? ((byte) ((uint) num7 & 2U) == (byte) 0 ? (num7 > (byte) 0 ? 4294901760U : num6) : ((byte) ((uint) num7 & 8U) == (byte) 0 ? ((byte) ((uint) num7 & 16U /*0x10*/) != (byte) 0 ? 4294612480U : 4278219730U) : 4278205568U)) : ((byte) ((uint) num7 & 8U) == (byte) 0 ? ((byte) ((uint) num7 & 16U /*0x10*/) != (byte) 0 ? 4293651435U : 4286611584U) : 4290032820U)) : 4284794980U;
              \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002EDrawRawPixel(editableTexturePtr2, num4, iY, num8);
              ++iY;
            }
            while (iY < grid1.Height);
          }
          ++num4;
          num5 += -1;
        }
        while (num4 < grid1.Width);
      }
      \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002EUnlock(editableTexturePtr2);
    }
    __fault
    {
      // ISSUE: method pointer
      // ISSUE: cast to a function pointer type
      \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AEditableTexture\u003E\u002E\u007Bdtor\u007D), (void*) &gfxEditableTexture);
    }
    if ((IntPtr) editableTexturePtr2 == IntPtr.Zero)
      return;
    uint num = (uint) *(int*) ((IntPtr) editableTexturePtr2 + 8L);
    if (num > 0U)
      *(int*) ((IntPtr) editableTexturePtr2 + 8L) = (int) num - 1;
    if (*(int*) ((IntPtr) editableTexturePtr2 + 8L) != 0)
      return;
    EditableTexture* editableTexturePtr4 = editableTexturePtr2;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) editableTexturePtr2)((IntPtr) editableTexturePtr4, 1U);
  }

  public unsafe void FinishNavTexture(string sectionName, int subset)
  {
    NavSection navSection = this.m_Lev.get_Sections(sectionName);
    if (navSection == null)
      return;
    Grid grid1 = navSection.GetGrid(subset);
    if (grid1 == null)
      return;
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain1 = this.m_pTerrain;
    EditableTexture* editableTexturePtr1 = (EditableTexture*) *(long*) (((IntPtr) pTerrain1 == IntPtr.Zero ? 0L : *(long*) ((IntPtr) pTerrain1 + 8L)) + 384L);
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AEditableTexture\u003E gfxEditableTexture;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ref gfxEditableTexture = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VEditableTexture\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ((IntPtr) &gfxEditableTexture + 8) = (long) editableTexturePtr1;
    if ((IntPtr) editableTexturePtr1 != IntPtr.Zero)
      *(int*) ((IntPtr) editableTexturePtr1 + 8L) = *(int*) ((IntPtr) editableTexturePtr1 + 8L) + 1;
    // ISSUE: fault handler
    try
    {
      if ((IntPtr) editableTexturePtr1 != IntPtr.Zero)
      {
        \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002ELock(editableTexturePtr1);
        int num1 = 0;
        if (0 < grid1.Width)
        {
          int num2 = -1;
          do
          {
            int iY = 0;
            if (0 < grid1.Height)
            {
              do
              {
                Grid grid2 = grid1;
                GridCell cellAt = grid2.GetCellAt(grid2.Width + num2, iY);
                uint rawPixel = \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002EGetRawPixel(editableTexturePtr1, num1, iY);
                byte num3 = 0;
                byte num4;
                int num5;
                switch (rawPixel)
                {
                  case 4278219730:
                    num5 = 2;
                    break;
                  case 4286611584:
                    num4 = (byte) 1;
                    goto label_14;
                  default:
                    num5 = (int) num3;
                    break;
                }
                num4 = (byte) num5;
label_14:
                int num6 = (int) num4;
                cellAt.Value = (byte) num6;
                ++iY;
              }
              while (iY < grid1.Height);
            }
            ++num1;
            num2 += -1;
          }
          while (num1 < grid1.Width);
        }
        \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002EUnlock(editableTexturePtr1);
        SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain2 = this.m_pTerrain;
        \u003CModule\u003E.FableMod\u002EGfx\u002ETerrain\u002ESetTexture((IntPtr) pTerrain2 == IntPtr.Zero ? (Terrain*) 0L : (Terrain*) *(long*) ((IntPtr) pTerrain2 + 8L), 1U, (EditableTexture*) 0L);
      }
    }
    __fault
    {
      // ISSUE: method pointer
      // ISSUE: cast to a function pointer type
      \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AEditableTexture\u003E\u002E\u007Bdtor\u007D), (void*) &gfxEditableTexture);
    }
    if ((IntPtr) editableTexturePtr1 == IntPtr.Zero)
      return;
    uint num = (uint) *(int*) ((IntPtr) editableTexturePtr1 + 8L);
    if (num > 0U)
      *(int*) ((IntPtr) editableTexturePtr1 + 8L) = (int) num - 1;
    if (*(int*) ((IntPtr) editableTexturePtr1 + 8L) != 0)
      return;
    EditableTexture* editableTexturePtr2 = editableTexturePtr1;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) editableTexturePtr1)((IntPtr) editableTexturePtr2, 1U);
  }

  protected unsafe void CreateTerrainTexture(GfxController controller)
  {
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATerrain\u003E* pTerrain = this.m_pTerrain;
    EditableTexture* editableTexturePtr = (EditableTexture*) *(long*) (((IntPtr) pTerrain == IntPtr.Zero ? 0L : *(long*) ((IntPtr) pTerrain + 8L)) + 368L);
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AEditableTexture\u003E gfxEditableTexture;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ref gfxEditableTexture = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VEditableTexture\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ((IntPtr) &gfxEditableTexture + 8) = (long) editableTexturePtr;
    if ((IntPtr) editableTexturePtr != IntPtr.Zero)
      *(int*) ((IntPtr) editableTexturePtr + 8L) = *(int*) ((IntPtr) editableTexturePtr + 8L) + 1;
    // ISSUE: fault handler
    try
    {
      ID3DXRenderToSurface* dxRenderToSurfacePtr1 = (ID3DXRenderToSurface*) 0L;
      if (\u003CModule\u003E.D3DXCreateRenderToSurface((IDirect3DDevice9*) *(long*) ((IntPtr) \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA + 8L), \u003CModule\u003E.FableMod\u002EGfx\u002ETexture\u002EGetWidth((Texture*) editableTexturePtr, 0U), \u003CModule\u003E.FableMod\u002EGfx\u002ETexture\u002EGetHeight((Texture*) editableTexturePtr, 0U), \u003CModule\u003E.FableMod\u002EGfx\u002ETexture\u002EGetFormat((Texture*) editableTexturePtr, 0U), 0, (_D3DFORMAT) 75, &dxRenderToSurfacePtr1) < 0)
      {
        \u003CModule\u003E.FableMod\u002ELogFile\u002EWrite(\u003CModule\u003E.FableMod\u002ELogFile\u002EGetInstance(), (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CM\u0040OKAGFLBM\u0040\u003F\u0024AA\u003F\u0024CI\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAs\u003F\u0024AA\u003F3\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAd\u003F\u0024AA\u003F\u0024CJ\u003F\u0024AA\u003F3\u003F\u0024AA\u003F5\u003F\u0024AAD\u003F\u0024AAX\u003F\u0024AAE\u003F\u0024AAR\u003F\u0024AAR\u003F\u0024AA\u003F3\u003F\u0024AA\u003F5\u003F\u0024AA\u003F\u0024CC\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAs\u003F\u0024AA\u003F\u0024CC\u003F\u0024AA\u003F6\u003F\u0024AA\u003F\u0024AA\u0040, __arglist (out \u003CModule\u003E.\u003F\u003F_C\u0040_1BA\u0040GOAFNGME\u0040\u003F\u0024AAM\u003F\u0024AAa\u003F\u0024AAp\u003F\u0024AA\u003F4\u003F\u0024AAc\u003F\u0024AAp\u003F\u0024AAp\u003F\u0024AA\u003F\u0024AA\u0040, 404, out \u003CModule\u003E.\u003F\u003F_C\u0040_1BGA\u0040EAHBCOAN\u0040\u003F\u0024AAD\u003F\u0024AA3\u003F\u0024AAD\u003F\u0024AAX\u003F\u0024AAC\u003F\u0024AAr\u003F\u0024AAe\u003F\u0024AAa\u003F\u0024AAt\u003F\u0024AAe\u003F\u0024AAR\u003F\u0024AAe\u003F\u0024AAn\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AAr\u003F\u0024AAT\u003F\u0024AAo\u003F\u0024AAS\u003F\u0024AAu\u003F\u0024AAr\u003F\u0024AAf\u003F\u0024AAa\u003F\u0024AAc\u003F\u0024AAe\u003F\u0024AA\u003F\u0024CI\u003F\u0024AA\u003F5\u003F\u0024AAM\u003F\u0024AAa\u003F\u0024AAn\u003F\u0024AAa\u003F\u0024AAg\u0040));
        FableMod.Gfx.Exception exception;
        \u003CModule\u003E.FableMod\u002EGfx\u002EException\u002E\u007Bctor\u007D(&exception, (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CE\u0040PFCJEMGN\u0040\u003F\u0024AAG\u003F\u0024AAF\u003F\u0024AAX\u003F\u0024AA_\u003F\u0024AAD\u003F\u0024AA3\u003F\u0024AAD\u003F\u0024AA_\u003F\u0024AAE\u003F\u0024AAX\u003F\u0024AAC\u003F\u0024AAE\u003F\u0024AAP\u003F\u0024AAT\u003F\u0024AAI\u003F\u0024AAO\u003F\u0024AAN\u003F\u0024AA\u003F\u0024AA\u0040);
        \u003CModule\u003E._CxxThrowException((void*) &exception, &\u003CModule\u003E._TI1\u003FAVException\u0040Gfx\u0040FableMod\u0040\u0040);
      }
      ID3DXSprite* pSprite = (ID3DXSprite*) 0L;
      if (\u003CModule\u003E.D3DXCreateSprite((IDirect3DDevice9*) *(long*) ((IntPtr) \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA + 8L), &pSprite) < 0)
      {
        \u003CModule\u003E.FableMod\u002ELogFile\u002EWrite(\u003CModule\u003E.FableMod\u002ELogFile\u002EGetInstance(), (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CM\u0040OKAGFLBM\u0040\u003F\u0024AA\u003F\u0024CI\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAs\u003F\u0024AA\u003F3\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAd\u003F\u0024AA\u003F\u0024CJ\u003F\u0024AA\u003F3\u003F\u0024AA\u003F5\u003F\u0024AAD\u003F\u0024AAX\u003F\u0024AAE\u003F\u0024AAR\u003F\u0024AAR\u003F\u0024AA\u003F3\u003F\u0024AA\u003F5\u003F\u0024AA\u003F\u0024CC\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAs\u003F\u0024AA\u003F\u0024CC\u003F\u0024AA\u003F6\u003F\u0024AA\u003F\u0024AA\u0040, __arglist (out \u003CModule\u003E.\u003F\u003F_C\u0040_1BA\u0040GOAFNGME\u0040\u003F\u0024AAM\u003F\u0024AAa\u003F\u0024AAp\u003F\u0024AA\u003F4\u003F\u0024AAc\u003F\u0024AAp\u003F\u0024AAp\u003F\u0024AA\u003F\u0024AA\u0040, 410, out \u003CModule\u003E.\u003F\u003F_C\u0040_1IE\u0040KMKKAJAB\u0040\u003F\u0024AAD\u003F\u0024AA3\u003F\u0024AAD\u003F\u0024AAX\u003F\u0024AAC\u003F\u0024AAr\u003F\u0024AAe\u003F\u0024AAa\u003F\u0024AAt\u003F\u0024AAe\u003F\u0024AAS\u003F\u0024AAp\u003F\u0024AAr\u003F\u0024AAi\u003F\u0024AAt\u003F\u0024AAe\u003F\u0024AA\u003F\u0024CI\u003F\u0024AA\u003F5\u003F\u0024AAM\u003F\u0024AAa\u003F\u0024AAn\u003F\u0024AAa\u003F\u0024AAg\u003F\u0024AAe\u003F\u0024AAr\u003F\u0024AA\u003F3\u003F\u0024AA\u003F3\u003F\u0024AAG\u003F\u0024AAe\u003F\u0024AAt\u003F\u0024AAD\u003F\u0024AAe\u0040));
        FableMod.Gfx.Exception exception;
        \u003CModule\u003E.FableMod\u002EGfx\u002EException\u002E\u007Bctor\u007D(&exception, (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CE\u0040PFCJEMGN\u0040\u003F\u0024AAG\u003F\u0024AAF\u003F\u0024AAX\u003F\u0024AA_\u003F\u0024AAD\u003F\u0024AA3\u003F\u0024AAD\u003F\u0024AA_\u003F\u0024AAE\u003F\u0024AAX\u003F\u0024AAC\u003F\u0024AAE\u003F\u0024AAP\u003F\u0024AAT\u003F\u0024AAI\u003F\u0024AAO\u003F\u0024AAN\u003F\u0024AA\u003F\u0024AA\u0040);
        \u003CModule\u003E._CxxThrowException((void*) &exception, &\u003CModule\u003E._TI1\u003FAVException\u0040Gfx\u0040FableMod\u0040\u0040);
      }
      IDirect3DSurface9* idirect3Dsurface9Ptr1 = (IDirect3DSurface9*) 0L;
      long num1 = *(long*) ((IntPtr) editableTexturePtr + 24L);
      long num2 = num1;
      ref IDirect3DSurface9* local1 = ref idirect3Dsurface9Ptr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      if (__calli((__FnPtr<int (IntPtr, uint, IDirect3DSurface9**)>) *(long*) (*(long*) num1 + 144L /*0x90*/))((IntPtr) num2, 0U, (IDirect3DSurface9**) ref local1) < 0)
      {
        \u003CModule\u003E.FableMod\u002ELogFile\u002EWrite(\u003CModule\u003E.FableMod\u002ELogFile\u002EGetInstance(), (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CM\u0040OKAGFLBM\u0040\u003F\u0024AA\u003F\u0024CI\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAs\u003F\u0024AA\u003F3\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAd\u003F\u0024AA\u003F\u0024CJ\u003F\u0024AA\u003F3\u003F\u0024AA\u003F5\u003F\u0024AAD\u003F\u0024AAX\u003F\u0024AAE\u003F\u0024AAR\u003F\u0024AAR\u003F\u0024AA\u003F3\u003F\u0024AA\u003F5\u003F\u0024AA\u003F\u0024CC\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAs\u003F\u0024AA\u003F\u0024CC\u003F\u0024AA\u003F6\u003F\u0024AA\u003F\u0024AA\u0040, __arglist (out \u003CModule\u003E.\u003F\u003F_C\u0040_1BA\u0040GOAFNGME\u0040\u003F\u0024AAM\u003F\u0024AAa\u003F\u0024AAp\u003F\u0024AA\u003F4\u003F\u0024AAc\u003F\u0024AAp\u003F\u0024AAp\u003F\u0024AA\u003F\u0024AA\u0040, 414, out \u003CModule\u003E.\u003F\u003F_C\u0040_1HE\u0040PHDMMFAC\u0040\u003F\u0024AAs\u003F\u0024AAp\u003F\u0024AAT\u003F\u0024AAe\u003F\u0024AAx\u003F\u0024AAt\u003F\u0024AAu\u003F\u0024AAr\u003F\u0024AAe\u003F\u0024AA\u003F9\u003F\u0024AA\u003F\u0024DO\u003F\u0024AAG\u003F\u0024AAe\u003F\u0024AAt\u003F\u0024AAD\u003F\u0024AA3\u003F\u0024AAD\u003F\u0024AAT\u003F\u0024AAe\u003F\u0024AAx\u003F\u0024AAt\u003F\u0024AAu\u003F\u0024AAr\u003F\u0024AAe\u003F\u0024AA\u003F\u0024CI\u003F\u0024AA\u003F\u0024CJ\u003F\u0024AA\u003F9\u003F\u0024AA\u003F\u0024DO\u003F\u0024AAG\u003F\u0024AAe\u003F\u0024AAt\u003F\u0024AAS\u0040));
        FableMod.Gfx.Exception exception;
        \u003CModule\u003E.FableMod\u002EGfx\u002EException\u002E\u007Bctor\u007D(&exception, (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CE\u0040PFCJEMGN\u0040\u003F\u0024AAG\u003F\u0024AAF\u003F\u0024AAX\u003F\u0024AA_\u003F\u0024AAD\u003F\u0024AA3\u003F\u0024AAD\u003F\u0024AA_\u003F\u0024AAE\u003F\u0024AAX\u003F\u0024AAC\u003F\u0024AAE\u003F\u0024AAP\u003F\u0024AAT\u003F\u0024AAI\u003F\u0024AAO\u003F\u0024AAN\u003F\u0024AA\u003F\u0024AA\u0040);
        \u003CModule\u003E._CxxThrowException((void*) &exception, &\u003CModule\u003E._TI1\u003FAVException\u0040Gfx\u0040FableMod\u0040\u0040);
      }
      _D3DVIEWPORT9 d3DviewporT9;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref d3DviewporT9 = 0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &d3DviewporT9 + 4) = 0;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &d3DviewporT9 + 8) = (int) \u003CModule\u003E.FableMod\u002EGfx\u002ETexture\u002EGetWidth((Texture*) editableTexturePtr, 0U);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ((IntPtr) &d3DviewporT9 + 12) = (int) \u003CModule\u003E.FableMod\u002EGfx\u002ETexture\u002EGetHeight((Texture*) editableTexturePtr, 0U);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3DviewporT9 + 16 /*0x10*/) = 0.0f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3DviewporT9 + 20) = 1f;
      ID3DXRenderToSurface* dxRenderToSurfacePtr2 = dxRenderToSurfacePtr1;
      IDirect3DSurface9* idirect3Dsurface9Ptr2 = idirect3Dsurface9Ptr1;
      ref _D3DVIEWPORT9 local2 = ref d3DviewporT9;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      if (__calli((__FnPtr<int (IntPtr, IDirect3DSurface9*, _D3DVIEWPORT9*)>) *(long*) (*(long*) dxRenderToSurfacePtr1 + 40L))((IntPtr) dxRenderToSurfacePtr2, idirect3Dsurface9Ptr2, (_D3DVIEWPORT9*) ref local2) < 0)
      {
        \u003CModule\u003E.FableMod\u002ELogFile\u002EWrite(\u003CModule\u003E.FableMod\u002ELogFile\u002EGetInstance(), (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CM\u0040OKAGFLBM\u0040\u003F\u0024AA\u003F\u0024CI\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAs\u003F\u0024AA\u003F3\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAd\u003F\u0024AA\u003F\u0024CJ\u003F\u0024AA\u003F3\u003F\u0024AA\u003F5\u003F\u0024AAD\u003F\u0024AAX\u003F\u0024AAE\u003F\u0024AAR\u003F\u0024AAR\u003F\u0024AA\u003F3\u003F\u0024AA\u003F5\u003F\u0024AA\u003F\u0024CC\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAs\u003F\u0024AA\u003F\u0024CC\u003F\u0024AA\u003F6\u003F\u0024AA\u003F\u0024AA\u0040, __arglist (out \u003CModule\u003E.\u003F\u003F_C\u0040_1BA\u0040GOAFNGME\u0040\u003F\u0024AAM\u003F\u0024AAa\u003F\u0024AAp\u003F\u0024AA\u003F4\u003F\u0024AAc\u003F\u0024AAp\u003F\u0024AAp\u003F\u0024AA\u003F\u0024AA\u0040, 425, out \u003CModule\u003E.\u003F\u003F_C\u0040_1GE\u0040DLMGDEEJ\u0040\u003F\u0024AAp\u003F\u0024AAR\u003F\u0024AAe\u003F\u0024AAn\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AAr\u003F\u0024AAT\u003F\u0024AAo\u003F\u0024AAS\u003F\u0024AAu\u003F\u0024AAr\u003F\u0024AAf\u003F\u0024AAa\u003F\u0024AAc\u003F\u0024AAe\u003F\u0024AA\u003F9\u003F\u0024AA\u003F\u0024DO\u003F\u0024AAB\u003F\u0024AAe\u003F\u0024AAg\u003F\u0024AAi\u003F\u0024AAn\u003F\u0024AAS\u003F\u0024AAc\u003F\u0024AAe\u003F\u0024AAn\u003F\u0024AAe\u003F\u0024AA\u003F\u0024CI\u003F\u0024AAp\u003F\u0024AAS\u003F\u0024AAu\u0040));
        FableMod.Gfx.Exception exception;
        \u003CModule\u003E.FableMod\u002EGfx\u002EException\u002E\u007Bctor\u007D(&exception, (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CE\u0040PFCJEMGN\u0040\u003F\u0024AAG\u003F\u0024AAF\u003F\u0024AAX\u003F\u0024AA_\u003F\u0024AAD\u003F\u0024AA3\u003F\u0024AAD\u003F\u0024AA_\u003F\u0024AAE\u003F\u0024AAX\u003F\u0024AAC\u003F\u0024AAE\u003F\u0024AAP\u003F\u0024AAT\u003F\u0024AAI\u003F\u0024AAO\u003F\u0024AAN\u003F\u0024AA\u003F\u0024AA\u0040);
        \u003CModule\u003E._CxxThrowException((void*) &exception, &\u003CModule\u003E._TI1\u003FAVException\u0040Gfx\u0040FableMod\u0040\u0040);
      }
      ID3DXSprite* id3DxSpritePtr1 = pSprite;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      if (__calli((__FnPtr<int (IntPtr, uint)>) *(long*) (*(long*) pSprite + 64L /*0x40*/))((IntPtr) id3DxSpritePtr1, 16U /*0x10*/) < 0)
      {
        \u003CModule\u003E.FableMod\u002ELogFile\u002EWrite(\u003CModule\u003E.FableMod\u002ELogFile\u002EGetInstance(), (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CM\u0040OKAGFLBM\u0040\u003F\u0024AA\u003F\u0024CI\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAs\u003F\u0024AA\u003F3\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAd\u003F\u0024AA\u003F\u0024CJ\u003F\u0024AA\u003F3\u003F\u0024AA\u003F5\u003F\u0024AAD\u003F\u0024AAX\u003F\u0024AAE\u003F\u0024AAR\u003F\u0024AAR\u003F\u0024AA\u003F3\u003F\u0024AA\u003F5\u003F\u0024AA\u003F\u0024CC\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAs\u003F\u0024AA\u003F\u0024CC\u003F\u0024AA\u003F6\u003F\u0024AA\u003F\u0024AA\u0040, __arglist (out \u003CModule\u003E.\u003F\u003F_C\u0040_1BA\u0040GOAFNGME\u0040\u003F\u0024AAM\u003F\u0024AAa\u003F\u0024AAp\u003F\u0024AA\u003F4\u003F\u0024AAc\u003F\u0024AAp\u003F\u0024AAp\u003F\u0024AA\u003F\u0024AA\u0040, 427, out \u003CModule\u003E.\u003F\u003F_C\u0040_1EM\u0040HGKLLBBD\u0040\u003F\u0024AAp\u003F\u0024AAS\u003F\u0024AAp\u003F\u0024AAr\u003F\u0024AAi\u003F\u0024AAt\u003F\u0024AAe\u003F\u0024AA\u003F9\u003F\u0024AA\u003F\u0024DO\u003F\u0024AAB\u003F\u0024AAe\u003F\u0024AAg\u003F\u0024AAi\u003F\u0024AAn\u003F\u0024AA\u003F\u0024CI\u003F\u0024AAD\u003F\u0024AA3\u003F\u0024AAD\u003F\u0024AAX\u003F\u0024AAS\u003F\u0024AAP\u003F\u0024AAR\u003F\u0024AAI\u003F\u0024AAT\u003F\u0024AAE\u003F\u0024AA_\u003F\u0024AAA\u003F\u0024AAL\u003F\u0024AAP\u003F\u0024AAH\u003F\u0024AAA\u003F\u0024AAB\u0040));
        FableMod.Gfx.Exception exception;
        \u003CModule\u003E.FableMod\u002EGfx\u002EException\u002E\u007Bctor\u007D(&exception, (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CE\u0040PFCJEMGN\u0040\u003F\u0024AAG\u003F\u0024AAF\u003F\u0024AAX\u003F\u0024AA_\u003F\u0024AAD\u003F\u0024AA3\u003F\u0024AAD\u003F\u0024AA_\u003F\u0024AAE\u003F\u0024AAX\u003F\u0024AAC\u003F\u0024AAE\u003F\u0024AAP\u003F\u0024AAT\u003F\u0024AAI\u003F\u0024AAO\u003F\u0024AAN\u003F\u0024AA\u003F\u0024AA\u0040);
        \u003CModule\u003E._CxxThrowException((void*) &exception, &\u003CModule\u003E._TI1\u003FAVException\u0040Gfx\u0040FableMod\u0040\u0040);
      }
      D3DXMATRIX d3Dxmatrix;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3Dxmatrix + 56) = 0.0f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3Dxmatrix + 52) = 0.0f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3Dxmatrix + 48 /*0x30*/) = 0.0f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3Dxmatrix + 44) = 0.0f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3Dxmatrix + 36) = 0.0f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3Dxmatrix + 32 /*0x20*/) = 0.0f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3Dxmatrix + 28) = 0.0f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3Dxmatrix + 24) = 0.0f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3Dxmatrix + 16 /*0x10*/) = 0.0f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3Dxmatrix + 12) = 0.0f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3Dxmatrix + 8) = 0.0f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3Dxmatrix + 4) = 0.0f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3Dxmatrix + 60) = 1f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3Dxmatrix + 40) = 1f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3Dxmatrix + 20) = 1f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ref d3Dxmatrix = 1f;
      float textureMultiplier = (float) \u003CModule\u003E.FableMod\u002EGfx\u002ETerrain\u002EGetTextureMultiplier();
      ThemeManager themeManager = GfxManager.GetThemeManager();
      float num3 = *(float*) ((IntPtr) this.GetTerrain() + 400L);
      float num4 = *(float*) ((IntPtr) this.GetTerrain() + 180L);
      int num5 = 0;
      if (0 < this.m_Lev.Width)
      {
        do
        {
          float num6 = *(float*) ((IntPtr) this.GetTerrain() + 184L);
          int num7 = 0;
          if (0 < this.m_Lev.Height)
          {
            float num8 = (float) num5 * textureMultiplier;
            double num9 = (double) num3 * 0.5;
            float fWorldX = num4 + (float) num9;
            do
            {
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ((IntPtr) &d3Dxmatrix + 48 /*0x30*/) = num8;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ((IntPtr) &d3Dxmatrix + 52) = (float) num7 * textureMultiplier;
              byte index = this.m_Lev.get_GroundThemeIndices(num5, num7, 0);
              int num10 = (int) this.m_Lev.get_GroundThemeIndices(num5, num7, 1);
              int num11 = (int) this.m_Lev.get_GroundThemeIndices(num5, num7, 2);
              int num12 = (int) this.m_Lev.get_GroundThemeStrengths(num5, num7, 0);
              int num13 = (int) this.m_Lev.get_GroundThemeStrengths(num5, num7, 1);
              int num14 = (int) this.m_Lev.get_GroundThemeStrengths(num5, num7, 2);
              float fWorldY = num6 + (float) num9;
              Theme theme = themeManager.get_Themes(this.m_Lev.get_GroundThemeNames((int) index));
              this.ApplyTheme(controller, pSprite, theme, &d3Dxmatrix, byte.MaxValue, fWorldX, fWorldY, false, num5, num7);
              num6 += num3;
              ++num7;
            }
            while (num7 < this.m_Lev.Height);
          }
          num4 += num3;
          ++num5;
        }
        while (num5 < this.m_Lev.Width);
      }
      ID3DXSprite* id3DxSpritePtr2 = pSprite;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      if (__calli((__FnPtr<int (IntPtr)>) *(long*) (*(long*) id3DxSpritePtr2 + 88L))((IntPtr) id3DxSpritePtr2) < 0)
      {
        \u003CModule\u003E.FableMod\u002ELogFile\u002EWrite(\u003CModule\u003E.FableMod\u002ELogFile\u002EGetInstance(), (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CM\u0040OKAGFLBM\u0040\u003F\u0024AA\u003F\u0024CI\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAs\u003F\u0024AA\u003F3\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAd\u003F\u0024AA\u003F\u0024CJ\u003F\u0024AA\u003F3\u003F\u0024AA\u003F5\u003F\u0024AAD\u003F\u0024AAX\u003F\u0024AAE\u003F\u0024AAR\u003F\u0024AAR\u003F\u0024AA\u003F3\u003F\u0024AA\u003F5\u003F\u0024AA\u003F\u0024CC\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAs\u003F\u0024AA\u003F\u0024CC\u003F\u0024AA\u003F6\u003F\u0024AA\u003F\u0024AA\u0040, __arglist (out \u003CModule\u003E.\u003F\u003F_C\u0040_1BA\u0040GOAFNGME\u0040\u003F\u0024AAM\u003F\u0024AAa\u003F\u0024AAp\u003F\u0024AA\u003F4\u003F\u0024AAc\u003F\u0024AAp\u003F\u0024AAp\u003F\u0024AA\u003F\u0024AA\u0040, 488, out \u003CModule\u003E.\u003F\u003F_C\u0040_1BO\u0040IIOBCGCK\u0040\u003F\u0024AAp\u003F\u0024AAS\u003F\u0024AAp\u003F\u0024AAr\u003F\u0024AAi\u003F\u0024AAt\u003F\u0024AAe\u003F\u0024AA\u003F9\u003F\u0024AA\u003F\u0024DO\u003F\u0024AAE\u003F\u0024AAn\u003F\u0024AAd\u003F\u0024AA\u003F\u0024CI\u003F\u0024AA\u003F\u0024CJ\u003F\u0024AA\u003F\u0024AA\u0040));
        FableMod.Gfx.Exception exception;
        \u003CModule\u003E.FableMod\u002EGfx\u002EException\u002E\u007Bctor\u007D(&exception, (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CE\u0040PFCJEMGN\u0040\u003F\u0024AAG\u003F\u0024AAF\u003F\u0024AAX\u003F\u0024AA_\u003F\u0024AAD\u003F\u0024AA3\u003F\u0024AAD\u003F\u0024AA_\u003F\u0024AAE\u003F\u0024AAX\u003F\u0024AAC\u003F\u0024AAE\u003F\u0024AAP\u003F\u0024AAT\u003F\u0024AAI\u003F\u0024AAO\u003F\u0024AAN\u003F\u0024AA\u003F\u0024AA\u0040);
        \u003CModule\u003E._CxxThrowException((void*) &exception, &\u003CModule\u003E._TI1\u003FAVException\u0040Gfx\u0040FableMod\u0040\u0040);
      }
      ID3DXRenderToSurface* dxRenderToSurfacePtr3 = dxRenderToSurfacePtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      if (__calli((__FnPtr<int (IntPtr, uint)>) *(long*) (*(long*) dxRenderToSurfacePtr1 + 48L /*0x30*/))((IntPtr) dxRenderToSurfacePtr3, 3U) < 0)
      {
        \u003CModule\u003E.FableMod\u002ELogFile\u002EWrite(\u003CModule\u003E.FableMod\u002ELogFile\u002EGetInstance(), (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CM\u0040OKAGFLBM\u0040\u003F\u0024AA\u003F\u0024CI\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAs\u003F\u0024AA\u003F3\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAd\u003F\u0024AA\u003F\u0024CJ\u003F\u0024AA\u003F3\u003F\u0024AA\u003F5\u003F\u0024AAD\u003F\u0024AAX\u003F\u0024AAE\u003F\u0024AAR\u003F\u0024AAR\u003F\u0024AA\u003F3\u003F\u0024AA\u003F5\u003F\u0024AA\u003F\u0024CC\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAs\u003F\u0024AA\u003F\u0024CC\u003F\u0024AA\u003F6\u003F\u0024AA\u003F\u0024AA\u0040, __arglist (out \u003CModule\u003E.\u003F\u003F_C\u0040_1BA\u0040GOAFNGME\u0040\u003F\u0024AAM\u003F\u0024AAa\u003F\u0024AAp\u003F\u0024AA\u003F4\u003F\u0024AAc\u003F\u0024AAp\u003F\u0024AAp\u003F\u0024AA\u003F\u0024AA\u0040, 490, out \u003CModule\u003E.\u003F\u003F_C\u0040_1FO\u0040DFCDCANC\u0040\u003F\u0024AAp\u003F\u0024AAR\u003F\u0024AAe\u003F\u0024AAn\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AAr\u003F\u0024AAT\u003F\u0024AAo\u003F\u0024AAS\u003F\u0024AAu\u003F\u0024AAr\u003F\u0024AAf\u003F\u0024AAa\u003F\u0024AAc\u003F\u0024AAe\u003F\u0024AA\u003F9\u003F\u0024AA\u003F\u0024DO\u003F\u0024AAE\u003F\u0024AAn\u003F\u0024AAd\u003F\u0024AAS\u003F\u0024AAc\u003F\u0024AAe\u003F\u0024AAn\u003F\u0024AAe\u003F\u0024AA\u003F\u0024CI\u003F\u0024AAD\u003F\u0024AA3\u003F\u0024AAD\u003F\u0024AAX\u003F\u0024AA_\u0040));
        FableMod.Gfx.Exception exception;
        \u003CModule\u003E.FableMod\u002EGfx\u002EException\u002E\u007Bctor\u007D(&exception, (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CE\u0040PFCJEMGN\u0040\u003F\u0024AAG\u003F\u0024AAF\u003F\u0024AAX\u003F\u0024AA_\u003F\u0024AAD\u003F\u0024AA3\u003F\u0024AAD\u003F\u0024AA_\u003F\u0024AAE\u003F\u0024AAX\u003F\u0024AAC\u003F\u0024AAE\u003F\u0024AAP\u003F\u0024AAT\u003F\u0024AAI\u003F\u0024AAO\u003F\u0024AAN\u003F\u0024AA\u003F\u0024AA\u0040);
        \u003CModule\u003E._CxxThrowException((void*) &exception, &\u003CModule\u003E._TI1\u003FAVException\u0040Gfx\u0040FableMod\u0040\u0040);
      }
      if ((IntPtr) idirect3Dsurface9Ptr1 != IntPtr.Zero)
      {
        IDirect3DSurface9* idirect3Dsurface9Ptr3 = idirect3Dsurface9Ptr1;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int num15 = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) idirect3Dsurface9Ptr3 + 16L /*0x10*/))((IntPtr) idirect3Dsurface9Ptr3);
      }
      if ((IntPtr) pSprite != IntPtr.Zero)
      {
        ID3DXSprite* id3DxSpritePtr3 = pSprite;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int num16 = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxSpritePtr3 + 16L /*0x10*/))((IntPtr) id3DxSpritePtr3);
        pSprite = (ID3DXSprite*) 0L;
      }
      if ((IntPtr) dxRenderToSurfacePtr1 != IntPtr.Zero)
      {
        ID3DXRenderToSurface* dxRenderToSurfacePtr4 = dxRenderToSurfacePtr1;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int num17 = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) dxRenderToSurfacePtr4 + 16L /*0x10*/))((IntPtr) dxRenderToSurfacePtr4);
      }
    }
    __fault
    {
      // ISSUE: method pointer
      // ISSUE: cast to a function pointer type
      \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AEditableTexture\u003E\u002E\u007Bdtor\u007D), (void*) &gfxEditableTexture);
    }
    \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AEditableTexture\u003E\u002E\u007Bdtor\u007D(&gfxEditableTexture);
    // ISSUE: fault handler
    try
    {
    }
    __fault
    {
      // ISSUE: method pointer
      // ISSUE: cast to a function pointer type
      \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AEditableTexture\u003E\u002E\u007Bdtor\u007D), (void*) &gfxEditableTexture);
    }
  }

  protected unsafe void DrawSprite(ID3DXSprite* pSprite, IDirect3DTexture9* pTexture, byte ucAlpha)
  {
    D3DXVECTOR3 d3DxvectoR3;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3DxvectoR3 = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3 + 4) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3 + 8) = 0.0f;
    ID3DXSprite* id3DxSpritePtr = pSprite;
    IDirect3DTexture9* idirect3Dtexture9Ptr = pTexture;
    ref D3DXVECTOR3 local = ref d3DxvectoR3;
    int num1 = (int) ucAlpha << 24 | 16777215 /*0xFFFFFF*/;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num2 = __calli((__FnPtr<int (IntPtr, IDirect3DTexture9*, tagRECT*, D3DXVECTOR3*, D3DXVECTOR3*, uint)>) *(long*) (*(long*) pSprite + 72L))((IntPtr) id3DxSpritePtr, idirect3Dtexture9Ptr, (tagRECT*) 0L, (D3DXVECTOR3*) 0L, (D3DXVECTOR3*) ref local, (uint) num1);
  }

  protected unsafe void ApplyTheme(
    GfxController controller,
    ID3DXSprite* pSprite,
    Theme theme,
    D3DXMATRIX* pmatWorld,
    byte ucStrength,
    float fWorldX,
    float fWorldY,
    [MarshalAs(UnmanagedType.U1)] bool bModel,
    int iCellX,
    int iCellY)
  {
    if (theme == null || theme.Texture == null)
      return;
    IDirect3DTexture9* baseD3Dtexture = theme.Texture.GetBaseD3DTexture();
    if ((IntPtr) baseD3Dtexture == IntPtr.Zero)
      return;
    IDirect3DTexture9* idirect3Dtexture9Ptr = baseD3Dtexture;
    _D3DSURFACE_DESC d3DsurfaceDesc;
    ref _D3DSURFACE_DESC local = ref d3DsurfaceDesc;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num1 = __calli((__FnPtr<int (IntPtr, uint, _D3DSURFACE_DESC*)>) *(long*) (*(long*) baseD3Dtexture + 136L))((IntPtr) idirect3Dtexture9Ptr, 0U, (_D3DSURFACE_DESC*) ref local);
    float textureMultiplier = (float) \u003CModule\u003E.FableMod\u002EGfx\u002ETerrain\u002EGetTextureMultiplier();
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    *(float*) pmatWorld = textureMultiplier / (float) (uint) ^(int&) ((IntPtr) &d3DsurfaceDesc + 24);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    *(float*) ((IntPtr) pmatWorld + 20L) = textureMultiplier / (float) (uint) ^(int&) ((IntPtr) &d3DsurfaceDesc + 28);
    ID3DXSprite* id3DxSpritePtr = pSprite;
    D3DXMATRIX* d3DxmatrixPtr = pmatWorld;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num2 = __calli((__FnPtr<int (IntPtr, D3DXMATRIX*)>) *(long*) (*(long*) pSprite + 40L))((IntPtr) id3DxSpritePtr, d3DxmatrixPtr);
    this.DrawSprite(pSprite, baseD3Dtexture, ucStrength);
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EMap();
    }
    else
    {
      // ISSUE: explicit finalizer call
      this.Finalize();
    }
  }


  public virtual void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }
}
