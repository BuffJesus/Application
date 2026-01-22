// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.GfxView
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Gfx.Integration;

public class GfxView : UserControl
{
  protected unsafe Camera* m_pCamera;
  protected unsafe D3DXVECTOR3* m_pvWorld;
  protected unsafe D3DXVECTOR2* m_pvMouse;
  protected CameraDirection m_SetCamera;
  protected unsafe CameraController* m_pCameraController;
  protected ButtonConfiguration m_BtnCfg;
  protected bool m_bRenderBlock;
  private Timer timerCameraUpdate;
  protected GfxController m_Controller;
  private IContainer components;
  private bool m_bCameraInitialized;
  private bool m_bInitialized;
  private bool m_bActive;
  private bool m_bErrors;

  public GfxView()
  {
    // ISSUE: fault handler
    try
    {
      this.InitializeComponent();
      this.m_bInitialized = false;
      this.m_bCameraInitialized = false;
      this.m_bActive = false;
      this.m_bErrors = false;
      Application.Idle += new EventHandler(this.GfxView_OnIdle);
      this.m_BtnCfg = new ButtonConfiguration();
      this.DefaultButtonConfiguration();
      try
      {
        this.m_BtnCfg.Load(AppDomain.CurrentDomain.BaseDirectory + "FableMod.Buttons.XML");
      }
      catch (System.Exception ex)
      {
      }
      this.m_BtnCfg.Attach((Control) this);
      this.m_BtnCfg.OnButtonDown += new ButtonEventHandler(this.BtnCfg_OnButtonDown);
      this.m_BtnCfg.OnButtonUp += new ButtonEventHandler(this.BtnCfg_OnButtonUp);
      this.m_bRenderBlock = false;
    }
    __fault
    {
      base.Dispose(true);
    }
  }

