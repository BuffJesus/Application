// Decompiled with JetBrains decompiler
// Type: FableMod.Data.ZLib
// Assembly: FableMod.Data, Version=1.0.4918.427, Culture=neutral, PublicKeyToken=null
// MVID: 4E10122A-6FC9-49D4-9087-7FA415462296
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Data.dll

using System;

#nullable disable
namespace FableMod.Data;

public class ZLib
{
  public static unsafe int Compress(void* dest, int maxCount, void* src, int count, int level)
  {
    uint num = (uint) maxCount;
    return \u003CModule\u003E.compress2((byte*) dest, &num, (byte*) src, (uint) count, level) != 0 ? -1 : (int) num;
  }

  public static unsafe byte[] Compress(byte[] data, int offset, int count, int strength)
  {
    fixed (byte* numPtr1 = &data[offset])
    {
      uint length = (uint) (count * 2);
      byte* numPtr2 = (byte*) \u003CModule\u003E.@new((ulong) length);
      if (\u003CModule\u003E.compress2(numPtr2, &length, numPtr1, (uint) count, strength) != 0)
        throw new InvalidOperationException();
      byte[] numArray = new byte[(int) length];
      int index = 0;
      long num1 = 0;
      long num2 = (long) (int) length;
      if (0L < num2)
      {
        do
        {
          numArray[index] = *(byte*) (num1 + (IntPtr) numPtr2);
          ++index;
          ++num1;
        }
        while (num1 < num2);
      }
      \u003CModule\u003E.delete((void*) numPtr2);
      return numArray;
    }
  }

  public static unsafe byte[] Compress(byte[] data, int offset, int count)
  {
    fixed (byte* numPtr1 = &data[offset])
    {
      uint length = (uint) (count * 2);
      byte* numPtr2 = (byte*) \u003CModule\u003E.@new((ulong) length);
      if (\u003CModule\u003E.compress(numPtr2, &length, numPtr1, (uint) count) != 0)
        throw new InvalidOperationException();
      byte[] numArray = new byte[(int) length];
      int index = 0;
      long num1 = 0;
      long num2 = (long) (int) length;
      if (0L < num2)
      {
        do
        {
          numArray[index] = *(byte*) (num1 + (IntPtr) numPtr2);
          ++index;
          ++num1;
        }
        while (num1 < num2);
      }
      \u003CModule\u003E.delete((void*) numPtr2);
      return numArray;
    }
  }

  public static unsafe int Uncompress(void* dest, int maxCount, void* src, int count)
  {
    uint num = (uint) maxCount;
    return \u003CModule\u003E.uncompress((byte*) dest, &num, (byte*) src, (uint) count) != 0 ? -1 : (int) num;
  }

  public static unsafe byte[] Uncompress(byte[] data, int offset, int count, int realSize)
  {
    fixed (byte* numPtr1 = &data[offset])
    {
      byte* numPtr2 = (byte*) \u003CModule\u003E.@new((ulong) realSize);
      uint length = (uint) realSize;
      if (\u003CModule\u003E.uncompress(numPtr2, &length, numPtr1, (uint) count) != 0)
        throw new InvalidOperationException();
      byte[] numArray = new byte[(int) length];
      int index = 0;
      long num1 = 0;
      long num2 = (long) (int) length;
      if (0L < num2)
      {
        do
        {
          numArray[index] = *(byte*) (num1 + (IntPtr) numPtr2);
          ++index;
          ++num1;
        }
        while (num1 < num2);
      }
      \u003CModule\u003E.delete((void*) numPtr2);
      return numArray;
    }
  }

  public static unsafe uint CRC32(uint start, void* src, int count)
  {
    return \u003CModule\u003E.crc32(start, (byte*) src, (uint) count);
  }

  public static unsafe uint Adler32(uint adler, void* data, int count)
  {
    return \u003CModule\u003E.adler32(adler, (byte*) data, (uint) count);
  }
}
