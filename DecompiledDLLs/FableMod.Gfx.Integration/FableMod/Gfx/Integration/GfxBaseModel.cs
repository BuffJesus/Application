// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.GfxBaseModel
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using FableMod.BIG;
using FableMod.ContentManagement;
using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.Gfx.Integration;

public class GfxBaseModel : AssetLinker
{
  protected unsafe D3DXEXTENDEDFRAME* m_pRoot = (D3DXEXTENDEDFRAME*) 0L;
  protected unsafe SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* m_pNode = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;

  public GfxBaseModel(AssetEntry entry)
  {
  }

  private unsafe void \u007EGfxBaseModel()
  {
    D3DXEXTENDEDFRAME* pRoot = this.m_pRoot;
    if ((IntPtr) pRoot != IntPtr.Zero)
    {
      D3DXEXTENDEDFRAME* d3DxextendedframePtr = pRoot;
      // ISSUE: fault handler
      try
      {
        // ISSUE: fault handler
        try
        {
          // ISSUE: fault handler
          try
          {
            \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ED3DXEXTENDEDFRAME\u002EDestroy(d3DxextendedframePtr);
          }
          __fault
          {
            // ISSUE: method pointer
            // ISSUE: cast to a function pointer type
            \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D), (void*) ((IntPtr) d3DxextendedframePtr + 136L));
          }
          \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D((SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) d3DxextendedframePtr + 136L));
        }
        __fault
        {
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D), (void*) ((IntPtr) d3DxextendedframePtr + 120L));
        }
        \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D((SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) d3DxextendedframePtr + 120L));
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D), (void*) ((IntPtr) d3DxextendedframePtr + 104L));
      }
      \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D((SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) d3DxextendedframePtr + 104L));
      \u003CModule\u003E.delete((void*) d3DxextendedframePtr);
      this.m_pRoot = (D3DXEXTENDEDFRAME*) 0L;
    }
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pNode = this.m_pNode;
    if ((IntPtr) pNode == IntPtr.Zero)
      return;
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr1 = pNode;
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr2 = pointerFableModGfxNodePtr1;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) pointerFableModGfxNodePtr1)((IntPtr) pointerFableModGfxNodePtr2, 1U);
    this.m_pNode = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
  }

  public virtual unsafe void ExportX(string fileName)
  {
    GfxBaseModel.ExportToX(fileName, this.m_pRoot);
  }

  public virtual void CompileToEntry(AssetEntry entry)
  {
  }

  public virtual unsafe Node* GetGfx()
  {
    if ((IntPtr) this.m_pNode == IntPtr.Zero)
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr1 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr2;
      // ISSUE: fault handler
      try
      {
        if ((IntPtr) pointerFableModGfxNodePtr1 != IntPtr.Zero)
        {
          Node* nodePtr = this.BuildModel();
          *(long*) pointerFableModGfxNodePtr1 = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VNode\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
          *(long*) ((IntPtr) pointerFableModGfxNodePtr1 + 8L) = (long) nodePtr;
          if ((IntPtr) nodePtr != IntPtr.Zero)
            *(int*) ((IntPtr) nodePtr + 8L) = *(int*) ((IntPtr) nodePtr + 8L) + 1;
          pointerFableModGfxNodePtr2 = pointerFableModGfxNodePtr1;
        }
        else
          pointerFableModGfxNodePtr2 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) pointerFableModGfxNodePtr1);
      }
      this.m_pNode = pointerFableModGfxNodePtr2;
    }
    long num = *(long*) ((IntPtr) this.m_pNode + 8L);
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    Node* gfx = (Node*) __calli((__FnPtr<Spatial* (IntPtr)>) *(long*) (*(long*) num + 56L))((IntPtr) num);
    Node* nodePtr1 = gfx;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, byte)>) *(long*) (*(long*) gfx + 16L /*0x10*/))((IntPtr) nodePtr1, (byte) 1);
    return gfx;
  }

  public static unsafe Node* BuildGfx(D3DXEXTENDEDFRAME* pFrame, Node* pParent)
  {
    Node* nodePtr1 = (Node*) \u003CModule\u003E.@new(304UL);
    Node* pParent1;
    // ISSUE: fault handler
    try
    {
      pParent1 = (IntPtr) nodePtr1 == IntPtr.Zero ? (Node*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002ENode\u002E\u007Bctor\u007D(nodePtr1);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) nodePtr1);
    }
    ulong num1 = (ulong) *(long*) ((IntPtr) pFrame + 72L);
    if (num1 != 0UL)
    {
      _D3DXMESHCONTAINER* d3DxmeshcontainerPtr = (_D3DXMESHCONTAINER*) num1;
      if (*(long*) ((IntPtr) d3DxmeshcontainerPtr + 16L /*0x10*/) != 0L)
      {
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
          D3DXModel.SMaterial smaterial;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(long&) ((IntPtr) &smaterial + 72) = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VTexture\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(long&) ((IntPtr) &smaterial + 80 /*0x50*/) = 0L;
          // ISSUE: fault handler
          try
          {
            // ISSUE: cpblk instruction
            __memcpy(ref smaterial, *(long*) ((IntPtr) d3DxmeshcontainerPtr + 24L), 68);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(sbyte&) ((IntPtr) &smaterial + 88) = (sbyte) *(byte*) ((IntPtr) pFrame + 152L);
            \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u003D((SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) &smaterial + 72), (Texture*) *(long*) ((IntPtr) pFrame + 112L /*0x70*/));
            \u003CModule\u003E.FableMod\u002EGfx\u002ED3DXModel\u002EBuild(d3DxModelPtr2, (ID3DXMesh*) *(long*) ((IntPtr) d3DxmeshcontainerPtr + 16L /*0x10*/), &smaterial, 1U);
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
              float num2 = *(float*) ((IntPtr) pFrame + 64L /*0x40*/);
              float num3 = *(float*) ((IntPtr) pFrame + 60L);
              D3DXVECTOR3 d3DxvectoR3;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ref d3DxvectoR3 = *(float*) ((IntPtr) pFrame + 56L);
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ((IntPtr) &d3DxvectoR3 + 4) = num3;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ((IntPtr) &d3DxvectoR3 + 8) = num2;
              *(int*) ((IntPtr) meshPtr2 + 32L /*0x20*/) = *(int*) ((IntPtr) meshPtr2 + 32L /*0x20*/) | 1;
              // ISSUE: cpblk instruction
              __memcpy((IntPtr) meshPtr2 + 100L, ref d3DxvectoR3, 12);
              D3DXMATRIX d3Dxmatrix;
              \u003CModule\u003E.D3DXMatrixRotationZ(&d3Dxmatrix, -3.14159274f);
              \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002ESetRotation((Spatial*) meshPtr2, &d3Dxmatrix);
              Node* nodePtr2 = pParent1;
              Mesh* meshPtr3 = meshPtr2;
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) pParent1 + 120L))((IntPtr) nodePtr2, (Spatial*) meshPtr3);
            }
            __fault
            {
              // ISSUE: method pointer
              // ISSUE: cast to a function pointer type
              \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E\u002E\u007Bdtor\u007D), (void*) &pointerFableModGfxMesh);
            }
            \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E\u002E\u007Bdtor\u007D(&pointerFableModGfxMesh);
          }
          __fault
          {
            // ISSUE: method pointer
            // ISSUE: cast to a function pointer type
            \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ED3DXModel\u002ESMaterial\u002E\u007Bdtor\u007D), (void*) &smaterial);
          }
          \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D((SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) &smaterial + 72));
        }
        __fault
        {
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AD3DXModel\u003E\u002E\u007Bdtor\u007D), (void*) &fableModGfxD3DxModel);
        }
        \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AD3DXModel\u003E\u002E\u007Bdtor\u007D(&fableModGfxD3DxModel);
      }
    }
    ulong pFrame1 = (ulong) *(long*) ((IntPtr) pFrame + 88L);
    if (pFrame1 != 0UL)
      GfxBaseModel.BuildGfx((D3DXEXTENDEDFRAME*) pFrame1, pParent1);
    ulong pFrame2 = (ulong) *(long*) ((IntPtr) pFrame + 80L /*0x50*/);
    if (pFrame2 != 0UL)
      GfxBaseModel.BuildGfx((D3DXEXTENDEDFRAME*) pFrame2, pParent1);
    if ((IntPtr) pParent != IntPtr.Zero)
    {
      Node* nodePtr3 = pParent;
      Node* nodePtr4 = pParent1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) pParent + 120L))((IntPtr) nodePtr3, (Spatial*) nodePtr4);
    }
    return pParent1;
  }

  public static unsafe void ExportToX(string fileName, D3DXEXTENDEDFRAME* pRoot)
  {
    IntPtr hglobalUni = Marshal.StringToHGlobalUni(fileName);
    if (\u003CModule\u003E.D3DXSaveMeshHierarchyToFileW((char*) hglobalUni.ToPointer(), 1U, (_D3DXFRAME*) pRoot, (ID3DXAnimationController*) 0L, (ID3DXSaveUserData*) 0L) < 0)
      throw new System.Exception("Failed to export file");
    Marshal.FreeHGlobal(hglobalUni);
  }

  protected virtual unsafe Node* BuildModel() => GfxBaseModel.BuildGfx(this.m_pRoot, (Node*) 0L);

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EGfxBaseModel();
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
