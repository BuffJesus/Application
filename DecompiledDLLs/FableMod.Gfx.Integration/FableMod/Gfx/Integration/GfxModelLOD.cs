// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.GfxModelLOD
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using FableMod.Data;
using std;
using System;
using System.Globalization;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Gfx.Integration;

public class GfxModelLOD : IDisposable
{
  internal unsafe CompiledModel* m_CompiledModel;
  private bool m_CompiledSynced;
  private unsafe D3DXEXTENDEDFRAME* m_SceneRoot = (D3DXEXTENDEDFRAME*) 0L;
  private unsafe SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* m_pNode = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;

  public unsafe GfxModelLOD(sbyte* data, uint length)
  {
    CompiledModel* compiledModelPtr1 = (CompiledModel*) \u003CModule\u003E.@new(220UL);
    CompiledModel* compiledModelPtr2;
    // ISSUE: fault handler
    try
    {
      compiledModelPtr2 = (IntPtr) compiledModelPtr1 == IntPtr.Zero ? (CompiledModel*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002E\u007Bctor\u007D(compiledModelPtr1, data, &length);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) compiledModelPtr1);
    }
    this.m_CompiledModel = compiledModelPtr2;
  }

  private void \u007EGfxModelLOD() => this.\u0021GfxModelLOD();

  private unsafe void \u0021GfxModelLOD()
  {
    CompiledModel* compiledModel = this.m_CompiledModel;
    if ((IntPtr) compiledModel != IntPtr.Zero)
    {
      CompiledModel* compiledModelPtr = compiledModel;
      \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002E\u007Bdtor\u007D(compiledModelPtr);
      \u003CModule\u003E.delete((void*) compiledModelPtr);
      this.m_CompiledModel = (CompiledModel*) 0L;
    }
    this.DestroyFrames();
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

  public unsafe void DestroyCompiledModel()
  {
    CompiledModel* compiledModel = this.m_CompiledModel;
    if ((IntPtr) compiledModel == IntPtr.Zero)
      return;
    CompiledModel* compiledModelPtr = compiledModel;
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002E\u007Bdtor\u007D(compiledModelPtr);
    \u003CModule\u003E.delete((void*) compiledModelPtr);
    this.m_CompiledModel = (CompiledModel*) 0L;
  }

  public unsafe void DestroyFrames()
  {
    D3DXEXTENDEDFRAME* sceneRoot1 = this.m_SceneRoot;
    if ((IntPtr) sceneRoot1 == IntPtr.Zero)
      return;
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ED3DXEXTENDEDFRAME\u002EDestroy(sceneRoot1);
    D3DXEXTENDEDFRAME* sceneRoot2 = this.m_SceneRoot;
    if ((IntPtr) sceneRoot2 != IntPtr.Zero)
    {
      // ISSUE: fault handler
      try
      {
        // ISSUE: fault handler
        try
        {
          // ISSUE: fault handler
          try
          {
            \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ED3DXEXTENDEDFRAME\u002EDestroy(sceneRoot2);
          }
          __fault
          {
            // ISSUE: method pointer
            // ISSUE: cast to a function pointer type
            \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D), (void*) ((IntPtr) sceneRoot2 + 136L));
          }
          \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D((SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) sceneRoot2 + 136L));
        }
        __fault
        {
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D), (void*) ((IntPtr) sceneRoot2 + 120L));
        }
        \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D((SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) sceneRoot2 + 120L));
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D), (void*) ((IntPtr) sceneRoot2 + 104L));
      }
      \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D((SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) sceneRoot2 + 104L));
      \u003CModule\u003E.delete((void*) sceneRoot2);
    }
    this.m_SceneRoot = (D3DXEXTENDEDFRAME*) 0L;
  }

  public unsafe void Save(sbyte* data, uint* length)
  {
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002ESaveToBuffer(this.m_CompiledModel, data, (int*) length);
  }

  public unsafe void ExportX(string fileName)
  {
    if ((IntPtr) this.m_SceneRoot == IntPtr.Zero)
      this.CreateFrameFromCompiled();
    GfxBaseModel.ExportToX(fileName, this.m_SceneRoot);
    this.ExportTextures(fileName.Substring(0, fileName.Length - Path.GetFileName(fileName).Length));
  }

  public unsafe void ImportX(string fileName)
  {
    AllocateHierarchy* allocateHierarchyPtr1 = (AllocateHierarchy*) \u003CModule\u003E.@new(8UL);
    AllocateHierarchy* allocateHierarchyPtr2;
    if ((IntPtr) allocateHierarchyPtr1 != IntPtr.Zero)
    {
      *(long*) allocateHierarchyPtr1 = (long) &\u003CModule\u003E.\u003F\u003F_7AllocateHierarchy\u0040Integration\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
      allocateHierarchyPtr2 = allocateHierarchyPtr1;
    }
    else
      allocateHierarchyPtr2 = (AllocateHierarchy*) 0L;
    IDirect3DDevice9* idirect3Ddevice9Ptr = (IDirect3DDevice9*) *(long*) ((IntPtr) \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA + 8L);
    IntPtr hglobalUni = Marshal.StringToHGlobalUni(fileName);
    _D3DXFRAME* frame1;
    try
    {
      ID3DXAnimationController* animationControllerPtr;
      \u003CModule\u003E.D3DXLoadMeshHierarchyFromXW((char*) hglobalUni.ToPointer(), 544U, idirect3Ddevice9Ptr, (ID3DXAllocateHierarchy*) allocateHierarchyPtr2, (ID3DXLoadUserData*) 0L, &frame1, &animationControllerPtr);
    }
    catch (System.Exception ex)
    {
      int num = (int) MessageBox.Show(ex.Message, "Exception during X-File parsing.", MessageBoxButtons.OK, MessageBoxIcon.Hand);
      Marshal.FreeHGlobal(hglobalUni);
      \u003CModule\u003E.delete((void*) allocateHierarchyPtr2);
      return;
    }
    Marshal.FreeHGlobal(hglobalUni);
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EDestroySubMeshes(this.m_CompiledModel);
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EDestroyMaterials(this.m_CompiledModel);
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EDestroyBones(this.m_CompiledModel);
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EDestroyHelpers(this.m_CompiledModel);
    D3DXMATRIX d3Dxmatrix1;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 56) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 52) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 48 /*0x30*/) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 44) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 36) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 32 /*0x20*/) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 28) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 24) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 16 /*0x10*/) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 12) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 8) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 4) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 60) = 1f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 40) = 1f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 20) = 1f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3Dxmatrix1 = 1f;
    _D3DXFRAME* frame2 = \u003CModule\u003E.D3DXFrameFind(frame1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0P\u0040HODBKJBP\u0040Movement_dummy\u003F\u0024AA\u0040);
    if ((IntPtr) frame2 == IntPtr.Zero)
    {
      frame2 = \u003CModule\u003E.D3DXFrameFind(frame1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_08IMCBCIPL\u0040Movement\u003F\u0024AA\u0040);
      if ((IntPtr) frame2 == IntPtr.Zero)
      {
        frame2 = \u003CModule\u003E.D3DXFrameFind(frame1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0O\u0040PFDLJPFL\u0040Bow_bone_main\u003F\u0024AA\u0040);
        if ((IntPtr) frame2 == IntPtr.Zero)
        {
          frame2 = \u003CModule\u003E.D3DXFrameFind(frame1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_09FLFDMKOE\u0040root_bone\u003F\u0024AA\u0040);
          if ((IntPtr) frame2 == IntPtr.Zero)
          {
            frame2 = \u003CModule\u003E.D3DXFrameFind(frame1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0P\u0040GFPCKMMN\u0040Main_Root_Bone\u003F\u0024AA\u0040);
            if ((IntPtr) frame2 == IntPtr.Zero)
              goto label_19;
          }
        }
      }
    }
    sbyte* bonename1 = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EStringFromXFileString((sbyte*) *(long*) frame1);
    int num1 = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EAddBone(this.m_CompiledModel, bonename1);
    long num2 = (long) num1;
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EBONE_SUB_CHUNK_2\u002ESetMatrix((BONE_SUB_CHUNK_2*) (num2 * 48L /*0x30*/ + *(long*) ((IntPtr) this.m_CompiledModel + 140L)), (D3DXMATRIX*) ((IntPtr) frame1 + 8L));
    long num3 = num2 * 60L;
    *(int*) (num3 + *(long*) ((IntPtr) this.m_CompiledModel + 132L) + 4L) = -1;
    D3DXMATRIX d3Dxmatrix2;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix2 + 56) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix2 + 52) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix2 + 48 /*0x30*/) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix2 + 44) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix2 + 36) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix2 + 32 /*0x20*/) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix2 + 28) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix2 + 24) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix2 + 16 /*0x10*/) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix2 + 12) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix2 + 8) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix2 + 4) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix2 + 60) = 1f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix2 + 40) = 1f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix2 + 20) = 1f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3Dxmatrix2 = 1f;
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EBONE_SUB_CHUNK_1\u002ESetMatrix((BONE_SUB_CHUNK_1*) (*(long*) ((IntPtr) this.m_CompiledModel + 132L) + num3), &d3Dxmatrix2);
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EBONE_SUB_CHUNK_3\u002ESetMatrix((BONE_SUB_CHUNK_3*) (num2 * 64L /*0x40*/ + *(long*) ((IntPtr) this.m_CompiledModel + 148L)), &d3Dxmatrix2);
    \u003CModule\u003E.delete\u005B\u005D((void*) bonename1);
    sbyte* bonename2 = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EStringFromXFileString((sbyte*) *(long*) frame2);
    long num4 = (long) \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EAddBone(this.m_CompiledModel, bonename2);
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EBONE_SUB_CHUNK_2\u002ESetMatrix((BONE_SUB_CHUNK_2*) (num4 * 48L /*0x30*/ + *(long*) ((IntPtr) this.m_CompiledModel + 140L)), (D3DXMATRIX*) ((IntPtr) frame2 + 8L));
    long num5 = num4 * 60L;
    *(int*) (num5 + *(long*) ((IntPtr) this.m_CompiledModel + 132L) + 4L) = num1;
    long num6 = num3 + *(long*) ((IntPtr) this.m_CompiledModel + 132L) + 8L;
    *(int*) num6 = *(int*) num6 + 1;
    _D3DXFRAME* d3DxframePtr = (_D3DXFRAME*) *(long*) ((IntPtr) frame2 + 88L);
    if ((IntPtr) d3DxframePtr != IntPtr.Zero)
    {
      do
      {
        sbyte* numPtr = (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BD\u0040GNOHACJB\u0040BONE_OFFSET_MATRIX\u003F\u0024AA\u0040;
        long num7 = *(long*) d3DxframePtr;
        sbyte num8 = *(sbyte*) num7;
        sbyte num9 = 66;
        if (num8 >= (sbyte) 66)
        {
          do
          {
            if ((int) num8 <= (int) num9)
            {
              if (num8 != (sbyte) 0)
              {
                ++num7;
                ++numPtr;
                num8 = *(sbyte*) num7;
                num9 = *numPtr;
              }
              else
                goto label_17;
            }
            else
              break;
          }
          while ((int) num8 >= (int) num9);
        }
        d3DxframePtr = (_D3DXFRAME*) *(long*) ((IntPtr) d3DxframePtr + 80L /*0x50*/);
      }
      while ((IntPtr) d3DxframePtr != IntPtr.Zero);
      goto label_18;
label_17:
      _D3DXFRAME* mat = (_D3DXFRAME*) ((IntPtr) d3DxframePtr + 8L);
      \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EBONE_SUB_CHUNK_1\u002ESetMatrix((BONE_SUB_CHUNK_1*) (*(long*) ((IntPtr) this.m_CompiledModel + 132L) + num5), (D3DXMATRIX*) mat);
      \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EBONE_SUB_CHUNK_3\u002ESetMatrix((BONE_SUB_CHUNK_3*) (num4 * 64L /*0x40*/ + *(long*) ((IntPtr) this.m_CompiledModel + 148L)), (D3DXMATRIX*) mat);
    }
label_18:
    \u003CModule\u003E.delete\u005B\u005D((void*) bonename2);
    this.ImportBones(frame2);
label_19:
    this.ImportMeshesFromFrame(frame1, &d3Dxmatrix1);
    this.ImportHelpers(frame1, &d3Dxmatrix1);
    this.CreateFrameFromCompiled();
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

  public unsafe void ImportObj(string fileName)
  {
    TextReader textReader = (TextReader) new StreamReader(fileName);
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EDestroySubMeshes(this.m_CompiledModel);
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EDestroyMaterials(this.m_CompiledModel);
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EDestroyBones(this.m_CompiledModel);
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EDestroyHelpers(this.m_CompiledModel);
    vector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E allocatorD3DxvectoR3_1;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ref allocatorD3DxvectoR3_1 = 0L;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ((IntPtr) &allocatorD3DxvectoR3_1 + 8) = 0L;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ((IntPtr) &allocatorD3DxvectoR3_1 + 16 /*0x10*/) = 0L;
    vector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E allocatorD3DxvectoR3_2;
    vector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E allocatorD3DxvectoR2_1;
    vector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E allocatorD3DxvectoR3_3;
    vector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E allocatorD3DxvectoR3_4;
    vector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E allocatorD3DxvectoR2_2;
    vector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E intStdAllocatorInt1;
    vector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E intStdAllocatorInt2;
    // ISSUE: fault handler
    try
    {
      \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002Ereserve(&allocatorD3DxvectoR3_1, 8000UL);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(long&) ref allocatorD3DxvectoR3_2 = 0L;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(long&) ((IntPtr) &allocatorD3DxvectoR3_2 + 8) = 0L;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(long&) ((IntPtr) &allocatorD3DxvectoR3_2 + 16 /*0x10*/) = 0L;
      // ISSUE: fault handler
      try
      {
        \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002Ereserve(&allocatorD3DxvectoR3_2, 8000UL);
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(long&) ref allocatorD3DxvectoR2_1 = 0L;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(long&) ((IntPtr) &allocatorD3DxvectoR2_1 + 8) = 0L;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(long&) ((IntPtr) &allocatorD3DxvectoR2_1 + 16 /*0x10*/) = 0L;
        // ISSUE: fault handler
        try
        {
          \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E\u002Ereserve(&allocatorD3DxvectoR2_1, 8000UL);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(long&) ref allocatorD3DxvectoR3_3 = 0L;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(long&) ((IntPtr) &allocatorD3DxvectoR3_3 + 8) = 0L;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(long&) ((IntPtr) &allocatorD3DxvectoR3_3 + 16 /*0x10*/) = 0L;
          // ISSUE: fault handler
          try
          {
            \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002Ereserve(&allocatorD3DxvectoR3_3, 8000UL);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(long&) ref allocatorD3DxvectoR3_4 = 0L;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(long&) ((IntPtr) &allocatorD3DxvectoR3_4 + 8) = 0L;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(long&) ((IntPtr) &allocatorD3DxvectoR3_4 + 16 /*0x10*/) = 0L;
            // ISSUE: fault handler
            try
            {
              \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002Ereserve(&allocatorD3DxvectoR3_4, 8000UL);
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(long&) ref allocatorD3DxvectoR2_2 = 0L;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(long&) ((IntPtr) &allocatorD3DxvectoR2_2 + 8) = 0L;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(long&) ((IntPtr) &allocatorD3DxvectoR2_2 + 16 /*0x10*/) = 0L;
              // ISSUE: fault handler
              try
              {
                \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E\u002Ereserve(&allocatorD3DxvectoR2_2, 8000UL);
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(long&) ref intStdAllocatorInt1 = 0L;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(long&) ((IntPtr) &intStdAllocatorInt1 + 8) = 0L;
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(long&) ((IntPtr) &intStdAllocatorInt1 + 16 /*0x10*/) = 0L;
                // ISSUE: fault handler
                try
                {
                  \u003CModule\u003E.std\u002Evector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E\u002Ereserve(&intStdAllocatorInt1, 8000UL);
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  ^(long&) ref intStdAllocatorInt2 = 0L;
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  ^(long&) ((IntPtr) &intStdAllocatorInt2 + 8) = 0L;
                  // ISSUE: cast to a reference type
                  // ISSUE: explicit reference operation
                  ^(long&) ((IntPtr) &intStdAllocatorInt2 + 16 /*0x10*/) = 0L;
                  // ISSUE: fault handler
                  try
                  {
                    \u003CModule\u003E.std\u002Evector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E\u002Ereserve(&intStdAllocatorInt2, 8000UL);
                    int iMaterial = -1;
                    char[] chArray = new char[2]
                    {
                      ' ',
                      '\n'
                    };
                    bool flag = false;
                    CultureInfo provider = new CultureInfo("en-US");
                    string str = textReader.ReadLine();
                    if (str != (string) null)
                    {
                      do
                      {
                        string[] strArray = str.Split(chArray);
                        int index1 = 0;
                        if (0 < strArray.Length)
                        {
                          do
                          {
                            switch (strArray[index1])
                            {
                              case "#":
                                goto label_12;
                              case "mtllib":
                                goto label_37;
                              case "usemtl":
                                goto label_14;
                              case "g":
                              case "o":
                                goto label_34;
                              case "v":
                                goto label_21;
                              case "vn":
                                goto label_22;
                              case "vt":
                                goto label_23;
                              case "f":
                                goto label_26;
                              default:
                                ++index1;
                                continue;
                            }
                          }
                          while (index1 < strArray.Length);
                          goto label_37;
label_12:
                          switch (str)
                          {
                            case "# www.blender3d.org":
                              flag = true;
                              goto label_37;
                            default:
                              goto label_37;
                          }
label_14:
                          // ISSUE: cast to a reference type
                          // ISSUE: explicit reference operation
                          // ISSUE: cast to a reference type
                          // ISSUE: explicit reference operation
                          if ((ulong) (^(long&) ((IntPtr) &allocatorD3DxvectoR3_3 + 8) - ^(long&) ref allocatorD3DxvectoR3_3) / 12UL > 0UL)
                            this.BuildObjMesh(&allocatorD3DxvectoR3_3, &allocatorD3DxvectoR3_4, &allocatorD3DxvectoR2_2, &intStdAllocatorInt1, iMaterial);
                          \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002Eclear(&allocatorD3DxvectoR3_3);
                          \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E\u002Eclear(&allocatorD3DxvectoR2_2);
                          \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002Eclear(&allocatorD3DxvectoR3_4);
                          \u003CModule\u003E.std\u002Evector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E\u002Eclear(&intStdAllocatorInt1);
                          \u003CModule\u003E.std\u002Evector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E\u002Eclear(&intStdAllocatorInt2);
                          iMaterial = *(int*) ((IntPtr) this.m_CompiledModel + 95L);
                          IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi(strArray[index1 + 1]);
                          sbyte* pointer = (sbyte*) hglobalAnsi.ToPointer();
                          CompiledModel* compiledModel = this.m_CompiledModel;
                          int id = *(int*) ((IntPtr) compiledModel + 95L);
                          MTRL* mtrlPtr = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EAddMaterial(compiledModel, (uint) id);
                          sbyte* numPtr1 = pointer;
                          if (*pointer != (sbyte) 0)
                          {
                            do
                            {
                              ++numPtr1;
                            }
                            while (*numPtr1 != (sbyte) 0);
                          }
                          sbyte* numPtr2 = (sbyte*) \u003CModule\u003E.new\u005B\u005D((ulong) ((IntPtr) numPtr1 - (IntPtr) pointer + 1L));
                          *(long*) ((IntPtr) mtrlPtr + 4L) = (long) numPtr2;
                          sbyte* numPtr3 = pointer;
                          long index2 = (long) ((IntPtr) numPtr2 - (IntPtr) pointer);
                          sbyte num1;
                          do
                          {
                            num1 = *numPtr3;
                            numPtr3[index2] = num1;
                            ++numPtr3;
                          }
                          while (num1 != (sbyte) 0);
                          Marshal.FreeHGlobal(hglobalAnsi);
                          goto label_37;
label_21:
                          float num2 = float.Parse(strArray[index1 + 3], (IFormatProvider) provider);
                          float num3 = float.Parse(strArray[index1 + 2], (IFormatProvider) provider);
                          D3DXVECTOR3 d3DxvectoR3_1;
                          // ISSUE: cast to a reference type
                          // ISSUE: explicit reference operation
                          ^(float&) ref d3DxvectoR3_1 = float.Parse(strArray[index1 + 1], (IFormatProvider) provider);
                          // ISSUE: cast to a reference type
                          // ISSUE: explicit reference operation
                          ^(float&) ((IntPtr) &d3DxvectoR3_1 + 4) = num3;
                          // ISSUE: cast to a reference type
                          // ISSUE: explicit reference operation
                          ^(float&) ((IntPtr) &d3DxvectoR3_1 + 8) = num2;
                          \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002Epush_back(&allocatorD3DxvectoR3_1, &d3DxvectoR3_1);
                          goto label_37;
label_22:
                          float num4 = float.Parse(strArray[index1 + 3], (IFormatProvider) provider);
                          float num5 = float.Parse(strArray[index1 + 2], (IFormatProvider) provider);
                          D3DXVECTOR3 d3DxvectoR3_2;
                          // ISSUE: cast to a reference type
                          // ISSUE: explicit reference operation
                          ^(float&) ref d3DxvectoR3_2 = float.Parse(strArray[index1 + 1], (IFormatProvider) provider);
                          // ISSUE: cast to a reference type
                          // ISSUE: explicit reference operation
                          ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4) = num5;
                          // ISSUE: cast to a reference type
                          // ISSUE: explicit reference operation
                          ^(float&) ((IntPtr) &d3DxvectoR3_2 + 8) = num4;
                          \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002Epush_back(&allocatorD3DxvectoR3_2, &d3DxvectoR3_2);
                          goto label_37;
label_23:
                          float num6 = float.Parse(strArray[index1 + 1], (IFormatProvider) provider);
                          float num7 = float.Parse(strArray[index1 + 2], (IFormatProvider) provider);
                          if (flag)
                            num7 *= -1f;
                          D3DXVECTOR2 d3DxvectoR2;
                          // ISSUE: cast to a reference type
                          // ISSUE: explicit reference operation
                          ^(float&) ref d3DxvectoR2 = num6;
                          // ISSUE: cast to a reference type
                          // ISSUE: explicit reference operation
                          ^(float&) ((IntPtr) &d3DxvectoR2 + 4) = num7;
                          \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E\u002Epush_back(&allocatorD3DxvectoR2_1, &d3DxvectoR2);
                          goto label_37;
label_26:
                          switch (strArray.Length)
                          {
                            case 4:
                              int num8 = 1;
                              do
                              {
                                int _Pos1;
                                int _Pos2;
                                int _Pos3;
                                \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002E\u003FA0x23fa0976\u002EParseVertexIndices(strArray[num8 + index1], &_Pos1, &_Pos2, &_Pos3);
                                \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002Epush_back(&allocatorD3DxvectoR3_3, \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u005B\u005D(&allocatorD3DxvectoR3_1, (ulong) _Pos1));
                                if (_Pos2 >= 0 && !\u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002Eempty(&allocatorD3DxvectoR3_2))
                                  \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002Epush_back(&allocatorD3DxvectoR3_4, \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u005B\u005D(&allocatorD3DxvectoR3_2, (ulong) _Pos2));
                                if (_Pos3 >= 0)
                                  \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E\u002Epush_back(&allocatorD3DxvectoR2_2, \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E\u002E\u005B\u005D(&allocatorD3DxvectoR2_1, (ulong) _Pos3));
                                ++num8;
                              }
                              while (num8 <= 3);
                              int num9 = (int) \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002Esize(&allocatorD3DxvectoR3_3);
                              int num10 = num9 + 2;
                              \u003CModule\u003E.std\u002Evector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E\u002Epush_back(&intStdAllocatorInt1, &num10);
                              int num11 = num9 + 1;
                              \u003CModule\u003E.std\u002Evector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E\u002Epush_back(&intStdAllocatorInt1, &num11);
                              int num12 = num9;
                              \u003CModule\u003E.std\u002Evector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E\u002Epush_back(&intStdAllocatorInt1, &num12);
                              \u003CModule\u003E.std\u002Evector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E\u002Epush_back(&intStdAllocatorInt2, &iMaterial);
                              goto label_37;
                            case 5:
                              goto label_38;
                            default:
                              goto label_39;
                          }
label_34:
                          // ISSUE: cast to a reference type
                          // ISSUE: explicit reference operation
                          // ISSUE: cast to a reference type
                          // ISSUE: explicit reference operation
                          if ((ulong) (^(long&) ((IntPtr) &allocatorD3DxvectoR3_3 + 8) - ^(long&) ref allocatorD3DxvectoR3_3) / 12UL > 0UL)
                            this.BuildObjMesh(&allocatorD3DxvectoR3_3, &allocatorD3DxvectoR3_4, &allocatorD3DxvectoR2_2, &intStdAllocatorInt1, iMaterial);
                          \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002Eclear(&allocatorD3DxvectoR3_3);
                          \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E\u002Eclear(&allocatorD3DxvectoR2_2);
                          \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002Eclear(&allocatorD3DxvectoR3_4);
                          \u003CModule\u003E.std\u002Evector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E\u002Eclear(&intStdAllocatorInt1);
                          \u003CModule\u003E.std\u002Evector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E\u002Eclear(&intStdAllocatorInt2);
                        }
label_37:
                        str = textReader.ReadLine();
                      }
                      while (str != (string) null);
                      goto label_40;
label_38:
                      throw new System.Exception("Triangulated mesh required");
label_39:
                      throw new System.Exception("File format not supported");
label_40:
                      if (iMaterial >= 0)
                        goto label_42;
                    }
                    CompiledModel* compiledModel1 = this.m_CompiledModel;
                    int id1 = *(int*) ((IntPtr) compiledModel1 + 95L);
                    iMaterial = id1;
                    MTRL* mtrlPtr1 = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EAddMaterial(compiledModel1, (uint) id1);
                    sbyte* numPtr = (sbyte*) \u003CModule\u003E.new\u005B\u005D(8UL);
                    *(long*) ((IntPtr) mtrlPtr1 + 4L) = (long) numPtr;
                    // ISSUE: cpblk instruction
                    __memcpy((IntPtr) numPtr, ref \u003CModule\u003E.\u003F\u003F_C\u0040_07MGCPDNLD\u0040DEFAULT\u003F\u0024AA\u0040, 8);
label_42:
                    // ISSUE: cast to a reference type
                    // ISSUE: explicit reference operation
                    // ISSUE: cast to a reference type
                    // ISSUE: explicit reference operation
                    if ((ulong) (^(long&) ((IntPtr) &allocatorD3DxvectoR3_3 + 8) - ^(long&) ref allocatorD3DxvectoR3_3) / 12UL > 0UL)
                      this.BuildObjMesh(&allocatorD3DxvectoR3_3, &allocatorD3DxvectoR3_4, &allocatorD3DxvectoR2_2, &intStdAllocatorInt1, iMaterial);
                    textReader.Close();
                    this.CreateFrameFromCompiled();
                    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pNode = this.m_pNode;
                    if ((IntPtr) pNode != IntPtr.Zero)
                    {
                      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr1 = pNode;
                      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr2 = pointerFableModGfxNodePtr1;
                      // ISSUE: cast to a function pointer type
                      // ISSUE: function pointer call
                      void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) pointerFableModGfxNodePtr1)((IntPtr) pointerFableModGfxNodePtr2, 1U);
                      this.m_pNode = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
                    }
                  }
                  __fault
                  {
                    // ISSUE: method pointer
                    // ISSUE: cast to a function pointer type
                    \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(std\u002Evector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &intStdAllocatorInt2);
                  }
                  \u003CModule\u003E.std\u002Evector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E\u002E\u007Bdtor\u007D(&intStdAllocatorInt2);
                }
                __fault
                {
                  // ISSUE: method pointer
                  // ISSUE: cast to a function pointer type
                  \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(std\u002Evector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &intStdAllocatorInt1);
                }
                \u003CModule\u003E.std\u002Evector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E\u002E\u007Bdtor\u007D(&intStdAllocatorInt1);
              }
              __fault
              {
                // ISSUE: method pointer
                // ISSUE: cast to a function pointer type
                \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(std\u002Evector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &allocatorD3DxvectoR2_2);
              }
              \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E\u002E\u007Bdtor\u007D(&allocatorD3DxvectoR2_2);
            }
            __fault
            {
              // ISSUE: method pointer
              // ISSUE: cast to a function pointer type
              \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &allocatorD3DxvectoR3_4);
            }
            \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u007Bdtor\u007D(&allocatorD3DxvectoR3_4);
          }
          __fault
          {
            // ISSUE: method pointer
            // ISSUE: cast to a function pointer type
            \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &allocatorD3DxvectoR3_3);
          }
          \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u007Bdtor\u007D(&allocatorD3DxvectoR3_3);
        }
        __fault
        {
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(std\u002Evector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &allocatorD3DxvectoR2_1);
        }
        \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E\u002E\u007Bdtor\u007D(&allocatorD3DxvectoR2_1);
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &allocatorD3DxvectoR3_2);
      }
      \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u007Bdtor\u007D(&allocatorD3DxvectoR3_2);
    }
    __fault
    {
      // ISSUE: method pointer
      // ISSUE: cast to a function pointer type
      \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &allocatorD3DxvectoR3_1);
    }
    \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u007Bdtor\u007D(&allocatorD3DxvectoR3_1);
    // ISSUE: fault handler
    try
    {
      // ISSUE: fault handler
      try
      {
        // ISSUE: fault handler
        try
        {
          // ISSUE: fault handler
          try
          {
            // ISSUE: fault handler
            try
            {
              // ISSUE: fault handler
              try
              {
                // ISSUE: fault handler
                try
                {
                  // ISSUE: fault handler
                  try
                  {
                  }
                  __fault
                  {
                    // ISSUE: method pointer
                    // ISSUE: cast to a function pointer type
                    \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(std\u002Evector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &intStdAllocatorInt2);
                  }
                }
                __fault
                {
                  // ISSUE: method pointer
                  // ISSUE: cast to a function pointer type
                  \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(std\u002Evector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &intStdAllocatorInt1);
                }
              }
              __fault
              {
                // ISSUE: method pointer
                // ISSUE: cast to a function pointer type
                \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(std\u002Evector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &allocatorD3DxvectoR2_2);
              }
            }
            __fault
            {
              // ISSUE: method pointer
              // ISSUE: cast to a function pointer type
              \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &allocatorD3DxvectoR3_4);
            }
          }
          __fault
          {
            // ISSUE: method pointer
            // ISSUE: cast to a function pointer type
            \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &allocatorD3DxvectoR3_3);
          }
        }
        __fault
        {
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(std\u002Evector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &allocatorD3DxvectoR2_1);
        }
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &allocatorD3DxvectoR3_2);
      }
    }
    __fault
    {
      // ISSUE: method pointer
      // ISSUE: cast to a function pointer type
      \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u007Bdtor\u007D), (void*) &allocatorD3DxvectoR3_1);
    }
  }

  public unsafe int MaterialCount => *(int*) ((IntPtr) this.m_CompiledModel + 95L);

  public unsafe Material get_Materials(uint index)
  {
    CompiledModel* compiledModel = this.m_CompiledModel;
    return index < (uint) *(int*) ((IntPtr) compiledModel + 95L) ? new Material((MTRL*) ((long) index * 45L + *(long*) ((IntPtr) compiledModel + 204L))) : (Material) null;
  }

  public unsafe Material AddMaterial(uint id)
  {
    return new Material(\u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EAddMaterial(this.m_CompiledModel, id));
  }

  public unsafe void RemoveMaterial(uint id)
  {
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002ERemoveMaterial(this.m_CompiledModel, id);
  }

  public unsafe int SubMeshCount => *(int*) ((IntPtr) this.m_CompiledModel + 99L);

  public unsafe SubMesh get_SubMeshes(uint index)
  {
    CompiledModel* compiledModel = this.m_CompiledModel;
    return index < (uint) *(int*) ((IntPtr) compiledModel + 99L) ? new SubMesh((SUBM*) *(long*) ((long) index * 8L + *(long*) ((IntPtr) compiledModel + 212L))) : (SubMesh) null;
  }

  public unsafe void BuildGfx(Node* pRoot)
  {
    if ((IntPtr) this.m_SceneRoot == IntPtr.Zero)
      this.CreateFrameFromCompiled();
    GfxBaseModel.BuildGfx(this.m_SceneRoot, pRoot);
  }

  public unsafe Node* GetGfx()
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
          *(long*) pointerFableModGfxNodePtr1 = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VNode\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
          *(long*) ((IntPtr) pointerFableModGfxNodePtr1 + 8L) = 0L;
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
      Node* nodePtr1 = (Node*) \u003CModule\u003E.@new(304UL);
      Node* nodePtr2;
      // ISSUE: fault handler
      try
      {
        nodePtr2 = (IntPtr) nodePtr1 == IntPtr.Zero ? (Node*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002ENode\u002E\u007Bctor\u007D(nodePtr1);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) nodePtr1);
      }
      if ((IntPtr) this.m_SceneRoot == IntPtr.Zero)
        this.CreateFrameFromCompiled();
      GfxBaseModel.BuildGfx(this.m_SceneRoot, nodePtr2);
      this.DestroyFrames();
      \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E\u002E\u003D(this.m_pNode, nodePtr2);
    }
    long num = *(long*) ((IntPtr) this.m_pNode + 8L);
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    return (Node*) __calli((__FnPtr<Spatial* (IntPtr)>) *(long*) (*(long*) num + 56L))((IntPtr) num);
  }

  private unsafe Texture* FindTexture(uint id)
  {
    GfxTexture gfxTexture = GfxManager.GetTextureManager().Get(id);
    return gfxTexture != null ? gfxTexture.GetBaseTexture() : (Texture*) 0L;
  }

  private unsafe void BuildObjMesh(
    vector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E* Vertices,
    vector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E* Normals,
    vector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E* TexCoords,
    vector\u003Cint\u002Cstd\u003A\u003Aallocator\u003Cint\u003E\u0020\u003E* Faces,
    int iMaterial)
  {
    SUBM* submPtr = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EAddSubMesh(this.m_CompiledModel);
    long num1 = *(long*) ((IntPtr) Faces + 8L) - *(long*) Faces >> 2;
    int num2 = (int) ((ulong) num1 / 3UL);
    ulong num3 = (ulong) num1;
    ushort* numPtr1 = (ushort*) \u003CModule\u003E.new\u005B\u005D(num3 > (ulong) long.MaxValue ? ulong.MaxValue : num3 * 2UL);
    int num4 = 0;
    long num5 = *(long*) Faces;
    if (0UL < (ulong) (*(long*) ((IntPtr) Faces + 8L) - num5 >> 2))
    {
      long num6 = 0;
      ushort* numPtr2 = numPtr1;
      do
      {
        *numPtr2 = (ushort) *(int*) (num6 + num5);
        ++num4;
        num6 += 4L;
        numPtr2 += 2L;
        num5 = *(long*) Faces;
      }
      while ((ulong) num4 < (ulong) (*(long*) ((IntPtr) Faces + 8L) - num5 >> 2));
    }
    ulong __n = (ulong) (*(long*) ((IntPtr) Vertices + 8L) - *(long*) Vertices) / 12UL;
    Vertex_20_20* __t = (Vertex_20_20*) \u003CModule\u003E.new\u005B\u005D(__n > 922337203685477580UL /*0x0CCCCCCCCCCCCCCC*/ ? ulong.MaxValue : __n * 20UL);
    Vertex_20_20* vertex2020Ptr1;
    // ISSUE: fault handler
    try
    {
      if ((IntPtr) __t != IntPtr.Zero)
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.__vec_ctor((void*) __t, 20UL, (int) __n, (__FnPtr<void* (void*)>) __methodptr(FableMod\u002EGfx\u002EIntegration\u002EVertex_20_20\u002E\u007Bctor\u007D));
        vertex2020Ptr1 = __t;
      }
      else
        vertex2020Ptr1 = (Vertex_20_20*) 0L;
    }
    __fault
    {
      \u003CModule\u003E.delete\u005B\u005D((void*) __t);
    }
    long num7 = *(long*) Vertices;
    long num8 = (*(long*) ((IntPtr) Vertices + 8L) - num7) / 12L;
    bool flag1 = (*(long*) ((IntPtr) Normals + 8L) - *(long*) Normals) / 12L == num8;
    bool flag2 = *(long*) ((IntPtr) TexCoords + 8L) - *(long*) TexCoords >> 3 == num8;
    int num9 = 0;
    if (0UL < (ulong) num8)
    {
      ulong _Pos = 0;
      long num10 = 0;
      Vertex_20_20* vertex2020Ptr2 = (Vertex_20_20*) ((IntPtr) vertex2020Ptr1 + 8L);
      do
      {
        *(float*) ((IntPtr) vertex2020Ptr2 - 8L) = *(float*) (num10 + num7);
        *(float*) ((IntPtr) vertex2020Ptr2 - 4L) = *(float*) (*(long*) Vertices + num10 + 4L);
        *(float*) vertex2020Ptr2 = *(float*) (*(long*) Vertices + num10 + 8L);
        if (flag1)
        {
          D3DXVECTOR3* d3DxvectoR3Ptr = \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u005B\u005D(Normals, _Pos);
          Vertex_20_20* vertex2020Ptr3 = (Vertex_20_20*) ((IntPtr) vertex2020Ptr1 + (long) num9 * 20L + 12L);
          \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EPackedXYZ\u002ESetX((PackedXYZ*) vertex2020Ptr3, *(float*) d3DxvectoR3Ptr);
          \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EPackedXYZ\u002ESetY((PackedXYZ*) vertex2020Ptr3, *(float*) ((IntPtr) \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u005B\u005D(Normals, _Pos) + 4L));
          \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EPackedXYZ\u002ESetZ((PackedXYZ*) vertex2020Ptr3, *(float*) ((IntPtr) \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR3\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR3\u003E\u0020\u003E\u002E\u005B\u005D(Normals, _Pos) + 8L));
        }
        if (flag2)
        {
          D3DXVECTOR2* d3DxvectoR2Ptr = \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E\u002E\u005B\u005D(TexCoords, _Pos);
          Vertex_20_20* vertex2020Ptr4 = (Vertex_20_20*) ((long) num9 * 20L + (IntPtr) vertex2020Ptr1);
          \u003CModule\u003E.CTFixed\u003Cshort\u002C11\u002C2048\u003E\u002E\u003D((CTFixed\u003Cshort\u002C11\u002C2048\u003E*) ((IntPtr) vertex2020Ptr4 + 16L /*0x10*/), *(float*) d3DxvectoR2Ptr);
          \u003CModule\u003E.CTFixed\u003Cshort\u002C11\u002C2048\u003E\u002E\u003D((CTFixed\u003Cshort\u002C11\u002C2048\u003E*) ((IntPtr) vertex2020Ptr4 + 18L), *(float*) ((IntPtr) \u003CModule\u003E.std\u002Evector\u003CD3DXVECTOR2\u002Cstd\u003A\u003Aallocator\u003CD3DXVECTOR2\u003E\u0020\u003E\u002E\u005B\u005D(TexCoords, _Pos) + 4L));
        }
        ++num9;
        num10 += 12L;
        vertex2020Ptr2 += 20L;
        num7 = *(long*) Vertices;
        _Pos = (ulong) num9;
      }
      while (_Pos < (ulong) (*(long*) ((IntPtr) Vertices + 8L) - num7) / 12UL);
    }
    *(int*) ((IntPtr) submPtr + 40L) = num2;
    *(int*) ((IntPtr) submPtr + 36L) = (int) (uint) ((ulong) (*(long*) ((IntPtr) Vertices + 8L) - *(long*) Vertices) / 12UL);
    *(long*) ((IntPtr) submPtr + 124L) = (long) numPtr1;
    *(int*) ((IntPtr) submPtr + 44L) = (int) (uint) (*(long*) ((IntPtr) Faces + 8L) - *(long*) Faces >> 2);
    *(float*) ((IntPtr) submPtr + 76L) = 1f;
    *(float*) ((IntPtr) submPtr + 80L /*0x50*/) = 1f;
    *(float*) ((IntPtr) submPtr + 84L) = 1f;
    *(float*) ((IntPtr) submPtr + 88L) = 1f;
    *(long*) ((IntPtr) submPtr + 116L) = (long) vertex2020Ptr1;
    *(int*) ((IntPtr) submPtr + 108L) = 20;
    *(int*) ((IntPtr) submPtr + 48L /*0x30*/) = 20;
    *(float*) ((IntPtr) submPtr + 8L) = 1.95445311f;
    *(float*) ((IntPtr) submPtr + 12L) = -73.63134f;
    *(float*) ((IntPtr) submPtr + 16L /*0x10*/) = 496.841583f;
    *(float*) ((IntPtr) submPtr + 20L) = 1592.58691f;
    *(float*) ((IntPtr) submPtr + 24L) = 0.0007336853f;
    *(int*) ((IntPtr) submPtr + 28L) = 1;
    *(int*) ((IntPtr) submPtr + 52L) = 1;
    void* voidPtr = \u003CModule\u003E.@new(15UL);
    *(long*) ((IntPtr) submPtr + 60L) = (long) voidPtr;
    // ISSUE: initblk instruction
    __memset((IntPtr) voidPtr, 0, 15);
    *(int*) *(long*) ((IntPtr) submPtr + 60L) = num2;
    *(int*) (*(long*) ((IntPtr) submPtr + 60L) + 4L) = 0;
    *(short*) (*(long*) ((IntPtr) submPtr + 60L) + 8L) = (short) 0;
    *(int*) submPtr = iMaterial;
  }

  private unsafe void ImportMeshesFromFrame(_D3DXFRAME* frame, D3DXMATRIX* parentTransform)
  {
    // ISSUE: untyped stack allocation
    long num1 = (long) __untypedstackalloc(\u003CModule\u003E.__CxxQueryExceptionSize());
    D3DXMATRIX d3Dxmatrix1;
    \u003CModule\u003E.D3DXMatrixMultiply(&d3Dxmatrix1, (D3DXMATRIX*) ((IntPtr) frame + 8L), parentTransform);
    D3DXMATRIX d3Dxmatrix2 = d3Dxmatrix1;
    ulong num2 = (ulong) *(long*) ((IntPtr) frame + 72L);
    if (num2 != 0UL && *(long*) ((long) num2 + 16L /*0x10*/) != 0L)
    {
      ID3DXMesh* id3DxMeshPtr1 = (ID3DXMesh*) *(long*) ((long) num2 + 16L /*0x10*/);
      SUBM* submPtr = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EAddSubMesh(this.m_CompiledModel);
      ID3DXMesh* id3DxMeshPtr2 = id3DxMeshPtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      uint num3 = __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr2 + 32L /*0x20*/))((IntPtr) id3DxMeshPtr2);
      ID3DXMesh* id3DxMeshPtr3 = id3DxMeshPtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      uint num4 = __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr3 + 40L))((IntPtr) id3DxMeshPtr3);
      uint num5 = num3 * 3U;
      ushort* numPtr1 = (ushort*) \u003CModule\u003E.new\u005B\u005D((ulong) num5 * 2UL);
      ID3DXMesh* id3DxMeshPtr4 = id3DxMeshPtr1;
      ushort* numPtr2;
      ref ushort* local1 = ref numPtr2;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int num6 = __calli((__FnPtr<int (IntPtr, uint, void**)>) *(long*) (*(long*) id3DxMeshPtr1 + 136L))((IntPtr) id3DxMeshPtr4, 16U /*0x10*/, (void**) ref local1);
      if (0U < num5)
      {
        ushort* numPtr3 = (ushort*) ((IntPtr) numPtr1 + 4L);
        long num7 = (long) -(IntPtr) numPtr1;
        ushort* numPtr4 = (ushort*) (-4L - (IntPtr) numPtr1);
        uint num8 = (num5 - 1U) / 3U + 1U;
        do
        {
          *(short*) ((IntPtr) numPtr3 - 4L) = (short) *(ushort*) (num7 + (IntPtr) numPtr3 + (IntPtr) numPtr2);
          ushort* numPtr5 = (ushort*) ((IntPtr) numPtr4 + (IntPtr) numPtr3);
          *(short*) ((IntPtr) numPtr3 - 2L) = (short) *(ushort*) ((IntPtr) numPtr2 + (IntPtr) numPtr5 + 2L);
          *numPtr3 = *(ushort*) ((IntPtr) numPtr5 + (IntPtr) numPtr2);
          numPtr3 += 6L;
          num8 += uint.MaxValue;
        }
        while (num8 > 0U);
      }
      ID3DXMesh* id3DxMeshPtr5 = id3DxMeshPtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      int num9 = __calli((__FnPtr<int (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr5 + 144L /*0x90*/))((IntPtr) id3DxMeshPtr5);
      *(int*) ((IntPtr) submPtr + 40L) = (int) num3;
      *(int*) ((IntPtr) submPtr + 36L) = (int) num4;
      *(long*) ((IntPtr) submPtr + 124L) = (long) numPtr1;
      *(int*) ((IntPtr) submPtr + 44L) = (int) num5;
      *(float*) ((IntPtr) submPtr + 76L) = 1f;
      *(float*) ((IntPtr) submPtr + 80L /*0x50*/) = 1f;
      *(float*) ((IntPtr) submPtr + 84L) = 1f;
      *(float*) ((IntPtr) submPtr + 88L) = 1f;
      if (*(long*) (*(long*) ((IntPtr) frame + 72L) + 56L) != 0L)
      {
        ulong __n = (ulong) num4;
        Vertex_28_20* __t = (Vertex_28_20*) \u003CModule\u003E.new\u005B\u005D(__n > 658812288346769700UL /*0x0924924924924924*/ ? ulong.MaxValue : __n * 28UL);
        Vertex_28_20* vertex2820Ptr1;
        // ISSUE: fault handler
        try
        {
          if ((IntPtr) __t != IntPtr.Zero)
          {
            // ISSUE: method pointer
            // ISSUE: cast to a function pointer type
            \u003CModule\u003E.__vec_ctor((void*) __t, 28UL, (int) __n, (__FnPtr<void* (void*)>) __methodptr(FableMod\u002EGfx\u002EIntegration\u002EVertex_28_20\u002E\u007Bctor\u007D));
            vertex2820Ptr1 = __t;
          }
          else
            vertex2820Ptr1 = (Vertex_28_20*) 0L;
        }
        __fault
        {
          \u003CModule\u003E.delete\u005B\u005D((void*) __t);
        }
        // ISSUE: initblk instruction
        __memset((IntPtr) vertex2820Ptr1, 0, (long) __n * 28L);
        ID3DXMesh* id3DxMeshPtr6 = id3DxMeshPtr1;
        Vertex* vertexPtr1;
        ref Vertex* local2 = ref vertexPtr1;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int num10 = __calli((__FnPtr<int (IntPtr, uint, void**)>) *(long*) (*(long*) id3DxMeshPtr1 + 120L))((IntPtr) id3DxMeshPtr6, 16U /*0x10*/, (void**) ref local2);
        int num11 = 0;
        if (0U < num4)
        {
          long num12 = 0;
          Vertex_28_20* vertex2820Ptr2 = (Vertex_28_20*) ((IntPtr) vertex2820Ptr1 + 8L);
          do
          {
            D3DXVECTOR4 d3DxvectoR4;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR4 + 12) = 1f;
            Vertex* vertexPtr2 = (Vertex*) (num12 + (IntPtr) vertexPtr1);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ref d3DxvectoR4 = *(float*) vertexPtr2;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR4 + 4) = *(float*) ((IntPtr) vertexPtr2 + 4L);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR4 + 8) = *(float*) ((IntPtr) vertexPtr2 + 8L);
            \u003CModule\u003E.D3DXVec4Transform(&d3DxvectoR4, &d3DxvectoR4, &d3Dxmatrix2);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            *(float*) ((IntPtr) vertex2820Ptr2 - 8L) = ^(float&) ref d3DxvectoR4;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            *(float*) ((IntPtr) vertex2820Ptr2 - 4L) = ^(float&) ((IntPtr) &d3DxvectoR4 + 4);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            *(float*) vertex2820Ptr2 = ^(float&) ((IntPtr) &d3DxvectoR4 + 8);
            Vertex_28_20* vertex2820Ptr3 = (Vertex_28_20*) ((IntPtr) vertex2820Ptr1 + (long) num11 * 28L + 20L);
            \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EPackedXYZ\u002ESetX((PackedXYZ*) vertex2820Ptr3, *(float*) ((IntPtr) vertexPtr1 + num12 + 12L));
            \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EPackedXYZ\u002ESetY((PackedXYZ*) vertex2820Ptr3, *(float*) ((IntPtr) vertexPtr1 + num12 + 16L /*0x10*/));
            \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EPackedXYZ\u002ESetZ((PackedXYZ*) vertex2820Ptr3, *(float*) ((IntPtr) vertexPtr1 + num12 + 20L));
            float num13 = *(float*) ((IntPtr) vertexPtr1 + num12 + 24L);
            *(short*) ((IntPtr) vertex2820Ptr2 + 16L /*0x10*/) = (short) (int) \u003CModule\u003E.floorf(num13 * 2048f);
            float num14 = *(float*) ((IntPtr) vertexPtr1 + num12 + 28L);
            *(short*) ((IntPtr) vertex2820Ptr2 + 18L) = (short) (int) \u003CModule\u003E.floorf(num14 * 2048f);
            ++num11;
            num12 += 32L /*0x20*/;
            vertex2820Ptr2 += 28L;
          }
          while ((uint) num11 < num4);
        }
        ID3DXMesh* id3DxMeshPtr7 = id3DxMeshPtr1;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int num15 = __calli((__FnPtr<int (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr7 + 128L /*0x80*/))((IntPtr) id3DxMeshPtr7);
        long num16 = *(long*) (*(long*) ((IntPtr) frame + 72L) + 56L);
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int num17 = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) num16 + 72L))((IntPtr) num16);
        int num18 = 0;
        int num19 = 0;
        if (0 < num17)
        {
          do
          {
            long num20 = *(long*) (*(long*) ((IntPtr) frame + 72L) + 56L);
            long num21 = num20;
            int num22 = num19;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            sbyte* src = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EStringFromXFileString(__calli((__FnPtr<sbyte* (IntPtr, uint)>) *(long*) (*(long*) num20 + 120L))((IntPtr) num21, (uint) num22));
            sbyte* numPtr6 = src;
            if (*src != (sbyte) 0)
            {
              do
              {
                ++numPtr6;
              }
              while (*numPtr6 != (sbyte) 0);
            }
            long count = (long) ((IntPtr) numPtr6 - (IntPtr) src);
            int bone = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EFindBone(this.m_CompiledModel, uint.MaxValue - ZLib.CRC32(uint.MaxValue, (void*) src, (int) count));
            long num23 = *(long*) (*(long*) ((IntPtr) frame + 72L) + 56L);
            long num24 = num23;
            int num25 = num19;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            D3DXMATRIX* mat = __calli((__FnPtr<D3DXMATRIX* (IntPtr, uint)>) *(long*) (*(long*) num23 + 136L))((IntPtr) num24, (uint) num25);
            long num26 = (long) bone;
            \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EBONE_SUB_CHUNK_1\u002ESetMatrix((BONE_SUB_CHUNK_1*) (num26 * 60L + *(long*) ((IntPtr) this.m_CompiledModel + 132L)), mat);
            \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EBONE_SUB_CHUNK_3\u002ESetMatrix((BONE_SUB_CHUNK_3*) (num26 * 64L /*0x40*/ + *(long*) ((IntPtr) this.m_CompiledModel + 148L)), mat);
            long num27 = *(long*) (*(long*) ((IntPtr) frame + 72L) + 56L);
            long num28 = num27;
            int num29 = num19;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            int num30 = (int) __calli((__FnPtr<uint (IntPtr, uint)>) *(long*) (*(long*) num27 + 40L))((IntPtr) num28, (uint) num29);
            ulong num31 = (ulong) num30;
            ulong num32 = num31;
            uint* numPtr7 = (uint*) \u003CModule\u003E.new\u005B\u005D(num32 > 4611686018427387903UL /*0x3FFFFFFFFFFFFFFF*/ ? ulong.MaxValue : num32 * 4UL);
            ulong num33 = num31;
            float* numPtr8 = (float*) \u003CModule\u003E.new\u005B\u005D(num33 > 4611686018427387903UL /*0x3FFFFFFFFFFFFFFF*/ ? ulong.MaxValue : num33 * 4UL);
            long num34 = *(long*) (*(long*) ((IntPtr) frame + 72L) + 56L);
            long num35 = num34;
            int num36 = num19;
            uint* numPtr9 = numPtr7;
            float* numPtr10 = numPtr8;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            int num37 = __calli((__FnPtr<int (IntPtr, uint, uint*, float*)>) *(long*) (*(long*) num34 + 48L /*0x30*/))((IntPtr) num35, (uint) num36, numPtr9, numPtr10);
            long num38 = (long) num30;
            if (0L < num38)
            {
              byte num39 = (byte) bone;
              float* numPtr11 = numPtr8;
              long num40 = (long) ((IntPtr) numPtr7 - (IntPtr) numPtr8);
              ulong num41 = (ulong) num38;
              do
              {
                Vertex_28_20* vertex2820Ptr4 = (Vertex_28_20*) ((long) (uint) *(int*) (num40 + (IntPtr) numPtr11) * 28L + (IntPtr) vertex2820Ptr1);
                int num42 = 0;
                long num43 = 0;
                Vertex_28_20* vertex2820Ptr5 = (Vertex_28_20*) ((IntPtr) vertex2820Ptr4 + 16L /*0x10*/);
                while (*(byte*) (num43 + (IntPtr) vertex2820Ptr5) != (byte) 0)
                {
                  ++num42;
                  ++num43;
                  if (num43 >= 4L)
                    break;
                }
                *(sbyte*) (num43 + (IntPtr) vertex2820Ptr4 + 12L) = (sbyte) num39;
                *(sbyte*) (num43 + (IntPtr) vertex2820Ptr4 + 16L /*0x10*/) = (sbyte) ((int) (uint) Math.Round((double) *numPtr11 * (double) byte.MaxValue) & (int) byte.MaxValue);
                num18 = num42 + 1 > num18 ? num42 + 1 : num18;
                numPtr11 += 4L;
                --num41;
              }
              while (num41 > 0UL);
            }
            \u003CModule\u003E.delete\u005B\u005D((void*) src);
            \u003CModule\u003E.delete\u005B\u005D((void*) numPtr7);
            \u003CModule\u003E.delete\u005B\u005D((void*) numPtr8);
            ++num19;
          }
          while (num19 < num17);
        }
        byte* numPtr12 = (byte*) \u003CModule\u003E.new\u005B\u005D((ulong) (uint) *(int*) ((IntPtr) this.m_CompiledModel + 103L));
        int num44 = 0;
        long num45 = 0;
        if (0U < num4)
        {
          Vertex_28_20* vertex2820Ptr6 = (Vertex_28_20*) ((IntPtr) vertex2820Ptr1 + 12L);
          uint num46 = num4;
          do
          {
            Vertex_28_20* vertex2820Ptr7 = vertex2820Ptr6;
            ulong num47 = 4;
            do
            {
              if (*(byte*) ((IntPtr) vertex2820Ptr7 + 4L) == (byte) 0)
              {
                *(sbyte*) vertex2820Ptr7 = (sbyte) 0;
              }
              else
              {
                int num48 = (int) *(byte*) vertex2820Ptr7;
                int num49 = 0;
                long num50 = 0;
                if (0L < num45)
                {
                  while ((int) *(byte*) (num50 + (IntPtr) numPtr12) != num48)
                  {
                    ++num49;
                    ++num50;
                    if (num50 >= num45)
                      goto label_31;
                  }
                  goto label_32;
                }
label_31:
                *(sbyte*) (num45 + (IntPtr) numPtr12) = (sbyte) num48;
                ++num44;
                ++num45;
label_32:
                *(sbyte*) vertex2820Ptr7 = (sbyte) (num49 * 3);
              }
              ++vertex2820Ptr7;
              --num47;
            }
            while (num47 > 0UL);
            vertex2820Ptr6 += 28L;
            num46 += uint.MaxValue;
          }
          while (num46 > 0U);
        }
        *(long*) ((IntPtr) submPtr + 116L) = (long) vertex2820Ptr1;
        *(int*) ((IntPtr) submPtr + 108L) = 28;
        *(int*) ((IntPtr) submPtr + 48L /*0x30*/) = 20;
        *(float*) ((IntPtr) submPtr + 8L) = 0.0141897025f;
        *(float*) ((IntPtr) submPtr + 12L) = 6.48409462f;
        *(float*) ((IntPtr) submPtr + 16L /*0x10*/) = 140.265671f;
        *(float*) ((IntPtr) submPtr + 20L) = 33.87893f;
        *(float*) ((IntPtr) submPtr + 24L) = 0.01618274f;
        *(int*) ((IntPtr) submPtr + 32L /*0x20*/) = 1;
        *(int*) ((IntPtr) submPtr + 56L) = 1;
        SUBM_SUB_CHUNK_2** submSubChunk2Ptr1 = (SUBM_SUB_CHUNK_2**) \u003CModule\u003E.@new(8UL);
        *(long*) ((IntPtr) submPtr + 68L) = (long) submSubChunk2Ptr1;
        SUBM_SUB_CHUNK_2* submSubChunk2Ptr2 = (SUBM_SUB_CHUNK_2*) \u003CModule\u003E.@new(27UL);
        SUBM_SUB_CHUNK_2* submSubChunk2Ptr3;
        // ISSUE: fault handler
        try
        {
          if ((IntPtr) submSubChunk2Ptr2 != IntPtr.Zero)
          {
            *(long*) ((IntPtr) submSubChunk2Ptr2 + 19L) = 0L;
            submSubChunk2Ptr3 = submSubChunk2Ptr2;
          }
          else
            submSubChunk2Ptr3 = (SUBM_SUB_CHUNK_2*) 0L;
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) submSubChunk2Ptr2);
        }
        *(long*) *(long*) ((IntPtr) submPtr + 68L) = (long) submSubChunk2Ptr3;
        // ISSUE: initblk instruction
        __memset(*(long*) *(long*) ((IntPtr) submPtr + 68L), 0, 27);
        *(int*) *(long*) *(long*) ((IntPtr) submPtr + 68L) = (int) num3;
        *(int*) (*(long*) *(long*) ((IntPtr) submPtr + 68L) + 4L) = 0;
        *(short*) (*(long*) *(long*) ((IntPtr) submPtr + 68L) + 8L) = (short) 0;
        *(sbyte*) (*(long*) *(long*) ((IntPtr) submPtr + 68L) + 18L) = (sbyte) num44;
        ulong num51 = (ulong) num44;
        byte* numPtr13 = (byte*) \u003CModule\u003E.new\u005B\u005D(num51);
        *(long*) (*(long*) *(long*) ((IntPtr) submPtr + 68L) + 19L) = (long) numPtr13;
        // ISSUE: cpblk instruction
        __memcpy(*(long*) (*(long*) *(long*) ((IntPtr) submPtr + 68L) + 19L), (IntPtr) numPtr12, (long) num51);
        \u003CModule\u003E.delete\u005B\u005D((void*) numPtr12);
        *(sbyte*) (*(long*) *(long*) ((IntPtr) submPtr + 68L) + 17L) = (sbyte) 1;
        *(short*) (*(long*) *(long*) ((IntPtr) submPtr + 68L) + 15L) = (short) num18;
      }
      else
      {
        ulong __n = (ulong) num4;
        Vertex_20_20* __t = (Vertex_20_20*) \u003CModule\u003E.new\u005B\u005D(__n > 922337203685477580UL /*0x0CCCCCCCCCCCCCCC*/ ? ulong.MaxValue : __n * 20UL);
        Vertex_20_20* vertex2020Ptr1;
        // ISSUE: fault handler
        try
        {
          if ((IntPtr) __t != IntPtr.Zero)
          {
            // ISSUE: method pointer
            // ISSUE: cast to a function pointer type
            \u003CModule\u003E.__vec_ctor((void*) __t, 20UL, (int) __n, (__FnPtr<void* (void*)>) __methodptr(FableMod\u002EGfx\u002EIntegration\u002EVertex_20_20\u002E\u007Bctor\u007D));
            vertex2020Ptr1 = __t;
          }
          else
            vertex2020Ptr1 = (Vertex_20_20*) 0L;
        }
        __fault
        {
          \u003CModule\u003E.delete\u005B\u005D((void*) __t);
        }
        ID3DXMesh* id3DxMeshPtr8 = id3DxMeshPtr1;
        Vertex* vertexPtr3;
        ref Vertex* local3 = ref vertexPtr3;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int num52 = __calli((__FnPtr<int (IntPtr, uint, void**)>) *(long*) (*(long*) id3DxMeshPtr1 + 120L))((IntPtr) id3DxMeshPtr8, 16U /*0x10*/, (void**) ref local3);
        int num53 = 0;
        if (0U < num4)
        {
          long num54 = 0;
          Vertex_20_20* vertex2020Ptr2 = (Vertex_20_20*) ((IntPtr) vertex2020Ptr1 + 8L);
          do
          {
            D3DXVECTOR4 d3DxvectoR4;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR4 + 12) = 1f;
            Vertex* vertexPtr4 = (Vertex*) (num54 + (IntPtr) vertexPtr3);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ref d3DxvectoR4 = *(float*) vertexPtr4;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR4 + 4) = *(float*) ((IntPtr) vertexPtr4 + 4L);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR4 + 8) = *(float*) ((IntPtr) vertexPtr4 + 8L);
            \u003CModule\u003E.D3DXVec4Transform(&d3DxvectoR4, &d3DxvectoR4, &d3Dxmatrix2);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            *(float*) ((IntPtr) vertex2020Ptr2 - 8L) = ^(float&) ref d3DxvectoR4;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            *(float*) ((IntPtr) vertex2020Ptr2 - 4L) = ^(float&) ((IntPtr) &d3DxvectoR4 + 4);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            *(float*) vertex2020Ptr2 = ^(float&) ((IntPtr) &d3DxvectoR4 + 8);
            Vertex_20_20* vertex2020Ptr3 = (Vertex_20_20*) ((IntPtr) vertex2020Ptr1 + (long) num53 * 20L + 12L);
            \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EPackedXYZ\u002ESetX((PackedXYZ*) vertex2020Ptr3, *(float*) ((IntPtr) vertexPtr3 + num54 + 12L));
            \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EPackedXYZ\u002ESetY((PackedXYZ*) vertex2020Ptr3, *(float*) ((IntPtr) vertexPtr3 + num54 + 16L /*0x10*/));
            \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EPackedXYZ\u002ESetZ((PackedXYZ*) vertex2020Ptr3, *(float*) ((IntPtr) vertexPtr3 + num54 + 20L));
            float num55 = *(float*) ((IntPtr) vertexPtr3 + num54 + 24L);
            *(short*) ((IntPtr) vertex2020Ptr2 + 8L) = (short) (int) \u003CModule\u003E.floorf(num55 * 2048f);
            float num56 = *(float*) ((IntPtr) vertexPtr3 + num54 + 28L);
            *(short*) ((IntPtr) vertex2020Ptr2 + 10L) = (short) (int) \u003CModule\u003E.floorf(num56 * 2048f);
            ++num53;
            num54 += 32L /*0x20*/;
            vertex2020Ptr2 += 20L;
          }
          while ((uint) num53 < num4);
        }
        ID3DXMesh* id3DxMeshPtr9 = id3DxMeshPtr1;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int num57 = __calli((__FnPtr<int (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr9 + 128L /*0x80*/))((IntPtr) id3DxMeshPtr9);
        *(long*) ((IntPtr) submPtr + 116L) = (long) vertex2020Ptr1;
        *(int*) ((IntPtr) submPtr + 108L) = 20;
        *(int*) ((IntPtr) submPtr + 48L /*0x30*/) = 20;
        *(float*) ((IntPtr) submPtr + 8L) = 1.95445311f;
        *(float*) ((IntPtr) submPtr + 12L) = -73.63134f;
        *(float*) ((IntPtr) submPtr + 16L /*0x10*/) = 496.841583f;
        *(float*) ((IntPtr) submPtr + 20L) = 1592.58691f;
        *(float*) ((IntPtr) submPtr + 24L) = 0.0007336853f;
        *(int*) ((IntPtr) submPtr + 28L) = 1;
        *(int*) ((IntPtr) submPtr + 52L) = 1;
        SUBM_SUB_CHUNK_1* submSubChunk1Ptr = (SUBM_SUB_CHUNK_1*) \u003CModule\u003E.@new(15UL);
        *(long*) ((IntPtr) submPtr + 60L) = (long) submSubChunk1Ptr;
        // ISSUE: initblk instruction
        __memset((IntPtr) submSubChunk1Ptr, 0, 15);
        *(int*) *(long*) ((IntPtr) submPtr + 60L) = (int) num3;
        *(int*) (*(long*) ((IntPtr) submPtr + 60L) + 4L) = 0;
        *(short*) (*(long*) ((IntPtr) submPtr + 60L) + 8L) = (short) 0;
      }
      *(int*) submPtr = *(int*) ((IntPtr) this.m_CompiledModel + 95L);
      CompiledModel* compiledModel = this.m_CompiledModel;
      int id1 = *(int*) ((IntPtr) compiledModel + 95L);
      MTRL* mtrlPtr = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EAddMaterial(compiledModel, (uint) id1);
      long num58 = *(long*) frame;
      long num59 = num58;
      if (*(sbyte*) num58 != (sbyte) 0)
      {
        do
        {
          ++num59;
        }
        while (*(sbyte*) num59 != (sbyte) 0);
      }
      sbyte* numPtr14 = (sbyte*) \u003CModule\u003E.new\u005B\u005D((ulong) (num59 - num58) + 1UL);
      *(long*) ((IntPtr) mtrlPtr + 4L) = (long) numPtr14;
      long num60 = *(long*) frame;
      sbyte* numPtr15 = numPtr14;
      sbyte num61;
      do
      {
        num61 = *(sbyte*) num60;
        *numPtr15 = num61;
        ++num60;
        ++numPtr15;
      }
      while (num61 != (sbyte) 0);
      _D3DXMATERIAL d3Dxmaterial;
      // ISSUE: cpblk instruction
      __memcpy(ref d3Dxmaterial, *(long*) (*(long*) ((IntPtr) frame + 72L) + 24L), 80 /*0x50*/);
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      if (^(long&) ((IntPtr) &d3Dxmaterial + 72) != 0L)
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        string str = new string((sbyte*) ^(long&) ((IntPtr) &d3Dxmaterial + 72));
        try
        {
          uint id2 = uint.Parse(str.Substring(0, str.IndexOf('.')));
          if ((IntPtr) this.FindTexture(id2) != IntPtr.Zero)
            *(int*) ((IntPtr) mtrlPtr + 16L /*0x10*/) = (int) id2;
        }
        catch (System.Exception ex1) when (
        {
          // ISSUE: unable to correctly present filter
          uint exceptionCode = (uint) Marshal.GetExceptionCode();
          if (\u003CModule\u003E.__CxxExceptionFilter((void*) Marshal.GetExceptionPointers(), (void*) 0L, 0, (void*) 0L) != 0)
          {
            SuccessfulFiltering;
          }
          else
            throw;
        }
        )
        {
          uint num62 = 0;
          \u003CModule\u003E.__CxxRegisterExceptionObject((void*) Marshal.GetExceptionPointers(), (void*) num1);
          try
          {
            try
            {
            }
            catch (System.Exception ex2) when (
            {
              // ISSUE: unable to correctly present filter
              num62 = (uint) \u003CModule\u003E.__CxxDetectRethrow((void*) Marshal.GetExceptionPointers());
              if (num62 != 0U)
              {
                SuccessfulFiltering;
              }
              else
                throw;
            }
            )
            {
            }
            goto label_67;
            if (num62 != 0U)
              throw;
          }
          finally
          {
            \u003CModule\u003E.__CxxUnregisterExceptionObject((void*) num1, (int) num62);
          }
        }
      }
    }
