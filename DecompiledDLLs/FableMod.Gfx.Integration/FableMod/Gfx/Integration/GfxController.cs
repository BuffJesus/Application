// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.GfxController
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Gfx.Integration;

public class GfxController : IDisposable
{
  private List<GfxView> m_Views;
  private unsafe SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* m_pRoot = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;

  public GfxController()
  {
    this.m_Views = new List<GfxView>();
    this.ResetObjects();
  }

  private void \u007EGfxController() => this.Destroy();

  public virtual unsafe void Destroy()
  {
    int numObjects1 = (int) \u003CModule\u003E.FableMod\u002EGfx\u002ERootObject\u002EGetNumObjects();
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pRoot = this.m_pRoot;
    if ((IntPtr) pRoot != IntPtr.Zero)
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr1 = pRoot;
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr2 = pointerFableModGfxNodePtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) pointerFableModGfxNodePtr1)((IntPtr) pointerFableModGfxNodePtr2, 1U);
      this.m_pRoot = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
    }
    int index = 0;
    if (0 < this.m_Views.Count)
    {
      do
      {
        this.m_Views[index].Destroy();
        ++index;
      }
      while (index < this.m_Views.Count);
    }
    this.m_Views.Clear();
    int numObjects2 = (int) \u003CModule\u003E.FableMod\u002EGfx\u002ERootObject\u002EGetNumObjects();
    \u003CModule\u003E.FableMod\u002EGfx\u002ETerrainManager\u002ERemoveAll(\u003CModule\u003E.FableMod\u002EGfx\u002ETerrainManager\u002EGetInstance());
  }

  public virtual unsafe void ResetObjects()
  {
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pRoot = this.m_pRoot;
    if ((IntPtr) pRoot != IntPtr.Zero)
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr1 = pRoot;
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr2 = pointerFableModGfxNodePtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) pointerFableModGfxNodePtr1)((IntPtr) pointerFableModGfxNodePtr2, 1U);
      this.m_pRoot = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
    }
    if (\u003CModule\u003E.FableMod\u002EGfx\u002ESettings\u002EGetInt(\u003CModule\u003E.FableMod\u002EGfx\u002ESettings\u002EGetInstance(), (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1BA\u0040GOGLBMBE\u0040\u003F\u0024AAO\u003F\u0024AAc\u003F\u0024AAt\u003F\u0024AAT\u003F\u0024AAr\u003F\u0024AAe\u003F\u0024AAe\u003F\u0024AA\u003F\u0024AA\u0040, (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_17GFFKFGK\u0040\u003F\u0024AAU\u003F\u0024AAs\u003F\u0024AAe\u003F\u0024AA\u003F\u0024AA\u0040, 0) != 0)
    {
      OctTree* octTreePtr1 = (OctTree*) \u003CModule\u003E.@new(424UL);
      OctTree* octTreePtr2;
      // ISSUE: fault handler
      try
      {
        octTreePtr2 = (IntPtr) octTreePtr1 == IntPtr.Zero ? (OctTree*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EOctTree\u002E\u007Bctor\u007D(octTreePtr1);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) octTreePtr1);
      }
      D3DXVECTOR3 d3DxvectoR3_1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ref d3DxvectoR3_1 = 500f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3DxvectoR3_1 + 4) = 500f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3DxvectoR3_1 + 8) = 500f;
      D3DXVECTOR3 d3DxvectoR3_2;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ref d3DxvectoR3_2 = 2000f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4) = 2000f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3DxvectoR3_2 + 8) = 2000f;
      D3DXVECTOR3 d3DxvectoR3_3;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ref d3DxvectoR3_3 = -2000f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3DxvectoR3_3 + 4) = -2000f;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(float&) ((IntPtr) &d3DxvectoR3_3 + 8) = -2000f;
      \u003CModule\u003E.FableMod\u002EGfx\u002EOctTreeNode\u002EBuild((OctTreeNode*) octTreePtr2, &d3DxvectoR3_3, &d3DxvectoR3_2, &d3DxvectoR3_1);
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr3 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr4;
      // ISSUE: fault handler
      try
      {
        if ((IntPtr) pointerFableModGfxNodePtr3 != IntPtr.Zero)
        {
          *(long*) pointerFableModGfxNodePtr3 = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VNode\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
          *(long*) ((IntPtr) pointerFableModGfxNodePtr3 + 8L) = (long) octTreePtr2;
          if ((IntPtr) octTreePtr2 != IntPtr.Zero)
            *(int*) ((IntPtr) octTreePtr2 + 8L) = *(int*) ((IntPtr) octTreePtr2 + 8L) + 1;
          pointerFableModGfxNodePtr4 = pointerFableModGfxNodePtr3;
        }
        else
          pointerFableModGfxNodePtr4 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) pointerFableModGfxNodePtr3);
      }
      this.m_pRoot = pointerFableModGfxNodePtr4;
      \u003CModule\u003E.FableMod\u002EGfx\u002ERootObject\u002ESetName((RootObject*) *(long*) ((IntPtr) pointerFableModGfxNodePtr4 + 8L), (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1DG\u0040KEJLMGJG\u0040\u003F\u0024AAG\u003F\u0024AAf\u003F\u0024AAx\u003F\u0024AAC\u003F\u0024AAo\u003F\u0024AAn\u003F\u0024AAt\u003F\u0024AAr\u003F\u0024AAo\u003F\u0024AAl\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAr\u003F\u0024AA_\u003F\u0024AAR\u003F\u0024AAo\u003F\u0024AAo\u003F\u0024AAt\u003F\u0024AA_\u003F\u0024AAO\u003F\u0024AAc\u003F\u0024AAt\u003F\u0024AAT\u003F\u0024AAr\u003F\u0024AAe\u003F\u0024AAe\u003F\u0024AA\u003F\u0024AA\u0040);
    }
    else
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr5 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr6;
      // ISSUE: fault handler
      try
      {
        if ((IntPtr) pointerFableModGfxNodePtr5 != IntPtr.Zero)
        {
          FableMod.Gfx.Node* nodePtr1 = (FableMod.Gfx.Node*) \u003CModule\u003E.@new(304UL);
          FableMod.Gfx.Node* nodePtr2;
          // ISSUE: fault handler
          try
          {
            nodePtr2 = (IntPtr) nodePtr1 == IntPtr.Zero ? (FableMod.Gfx.Node*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002ENode\u002E\u007Bctor\u007D(nodePtr1);
          }
          __fault
          {
            \u003CModule\u003E.delete((void*) nodePtr1);
          }
          *(long*) pointerFableModGfxNodePtr5 = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VNode\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
          *(long*) ((IntPtr) pointerFableModGfxNodePtr5 + 8L) = (long) nodePtr2;
          if ((IntPtr) nodePtr2 != IntPtr.Zero)
            *(int*) ((IntPtr) nodePtr2 + 8L) = *(int*) ((IntPtr) nodePtr2 + 8L) + 1;
          pointerFableModGfxNodePtr6 = pointerFableModGfxNodePtr5;
        }
        else
          pointerFableModGfxNodePtr6 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) pointerFableModGfxNodePtr5);
      }
      this.m_pRoot = pointerFableModGfxNodePtr6;
      \u003CModule\u003E.FableMod\u002EGfx\u002ERootObject\u002ESetName((RootObject*) *(long*) ((IntPtr) pointerFableModGfxNodePtr6 + 8L), (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CG\u0040DGKMFBFC\u0040\u003F\u0024AAG\u003F\u0024AAf\u003F\u0024AAx\u003F\u0024AAC\u003F\u0024AAo\u003F\u0024AAn\u003F\u0024AAt\u003F\u0024AAr\u003F\u0024AAo\u003F\u0024AAl\u003F\u0024AAl\u003F\u0024AAe\u003F\u0024AAr\u003F\u0024AA_\u003F\u0024AAR\u003F\u0024AAo\u003F\u0024AAo\u003F\u0024AAt\u003F\u0024AA\u003F\u0024AA\u0040);
    }
    int index = 0;
    if (0 >= this.m_Views.Count)
      return;
    do
    {
      this.m_Views[index].OnResetObjects();
      ++index;
    }
    while (index < this.m_Views.Count);
  }

  public void AddView(GfxView view)
  {
    if (this.m_Views.IndexOf(view) >= 0)
      return;
    this.m_Views.Add(view);
    view.Controller = this;
    view.Initialize();
    view.FrontCamera();
  }

  public unsafe void LoadObject(string fileName)
  {
    // ISSUE: untyped stack allocation
    long num1 = (long) __untypedstackalloc(\u003CModule\u003E.__CxxQueryExceptionSize());
    IntPtr hglobalUni = Marshal.StringToHGlobalUni(fileName);
    D3DXModel* d3DxModelPtr1 = (D3DXModel*) \u003CModule\u003E.@new(72UL);
    D3DXModel* d3DxModelPtr2;
    // ISSUE: fault handler
    try
    {
      d3DxModelPtr2 = (IntPtr) d3DxModelPtr1 == IntPtr.Zero ? (D3DXModel*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002ED3DXModel\u002E\u007Bctor\u007D(d3DxModelPtr1);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) d3DxModelPtr1);
    }
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AD3DXModel\u003E fableModGfxD3DxModel;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ref fableModGfxD3DxModel = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VD3DXModel\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ((IntPtr) &fableModGfxD3DxModel + 8) = (long) d3DxModelPtr2;
    if ((IntPtr) d3DxModelPtr2 != IntPtr.Zero)
      *(int*) ((IntPtr) d3DxModelPtr2 + 8L) = *(int*) ((IntPtr) d3DxModelPtr2 + 8L) + 1;
    // ISSUE: fault handler
    try
    {
      FableMod.Gfx.Exception* exceptionPtr;
      try
      {
        \u003CModule\u003E.FableMod\u002EGfx\u002ED3DXModel\u002ELoad(d3DxModelPtr2, (char*) hglobalUni.ToPointer());
      }
      catch (System.Exception ex1) when (\u003CModule\u003E.__CxxExceptionFilter((void*) Marshal.GetExceptionPointers(), (void*) &\u003CModule\u003E.\u003F\u003F_R0\u003FAVException\u0040Gfx\u0040FableMod\u0040\u0040\u00408, 9, (void*) &exceptionPtr) != 0)
      {
        uint num2 = 0;
        \u003CModule\u003E.__CxxRegisterExceptionObject((void*) Marshal.GetExceptionPointers(), (void*) num1);
        try
        {
          try
          {
            int num3 = (int) MessageBox.Show(new string(\u003CModule\u003E.FableMod\u002EGfx\u002EException\u002EGetMsg(exceptionPtr)));
            Marshal.FreeHGlobal(hglobalUni);
            goto label_35;
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
      Mesh* meshPtr1 = (Mesh*) \u003CModule\u003E.@new(336UL);
      Mesh* meshPtr2;
      // ISSUE: fault handler
      try
      {
        meshPtr2 = (IntPtr) meshPtr1 == IntPtr.Zero ? (Mesh*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EMesh\u002E\u007Bctor\u007D(meshPtr1);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) meshPtr1);
      }
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E pointerFableModGfxMesh;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(long&) ref pointerFableModGfxMesh = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VMesh\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(long&) ((IntPtr) &pointerFableModGfxMesh + 8) = (long) meshPtr2;
      if ((IntPtr) meshPtr2 != IntPtr.Zero)
        *(int*) ((IntPtr) meshPtr2 + 8L) = *(int*) ((IntPtr) meshPtr2 + 8L) + 1;
      // ISSUE: fault handler
      try
      {
        \u003CModule\u003E.FableMod\u002EGfx\u002EMesh\u002ESetModel(meshPtr2, (Model*) d3DxModelPtr2);
        if ((IntPtr) meshPtr2 != IntPtr.Zero)
        {
          long num4 = *(long*) ((IntPtr) this.m_pRoot + 8L);
          long num5 = num4;
          Mesh* meshPtr3 = meshPtr2;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) num4 + 120L))((IntPtr) num5, (Spatial*) meshPtr3);
        }
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E\u002E\u007Bdtor\u007D), (void*) &pointerFableModGfxMesh);
      }
      if ((IntPtr) meshPtr2 != IntPtr.Zero)
      {
        uint num6 = (uint) *(int*) ((IntPtr) meshPtr2 + 8L);
        if (num6 > 0U)
          *(int*) ((IntPtr) meshPtr2 + 8L) = (int) num6 - 1;
        if (*(int*) ((IntPtr) meshPtr2 + 8L) == 0)
        {
          Mesh* meshPtr4 = meshPtr2;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) meshPtr2)((IntPtr) meshPtr4, 1U);
        }
      }
    }
    __fault
    {
      // ISSUE: method pointer
      // ISSUE: cast to a function pointer type
      \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AD3DXModel\u003E\u002E\u007Bdtor\u007D), (void*) &fableModGfxD3DxModel);
    }
    if ((IntPtr) d3DxModelPtr2 == IntPtr.Zero)
      return;
    uint num7 = (uint) *(int*) ((IntPtr) d3DxModelPtr2 + 8L);
    if (num7 > 0U)
      *(int*) ((IntPtr) d3DxModelPtr2 + 8L) = (int) num7 - 1;
    if (*(int*) ((IntPtr) d3DxModelPtr2 + 8L) != 0)
      return;
    D3DXModel* d3DxModelPtr3 = d3DxModelPtr2;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    void* voidPtr1 = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) d3DxModelPtr2)((IntPtr) d3DxModelPtr3, 1U);
    return;
