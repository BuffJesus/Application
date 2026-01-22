// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.GfxThingView
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using \u003CCppImplementationDetails\u003E;
using FableMod.TNG;
using System;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Gfx.Integration;

public class GfxThingView : GfxView
{
  protected Collection<ThingMap> m_Maps;
  protected string m_EditSection;
  protected int m_iEditSubset;
  protected ThingMap m_EditMap;
  protected unsafe SelectionBox* m_pSelection;
  protected unsafe SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* m_pXArrowMesh;
  protected unsafe SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* m_pYArrowMesh;
  protected unsafe SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* m_pZArrowMesh;
  protected unsafe Mesh* m_pArrow;
  protected unsafe D3DXVECTOR3* m_pvMoveDiff;
  protected unsafe D3DXVECTOR3* m_pvMovePoint;
  protected unsafe D3DXVECTOR2* m_pvMousePoint;
  protected bool m_bMoving;
  protected bool m_bRotating;
  protected unsafe SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* m_pAxes;
  protected unsafe Plane* m_pPlane;
  protected EditorMode m_EditorMode;
  protected bool m_bDirectionAxes;
  protected unsafe EditableTexture* m_pWorkTexture;
  protected int m_iX;
  protected int m_iY;
  protected int m_iBrushSize;
  protected NavigationEditMode m_NavEditMode;
  protected float m_fPlayerHeight;
  protected float m_fPlayerFOV;
  protected float m_fSavedFOV;
  protected uint m_MapColor;
  protected int m_iDrawState;
  private System.ComponentModel.Container components;

  public GfxThingView()
  {
    // ISSUE: fault handler
    try
    {
      this.InitializeComponent();
    }
    __fault
    {
      base.Dispose(true);
    }
  }

  public Collection<ThingMap> Maps
  {
    get => this.m_Maps;
    set => this.m_Maps = value;
  }

  public GfxThingController ThingController => (GfxThingController) this.m_Controller;

  public unsafe EditorMode Mode
  {
    get => this.m_EditorMode;
    set
    {
      if (value == this.m_EditorMode)
        return;
      switch (value)
      {
        case EditorMode.Create:
          this.m_BtnCfg.Enable("Thing", false);
          this.m_BtnCfg.Enable("ThingCreate", true);
          this.Cursor = Cursors.Cross;
          break;
        case EditorMode.Pick:
          this.m_BtnCfg.Enable("ThingCreate", true);
          this.Cursor = Cursors.Cross;
          break;
        default:
          this.Cursor = Cursors.Default;
          if (value == EditorMode.Walk)
          {
            this.ToWalkMode();
            break;
          }
          if (value == EditorMode.Navigation)
          {
            this.ToNavMode();
            break;
          }
          if (value == EditorMode.Player)
          {
            this.ToNormalMode();
            this.m_fSavedFOV = *(float*) ((IntPtr) this.m_pCamera + 380L);
            float fPlayerFov = this.m_fPlayerFOV;
            Camera* pCamera = this.m_pCamera;
            *(float*) ((IntPtr) pCamera + 380L) = fPlayerFov;
            *(int*) ((IntPtr) pCamera + 8L) = *(int*) ((IntPtr) pCamera + 8L) | 18;
            break;
          }
          if (value == EditorMode.Normal)
          {
            this.ToNormalMode();
            break;
          }
          break;
      }
      this.m_EditorMode = value;
    }
  }

  public string EditSection
  {
    get => this.m_EditSection;
    set
    {
      this.SaveGroundChanges();
      this.m_EditSection = value;
      if (this.Mode != EditorMode.Navigation)
        return;
      this.ToNavMode();
    }
  }

  public int EditSubset
  {
    get => this.m_iEditSubset;
    set
    {
      this.SaveGroundChanges();
      this.m_iEditSubset = value;
      if (this.Mode != EditorMode.Navigation)
        return;
      this.ToNavMode();
    }
  }

  public ThingMap EditMap
  {
    get => this.m_EditMap;
    set
    {
      this.SaveGroundChanges();
      this.m_EditMap = value;
      if (this.Mode == EditorMode.Navigation)
      {
        this.ToNavMode();
      }
      else
      {
        if (this.Mode != EditorMode.Walk)
          return;
        this.ToWalkMode();
      }
    }
  }

  public int BrushSize
  {
    get => this.m_iBrushSize;
    set => this.m_iBrushSize = value;
  }

  public NavigationEditMode NavEditMode
  {
    get => this.m_NavEditMode;
    set => this.m_NavEditMode = value;
  }

