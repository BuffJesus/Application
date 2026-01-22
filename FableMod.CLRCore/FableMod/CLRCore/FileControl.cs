// Decompiled with JetBrains decompiler
// Type: FableMod.CLRCore.FileControl
// Assembly: FableMod.CLRCore, Version=1.0.4918.423, Culture=neutral, PublicKeyToken=null
// MVID: 9BFDF4CA-2166-4C71-B7DE-FD9072E9B599
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.CLRCore.dll

using System.IO;

#nullable disable
namespace FableMod.CLRCore;

public class FileControl
{
  public static unsafe uint Read(FileStream File, void* pBuffer, uint uiCount)
  {
    uint num = 0;
    \u003CModule\u003E.ReadFile((void*) File.Handle, pBuffer, uiCount, &num, (_OVERLAPPED*) 0L);
    return num;
  }

  public static unsafe uint Write(FileStream File, void* pBuffer, uint uiCount)
  {
    uint num = 0;
    \u003CModule\u003E.WriteFile((void*) File.Handle, pBuffer, uiCount, &num, (_OVERLAPPED*) 0L);
    _LARGE_INTEGER largeInteger;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(long&) ref largeInteger = (long) -num;
    \u003CModule\u003E.SetFilePointerEx((void*) File.Handle, largeInteger, (_LARGE_INTEGER*) 0L, 1U);
    File.Position += (long) num;
    return num;
  }

  public static void WriteNull(FileStream File, int count)
  {
    if (0 >= count)
      return;
    int num = count;
    do
    {
      File.WriteByte((byte) 0);
      num += -1;
    }
    while (num != 0);
  }
}
