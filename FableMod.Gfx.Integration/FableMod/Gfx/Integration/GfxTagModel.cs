// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.GfxTagModel
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using \u003CCppImplementationDetails\u003E;
using FableMod.BIG;
using FableMod.CLRCore;
using FableMod.Data;
using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.Gfx.Integration;

public class GfxTagModel : GfxBaseModel
{
  public unsafe GfxTagModel(AssetEntry entry)
    : base(entry)
  {
    // ISSUE: fault handler
    try
    {
      byte[] data = entry.Data;
      int length = data.Length;
      if (data[length - 3] == (byte) 17 && data[length - 2] == (byte) 0 && data[length - 1] == (byte) 0)
      {
        byte[] numArray = LZO.DecompressRaw(data, 4, length - 4);
        fixed (byte* pBuffer = &numArray[0])
        {
          // ISSUE: variable of a reference type
          byte* local;
          // ISSUE: fault handler
          try
          {
            BufferStream stream = new BufferStream((void*) pBuffer, numArray.Length);
            this.ReadData(stream);
            stream.Close();
          }
          __fault
          {
            // ISSUE: cast to a reference type
            local = (byte*) 0L;
          }
          // ISSUE: cast to a reference type
          local = (byte*) 0L;
        }
      }
      else
      {
        BufferStream stream = new BufferStream((void*) entry.GetData(), (int) entry.Length);
        this.ReadData(stream);
        stream.Close();
      }
    }
    __fault
    {
      base.Dispose(true);
    }
  }