  public bool DirectionAxes
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_bDirectionAxes;
    [param: MarshalAs(UnmanagedType.U1)] set => this.m_bDirectionAxes = value;
  }

  public bool IsDoing
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_bMoving || this.m_bRotating;
  }

  public event ThingSelectedHandler ThingSelected;

  [SpecialName]
  protected void raise_ThingSelected(Thing[] value0)
  {
    ThingSelectedHandler storeThingSelected = this.\u003Cbacking_store\u003EThingSelected;
    if (storeThingSelected == null)
      return;
    storeThingSelected(value0);
  }

  public event ThingCreateHandler ThingCreated;

  [SpecialName]
  protected void raise_ThingCreated(
    float value0,
    float value1,
    float value2,
    float value3,
    float value4,
    float value5)
  {
    ThingCreateHandler storeThingCreated = this.\u003Cbacking_store\u003EThingCreated;
    if (storeThingCreated == null)
      return;
    storeThingCreated(value0, value1, value2, value3, value4, value5);
  }

  public event ThingPickHandler ThingPicked;

  [SpecialName]
  protected void raise_ThingPicked(Thing value0)
  {
    ThingPickHandler storeThingPicked = this.\u003Cbacking_store\u003EThingPicked;
    if (storeThingPicked == null)
      return;
    storeThingPicked(value0);
  }

  public override unsafe void Destroy()
  {
    base.Destroy();
    EditableTexture* pWorkTexture = this.m_pWorkTexture;
    if ((IntPtr) pWorkTexture != IntPtr.Zero)
    {
      EditableTexture* editableTexturePtr1 = pWorkTexture;
      EditableTexture* editableTexturePtr2 = editableTexturePtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) editableTexturePtr1)((IntPtr) editableTexturePtr2, 1U);
      this.m_pWorkTexture = (EditableTexture*) 0L;
    }
    Plane* pPlane = this.m_pPlane;
    if ((IntPtr) pPlane != IntPtr.Zero)
    {
      \u003CModule\u003E.delete((void*) pPlane);
      this.m_pPlane = (Plane*) 0L;
    }
    SelectionBox* pSelection = this.m_pSelection;
    if ((IntPtr) pSelection != IntPtr.Zero)
    {
      SelectionBox* selectionBoxPtr1 = pSelection;
      SelectionBox* selectionBoxPtr2 = selectionBoxPtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) selectionBoxPtr1)((IntPtr) selectionBoxPtr2, 1U);
      this.m_pSelection = (SelectionBox*) 0L;
    }
    D3DXVECTOR3* pvMoveDiff = this.m_pvMoveDiff;
    if ((IntPtr) pvMoveDiff != IntPtr.Zero)
    {
      \u003CModule\u003E.delete((void*) pvMoveDiff);
      this.m_pvMoveDiff = (D3DXVECTOR3*) 0L;
    }
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pAxes = this.m_pAxes;
    if ((IntPtr) pAxes != IntPtr.Zero)
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr1 = pAxes;
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr2 = pointerFableModGfxNodePtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) pointerFableModGfxNodePtr1)((IntPtr) pointerFableModGfxNodePtr2, 1U);
      this.m_pAxes = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
    }
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pXarrowMesh = this.m_pXArrowMesh;
    if ((IntPtr) pXarrowMesh != IntPtr.Zero)
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pointerFableModGfxMeshPtr1 = pXarrowMesh;
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pointerFableModGfxMeshPtr2 = pointerFableModGfxMeshPtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) pointerFableModGfxMeshPtr1)((IntPtr) pointerFableModGfxMeshPtr2, 1U);
      this.m_pXArrowMesh = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E*) 0L;
    }
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pYarrowMesh = this.m_pYArrowMesh;
    if ((IntPtr) pYarrowMesh != IntPtr.Zero)
    {
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pointerFableModGfxMeshPtr3 = pYarrowMesh;
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pointerFableModGfxMeshPtr4 = pointerFableModGfxMeshPtr3;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) pointerFableModGfxMeshPtr3)((IntPtr) pointerFableModGfxMeshPtr4, 1U);
      this.m_pYArrowMesh = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E*) 0L;
    }
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pZarrowMesh = this.m_pZArrowMesh;
    if ((IntPtr) pZarrowMesh == IntPtr.Zero)
      return;
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pointerFableModGfxMeshPtr5 = pZarrowMesh;
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pointerFableModGfxMeshPtr6 = pointerFableModGfxMeshPtr5;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    void* voidPtr1 = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) pointerFableModGfxMeshPtr5)((IntPtr) pointerFableModGfxMeshPtr6, 1U);
    this.m_pZArrowMesh = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E*) 0L;
  }

  public override unsafe void OnResetObjects()
  {
    Node* root = this.GetRoot();
    Node* nodePtr = root;
    long num = *(long*) ((IntPtr) this.m_pAxes + 8L);
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) root + 120L))((IntPtr) nodePtr, (Spatial*) num);
  }

  public override void DefaultButtonConfiguration()
  {
    base.DefaultButtonConfiguration();
    this.m_BtnCfg.Add("ThingMove", Buttons.LButton);
    this.m_BtnCfg.Add("ThingCopyMove", Buttons.LButton | Buttons.Ctrl);
    this.m_BtnCfg.Add("ThingRotate", Buttons.RButton);
    this.m_BtnCfg.Add("ThingCopyRotate", Buttons.RButton | Buttons.Ctrl);
    this.m_BtnCfg.Add("ThingSelect", Buttons.LButton);
    this.m_BtnCfg.Add("ThingAddSelect", Buttons.LButton | Buttons.Ctrl);
    this.m_BtnCfg.Add("ThingFocus", Buttons.DLButton);
    this.m_BtnCfg.Add("ThingCollisions", Buttons.Shift);
    this.m_BtnCfg.Add("ThingCreate", Buttons.LButton);
    this.m_BtnCfg.Add("WalkDraw", Buttons.LButton);
    this.m_BtnCfg.Add("WalkErase", Buttons.RButton);
    this.m_BtnCfg.Enable("ThingCreate", false);
    this.m_BtnCfg.Enable("Walk.+", false);
  }

  public void SaveChanges()
  {
    this.SaveGroundChanges();
    if (this.Mode == EditorMode.Navigation)
    {
      this.ToNavMode();
    }
    else
    {
      if (this.Mode != EditorMode.Walk)
        return;
      this.ToWalkMode();
    }
  }

  public void ResetNavSections()
  {
    this.m_EditMap.LEV.ResetSections();
    if (this.Mode != EditorMode.Navigation)
      return;
    this.ToNavMode();
  }

  public unsafe void FindByUID(string uid)
  {
    this.ClearSelection();
    Thing thingUid = this.ThingController.FindThingUID(uid);
    if (thingUid == null)
      return;
    GfxThingInterface gfxThingInterface = (GfxThingInterface) thingUid.Interface;
    if (gfxThingInterface == null)
      return;
    \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EAdd(this.m_pSelection, (Spatial*) gfxThingInterface.GetNode());
    this.SetSelectionHighlight(true);
    this.CallThingSelected();
    this.CameraFocus((Spatial*) gfxThingInterface.GetNode(), CameraDirection.Front);
    this.UpdateAll();
  }

  public unsafe void UpdateAll()
  {
    if ((IntPtr) this.GetRoot() != IntPtr.Zero)
    {
      Node* root = this.GetRoot();
      Node* nodePtr = root;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      __calli((__FnPtr<void (IntPtr, byte)>) *(long*) (*(long*) root + 16L /*0x10*/))((IntPtr) nodePtr, (byte) 1);
    }
    \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EUpdate(this.m_pSelection);
    this.SetupAxes();
    this.Render();
  }

  public void Reset()
  {
    this.Mode = EditorMode.Normal;
    this.ClearSelection();
  }

  public unsafe void ClearSelection()
  {
    this.SetSelectionHighlight(false);
    \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EClear(this.m_pSelection);
    IntPtr num = (IntPtr) this.GetAxes() + 32L /*0x20*/;
    *(int*) num = *(int*) num | 32 /*0x20*/;
    this.raise_ThingSelected((Thing[]) null);
  }

  internal override unsafe void Initialize()
  {
    base.Initialize();
    this.m_fPlayerHeight = 2f;
    this.m_fPlayerFOV = 45f;
    SelectionBox* selectionBoxPtr1 = (SelectionBox*) \u003CModule\u003E.@new(120UL);
    SelectionBox* selectionBoxPtr2;
    // ISSUE: fault handler
    try
    {
      selectionBoxPtr2 = (IntPtr) selectionBoxPtr1 == IntPtr.Zero ? (SelectionBox*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002E\u007Bctor\u007D(selectionBoxPtr1);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) selectionBoxPtr1);
    }
    this.m_pSelection = selectionBoxPtr2;
    this.m_pArrow = (Mesh*) 0L;
    this.m_bMoving = false;
    D3DXVECTOR3* d3DxvectoR3Ptr1 = (D3DXVECTOR3*) \u003CModule\u003E.@new(12UL);
    D3DXVECTOR3* d3DxvectoR3Ptr2;
    // ISSUE: fault handler
    try
    {
      if ((IntPtr) d3DxvectoR3Ptr1 != IntPtr.Zero)
      {
        *(float*) d3DxvectoR3Ptr1 = 0.0f;
        *(float*) ((IntPtr) d3DxvectoR3Ptr1 + 4L) = 0.0f;
        *(float*) ((IntPtr) d3DxvectoR3Ptr1 + 8L) = 0.0f;
        d3DxvectoR3Ptr2 = d3DxvectoR3Ptr1;
      }
      else
        d3DxvectoR3Ptr2 = (D3DXVECTOR3*) 0L;
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) d3DxvectoR3Ptr1);
    }
    this.m_pvMoveDiff = d3DxvectoR3Ptr2;
    D3DXVECTOR3* d3DxvectoR3Ptr3 = (D3DXVECTOR3*) \u003CModule\u003E.@new(12UL);
    D3DXVECTOR3* d3DxvectoR3Ptr4;
    // ISSUE: fault handler
    try
    {
      if ((IntPtr) d3DxvectoR3Ptr3 != IntPtr.Zero)
      {
        *(float*) d3DxvectoR3Ptr3 = 0.0f;
        *(float*) ((IntPtr) d3DxvectoR3Ptr3 + 4L) = 0.0f;
        *(float*) ((IntPtr) d3DxvectoR3Ptr3 + 8L) = 0.0f;
        d3DxvectoR3Ptr4 = d3DxvectoR3Ptr3;
      }
      else
        d3DxvectoR3Ptr4 = (D3DXVECTOR3*) 0L;
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) d3DxvectoR3Ptr3);
    }
    this.m_pvMovePoint = d3DxvectoR3Ptr4;
    D3DXVECTOR2* d3DxvectoR2Ptr1 = (D3DXVECTOR2*) \u003CModule\u003E.@new(8UL);
    D3DXVECTOR2* d3DxvectoR2Ptr2;
    // ISSUE: fault handler
    try
    {
      if ((IntPtr) d3DxvectoR2Ptr1 != IntPtr.Zero)
      {
        *(float*) d3DxvectoR2Ptr1 = 0.0f;
        *(float*) ((IntPtr) d3DxvectoR2Ptr1 + 4L) = 0.0f;
        d3DxvectoR2Ptr2 = d3DxvectoR2Ptr1;
      }
      else
        d3DxvectoR2Ptr2 = (D3DXVECTOR2*) 0L;
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) d3DxvectoR2Ptr1);
    }
    this.m_pvMousePoint = d3DxvectoR2Ptr2;
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pointerFableModGfxMeshPtr1 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pointerFableModGfxMeshPtr2;
    // ISSUE: fault handler
    try
    {
      if ((IntPtr) pointerFableModGfxMeshPtr1 != IntPtr.Zero)
      {
        *(long*) pointerFableModGfxMeshPtr1 = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VMesh\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
        *(long*) ((IntPtr) pointerFableModGfxMeshPtr1 + 8L) = 0L;
        pointerFableModGfxMeshPtr2 = pointerFableModGfxMeshPtr1;
      }
      else
        pointerFableModGfxMeshPtr2 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E*) 0L;
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) pointerFableModGfxMeshPtr1);
    }
    this.m_pXArrowMesh = pointerFableModGfxMeshPtr2;
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pointerFableModGfxMeshPtr3 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pointerFableModGfxMeshPtr4;
    // ISSUE: fault handler
    try
    {
      if ((IntPtr) pointerFableModGfxMeshPtr3 != IntPtr.Zero)
      {
        *(long*) pointerFableModGfxMeshPtr3 = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VMesh\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
        *(long*) ((IntPtr) pointerFableModGfxMeshPtr3 + 8L) = 0L;
        pointerFableModGfxMeshPtr4 = pointerFableModGfxMeshPtr3;
      }
      else
        pointerFableModGfxMeshPtr4 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E*) 0L;
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) pointerFableModGfxMeshPtr3);
    }
    this.m_pYArrowMesh = pointerFableModGfxMeshPtr4;
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pointerFableModGfxMeshPtr5 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E* pointerFableModGfxMeshPtr6;
    // ISSUE: fault handler
    try
    {
      if ((IntPtr) pointerFableModGfxMeshPtr5 != IntPtr.Zero)
      {
        *(long*) pointerFableModGfxMeshPtr5 = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VMesh\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
        *(long*) ((IntPtr) pointerFableModGfxMeshPtr5 + 8L) = 0L;
        pointerFableModGfxMeshPtr6 = pointerFableModGfxMeshPtr5;
      }
      else
        pointerFableModGfxMeshPtr6 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E*) 0L;
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) pointerFableModGfxMeshPtr5);
    }
    this.m_pZArrowMesh = pointerFableModGfxMeshPtr6;
    Mesh* meshPtr1 = (Mesh*) \u003CModule\u003E.@new(336UL);
    Mesh* pObject1;
    // ISSUE: fault handler
    try
    {
      pObject1 = (IntPtr) meshPtr1 == IntPtr.Zero ? (Mesh*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EMesh\u002E\u007Bctor\u007D(meshPtr1);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) meshPtr1);
    }
    \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E\u002E\u003D(this.m_pXArrowMesh, pObject1);
    Mesh* meshPtr2 = (Mesh*) \u003CModule\u003E.@new(336UL);
    Mesh* pObject2;
    // ISSUE: fault handler
    try
    {
      pObject2 = (IntPtr) meshPtr2 == IntPtr.Zero ? (Mesh*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EMesh\u002E\u007Bctor\u007D(meshPtr2);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) meshPtr2);
    }
    \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E\u002E\u003D(this.m_pYArrowMesh, pObject2);
    Mesh* meshPtr3 = (Mesh*) \u003CModule\u003E.@new(336UL);
    Mesh* pObject3;
    // ISSUE: fault handler
    try
    {
      pObject3 = (IntPtr) meshPtr3 == IntPtr.Zero ? (Mesh*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EMesh\u002E\u007Bctor\u007D(meshPtr3);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) meshPtr3);
    }
    \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E\u002E\u003D(this.m_pZArrowMesh, pObject3);
    D3DXVECTOR3 d3DxvectoR3_1;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3DxvectoR3_1 = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_1 + 4) = 1.57079637f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_1 + 8) = 0.0f;
    \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002ESetRotation((Spatial*) *(long*) ((IntPtr) this.m_pXArrowMesh + 8L), &d3DxvectoR3_1);
    D3DXVECTOR3 d3DxvectoR3_2;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3DxvectoR3_2 = -1.57079637f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_2 + 8) = 0.0f;
    \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002ESetRotation((Spatial*) *(long*) ((IntPtr) this.m_pYArrowMesh + 8L), &d3DxvectoR3_2);
    ArrowheadModel* arrowheadModelPtr1 = (ArrowheadModel*) \u003CModule\u003E.@new(272UL);
    ArrowheadModel* arrowheadModelPtr2;
    // ISSUE: fault handler
    try
    {
      arrowheadModelPtr2 = (IntPtr) arrowheadModelPtr1 == IntPtr.Zero ? (ArrowheadModel*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EArrowheadModel\u002E\u007Bctor\u007D(arrowheadModelPtr1);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) arrowheadModelPtr1);
    }
    SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AArrowheadModel\u003E gfxArrowheadModel1;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ref gfxArrowheadModel1 = (long) &\u003CModule\u003E.\u003F\u003F_7\u003F\u0024SmartPointer\u0040VArrowheadModel\u0040Gfx\u0040FableMod\u0040\u0040\u0040Gfx\u0040FableMod\u0040\u00406B\u0040;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ((IntPtr) &gfxArrowheadModel1 + 8) = (long) arrowheadModelPtr2;
    if ((IntPtr) arrowheadModelPtr2 != IntPtr.Zero)
      *(int*) ((IntPtr) arrowheadModelPtr2 + 8L) = *(int*) ((IntPtr) arrowheadModelPtr2 + 8L) + 1;
    // ISSUE: fault handler
    try
    {
      \u003CModule\u003E.FableMod\u002EGfx\u002EArrowheadModel\u002ESetColor(arrowheadModelPtr2, 4294901760U);
      \u003CModule\u003E.FableMod\u002EGfx\u002EMesh\u002ESetModel((Mesh*) *(long*) ((IntPtr) this.m_pXArrowMesh + 8L), (Model*) arrowheadModelPtr2);
      Mesh* meshPtr4 = (Mesh*) *(long*) ((IntPtr) this.m_pXArrowMesh + 8L);
      *(int*) ((IntPtr) meshPtr4 + 32L /*0x20*/) = *(int*) ((IntPtr) meshPtr4 + 32L /*0x20*/) & -17;
      Mesh* meshPtr5 = (Mesh*) *(long*) ((IntPtr) this.m_pXArrowMesh + 8L);
      *(int*) ((IntPtr) meshPtr5 + 320L) = *(int*) ((IntPtr) meshPtr5 + 320L) | 256 /*0x0100*/;
      ArrowheadModel* arrowheadModelPtr3 = (ArrowheadModel*) \u003CModule\u003E.@new(272UL);
      ArrowheadModel* pObject4;
      // ISSUE: fault handler
      try
      {
        pObject4 = (IntPtr) arrowheadModelPtr3 == IntPtr.Zero ? (ArrowheadModel*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EArrowheadModel\u002E\u007Bctor\u007D(arrowheadModelPtr3);
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) arrowheadModelPtr3);
      }
      SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AArrowheadModel\u003E gfxArrowheadModel2;
      \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AArrowheadModel\u003E\u002E\u007Bctor\u007D(&gfxArrowheadModel2, pObject4);
      // ISSUE: fault handler
      try
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        \u003CModule\u003E.FableMod\u002EGfx\u002EArrowheadModel\u002ESetColor((ArrowheadModel*) ^(long&) ((IntPtr) &gfxArrowheadModel2 + 8), 4278255360U /*0xFF00FF00*/);
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        \u003CModule\u003E.FableMod\u002EGfx\u002EMesh\u002ESetModel((Mesh*) *(long*) ((IntPtr) this.m_pYArrowMesh + 8L), (Model*) ^(long&) ((IntPtr) &gfxArrowheadModel2 + 8));
        Mesh* meshPtr6 = (Mesh*) *(long*) ((IntPtr) this.m_pYArrowMesh + 8L);
        *(int*) ((IntPtr) meshPtr6 + 32L /*0x20*/) = *(int*) ((IntPtr) meshPtr6 + 32L /*0x20*/) & -17;
        Mesh* meshPtr7 = (Mesh*) *(long*) ((IntPtr) this.m_pYArrowMesh + 8L);
        *(int*) ((IntPtr) meshPtr7 + 320L) = *(int*) ((IntPtr) meshPtr7 + 320L) | 256 /*0x0100*/;
        ArrowheadModel* arrowheadModelPtr4 = (ArrowheadModel*) \u003CModule\u003E.@new(272UL);
        ArrowheadModel* pObject5;
        // ISSUE: fault handler
        try
        {
          pObject5 = (IntPtr) arrowheadModelPtr4 == IntPtr.Zero ? (ArrowheadModel*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EArrowheadModel\u002E\u007Bctor\u007D(arrowheadModelPtr4);
        }
        __fault
        {
          \u003CModule\u003E.delete((void*) arrowheadModelPtr4);
        }
        SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AArrowheadModel\u003E gfxArrowheadModel3;
        \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AArrowheadModel\u003E\u002E\u007Bctor\u007D(&gfxArrowheadModel3, pObject5);
        // ISSUE: fault handler
        try
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          \u003CModule\u003E.FableMod\u002EGfx\u002EArrowheadModel\u002ESetColor((ArrowheadModel*) ^(long&) ((IntPtr) &gfxArrowheadModel3 + 8), 4278190335U);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          \u003CModule\u003E.FableMod\u002EGfx\u002EMesh\u002ESetModel((Mesh*) *(long*) ((IntPtr) this.m_pZArrowMesh + 8L), (Model*) ^(long&) ((IntPtr) &gfxArrowheadModel3 + 8));
          Mesh* meshPtr8 = (Mesh*) *(long*) ((IntPtr) this.m_pZArrowMesh + 8L);
          *(int*) ((IntPtr) meshPtr8 + 32L /*0x20*/) = *(int*) ((IntPtr) meshPtr8 + 32L /*0x20*/) & -17;
          Mesh* meshPtr9 = (Mesh*) *(long*) ((IntPtr) this.m_pZArrowMesh + 8L);
          *(int*) ((IntPtr) meshPtr9 + 320L) = *(int*) ((IntPtr) meshPtr9 + 320L) | 256 /*0x0100*/;
          SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr1 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) \u003CModule\u003E.@new(16UL /*0x10*/);
          SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E* pointerFableModGfxNodePtr2;
          // ISSUE: fault handler
          try
          {
            if ((IntPtr) pointerFableModGfxNodePtr1 != IntPtr.Zero)
            {
              Node* nodePtr = (Node*) \u003CModule\u003E.@new(304UL);
              Node* pObject6;
              // ISSUE: fault handler
              try
              {
                pObject6 = (IntPtr) nodePtr == IntPtr.Zero ? (Node*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002ENode\u002E\u007Bctor\u007D(nodePtr);
              }
              __fault
              {
                \u003CModule\u003E.delete((void*) nodePtr);
              }
              pointerFableModGfxNodePtr2 = \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E\u002E\u007Bctor\u007D(pointerFableModGfxNodePtr1, pObject6);
            }
            else
              pointerFableModGfxNodePtr2 = (SmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003ANode\u003E*) 0L;
          }
          __fault
          {
            \u003CModule\u003E.delete((void*) pointerFableModGfxNodePtr1);
          }
          this.m_pAxes = pointerFableModGfxNodePtr2;
          \u003CModule\u003E.FableMod\u002EGfx\u002ERootObject\u002ESetName((RootObject*) *(long*) ((IntPtr) pointerFableModGfxNodePtr2 + 8L), (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CE\u0040FALCKKJ\u0040\u003F\u0024AAG\u003F\u0024AAf\u003F\u0024AAx\u003F\u0024AAT\u003F\u0024AAh\u003F\u0024AAi\u003F\u0024AAn\u003F\u0024AAg\u003F\u0024AAV\u003F\u0024AAi\u003F\u0024AAe\u003F\u0024AAw\u003F\u0024AA_\u003F\u0024AAA\u003F\u0024AAx\u003F\u0024AAe\u003F\u0024AAs\u003F\u0024AA\u003F\u0024AA\u0040);
          long num1 = *(long*) ((IntPtr) this.m_pAxes + 8L);
          Mesh* meshPtr10 = (Mesh*) *(long*) ((IntPtr) this.m_pXArrowMesh + 8L);
          long num2 = num1;
          Mesh* meshPtr11 = meshPtr10;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) num1 + 120L))((IntPtr) num2, (Spatial*) meshPtr11);
          long num3 = *(long*) ((IntPtr) this.m_pAxes + 8L);
          Mesh* meshPtr12 = (Mesh*) *(long*) ((IntPtr) this.m_pYArrowMesh + 8L);
          long num4 = num3;
          Mesh* meshPtr13 = meshPtr12;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) num3 + 120L))((IntPtr) num4, (Spatial*) meshPtr13);
          long num5 = *(long*) ((IntPtr) this.m_pAxes + 8L);
          Mesh* meshPtr14 = (Mesh*) *(long*) ((IntPtr) this.m_pZArrowMesh + 8L);
          long num6 = num5;
          Mesh* meshPtr15 = meshPtr14;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) num5 + 120L))((IntPtr) num6, (Spatial*) meshPtr15);
          Node* nodePtr1 = (Node*) *(long*) ((IntPtr) this.m_pAxes + 8L);
          *(int*) ((IntPtr) nodePtr1 + 32L /*0x20*/) = *(int*) ((IntPtr) nodePtr1 + 32L /*0x20*/) | 32 /*0x20*/;
          Node* root = this.GetRoot();
          Node* nodePtr2 = (Node*) *(long*) ((IntPtr) this.m_pAxes + 8L);
          Node* nodePtr3 = root;
          Node* nodePtr4 = nodePtr2;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) root + 120L))((IntPtr) nodePtr3, (Spatial*) nodePtr4);
          Plane* planePtr1 = (Plane*) \u003CModule\u003E.@new(16UL /*0x10*/);
          Plane* planePtr2;
          // ISSUE: fault handler
          try
          {
            planePtr2 = (IntPtr) planePtr1 == IntPtr.Zero ? (Plane*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EPlane\u002E\u007Bctor\u007D(planePtr1);
          }
          __fault
          {
            \u003CModule\u003E.delete((void*) planePtr1);
          }
          this.m_pPlane = planePtr2;
          this.Mode = EditorMode.Normal;
          this.m_bDirectionAxes = false;
          this.m_pWorkTexture = (EditableTexture*) 0L;
          this.m_iBrushSize = 1;
          this.m_NavEditMode = NavigationEditMode.Navigation;
          this.m_iDrawState = 0;
        }
        __fault
        {
          // ISSUE: method pointer
          // ISSUE: cast to a function pointer type
          \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AArrowheadModel\u003E\u002E\u007Bdtor\u007D), (void*) &gfxArrowheadModel3);
        }
        \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AArrowheadModel\u003E\u002E\u007Bdtor\u007D(&gfxArrowheadModel3);
      }
      __fault
      {
        // ISSUE: method pointer
        // ISSUE: cast to a function pointer type
        \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AArrowheadModel\u003E\u002E\u007Bdtor\u007D), (void*) &gfxArrowheadModel2);
      }
      \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AArrowheadModel\u003E\u002E\u007Bdtor\u007D(&gfxArrowheadModel2);
    }
    __fault
    {
      // ISSUE: method pointer
      // ISSUE: cast to a function pointer type
      \u003CModule\u003E.___CxxCallUnwindDtor((__FnPtr<void (void*)>) __methodptr(FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AArrowheadModel\u003E\u002E\u007Bdtor\u007D), (void*) &gfxArrowheadModel1);
    }
    \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AArrowheadModel\u003E\u002E\u007Bdtor\u007D(&gfxArrowheadModel1);
  }

  private void \u007EGfxThingView() => this.components?.Dispose();

  protected override unsafe void RenderInterface()
  {
    Device* mod1PeavDevice23Ea = \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA;
    \u0024ArrayType\u0024\u0024\u0024BY0IAA\u0040_W arrayTypeBy0IaaW;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(short&) ref arrayTypeBy0IaaW = (short) 0;
    if (!\u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EIsEmpty(this.m_pSelection))
    {
      if (this.m_bDirectionAxes)
      {
        int num1 = 0;
        if (0 < \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection))
        {
          do
          {
            Spatial* objectAt = \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetObjectAt(this.m_pSelection, num1);
            long num2 = *(long*) ((IntPtr) objectAt + 264L);
            float num3 = *(float*) (num2 + 20L) * 2f;
            BoundSphere* boundSpherePtr = (BoundSphere*) num2;
            D3DXVECTOR3 d3DxvectoR3_1;
            // ISSUE: cpblk instruction
            __memcpy(ref d3DxvectoR3_1, (IntPtr) boundSpherePtr + 8L, 12);
            D3DXVECTOR3 d3DxvectoR3_2;
            \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EGetWorldForward(objectAt, &d3DxvectoR3_2);
            D3DXVECTOR3 d3DxvectoR3_3;
            \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EGetWorldRight(objectAt, &d3DxvectoR3_3);
            D3DXVECTOR3 d3DxvectoR3_4;
            \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002EGetWorldUp(objectAt, &d3DxvectoR3_4);
            uint num4 = 4294902015;
            D3DXVECTOR3 d3DxvectoR3_5;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ref d3DxvectoR3_5 = ^(float&) ref d3DxvectoR3_3 * num3;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3_5 + 4) = ^(float&) ((IntPtr) &d3DxvectoR3_3 + 4) * num3;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3_5 + 8) = ^(float&) ((IntPtr) &d3DxvectoR3_3 + 8) * num3;
            D3DXVECTOR3 d3DxvectoR3_6;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ref d3DxvectoR3_6 = ^(float&) ref d3DxvectoR3_5 + ^(float&) ref d3DxvectoR3_1;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3_6 + 4) = ^(float&) ((IntPtr) &d3DxvectoR3_1 + 4) + ^(float&) ((IntPtr) &d3DxvectoR3_5 + 4);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3_6 + 8) = ^(float&) ((IntPtr) &d3DxvectoR3_1 + 8) + ^(float&) ((IntPtr) &d3DxvectoR3_5 + 8);
            \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EDrawLine(\u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA, &d3DxvectoR3_1, &d3DxvectoR3_6, &num4);
            uint num5 = 4294967040;
            D3DXVECTOR3 d3DxvectoR3_7;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ref d3DxvectoR3_7 = ^(float&) ref d3DxvectoR3_2 * num3;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3_7 + 4) = ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4) * num3;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3_7 + 8) = ^(float&) ((IntPtr) &d3DxvectoR3_2 + 8) * num3;
            D3DXVECTOR3 d3DxvectoR3_8;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ref d3DxvectoR3_8 = ^(float&) ref d3DxvectoR3_7 + ^(float&) ref d3DxvectoR3_1;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3_8 + 4) = ^(float&) ((IntPtr) &d3DxvectoR3_7 + 4) + ^(float&) ((IntPtr) &d3DxvectoR3_1 + 4);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3_8 + 8) = ^(float&) ((IntPtr) &d3DxvectoR3_7 + 8) + ^(float&) ((IntPtr) &d3DxvectoR3_1 + 8);
            \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EDrawLine(\u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA, &d3DxvectoR3_1, &d3DxvectoR3_8, &num5);
            uint num6 = 4278255615;
            D3DXVECTOR3 d3DxvectoR3_9;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ref d3DxvectoR3_9 = ^(float&) ref d3DxvectoR3_4 * num3;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3_9 + 4) = ^(float&) ((IntPtr) &d3DxvectoR3_4 + 4) * num3;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3_9 + 8) = ^(float&) ((IntPtr) &d3DxvectoR3_4 + 8) * num3;
            D3DXVECTOR3 d3DxvectoR3_10;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ref d3DxvectoR3_10 = ^(float&) ref d3DxvectoR3_9 + ^(float&) ref d3DxvectoR3_1;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3_10 + 4) = ^(float&) ((IntPtr) &d3DxvectoR3_9 + 4) + ^(float&) ((IntPtr) &d3DxvectoR3_1 + 4);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3_10 + 8) = ^(float&) ((IntPtr) &d3DxvectoR3_9 + 8) + ^(float&) ((IntPtr) &d3DxvectoR3_1 + 8);
            \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EDrawLine(\u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA, &d3DxvectoR3_1, &d3DxvectoR3_10, &num6);
            ++num1;
          }
          while (num1 < \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection));
        }
      }
      if (!this.m_bRotating)
      {
        uint num = 4294967065;
        \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EDrawBox(mod1PeavDevice23Ea, \u003CModule\u003E.FableMod\u002EGfx\u002EAxisBox\u002EGetMin((AxisBox*) this.m_pSelection), \u003CModule\u003E.FableMod\u002EGfx\u002EAxisBox\u002EGetMax((AxisBox*) this.m_pSelection), &num);
      }
    }
    this.DrawAxes();
    if (this.Mode == EditorMode.Player)
    {
      \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EDrawTextW(mod1PeavDevice23Ea, 6, 26, (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CE\u0040GEHKGPEF\u0040\u003F\u0024AAF\u003F\u0024AAi\u003F\u0024AAr\u003F\u0024AAs\u003F\u0024AAt\u003F\u0024AA\u003F5\u003F\u0024AAP\u003F\u0024AAe\u003F\u0024AAr\u003F\u0024AAs\u003F\u0024AAo\u003F\u0024AAn\u003F\u0024AA\u003F5\u003F\u0024AAM\u003F\u0024AAo\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AA\u003F\u0024AA\u0040, 4278190080U /*0xFF000000*/);
      \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EDrawTextW(mod1PeavDevice23Ea, 5, 25, (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CE\u0040GEHKGPEF\u0040\u003F\u0024AAF\u003F\u0024AAi\u003F\u0024AAr\u003F\u0024AAs\u003F\u0024AAt\u003F\u0024AA\u003F5\u003F\u0024AAP\u003F\u0024AAe\u003F\u0024AAr\u003F\u0024AAs\u003F\u0024AAo\u003F\u0024AAn\u003F\u0024AA\u003F5\u003F\u0024AAM\u003F\u0024AAo\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AA\u003F\u0024AA\u0040, uint.MaxValue);
    }
    \u003CModule\u003E._swprintf((char*) &arrayTypeBy0IaaW, (char*) &\u003CModule\u003E.\u003F\u003F_C\u0040_1CK\u0040DGILGNGD\u0040\u003F\u0024AAR\u003F\u0024AAe\u003F\u0024AAn\u003F\u0024AAd\u003F\u0024AAe\u003F\u0024AAr\u003F\u0024AAe\u003F\u0024AAd\u003F\u0024AA\u003F5\u003F\u0024AAo\u003F\u0024AAb\u003F\u0024AAj\u003F\u0024AAe\u003F\u0024AAc\u003F\u0024AAt\u003F\u0024AAs\u003F\u0024AA\u003F3\u003F\u0024AA\u003F5\u003F\u0024AA\u003F\u0024CF\u003F\u0024AAu\u003F\u0024AA\u003F\u0024AA\u0040, __arglist ((int) \u003CModule\u003E.FableMod\u002EGfx\u002ECamera\u002EGetRenderCount(this.m_pCamera)));
    \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EDrawTextW(mod1PeavDevice23Ea, 6, 6, (char*) &arrayTypeBy0IaaW, 4278190080U /*0xFF000000*/);
    \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EDrawTextW(mod1PeavDevice23Ea, 5, 5, (char*) &arrayTypeBy0IaaW, uint.MaxValue);
  }

  protected override unsafe void CameraHeartbeat()
  {
    base.CameraHeartbeat();
    if (this.Mode != EditorMode.Player)
      return;
    Camera* pCamera1 = this.m_pCamera;
    Camera* cameraPtr = pCamera1;
    D3DXVECTOR3 d3DxvectoR3_1;
    // ISSUE: cpblk instruction
    __memcpy(ref d3DxvectoR3_1, (IntPtr) cameraPtr + 88L, 12);
    D3DXVECTOR3 d3DxvectoR3_2;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3DxvectoR3_2 = *(float*) ((IntPtr) pCamera1 + 76L) - *(float*) ((IntPtr) pCamera1 + 88L);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4) = *(float*) ((IntPtr) pCamera1 + 76L + 4L) - *(float*) ((IntPtr) pCamera1 + 88L + 4L);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_2 + 8) = *(float*) ((IntPtr) pCamera1 + 76L + 8L) - *(float*) ((IntPtr) pCamera1 + 88L + 8L);
    D3DXVECTOR2 d3DxvectoR2;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3DxvectoR2 = ^(float&) ref d3DxvectoR3_1;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR2 + 4) = ^(float&) ((IntPtr) &d3DxvectoR3_1 + 4);
    float num;
    if (!\u003CModule\u003E.FableMod\u002EGfx\u002ETerrainManager\u002EGetHeight(\u003CModule\u003E.FableMod\u002EGfx\u002ETerrainManager\u002EGetInstance(), &d3DxvectoR2, &num))
      return;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_1 + 8) = this.m_fPlayerHeight + num;
    Camera* pCamera2 = this.m_pCamera;
    *(int*) ((IntPtr) pCamera2 + 8L) = *(int*) ((IntPtr) pCamera2 + 8L) | 6;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) pCamera2 + 88L, ref d3DxvectoR3_1, 12);
    D3DXVECTOR3 d3DxvectoR3_3;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3DxvectoR3_3 = ^(float&) ref d3DxvectoR3_2 + ^(float&) ref d3DxvectoR3_1;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_3 + 4) = ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4) + ^(float&) ((IntPtr) &d3DxvectoR3_1 + 4);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_3 + 8) = ^(float&) ((IntPtr) &d3DxvectoR3_2 + 8) + ^(float&) ((IntPtr) &d3DxvectoR3_1 + 8);
    Camera* pCamera3 = this.m_pCamera;
    *(int*) ((IntPtr) pCamera3 + 8L) = *(int*) ((IntPtr) pCamera3 + 8L) | 6;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) pCamera3 + 76L, ref d3DxvectoR3_3, 12);
  }

  protected unsafe void DrawAxes()
  {
    if (!this.AxesOn())
      return;
    Spatial* axes = (Spatial*) this.GetAxes();
    D3DXVECTOR3 d3DxvectoR3_1;
    // ISSUE: cpblk instruction
    __memcpy(ref d3DxvectoR3_1, (IntPtr) axes + 180L, 12);
    Mesh* meshPtr1 = (Mesh*) *(long*) ((IntPtr) this.m_pXArrowMesh + 8L);
    D3DXVECTOR3 d3DxvectoR3_2;
    // ISSUE: cpblk instruction
    __memcpy(ref d3DxvectoR3_2, (IntPtr) meshPtr1 + 180L, 12);
    Mesh* meshPtr2 = (Mesh*) *(long*) ((IntPtr) this.m_pYArrowMesh + 8L);
    D3DXVECTOR3 d3DxvectoR3_3;
    // ISSUE: cpblk instruction
    __memcpy(ref d3DxvectoR3_3, (IntPtr) meshPtr2 + 180L, 12);
    Mesh* meshPtr3 = (Mesh*) *(long*) ((IntPtr) this.m_pZArrowMesh + 8L);
    D3DXVECTOR3 d3DxvectoR3_4;
    // ISSUE: cpblk instruction
    __memcpy(ref d3DxvectoR3_4, (IntPtr) meshPtr3 + 180L, 12);
    \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EDrawLine(\u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA, &d3DxvectoR3_1, &d3DxvectoR3_2, &\u003CModule\u003E.\u003FA0x98271962\u002E\u003FCOLORX\u0040\u003F1\u003F\u003FDrawAxes\u0040GfxThingView\u0040Integration\u0040Gfx\u0040FableMod\u0040\u0040IE\u0024AAMXXZ\u00404KB);
    \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EDrawLine(\u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA, &d3DxvectoR3_1, &d3DxvectoR3_3, &\u003CModule\u003E.\u003FA0x98271962\u002E\u003FCOLORY\u0040\u003F1\u003F\u003FDrawAxes\u0040GfxThingView\u0040Integration\u0040Gfx\u0040FableMod\u0040\u0040IE\u0024AAMXXZ\u00404KB);
    \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EDrawLine(\u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA, &d3DxvectoR3_1, &d3DxvectoR3_4, &\u003CModule\u003E.\u003FA0x98271962\u002E\u003FCOLORZ\u0040\u003F1\u003F\u003FDrawAxes\u0040GfxThingView\u0040Integration\u0040Gfx\u0040FableMod\u0040\u0040IE\u0024AAMXXZ\u00404KB);
    \u003CModule\u003E.FableMod\u002EGfx\u002ECamera\u002ESetDistanceToCamera(this.m_pCamera, (Mesh*) *(long*) ((IntPtr) this.m_pXArrowMesh + 8L));
    \u003CModule\u003E.FableMod\u002EGfx\u002ECamera\u002ESetDistanceToCamera(this.m_pCamera, (Mesh*) *(long*) ((IntPtr) this.m_pYArrowMesh + 8L));
    \u003CModule\u003E.FableMod\u002EGfx\u002ECamera\u002ESetDistanceToCamera(this.m_pCamera, (Mesh*) *(long*) ((IntPtr) this.m_pZArrowMesh + 8L));
    long num1 = *(long*) ((IntPtr) this.m_pXArrowMesh + 8L);
    long num2 = num1;
    Camera* pCamera1 = this.m_pCamera;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, Camera*)>) *(long*) (*(long*) num1 + 112L /*0x70*/))((IntPtr) num2, pCamera1);
    long num3 = *(long*) ((IntPtr) this.m_pYArrowMesh + 8L);
    long num4 = num3;
    Camera* pCamera2 = this.m_pCamera;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, Camera*)>) *(long*) (*(long*) num3 + 112L /*0x70*/))((IntPtr) num4, pCamera2);
    long num5 = *(long*) ((IntPtr) this.m_pZArrowMesh + 8L);
    long num6 = num5;
    Camera* pCamera3 = this.m_pCamera;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, Camera*)>) *(long*) (*(long*) num5 + 112L /*0x70*/))((IntPtr) num6, pCamera3);
  }

  protected unsafe void SetupAxes()
  {
    IntPtr num1 = (IntPtr) this.GetAxes() + 32L /*0x20*/;
    *(int*) num1 = *(int*) num1 & -33;
    D3DXVECTOR3 d3DxvectoR3_1;
    \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002ESetWorldTranslation((Spatial*) this.GetAxes(), \u003CModule\u003E.FableMod\u002EGfx\u002EAxisBox\u002EGetCenter((AxisBox*) this.m_pSelection, &d3DxvectoR3_1));
    float num2 = \u003CModule\u003E.FableMod\u002EGfx\u002EAxisBox\u002EGetWidth((AxisBox*) this.m_pSelection) / 1.5f;
    float num3 = \u003CModule\u003E.FableMod\u002EGfx\u002EAxisBox\u002EGetHeight((AxisBox*) this.m_pSelection) / 1.5f;
    float num4 = \u003CModule\u003E.FableMod\u002EGfx\u002EAxisBox\u002EGetDepth((AxisBox*) this.m_pSelection) / 1.5f;
    float num5 = \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetRadius(this.m_pSelection) * 0.125f;
    if ((double) num5 < 0.0099999997764825821)
      num5 = 0.01f;
    Mesh* meshPtr1 = (Mesh*) *(long*) ((IntPtr) this.m_pXArrowMesh + 8L);
    *(int*) ((IntPtr) meshPtr1 + 32L /*0x20*/) = *(int*) ((IntPtr) meshPtr1 + 32L /*0x20*/) | 1;
    *(float*) ((IntPtr) meshPtr1 + 176L /*0xB0*/) = num5;
    Mesh* meshPtr2 = (Mesh*) *(long*) ((IntPtr) this.m_pYArrowMesh + 8L);
    *(int*) ((IntPtr) meshPtr2 + 32L /*0x20*/) = *(int*) ((IntPtr) meshPtr2 + 32L /*0x20*/) | 1;
    *(float*) ((IntPtr) meshPtr2 + 176L /*0xB0*/) = num5;
    Mesh* meshPtr3 = (Mesh*) *(long*) ((IntPtr) this.m_pZArrowMesh + 8L);
    *(int*) ((IntPtr) meshPtr3 + 32L /*0x20*/) = *(int*) ((IntPtr) meshPtr3 + 32L /*0x20*/) | 1;
    *(float*) ((IntPtr) meshPtr3 + 176L /*0xB0*/) = num5;
    D3DXVECTOR3 d3DxvectoR3_2;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3DxvectoR3_2 = num2;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_2 + 8) = 0.0f;
    Mesh* meshPtr4 = (Mesh*) *(long*) ((IntPtr) this.m_pXArrowMesh + 8L);
    *(int*) ((IntPtr) meshPtr4 + 32L /*0x20*/) = *(int*) ((IntPtr) meshPtr4 + 32L /*0x20*/) | 1;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) meshPtr4 + 100L, ref d3DxvectoR3_2, 12);
    D3DXVECTOR3 d3DxvectoR3_3;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3DxvectoR3_3 = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_3 + 4) = num4;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_3 + 8) = 0.0f;
    Mesh* meshPtr5 = (Mesh*) *(long*) ((IntPtr) this.m_pYArrowMesh + 8L);
    *(int*) ((IntPtr) meshPtr5 + 32L /*0x20*/) = *(int*) ((IntPtr) meshPtr5 + 32L /*0x20*/) | 1;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) meshPtr5 + 100L, ref d3DxvectoR3_3, 12);
    D3DXVECTOR3 d3DxvectoR3_4;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3DxvectoR3_4 = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_4 + 4) = 0.0f;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_4 + 8) = num3;
    Mesh* meshPtr6 = (Mesh*) *(long*) ((IntPtr) this.m_pZArrowMesh + 8L);
    *(int*) ((IntPtr) meshPtr6 + 32L /*0x20*/) = *(int*) ((IntPtr) meshPtr6 + 32L /*0x20*/) | 1;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) meshPtr6 + 100L, ref d3DxvectoR3_4, 12);
    Node* axes = this.GetAxes();
    Node* nodePtr = axes;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, byte)>) *(long*) (*(long*) axes + 16L /*0x10*/))((IntPtr) nodePtr, (byte) 1);
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected unsafe bool AxesOn()
  {
    return (~(*(int*) ((IntPtr) this.GetAxes() + 32L /*0x20*/) >>> 5) & 1) != 0;
  }

  protected unsafe void SetupPlane(Mesh* pArrowMesh)
  {
    Spatial* axes = (Spatial*) this.GetAxes();
    D3DXVECTOR3 d3DxvectoR3_1;
    // ISSUE: cpblk instruction
    __memcpy(ref d3DxvectoR3_1, (IntPtr) axes + 180L, 12);
    D3DXVECTOR3* d3DxvectoR3Ptr = (D3DXVECTOR3*) ((IntPtr) this.GetCamera() + 88L);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    float num1 = ^(float&) ((IntPtr) &d3DxvectoR3_1 + 8) - *(float*) ((IntPtr) d3DxvectoR3Ptr + 8L);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    float num2 = ^(float&) ((IntPtr) &d3DxvectoR3_1 + 4) - *(float*) ((IntPtr) d3DxvectoR3Ptr + 4L);
    D3DXVECTOR3 d3DxvectoR3_2;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3DxvectoR3_2 = ^(float&) ref d3DxvectoR3_1 - *(float*) d3DxvectoR3Ptr;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4) = num2;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_2 + 8) = num1;
    \u003CModule\u003E.D3DXVec3Normalize(&d3DxvectoR3_2, &d3DxvectoR3_2);
    if ((IntPtr) pArrowMesh == *(long*) ((IntPtr) this.m_pXArrowMesh + 8L))
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      if (Math.Abs((double) ^(float&) ((IntPtr) &d3DxvectoR3_2 + 8)) >= Math.Abs((double) ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4)))
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if ((double) ^(float&) ((IntPtr) &d3DxvectoR3_2 + 8) < 0.0)
        {
          D3DXVECTOR3 d3DxvectoR3_3;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ref d3DxvectoR3_3 = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_3 + 4) = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_3 + 8) = 1f;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) this.m_pPlane, ref d3DxvectoR3_3, 12);
        }
        else
        {
          D3DXVECTOR3 d3DxvectoR3_4;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ref d3DxvectoR3_4 = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_4 + 4) = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_4 + 8) = -1f;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) this.m_pPlane, ref d3DxvectoR3_4, 12);
        }
      }
      else
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if ((double) ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4) < 0.0)
        {
          D3DXVECTOR3 d3DxvectoR3_5;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ref d3DxvectoR3_5 = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_5 + 4) = 1f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_5 + 8) = 0.0f;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) this.m_pPlane, ref d3DxvectoR3_5, 12);
        }
        else
        {
          D3DXVECTOR3 d3DxvectoR3_6;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ref d3DxvectoR3_6 = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_6 + 4) = -1f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_6 + 8) = 0.0f;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) this.m_pPlane, ref d3DxvectoR3_6, 12);
        }
      }
    }
    else if ((IntPtr) pArrowMesh == *(long*) ((IntPtr) this.m_pYArrowMesh + 8L))
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      if (Math.Abs((double) ^(float&) ((IntPtr) &d3DxvectoR3_2 + 8)) >= Math.Abs((double) ^(float&) ref d3DxvectoR3_2))
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if ((double) ^(float&) ((IntPtr) &d3DxvectoR3_2 + 8) < 0.0)
        {
          D3DXVECTOR3 d3DxvectoR3_7;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ref d3DxvectoR3_7 = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_7 + 4) = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_7 + 8) = 1f;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) this.m_pPlane, ref d3DxvectoR3_7, 12);
        }
        else
        {
          D3DXVECTOR3 d3DxvectoR3_8;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ref d3DxvectoR3_8 = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_8 + 4) = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_8 + 8) = -1f;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) this.m_pPlane, ref d3DxvectoR3_8, 12);
        }
      }
      else
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if ((double) ^(float&) ref d3DxvectoR3_2 < 0.0)
        {
          D3DXVECTOR3 d3DxvectoR3_9;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ref d3DxvectoR3_9 = 1f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_9 + 4) = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_9 + 8) = 0.0f;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) this.m_pPlane, ref d3DxvectoR3_9, 12);
        }
        else
        {
          D3DXVECTOR3 d3DxvectoR3_10;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ref d3DxvectoR3_10 = -1f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_10 + 4) = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_10 + 8) = 0.0f;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) this.m_pPlane, ref d3DxvectoR3_10, 12);
        }
      }
    }
    else if ((IntPtr) pArrowMesh == *(long*) ((IntPtr) this.m_pZArrowMesh + 8L))
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      if (Math.Abs((double) ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4)) >= Math.Abs((double) ^(float&) ref d3DxvectoR3_2))
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if ((double) ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4) < 0.0)
        {
          D3DXVECTOR3 d3DxvectoR3_11;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ref d3DxvectoR3_11 = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_11 + 4) = 1f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_11 + 8) = 0.0f;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) this.m_pPlane, ref d3DxvectoR3_11, 12);
        }
        else
        {
          D3DXVECTOR3 d3DxvectoR3_12;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ref d3DxvectoR3_12 = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_12 + 4) = -1f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_12 + 8) = 0.0f;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) this.m_pPlane, ref d3DxvectoR3_12, 12);
        }
      }
      else
      {
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        if ((double) ^(float&) ref d3DxvectoR3_2 < 0.0)
        {
          D3DXVECTOR3 d3DxvectoR3_13;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ref d3DxvectoR3_13 = 1f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_13 + 4) = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_13 + 8) = 0.0f;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) this.m_pPlane, ref d3DxvectoR3_13, 12);
        }
        else
        {
          D3DXVECTOR3 d3DxvectoR3_14;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ref d3DxvectoR3_14 = -1f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_14 + 4) = 0.0f;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3_14 + 8) = 0.0f;
          // ISSUE: cpblk instruction
          __memcpy((IntPtr) this.m_pPlane, ref d3DxvectoR3_14, 12);
        }
      }
    }
    Plane* pPlane = this.m_pPlane;
    Plane* planePtr = pPlane;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    *(float*) ((IntPtr) pPlane + 12L) = (float) ((double) *(float*) ((IntPtr) planePtr + 4L) * (double) ^(float&) ((IntPtr) &d3DxvectoR3_1 + 4) + (double) *(float*) planePtr * (double) ^(float&) ref d3DxvectoR3_1 + (double) *(float*) ((IntPtr) planePtr + 8L) * (double) ^(float&) ((IntPtr) &d3DxvectoR3_1 + 8));
  }

  protected unsafe void UpdateMovePoint()
  {
    D3DXVECTOR3 d3DxvectoR3_1;
    D3DXVECTOR3 d3DxvectoR3_2;
    if (!\u003CModule\u003E.FableMod\u002EGfx\u002ECamera\u002EScreenToRay(this.GetCamera(), this.m_pvMouse, &d3DxvectoR3_1, &d3DxvectoR3_2))
      return;
    float planeDistance = this.GetPlaneDistance(&d3DxvectoR3_1, &d3DxvectoR3_2, this.m_pPlane);
    if (Math.Abs((double) (planeDistance - float.MaxValue)) < 2.0)
      return;
    D3DXVECTOR3 d3DxvectoR3_3;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3DxvectoR3_3 = ^(float&) ref d3DxvectoR3_2 * planeDistance;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_3 + 4) = ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4) * planeDistance;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_3 + 8) = ^(float&) ((IntPtr) &d3DxvectoR3_2 + 8) * planeDistance;
    D3DXVECTOR3 d3DxvectoR3_4;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ref d3DxvectoR3_4 = ^(float&) ref d3DxvectoR3_3 + ^(float&) ref d3DxvectoR3_1;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_4 + 4) = ^(float&) ((IntPtr) &d3DxvectoR3_1 + 4) + ^(float&) ((IntPtr) &d3DxvectoR3_3 + 4);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &d3DxvectoR3_4 + 8) = ^(float&) ((IntPtr) &d3DxvectoR3_1 + 8) + ^(float&) ((IntPtr) &d3DxvectoR3_3 + 8);
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) this.m_pvMovePoint, ref d3DxvectoR3_4, 12);
    Mesh* pArrow = this.m_pArrow;
    if ((IntPtr) pArrow == *(long*) ((IntPtr) this.m_pXArrowMesh + 8L))
    {
      *(float*) ((IntPtr) this.m_pvMovePoint + 4L) = *(float*) ((IntPtr) this.GetAxes() + 184L);
      *(float*) ((IntPtr) this.m_pvMovePoint + 8L) = *(float*) ((IntPtr) this.GetAxes() + 188L);
    }
    else if ((IntPtr) pArrow == *(long*) ((IntPtr) this.m_pYArrowMesh + 8L))
    {
      *(float*) this.m_pvMovePoint = *(float*) ((IntPtr) this.GetAxes() + 180L);
      *(float*) ((IntPtr) this.m_pvMovePoint + 8L) = *(float*) ((IntPtr) this.GetAxes() + 188L);
    }
    else
    {
      if ((IntPtr) pArrow != *(long*) ((IntPtr) this.m_pZArrowMesh + 8L))
        return;
      *(float*) this.m_pvMovePoint = *(float*) ((IntPtr) this.GetAxes() + 180L);
      *(float*) ((IntPtr) this.m_pvMovePoint + 4L) = *(float*) ((IntPtr) this.GetAxes() + 184L);
    }
  }

  protected unsafe float GetPlaneDistance(
    D3DXVECTOR3* pvOrigin,
    D3DXVECTOR3* pvDirection,
    Plane* pPlane)
  {
    float num1 = *(float*) ((IntPtr) pPlane + 4L);
    float num2 = *(float*) pPlane;
    float num3 = *(float*) ((IntPtr) pPlane + 8L);
    float num4 = (float) ((double) *(float*) ((IntPtr) pvDirection + 4L) * (double) num1 + (double) *(float*) pvDirection * (double) num2 + (double) *(float*) ((IntPtr) pvDirection + 8L) * (double) num3);
    return Math.Abs((double) num4) > 9.9999997473787516E-05 ? (*(float*) ((IntPtr) pPlane + 12L) - (float) ((double) *(float*) ((IntPtr) pvOrigin + 4L) * (double) num1 + (double) *(float*) pvOrigin * (double) num2 + (double) *(float*) ((IntPtr) pvOrigin + 8L) * (double) num3)) / num4 : float.MaxValue;
  }

  protected unsafe void StopMove()
  {
    if (!this.m_bMoving)
      return;
    this.m_bMoving = false;
    this.UpdateSelectedThings();
    Mesh* pArrow = this.m_pArrow;
    if ((IntPtr) pArrow == IntPtr.Zero)
      return;
    *(int*) ((IntPtr) pArrow + 320L) = *(int*) ((IntPtr) pArrow + 320L) & -2;
    this.m_pArrow = (Mesh*) 0L;
  }

  protected unsafe void StopRotation()
  {
    if (!this.m_bRotating)
      return;
    \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EStopRotation(this.m_pSelection);
    this.m_bRotating = false;
    this.UpdateSelectedThings();
    Mesh* pArrow = this.m_pArrow;
    if ((IntPtr) pArrow == IntPtr.Zero)
      return;
    *(int*) ((IntPtr) pArrow + 320L) = *(int*) ((IntPtr) pArrow + 320L) & -2;
    this.m_pArrow = (Mesh*) 0L;
  }

  protected unsafe void UpdateSelectedThings()
  {
    int num = 0;
    if (0 >= \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection))
      return;
    do
    {
      ((GfxThingInterface) this.ThingController.FindThing((uint) *(int*) ((IntPtr) \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetObjectAt(this.m_pSelection, num) + 272L)).Interface).UpdateThing();
      ++num;
    }
    while (num < \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection));
  }

  protected unsafe void SelectDuplicates()
  {
    ulong numObjects1 = (ulong) \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection);
    Spatial** spatialPtr = (Spatial**) \u003CModule\u003E.new\u005B\u005D(numObjects1 > 2305843009213693951UL /*0x1FFFFFFFFFFFFFFF*/ ? ulong.MaxValue : numObjects1 * 8UL);
    int numObjects2 = \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection);
    int num1 = 0;
    long num2 = 0;
    long num3 = (long) numObjects2;
    if (0L < num3)
    {
      do
      {
        Thing thing1 = this.ThingController.FindThing((uint) *(int*) ((IntPtr) \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetObjectAt(this.m_pSelection, num1) + 272L));
        GfxThingInterface gfxThingInterface1 = (GfxThingInterface) thing1.Interface;
        Thing thing2 = (Thing) thing1.Duplicate();
        GfxThingInterface gfxThingInterface2 = new GfxThingInterface(gfxThingInterface1.Map, thing2);
        if (!gfxThingInterface2.Create(this.ThingController))
        {
          thing2?.Dispose();
          gfxThingInterface2?.Dispose();
        }
        else
        {
          *(long*) (num2 * 8L + (IntPtr) spatialPtr) = (long) gfxThingInterface2.GetNode();
          gfxThingInterface1.Highlight = false;
          gfxThingInterface2.Highlight = true;
          gfxThingInterface2.Update();
        }
        ++num1;
        ++num2;
      }
      while (num2 < num3);
    }
    \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EClear(this.m_pSelection);
    long num4 = 0;
    if (0L < num3)
    {
      do
      {
        ulong num5 = (ulong) *(long*) (num4 * 8L + (IntPtr) spatialPtr);
        if (num5 != 0UL)
          \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EAdd(this.m_pSelection, (Spatial*) num5);
        ++num4;
      }
      while (num4 < num3);
    }
    \u003CModule\u003E.delete\u005B\u005D((void*) spatialPtr);
    this.CallThingSelected();
  }

  protected unsafe void SetSelectionHighlight([MarshalAs(UnmanagedType.U1)] bool on)
  {
    int num = 0;
    if (0 >= \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection))
      return;
    do
    {
      Thing thing = this.ThingController.FindThing((uint) *(int*) ((IntPtr) \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetObjectAt(this.m_pSelection, num) + 272L));
      if (thing != null)
        ((GfxThingInterface) thing.Interface).Highlight = on;
      ++num;
    }
    while (num < \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection));
  }

  protected unsafe void ToWalkMode()
  {
    this.ToGroundMode();
    if (this.m_Maps != null && this.m_EditMap != null)
    {
      EditableTexture* pWorkTexture = this.m_pWorkTexture;
      if ((IntPtr) pWorkTexture != IntPtr.Zero)
      {
        EditableTexture* editableTexturePtr1 = pWorkTexture;
        EditableTexture* editableTexturePtr2 = editableTexturePtr1;
        // ISSUE: cast to a function pointer type
        // ISSUE: function pointer call
        void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) editableTexturePtr1)((IntPtr) editableTexturePtr2, 1U);
        this.m_pWorkTexture = (EditableTexture*) 0L;
      }
      this.m_EditMap.CreateWalkTexture();
      EditableTexture* editableTexturePtr3 = (EditableTexture*) *(long*) ((IntPtr) this.m_EditMap.GetTerrain() + 384L);
      EditableTexture* editableTexturePtr4 = \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002EDuplicate(editableTexturePtr3);
      this.m_pWorkTexture = editableTexturePtr4;
      \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002ECopyTo(editableTexturePtr3, editableTexturePtr4, (tagRECT*) 0L);
    }
    this.m_BtnCfg.Enable("Walk", true);
    this.m_BtnCfg.Enable("Thing", false);
    this.m_iY = -1;
    this.m_iX = -1;
  }

  protected unsafe void ToNormalMode()
  {
    this.m_BtnCfg.Enable("Walk", false);
    this.m_BtnCfg.Enable("Thing", true);
    this.m_BtnCfg.Enable("ThingCreate", false);
    this.SaveGroundChanges();
    this.ThingController.ToGroundEditMode(false);
    if (this.Mode != EditorMode.Player)
      return;
    float fSavedFov = this.m_fSavedFOV;
    Camera* pCamera = this.m_pCamera;
    *(float*) ((IntPtr) pCamera + 380L) = fSavedFov;
    *(int*) ((IntPtr) pCamera + 8L) = *(int*) ((IntPtr) pCamera + 8L) | 18;
  }

  protected unsafe void ToNavMode()
  {
    this.ToGroundMode();
    if (this.m_Maps != null)
    {
      ThingMap editMap = this.m_EditMap;
      if (editMap != null)
      {
        editMap.CreateNavTexture(this.EditSection, this.EditSubset);
        EditableTexture* editableTexturePtr1 = (EditableTexture*) *(long*) ((IntPtr) this.m_EditMap.GetTerrain() + 384L);
        if ((IntPtr) editableTexturePtr1 != IntPtr.Zero)
        {
          EditableTexture* pWorkTexture = this.m_pWorkTexture;
          if ((IntPtr) pWorkTexture != IntPtr.Zero)
          {
            EditableTexture* editableTexturePtr2 = pWorkTexture;
            EditableTexture* editableTexturePtr3 = editableTexturePtr2;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) editableTexturePtr2)((IntPtr) editableTexturePtr3, 1U);
            this.m_pWorkTexture = (EditableTexture*) 0L;
          }
          EditableTexture* editableTexturePtr4 = \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002EDuplicate(editableTexturePtr1);
          this.m_pWorkTexture = editableTexturePtr4;
          \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002ECopyTo(editableTexturePtr1, editableTexturePtr4, (tagRECT*) 0L);
        }
      }
    }
    this.m_iY = -1;
    this.m_iX = -1;
  }

  protected void ToGroundMode()
  {
    if (this.Mode != EditorMode.Normal && this.Mode != EditorMode.Player)
    {
      this.SaveGroundChanges();
    }
    else
    {
      this.ClearSelection();
      this.ThingController.ToGroundEditMode(true);
    }
  }

  protected unsafe void SaveGroundChanges()
  {
    if (this.Mode != EditorMode.Walk && this.Mode != EditorMode.Navigation || this.m_Maps == null)
      return;
    ThingMap editMap = this.m_EditMap;
    if (editMap == null)
      return;
    long num = *(long*) ((IntPtr) editMap.GetTerrain() + 384L);
    if (num != 0L)
    {
      \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002ECopyTo(this.m_pWorkTexture, (EditableTexture*) num, (tagRECT*) 0L);
      if (this.Mode == EditorMode.Walk)
        this.m_EditMap.FinishWalkTexture();
      else if (this.Mode == EditorMode.Navigation)
        this.m_EditMap.FinishNavTexture(this.EditSection, this.EditSubset);
    }
    EditableTexture* pWorkTexture = this.m_pWorkTexture;
    if ((IntPtr) pWorkTexture == IntPtr.Zero)
      return;
    EditableTexture* editableTexturePtr1 = pWorkTexture;
    EditableTexture* editableTexturePtr2 = editableTexturePtr1;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) editableTexturePtr1)((IntPtr) editableTexturePtr2, 1U);
    this.m_pWorkTexture = (EditableTexture*) 0L;
  }

  protected unsafe void CallThingSelected()
  {
    if (\u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection) <= 0)
      return;
    GfxThingView gfxThingView = this;
    gfxThingView.raise_ThingSelected(gfxThingView.GetSelectedThings());
  }

  protected unsafe Thing[] GetSelectedThings()
  {
    int numObjects = \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection);
    Thing[] selectedThings = new Thing[numObjects];
    int index = 0;
    if (0 < numObjects)
    {
      do
      {
        Spatial* objectAt = \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetObjectAt(this.m_pSelection, index);
        selectedThings[index] = this.ThingController.FindThing((uint) *(int*) ((IntPtr) objectAt + 272L));
        ++index;
      }
      while (index < numObjects);
    }
    return selectedThings;
  }

  protected void ApplyOwnership(Thing parent)
  {
    Thing[] selectedThings = this.GetSelectedThings();
    int num1 = 0;
    int index = 0;
    if (0 >= selectedThings.Length)
      return;
    do
    {
      if (selectedThings[index] != parent)
      {
        CTCBlock ctcBlock = selectedThings[index].ApplyCTC((TNGDefinitions) null, "CTCOwnedEntity");
        if (ctcBlock != null && ctcBlock.get_Variables("OwnerUID").StringValue != parent.UID)
        {
          ctcBlock.get_Variables("OwnerUID").Value = (object) parent.UID;
          ++num1;
        }
      }
      ++index;
    }
    while (index < selectedThings.Length);
    if (num1 <= 0)
      return;
    int num2 = (int) MessageBox.Show($"Made {parent.DefinitionType}:{parent.UID} as the owner of {num1} thing(s).", "Ownership");
  }

  protected void RemoveOwnership(Thing parent)
  {
    Thing[] selectedThings = this.GetSelectedThings();
    int num1 = 0;
    int index = 0;
    if (0 >= selectedThings.Length)
      return;
    do
    {
      if (selectedThings[index] != parent)
      {
        CTCBlock ctcBlock = selectedThings[index].ApplyCTC((TNGDefinitions) null, "CTCOwnedEntity");
        if (ctcBlock != null && ctcBlock.get_Variables("OwnerUID").StringValue == parent.UID)
        {
          ctcBlock.get_Variables("OwnerUID").Value = (object) "0";
          ++num1;
        }
      }
      ++index;
    }
    while (index < selectedThings.Length);
    if (num1 <= 0)
      return;
    int num2 = (int) MessageBox.Show($"Removed {parent.DefinitionType}:{parent.UID} as the owner of {num1} thing(s).", "Ownership");
  }

  protected override unsafe void OnRender()
  {
    if (this.Mode != EditorMode.Navigation && this.Mode != EditorMode.Walk || this.m_iDrawState == 0)
      return;
    D3DXVECTOR3 d3DxvectoR3_1;
    D3DXVECTOR3 d3DxvectoR3_2;
    if (\u003CModule\u003E.FableMod\u002EGfx\u002ECamera\u002EScreenToRay(this.GetCamera(), this.m_pvMouse, &d3DxvectoR3_1, &d3DxvectoR3_2))
    {
      Terrain* terrain = this.m_EditMap.GetTerrain();
      int num1 = *(int*) ((IntPtr) terrain + 392L);
      int num2 = *(int*) ((IntPtr) terrain + 396L);
      if (this.Mode == EditorMode.Navigation)
      {
        num1 *= 2;
        num2 *= 2;
      }
      uint iX1;
      uint iY1;
      if (\u003CModule\u003E.FableMod\u002EGfx\u002ETerrain\u002EGetLocation(terrain, &d3DxvectoR3_1, &d3DxvectoR3_2, (uint) num1, (uint) num2, &iX1, &iY1))
      {
        EditableTexture* pTexture = (EditableTexture*) *(long*) ((IntPtr) terrain + 384L);
        if ((IntPtr) pTexture == IntPtr.Zero)
          return;
        int iX2 = this.m_iX;
        tagRECT tagRect;
        if (iX2 >= 0)
        {
          int iY2 = this.m_iY;
          if (iY2 >= 0)
          {
            \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002E\u003FA0x98271962\u002ECreateDirtyRect(pTexture, iX2, iY2, this.m_iBrushSize, &tagRect);
            \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002ECopyTo(this.m_pWorkTexture, pTexture, &tagRect);
            goto label_10;
          }
        }
        \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002ECopyTo(this.m_pWorkTexture, pTexture, (tagRECT*) 0L);
label_10:
        uint num3 = 0;
        if (this.Mode == EditorMode.Walk)
        {
          num3 = this.m_MapColor;
        }
        else
        {
          switch (this.m_iDrawState)
          {
            case 1:
              num3 = this.NavEditMode == NavigationEditMode.Navigation ? 4286611584U : 4278219730U;
              break;
            case 2:
              num3 = 4278190080U /*0xFF000000*/;
              break;
          }
        }
        bool flag = false;
        \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002ELock(pTexture);
        if (num3 != 0U)
        {
          int iBrushSize = this.m_iBrushSize;
          int iSize = iBrushSize;
          int iX3 = this.m_iX;
          if (iX3 >= 0)
          {
            int iY3 = this.m_iY;
            if (iY3 >= 0)
            {
              \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002EDrawThickLine(pTexture, iX3, iY3, (int) iX1, (int) iY1, num3, iBrushSize);
              int num4 = Math.Abs(this.m_iX - (int) iX1);
              int num5 = Math.Abs(this.m_iY - (int) iY1);
              int num6 = num4 > num5 ? num4 : num5;
              iSize += num6;
              goto label_20;
            }
          }
          \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002EDrawFilledCircle(pTexture, (int) iX1, (int) iY1, iBrushSize / 2, num3);
label_20:
          \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002E\u003FA0x98271962\u002ECreateDirtyRect(pTexture, (int) iX1, (int) iY1, iSize, &tagRect);
          flag = true;
        }
        else
          \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002EDrawFilledCircle(pTexture, (int) iX1, (int) iY1, this.m_iBrushSize / 2, 4294630400U);
        this.m_iX = (int) iX1;
        this.m_iY = (int) iY1;
        \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002EUnlock(pTexture);
        if (flag)
        {
          if (this.Mode == EditorMode.Navigation)
            this.m_EditMap.LEV.Modified = true;
          \u003CModule\u003E.FableMod\u002EGfx\u002EEditableTexture\u002ECopyTo(pTexture, this.m_pWorkTexture, &tagRect);
        }
      }
      else
      {
        this.m_iX = -1;
        this.m_iY = -1;
      }
    }
    this.m_iDrawState = 0;
  }

  protected unsafe Mesh* GetMeshAtMouse()
  {
    D3DXVECTOR3 d3DxvectoR3_1;
    D3DXVECTOR3 d3DxvectoR3_2;
    if (!\u003CModule\u003E.FableMod\u002EGfx\u002ECamera\u002EScreenToRay(this.GetCamera(), this.m_pvMouse, &d3DxvectoR3_1, &d3DxvectoR3_2))
      return (Mesh*) 0L;
    PickData pickData;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ref pickData = 0L;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &pickData + 8) = float.MaxValue;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &pickData + 36) = -1;
    Node* root = this.GetRoot();
    Node* nodePtr = root;
    ref D3DXVECTOR3 local1 = ref d3DxvectoR3_1;
    ref D3DXVECTOR3 local2 = ref d3DxvectoR3_2;
    ref PickData local3 = ref pickData;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, D3DXVECTOR3*, D3DXVECTOR3*, PickData*)>) *(long*) (*(long*) root + 64L /*0x40*/))((IntPtr) nodePtr, (D3DXVECTOR3*) ref local1, (D3DXVECTOR3*) ref local2, (PickData*) ref local3);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    return (IntPtr) \u003CModule\u003E.__RTDynamicCast((void*) ^(long&) ref pickData, 0, (void*) &\u003CModule\u003E.\u003F\u003F_R0\u003FAVMesh\u0040Gfx\u0040FableMod\u0040\u0040\u00408, (void*) &\u003CModule\u003E.\u003F\u003F_R0\u003FAVTerrain\u0040Gfx\u0040FableMod\u0040\u0040\u00408, 0) == 0L ? (Mesh*) ^(long&) ref pickData : (Mesh*) 0L;
  }

  protected unsafe Mesh* GetArrowAtMouse()
  {
    D3DXVECTOR3 d3DxvectoR3_1;
    D3DXVECTOR3 d3DxvectoR3_2;
    if (!\u003CModule\u003E.FableMod\u002EGfx\u002ECamera\u002EScreenToRay(this.GetCamera(), this.m_pvMouse, &d3DxvectoR3_1, &d3DxvectoR3_2))
      return (Mesh*) 0L;
    PickData pickData;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ref pickData = 0L;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(float&) ((IntPtr) &pickData + 8) = float.MaxValue;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &pickData + 36) = -1;
    Node* axes = this.GetAxes();
    Node* nodePtr = axes;
    ref D3DXVECTOR3 local1 = ref d3DxvectoR3_1;
    ref D3DXVECTOR3 local2 = ref d3DxvectoR3_2;
    ref PickData local3 = ref pickData;
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, D3DXVECTOR3*, D3DXVECTOR3*, PickData*)>) *(long*) (*(long*) axes + 64L /*0x40*/))((IntPtr) nodePtr, (D3DXVECTOR3*) ref local1, (D3DXVECTOR3*) ref local2, (PickData*) ref local3);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    return (Mesh*) ^(long&) ref pickData;
  }

  protected unsafe Node* GetAxes() => (Node*) *(long*) ((IntPtr) this.m_pAxes + 8L);

  [return: MarshalAs(UnmanagedType.U1)]
  protected unsafe bool CheckLockedInPlace([MarshalAs(UnmanagedType.U1)] bool begin)
  {
    int num1 = 0;
    if (0 < \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection))
    {
      do
      {
        if (((GfxThingInterface) this.ThingController.FindThing((uint) *(int*) ((IntPtr) \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetObjectAt(this.m_pSelection, num1) + 272L)).Interface).LockedInPlace)
        {
          byte num2 = (byte) !begin;
          \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EEnableAt(this.m_pSelection, num1, (bool) num2);
        }
        ++num1;
      }
      while (num1 < \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection));
    }
    return \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EHasEnabled(this.m_pSelection);
  }

  protected override void BtnCfg_OnButtonDown(string button, MouseEventArgs e)
  {
    if (this.Mode == EditorMode.Normal)
      this.ButtonDownNormal(button);
    else if (this.Mode == EditorMode.Walk)
    {
      switch (button)
      {
        case "WalkDraw":
          this.m_MapColor = 4286085250U;
          break;
        case "WalkErase":
          this.m_MapColor = 4278190080U /*0xFF000000*/;
          break;
      }
    }
    base.BtnCfg_OnButtonDown(button, e);
  }

  protected override unsafe void BtnCfg_OnButtonUp(string button, MouseEventArgs e)
  {
    if (button.StartsWith("Thing"))
    {
      if (this.Mode == EditorMode.Create && button == "ThingCreate")
      {
        D3DXVECTOR3 d3DxvectoR3_1;
        D3DXVECTOR3 d3DxvectoR3_2;
        if (\u003CModule\u003E.FableMod\u002EGfx\u002ECamera\u002EScreenToRay(this.GetCamera(), this.m_pvMouse, &d3DxvectoR3_1, &d3DxvectoR3_2))
        {
          PickData pickData;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(long&) ref pickData = 0L;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &pickData + 8) = float.MaxValue;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ((IntPtr) &pickData + 36) = -1;
          Node* root = this.GetRoot();
          Node* nodePtr = root;
          ref D3DXVECTOR3 local1 = ref d3DxvectoR3_1;
          ref D3DXVECTOR3 local2 = ref d3DxvectoR3_2;
          ref PickData local3 = ref pickData;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          __calli((__FnPtr<void (IntPtr, D3DXVECTOR3*, D3DXVECTOR3*, PickData*)>) *(long*) (*(long*) root + 64L /*0x40*/))((IntPtr) nodePtr, (D3DXVECTOR3*) ref local1, (D3DXVECTOR3*) ref local2, (PickData*) ref local3);
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          if (^(long&) ref pickData != 0L)
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            this.raise_ThingCreated(^(float&) ((IntPtr) &pickData + 12), ^(float&) ((IntPtr) &pickData + 16 /*0x10*/), ^(float&) ((IntPtr) &pickData + 20), ^(float&) ((IntPtr) &pickData + 24), ^(float&) ((IntPtr) &pickData + 28), ^(float&) ((IntPtr) &pickData + 32 /*0x20*/));
          }
        }
      }
      else if (this.Mode == EditorMode.Pick && button == "ThingCreate")
      {
        Mesh* meshAtMouse = this.GetMeshAtMouse();
        if ((IntPtr) meshAtMouse != IntPtr.Zero)
        {
          Spatial* idObject = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002E\u003FA0x98271962\u002EGetIDObject((Spatial*) meshAtMouse);
          if ((IntPtr) idObject != IntPtr.Zero)
          {
            Thing thing = this.ThingController.FindThing((uint) *(int*) ((IntPtr) idObject + 272L));
            if (thing != null)
              this.raise_ThingPicked(thing);
          }
        }
      }
      else
      {
        this.StopMove();
        this.StopRotation();
        this.CheckLockedInPlace(false);
      }
    }
    else if (this.Mode == EditorMode.Walk)
      this.m_MapColor = 0U;
    base.BtnCfg_OnButtonUp(button, e);
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected unsafe bool ButtonDownNormal(string button)
  {
    if ((IntPtr) this.m_pArrow != IntPtr.Zero)
    {
      if (this.CheckLockedInPlace(true))
      {
        switch (button)
        {
          case "ThingMove":
          case "ThingCopyMove":
            if (button == "ThingCopyMove")
              this.SelectDuplicates();
            GfxThingView gfxThingView = this;
            gfxThingView.SetupPlane(gfxThingView.m_pArrow);
            this.m_bMoving = true;
            this.UpdateMovePoint();
            D3DXVECTOR3* pvMovePoint = this.m_pvMovePoint;
            D3DXVECTOR3* d3DxvectoR3Ptr = (D3DXVECTOR3*) ((IntPtr) this.GetAxes() + 180L);
            D3DXVECTOR3 d3DxvectoR3;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ref d3DxvectoR3 = *(float*) d3DxvectoR3Ptr - *(float*) pvMovePoint;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3 + 4) = *(float*) ((IntPtr) d3DxvectoR3Ptr + 4L) - *(float*) ((IntPtr) pvMovePoint + 4L);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3 + 8) = *(float*) ((IntPtr) d3DxvectoR3Ptr + 8L) - *(float*) ((IntPtr) pvMovePoint + 8L);
            // ISSUE: cpblk instruction
            __memcpy((IntPtr) this.m_pvMoveDiff, ref d3DxvectoR3, 12);
            break;
          case "ThingRotate":
          case "ThingCopyRotate":
            if (button == "ThingCopyRotate")
              this.SelectDuplicates();
            \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EStartRotation(this.m_pSelection);
            this.m_bRotating = true;
            // ISSUE: cpblk instruction
            __memcpy((IntPtr) this.m_pvMousePoint, (IntPtr) this.m_pvMouse, 8);
            break;
        }
      }
      return true;
    }
    switch (button)
    {
      case "ThingSelect":
        this.SelectThing(false);
        break;
      case "ThingAddSelect":
        this.SelectThing(true);
        break;
      case "ThingFocus":
        if (!\u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EIsEmpty(this.m_pSelection))
        {
          Mesh* meshAtMouse = this.GetMeshAtMouse();
          if ((IntPtr) meshAtMouse != IntPtr.Zero)
          {
            Spatial* idObject = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002E\u003FA0x98271962\u002EGetIDObject((Spatial*) meshAtMouse);
            if ((IntPtr) idObject != IntPtr.Zero && \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EHasObject(this.m_pSelection, idObject))
            {
              this.CameraFocus(idObject, CameraDirection.Front);
              break;
            }
            break;
          }
          break;
        }
        break;
    }
    return false;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected unsafe bool MouseMoveNormal(MouseEventArgs e)
  {
    int modifierKeys1 = (int) Control.ModifierKeys;
    int modifierKeys2 = (int) Control.ModifierKeys;
    int modifierKeys3 = (int) Control.ModifierKeys;
    if (this.Mode == EditorMode.Normal)
    {
      if (this.m_bRotating)
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
        float num = (float) (((double) *(float*) ((IntPtr) this.m_pvMouse + 4L) - (double) *(float*) ((IntPtr) this.m_pvMousePoint + 4L)) * (Math.PI / 180.0));
        Mesh* pArrow = this.m_pArrow;
        if ((IntPtr) pArrow == *(long*) ((IntPtr) this.m_pXArrowMesh + 8L))
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ref d3DxvectoR3 = num;
        }
        else if ((IntPtr) pArrow == *(long*) ((IntPtr) this.m_pYArrowMesh + 8L))
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3 + 4) = num;
        }
        else if ((IntPtr) pArrow == *(long*) ((IntPtr) this.m_pZArrowMesh + 8L))
        {
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3 + 8) = num;
        }
        D3DXQUATERNION d3Dxquaternion;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        \u003CModule\u003E.D3DXQuaternionRotationYawPitchRoll(&d3Dxquaternion, ^(float&) ((IntPtr) &d3DxvectoR3 + 4), ^(float&) ref d3DxvectoR3, ^(float&) ((IntPtr) &d3DxvectoR3 + 8));
        D3DXMATRIX d3Dxmatrix;
        \u003CModule\u003E.D3DXMatrixRotationQuaternion(&d3Dxmatrix, &d3Dxquaternion);
        \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EAddRotation(this.m_pSelection, &d3Dxmatrix);
        // ISSUE: cpblk instruction
        __memcpy((IntPtr) this.m_pvMousePoint, (IntPtr) this.m_pvMouse, 8);
      }
      else if (this.m_bMoving)
      {
        this.UpdateMovePoint();
        D3DXVECTOR3* pvMoveDiff = this.m_pvMoveDiff;
        D3DXVECTOR3* pvMovePoint = this.m_pvMovePoint;
        float num1 = *(float*) ((IntPtr) pvMoveDiff + 8L) + *(float*) ((IntPtr) pvMovePoint + 8L);
        float num2 = *(float*) ((IntPtr) pvMoveDiff + 4L) + *(float*) ((IntPtr) pvMovePoint + 4L);
        D3DXVECTOR3 d3DxvectoR3_1;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ref d3DxvectoR3_1 = *(float*) pvMoveDiff + *(float*) pvMovePoint;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3DxvectoR3_1 + 4) = num2;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3DxvectoR3_1 + 8) = num1;
        if (this.m_BtnCfg.IsDown("ThingCollisions"))
        {
          int num3 = 0;
          if (0 < \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection))
          {
            do
            {
              IntPtr num4 = (IntPtr) \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetObjectAt(this.m_pSelection, num3) + 32L /*0x20*/;
              *(int*) num4 = *(int*) num4 | 256 /*0x0100*/;
              ++num3;
            }
            while (num3 < \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection));
          }
          PickData pickData;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(long&) ref pickData = 0L;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &pickData + 8) = float.MaxValue;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(int&) ((IntPtr) &pickData + 36) = -1;
          D3DXVECTOR3 d3DxvectoR3_2;
          \u003CModule\u003E.FableMod\u002EGfx\u002EAxisBox\u002EGetCenter((AxisBox*) this.m_pSelection, &d3DxvectoR3_2);
          Spatial* axes = (Spatial*) this.GetAxes();
          D3DXVECTOR3 d3DxvectoR3_3;
          // ISSUE: cpblk instruction
          __memcpy(ref d3DxvectoR3_3, (IntPtr) axes + 180L, 12);
          Mesh* pArrow = this.m_pArrow;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          if ((IntPtr) pArrow == *(long*) ((IntPtr) this.m_pZArrowMesh + 8L) && (double) ^(float&) ((IntPtr) &d3DxvectoR3_3 + 8) > (double) ^(float&) ((IntPtr) &d3DxvectoR3_1 + 8))
          {
            float num5 = \u003CModule\u003E.FableMod\u002EGfx\u002EAxisBox\u002EGetHeight((AxisBox*) this.m_pSelection) * 0.5f;
            D3DXVECTOR3 d3DxvectoR3_4;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3_4 + 8) = -num5;
            D3DXVECTOR3 d3DxvectoR3_5;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3_5 + 8) = ^(float&) ((IntPtr) &d3DxvectoR3_2 + 8) + ^(float&) ((IntPtr) &d3DxvectoR3_4 + 8);
            Node* root = this.GetRoot();
            D3DXVECTOR3 d3DxvectoR3_6;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ref d3DxvectoR3_6 = 0.0f;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3_6 + 4) = 0.0f;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &d3DxvectoR3_6 + 8) = -1f;
            Node* nodePtr = root;
            ref D3DXVECTOR3 local1 = ref d3DxvectoR3_2;
            ref D3DXVECTOR3 local2 = ref d3DxvectoR3_6;
            ref PickData local3 = ref pickData;
            // ISSUE: cast to a function pointer type
            // ISSUE: function pointer call
            __calli((__FnPtr<void (IntPtr, D3DXVECTOR3*, D3DXVECTOR3*, PickData*)>) *(long*) (*(long*) root + 64L /*0x40*/))((IntPtr) nodePtr, (D3DXVECTOR3*) ref local1, (D3DXVECTOR3*) ref local2, (PickData*) ref local3);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            if (^(long&) ref pickData != 0L && (double) ^(float&) ((IntPtr) &pickData + 20) >= (double) ^(float&) ((IntPtr) &d3DxvectoR3_5 + 8))
            {
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ((IntPtr) &d3DxvectoR3_1 + 8) = ^(float&) ((IntPtr) &pickData + 20) + num5;
            }
          }
          else if ((IntPtr) pArrow == *(long*) ((IntPtr) this.m_pXArrowMesh + 8L))
          {
            float num6 = \u003CModule\u003E.FableMod\u002EGfx\u002EAxisBox\u002EGetWidth((AxisBox*) this.m_pSelection) * 0.5f;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            if ((double) ^(float&) ref d3DxvectoR3_3 > (double) ^(float&) ref d3DxvectoR3_1)
            {
              D3DXVECTOR3 d3DxvectoR3_7;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ref d3DxvectoR3_7 = -num6;
              D3DXVECTOR3 d3DxvectoR3_8;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ref d3DxvectoR3_8 = ^(float&) ref d3DxvectoR3_7 + ^(float&) ref d3DxvectoR3_2;
              Node* root = this.GetRoot();
              D3DXVECTOR3 d3DxvectoR3_9;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ref d3DxvectoR3_9 = -1f;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ((IntPtr) &d3DxvectoR3_9 + 4) = 0.0f;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ((IntPtr) &d3DxvectoR3_9 + 8) = 0.0f;
              Node* nodePtr = root;
              ref D3DXVECTOR3 local4 = ref d3DxvectoR3_2;
              ref D3DXVECTOR3 local5 = ref d3DxvectoR3_9;
              ref PickData local6 = ref pickData;
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              __calli((__FnPtr<void (IntPtr, D3DXVECTOR3*, D3DXVECTOR3*, PickData*)>) *(long*) (*(long*) root + 64L /*0x40*/))((IntPtr) nodePtr, (D3DXVECTOR3*) ref local4, (D3DXVECTOR3*) ref local5, (PickData*) ref local6);
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              if (^(long&) ref pickData != 0L && (double) ^(float&) ((IntPtr) &pickData + 12) >= (double) ^(float&) ref d3DxvectoR3_8)
              {
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) ref d3DxvectoR3_1 = ^(float&) ((IntPtr) &pickData + 12) + num6;
              }
            }
            else
            {
              D3DXVECTOR3 d3DxvectoR3_10;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ref d3DxvectoR3_10 = ^(float&) ref d3DxvectoR3_2 + num6;
              Node* root = this.GetRoot();
              D3DXVECTOR3 d3DxvectoR3_11;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ref d3DxvectoR3_11 = 1f;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ((IntPtr) &d3DxvectoR3_11 + 4) = 0.0f;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ((IntPtr) &d3DxvectoR3_11 + 8) = 0.0f;
              Node* nodePtr = root;
              ref D3DXVECTOR3 local7 = ref d3DxvectoR3_2;
              ref D3DXVECTOR3 local8 = ref d3DxvectoR3_11;
              ref PickData local9 = ref pickData;
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              __calli((__FnPtr<void (IntPtr, D3DXVECTOR3*, D3DXVECTOR3*, PickData*)>) *(long*) (*(long*) root + 64L /*0x40*/))((IntPtr) nodePtr, (D3DXVECTOR3*) ref local7, (D3DXVECTOR3*) ref local8, (PickData*) ref local9);
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              if (^(long&) ref pickData != 0L && (double) ^(float&) ((IntPtr) &pickData + 12) <= (double) ^(float&) ref d3DxvectoR3_10)
              {
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) ref d3DxvectoR3_1 = ^(float&) ((IntPtr) &pickData + 12) - num6;
              }
            }
          }
          else if ((IntPtr) pArrow == *(long*) ((IntPtr) this.m_pYArrowMesh + 8L))
          {
            float num7 = \u003CModule\u003E.FableMod\u002EGfx\u002EAxisBox\u002EGetDepth((AxisBox*) this.m_pSelection) * 0.5f;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            if ((double) ^(float&) ((IntPtr) &d3DxvectoR3_3 + 4) > (double) ^(float&) ((IntPtr) &d3DxvectoR3_1 + 4))
            {
              D3DXVECTOR3 d3DxvectoR3_12;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ((IntPtr) &d3DxvectoR3_12 + 4) = -num7;
              D3DXVECTOR3 d3DxvectoR3_13;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ((IntPtr) &d3DxvectoR3_13 + 4) = ^(float&) ((IntPtr) &d3DxvectoR3_12 + 4) + ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4);
              Node* root = this.GetRoot();
              D3DXVECTOR3 d3DxvectoR3_14;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ref d3DxvectoR3_14 = 0.0f;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ((IntPtr) &d3DxvectoR3_14 + 4) = -1f;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ((IntPtr) &d3DxvectoR3_14 + 8) = 0.0f;
              Node* nodePtr = root;
              ref D3DXVECTOR3 local10 = ref d3DxvectoR3_2;
              ref D3DXVECTOR3 local11 = ref d3DxvectoR3_14;
              ref PickData local12 = ref pickData;
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              __calli((__FnPtr<void (IntPtr, D3DXVECTOR3*, D3DXVECTOR3*, PickData*)>) *(long*) (*(long*) root + 64L /*0x40*/))((IntPtr) nodePtr, (D3DXVECTOR3*) ref local10, (D3DXVECTOR3*) ref local11, (PickData*) ref local12);
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              if (^(long&) ref pickData != 0L && (double) ^(float&) ((IntPtr) &pickData + 16 /*0x10*/) >= (double) ^(float&) ((IntPtr) &d3DxvectoR3_13 + 4))
              {
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) ((IntPtr) &d3DxvectoR3_1 + 4) = ^(float&) ((IntPtr) &pickData + 16 /*0x10*/) + num7;
              }
            }
            else
            {
              D3DXVECTOR3 d3DxvectoR3_15;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ((IntPtr) &d3DxvectoR3_15 + 4) = ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4) + num7;
              Node* root = this.GetRoot();
              D3DXVECTOR3 d3DxvectoR3_16;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ref d3DxvectoR3_16 = 0.0f;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ((IntPtr) &d3DxvectoR3_16 + 4) = 1f;
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              ^(float&) ((IntPtr) &d3DxvectoR3_16 + 8) = 0.0f;
              Node* nodePtr = root;
              ref D3DXVECTOR3 local13 = ref d3DxvectoR3_2;
              ref D3DXVECTOR3 local14 = ref d3DxvectoR3_16;
              ref PickData local15 = ref pickData;
              // ISSUE: cast to a function pointer type
              // ISSUE: function pointer call
              __calli((__FnPtr<void (IntPtr, D3DXVECTOR3*, D3DXVECTOR3*, PickData*)>) *(long*) (*(long*) root + 64L /*0x40*/))((IntPtr) nodePtr, (D3DXVECTOR3*) ref local13, (D3DXVECTOR3*) ref local14, (PickData*) ref local15);
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              if (^(long&) ref pickData != 0L && (double) ^(float&) ((IntPtr) &pickData + 16 /*0x10*/) <= (double) ^(float&) ((IntPtr) &d3DxvectoR3_15 + 4))
              {
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                // ISSUE: cast to a reference type
                // ISSUE: explicit reference operation
                ^(float&) ((IntPtr) &d3DxvectoR3_1 + 4) = ^(float&) ((IntPtr) &pickData + 16 /*0x10*/) - num7;
              }
            }
          }
          int num8 = 0;
          if (0 < \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection))
          {
            do
            {
              IntPtr num9 = (IntPtr) \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetObjectAt(this.m_pSelection, num8) + 32L /*0x20*/;
              *(int*) num9 = *(int*) num9 & -257;
              ++num8;
            }
            while (num8 < \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection));
          }
        }
        \u003CModule\u003E.FableMod\u002EGfx\u002ESpatial\u002ESetWorldTranslation((Spatial*) this.GetAxes(), &d3DxvectoR3_1);
        \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002ESetCenter(this.m_pSelection, &d3DxvectoR3_1);
      }
      else if (this.AxesOn())
      {
        long num10 = *(long*) ((IntPtr) this.m_pXArrowMesh + 8L) + 320L;
        *(int*) num10 = *(int*) num10 & -2;
        long num11 = *(long*) ((IntPtr) this.m_pYArrowMesh + 8L) + 320L;
        *(int*) num11 = *(int*) num11 & -2;
        long num12 = *(long*) ((IntPtr) this.m_pZArrowMesh + 8L) + 320L;
        *(int*) num12 = *(int*) num12 & -2;
        Mesh* arrowAtMouse = this.GetArrowAtMouse();
        this.m_pArrow = arrowAtMouse;
        if ((IntPtr) arrowAtMouse != IntPtr.Zero)
        {
          D3DXVECTOR3* d3DxvectoR3Ptr1 = (D3DXVECTOR3*) ((IntPtr) this.GetAxes() + 180L);
          D3DXVECTOR3* d3DxvectoR3Ptr2 = (D3DXVECTOR3*) ((IntPtr) this.GetCamera() + 88L);
          float num13 = *(float*) ((IntPtr) d3DxvectoR3Ptr2 + 8L) - *(float*) ((IntPtr) d3DxvectoR3Ptr1 + 8L);
          float num14 = *(float*) ((IntPtr) d3DxvectoR3Ptr2 + 4L) - *(float*) ((IntPtr) d3DxvectoR3Ptr1 + 4L);
          D3DXVECTOR3 d3DxvectoR3;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ref d3DxvectoR3 = *(float*) d3DxvectoR3Ptr2 - *(float*) d3DxvectoR3Ptr1;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3 + 4) = num14;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          ^(float&) ((IntPtr) &d3DxvectoR3 + 8) = num13;
          \u003CModule\u003E.D3DXVec3Normalize(&d3DxvectoR3, &d3DxvectoR3);
          Mesh* pArrow = this.m_pArrow;
          // ISSUE: cast to a reference type
          // ISSUE: explicit reference operation
          if ((IntPtr) pArrow == *(long*) ((IntPtr) this.m_pXArrowMesh + 8L) && (double) \u003CModule\u003E.fabs(^(float&) ref d3DxvectoR3) < 0.95)
          {
            uint* numPtr = \u003CModule\u003E.FableMod\u002EGfx\u002EMesh\u002ERenderFlags(pArrow);
            int num15 = (int) *numPtr | 1;
            *numPtr = (uint) num15;
          }
          else
          {
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            if (pArrow == \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E\u002E\u002EPEAVMesh\u0040Gfx\u0040FableMod\u0040\u0040(this.m_pYArrowMesh) && (double) \u003CModule\u003E.fabs(^(float&) ((IntPtr) &d3DxvectoR3 + 4)) < 0.95)
            {
              uint* numPtr = \u003CModule\u003E.FableMod\u002EGfx\u002EMesh\u002ERenderFlags(pArrow);
              int num16 = (int) *numPtr | 1;
              *numPtr = (uint) num16;
            }
            else
            {
              // ISSUE: cast to a reference type
              // ISSUE: explicit reference operation
              if (pArrow == \u003CModule\u003E.FableMod\u002EGfx\u002ESmartPointer\u003CFableMod\u003A\u003AGfx\u003A\u003AMesh\u003E\u002E\u002EPEAVMesh\u0040Gfx\u0040FableMod\u0040\u0040(this.m_pZArrowMesh) && (double) \u003CModule\u003E.fabs(^(float&) ((IntPtr) &d3DxvectoR3 + 8)) < 0.95)
              {
                uint* numPtr = \u003CModule\u003E.FableMod\u002EGfx\u002EMesh\u002ERenderFlags(pArrow);
                int num17 = (int) *numPtr | 1;
                *numPtr = (uint) num17;
              }
            }
          }
        }
      }
    }
    return false;
  }

  protected unsafe void SelectThing([MarshalAs(UnmanagedType.U1)] bool add)
  {
    Mesh* meshAtMouse = this.GetMeshAtMouse();
    if ((IntPtr) meshAtMouse == IntPtr.Zero)
      return;
    Spatial* spatialPtr;
    if (*(int*) ((IntPtr) meshAtMouse + 272L) != 0)
    {
      spatialPtr = (Spatial*) meshAtMouse;
    }
    else
    {
      ulong pSpatial = (ulong) *(long*) ((IntPtr) meshAtMouse + 24L);
      if (pSpatial == 0UL)
        return;
      spatialPtr = \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002E\u003FA0x98271962\u002EGetIDObject((Spatial*) pSpatial);
    }
    if ((IntPtr) spatialPtr == IntPtr.Zero || this.ThingController.FindThing((uint) *(int*) ((IntPtr) spatialPtr + 272L)) == null)
      return;
    if (add && !\u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EIsEmpty(this.m_pSelection))
    {
      \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EAdd(this.m_pSelection, spatialPtr);
      this.SetSelectionHighlight(true);
      this.SetupAxes();
    }
    else
    {
      this.ClearSelection();
      \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EAdd(this.m_pSelection, spatialPtr);
      this.SetSelectionHighlight(true);
      this.SetupAxes();
    }
    this.CallThingSelected();
  }

  protected override void OnMouseDown(MouseEventArgs e) => base.OnMouseDown(e);

  protected override unsafe void OnMouseMove(MouseEventArgs e)
  {
    if (!this.IsReady())
      return;
    int modifierKeys = (int) Control.ModifierKeys;
    this.UpdateMouse(e.X, e.Y);
    if (this.Mode == EditorMode.Normal)
    {
      if (this.MouseMoveNormal(e))
        return;
    }
    else if (this.Mode == EditorMode.Player)
      \u003CModule\u003E.FableMod\u002EGfx\u002ECameraController\u002EOnMouseMove(this.m_pCameraController, this.m_pvMouse);
    else if (this.Mode == EditorMode.Navigation || this.Mode == EditorMode.Walk)
      this.m_iDrawState = e.Button != MouseButtons.Right ? (e.Button != MouseButtons.Left ? -1 : 1) : 2;
    base.OnMouseMove(e);
  }

  protected override void OnMouseUp(MouseEventArgs e) => base.OnMouseUp(e);

  protected override void OnMouseDoubleClick(MouseEventArgs e) => base.OnMouseDoubleClick(e);

  protected override unsafe void OnKeyUp(KeyEventArgs e)
  {
    if (e.KeyCode == Keys.Delete && !\u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EIsEmpty(this.m_pSelection))
    {
      if (MessageBox.Show("Are you sure?", "Delete selection", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Exclamation) != DialogResult.Yes)
        return;
      this.m_bRenderBlock = true;
      int num = 0;
      if (0 < \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection))
      {
        do
        {
          uint id = (uint) *(int*) ((IntPtr) \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetObjectAt(this.m_pSelection, num) + 272L);
          Thing thing = this.ThingController.FindThing(id);
          if (thing != null)
          {
            CTCBlock ctcBlock = thing.get_CTCs("CTCActionUseScriptedHook");
            if (ctcBlock != null)
            {
              Thing thingUid = this.ThingController.FindThingUID(ctcBlock.get_Variables("EntranceConnectedToUID").StringValue);
              if (thingUid != null && MessageBox.Show("Delete the teleporter marker?", "Delete Object", MessageBoxButtons.YesNo) == DialogResult.Yes)
                this.ThingController.RemoveThing(thingUid);
            }
            this.ThingController.RemoveThing(id);
          }
          ++num;
        }
        while (num < \u003CModule\u003E.FableMod\u002EGfx\u002ESelectionBox\u002EGetNumObjects(this.m_pSelection));
      }
      this.ClearSelection();
      this.m_bRenderBlock = false;
    }
    else if (e.KeyCode == Keys.Space)
    {
      if (this.Mode == EditorMode.Normal)
        this.Mode = EditorMode.Player;
      else if (this.Mode == EditorMode.Player)
        this.Mode = EditorMode.Normal;
    }
    base.OnKeyUp(e);
  }

  private void InitializeComponent() => this.AutoScaleMode = AutoScaleMode.Font;

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EGfxThingView();
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