label_67:
    ulong frame1 = (ulong) *(long*) ((IntPtr) frame + 88L);
    if (frame1 != 0UL)
      this.ImportMeshesFromFrame((_D3DXFRAME*) frame1, &d3Dxmatrix2);
    ulong frame2 = (ulong) *(long*) ((IntPtr) frame + 80L /*0x50*/);
    if (frame2 == 0UL)
      return;
    this.ImportMeshesFromFrame((_D3DXFRAME*) frame2, &d3Dxmatrix2);
  }

  private unsafe void ImportHelpers(_D3DXFRAME* frame, D3DXMATRIX* parentTransform)
  {
    _D3DXFRAME* frame1 = (_D3DXFRAME*) *(long*) ((IntPtr) frame + 88L);
    sbyte* src = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EStringFromXFileString((sbyte*) *(long*) frame);
    D3DXMATRIX d3Dxmatrix1;
    \u003CModule\u003E.D3DXMatrixMultiply(&d3Dxmatrix1, (D3DXMATRIX*) ((IntPtr) frame + 8L), parentTransform);
    if ((IntPtr) frame1 != IntPtr.Zero)
    {
      do
      {
        if (\u003CModule\u003E.strncmp((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05LIHIPBGP\u0040HPNT_\u003F\u0024AA\u0040, (sbyte*) *(long*) frame1, 5UL) == 0)
        {
          sbyte* name = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EStringFromXFileString((sbyte*) (*(long*) frame1 + 5L));
          HPNT* hpntPtr = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EAddHPNT(this.m_CompiledModel, name);
          sbyte* numPtr = src;
          if (*src != (sbyte) 0)
          {
            do
            {
              ++numPtr;
            }
            while (*numPtr != (sbyte) 0);
          }
          long count = (long) ((IntPtr) numPtr - (IntPtr) src);
          uint crc = uint.MaxValue - ZLib.CRC32(uint.MaxValue, (void*) src, (int) count);
          *(int*) ((IntPtr) hpntPtr + 16L /*0x10*/) = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EFindBone(this.m_CompiledModel, crc);
          D3DXMATRIX d3Dxmatrix2;
          \u003CModule\u003E.D3DXMatrixMultiply(&d3Dxmatrix2, (D3DXMATRIX*) ((IntPtr) frame1 + 8L), &d3Dxmatrix1);
          \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EHPNT\u002ESetMatrix(hpntPtr, &d3Dxmatrix2);
          \u003CModule\u003E.delete\u005B\u005D((void*) name);
        }
        else if (\u003CModule\u003E.strncmp((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05DHHFPMJA\u0040HDMY_\u003F\u0024AA\u0040, (sbyte*) *(long*) frame1, 5UL) == 0)
        {
          sbyte* name = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EStringFromXFileString((sbyte*) (*(long*) frame1 + 5L));
          HDMY* hdmyPtr = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EAddHDMY(this.m_CompiledModel, name);
          sbyte* numPtr = src;
          if (*src != (sbyte) 0)
          {
            do
            {
              ++numPtr;
            }
            while (*numPtr != (sbyte) 0);
          }
          long count = (long) ((IntPtr) numPtr - (IntPtr) src);
          uint crc = uint.MaxValue - ZLib.CRC32(uint.MaxValue, (void*) src, (int) count);
          *(int*) ((IntPtr) hdmyPtr + 52L) = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EFindBone(this.m_CompiledModel, crc);
          D3DXMATRIX d3Dxmatrix3;
          \u003CModule\u003E.D3DXMatrixMultiply(&d3Dxmatrix3, (D3DXMATRIX*) ((IntPtr) frame1 + 8L), &d3Dxmatrix1);
          \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EHDMY\u002ESetMatrix(hdmyPtr, &d3Dxmatrix3);
          \u003CModule\u003E.delete\u005B\u005D((void*) name);
        }
        else
          this.ImportHelpers(frame1, &d3Dxmatrix1);
        frame1 = (_D3DXFRAME*) *(long*) ((IntPtr) frame1 + 80L /*0x50*/);
      }
      while ((IntPtr) frame1 != IntPtr.Zero);
    }
    \u003CModule\u003E.delete\u005B\u005D((void*) src);
  }

  private unsafe void ImportBones(_D3DXFRAME* frame)
  {
    _D3DXFRAME* frame1 = (_D3DXFRAME*) *(long*) ((IntPtr) frame + 88L);
    sbyte* src1 = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EStringFromXFileString((sbyte*) *(long*) frame);
    if ((IntPtr) frame1 != IntPtr.Zero)
    {
      do
      {
        if (\u003CModule\u003E.strncmp((sbyte*) *(long*) frame1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05LIHIPBGP\u0040HPNT_\u003F\u0024AA\u0040, 5UL) != 0 && \u003CModule\u003E.strncmp((sbyte*) *(long*) frame1, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_05DHHFPMJA\u0040HDMY_\u003F\u0024AA\u0040, 5UL) != 0)
        {
          sbyte* numPtr1 = (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BD\u0040GNOHACJB\u0040BONE_OFFSET_MATRIX\u003F\u0024AA\u0040;
          long src2 = *(long*) frame1;
          long num1 = src2;
          sbyte num2 = *(sbyte*) num1;
          sbyte num3 = 66;
          if (num2 >= (sbyte) 66)
          {
            while ((int) num2 <= (int) num3)
            {
              if (num2 != (sbyte) 0)
              {
                ++num1;
                ++numPtr1;
                num2 = *(sbyte*) num1;
                num3 = *numPtr1;
                if ((int) num2 < (int) num3)
                  break;
              }
              else
                goto label_16;
            }
          }
          sbyte* bonename = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EStringFromXFileString((sbyte*) src2);
          long num4 = (long) \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EAddBone(this.m_CompiledModel, bonename);
          \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EBONE_SUB_CHUNK_2\u002ESetMatrix((BONE_SUB_CHUNK_2*) (num4 * 48L /*0x30*/ + *(long*) ((IntPtr) this.m_CompiledModel + 140L)), (D3DXMATRIX*) ((IntPtr) frame1 + 8L));
          _D3DXFRAME* d3DxframePtr = (_D3DXFRAME*) *(long*) ((IntPtr) frame1 + 88L);
          if ((IntPtr) d3DxframePtr != IntPtr.Zero)
          {
            do
            {
              sbyte* numPtr2 = (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0BD\u0040GNOHACJB\u0040BONE_OFFSET_MATRIX\u003F\u0024AA\u0040;
              long num5 = *(long*) d3DxframePtr;
              sbyte num6 = *(sbyte*) num5;
              sbyte num7 = 66;
              if (num6 >= (sbyte) 66)
              {
                do
                {
                  if ((int) num6 <= (int) num7)
                  {
                    if (num6 != (sbyte) 0)
                    {
                      ++num5;
                      ++numPtr2;
                      num6 = *(sbyte*) num5;
                      num7 = *numPtr2;
                    }
                    else
                      goto label_12;
                  }
                  else
                    break;
                }
                while ((int) num6 >= (int) num7);
              }
              d3DxframePtr = (_D3DXFRAME*) *(long*) ((IntPtr) d3DxframePtr + 80L /*0x50*/);
            }
            while ((IntPtr) d3DxframePtr != IntPtr.Zero);
            goto label_13;
label_12:
            _D3DXFRAME* mat = (_D3DXFRAME*) ((IntPtr) d3DxframePtr + 8L);
            \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EBONE_SUB_CHUNK_1\u002ESetMatrix((BONE_SUB_CHUNK_1*) (num4 * 60L + *(long*) ((IntPtr) this.m_CompiledModel + 132L)), (D3DXMATRIX*) mat);
            \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EBONE_SUB_CHUNK_3\u002ESetMatrix((BONE_SUB_CHUNK_3*) (num4 * 64L /*0x40*/ + *(long*) ((IntPtr) this.m_CompiledModel + 148L)), (D3DXMATRIX*) mat);
          }
label_13:
          sbyte* numPtr3 = src1;
          if (*src1 != (sbyte) 0)
          {
            do
            {
              ++numPtr3;
            }
            while (*numPtr3 != (sbyte) 0);
          }
          long count = (long) ((IntPtr) numPtr3 - (IntPtr) src1);
          uint crc = uint.MaxValue - ZLib.CRC32(uint.MaxValue, (void*) src1, (int) count);
          long num8 = num4 * 60L;
          *(int*) (num8 + *(long*) ((IntPtr) this.m_CompiledModel + 132L) + 4L) = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EFindBone(this.m_CompiledModel, crc);
          long num9 = *(long*) ((IntPtr) this.m_CompiledModel + 132L);
          long num10 = num9 + (long) (uint) *(int*) (num9 + num8 + 4L) * 60L + 8L;
          *(int*) num10 = *(int*) num10 + 1;
          \u003CModule\u003E.delete\u005B\u005D((void*) bonename);
          this.ImportBones(frame1);
        }
label_16:
        frame1 = (_D3DXFRAME*) *(long*) ((IntPtr) frame1 + 80L /*0x50*/);
      }
      while ((IntPtr) frame1 != IntPtr.Zero);
    }
    \u003CModule\u003E.delete\u005B\u005D((void*) src1);
  }

  private unsafe void ExportTextures(string path)
  {
    int num1 = 0;
    CompiledModel* compiledModel = this.m_CompiledModel;
    if (0U >= (uint) *(int*) ((IntPtr) compiledModel + 99L))
      return;
    long num2 = 0;
    do
    {
      SUBM* submPtr = (SUBM*) *(long*) (*(long*) ((IntPtr) compiledModel + 212L) + num2);
      MTRL* materialFromId = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EGetMaterialFromID(compiledModel, (uint) *(int*) submPtr);
      GfxTexture gfxTexture = GfxManager.GetTextureManager().Get((uint) *(int*) ((IntPtr) materialFromId + 16L /*0x10*/));
      if (gfxTexture != null)
      {
        uint num3 = (uint) *(int*) ((IntPtr) materialFromId + 16L /*0x10*/);
        string filename = $"{path}{num3.ToString()}.dds";
        gfxTexture.Save(filename, 0);
      }
      ++num1;
      num2 += 8L;
      compiledModel = this.m_CompiledModel;
    }
    while ((uint) num1 < (uint) *(int*) ((IntPtr) compiledModel + 99L));
  }

  private unsafe void CreateFrameFromCompiled()
  {
    D3DXMATRIX d3Dxmatrix1;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 56) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 52) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 48 /*0x30*/) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 44) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 36) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 32 /*0x20*/) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 28) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 24) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 16 /*0x10*/) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 12) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 8) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 4) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 60) = 1f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 40) = 1f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix1 + 20) = 1f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3Dxmatrix1 = 1f;
    D3DXEXTENDEDFRAME* d3DxextendedframePtr1 = (D3DXEXTENDEDFRAME*) \u003CModule\u003E.@new(168UL);
    D3DXEXTENDEDFRAME* d3DxextendedframePtr2;
    // ISSUE: fault handler
    try
    {
      d3DxextendedframePtr2 = (IntPtr) d3DxextendedframePtr1 == IntPtr.Zero ? (D3DXEXTENDEDFRAME*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ED3DXEXTENDEDFRAME\u002E\u007Bctor\u007D(d3DxextendedframePtr1);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) d3DxextendedframePtr1);
    }
    this.m_SceneRoot = d3DxextendedframePtr2;
    *(long*) this.m_SceneRoot = (long) \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECopyXFileSafeString((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040GLANBBEC\u0040Scene\u003F5Root\u003F\u0024AA\u0040);
    *(long*) ((IntPtr) this.m_SceneRoot + 80L /*0x50*/) = 0L;
    *(long*) ((IntPtr) this.m_SceneRoot + 72L) = 0L;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) this.m_SceneRoot + 8L, ref d3Dxmatrix1, 64 /*0x40*/);
    D3DXEXTENDEDFRAME* d3DxextendedframePtr3 = (D3DXEXTENDEDFRAME*) \u003CModule\u003E.@new(168UL);
    // ISSUE: fault handler
    try
    {
      d3DxextendedframePtr2 = (IntPtr) d3DxextendedframePtr3 == IntPtr.Zero ? (D3DXEXTENDEDFRAME*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ED3DXEXTENDEDFRAME\u002E\u007Bctor\u007D(d3DxextendedframePtr3);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) d3DxextendedframePtr3);
    }
    sbyte* src = (sbyte*) *(long*) this.m_CompiledModel;
    *(long*) d3DxextendedframePtr2 = (long) \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECopyXFileSafeString(src);
    *(long*) ((IntPtr) d3DxextendedframePtr2 + 80L /*0x50*/) = 0L;
    *(long*) ((IntPtr) d3DxextendedframePtr2 + 72L) = 0L;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) d3DxextendedframePtr2 + 8L, ref d3Dxmatrix1, 64 /*0x40*/);
    \u003CModule\u003E.D3DXFrameAppendChild((_D3DXFRAME*) this.m_SceneRoot, (_D3DXFRAME*) d3DxextendedframePtr2);
    IDirect3DDevice9* idirect3Ddevice9Ptr = (IDirect3DDevice9*) *(long*) ((IntPtr) \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA + 8L);
    int num1 = 0;
    if (0U < (uint) *(int*) ((IntPtr) this.m_CompiledModel + 99L))
    {
      long num2 = 0;
      do
      {
        SUBM* submPtr = (SUBM*) *(long*) (*(long*) ((IntPtr) this.m_CompiledModel + 212L) + num2);
        D3DXEXTENDEDFRAME* d3DxextendedframePtr4 = (D3DXEXTENDEDFRAME*) \u003CModule\u003E.@new(168UL);
        D3DXEXTENDEDFRAME* d3DxextendedframePtr5;
        // ISSUE: fault handler
        try
        {
          d3DxextendedframePtr5 = (IntPtr) d3DxextendedframePtr4 == IntPtr.Zero ? (D3DXEXTENDEDFRAME*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ED3DXEXTENDEDFRAME\u002E\u007Bctor\u007D(d3DxextendedframePtr4);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) d3DxextendedframePtr4);
        }
        *(long*) d3DxextendedframePtr5 = (long) \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECopyXFileSafeString((sbyte*) *(long*) ((IntPtr) \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EGetMaterialFromID(this.m_CompiledModel, (uint) *(int*) submPtr) + 4L));
        // ISSUE: cpblk instruction
        __memcpy((IntPtr) d3DxextendedframePtr5 + 8L, ref d3Dxmatrix1, 64 /*0x40*/);
        *(long*) ((IntPtr) d3DxextendedframePtr5 + 80L /*0x50*/) = 0L;
        *(long*) ((IntPtr) d3DxextendedframePtr5 + 88L) = 0L;
        ID3DXMesh* id3DxMeshPtr1 = (ID3DXMesh*) 0L;
        if (\u003CModule\u003E.D3DXCreateMeshFVF((uint) *(int*) ((IntPtr) submPtr + 40L), (uint) *(int*) ((IntPtr) submPtr + 36L), 544U, 274U, idirect3Ddevice9Ptr, &id3DxMeshPtr1) >= 0)
        {
          ID3DXMesh* id3DxMeshPtr2 = id3DxMeshPtr1;
          Vertex* vertices;
          ref Vertex* local1 = ref vertices;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          int num3 = __calli((__FnPtr<int (IntPtr, uint, void**)>) *(long*) (*(long*) id3DxMeshPtr1 + 120L))((IntPtr) id3DxMeshPtr2, 0U, (void**) ref local1);
          \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ESUBM\u002EGetVertices(submPtr, vertices);
          ID3DXMesh* id3DxMeshPtr3 = id3DxMeshPtr1;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          int num4 = __calli((__FnPtr<int (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr3 + 128L /*0x80*/))((IntPtr) id3DxMeshPtr3);
          VertexBones* bones = (VertexBones*) 0L;
          if (*(byte*) ((IntPtr) this.m_CompiledModel + 8L) != (byte) 0)
          {
            bones = (VertexBones*) \u003CModule\u003E.new\u005B\u005D((ulong) (uint) *(int*) ((IntPtr) submPtr + 36L) * 9UL);
            \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ESUBM\u002EGetVertexBones(submPtr, bones);
          }
          ID3DXMesh* id3DxMeshPtr4 = id3DxMeshPtr1;
          ushort* faces;
          ref ushort* local2 = ref faces;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          int num5 = __calli((__FnPtr<int (IntPtr, uint, void**)>) *(long*) (*(long*) id3DxMeshPtr1 + 136L))((IntPtr) id3DxMeshPtr4, 0U, (void**) ref local2);
          \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ESUBM\u002EGetFaces(submPtr, faces, bones);
          ID3DXMesh* id3DxMeshPtr5 = id3DxMeshPtr1;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          int num6 = __calli((__FnPtr<int (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr5 + 144L /*0x90*/))((IntPtr) id3DxMeshPtr5);
          _D3DXMATERIAL* d3DxmaterialPtr = (_D3DXMATERIAL*) \u003CModule\u003E.@new(80UL /*0x50*/);
          *(float*) ((IntPtr) d3DxmaterialPtr + 28L) = 0.3f;
          *(float*) ((IntPtr) d3DxmaterialPtr + 16L /*0x10*/) = 0.3f;
          *(float*) ((IntPtr) d3DxmaterialPtr + 20L) = 0.3f;
          *(float*) ((IntPtr) d3DxmaterialPtr + 24L) = 0.3f;
          *(float*) ((IntPtr) d3DxmaterialPtr + 12L) = 1f;
          *(float*) d3DxmaterialPtr = 1f;
          *(float*) ((IntPtr) d3DxmaterialPtr + 4L) = 1f;
          *(float*) ((IntPtr) d3DxmaterialPtr + 8L) = 1f;
          *(float*) ((IntPtr) d3DxmaterialPtr + 60L) = 0.0f;
          *(float*) ((IntPtr) d3DxmaterialPtr + 48L /*0x30*/) = 0.0f;
          *(float*) ((IntPtr) d3DxmaterialPtr + 52L) = 0.0f;
          *(float*) ((IntPtr) d3DxmaterialPtr + 56L) = 0.0f;
          *(float*) ((IntPtr) d3DxmaterialPtr + 44L) = 0.0f;
          *(float*) ((IntPtr) d3DxmaterialPtr + 32L /*0x20*/) = 0.0f;
          *(float*) ((IntPtr) d3DxmaterialPtr + 36L) = 0.0f;
          *(float*) ((IntPtr) d3DxmaterialPtr + 40L) = 0.0f;
          *(float*) ((IntPtr) d3DxmaterialPtr + 64L /*0x40*/) = 0.0f;
          IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi(((uint) *(int*) ((IntPtr) \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EGetMaterialFromID(this.m_CompiledModel, (uint) *(int*) submPtr) + 16L /*0x10*/)).ToString() + ".dds");
          void* pointer1 = hglobalAnsi.ToPointer();
          void* voidPtr = pointer1;
          if (*(sbyte*) pointer1 != (sbyte) 0)
          {
            do
            {
              ++voidPtr;
            }
            while (*(sbyte*) voidPtr != (sbyte) 0);
          }
          sbyte* numPtr1 = (sbyte*) \u003CModule\u003E.new\u005B\u005D((ulong) ((IntPtr) voidPtr - (IntPtr) pointer1 + 1L));
          *(long*) ((IntPtr) d3DxmaterialPtr + 72L) = (long) numPtr1;
          void* pointer2 = hglobalAnsi.ToPointer();
          long num7 = *(long*) ((IntPtr) d3DxmaterialPtr + 72L);
          sbyte num8;
          do
          {
            num8 = *(sbyte*) pointer2;
            *(sbyte*) num7 = num8;
            ++pointer2;
            ++num7;
          }
          while (num8 != (sbyte) 0);
          Marshal.FreeHGlobal(hglobalAnsi);
          MTRL* materialFromId = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EGetMaterialFromID(this.m_CompiledModel, (uint) *(int*) submPtr);
          Texture* texture1 = this.FindTexture((uint) *(int*) ((IntPtr) materialFromId + 16L /*0x10*/));
          if ((IntPtr) texture1 != IntPtr.Zero)
            \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u003D((SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) d3DxextendedframePtr5 + 104L), texture1);
          Texture* texture2 = this.FindTexture((uint) *(int*) ((IntPtr) materialFromId + 20L));
          if ((IntPtr) texture2 != IntPtr.Zero)
            \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u003D((SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) d3DxextendedframePtr5 + 120L), texture2);
          Texture* texture3 = this.FindTexture((uint) *(int*) ((IntPtr) materialFromId + 24L));
          if ((IntPtr) texture3 != IntPtr.Zero)
            \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u003D((SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) d3DxextendedframePtr5 + 136L), texture3);
          int num9 = *(byte*) ((IntPtr) materialFromId + 41L) > (byte) 0 ? 1 : 0;
          *(sbyte*) ((IntPtr) d3DxextendedframePtr5 + 152L) = (sbyte) num9;
          ID3DXSkinInfo* id3DxSkinInfoPtr1 = (ID3DXSkinInfo*) 0L;
          CompiledModel* compiledModel = this.m_CompiledModel;
          if (*(byte*) ((IntPtr) compiledModel + 8L) != (byte) 0)
          {
            int num10 = 0;
            long num11 = 0;
            byte* numPtr2 = (byte*) \u003CModule\u003E.new\u005B\u005D((ulong) (uint) *(int*) ((IntPtr) compiledModel + 103L));
            int num12 = 0;
            if (0U < (uint) *(int*) ((IntPtr) submPtr + 36L))
            {
              VertexBones* vertexBonesPtr = bones;
              do
              {
                long num13 = 0;
                do
                {
                  bool flag = false;
                  long num14 = 0;
                  if (0L < num11)
                  {
                    byte num15 = *(byte*) (num13 + (IntPtr) vertexBonesPtr);
                    do
                    {
                      flag = (int) *(byte*) (num14 + (IntPtr) numPtr2) == (int) num15 || flag;
                      ++num14;
                    }
                    while (num14 < num11);
                    if (flag)
                      goto label_33;
                  }
                  *(sbyte*) (num11 + (IntPtr) numPtr2) = (sbyte) *(byte*) (num13 + (IntPtr) vertexBonesPtr);
                  ++num10;
                  ++num11;
label_33:
                  ++num13;
                }
                while (num13 < 4L);
                ++num12;
                vertexBonesPtr += 9L;
              }
              while ((uint) num12 < (uint) *(int*) ((IntPtr) submPtr + 36L));
            }
            \u003CModule\u003E.D3DXCreateSkinInfoFVF((uint) *(int*) ((IntPtr) submPtr + 36L), 274U, (uint) num10, &id3DxSkinInfoPtr1);
            int num16 = 0;
            long num17 = (long) num10;
            if (0L < num17)
            {
              byte* numPtr3 = numPtr2;
              ulong num18 = (ulong) num17;
              do
              {
                D3DXMATRIX d3Dxmatrix2 = d3Dxmatrix1;
                \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EBONE_SUB_CHUNK_3\u002EGetMatrix((BONE_SUB_CHUNK_3*) ((long) *numPtr3 * 64L /*0x40*/ + *(long*) ((IntPtr) this.m_CompiledModel + 148L)), &d3Dxmatrix2);
                sbyte* numPtr4 = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECopyXFileSafeString((sbyte*) *(long*) (*(long*) ((IntPtr) this.m_CompiledModel + 124L) + (long) *numPtr3 * 8L));
                ID3DXSkinInfo* id3DxSkinInfoPtr2 = id3DxSkinInfoPtr1;
                int num19 = num16;
                sbyte* numPtr5 = numPtr4;
                // ISSUE: cast to a function pointer type
                // ISSUE: function pointer call
                int num20 = __calli((__FnPtr<int (IntPtr, uint, sbyte*)>) *(long*) (*(long*) id3DxSkinInfoPtr1 + 112L /*0x70*/))((IntPtr) id3DxSkinInfoPtr2, (uint) num19, numPtr5);
                \u003CModule\u003E.delete\u005B\u005D((void*) numPtr4);
                ID3DXSkinInfo* id3DxSkinInfoPtr3 = id3DxSkinInfoPtr1;
                int num21 = num16;
                ref D3DXMATRIX local3 = ref d3Dxmatrix2;
                // ISSUE: cast to a function pointer type
                // ISSUE: function pointer call
                int num22 = __calli((__FnPtr<int (IntPtr, uint, D3DXMATRIX*)>) *(long*) (*(long*) id3DxSkinInfoPtr1 + 128L /*0x80*/))((IntPtr) id3DxSkinInfoPtr3, (uint) num21, (D3DXMATRIX*) ref local3);
                ++num16;
                ++numPtr3;
                --num18;
              }
              while (num18 > 0UL);
            }
            int num23 = 0;
            if (0L < num17)
            {
              byte* numPtr6 = numPtr2;
              ulong num24 = (ulong) num17;
              do
              {
                int num25 = 0;
                uint num26 = (uint) *(int*) ((IntPtr) submPtr + 36L);
                if (0U < num26)
                {
                  byte num27 = *numPtr6;
                  VertexBones* vertexBonesPtr = (VertexBones*) ((IntPtr) bones + 1L);
                  uint num28 = num26;
                  do
                  {
                    if ((int) *(byte*) ((IntPtr) vertexBonesPtr - 1L) == (int) num27 && *(byte*) ((IntPtr) vertexBonesPtr + 3L) != (byte) 0 || (int) *(byte*) vertexBonesPtr == (int) num27 && *(byte*) ((IntPtr) vertexBonesPtr + 4L) != (byte) 0 || (int) *(byte*) ((IntPtr) vertexBonesPtr + 1L) == (int) num27 && *(byte*) ((IntPtr) vertexBonesPtr + 5L) != (byte) 0 || (int) *(byte*) ((IntPtr) vertexBonesPtr + 2L) == (int) num27 && *(byte*) ((IntPtr) vertexBonesPtr + 6L) != (byte) 0)
                      ++num25;
                    vertexBonesPtr += 9L;
                    num28 += uint.MaxValue;
                  }
                  while (num28 > 0U);
                }
                ulong num29 = (ulong) num25;
                ulong num30 = num29;
                uint* numPtr7 = (uint*) \u003CModule\u003E.new\u005B\u005D(num30 > 4611686018427387903UL /*0x3FFFFFFFFFFFFFFF*/ ? ulong.MaxValue : num30 * 4UL);
                ulong num31 = num29;
                float* numPtr8 = (float*) \u003CModule\u003E.new\u005B\u005D(num31 > 4611686018427387903UL /*0x3FFFFFFFFFFFFFFF*/ ? ulong.MaxValue : num31 * 4UL);
                int num32 = 0;
                if (0U < (uint) *(int*) ((IntPtr) submPtr + 36L))
                {
                  float* numPtr9 = numPtr8;
                  uint* numPtr10 = numPtr7;
                  VertexBones* vertexBonesPtr = (VertexBones*) ((IntPtr) bones + 4L);
                  do
                  {
                    byte num33 = *numPtr6;
                    if ((int) *(byte*) ((IntPtr) vertexBonesPtr - 4L) == (int) num33 && *(byte*) vertexBonesPtr != (byte) 0)
                    {
                      *numPtr10 = (uint) num32;
                      *numPtr9 = (float) *(byte*) vertexBonesPtr / (float) byte.MaxValue;
                      numPtr10 += 4L;
                      numPtr9 += 4L;
                    }
                    else if ((int) *(byte*) ((IntPtr) vertexBonesPtr - 3L) == (int) num33 && *(byte*) ((IntPtr) vertexBonesPtr + 1L) != (byte) 0)
                    {
                      *numPtr10 = (uint) num32;
                      *numPtr9 = (float) *(byte*) ((IntPtr) vertexBonesPtr + 1L) / (float) byte.MaxValue;
                      numPtr10 += 4L;
                      numPtr9 += 4L;
                    }
                    else if ((int) *(byte*) ((IntPtr) vertexBonesPtr - 2L) == (int) num33 && *(byte*) ((IntPtr) vertexBonesPtr + 2L) != (byte) 0)
                    {
                      *numPtr10 = (uint) num32;
                      *numPtr9 = (float) *(byte*) ((IntPtr) vertexBonesPtr + 2L) / (float) byte.MaxValue;
                      numPtr10 += 4L;
                      numPtr9 += 4L;
                    }
                    else if ((int) *(byte*) ((IntPtr) vertexBonesPtr - 1L) == (int) num33 && *(byte*) ((IntPtr) vertexBonesPtr + 3L) != (byte) 0)
                    {
                      *numPtr10 = (uint) num32;
                      *numPtr9 = (float) *(byte*) ((IntPtr) vertexBonesPtr + 3L) / (float) byte.MaxValue;
                      numPtr10 += 4L;
                      numPtr9 += 4L;
                    }
                    ++num32;
                    vertexBonesPtr += 9L;
                  }
                  while ((uint) num32 < (uint) *(int*) ((IntPtr) submPtr + 36L));
                }
                ID3DXSkinInfo* id3DxSkinInfoPtr4 = id3DxSkinInfoPtr1;
                int num34 = num23;
                int num35 = num25;
                uint* numPtr11 = numPtr7;
                float* numPtr12 = numPtr8;
                // ISSUE: cast to a function pointer type
                // ISSUE: function pointer call
                int num36 = __calli((__FnPtr<int (IntPtr, uint, uint, uint*, float*)>) *(long*) (*(long*) id3DxSkinInfoPtr1 + 24L))((IntPtr) id3DxSkinInfoPtr4, (uint) num34, (uint) num35, numPtr11, numPtr12);
                \u003CModule\u003E.delete\u005B\u005D((void*) numPtr7);
                \u003CModule\u003E.delete\u005B\u005D((void*) numPtr8);
                ++num23;
                ++numPtr6;
                --num24;
              }
              while (num24 > 0UL);
            }
            \u003CModule\u003E.delete\u005B\u005D((void*) numPtr2);
            \u003CModule\u003E.delete\u005B\u005D((void*) bones);
          }
          _D3DXMESHCONTAINER* d3DxmeshcontainerPtr = (_D3DXMESHCONTAINER*) \u003CModule\u003E.@new(72UL);
          *(long*) ((IntPtr) d3DxextendedframePtr5 + 72L) = (long) d3DxmeshcontainerPtr;
          *(int*) ((IntPtr) d3DxmeshcontainerPtr + 8L) = 1;
          *(long*) (*(long*) ((IntPtr) d3DxextendedframePtr5 + 72L) + 16L /*0x10*/) = (long) id3DxMeshPtr1;
          *(long*) *(long*) ((IntPtr) d3DxextendedframePtr5 + 72L) = 0L;
          *(long*) (*(long*) ((IntPtr) d3DxextendedframePtr5 + 72L) + 24L) = (long) d3DxmaterialPtr;
          *(int*) (*(long*) ((IntPtr) d3DxextendedframePtr5 + 72L) + 40L) = 1;
          *(long*) (*(long*) ((IntPtr) d3DxextendedframePtr5 + 72L) + 48L /*0x30*/) = 0L;
          *(long*) (*(long*) ((IntPtr) d3DxextendedframePtr5 + 72L) + 32L /*0x20*/) = 0L;
          *(long*) (*(long*) ((IntPtr) d3DxextendedframePtr5 + 72L) + 64L /*0x40*/) = 0L;
          *(long*) (*(long*) ((IntPtr) d3DxextendedframePtr5 + 72L) + 56L) = (long) id3DxSkinInfoPtr1;
          \u003CModule\u003E.D3DXFrameAppendChild((_D3DXFRAME*) d3DxextendedframePtr2, (_D3DXFRAME*) d3DxextendedframePtr5);
          ++num1;
          num2 += 8L;
        }
        else
          goto label_59;
      }
      while ((uint) num1 < (uint) *(int*) ((IntPtr) this.m_CompiledModel + 99L));
      goto label_60;
