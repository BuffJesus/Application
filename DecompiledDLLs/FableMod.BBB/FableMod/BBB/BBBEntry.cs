// Decompiled with JetBrains decompiler
// Type: FableMod.BBB.BBBEntry
// Assembly: FableMod.BBB, Version=1.0.4918.427, Culture=neutral, PublicKeyToken=null
// MVID: E6F7EB8A-26AC-4E0A-8433-40351F83A480
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.BBB.dll

using FableMod.CLRCore;
using System;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.BBB;

public class BBBEntry : IDisposable
{
  protected string m_FileName;
  protected string m_DevFileName;
  protected uint m_HeaderOffset;
  protected unsafe BBBFileHeaderA* m_Header;

  public unsafe BBBEntry()
  {
    BBBFileHeaderA* bbbFileHeaderAPtr1 = (BBBFileHeaderA*) \u003CModule\u003E.@new(28UL);
    BBBFileHeaderA* bbbFileHeaderAPtr2;
    if ((IntPtr) bbbFileHeaderAPtr1 != IntPtr.Zero)
    {
      // ISSUE: initblk instruction
      __memset((IntPtr) bbbFileHeaderAPtr1, 0, 28);
      bbbFileHeaderAPtr2 = bbbFileHeaderAPtr1;
    }
    else
      bbbFileHeaderAPtr2 = (BBBFileHeaderA*) 0L;
    this.m_Header = bbbFileHeaderAPtr2;
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  private unsafe void \u007EBBBEntry()
  {
    \u003CModule\u003E.delete((void*) this.m_Header);
    this.m_FileName = (string) null;
    this.m_DevFileName = (string) null;
  }

  public unsafe void ReadHeader(FileStream file)
  {
    this.m_HeaderOffset = (uint) (int) file.Position;
    int num1 = (int) FileControl.Read(file, (void*) this.m_Header, 28U);
    sbyte* pBuffer1 = (sbyte*) \u003CModule\u003E.@new((ulong) (uint) (*(int*) ((IntPtr) this.m_Header + 24L) + 1));
    int num2 = (int) FileControl.Read(file, (void*) pBuffer1, (uint) *(int*) ((IntPtr) this.m_Header + 24L));
    *(sbyte*) ((long) (uint) *(int*) ((IntPtr) this.m_Header + 24L) + (IntPtr) pBuffer1) = (sbyte) 0;
    this.m_FileName = new string(pBuffer1);
    \u003CModule\u003E.delete((void*) pBuffer1);
    uint num3;
    int num4 = (int) FileControl.Read(file, (void*) &num3, 4U);
    uint num5;
    int num6 = (int) FileControl.Read(file, (void*) &num5, 4U);
    uint uiCount;
    int num7 = (int) FileControl.Read(file, (void*) &uiCount, 4U);
    sbyte* pBuffer2 = (sbyte*) \u003CModule\u003E.@new((ulong) (uiCount + 1U));
    int num8 = (int) FileControl.Read(file, (void*) pBuffer2, uiCount);
    *(sbyte*) ((long) uiCount + (IntPtr) pBuffer2) = (sbyte) 0;
    this.m_DevFileName = new string(pBuffer2);
    \u003CModule\u003E.delete((void*) pBuffer2);
    uint size;
    int num9 = (int) FileControl.Read(file, (void*) &size, 4U);
    this.ReadHeaderExtra(file, size);
  }

  public string FileName => this.m_FileName;

  public unsafe uint FileOffset => (uint) *(int*) ((IntPtr) this.m_Header + 16L /*0x10*/);

  public unsafe uint FileSize => (uint) *(int*) ((IntPtr) this.m_Header + 12L);

  protected virtual void ReadHeaderExtra(FileStream file, uint size)
  {
    file.Position += (long) size;
  }

  protected virtual unsafe void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      \u003CModule\u003E.delete((void*) this.m_Header);
      this.m_FileName = (string) null;
      this.m_DevFileName = (string) null;
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