label_35:
    \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AD3DXModel\u003E\u002E\u007Bdtor\u007D(&fableModGfxD3DxModel);
  }

  public unsafe void AddModel(GfxModelLOD modelLOD)
  {
    Spatial* gfx = (Spatial*) modelLOD.GetGfx();
    *(int*) ((IntPtr) gfx + 32L /*0x20*/) = *(int*) ((IntPtr) gfx + 32L /*0x20*/) | 1;
    *(float*) ((IntPtr) gfx + 176L /*0xB0*/) = 0.01f;
    long num1 = *(long*) ((IntPtr) this.m_pRoot + 8L);
    long num2 = num1;
    Spatial* spatialPtr = gfx;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) num1 + 120L))((IntPtr) num2, spatialPtr);
  }

  public unsafe void AddModel(GfxBaseModel model)
  {
    Spatial* gfx = (Spatial*) model.GetGfx();
    *(int*) ((IntPtr) gfx + 32L /*0x20*/) = *(int*) ((IntPtr) gfx + 32L /*0x20*/) | 1;
    *(float*) ((IntPtr) gfx + 176L /*0xB0*/) = 0.01f;
    long num1 = *(long*) ((IntPtr) this.m_pRoot + 8L);
    long num2 = num1;
    Spatial* spatialPtr = gfx;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) num1 + 120L))((IntPtr) num2, spatialPtr);
  }

  public unsafe void UpdateObjects()
  {
    long num1 = *(long*) ((IntPtr) this.m_pRoot + 8L);
    long num2 = num1;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, byte)>) *(long*) (*(long*) num1 + 16L /*0x10*/))((IntPtr) num2, (byte) 1);
    int index = 0;
    if (0 >= this.m_Views.Count)
      return;
    do
    {
      this.m_Views[index].Render();
      ++index;
    }
    while (index < this.m_Views.Count);
  }

  public unsafe FableMod.Gfx.Node* GetRoot() => (FableMod.Gfx.Node*) *(long*) ((IntPtr) this.m_pRoot + 8L);

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.Destroy();
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