  public void SaveScreenshot()
  {
    SaveFileDialog saveFileDialog = new SaveFileDialog();
    saveFileDialog.Filter = "JPG Files (*.jpg)|*.jpg|PNG Files (*.png)|*.png||";
    saveFileDialog.DefaultExt = "jpg";
    if (saveFileDialog.ShowDialog() != DialogResult.OK)
      return;
    try
    {
      GfxManager.SaveScreenToFile(saveFileDialog.FileName);
    }
    catch (System.Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, ex.Message);
    }
  }

  public virtual void OnResetObjects()
  {
  }

  public unsafe void Render()
  {
    // ISSUE: untyped stack allocation
    long num1 = (long) __untypedstackalloc(\u003CModule\u003E.__CxxQueryExceptionSize());
    if (!this.m_bInitialized || this.m_bRenderBlock || this.m_bErrors || !this.IsActive())
      return;
    Device* mod1PeavDevice23Ea = \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA;
    switch (\u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002ECheckState(mod1PeavDevice23Ea))
    {
      case (Device.EDeviceState) -2:
      case (Device.EDeviceState) -1:
        \u003CModule\u003E.Sleep(200U);
        return;
      case (Device.EDeviceState) 1:
        if (!\u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EReset(mod1PeavDevice23Ea))
        {
          this.m_bErrors = true;
          int num2 = (int) MessageBox.Show("Failed to reset Direct3D device.");
          return;
        }
        break;
    }
    \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002ELock(mod1PeavDevice23Ea);
    FableMod.Gfx.Exception* exceptionPtr;
    try
    {
      \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EClearBuffers(mod1PeavDevice23Ea, 4288716960U);
      Node* root = this.GetRoot();
      if (this.m_bInitialized)
      {
        if ((IntPtr) root != IntPtr.Zero)
        {
          Node* nodePtr = root;
          // ISSUE: cast to a function pointer type
          // ISSUE: function pointer call
          __calli((__FnPtr<void (IntPtr, byte)>) *(long*) (*(long*) root + 16L /*0x10*/))((IntPtr) nodePtr, (byte) 1);
          if (this.m_SetCamera != CameraDirection.None)
          {
            if (!this.m_bCameraInitialized)
            {
              uint num3 = (uint) *(int*) ((IntPtr) mod1PeavDevice23Ea + 164L);
              \u003CModule\u003E.FableMod\u002EGfx\u002ECameraController\u002ESetup(this.m_pCameraController, this.m_pCamera, *(int*) ((IntPtr) mod1PeavDevice23Ea + 160L /*0xA0*/), (int) num3, false);
            }
            this.m_bCameraInitialized = true;
            GfxView gfxView = this;
            gfxView.SetCamera(gfxView.m_SetCamera);
            this.m_SetCamera = CameraDirection.None;
          }
          \u003CModule\u003E.FableMod\u002EGfx\u002ECamera\u002EUpdate(this.m_pCamera);
          this.OnRender();
          \u003CModule\u003E.FableMod\u002EGfx\u002ECamera\u002ERenderScene(this.m_pCamera, (Spatial*) root);
        }
        this.RenderInterface();
      }
      IntPtr handle = this.Handle;
      \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EPresent(mod1PeavDevice23Ea, (HWND__*) handle.ToPointer(), (tagRECT*) 0L, (tagRECT*) 0L);
    }
    catch (System.Exception ex1) when (\u003CModule\u003E.__CxxExceptionFilter((void*) Marshal.GetExceptionPointers(), (void*) &\u003CModule\u003E.\u003F\u003F_R0\u003FAVException\u0040Gfx\u0040FableMod\u0040\u0040\u00408, 9, (void*) &exceptionPtr) != 0)
    {
      uint num4 = 0;
      \u003CModule\u003E.__CxxRegisterExceptionObject((void*) Marshal.GetExceptionPointers(), (void*) num1);
      try
      {
        try
        {
          this.m_bErrors = true;
          int num5 = (int) MessageBox.Show($"Graphics Exception: {new string(\u003CModule\u003E.FableMod\u002EGfx\u002EException\u002EGetMsg(exceptionPtr))}.\r\nRendering stopped.");
          goto label_24;
        }
        catch (System.Exception ex2) when (
        {
          // ISSUE: unable to correctly present filter
          num4 = (uint) \u003CModule\u003E.__CxxDetectRethrow((void*) Marshal.GetExceptionPointers());
          if (num4 != 0U)
          {
            SuccessfulFiltering;
          }
          else
            throw;
        }
        )
        {
        }
        if (num4 != 0U)
          throw;
      }
      finally
      {
        \u003CModule\u003E.__CxxUnregisterExceptionObject((void*) num1, (int) num4);
      }
    }
    catch (System.Exception ex)
    {
      this.m_bErrors = true;
      int num6 = (int) MessageBox.Show($"Exception: {ex.ToString()}.\r\nRendering stopped.");
    }
label_24:
    \u003CModule\u003E.FableMod\u002EGfx\u002EDevice\u002EUnlock(mod1PeavDevice23Ea);
  }

  public void TopCamera() => this.m_SetCamera = CameraDirection.Top;

  public void RightCamera() => this.m_SetCamera = CameraDirection.Right;

  public void FrontCamera() => this.m_SetCamera = CameraDirection.Front;

  public void IsometricCamera() => this.m_SetCamera = CameraDirection.Isometric;

  public unsafe void Activate([MarshalAs(UnmanagedType.U1)] bool activate)
  {
    bool bActive = this.m_bActive;
    this.m_bActive = activate;
    if (bActive == activate || !activate)
      return;
    Camera* pCamera = this.m_pCamera;
    if ((IntPtr) pCamera == IntPtr.Zero)
      return;
    \u003CModule\u003E.FableMod\u002EGfx\u002ECamera\u002ERefresh(pCamera);
    this.Render();
  }

  public unsafe void CameraFocus(Spatial* pSpatial, CameraDirection camDirection)
  {
    if (!this.m_bCameraInitialized)
      return;
    long num1 = *(long*) ((IntPtr) pSpatial + 264L);
    float num2 = *(float*) (num1 + 20L);
    BoundSphere* boundSpherePtr = (BoundSphere*) num1;
    Camera* pCamera1 = this.m_pCamera;
    *(int*) ((IntPtr) pCamera1 + 8L) = *(int*) ((IntPtr) pCamera1 + 8L) | 6;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) pCamera1 + 76L, (IntPtr) boundSpherePtr + 8L, 12);
    Camera* pCamera2 = this.m_pCamera;
    float num3 = \u003CModule\u003E.tanf((float) ((double) *(float*) ((IntPtr) pCamera2 + 380L) * (Math.PI / 180.0) * 0.5));
    float num4 = num2 / num3;
    switch (camDirection)
    {
      case CameraDirection.Front:
        D3DXVECTOR3 d3DxvectoR3_1;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ref d3DxvectoR3_1 = *(float*) ((IntPtr) pCamera2 + 76L) + 0.0f;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3DxvectoR3_1 + 4) = *(float*) ((IntPtr) pCamera2 + 76L + 4L) + num4;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3DxvectoR3_1 + 8) = *(float*) ((IntPtr) pCamera2 + 76L + 8L) + 0.0f;
        Camera* cameraPtr1 = pCamera2;
        *(int*) ((IntPtr) cameraPtr1 + 8L) = *(int*) ((IntPtr) cameraPtr1 + 8L) | 6;
        // ISSUE: cpblk instruction
        __memcpy((IntPtr) cameraPtr1 + 88L, ref d3DxvectoR3_1, 12);
        break;
      case CameraDirection.Top:
        D3DXVECTOR3 d3DxvectoR3_2;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ref d3DxvectoR3_2 = *(float*) ((IntPtr) pCamera2 + 76L) + 0.0f;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3DxvectoR3_2 + 4) = *(float*) ((IntPtr) pCamera2 + 76L + 4L) + 1f;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3DxvectoR3_2 + 8) = *(float*) ((IntPtr) pCamera2 + 76L + 8L) + num4;
        Camera* cameraPtr2 = pCamera2;
        *(int*) ((IntPtr) cameraPtr2 + 8L) = *(int*) ((IntPtr) cameraPtr2 + 8L) | 6;
        // ISSUE: cpblk instruction
        __memcpy((IntPtr) cameraPtr2 + 88L, ref d3DxvectoR3_2, 12);
        break;
      case CameraDirection.Right:
        D3DXVECTOR3 d3DxvectoR3_3;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ref d3DxvectoR3_3 = -num4;
        D3DXVECTOR3 d3DxvectoR3_4;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ref d3DxvectoR3_4 = ^(float&) ref d3DxvectoR3_3 + *(float*) ((IntPtr) pCamera2 + 76L);
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3DxvectoR3_4 + 4) = *(float*) ((IntPtr) pCamera2 + 76L + 4L) + 0.0f;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3DxvectoR3_4 + 8) = *(float*) ((IntPtr) pCamera2 + 76L + 8L) + 0.0f;
        Camera* cameraPtr3 = pCamera2;
        *(int*) ((IntPtr) cameraPtr3 + 8L) = *(int*) ((IntPtr) cameraPtr3 + 8L) | 6;
        // ISSUE: cpblk instruction
        __memcpy((IntPtr) cameraPtr3 + 88L, ref d3DxvectoR3_4, 12);
        break;
      case CameraDirection.Isometric:
        float num5 = num4 * 0.5f;
        D3DXVECTOR3 d3DxvectoR3_5;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ref d3DxvectoR3_5 = *(float*) ((IntPtr) pCamera2 + 76L) + num5;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3DxvectoR3_5 + 4) = *(float*) ((IntPtr) pCamera2 + 76L + 4L) + num5;
        // ISSUE: cast to a reference type
        // ISSUE: explicit reference operation
        ^(float&) ((IntPtr) &d3DxvectoR3_5 + 8) = *(float*) ((IntPtr) pCamera2 + 76L + 8L) + num5;
        Camera* cameraPtr4 = pCamera2;
        *(int*) ((IntPtr) cameraPtr4 + 8L) = *(int*) ((IntPtr) cameraPtr4 + 8L) | 6;
        // ISSUE: cpblk instruction
        __memcpy((IntPtr) cameraPtr4 + 88L, ref d3DxvectoR3_5, 12);
        break;
    }
  }

  public virtual unsafe void Destroy()
  {
    this.m_bInitialized = false;
    this.m_bCameraInitialized = false;
    CameraController* cameraController = this.m_pCameraController;
    if ((IntPtr) cameraController != IntPtr.Zero)
    {
      CameraController* cameraControllerPtr1 = cameraController;
      CameraController* cameraControllerPtr2 = cameraControllerPtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) cameraControllerPtr1)((IntPtr) cameraControllerPtr2, 1U);
      this.m_pCameraController = (CameraController*) 0L;
    }
    Camera* pCamera = this.m_pCamera;
    if ((IntPtr) pCamera != IntPtr.Zero)
    {
      Camera* cameraPtr1 = pCamera;
      Camera* cameraPtr2 = cameraPtr1;
      // ISSUE: cast to a function pointer type
      // ISSUE: function pointer call
      void* voidPtr = __calli((__FnPtr<void* (IntPtr, uint)>) *(long*) *(long*) cameraPtr1)((IntPtr) cameraPtr2, 1U);
      this.m_pCamera = (Camera*) 0L;
    }
    D3DXVECTOR3* pvWorld = this.m_pvWorld;
    if ((IntPtr) pvWorld != IntPtr.Zero)
    {
      \u003CModule\u003E.delete((void*) pvWorld);
      this.m_pvWorld = (D3DXVECTOR3*) 0L;
    }
    D3DXVECTOR2* pvMouse = this.m_pvMouse;
    if ((IntPtr) pvMouse == IntPtr.Zero)
      return;
    \u003CModule\u003E.delete((void*) pvMouse);
    this.m_pvMouse = (D3DXVECTOR2*) 0L;
  }

  public GfxController Controller
  {
    get => this.m_Controller;
    set => this.m_Controller = value;
  }

  public unsafe float DrawDistance
  {
    get => *(float*) ((IntPtr) this.m_pCamera + 376L);
    set
    {
      Camera* pCamera = this.m_pCamera;
      *(float*) ((IntPtr) pCamera + 376L) = value;
      *(int*) ((IntPtr) pCamera + 8L) = *(int*) ((IntPtr) pCamera + 8L) | 50;
      this.Render();
    }
  }

  internal virtual unsafe void Initialize()
  {
    if (this.m_bInitialized)
      return;
    Camera* cameraPtr1 = (Camera*) \u003CModule\u003E.@new(536UL);
    Camera* cameraPtr2;
    // ISSUE: fault handler
    try
    {
      cameraPtr2 = (IntPtr) cameraPtr1 == IntPtr.Zero ? (Camera*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002ECamera\u002E\u007Bctor\u007D(cameraPtr1);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) cameraPtr1);
    }
    this.m_pCamera = cameraPtr2;
    CameraController* cameraControllerPtr1 = (CameraController*) \u003CModule\u003E.@new(136UL);
    CameraController* cameraControllerPtr2;
    // ISSUE: fault handler
    try
    {
      cameraControllerPtr2 = (IntPtr) cameraControllerPtr1 == IntPtr.Zero ? (CameraController*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002ECameraController\u002E\u007Bctor\u007D(cameraControllerPtr1);
    }
    __fault
    {
      \u003CModule\u003E.delete((void*) cameraControllerPtr1);
    }
    this.m_pCameraController = cameraControllerPtr2;
    uint num = (uint) *(int*) ((IntPtr) \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA + 164L);
    \u003CModule\u003E.FableMod\u002EGfx\u002ECamera\u002ESetViewport(this.m_pCamera, 0, 0, *(int*) ((IntPtr) \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA + 160L /*0xA0*/), (int) num);
    Camera* pCamera = this.m_pCamera;
    *(float*) ((IntPtr) pCamera + 380L) = 35f;
    *(int*) ((IntPtr) pCamera + 8L) = *(int*) ((IntPtr) pCamera + 8L) | 18;
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
    this.m_pvWorld = d3DxvectoR3Ptr2;
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
    this.m_pvMouse = d3DxvectoR2Ptr2;
    this.m_bInitialized = true;
    this.m_SetCamera = CameraDirection.None;
  }

  private void \u007EGfxView() => this.components?.Dispose();

  protected unsafe Node* GetRoot() => this.m_Controller.GetRoot();

  protected unsafe Camera* GetCamera() => this.m_pCamera;

  protected virtual void RenderInterface()
  {
  }

  protected virtual unsafe void CameraHeartbeat()
  {
    \u003CModule\u003E.FableMod\u002EGfx\u002ECameraController\u002EUpdate(this.m_pCameraController);
  }

  protected virtual void DefaultButtonConfiguration()
  {
    this.m_BtnCfg.Add("CameraOrbit", Buttons.MButton);
    this.m_BtnCfg.Add("CameraMove", Buttons.MButton | Buttons.Shift);
    this.m_BtnCfg.Add("CameraLook", Buttons.MButton | Buttons.Ctrl);
    this.m_BtnCfg.Add("ViewTop", Buttons.NumPad7);
    this.m_BtnCfg.Add("ViewFront", Buttons.NumPad1);
    this.m_BtnCfg.Add("ViewSide", Buttons.NumPad3);
  }

  protected virtual void OnRender()
  {
  }

  protected unsafe void SetCamera(CameraDirection camDirection)
  {
    GfxView gfxView = this;
    gfxView.CameraFocus((Spatial*) gfxView.GetRoot(), camDirection);
  }

  protected unsafe void UpdateMouse(int x, int y)
  {
    if (!this.IsReady())
      return;
    Device* mod1PeavDevice23Ea = \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA;
    int num1 = *(int*) ((IntPtr) mod1PeavDevice23Ea + 160L /*0xA0*/);
    int num2 = *(int*) ((IntPtr) mod1PeavDevice23Ea + 164L);
    Size clientSize1 = this.ClientSize;
    *(float*) this.m_pvMouse = (float) num1 / (float) clientSize1.Width * (float) x;
    Size clientSize2 = this.ClientSize;
    *(float*) ((IntPtr) this.m_pvMouse + 4L) = (float) num2 / (float) clientSize2.Height * (float) y;
    \u003CModule\u003E.FableMod\u002EGfx\u002ECamera\u002EScreenToWorld(this.m_pCamera, this.m_pvMouse, 0.0f, this.m_pvWorld);
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected unsafe bool IsReady()
  {
    return this.m_bInitialized && !this.m_bErrors && *(byte*) ((IntPtr) \u003CModule\u003E.\u003Fm_pDevice\u0040Manager\u0040Gfx\u0040FableMod\u0040\u00401PEAVDevice\u004023\u0040EA + 168L) != (byte) 0;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected bool IsActive() => this.m_bActive;

  protected virtual unsafe void BtnCfg_OnButtonDown(string button, MouseEventArgs e)
  {
    if (!this.IsReady() || e == null)
      return;
    if (!this.Focused)
      this.Focus();
    this.UpdateMouse(e.X, e.Y);
    switch (button)
    {
      case "CameraMove":
        \u003CModule\u003E.FableMod\u002EGfx\u002ECameraController\u002EOnMoveDown(this.m_pCameraController, this.m_pvMouse);
        break;
      case "CameraOrbit":
        \u003CModule\u003E.FableMod\u002EGfx\u002ECameraController\u002EOnRotateDown(this.m_pCameraController, this.m_pvMouse);
        break;
      case "CameraLook":
        \u003CModule\u003E.FableMod\u002EGfx\u002ECameraController\u002EOnLookDown(this.m_pCameraController, this.m_pvMouse);
        break;
    }
  }

  protected virtual unsafe void BtnCfg_OnButtonUp(string button, MouseEventArgs e)
  {
    if (!this.IsReady())
      return;
    if (button.StartsWith("Camera"))
    {
      \u003CModule\u003E.FableMod\u002EGfx\u002ECameraController\u002EOnUp(this.m_pCameraController);
    }
    else
    {
      switch (button)
      {
        case "ViewFront":
          this.FrontCamera();
          break;
        case "ViewTop":
          this.TopCamera();
          break;
        case "ViewSide":
          this.RightCamera();
          break;
      }
    }
  }

  protected override void OnMouseDown(MouseEventArgs e) => base.OnMouseDown(e);

  protected override unsafe void OnMouseMove(MouseEventArgs e)
  {
    if (!this.IsReady())
      return;
    this.UpdateMouse(e.X, e.Y);
    \u003CModule\u003E.FableMod\u002EGfx\u002ECameraController\u002EOnMouseMove(this.m_pCameraController, this.m_pvMouse);
    base.OnMouseMove(e);
  }

  protected override void OnMouseUp(MouseEventArgs e) => base.OnMouseUp(e);

  private void InitializeComponent()
  {
    System.ComponentModel.Container container = new System.ComponentModel.Container();
    this.components = (IContainer) container;
    this.timerCameraUpdate = new Timer((IContainer) container);
    this.SuspendLayout();
    this.timerCameraUpdate.Enabled = true;
    this.timerCameraUpdate.Interval = 30;
    this.timerCameraUpdate.Tick += new EventHandler(this.timerCameraUpdate_Tick);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Name = nameof (GfxView);
    this.Size = new Size(442, 315);
    GfxView gfxView1 = this;
    gfxView1.MouseWheel += new MouseEventHandler(gfxView1.GfxView_MouseWheel);
    GfxView gfxView2 = this;
    gfxView2.Paint += new PaintEventHandler(gfxView2.GfxView_Paint);
    this.ResumeLayout(false);
  }

  private unsafe void GfxView_MouseWheel(object sender, MouseEventArgs e)
  {
    if (!this.IsReady())
      return;
    \u003CModule\u003E.FableMod\u002EGfx\u002ECameraController\u002EOnZoom(this.m_pCameraController, (float) e.Delta / 120f);
  }

  private void GfxView_Paint(object sender, PaintEventArgs e)
  {
    if (this.Focused)
      return;
    this.Render();
  }

  private void GfxView_OnIdle(object sender, EventArgs e)
  {
    if (!this.Focused)
      return;
    this.Render();
  }

  private void timerCameraUpdate_Tick(object sender, EventArgs e)
  {
    if (!this.Focused || !this.m_bCameraInitialized)
      return;
    this.CameraHeartbeat();
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EGfxView();
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