  private void \u007EGfxTagModel()
  {
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
    _D3DXFRAME* d3DxframePtr;
    if (\u003CModule\u003E.D3DXLoadMeshHierarchyFromXW((char*) hglobalUni.ToPointer(), 544U, idirect3Ddevice9Ptr, (ID3DXAllocateHierarchy*) allocateHierarchyPtr2, (ID3DXLoadUserData*) 0L, &d3DxframePtr, (ID3DXAnimationController**) 0L) < 0)
    {
      Marshal.FreeHGlobal(hglobalUni);
      throw new System.Exception("Failed to import file");
    }
    Marshal.FreeHGlobal(hglobalUni);
    D3DXEXTENDEDFRAME* pRoot1 = this.m_pRoot;
    if ((IntPtr) pRoot1 != IntPtr.Zero)
    {
      \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ED3DXEXTENDEDFRAME\u002EDestroy(pRoot1);
      D3DXEXTENDEDFRAME* pRoot2 = this.m_pRoot;
      if ((IntPtr) pRoot2 != IntPtr.Zero)
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
              \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ED3DXEXTENDEDFRAME\u002EDestroy(pRoot2);
            }
            __fault
            {
              // ISSUE: method pointer
              // ISSUE: cast to a function pointer type
              \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D), (void*) ((IntPtr) pRoot2 + 136L));
            }
            \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D((SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) pRoot2 + 136L));
          }
          __fault
          {
            // ISSUE: method pointer
            // ISSUE: cast to a function pointer type
            \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D), (void*) ((IntPtr) pRoot2 + 120L));
          }
          \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D((SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) pRoot2 + 120L));
        }
        __fault
        {
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D), (void*) ((IntPtr) pRoot2 + 104L));
        }
        \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D((SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) pRoot2 + 104L));
        \u003CModule\u003E.delete((void*) pRoot2);
      }
    }
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
    this.m_pRoot = (D3DXEXTENDEDFRAME*) d3DxframePtr;
  }

  public unsafe void Clear()
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
    if ((IntPtr) pNode != IntPtr.Zero)
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr1 = pNode;
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr2 = pointerFableModGfxNodePtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) pointerFableModGfxNodePtr1)((IntPtr) pointerFableModGfxNodePtr2, 1U);
      this.m_pNode = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
    }
    D3DXEXTENDEDFRAME* d3DxextendedframePtr1 = (D3DXEXTENDEDFRAME*) \u003CModule\u003E.@new(168UL);
    D3DXEXTENDEDFRAME* d3DxextendedframePtr2;
    // ISSUE: fault handler
    try
    {
      if ((IntPtr) d3DxextendedframePtr1 != IntPtr.Zero)
      {
        SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E* fableModGfxTexturePtr1 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) d3DxextendedframePtr1 + 104L);
        *(long*) fableModGfxTexturePtr1 = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VTexture\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
        *(long*) ((IntPtr) fableModGfxTexturePtr1 + 8L) = 0L;
        // ISSUE: fault handler
        try
        {
          SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E* fableModGfxTexturePtr2 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) d3DxextendedframePtr1 + 120L);
          *(long*) fableModGfxTexturePtr2 = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VTexture\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
          *(long*) ((IntPtr) fableModGfxTexturePtr2 + 8L) = 0L;
          // ISSUE: fault handler
          try
          {
            SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E* fableModGfxTexturePtr3 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E*) ((IntPtr) d3DxextendedframePtr1 + 136L);
            *(long*) fableModGfxTexturePtr3 = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VTexture\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
            *(long*) ((IntPtr) fableModGfxTexturePtr3 + 8L) = 0L;
            // ISSUE: fault handler
            try
            {
              // ISSUE: initblk instruction
              __memset((IntPtr) d3DxextendedframePtr1, 0, 168);
            }
            __fault
            {
              // ISSUE: method pointer
              // ISSUE: cast to a function pointer type
              \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D), (void*) ((IntPtr) d3DxextendedframePtr1 + 136L));
            }
          }
          __fault
          {
            // ISSUE: method pointer
            // ISSUE: cast to a function pointer type
            \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D), (void*) ((IntPtr) d3DxextendedframePtr1 + 120L));
          }
        }
        __fault
        {
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ATexture\u003E\u002E\u007Bdtor\u007D), (void*) ((IntPtr) d3DxextendedframePtr1 + 104L));
        }
        d3DxextendedframePtr2 = d3DxextendedframePtr1;
      }
      else
        d3DxextendedframePtr2 = (D3DXEXTENDEDFRAME*) 0L;
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) d3DxextendedframePtr1);
    }
    this.m_pRoot = d3DxextendedframePtr2;
    *(float*) ((IntPtr) d3DxextendedframePtr2 + 8L + 56L) = 0.0f;
    *(float*) ((IntPtr) d3DxextendedframePtr2 + 8L + 52L) = 0.0f;
    *(float*) ((IntPtr) d3DxextendedframePtr2 + 8L + 48L /*0x30*/) = 0.0f;
    *(float*) ((IntPtr) d3DxextendedframePtr2 + 8L + 44L) = 0.0f;
    *(float*) ((IntPtr) d3DxextendedframePtr2 + 8L + 36L) = 0.0f;
    *(float*) ((IntPtr) d3DxextendedframePtr2 + 8L + 32L /*0x20*/) = 0.0f;
    *(float*) ((IntPtr) d3DxextendedframePtr2 + 8L + 28L) = 0.0f;
    *(float*) ((IntPtr) d3DxextendedframePtr2 + 8L + 24L) = 0.0f;
    *(float*) ((IntPtr) d3DxextendedframePtr2 + 8L + 16L /*0x10*/) = 0.0f;
    *(float*) ((IntPtr) d3DxextendedframePtr2 + 8L + 12L) = 0.0f;
    *(float*) ((IntPtr) d3DxextendedframePtr2 + 8L + 8L) = 0.0f;
    *(float*) ((IntPtr) d3DxextendedframePtr2 + 8L + 4L) = 0.0f;
    *(float*) ((IntPtr) d3DxextendedframePtr2 + 8L + 60L) = 1f;
    *(float*) ((IntPtr) d3DxextendedframePtr2 + 8L + 40L) = 1f;
    *(float*) ((IntPtr) d3DxextendedframePtr2 + 8L + 20L) = 1f;
    *(float*) ((IntPtr) d3DxextendedframePtr2 + 8L) = 1f;
  }

  public override unsafe void CompileToEntry(AssetEntry entry)
  {
    BufferStream stream = new BufferStream();
    stream.WriteUInt32(1044266558U /*0x3E3E3E3E*/);
    stream.WriteUInt32(1179468851U);
    stream.WriteUInt32(100U);
    stream.WriteZString((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0CE\u0040BFKMGDCD\u0040Copyright\u003F5Big\u003F5Blue\u003F5Box\u003F5Studios\u003F5L\u0040);
    this.Compile3DRT(stream);
    entry.SetData((sbyte*) stream.GetData(), (uint) stream.GetWritten());
    stream.Close();
  }

  protected override unsafe Node* BuildModel()
  {
    Node* pSpatial = GfxBaseModel.BuildGfx(this.m_pRoot, (Node*) 0L);
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002E\u003FA0xd127aafc\u002EApplyEffects((Spatial*) pSpatial);
    return pSpatial;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  private unsafe bool ReadChunkInfo(BufferStream stream, SChunk* pChunk)
  {
    int num = stream.GetSize() - 8;
    if (stream.Tell() >= num)
      return false;
    stream.Read((void*) pChunk, 8);
    return true;
  }

  private unsafe void ReadData(BufferStream stream)
  {
    stream.Ignore(4);
    SChunk schunk;
    this.ReadChunkInfo(stream, &schunk);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    if (^(int&) ref schunk != 1179468851)
      throw new System.Exception("Invalid starting tag");
    string str = stream.MReadZString();
    if (str != "Copyright Big Blue Box Studios Ltd.")
      throw new System.Exception($"Invalid signature {str}");
    if (!this.ReadChunkInfo(stream, &schunk))
      return;
    do
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      if (^(int&) ref schunk == 1414677555)
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        this.Read3DRT(^(int&) ((IntPtr) &schunk + 4), stream);
      }
      else
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        stream.Ignore(^(int&) ((IntPtr) &schunk + 4));
      }
    }
    while (this.ReadChunkInfo(stream, &schunk));
  }

  private unsafe void Read3DRT(int iLength, BufferStream stream)
  {
    int iOffset = stream.Tell();
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
    this.m_pRoot = d3DxextendedframePtr2;
    *(long*) this.m_pRoot = (long) \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECopyXFileSafeString((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0L\u0040GLANBBEC\u0040Scene\u003F5Root\u003F\u0024AA\u0040);
    D3DXMATRIX* d3DxmatrixPtr = (D3DXMATRIX*) ((IntPtr) this.m_pRoot + 8L);
    *(float*) ((IntPtr) d3DxmatrixPtr + 56L) = 0.0f;
    *(float*) ((IntPtr) d3DxmatrixPtr + 52L) = 0.0f;
    *(float*) ((IntPtr) d3DxmatrixPtr + 48L /*0x30*/) = 0.0f;
    *(float*) ((IntPtr) d3DxmatrixPtr + 44L) = 0.0f;
    *(float*) ((IntPtr) d3DxmatrixPtr + 36L) = 0.0f;
    *(float*) ((IntPtr) d3DxmatrixPtr + 32L /*0x20*/) = 0.0f;
    *(float*) ((IntPtr) d3DxmatrixPtr + 28L) = 0.0f;
    *(float*) ((IntPtr) d3DxmatrixPtr + 24L) = 0.0f;
    *(float*) ((IntPtr) d3DxmatrixPtr + 16L /*0x10*/) = 0.0f;
    *(float*) ((IntPtr) d3DxmatrixPtr + 12L) = 0.0f;
    *(float*) ((IntPtr) d3DxmatrixPtr + 8L) = 0.0f;
    *(float*) ((IntPtr) d3DxmatrixPtr + 4L) = 0.0f;
    *(float*) ((IntPtr) d3DxmatrixPtr + 60L) = 1f;
    *(float*) ((IntPtr) d3DxmatrixPtr + 40L) = 1f;
    *(float*) ((IntPtr) d3DxmatrixPtr + 20L) = 1f;
    *(float*) d3DxmatrixPtr = 1f;
    SChunk schunk;
    if (this.ReadChunkInfo(stream, &schunk))
    {
      do
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if (^(int&) ref schunk == 1397511245)
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          this.ReadMTLS(^(int&) ((IntPtr) &schunk + 4), stream);
        }
        else
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          if (^(int&) ref schunk == 1296192851)
          {
            GfxTagModel gfxTagModel = this;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            gfxTagModel.ReadSUBM(gfxTagModel.m_pRoot, ^(int&) ((IntPtr) &schunk + 4), stream);
          }
          else
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            stream.Ignore(^(int&) ((IntPtr) &schunk + 4));
          }
        }
      }
      while (stream.Tell() - iOffset < iLength && this.ReadChunkInfo(stream, &schunk));
    }
    stream.Seek(iOffset);
    stream.Ignore(iLength);
  }

  private unsafe void ReadMTLS(int iLength, BufferStream stream)
  {
    int iOffset = stream.Tell();
    SChunk schunk;
    if (this.ReadChunkInfo(stream, &schunk))
    {
      do
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if (^(int&) ref schunk == 1280463949)
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          this.ReadMTRL(^(int&) ((IntPtr) &schunk + 4), stream);
        }
        else
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          if (^(int&) ref schunk == 1162630221)
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            this.ReadMTLE(^(int&) ((IntPtr) &schunk + 4), stream);
          }
          else
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            stream.Ignore(^(int&) ((IntPtr) &schunk + 4));
          }
        }
      }
      while (stream.Tell() - iOffset < iLength && this.ReadChunkInfo(stream, &schunk));
    }
    stream.Seek(iOffset);
    stream.Ignore(iLength);
  }

  private unsafe void ReadMTRL(int iLength, BufferStream stream)
  {
    int iOffset = stream.Tell();
    sbyte* numPtr = stream.ReadZString();
    Console.WriteLine("Material: \"{0}\"", (object) new string(numPtr));
    \u003CModule\u003E.delete\u005B\u005D((void*) numPtr);
    stream.Seek(iOffset);
    stream.Ignore(iLength);
  }

  private void ReadMTLE(int iLength, BufferStream stream) => stream.Ignore(iLength);

  private unsafe void ReadHLPR(D3DXEXTENDEDFRAME* pParent, int iLength, BufferStream stream)
  {
    Console.WriteLine("Reading HLPR...");
    int iOffset = stream.Tell();
    D3DXEXTENDEDFRAME* d3DxextendedframePtr = (D3DXEXTENDEDFRAME*) \u003CModule\u003E.@new(168UL);
    D3DXEXTENDEDFRAME* pFrame;
    // ISSUE: fault handler
    try
    {
      pFrame = (IntPtr) d3DxextendedframePtr == IntPtr.Zero ? (D3DXEXTENDEDFRAME*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ED3DXEXTENDEDFRAME\u002E\u007Bctor\u007D(d3DxextendedframePtr);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) d3DxextendedframePtr);
    }
    *(float*) ((IntPtr) pFrame + 8L + 56L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 52L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 48L /*0x30*/) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 44L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 36L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 32L /*0x20*/) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 28L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 24L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 16L /*0x10*/) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 12L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 8L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 4L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 60L) = 1f;
    *(float*) ((IntPtr) pFrame + 8L + 40L) = 1f;
    *(float*) ((IntPtr) pFrame + 8L + 20L) = 1f;
    *(float*) ((IntPtr) pFrame + 8L) = 1f;
    \u003CModule\u003E.D3DXFrameAppendChild((_D3DXFRAME*) pParent, (_D3DXFRAME*) pFrame);
    SChunk schunk;
    if (this.ReadChunkInfo(stream, &schunk))
    {
      do
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if (^(int&) ref schunk == 1280721736)
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          this.ReadHCVL(pFrame, ^(int&) ((IntPtr) &schunk + 4), stream);
        }
        else
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          if (^(int&) ref schunk == 1414418504)
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            this.ReadHPNT(pFrame, ^(int&) ((IntPtr) &schunk + 4), stream);
          }
          else
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            stream.Ignore(^(int&) ((IntPtr) &schunk + 4));
          }
        }
      }
      while (stream.Tell() - iOffset < iLength && this.ReadChunkInfo(stream, &schunk));
    }
    stream.Seek(iOffset);
    stream.Ignore(iLength);
  }

  private unsafe void ReadHPNT(D3DXEXTENDEDFRAME* pFrame, int iLength, BufferStream stream)
  {
    long num = *(long*) ((IntPtr) \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA + 8L);
    int iOffset = stream.Tell();
    D3DXVECTOR3 d3DxvectoR3;
    stream.Read((void*) &d3DxvectoR3, 12);
    stream.Ignore(4);
    ID3DXMesh* id3DxMeshPtr = (ID3DXMesh*) 0L;
    ref ID3DXMesh* local = ref id3DxMeshPtr;
    if (\u003CModule\u003E.D3DXCreateSphere((IDirect3DDevice9*) num, 10f, 8U, 8U, (ID3DXMesh**) ref local, (ID3DXBuffer**) 0L) < 0)
      throw new System.Exception("Failed to create mesh");
    _D3DXMATERIAL* d3DxmaterialPtr = (_D3DXMATERIAL*) \u003CModule\u003E.@new(80UL /*0x50*/);
    // ISSUE: initblk instruction
    __memset((IntPtr) d3DxmaterialPtr, 0, 80 /*0x50*/);
    *(float*) ((IntPtr) d3DxmaterialPtr + 12L) = 1f;
    *(float*) d3DxmaterialPtr = 0.95f;
    *(float*) ((IntPtr) d3DxmaterialPtr + 4L) = 0.95f;
    *(float*) ((IntPtr) d3DxmaterialPtr + 8L) = 0.6f;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) d3DxmaterialPtr + 16L /*0x10*/, (IntPtr) d3DxmaterialPtr, 16 /*0x10*/);
    _D3DXMESHCONTAINER* d3DxmeshcontainerPtr = (_D3DXMESHCONTAINER*) \u003CModule\u003E.@new(72UL);
    *(int*) ((IntPtr) d3DxmeshcontainerPtr + 8L) = 1;
    *(long*) ((IntPtr) d3DxmeshcontainerPtr + 16L /*0x10*/) = (long) id3DxMeshPtr;
    *(long*) d3DxmeshcontainerPtr = 0L;
    *(long*) ((IntPtr) d3DxmeshcontainerPtr + 24L) = (long) d3DxmaterialPtr;
    *(int*) ((IntPtr) d3DxmeshcontainerPtr + 40L) = 1;
    *(long*) ((IntPtr) d3DxmeshcontainerPtr + 48L /*0x30*/) = 0L;
    *(long*) ((IntPtr) d3DxmeshcontainerPtr + 32L /*0x20*/) = 0L;
    *(long*) ((IntPtr) d3DxmeshcontainerPtr + 64L /*0x40*/) = 0L;
    *(long*) ((IntPtr) d3DxmeshcontainerPtr + 56L) = 0L;
    *(long*) ((IntPtr) pFrame + 72L) = (long) d3DxmeshcontainerPtr;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    \u003CModule\u003E.D3DXMatrixTranslation((D3DXMATRIX*) ((IntPtr) pFrame + 8L), ^(float&) ref d3DxvectoR3, ^(float&) ((IntPtr) &d3DxvectoR3 + 4), ^(float&) ((IntPtr) &d3DxvectoR3 + 8));
    stream.Seek(iOffset);
    stream.Ignore(iLength);
  }

  private unsafe void ReadHCVL(D3DXEXTENDEDFRAME* pFrame, int iLength, BufferStream stream)
  {
    int iOffset = stream.Tell();
    stream.Ignore(4);
    sbyte* numPtr = stream.ReadZString();
    \u0024ArrayType\u0024\u0024\u0024BY0BAA\u0040D arrayTypeBy0BaaD;
    \u003CModule\u003E.sprintf((sbyte*) &arrayTypeBy0BaaD, (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_07PLGIHJEC\u0040HCVL_\u003F\u0024CFs\u003F\u0024AA\u0040, __arglist ((IntPtr) numPtr));
    *(long*) pFrame = (long) \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECopyXFileSafeString((sbyte*) &arrayTypeBy0BaaD);
    \u003CModule\u003E.delete\u005B\u005D((void*) numPtr);
    stream.Seek(iOffset);
    stream.Ignore(iLength);
  }

  private unsafe void ReadSUBM(D3DXEXTENDEDFRAME* pParent, int iLength, BufferStream stream)
  {
    Console.WriteLine("Reading SUBM...");
    int iOffset = stream.Tell();
    D3DXEXTENDEDFRAME* d3DxextendedframePtr = (D3DXEXTENDEDFRAME*) \u003CModule\u003E.@new(168UL);
    D3DXEXTENDEDFRAME* pFrame;
    // ISSUE: fault handler
    try
    {
      pFrame = (IntPtr) d3DxextendedframePtr == IntPtr.Zero ? (D3DXEXTENDEDFRAME*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ED3DXEXTENDEDFRAME\u002E\u007Bctor\u007D(d3DxextendedframePtr);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) d3DxextendedframePtr);
    }
    sbyte* src = stream.ReadZString();
    *(long*) pFrame = (long) \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECopyXFileSafeString(src);
    \u003CModule\u003E.delete\u005B\u005D((void*) src);
    *(float*) ((IntPtr) pFrame + 8L + 56L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 52L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 48L /*0x30*/) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 44L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 36L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 32L /*0x20*/) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 28L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 24L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 16L /*0x10*/) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 12L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 8L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 4L) = 0.0f;
    *(float*) ((IntPtr) pFrame + 8L + 60L) = 1f;
    *(float*) ((IntPtr) pFrame + 8L + 40L) = 1f;
    *(float*) ((IntPtr) pFrame + 8L + 20L) = 1f;
    *(float*) ((IntPtr) pFrame + 8L) = 1f;
    \u003CModule\u003E.D3DXFrameAppendChild((_D3DXFRAME*) pParent, (_D3DXFRAME*) pFrame);
    stream.Ignore(16 /*0x10*/);
    SChunk schunk;
    if (this.ReadChunkInfo(stream, &schunk))
    {
      do
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if (^(int&) ref schunk == 1296454228)
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          this.ReadTRFM(pFrame, ^(int&) ((IntPtr) &schunk + 4), stream);
        }
        else
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          if (^(int&) ref schunk == 1296650832)
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            this.ReadPRIM(pFrame, ^(int&) ((IntPtr) &schunk + 4), stream);
          }
          else
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            stream.Ignore(^(int&) ((IntPtr) &schunk + 4));
          }
        }
      }
      while (stream.Tell() - iOffset < iLength && this.ReadChunkInfo(stream, &schunk));
    }
    stream.Seek(iOffset);
    stream.Ignore(iLength);
  }

  private unsafe void ReadTRFM(D3DXEXTENDEDFRAME* pFrame, int iLength, BufferStream stream)
  {
    int iOffset = stream.Tell();
    \u0024ArrayType\u0024\u0024\u0024BY0M\u0040M arrayTypeBy0MM;
    stream.Read((void*) &arrayTypeBy0MM, 48 /*0x30*/);
    D3DXMATRIX d3Dxmatrix;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3Dxmatrix = ^(float&) ref arrayTypeBy0MM;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix + 4) = ^(float&) ((IntPtr) &arrayTypeBy0MM + 4);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix + 8) = ^(float&) ((IntPtr) &arrayTypeBy0MM + 8);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix + 12) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix + 16 /*0x10*/) = ^(float&) ((IntPtr) &arrayTypeBy0MM + 12);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix + 20) = ^(float&) ((IntPtr) &arrayTypeBy0MM + 16 /*0x10*/);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix + 24) = ^(float&) ((IntPtr) &arrayTypeBy0MM + 20);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix + 28) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix + 32 /*0x20*/) = ^(float&) ((IntPtr) &arrayTypeBy0MM + 24);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix + 36) = ^(float&) ((IntPtr) &arrayTypeBy0MM + 28);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix + 40) = ^(float&) ((IntPtr) &arrayTypeBy0MM + 32 /*0x20*/);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix + 44) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix + 48 /*0x30*/) = ^(float&) ((IntPtr) &arrayTypeBy0MM + 36);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix + 52) = ^(float&) ((IntPtr) &arrayTypeBy0MM + 40);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix + 56) = ^(float&) ((IntPtr) &arrayTypeBy0MM + 44);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3Dxmatrix + 60) = 1f;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) pFrame + 8L, ref d3Dxmatrix, 64 /*0x40*/);
    stream.Seek(iOffset);
    stream.Ignore(iLength);
  }

  private unsafe void ReadPRIM(D3DXEXTENDEDFRAME* pFrame, int iLength, BufferStream stream)
  {
    int iOffset = stream.Tell();
    IDirect3DDevice9* idirect3Ddevice9Ptr = (IDirect3DDevice9*) *(long*) ((IntPtr) \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA + 8L);
    stream.Ignore(4);
    int num1 = 0;
    ushort* numPtr1 = (ushort*) 0L;
    int num2 = 0;
    SVertex* svertexPtr1 = (SVertex*) 0L;
    SChunk schunk;
    if (this.ReadChunkInfo(stream, &schunk))
    {
      do
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if (^(int&) ref schunk == 1397314132)
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          this.ReadTRIS(^(int&) ((IntPtr) &schunk + 4), stream, &num1, &numPtr1);
        }
        else
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          if (^(int&) ref schunk == 1414677846)
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            this.ReadVERT(^(int&) ((IntPtr) &schunk + 4), stream, &num2, &svertexPtr1);
          }
          else
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            stream.Ignore(^(int&) ((IntPtr) &schunk + 4));
          }
        }
      }
      while (stream.Tell() - iOffset < iLength && this.ReadChunkInfo(stream, &schunk));
      if (num1 > 0 && num2 > 0)
      {
        ID3DXMesh* id3DxMeshPtr1 = (ID3DXMesh*) 0L;
        if (\u003CModule\u003E.D3DXCreateMeshFVF((uint) num1, (uint) num2, 544U, 274U, idirect3Ddevice9Ptr, &id3DxMeshPtr1) < 0)
          throw new System.Exception("Failed to create mesh");
        ushort* numPtr2 = (ushort*) 0L;
        ID3DXMesh* id3DxMeshPtr2 = id3DxMeshPtr1;
        ref ushort* local1 = ref numPtr2;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int num3 = __calli((__FnPtr<int (IntPtr, uint, void**)>) *(long*) (*(long*) id3DxMeshPtr1 + 136L))((IntPtr) id3DxMeshPtr2, 0U, (void**) ref local1);
        int num4 = num1 * 3;
        if (0 < num4)
        {
          ushort* numPtr3 = (ushort*) ((IntPtr) numPtr1 + 2L);
          long num5 = (long) -(IntPtr) numPtr1;
          ushort* numPtr4 = (ushort*) (2L - (IntPtr) numPtr1);
          ushort* numPtr5 = (ushort*) (-2L - (IntPtr) numPtr1);
          uint num6 = (uint) (num4 - 1) / 3U + 1U;
          do
          {
            *(short*) ((IntPtr) numPtr5 + (IntPtr) numPtr3 + (IntPtr) numPtr2) = (short) *(ushort*) ((IntPtr) numPtr3 + 2L);
            *(short*) (num5 + (IntPtr) numPtr3 + (IntPtr) numPtr2) = (short) *numPtr3;
            *(short*) ((IntPtr) numPtr4 + (IntPtr) numPtr3 + (IntPtr) numPtr2) = (short) *(ushort*) ((IntPtr) numPtr3 - 2L);
            numPtr3 += 6L;
            num6 += uint.MaxValue;
          }
          while (num6 > 0U);
        }
        ID3DXMesh* id3DxMeshPtr3 = id3DxMeshPtr1;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int num7 = __calli((__FnPtr<int (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr3 + 144L /*0x90*/))((IntPtr) id3DxMeshPtr3);
        SVertex* svertexPtr2 = (SVertex*) 0L;
        ID3DXMesh* id3DxMeshPtr4 = id3DxMeshPtr1;
        ref SVertex* local2 = ref svertexPtr2;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int num8 = __calli((__FnPtr<int (IntPtr, uint, void**)>) *(long*) (*(long*) id3DxMeshPtr1 + 120L))((IntPtr) id3DxMeshPtr4, 0U, (void**) ref local2);
        // ISSUE: cpblk instruction
        __memcpy((IntPtr) svertexPtr2, (IntPtr) svertexPtr1, (long) num2 * 32L /*0x20*/);
        ID3DXMesh* id3DxMeshPtr5 = id3DxMeshPtr1;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        int num9 = __calli((__FnPtr<int (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr5 + 128L /*0x80*/))((IntPtr) id3DxMeshPtr5);
        _D3DXMATERIAL* d3DxmaterialPtr = (_D3DXMATERIAL*) \u003CModule\u003E.@new(80UL /*0x50*/);
        // ISSUE: initblk instruction
        __memset((IntPtr) d3DxmaterialPtr, 0, 80 /*0x50*/);
        *(float*) ((IntPtr) d3DxmaterialPtr + 12L) = 0.8f;
        *(float*) d3DxmaterialPtr = 0.5f;
        *(float*) ((IntPtr) d3DxmaterialPtr + 4L) = 0.5f;
        *(float*) ((IntPtr) d3DxmaterialPtr + 8L) = 0.55f;
        // ISSUE: cpblk instruction
        __memcpy((IntPtr) d3DxmaterialPtr + 16L /*0x10*/, (IntPtr) d3DxmaterialPtr, 16 /*0x10*/);
        _D3DXMESHCONTAINER* d3DxmeshcontainerPtr = (_D3DXMESHCONTAINER*) \u003CModule\u003E.@new(72UL);
        *(int*) ((IntPtr) d3DxmeshcontainerPtr + 8L) = 1;
        *(long*) ((IntPtr) d3DxmeshcontainerPtr + 16L /*0x10*/) = (long) id3DxMeshPtr1;
        *(long*) d3DxmeshcontainerPtr = 0L;
        *(long*) ((IntPtr) d3DxmeshcontainerPtr + 24L) = (long) d3DxmaterialPtr;
        *(int*) ((IntPtr) d3DxmeshcontainerPtr + 40L) = 1;
        *(long*) ((IntPtr) d3DxmeshcontainerPtr + 48L /*0x30*/) = 0L;
        *(long*) ((IntPtr) d3DxmeshcontainerPtr + 32L /*0x20*/) = 0L;
        *(long*) ((IntPtr) d3DxmeshcontainerPtr + 64L /*0x40*/) = 0L;
        *(long*) ((IntPtr) d3DxmeshcontainerPtr + 56L) = 0L;
        *(long*) ((IntPtr) pFrame + 72L) = (long) d3DxmeshcontainerPtr;
        if ((IntPtr) svertexPtr1 != IntPtr.Zero)
          \u003CModule\u003E.delete\u005B\u005D((void*) svertexPtr1);
        if ((IntPtr) numPtr1 != IntPtr.Zero)
          \u003CModule\u003E.delete\u005B\u005D((void*) numPtr1);
        stream.Seek(iOffset);
        stream.Ignore(iLength);
        return;
      }
    }
    throw new System.Exception("Invalid mesh");
  }

  private unsafe void ReadTRIS(
    int iLength,
    BufferStream stream,
    int* piNumFaces,
    ushort** pawFaces)
  {
    int iOffset = stream.Tell();
    uint num = stream.ReadUInt32();
    ulong iCount = (ulong) (num * 3U) * 2UL;
    ushort* pData = (ushort*) \u003CModule\u003E.new\u005B\u005D(iCount);
    stream.Read((void*) pData, (int) iCount);
    *piNumFaces = (int) num;
    *(long*) pawFaces = (long) pData;
    stream.Seek(iOffset);
    stream.Ignore(iLength);
  }

  private unsafe void ReadVERT(
    int iLength,
    BufferStream stream,
    int* piNumVertices,
    SVertex** paVertices)
  {
    int iOffset = stream.Tell();
    uint num1 = stream.ReadUInt32();
    ulong num2 = (ulong) num1;
    SVertex* svertexPtr = (SVertex*) \u003CModule\u003E.new\u005B\u005D(num2 > 576460752303423487UL /*0x07FFFFFFFFFFFFFF*/ ? ulong.MaxValue : num2 * 32UL /*0x20*/);
    SVertex* pData;
    // ISSUE: fault handler
    try
    {
      pData = svertexPtr;
    }
    __fault
    {
      \u003CModule\u003E.delete\u005B\u005D((void*) svertexPtr);
    }
    stream.Read((void*) pData, (int) ((long) num2 * 32L /*0x20*/));
    *piNumVertices = (int) num1;
    *(long*) paVertices = (long) pData;
    stream.Seek(iOffset);
    stream.Ignore(iLength);
  }

  private unsafe void Compile3DRT(BufferStream stream)
  {
    \u0024ArrayType\u0024\u0024\u0024BY04D arrayTypeBy04D = (\u0024ArrayType\u0024\u0024\u0024BY04D) \u003CModule\u003E.\u003F\u003F_C\u0040_04HKMFGHFA\u00403DRT\u003F\u0024AA\u0040;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteUInt32((uint) ((((int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 3) << 8 | (int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 2)) << 8 | (int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 1)) << 8) | (uint) ^(sbyte&) ref arrayTypeBy04D);
    int written1 = stream.GetWritten();
    stream.WriteUInt32(uint.MaxValue);
    this.CompileMaterials(stream);
    GfxTagModel gfxTagModel = this;
    gfxTagModel.CompileFrame(gfxTagModel.m_pRoot, stream);
    int written2 = stream.GetWritten();
    stream.Seek(written1);
    stream.WriteUInt32((uint) (written2 - written1 - 4));
    stream.Seek(written2);
  }

  private unsafe void CompileMaterials(BufferStream stream)
  {
    \u0024ArrayType\u0024\u0024\u0024BY04D arrayTypeBy04D = (\u0024ArrayType\u0024\u0024\u0024BY04D) \u003CModule\u003E.\u003F\u003F_C\u0040_04DFMHHGNM\u0040MTLS\u003F\u0024AA\u0040;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteUInt32((uint) ((((int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 3) << 8 | (int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 2)) << 8 | (int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 1)) << 8) | (uint) ^(sbyte&) ref arrayTypeBy04D);
    int written1 = stream.GetWritten();
    stream.WriteUInt32(uint.MaxValue);
    stream.WriteUInt32(1280463949U);
    stream.WriteUInt32(58U);
    stream.WriteZString((sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0N\u0040GMGNLDOE\u0040Material\u003F5\u003F\u0024CD24\u003F\u0024AA\u0040);
    byte* pData = (byte*) \u003CModule\u003E.new\u005B\u005D(29UL);
    // ISSUE: initblk instruction
    __memset((IntPtr) pData, 0, 29);
    stream.Write((void*) pData, 29);
    \u003CModule\u003E.delete\u005B\u005D((void*) pData);
    stream.WriteUInt32(1162630221U);
    stream.WriteUInt32(8U);
    stream.WriteUInt32(1U);
    stream.WriteUInt32(0U);
    int written2 = stream.GetWritten();
    stream.Seek(written1);
    stream.WriteUInt32((uint) (written2 - written1 - 4));
    stream.Seek(written2);
  }

  private unsafe void CompileFrame(D3DXEXTENDEDFRAME* pFrame, BufferStream stream)
  {
    while (true)
    {
      if (*(long*) ((IntPtr) pFrame + 72L) != 0L)
        this.CompileSUBM(pFrame, stream);
      ulong pFrame1 = (ulong) *(long*) ((IntPtr) pFrame + 88L);
      if (pFrame1 != 0UL)
        this.CompileFrame((D3DXEXTENDEDFRAME*) pFrame1, stream);
      ulong num = (ulong) *(long*) ((IntPtr) pFrame + 80L /*0x50*/);
      if (num != 0UL)
        pFrame = (D3DXEXTENDEDFRAME*) num;
      else
        break;
    }
  }

  private unsafe void CompileSUBM(D3DXEXTENDEDFRAME* pFrame, BufferStream stream)
  {
    \u0024ArrayType\u0024\u0024\u0024BY04D arrayTypeBy04D = (\u0024ArrayType\u0024\u0024\u0024BY04D) \u003CModule\u003E.\u003F\u003F_C\u0040_04IMHECKIP\u0040SUBM\u003F\u0024AA\u0040;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteUInt32((uint) ((((int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 3) << 8 | (int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 2)) << 8 | (int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 1)) << 8) | (uint) ^(sbyte&) ref arrayTypeBy04D);
    int written1 = stream.GetWritten();
    stream.WriteUInt32(uint.MaxValue);
    stream.WriteZString((sbyte*) *(long*) pFrame);
    stream.WriteUInt32(0U);
    stream.WriteUInt32(uint.MaxValue);
    stream.WriteUInt32(uint.MaxValue);
    stream.WriteUInt32(uint.MaxValue);
    this.CompileTRFM(pFrame, stream);
    this.CompilePRIM((ID3DXMesh*) *(long*) (*(long*) ((IntPtr) pFrame + 72L) + 16L /*0x10*/), stream);
    int written2 = stream.GetWritten();
    stream.Seek(written1);
    stream.WriteUInt32((uint) (written2 - written1 - 4));
    stream.Seek(written2);
  }

  private unsafe void CompileTRFM(D3DXEXTENDEDFRAME* pFrame, BufferStream stream)
  {
    \u0024ArrayType\u0024\u0024\u0024BY04D arrayTypeBy04D = (\u0024ArrayType\u0024\u0024\u0024BY04D) \u003CModule\u003E.\u003F\u003F_C\u0040_04KEIKGGPK\u0040TRFM\u003F\u0024AA\u0040;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteUInt32((uint) ((((int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 3) << 8 | (int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 2)) << 8 | (int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 1)) << 8) | (uint) ^(sbyte&) ref arrayTypeBy04D);
    int written1 = stream.GetWritten();
    stream.WriteUInt32(uint.MaxValue);
    D3DXMATRIX d3Dxmatrix;
    // ISSUE: cpblk instruction
    __memcpy(ref d3Dxmatrix, (IntPtr) pFrame + 8L, 64 /*0x40*/);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteFloat(^(float&) ref d3Dxmatrix);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteFloat(^(float&) ((IntPtr) &d3Dxmatrix + 4));
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteFloat(^(float&) ((IntPtr) &d3Dxmatrix + 8));
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteFloat(^(float&) ((IntPtr) &d3Dxmatrix + 16 /*0x10*/));
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteFloat(^(float&) ((IntPtr) &d3Dxmatrix + 20));
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteFloat(^(float&) ((IntPtr) &d3Dxmatrix + 24));
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteFloat(^(float&) ((IntPtr) &d3Dxmatrix + 32 /*0x20*/));
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteFloat(^(float&) ((IntPtr) &d3Dxmatrix + 36));
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteFloat(^(float&) ((IntPtr) &d3Dxmatrix + 40));
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteFloat(^(float&) ((IntPtr) &d3Dxmatrix + 48 /*0x30*/));
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteFloat(^(float&) ((IntPtr) &d3Dxmatrix + 52));
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteFloat(^(float&) ((IntPtr) &d3Dxmatrix + 56));
    int written2 = stream.GetWritten();
    stream.Seek(written1);
    stream.WriteUInt32((uint) (written2 - written1 - 4));
    stream.Seek(written2);
  }

  private unsafe void CompilePRIM(ID3DXMesh* pMesh, BufferStream stream)
  {
    \u0024ArrayType\u0024\u0024\u0024BY04D arrayTypeBy04D = (\u0024ArrayType\u0024\u0024\u0024BY04D) \u003CModule\u003E.\u003F\u003F_C\u0040_04FKFGIHAH\u0040PRIM\u003F\u0024AA\u0040;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteUInt32((uint) ((((int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 3) << 8 | (int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 2)) << 8 | (int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 1)) << 8) | (uint) ^(sbyte&) ref arrayTypeBy04D);
    int written1 = stream.GetWritten();
    stream.WriteUInt32(uint.MaxValue);
    stream.WriteUInt32(0U);
    this.CompileTRIS(pMesh, stream);
    this.CompileVERT(pMesh, stream);
    int written2 = stream.GetWritten();
    stream.Seek(written1);
    stream.WriteUInt32((uint) (written2 - written1 - 4));
    stream.Seek(written2);
  }

  private unsafe void CompileTRIS(ID3DXMesh* pMesh, BufferStream stream)
  {
    \u0024ArrayType\u0024\u0024\u0024BY04D arrayTypeBy04D = (\u0024ArrayType\u0024\u0024\u0024BY04D) \u003CModule\u003E.\u003F\u003F_C\u0040_04HLJHBOBI\u0040TRIS\u003F\u0024AA\u0040;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteUInt32((uint) ((((int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 3) << 8 | (int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 2)) << 8 | (int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 1)) << 8) | (uint) ^(sbyte&) ref arrayTypeBy04D);
    int written1 = stream.GetWritten();
    stream.WriteUInt32(uint.MaxValue);
    BufferStream bufferStream = stream;
    ID3DXMesh* id3DxMeshPtr1 = pMesh;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int uiValue = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr1 + 32L /*0x20*/))((IntPtr) id3DxMeshPtr1);
    bufferStream.WriteUInt32((uint) uiValue);
    ushort* numPtr = (ushort*) 0L;
    ID3DXMesh* id3DxMeshPtr2 = pMesh;
    ref ushort* local = ref numPtr;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num1 = __calli((__FnPtr<int (IntPtr, uint, void**)>) *(long*) (*(long*) pMesh + 136L))((IntPtr) id3DxMeshPtr2, 0U, (void**) ref local);
    int num2 = 0;
    ID3DXMesh* id3DxMeshPtr3 = pMesh;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    if (0U < (uint) ((int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr3 + 32L /*0x20*/))((IntPtr) id3DxMeshPtr3) * 3))
    {
      int num3;
      int num4;
      do
      {
        stream.Write((void*) ((long) (num2 + 1 + 1) * 2L + (IntPtr) numPtr), 2);
        stream.Write((void*) ((long) (num2 + 1) * 2L + (IntPtr) numPtr), 2);
        stream.Write((void*) ((long) num2 * 2L + (IntPtr) numPtr), 2);
        num2 += 3;
        num3 = num2;
        ID3DXMesh* id3DxMeshPtr4 = pMesh;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        num4 = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr4 + 32L /*0x20*/))((IntPtr) id3DxMeshPtr4) * 3;
      }
      while ((uint) num3 < (uint) num4);
    }
    ID3DXMesh* id3DxMeshPtr5 = pMesh;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num5 = __calli((__FnPtr<int (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr5 + 144L /*0x90*/))((IntPtr) id3DxMeshPtr5);
    int written2 = stream.GetWritten();
    stream.Seek(written1);
    stream.WriteUInt32((uint) (written2 - written1 - 4));
    stream.Seek(written2);
  }

  private unsafe void CompileVERT(ID3DXMesh* pMesh, BufferStream stream)
  {
    \u0024ArrayType\u0024\u0024\u0024BY04D arrayTypeBy04D = (\u0024ArrayType\u0024\u0024\u0024BY04D) \u003CModule\u003E.\u003F\u003F_C\u0040_04JDKLPIAI\u0040VERT\u003F\u0024AA\u0040;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteUInt32((uint) ((((int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 3) << 8 | (int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 2)) << 8 | (int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 1)) << 8) | (uint) ^(sbyte&) ref arrayTypeBy04D);
    int written1 = stream.GetWritten();
    stream.WriteUInt32(uint.MaxValue);
    BufferStream bufferStream1 = stream;
    ID3DXMesh* id3DxMeshPtr1 = pMesh;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int uiValue = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr1 + 40L))((IntPtr) id3DxMeshPtr1);
    bufferStream1.WriteUInt32((uint) uiValue);
    SVertex* svertexPtr = (SVertex*) 0L;
    ID3DXMesh* id3DxMeshPtr2 = pMesh;
    ref SVertex* local = ref svertexPtr;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num1 = __calli((__FnPtr<int (IntPtr, uint, void**)>) *(long*) (*(long*) pMesh + 120L))((IntPtr) id3DxMeshPtr2, 0U, (void**) ref local);
    BufferStream bufferStream2 = stream;
    SVertex* pData = svertexPtr;
    ID3DXMesh* id3DxMeshPtr3 = pMesh;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int iCount = (int) ((long) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr3 + 40L))((IntPtr) id3DxMeshPtr3) * 32L /*0x20*/);
    bufferStream2.Write((void*) pData, iCount);
    ID3DXMesh* id3DxMeshPtr4 = pMesh;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num2 = __calli((__FnPtr<int (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr4 + 128L /*0x80*/))((IntPtr) id3DxMeshPtr4);
    int written2 = stream.GetWritten();
    stream.Seek(written1);
    stream.WriteUInt32((uint) (written2 - written1 - 4));
    stream.Seek(written2);
  }

  private unsafe void CompileUNIV(ID3DXMesh* pMesh, BufferStream stream)
  {
    \u0024ArrayType\u0024\u0024\u0024BY04D arrayTypeBy04D = (\u0024ArrayType\u0024\u0024\u0024BY04D) \u003CModule\u003E.\u003F\u003F_C\u0040_04CBEPCLMK\u0040UNIV\u003F\u0024AA\u0040;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    stream.WriteUInt32((uint) ((((int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 3) << 8 | (int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 2)) << 8 | (int) ^(sbyte&) ((IntPtr) &arrayTypeBy04D + 1)) << 8) | (uint) ^(sbyte&) ref arrayTypeBy04D);
    int written1 = stream.GetWritten();
    stream.WriteUInt32(uint.MaxValue);
    BufferStream bufferStream1 = stream;
    ID3DXMesh* id3DxMeshPtr1 = pMesh;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int uiValue = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr1 + 40L))((IntPtr) id3DxMeshPtr1);
    bufferStream1.WriteUInt32((uint) uiValue);
    SVertex* svertexPtr1 = (SVertex*) 0L;
    ID3DXMesh* id3DxMeshPtr2 = pMesh;
    ref SVertex* local = ref svertexPtr1;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num1 = __calli((__FnPtr<int (IntPtr, uint, void**)>) *(long*) (*(long*) pMesh + 120L))((IntPtr) id3DxMeshPtr2, 0U, (void**) ref local);
    ID3DXMesh* id3DxMeshPtr3 = pMesh;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    uint* numPtr1 = (uint*) \u003CModule\u003E.new\u005B\u005D((ulong) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr3 + 40L))((IntPtr) id3DxMeshPtr3) * 4UL);
    IntPtr num2 = (IntPtr) numPtr1;
    ID3DXMesh* id3DxMeshPtr4 = pMesh;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    long num3 = (long) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr4 + 40L))((IntPtr) id3DxMeshPtr4) * 4L;
    // ISSUE: initblk instruction
    __memset(num2, (int) byte.MaxValue, num3);
    uint num4 = 0;
    ID3DXMesh* id3DxMeshPtr5 = pMesh;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    if (0U < __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr5 + 40L))((IntPtr) id3DxMeshPtr5))
    {
      uint* numPtr2 = numPtr1;
      long num5 = 8;
      int num6;
      int num7;
      do
      {
        if (*numPtr2 == uint.MaxValue)
        {
          uint num8 = 0;
          ID3DXMesh* id3DxMeshPtr6 = pMesh;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          if (0U < __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr6 + 40L))((IntPtr) id3DxMeshPtr6))
          {
            long num9 = 0;
            uint* numPtr3 = numPtr1;
            int num10;
            int num11;
            do
            {
              if ((int) num4 != (int) num8 && *numPtr3 == uint.MaxValue)
              {
                SVertex* svertexPtr2 = (SVertex*) (num9 + (IntPtr) svertexPtr1);
                if (Math.Abs((double) (*(float*) ((IntPtr) svertexPtr1 + num5 - 8L) - *(float*) svertexPtr2)) < 0.01 && Math.Abs((double) (*(float*) ((IntPtr) svertexPtr1 + num5 - 4L) - *(float*) ((IntPtr) svertexPtr2 + 4L))) < 0.01 && Math.Abs((double) (*(float*) (num5 + (IntPtr) svertexPtr1) - *(float*) ((IntPtr) svertexPtr2 + 8L))) < 0.01)
                  *numPtr3 = num4;
              }
              ++num8;
              num9 += 32L /*0x20*/;
              numPtr3 += 4L;
              num10 = (int) num8;
              ID3DXMesh* id3DxMeshPtr7 = pMesh;
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              num11 = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr7 + 40L))((IntPtr) id3DxMeshPtr7);
            }
            while ((uint) num10 < (uint) num11);
          }
          *numPtr2 = num4;
        }
        ++num4;
        num5 += 32L /*0x20*/;
        numPtr2 += 4L;
        num6 = (int) num4;
        ID3DXMesh* id3DxMeshPtr8 = pMesh;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        num7 = (int) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr8 + 40L))((IntPtr) id3DxMeshPtr8);
      }
      while ((uint) num6 < (uint) num7);
    }
    ID3DXMesh* id3DxMeshPtr9 = pMesh;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int num12 = __calli((__FnPtr<int (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr9 + 128L /*0x80*/))((IntPtr) id3DxMeshPtr9);
    BufferStream bufferStream2 = stream;
    uint* pData = numPtr1;
    ID3DXMesh* id3DxMeshPtr10 = pMesh;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    int iCount = (int) ((long) __calli((__FnPtr<uint (IntPtr)>) *(long*) (*(long*) id3DxMeshPtr10 + 40L))((IntPtr) id3DxMeshPtr10) * 4L);
    bufferStream2.Write((void*) pData, iCount);
    \u003CModule\u003E.delete\u005B\u005D((void*) numPtr1);
    int written2 = stream.GetWritten();
    stream.Seek(written1);
    stream.WriteUInt32((uint) (written2 - written1 - 4));
    stream.Seek(written2);
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
