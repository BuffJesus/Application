// Decompiled with JetBrains decompiler
// Type: FableMod.STB.Flora
// Assembly: FableMod.STB, Version=1.0.4918.435, Culture=neutral, PublicKeyToken=null
// MVID: 2266CC66-C206-4DE0-80D4-5C3E14C1F606
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.STB.dll

using FableMod.CLRCore;
using FableMod.ContentManagement;
using System;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.STB;

public class Flora : IDisposable
{
  protected unsafe STBFloraHeader* m_pHeader;

  public unsafe Flora()
  {
    STBFloraHeader* stbFloraHeaderPtr1 = (STBFloraHeader*) \u003CModule\u003E.@new(64UL /*0x40*/);
    STBFloraHeader* stbFloraHeaderPtr2;
    if ((IntPtr) stbFloraHeaderPtr1 != IntPtr.Zero)
    {
      // ISSUE: initblk instruction
      __memset((IntPtr) stbFloraHeaderPtr1, 0, 64 /*0x40*/);
      stbFloraHeaderPtr2 = stbFloraHeaderPtr1;
    }
    else
      stbFloraHeaderPtr2 = (STBFloraHeader*) 0L;
    this.m_pHeader = stbFloraHeaderPtr2;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public unsafe void Read(FileStream file)
  {
    int num = (int) FileControl.Read(file, (void*) this.m_pHeader, 64U /*0x40*/);
    ContentObject entry1 = ContentManager.Instance.FindEntry(LinkDestination.ModelID, (object) (uint) *(int*) this.m_pHeader);
    Console.WriteLine(" gfx1: {0} {1}", (object) (uint) *(int*) this.m_pHeader, entry1 == null ? (object) "?" : (object) entry1.Name);
    ContentObject entry2 = ContentManager.Instance.FindEntry(LinkDestination.ModelID, (object) (uint) *(int*) ((IntPtr) this.m_pHeader + 4L));
    Console.WriteLine(" gfx2: {0} {1}", (object) (uint) *(int*) ((IntPtr) this.m_pHeader + 4L), entry2 == null ? (object) "?" : (object) entry2.Name);
    ContentObject entry3 = ContentManager.Instance.FindEntry(LinkDestination.ModelID, (object) (uint) *(int*) ((IntPtr) this.m_pHeader + 8L));
    Console.WriteLine(" gfx3: {0} {1}", (object) (uint) *(int*) ((IntPtr) this.m_pHeader + 8L), entry3 == null ? (object) "?" : (object) entry3.Name);
    Console.WriteLine(" x: {0}", (object) *(float*) ((IntPtr) this.m_pHeader + 12L));
    Console.WriteLine(" y: {0}", (object) *(float*) ((IntPtr) this.m_pHeader + 16L /*0x10*/));
    Console.WriteLine(" mx: {0}", (object) *(float*) ((IntPtr) this.m_pHeader + 40L));
    Console.WriteLine(" my: {0}", (object) *(float*) ((IntPtr) this.m_pHeader + 44L));
    Console.WriteLine(" rx: {0}", (object) *(float*) ((IntPtr) this.m_pHeader + 28L));
    Console.WriteLine(" ry: {0}", (object) *(float*) ((IntPtr) this.m_pHeader + 32L /*0x20*/));
    Console.WriteLine(" rz: {0}", (object) *(float*) ((IntPtr) this.m_pHeader + 36L));
    Console.WriteLine(" u1: {0}", (object) (uint) *(int*) ((IntPtr) this.m_pHeader + 20L));
    Console.WriteLine(" ns: {0}", (object) (uint) *(int*) ((IntPtr) this.m_pHeader + 48L /*0x30*/));
    Console.WriteLine(" type: {0}", (object) (uint) *(int*) ((IntPtr) this.m_pHeader + 52L));
    Console.WriteLine(" u2: {0}", (object) *(byte*) ((IntPtr) this.m_pHeader + 56L));
    Console.WriteLine(" u3: {0}", (object) *(byte*) ((IntPtr) this.m_pHeader + 57L));
    Console.WriteLine(" u4: {0}", (object) *(byte*) ((IntPtr) this.m_pHeader + 58L));
    Console.WriteLine(" u5: {0}", (object) *(byte*) ((IntPtr) this.m_pHeader + 59L));
    Console.WriteLine("");
  }

  private unsafe void \u007EFlora() => \u003CModule\u003E.delete((void*) this.m_pHeader);

  public unsafe uint get_GraphicsID(int index)
  {
    return (uint) *(int*) ((long) index * 4L + (IntPtr) this.m_pHeader);
  }

  public unsafe void set_GraphicsID(int index, uint value)
  {
    *(int*) ((long) index * 4L + (IntPtr) this.m_pHeader) = (int) value;
  }

  public unsafe float X
  {
    get => *(float*) ((IntPtr) this.m_pHeader + 28L);
    set => *(float*) ((IntPtr) this.m_pHeader + 28L) = value;
  }

  public unsafe float Y
  {
    get => *(float*) ((IntPtr) this.m_pHeader + 32L /*0x20*/);
    set => *(float*) ((IntPtr) this.m_pHeader + 32L /*0x20*/) = value;
  }

  protected virtual unsafe void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      \u003CModule\u003E.delete((void*) this.m_pHeader);
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