label_59:
      throw new System.Exception("D3DXCreateMeshFVF failed");
    }
label_60:
    \u003CModule\u003E.D3DXFrameAppendChild((_D3DXFRAME*) this.m_SceneRoot, (_D3DXFRAME*) \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EGetHelpers(this.m_CompiledModel, 0, (D3DXMATRIX*) ((IntPtr) this.m_SceneRoot + 8L)));
    CompiledModel* compiledModel1 = this.m_CompiledModel;
    if (*(byte*) ((IntPtr) compiledModel1 + 8L) != (byte) 0)
    {
      switch (*(int*) ((IntPtr) compiledModel1 + 103L))
      {
        case 0:
        case 1:
          break;
        default:
          \u003CModule\u003E.D3DXFrameAppendChild((_D3DXFRAME*) this.m_SceneRoot, (_D3DXFRAME*) \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002ECreateBoneHeirarchy(compiledModel1, 1, (D3DXMATRIX*) ((IntPtr) this.m_SceneRoot + 8L)));
          break;
      }
    }
    D3DXEXTENDEDFRAME* d3DxextendedframePtr6 = (D3DXEXTENDEDFRAME*) \u003CModule\u003E.@new(168UL);
    D3DXEXTENDEDFRAME* d3DxextendedframePtr7;
    // ISSUE: fault handler
    try
    {
      d3DxextendedframePtr7 = (IntPtr) d3DxextendedframePtr6 == IntPtr.Zero ? (D3DXEXTENDEDFRAME*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ED3DXEXTENDEDFRAME\u002E\u007Bctor\u007D(d3DxextendedframePtr6);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) d3DxextendedframePtr6);
    }
    *(long*) d3DxextendedframePtr7 = (long) \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECopyXFileSafeString((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0P\u0040CAOGKCDA\u0040Orphan_Helpers\u003F\u0024AA\u0040);
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) d3DxextendedframePtr7 + 8L, ref d3Dxmatrix1, 64 /*0x40*/);
    \u003CModule\u003E.D3DXFrameAppendChild((_D3DXFRAME*) d3DxextendedframePtr7, (_D3DXFRAME*) \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EGetHelpers(this.m_CompiledModel, -1, &d3Dxmatrix1));
    \u003CModule\u003E.D3DXFrameAppendChild((_D3DXFRAME*) this.m_SceneRoot, (_D3DXFRAME*) d3DxextendedframePtr7);
  }

  private unsafe D3DXMATRIX* GetBoneTransform(D3DXEXTENDEDFRAME* frame, sbyte* name)
  {
    ulong num1 = (ulong) *(long*) frame;
    if (num1 != 0UL)
    {
      sbyte* numPtr1 = name;
      sbyte num2 = *(sbyte*) num1;
      sbyte num3 = *name;
      if ((int) num2 >= (int) num3)
      {
        sbyte* numPtr2 = (sbyte*) ((long) num1 - (IntPtr) name);
        while ((int) num2 <= (int) num3)
        {
          if (num2 != (sbyte) 0)
          {
            ++numPtr1;
            num2 = *(sbyte*) ((IntPtr) numPtr2 + (IntPtr) numPtr1);
            num3 = *numPtr1;
            if ((int) num2 < (int) num3)
              break;
          }
          else
          {
            D3DXMATRIX* boneTransform = (D3DXMATRIX*) \u003CModule\u003E.@new(64UL /*0x40*/);
            if ((IntPtr) boneTransform == IntPtr.Zero)
              return (D3DXMATRIX*) 0L;
            // ISSUE: cpblk instruction
            __memcpy((IntPtr) boneTransform, (IntPtr) frame + 8L, 64 /*0x40*/);
            return boneTransform;
          }
        }
      }
    }
    ulong frame1 = (ulong) *(long*) ((IntPtr) frame + 88L);
    if (frame1 != 0UL)
    {
      D3DXMATRIX* boneTransform = this.GetBoneTransform((D3DXEXTENDEDFRAME*) frame1, name);
      if ((IntPtr) boneTransform != IntPtr.Zero)
      {
        D3DXMATRIX d3Dxmatrix;
        \u003CModule\u003E.D3DXMatrixMultiply(&d3Dxmatrix, boneTransform, (D3DXMATRIX*) ((IntPtr) frame + 8L));
        // ISSUE: cpblk instruction
        __memcpy((IntPtr) boneTransform, ref d3Dxmatrix, 64 /*0x40*/);
        return boneTransform;
      }
    }
    ulong frame2 = (ulong) *(long*) ((IntPtr) frame + 80L /*0x50*/);
    if (frame2 != 0UL)
    {
      D3DXMATRIX* boneTransform = this.GetBoneTransform((D3DXEXTENDEDFRAME*) frame2, name);
      if ((IntPtr) boneTransform != IntPtr.Zero)
        return boneTransform;
    }
    return (D3DXMATRIX*) 0L;
  }

  [HandleProcessCorruptedStateExceptions]
  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EGfxModelLOD();
    }
    else
    {
      try
      {
        this.\u0021GfxModelLOD();
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

  ~GfxModelLOD() => this.Dispose(false);
}
