// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.GfxThingInterface
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using FableMod.BIN;
using FableMod.ContentManagement;
using FableMod.TNG;
using System;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.Gfx.Integration;

public class GfxThingInterface(ThingMap map, Thing thing) : ThingInterface(thing)
{
  protected static string HDMY_BUILDING = "HDMY_CREATEBUILDING";
  protected static string HDMY_OBJECT = "HDMY_CREATEOBJECT";
  protected ThingMap m_Map = map;
  protected GfxModel m_Model;
  protected GfxThingController m_Controller;
  protected unsafe SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* m_pNode = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
  protected unsafe SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* m_pOwnershipNode = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
  protected unsafe SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* m_pObject = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
  protected unsafe SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* m_pSphereMesh = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E*) 0L;

  private void \u007EGfxThingInterface() => this.\u0021GfxThingInterface();

  private void \u0021GfxThingInterface() => this.Destroy();

  [return: MarshalAs(UnmanagedType.U1)]
  public unsafe bool Create(GfxThingController controller)
  {
    this.m_Controller = controller;
    string definitionType = this.Thing.DefinitionType;
    if (!string.IsNullOrEmpty(this.Thing.GraphicOverride))
    {
      this.m_Model = GfxManager.GetModelManager().Get(this.Thing.GraphicOverride);
    }
    else
    {
      if (this.Thing.Name == "Building" && this.CreateBuilding(controller))
        return true;
      GfxThingInterface gfxThingInterface = this;
      gfxThingInterface.m_Model = gfxThingInterface.GetGraphic(this.Thing.BinDefinitionType);
    }
    if (this.m_Model != null)
    {
      if (this.Thing.Name == "Building")
      {
        SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr1 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
        SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr2;
        // ISSUE: fault handler
        try
        {
          if ((IntPtr) pointerFableModGfxNodePtr1 != IntPtr.Zero)
          {
            ImpostorNode* impostorNodePtr = (ImpostorNode*) \u003CModule\u003E.@new(344UL);
            ImpostorNode* pObject;
            // ISSUE: fault handler
            try
            {
              pObject = (IntPtr) impostorNodePtr == IntPtr.Zero ? (ImpostorNode*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EImpostorNode\u002E\u007Bctor\u007D(impostorNodePtr);
            }
            __fault
            {
              \u003CModule\u003E.delete((void*) impostorNodePtr);
            }
            pointerFableModGfxNodePtr2 = \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E\u002E\u007Bctor\u007D(pointerFableModGfxNodePtr1, (Node*) pObject);
          }
          else
            pointerFableModGfxNodePtr2 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) pointerFableModGfxNodePtr1);
        }
        this.m_pObject = pointerFableModGfxNodePtr2;
        long num = *(long*) ((IntPtr) pointerFableModGfxNodePtr2 + 8L);
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) num + 120L))((IntPtr) num, (Spatial*) this.m_Model.GetGfx());
      }
      else
      {
        SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr3 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
        SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr4;
        // ISSUE: fault handler
        try
        {
          pointerFableModGfxNodePtr4 = (IntPtr) pointerFableModGfxNodePtr3 == IntPtr.Zero ? (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E\u002E\u007Bctor\u007D(pointerFableModGfxNodePtr3, this.m_Model.GetGfx());
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) pointerFableModGfxNodePtr3);
        }
        this.m_pObject = pointerFableModGfxNodePtr4;
      }
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr5 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr6;
      // ISSUE: fault handler
      try
      {
        if ((IntPtr) pointerFableModGfxNodePtr5 != IntPtr.Zero)
        {
          Node* nodePtr = (Node*) \u003CModule\u003E.@new(304UL);
          Node* pObject;
          // ISSUE: fault handler
          try
          {
            pObject = (IntPtr) nodePtr == IntPtr.Zero ? (Node*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002ENode\u002E\u007Bctor\u007D(nodePtr);
          }
          __fault
          {
            \u003CModule\u003E.delete((void*) nodePtr);
          }
          pointerFableModGfxNodePtr6 = \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E\u002E\u007Bctor\u007D(pointerFableModGfxNodePtr5, pObject);
        }
        else
          pointerFableModGfxNodePtr6 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) pointerFableModGfxNodePtr5);
      }
      this.m_pNode = pointerFableModGfxNodePtr6;
      Node* pNode = (Node*) *(long*) ((IntPtr) this.m_pObject + 8L);
      GfxThingInterface gfxThingInterface = this;
      gfxThingInterface.AddHelpers(gfxThingInterface.m_Model, pNode);
      if (definitionType == "MARKER_INFO_DISPLAY" || definitionType == "MARKER_LIGHT" || definitionType == "MARKER_CREATURE_GENERATOR" || definitionType == "REGION_EXIT_POINT")
      {
        SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pointerFableModGfxMeshPtr1 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
        SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pointerFableModGfxMeshPtr2;
        // ISSUE: fault handler
        try
        {
          if ((IntPtr) pointerFableModGfxMeshPtr1 != IntPtr.Zero)
          {
            Mesh* meshPtr = (Mesh*) \u003CModule\u003E.@new(336UL);
            Mesh* pObject;
            // ISSUE: fault handler
            try
            {
              pObject = (IntPtr) meshPtr == IntPtr.Zero ? (Mesh*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EMesh\u002E\u007Bctor\u007D(meshPtr);
            }
            __fault
            {
              \u003CModule\u003E.delete((void*) meshPtr);
            }
            pointerFableModGfxMeshPtr2 = \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E\u002E\u007Bctor\u007D(pointerFableModGfxMeshPtr1, pObject);
          }
          else
            pointerFableModGfxMeshPtr2 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E*) 0L;
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) pointerFableModGfxMeshPtr1);
        }
        this.m_pSphereMesh = pointerFableModGfxMeshPtr2;
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
        D3DXCOLOR d3Dxcolor;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ref d3Dxcolor = 0.7843138f;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3Dxcolor + 4) = 0.7843138f;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3Dxcolor + 8) = 0.5882353f;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3Dxcolor + 12) = 0.1254902f;
        \u003CModule\u003E.FableMod\u002EGfx\u002ED3DXModel\u002EBuildSphere(d3DxModelPtr2, 1f, d3Dxcolor);
        \u003CModule\u003E.FableMod\u002EGfx\u002EMesh\u002ESetModel(\u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E\u002E\u002D\u003E(this.m_pSphereMesh), (FableMod.Gfx.Model*) d3DxModelPtr2);
        Node* nodePtr1 = \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E\u002E\u002D\u003E(this.m_pNode);
        Node* nodePtr2 = nodePtr1;
        Mesh* meshPtr1 = \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E\u002E\u002EPEAVMesh\u0040Gfx\u0040FableMod\u0040\u0040(this.m_pSphereMesh);
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) nodePtr1 + 120L))((IntPtr) nodePtr2, (Spatial*) meshPtr1);
        uint* numPtr = \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EFlags((Spatial*) \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E\u002E\u002D\u003E(this.m_pSphereMesh));
        int num = (int) *numPtr | 1312;
        *numPtr = (uint) num;
      }
      if (this.Thing.Name == "Marker")
      {
        uint* numPtr = \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EFlags((Spatial*) \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E\u002E\u002D\u003E(this.m_pNode));
        int num = (int) *numPtr | 1024 /*0x0400*/;
        *numPtr = (uint) num;
      }
      IntPtr hglobalUni = Marshal.StringToHGlobalUni(this.Thing.UID);
      \u003CModule\u003E.FableMod\u002EGfx\u002ERootObject\u002ESetName((RootObject*) \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E\u002E\u002D\u003E(this.m_pObject), (char*) hglobalUni.ToPointer());
      \u003CModule\u003E.FableMod\u002EGfx\u002ERootObject\u002ESetName((RootObject*) \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E\u002E\u002D\u003E(this.m_pNode), (char*) hglobalUni.ToPointer());
      Marshal.FreeHGlobal(hglobalUni);
      *\u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EID((Spatial*) \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E\u002E\u002D\u003E(this.m_pNode)) = \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EGetNextID();
      Node* nodePtr3 = \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E\u002E\u002D\u003E(this.m_pNode);
      Node* nodePtr4 = nodePtr3;
      Node* nodePtr5 = \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E\u002E\u002EPEAVNode\u0040Gfx\u0040FableMod\u0040\u0040(this.m_pObject);
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) nodePtr3 + 120L))((IntPtr) nodePtr4, (Spatial*) nodePtr5);
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr7 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr8;
      // ISSUE: fault handler
      try
      {
        if ((IntPtr) pointerFableModGfxNodePtr7 != IntPtr.Zero)
        {
          Node* nodePtr6 = (Node*) \u003CModule\u003E.@new(304UL);
          Node* pObject;
          // ISSUE: fault handler
          try
          {
            pObject = (IntPtr) nodePtr6 == IntPtr.Zero ? (Node*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002ENode\u002E\u007Bctor\u007D(nodePtr6);
          }
          __fault
          {
            \u003CModule\u003E.delete((void*) nodePtr6);
          }
          pointerFableModGfxNodePtr8 = \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E\u002E\u007Bctor\u007D(pointerFableModGfxNodePtr7, pObject);
        }
        else
          pointerFableModGfxNodePtr8 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) pointerFableModGfxNodePtr7);
      }
      this.m_pOwnershipNode = pointerFableModGfxNodePtr8;
      Node* node = this.GetNode();
      Node* nodePtr7 = node;
      Node* nodePtr8 = \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E\u002E\u002EPEAVNode\u0040Gfx\u0040FableMod\u0040\u0040(pointerFableModGfxNodePtr8);
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) node + 120L))((IntPtr) nodePtr7, (Spatial*) nodePtr8);
      Node* nodePtr9 = \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E\u002E\u002D\u003E(this.m_pNode);
      Node* nodePtr10 = nodePtr9;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      __calli((__FnPtr<void (IntPtr, byte)>) *(long*) (*(long*) nodePtr9 + 16L /*0x10*/))((IntPtr) nodePtr10, (byte) 1);
      this.Thing.Interface = (ThingInterface) this;
      controller.AddThing(*\u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EID((Spatial*) \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E\u002E\u002D\u003E(this.m_pNode)), this.Thing);
      return true;
    }
    Console.WriteLine("No model for {0}", (object) this.Thing.DefinitionType);
    return false;
  }

  public override unsafe void Destroy()
  {
    if ((IntPtr) this.GetNode() != IntPtr.Zero)
      \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EDetach((Spatial*) this.GetNode());
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
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pOwnershipNode = this.m_pOwnershipNode;
    if ((IntPtr) pOwnershipNode != IntPtr.Zero)
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr3 = pOwnershipNode;
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr4 = pointerFableModGfxNodePtr3;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) pointerFableModGfxNodePtr3)((IntPtr) pointerFableModGfxNodePtr4, 1U);
      this.m_pOwnershipNode = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
    }
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pObject = this.m_pObject;
    if ((IntPtr) pObject != IntPtr.Zero)
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr5 = pObject;
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr6 = pointerFableModGfxNodePtr5;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) pointerFableModGfxNodePtr5)((IntPtr) pointerFableModGfxNodePtr6, 1U);
      this.m_pObject = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
    }
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pSphereMesh = this.m_pSphereMesh;
    if ((IntPtr) pSphereMesh != IntPtr.Zero)
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pointerFableModGfxMeshPtr1 = pSphereMesh;
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pointerFableModGfxMeshPtr2 = pointerFableModGfxMeshPtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) pointerFableModGfxMeshPtr1)((IntPtr) pointerFableModGfxMeshPtr2, 1U);
      this.m_pSphereMesh = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E*) 0L;
    }
    this.m_Model = (GfxModel) null;
    this.m_Thing = (Thing) null;
    this.m_Map = (ThingMap) null;
    this.m_Controller = (GfxThingController) null;
  }

  public override void Update()
  {
    this.UpdateOwnership();
    this.UpdateInterface();
  }

  public override unsafe void UpdateThing()
  {
    this.Thing.LockUpdate();
    CTCBlock ctcBlock = !(this.Thing.Name == "AICreature") ? this.Thing.get_CTCs("CTCPhysicsStandard") : this.Thing.get_CTCs("CTCPhysicsNavigator");
    if (ctcBlock != null)
    {
      Node* nodePtr1 = (Node*) *(long*) ((IntPtr) this.m_pNode + 8L);
      FableMod.Gfx.Integration.Map map1 = (FableMod.Gfx.Integration.Map) this.Map;
      float num1 = (IntPtr) map1.GetTerrain() != IntPtr.Zero ? *(float*) ((IntPtr) map1.GetTerrain() + 180L) : 0.0f;
      ctcBlock.get_Variables("PositionX").Value = (object) (*(float*) ((IntPtr) nodePtr1 + 180L) - num1);
      Node* nodePtr2 = (Node*) *(long*) ((IntPtr) this.m_pNode + 8L);
      FableMod.Gfx.Integration.Map map2 = (FableMod.Gfx.Integration.Map) this.Map;
      float num2 = (IntPtr) map2.GetTerrain() != IntPtr.Zero ? *(float*) ((IntPtr) map2.GetTerrain() + 184L) : 0.0f;
      ctcBlock.get_Variables("PositionY").Value = (object) (*(float*) ((IntPtr) nodePtr2 + 184L) - num2);
      Node* nodePtr3 = (Node*) *(long*) ((IntPtr) this.m_pNode + 8L);
      ctcBlock.get_Variables("PositionZ").Value = (object) *(float*) ((IntPtr) nodePtr3 + 188L);
      D3DXVECTOR3 d3DxvectoR3_1;
      D3DXVECTOR3* worldForward = \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EGetWorldForward((Spatial*) *(long*) ((IntPtr) this.m_pNode + 8L), &d3DxvectoR3_1);
      ctcBlock.get_Variables("RHSetForwardX").Value = (object) *(float*) worldForward;
      Node* nodePtr4 = (Node*) *(long*) ((IntPtr) this.m_pNode + 8L);
      D3DXVECTOR3 d3DxvectoR3_2;
      ctcBlock.get_Variables("RHSetForwardY").Value = (object) *(float*) ((IntPtr) \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EGetWorldForward((Spatial*) nodePtr4, &d3DxvectoR3_2) + 4L);
      Node* nodePtr5 = (Node*) *(long*) ((IntPtr) this.m_pNode + 8L);
      D3DXVECTOR3 d3DxvectoR3_3;
      ctcBlock.get_Variables("RHSetForwardZ").Value = (object) *(float*) ((IntPtr) \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EGetWorldForward((Spatial*) nodePtr5, &d3DxvectoR3_3) + 8L);
      D3DXVECTOR3 d3DxvectoR3_4;
      D3DXVECTOR3* worldUp = \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EGetWorldUp((Spatial*) *(long*) ((IntPtr) this.m_pNode + 8L), &d3DxvectoR3_4);
      ctcBlock.get_Variables("RHSetUpX").Value = (object) *(float*) worldUp;
      Node* nodePtr6 = (Node*) *(long*) ((IntPtr) this.m_pNode + 8L);
      D3DXVECTOR3 d3DxvectoR3_5;
      ctcBlock.get_Variables("RHSetUpY").Value = (object) *(float*) ((IntPtr) \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EGetWorldUp((Spatial*) nodePtr6, &d3DxvectoR3_5) + 4L);
      Node* nodePtr7 = (Node*) *(long*) ((IntPtr) this.m_pNode + 8L);
      D3DXVECTOR3 d3DxvectoR3_6;
      ctcBlock.get_Variables("RHSetUpZ").Value = (object) *(float*) ((IntPtr) \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EGetWorldUp((Spatial*) nodePtr7, &d3DxvectoR3_6) + 8L);
    }
    this.Thing.UnlockUpdate();
  }

  public ThingMap Map => this.m_Map;

  public GfxModel Model => this.m_Model;

  public unsafe void GroundMode([MarshalAs(UnmanagedType.U1)] bool value)
  {
    if (value)
    {
      long num = *(long*) ((IntPtr) this.m_pObject + 8L) + 32L /*0x20*/;
      *(int*) num = *(int*) num | 512 /*0x0200*/;
      \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002E\u003FA0xa15d0f8f\u002EApplyTreeFlags((Spatial*) this.GetNode(), true, 256U /*0x0100*/, true, 128U /*0x80*/);
      Node* node = this.GetNode();
      Node* nodePtr = node;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      __calli((__FnPtr<void (IntPtr, float)>) *(long*) (*(long*) node + 32L /*0x20*/))((IntPtr) nodePtr, 0.4f);
    }
    else
    {
      long num1 = *(long*) ((IntPtr) this.m_pObject + 8L) + 32L /*0x20*/;
      *(int*) num1 = *(int*) num1 & -513;
      Node* node = this.GetNode();
      Node* nodePtr = node;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      __calli((__FnPtr<void (IntPtr, float)>) *(long*) (*(long*) node + 32L /*0x20*/))((IntPtr) nodePtr, 1f);
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pSphereMesh = this.m_pSphereMesh;
      if ((IntPtr) pSphereMesh != IntPtr.Zero)
      {
        long num2 = *(long*) ((IntPtr) pSphereMesh + 8L) + 32L /*0x20*/;
        *(int*) num2 = *(int*) num2 | 1280 /*0x0500*/;
      }
      \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002E\u003FA0xa15d0f8f\u002EApplyTreeFlags((Spatial*) this.GetNode(), false, 256U /*0x0100*/, false, 128U /*0x80*/);
      if (this.Freezed)
      {
        \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002E\u003FA0xa15d0f8f\u002EApplyTreeFlags((Spatial*) this.GetNode(), true, 256U /*0x0100*/, true, 16U /*0x10*/);
      }
      else
      {
        if (!this.LockedInPlace)
          return;
        \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002E\u003FA0xa15d0f8f\u002EApplyTreeFlags((Spatial*) this.GetNode(), true, 0U, true, 128U /*0x80*/);
      }
    }
  }

  public unsafe void Show([MarshalAs(UnmanagedType.U1)] bool value)
  {
    if (!value)
    {
      IntPtr num = (IntPtr) this.GetNode() + 32L /*0x20*/;
      *(int*) num = *(int*) num | 32 /*0x20*/;
    }
    else
    {
      IntPtr num = (IntPtr) this.GetNode() + 32L /*0x20*/;
      *(int*) num = *(int*) num & -33;
    }
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pSphereMesh = this.m_pSphereMesh;
    if ((IntPtr) pSphereMesh == IntPtr.Zero)
      return;
    if (!value)
    {
      long num = *(long*) ((IntPtr) pSphereMesh + 8L) + 32L /*0x20*/;
      *(int*) num = *(int*) num & -33;
    }
    else
    {
      long num = *(long*) ((IntPtr) pSphereMesh + 8L) + 32L /*0x20*/;
      *(int*) num = *(int*) num | 32 /*0x20*/;
    }
  }

  public unsafe bool Highlight
  {
    [param: MarshalAs(UnmanagedType.U1)] set
    {
      if (value)
      {
        \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002E\u003FA0xa15d0f8f\u002EApplyTreeFlags((Spatial*) *(long*) ((IntPtr) this.m_pNode + 8L), true, 0U, true, 1U);
        long num = *(long*) ((IntPtr) this.m_pObject + 8L) + 32L /*0x20*/;
        *(int*) num = *(int*) num | 512 /*0x0200*/;
      }
      else
      {
        \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002E\u003FA0xa15d0f8f\u002EApplyTreeFlags((Spatial*) *(long*) ((IntPtr) this.m_pNode + 8L), true, 0U, false, 1U);
        long num = *(long*) ((IntPtr) this.m_pObject + 8L) + 32L /*0x20*/;
        *(int*) num = *(int*) num & -513;
      }
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pSphereMesh = this.m_pSphereMesh;
      if ((IntPtr) pSphereMesh == IntPtr.Zero)
        return;
      if (value)
      {
        long num = *(long*) ((IntPtr) pSphereMesh + 8L) + 32L /*0x20*/;
        *(int*) num = *(int*) num & -33;
      }
      else
      {
        long num = *(long*) ((IntPtr) pSphereMesh + 8L) + 32L /*0x20*/;
        *(int*) num = *(int*) num | 32 /*0x20*/;
      }
      long num1 = *(long*) ((IntPtr) this.m_pSphereMesh + 8L) + 320L;
      *(int*) num1 = *(int*) num1 & -2;
    }
  }

  public bool LockedInPlace
  {
    [return: MarshalAs(UnmanagedType.U1)] get
    {
      CTCBlock ctcBlock = this.Thing.get_CTCs("CTCEditor");
      if (ctcBlock != null)
      {
        try
        {
          return (bool) ctcBlock.get_Variables(nameof (LockedInPlace)).Value;
        }
        catch (System.Exception ex)
        {
        }
      }
      return false;
    }
  }

  public bool Freezed
  {
    [return: MarshalAs(UnmanagedType.U1)] get
    {
      CTCBlock ctcBlock = this.Thing.get_CTCs("CTCEditor");
      if (ctcBlock != null)
      {
        try
        {
          return (bool) ctcBlock.get_Variables(nameof (Freezed)).Value;
        }
        catch (System.Exception ex)
        {
        }
      }
      return false;
    }
    [param: MarshalAs(UnmanagedType.U1)] set
    {
      CTCBlock ctcBlock = this.Thing.get_CTCs("CTCEditor");
      if (ctcBlock == null)
        return;
      try
      {
        ctcBlock.get_Variables(nameof (Freezed)).Value = (object) value;
      }
      catch (System.Exception ex)
      {
      }
    }
  }

  public unsafe Node* GetNode()
  {
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pNode = this.m_pNode;
    return (IntPtr) pNode != IntPtr.Zero ? (Node*) *(long*) ((IntPtr) pNode + 8L) : (Node*) 0L;
  }

  public unsafe Node* GetOwnershipNode()
  {
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pOwnershipNode = this.m_pOwnershipNode;
    return (IntPtr) pOwnershipNode != IntPtr.Zero ? (Node*) *(long*) ((IntPtr) pOwnershipNode + 8L) : (Node*) 0L;
  }

  internal unsafe void UpdateOwnership()
  {
    CTCBlock ctcBlock = this.Thing.get_CTCs("CTCOwnedEntity");
    if (ctcBlock == null)
      return;
    string stringValue = ctcBlock.get_Variables("OwnerUID").StringValue;
    Thing thing = (Thing) null;
    if (stringValue != "0")
      thing = this.Map.TNG.FindThing(stringValue);
    if (thing != null)
    {
      GfxThingInterface gfxThingInterface = (GfxThingInterface) thing.Interface;
      if (gfxThingInterface != null && (IntPtr) gfxThingInterface.GetOwnershipNode() != IntPtr.Zero)
      {
        Spatial* node1 = (Spatial*) this.GetNode();
        SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ASpatial\u003E fableModGfxSpatial;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(long&) ref fableModGfxSpatial = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VSpatial\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(long&) ((IntPtr) &fableModGfxSpatial + 8) = (long) node1;
        if ((IntPtr) node1 != IntPtr.Zero)
          *(int*) ((IntPtr) node1 + 8L) = *(int*) ((IntPtr) node1 + 8L) + 1;
        // ISSUE: fault handler
        try
        {
          \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EDetach((Spatial*) this.GetNode());
          Node* ownershipNode = gfxThingInterface.GetOwnershipNode();
          Node* nodePtr = ownershipNode;
          Node* node2 = this.GetNode();
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) ownershipNode + 120L))((IntPtr) nodePtr, (Spatial*) node2);
        }
        __fault
        {
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ASpatial\u003E\u002E\u007Bdtor\u007D), (void*) &fableModGfxSpatial);
        }
        \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ASpatial\u003E\u002E\u007Bdtor\u007D(&fableModGfxSpatial);
      }
      else
        Console.WriteLine("Strange owner for {0} {1}", (object) this.Thing.DefinitionType, (object) this.Thing.UID);
    }
    else
    {
      if (*(long*) ((IntPtr) this.GetNode() + 24L) == (IntPtr) this.m_Controller.GetRoot())
        return;
      Console.WriteLine("Remove {0}:{1} as owned", (object) this.Thing.DefinitionType, (object) this.Thing.UID);
      Spatial* node3 = (Spatial*) this.GetNode();
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ASpatial\u003E fableModGfxSpatial;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(long&) ref fableModGfxSpatial = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VSpatial\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(long&) ((IntPtr) &fableModGfxSpatial + 8) = (long) node3;
      if ((IntPtr) node3 != IntPtr.Zero)
        *(int*) ((IntPtr) node3 + 8L) = *(int*) ((IntPtr) node3 + 8L) + 1;
      // ISSUE: fault handler
      try
      {
        \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EDetach((Spatial*) this.GetNode());
        Node* root = this.m_Controller.GetRoot();
        Node* nodePtr = root;
        Node* node4 = this.GetNode();
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) root + 120L))((IntPtr) nodePtr, (Spatial*) node4);
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ASpatial\u003E\u002E\u007Bdtor\u007D), (void*) &fableModGfxSpatial);
      }
      \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ASpatial\u003E\u002E\u007Bdtor\u007D(&fableModGfxSpatial);
    }
  }

  internal unsafe void UpdateInterface()
  {
    float num1 = 0.01f;
    try
    {
      num1 = (float) this.Thing.get_Variables("ObjectScale").Value * 0.01f;
    }
    catch (System.Exception ex)
    {
    }
    Node* nodePtr1 = (Node*) *(long*) ((IntPtr) this.m_pObject + 8L);
    *(int*) ((IntPtr) nodePtr1 + 32L /*0x20*/) = *(int*) ((IntPtr) nodePtr1 + 32L /*0x20*/) | 1;
    *(float*) ((IntPtr) nodePtr1 + 176L /*0xB0*/) = num1;
    FableMod.Gfx.Integration.Map map1 = (FableMod.Gfx.Integration.Map) this.Map;
    float num2 = (IntPtr) map1.GetTerrain() != IntPtr.Zero ? *(float*) ((IntPtr) map1.GetTerrain() + 184L) : 0.0f;
    FableMod.Gfx.Integration.Map map2 = (FableMod.Gfx.Integration.Map) this.Map;
    float num3 = (IntPtr) map2.GetTerrain() != IntPtr.Zero ? *(float*) ((IntPtr) map2.GetTerrain() + 180L) : 0.0f;
    CTCBlock ctcBlock = !(this.Thing.Name == "AICreature") ? this.Thing.get_CTCs("CTCPhysicsStandard") : this.Thing.get_CTCs("CTCPhysicsNavigator");
    if (ctcBlock != null)
    {
      try
      {
        Variable variable1 = ctcBlock.get_Variables("PositionX");
        Variable variable2 = ctcBlock.get_Variables("PositionY");
        float num4 = (float) ctcBlock.get_Variables("PositionZ").Value;
        float num5 = (float) variable2.Value;
        float num6 = (float) variable1.Value;
        D3DXVECTOR3 d3DxvectoR3_1;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ref d3DxvectoR3_1 = num6;
        float num7 = (float) ctcBlock.get_Variables("RHSetForwardZ").Value;
        float num8 = (float) ctcBlock.get_Variables("RHSetForwardY").Value;
        float num9 = (float) ctcBlock.get_Variables("RHSetForwardX").Value;
        D3DXVECTOR3 d3DxvectoR3_2;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ref d3DxvectoR3_2 = num9;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4) = num8;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3DxvectoR3_2 + 8) = num7;
        float num10 = (float) ctcBlock.get_Variables("RHSetUpZ").Value;
        float num11 = (float) ctcBlock.get_Variables("RHSetUpY").Value;
        float num12 = (float) ctcBlock.get_Variables("RHSetUpX").Value;
        D3DXVECTOR3 d3DxvectoR3_3;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ref d3DxvectoR3_3 = num12;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3DxvectoR3_3 + 4) = num11;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3DxvectoR3_3 + 8) = num10;
        float num13 = num4 + 0.0f;
        float num14 = num5 + num2;
        float num15 = num6 + num3;
        D3DXVECTOR3 d3DxvectoR3_4;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ref d3DxvectoR3_4 = num15;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3DxvectoR3_4 + 4) = num14;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3DxvectoR3_4 + 8) = num13;
        \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002ESetWorldTranslation((Spatial*) this.GetNode(), &d3DxvectoR3_4);
        \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002ESetWorldDirection((Spatial*) this.GetNode(), &d3DxvectoR3_2, &d3DxvectoR3_3);
      }
      catch (System.Exception ex)
      {
        Console.WriteLine("GfxThingInterface: {0}", (object) ex.ToString());
      }
    }
    if ((IntPtr) this.m_pSphereMesh != IntPtr.Zero)
    {
      try
      {
        string definitionType = this.Thing.DefinitionType;
        float num16 = 1f;
        switch (definitionType)
        {
          case "MARKER_INFO_DISPLAY":
            num16 = (float) this.Thing.get_CTCs("CTCInfoDisplay").get_Variables("Radius").Value;
            break;
          case "MARKER_CREATURE_GENERATOR":
            num16 = (float) this.Thing.get_CTCs("CTCCreatureGenerator").get_Variables("GenerationRadius").Value;
            break;
          case "REGION_EXIT_POINT":
            num16 = (float) this.Thing.get_CTCs("CTCDRegionExit").get_Variables("Radius").Value;
            break;
          case "MARKER_LIGHT":
            num16 = (float) this.Thing.get_CTCs("CTCLight").get_Variables("OuterRadius").Value;
            break;
        }
        Mesh* meshPtr = (Mesh*) *(long*) ((IntPtr) this.m_pSphereMesh + 8L);
        IntPtr num17 = (IntPtr) meshPtr + 32L /*0x20*/;
        *(int*) num17 = *(int*) num17 | 1;
        *(float*) ((IntPtr) meshPtr + 176L /*0xB0*/) = num16;
      }
      catch (System.Exception ex)
      {
      }
    }
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002E\u003FA0xa15d0f8f\u002EApplyTreeFlags((Spatial*) *(long*) ((IntPtr) this.m_pObject + 8L), false, 256U /*0x0100*/, false, 144U /*0x90*/);
    if (this.Freezed)
      \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002E\u003FA0xa15d0f8f\u002EApplyTreeFlags((Spatial*) *(long*) ((IntPtr) this.m_pObject + 8L), true, 256U /*0x0100*/, true, 16U /*0x10*/);
    else if (this.LockedInPlace)
      \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002E\u003FA0xa15d0f8f\u002EApplyTreeFlags((Spatial*) *(long*) ((IntPtr) this.m_pObject + 8L), true, 0U, true, 128U /*0x80*/);
    Node* node = this.GetNode();
    Node* nodePtr2 = node;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, byte)>) *(long*) (*(long*) node + 16L /*0x10*/))((IntPtr) nodePtr2, (byte) 1);
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected unsafe bool CreateBuilding(GfxThingController controller)
  {
    if (this.Thing.BinDefinitionType == null)
      return false;
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr1 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr2;
    // ISSUE: fault handler
    try
    {
      if ((IntPtr) pointerFableModGfxNodePtr1 != IntPtr.Zero)
      {
        ImpostorNode* impostorNodePtr1 = (ImpostorNode*) \u003CModule\u003E.@new(344UL);
        ImpostorNode* impostorNodePtr2;
        // ISSUE: fault handler
        try
        {
          impostorNodePtr2 = (IntPtr) impostorNodePtr1 == IntPtr.Zero ? (ImpostorNode*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EImpostorNode\u002E\u007Bctor\u007D(impostorNodePtr1);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) impostorNodePtr1);
        }
        *(long*) pointerFableModGfxNodePtr1 = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VNode\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
        *(long*) ((IntPtr) pointerFableModGfxNodePtr1 + 8L) = (long) impostorNodePtr2;
        if ((IntPtr) impostorNodePtr2 != IntPtr.Zero)
          *(int*) ((IntPtr) impostorNodePtr2 + 8L) = *(int*) ((IntPtr) impostorNodePtr2 + 8L) + 1;
        pointerFableModGfxNodePtr2 = pointerFableModGfxNodePtr1;
      }
      else
        pointerFableModGfxNodePtr2 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) pointerFableModGfxNodePtr1);
    }
    this.m_pObject = pointerFableModGfxNodePtr2;
    Node* pNode = (Node*) *(long*) ((IntPtr) pointerFableModGfxNodePtr2 + 8L);
    GfxThingInterface gfxThingInterface = this;
    if (!gfxThingInterface.AddBuilding(gfxThingInterface.Thing.BinDefinitionType, pNode))
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pObject = this.m_pObject;
      if ((IntPtr) pObject != IntPtr.Zero)
      {
        SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr3 = pObject;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) pObject)((IntPtr) pointerFableModGfxNodePtr3, 1U);
      }
      this.m_pObject = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
      return false;
    }
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr4 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr5;
    // ISSUE: fault handler
    try
    {
      if ((IntPtr) pointerFableModGfxNodePtr4 != IntPtr.Zero)
      {
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
        *(long*) pointerFableModGfxNodePtr4 = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VNode\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
        *(long*) ((IntPtr) pointerFableModGfxNodePtr4 + 8L) = (long) nodePtr2;
        if ((IntPtr) nodePtr2 != IntPtr.Zero)
          *(int*) ((IntPtr) nodePtr2 + 8L) = *(int*) ((IntPtr) nodePtr2 + 8L) + 1;
        pointerFableModGfxNodePtr5 = pointerFableModGfxNodePtr4;
      }
      else
        pointerFableModGfxNodePtr5 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) pointerFableModGfxNodePtr4);
    }
    this.m_pNode = pointerFableModGfxNodePtr5;
    IntPtr hglobalUni = Marshal.StringToHGlobalUni(this.Thing.UID);
    \u003CModule\u003E.FableMod\u002EGfx\u002ERootObject\u002ESetName((RootObject*) *(long*) ((IntPtr) this.m_pObject + 8L), (char*) hglobalUni.ToPointer());
    \u003CModule\u003E.FableMod\u002EGfx\u002ERootObject\u002ESetName((RootObject*) this.GetNode(), (char*) hglobalUni.ToPointer());
    Marshal.FreeHGlobal(hglobalUni);
    *(int*) ((IntPtr) this.GetNode() + 272L) = (int) \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EGetNextID();
    Node* node1 = this.GetNode();
    Node* nodePtr3 = (Node*) *(long*) ((IntPtr) this.m_pObject + 8L);
    Node* nodePtr4 = node1;
    Node* nodePtr5 = nodePtr3;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) node1 + 120L))((IntPtr) nodePtr4, (Spatial*) nodePtr5);
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr6 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr7;
    // ISSUE: fault handler
    try
    {
      if ((IntPtr) pointerFableModGfxNodePtr6 != IntPtr.Zero)
      {
        Node* nodePtr6 = (Node*) \u003CModule\u003E.@new(304UL);
        Node* nodePtr7;
        // ISSUE: fault handler
        try
        {
          nodePtr7 = (IntPtr) nodePtr6 == IntPtr.Zero ? (Node*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002ENode\u002E\u007Bctor\u007D(nodePtr6);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) nodePtr6);
        }
        *(long*) pointerFableModGfxNodePtr6 = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VNode\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
        *(long*) ((IntPtr) pointerFableModGfxNodePtr6 + 8L) = (long) nodePtr7;
        if ((IntPtr) nodePtr7 != IntPtr.Zero)
          *(int*) ((IntPtr) nodePtr7 + 8L) = *(int*) ((IntPtr) nodePtr7 + 8L) + 1;
        pointerFableModGfxNodePtr7 = pointerFableModGfxNodePtr6;
      }
      else
        pointerFableModGfxNodePtr7 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) pointerFableModGfxNodePtr6);
    }
    this.m_pOwnershipNode = pointerFableModGfxNodePtr7;
    Node* node2 = this.GetNode();
    Node* nodePtr8 = node2;
    long num = *(long*) ((IntPtr) pointerFableModGfxNodePtr7 + 8L);
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) node2 + 120L))((IntPtr) nodePtr8, (Spatial*) num);
    Node* node3 = this.GetNode();
    Node* nodePtr9 = node3;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, byte)>) *(long*) (*(long*) node3 + 16L /*0x10*/))((IntPtr) nodePtr9, (byte) 1);
    this.Thing.Interface = (ThingInterface) this;
    Spatial* node4 = (Spatial*) this.GetNode();
    controller.AddThing((uint) *(int*) ((IntPtr) node4 + 272L), this.Thing);
    return true;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected unsafe bool AddBuilding(DefinitionType defType, Node* pNode)
  {
    if (defType == null)
      return false;
    ArrayMember cdefs = defType.CDefs;
    if (cdefs.Elements.Count < 6)
      return false;
    ContentObject entry1 = ContentManager.Instance.FindEntry(LinkDestination.GameBINEntryID, (object) (int) ((Member) cdefs.Elements[5][1]).Value);
    GfxModel model1 = (GfxModel) null;
    GfxModel model2 = (GfxModel) null;
    if (entry1 != null)
    {
      BINEntry entry2 = (BINEntry) entry1.Object;
      DefinitionType definition = ContentManager.Instance.Definitions.GetDefinition(entry2.Definition);
      if (definition == null)
        return false;
      definition.ReadIn(entry2);
      Control control = definition.FindControl(30137356U);
      if (control != null)
      {
        ArrayMember member1 = (ArrayMember) control.Members[1];
        Member member2 = (Member) member1.Elements[0][1];
        Member member3 = (Member) member1.Elements[1][1];
        model2 = GfxManager.GetModelManager().Get(uint.Parse(member2.Value.ToString()));
        model1 = GfxManager.GetModelManager().Get(uint.Parse(member3.Value.ToString()));
      }
    }
    if (model1 == null || model2 == null)
      return false;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) pNode + 120L))((IntPtr) pNode, (Spatial*) model1.GetGfx());
    Spatial* gfx = (Spatial*) model2.GetGfx();
    *(int*) ((IntPtr) gfx + 32L /*0x20*/) = *(int*) ((IntPtr) gfx + 32L /*0x20*/) | 2048 /*0x0800*/;
    Node* nodePtr = pNode;
    Spatial* spatialPtr = gfx;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) pNode + 120L))((IntPtr) nodePtr, spatialPtr);
    this.AddHelpers(model1, pNode);
    this.AddHelpers(model2, pNode);
    return true;
  }

  protected unsafe void AddHelpers(GfxModel model, Node* pNode)
  {
    CompiledModel* compiledModel = model.get_LODs(0).m_CompiledModel;
    Debug.Assert((IntPtr) compiledModel != 0L);
    ContentManager instance = ContentManager.Instance;
    sbyte* numPtr1 = (sbyte*) \u003CModule\u003E.new\u005B\u005D(1024UL /*0x0400*/);
    D3DXMATRIX d3Dxmatrix1;
    \u003CModule\u003E.D3DXMatrixRotationZ(&d3Dxmatrix1, 3.14159274f);
    D3DXQUATERNION d3Dxquaternion1;
    \u003CModule\u003E.D3DXQuaternionRotationMatrix(&d3Dxquaternion1, &d3Dxmatrix1);
    int num1 = 0;
    if ((ushort) 0 < *(ushort*) ((IntPtr) compiledModel + 51L))
    {
      long num2 = 0;
      do
      {
        int hdmyString = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002ECompiledModel\u002EFindHDMYString(compiledModel, (uint) *(int*) (num2 + *(long*) ((IntPtr) compiledModel + 67L)));
        // ISSUE: cpblk instruction
        __memcpy((IntPtr) numPtr1, ref \u003CModule\u003E.\u003F\u003F_C\u0040_05DHHFPMJA\u0040HDMY_\u003F\u0024AA\u0040, 6);
        if (hdmyString > -1)
        {
          long index = *(long*) ((long) hdmyString * 8L + *(long*) ((IntPtr) compiledModel + 87L));
          sbyte* numPtr2 = numPtr1;
          if (*numPtr1 != (sbyte) 0)
          {
            do
            {
              ++numPtr2;
            }
            while (*numPtr2 != (sbyte) 0);
          }
          sbyte num3 = *(sbyte*) index;
          *numPtr2 = num3;
          if (num3 != (sbyte) 0)
          {
            sbyte* numPtr3 = numPtr2 - index;
            sbyte num4;
            do
            {
              ++index;
              num4 = *(sbyte*) index;
              numPtr3[index] = num4;
            }
            while (num4 != (sbyte) 0);
          }
        }
        else
        {
          sbyte* numPtr4 = (sbyte*) &\u003CModule\u003E.\u003F\u003F_C\u0040_0P\u0040POPMPNFF\u0040NAME_NOT_FOUND\u003F\u0024AA\u0040;
          sbyte* numPtr5 = numPtr1;
          if (*numPtr1 != (sbyte) 0)
          {
            do
            {
              ++numPtr5;
            }
            while (*numPtr5 != (sbyte) 0);
          }
          *numPtr5 = (sbyte) 78;
          sbyte num5;
          do
          {
            ++numPtr5;
            ++numPtr4;
            num5 = *numPtr4;
            *numPtr5 = num5;
          }
          while (num5 != (sbyte) 0);
        }
        string str = new string(numPtr1);
        D3DXQUATERNION d3Dxquaternion2;
        if (str.StartsWith(GfxThingInterface.HDMY_BUILDING))
        {
          string objectName = str.Substring(GfxThingInterface.HDMY_BUILDING.Length + 1);
          Console.WriteLine("Building Helper: \"{0}\"", (object) objectName);
          Node* nodePtr1 = (Node*) \u003CModule\u003E.@new(304UL);
          Node* pNode1;
          // ISSUE: fault handler
          try
          {
            pNode1 = (IntPtr) nodePtr1 == IntPtr.Zero ? (Node*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002ENode\u002E\u007Bctor\u007D(nodePtr1);
          }
          __fault
          {
            \u003CModule\u003E.delete((void*) nodePtr1);
          }
          D3DXMATRIX d3Dxmatrix2;
          \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EHDMY\u002EGetMatrix((HDMY*) ((long) num1 * 56L + *(long*) ((IntPtr) compiledModel + 67L)), &d3Dxmatrix2);
          D3DXVECTOR3 d3DxvectoR3;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ref d3DxvectoR3 = ^(float&) ((IntPtr) &d3Dxmatrix2 + 48 /*0x30*/);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3 + 4) = ^(float&) ((IntPtr) &d3Dxmatrix2 + 52);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3 + 8) = ^(float&) ((IntPtr) &d3Dxmatrix2 + 56);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3Dxmatrix2 + 48 /*0x30*/) = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3Dxmatrix2 + 52) = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3Dxmatrix2 + 56) = 0.0f;
          \u003CModule\u003E.D3DXVec3TransformCoord(&d3DxvectoR3, &d3DxvectoR3, &d3Dxmatrix1);
          *(int*) ((IntPtr) pNode1 + 32L /*0x20*/) = *(int*) ((IntPtr) pNode1 + 32L /*0x20*/) | 1;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) pNode1 + 100L, ref d3DxvectoR3, 12);
          \u003CModule\u003E.D3DXQuaternionRotationMatrix(&d3Dxquaternion2, &d3Dxmatrix2);
          D3DXQUATERNION d3Dxquaternion3;
          \u003CModule\u003E.D3DXQuaternionMultiply(&d3Dxquaternion3, &d3Dxquaternion1, &d3Dxquaternion2);
          D3DXQUATERNION d3Dxquaternion4 = d3Dxquaternion3;
          \u003CModule\u003E.D3DXMatrixRotationQuaternion(&d3Dxmatrix2, &d3Dxquaternion4);
          \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002ESetRotation((Spatial*) pNode1, &d3Dxmatrix2);
          DefinitionType objectDefinitionType = instance.FindObjectDefinitionType(objectName);
          if (this.AddBuilding(objectDefinitionType, pNode1))
          {
            Node* nodePtr2 = pNode;
            Node* nodePtr3 = pNode1;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) pNode + 120L))((IntPtr) nodePtr2, (Spatial*) nodePtr3);
          }
          else if (this.AddObject(objectDefinitionType, pNode1))
          {
            Node* nodePtr4 = pNode;
            Node* nodePtr5 = pNode1;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) pNode + 120L))((IntPtr) nodePtr4, (Spatial*) nodePtr5);
          }
          else
          {
            Node* nodePtr6 = pNode1;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) pNode1)((IntPtr) nodePtr6, 1U);
            Console.WriteLine(" Helper building ignored.");
          }
        }
        else if (str.StartsWith(GfxThingInterface.HDMY_OBJECT))
        {
          string objectName = str.Substring(GfxThingInterface.HDMY_OBJECT.Length + 1);
          Console.WriteLine("Object Helper: \"{0}\"", (object) objectName);
          Node* nodePtr7 = (Node*) \u003CModule\u003E.@new(304UL);
          Node* pNode2;
          // ISSUE: fault handler
          try
          {
            pNode2 = (IntPtr) nodePtr7 == IntPtr.Zero ? (Node*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002ENode\u002E\u007Bctor\u007D(nodePtr7);
          }
          __fault
          {
            \u003CModule\u003E.delete((void*) nodePtr7);
          }
          D3DXMATRIX d3Dxmatrix3;
          \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EHDMY\u002EGetMatrix((HDMY*) ((long) num1 * 56L + *(long*) ((IntPtr) compiledModel + 67L)), &d3Dxmatrix3);
          D3DXVECTOR3 d3DxvectoR3;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ref d3DxvectoR3 = ^(float&) ((IntPtr) &d3Dxmatrix3 + 48 /*0x30*/);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3 + 4) = ^(float&) ((IntPtr) &d3Dxmatrix3 + 52);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3 + 8) = ^(float&) ((IntPtr) &d3Dxmatrix3 + 56);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          Console.WriteLine("pos: {0},{1},{2}", (object) ^(float&) ((IntPtr) &d3Dxmatrix3 + 48 /*0x30*/), (object) ^(float&) ((IntPtr) &d3Dxmatrix3 + 52), (object) ^(float&) ((IntPtr) &d3Dxmatrix3 + 56));
          \u003CModule\u003E.D3DXVec3TransformCoord(&d3DxvectoR3, &d3DxvectoR3, &d3Dxmatrix1);
          *(int*) ((IntPtr) pNode2 + 32L /*0x20*/) = *(int*) ((IntPtr) pNode2 + 32L /*0x20*/) | 1;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) pNode2 + 100L, ref d3DxvectoR3, 12);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3Dxmatrix3 + 48 /*0x30*/) = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3Dxmatrix3 + 52) = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3Dxmatrix3 + 56) = 0.0f;
          \u003CModule\u003E.D3DXQuaternionRotationMatrix(&d3Dxquaternion2, &d3Dxmatrix3);
          D3DXQUATERNION d3Dxquaternion5;
          \u003CModule\u003E.D3DXQuaternionMultiply(&d3Dxquaternion5, &d3Dxquaternion1, &d3Dxquaternion2);
          D3DXQUATERNION d3Dxquaternion6 = d3Dxquaternion5;
          \u003CModule\u003E.D3DXMatrixRotationQuaternion(&d3Dxmatrix3, &d3Dxquaternion6);
          \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002ESetRotation((Spatial*) pNode2, &d3Dxmatrix3);
          if (this.AddObject(instance.FindObjectDefinitionType(objectName), pNode2))
          {
            Node* nodePtr8 = pNode;
            Node* nodePtr9 = pNode2;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) pNode + 120L))((IntPtr) nodePtr8, (Spatial*) nodePtr9);
          }
          else
          {
            Node* nodePtr10 = pNode2;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) pNode2)((IntPtr) nodePtr10, 1U);
            Console.WriteLine(" Helper object ignored.");
          }
        }
        ++num1;
        num2 += 56L;
      }
      while (num1 < (int) *(ushort*) ((IntPtr) compiledModel + 51L));
    }
    \u003CModule\u003E.delete\u005B\u005D((void*) numPtr1);
  }

  protected GfxModel GetGraphic(DefinitionType defType)
  {
    if (defType == null)
      return (GfxModel) null;
    Control control = defType.FindControl(3361958702U);
    if (control == null || control.Members.Count != 5)
      return (GfxModel) null;
    Member member = (Member) control.Members[1];
    return GfxManager.GetModelManager().Get(uint.Parse(member.Value.ToString()));
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected unsafe bool AddObject(DefinitionType defType, Node* pNode)
  {
    GfxModel graphic = this.GetGraphic(defType);
    if (graphic == null)
      return false;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) pNode + 120L))((IntPtr) pNode, (Spatial*) graphic.GetGfx());
    return true;
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EGfxThingInterface();
      }
      finally
      {
        base.Dispose(true);
      }
    }
    else
    {
      try
      {
        this.\u0021GfxThingInterface();
      }
      finally
      {
        base.Dispose(false);
      }
    }
  }
}
