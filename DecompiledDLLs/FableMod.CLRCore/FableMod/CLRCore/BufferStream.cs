// Decompiled with JetBrains decompiler
// Type: FableMod.CLRCore.BufferStream
// Assembly: FableMod.CLRCore, Version=1.0.4918.423, Culture=neutral, PublicKeyToken=null
// MVID: 9BFDF4CA-2166-4C71-B7DE-FD9072E9B599
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.CLRCore.dll

using System;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.CLRCore;

public class BufferStream : IDisposable
{
  public static int BUFFER_GROW = 4096 /*0x1000*/;
  protected unsafe byte* m_pucBuffer;
  protected int m_iIndex;
  protected int m_iLength;
  protected bool m_bNew;

  public unsafe BufferStream()
  {
    int bufferGrow = BufferStream.BUFFER_GROW;
    this.m_iLength = bufferGrow;
    this.m_pucBuffer = (byte*) \u003CModule\u003E.@new((ulong) bufferGrow);
    this.m_iIndex = 0;
    this.m_bNew = false;
  }

  public unsafe BufferStream(void* pBuffer, int iLength)
  {
    this.m_pucBuffer = (byte*) pBuffer;
    this.m_iIndex = 0;
    this.m_iLength = iLength;
    this.m_bNew = false;
  }

  public unsafe BufferStream(FileStream file)
  {
    int length = (int) file.Length;
    this.m_iLength = length;
    void* pBuffer = \u003CModule\u003E.@new((ulong) length);
    this.m_pucBuffer = (byte*) pBuffer;
    int num = (int) FileControl.Read(file, pBuffer, (uint) this.m_iLength);
    this.m_iIndex = 0;
    this.m_bNew = true;
  }

  private void \u007EBufferStream()
  {
    Console.WriteLine("BufferStream::Destructor");
    this.Close();
  }

  public unsafe void Close()
  {
    if (this.m_bNew)
    {
      byte* pucBuffer = this.m_pucBuffer;
      if ((IntPtr) pucBuffer != IntPtr.Zero)
        \u003CModule\u003E.delete((void*) pucBuffer);
    }
    this.m_pucBuffer = (byte*) 0L;
    this.m_iLength = 0;
    this.m_iIndex = -1;
  }

  public unsafe int Read(void* pData, int iCount)
  {
    int iIndex = this.m_iIndex;
    int num = this.m_iLength - iIndex;
    if (num < 0)
      return 0;
    iCount = iCount > num ? num : iCount;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) pData, (long) iIndex + (IntPtr) this.m_pucBuffer, (long) iCount);
    this.m_iIndex += iCount;
    return iCount;
  }

  public unsafe sbyte ReadChar()
  {
    int iIndex = this.m_iIndex;
    sbyte num = (sbyte) this.m_pucBuffer[(long) iIndex];
    this.m_iIndex = iIndex + 1;
    return num;
  }

  public unsafe uint ReadUInt32()
  {
    uint num;
    this.Read((void*) &num, 4);
    return num;
  }

  public unsafe sbyte* ReadZString()
  {
    byte* numPtr1 = (byte*) ((long) this.m_iIndex + (IntPtr) this.m_pucBuffer);
    byte* numPtr2 = numPtr1;
    if (*numPtr1 != (byte) 0)
    {
      do
      {
        ++numPtr2;
      }
      while (*numPtr2 != (byte) 0);
    }
    int iCount = (int) ((IntPtr) numPtr2 - (IntPtr) numPtr1) + 1;
    sbyte* pData = (sbyte*) \u003CModule\u003E.@new((ulong) iCount);
    this.Read((void*) pData, iCount);
    return pData;
  }

  public unsafe sbyte* ReadString()
  {
    uint iCount;
    this.Read((void*) &iCount, 4);
    sbyte* pData = (sbyte*) \u003CModule\u003E.@new((ulong) (iCount + 1U));
    this.Read((void*) pData, (int) iCount);
    *(sbyte*) ((long) iCount + (IntPtr) pData) = (sbyte) 0;
    return pData;
  }

  public unsafe string MReadZString()
  {
    sbyte* numPtr = this.ReadZString();
    string str = new string(numPtr);
    \u003CModule\u003E.delete((void*) numPtr);
    return str;
  }

  public unsafe string MReadString()
  {
    uint iCount;
    this.Read((void*) &iCount, 4);
    sbyte* pData = (sbyte*) \u003CModule\u003E.@new((ulong) (iCount + 1U));
    this.Read((void*) pData, (int) iCount);
    *(sbyte*) ((long) iCount + (IntPtr) pData) = (sbyte) 0;
    string str = new string(pData);
    \u003CModule\u003E.delete((void*) pData);
    return str;
  }

  public unsafe int Write(void* pData, int iCount)
  {
    if (this.m_iIndex + iCount >= this.m_iLength)
    {
      int num = this.m_iIndex + BufferStream.BUFFER_GROW + iCount;
      byte* numPtr = (byte*) \u003CModule\u003E.@new((ulong) num);
      // ISSUE: cpblk instruction
      __memcpy((IntPtr) numPtr, (IntPtr) this.m_pucBuffer, (long) this.m_iLength);
      \u003CModule\u003E.delete((void*) this.m_pucBuffer);
      this.m_pucBuffer = numPtr;
      this.m_iLength = num;
    }
    // ISSUE: cpblk instruction
    __memcpy((long) this.m_iIndex + (IntPtr) this.m_pucBuffer, (IntPtr) pData, (long) iCount);
    this.m_iIndex += iCount;
    return iCount;
  }

  public unsafe int WriteZString(sbyte* pszString)
  {
    sbyte* numPtr = pszString;
    if (*pszString != (sbyte) 0)
    {
      do
      {
        ++numPtr;
      }
      while (*numPtr != (sbyte) 0);
    }
    long num = (long) ((IntPtr) numPtr - (IntPtr) pszString);
    return this.Write((void*) pszString, (int) (num + 1L));
  }

  public unsafe int WriteUInt32(uint uiValue) => this.Write((void*) &uiValue, 4);

  public unsafe int WriteFloat(float fValue) => this.Write((void*) &fValue, 4);

  public void Seek(int iOffset) => this.m_iIndex = iOffset;

  public void Ignore(int iCount) => this.m_iIndex += iCount;

  public int Tell() => this.m_iIndex;

  [return: MarshalAs(UnmanagedType.U1)]
  public unsafe bool IsOpen() => (IntPtr) this.m_pucBuffer != 0L;

  public int GetSize() => this.m_iLength;

  public int GetWritten() => this.m_iIndex;

  public unsafe byte* GetData() => this.m_pucBuffer;

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      Console.WriteLine("BufferStream::Destructor");
      this.Close();
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
