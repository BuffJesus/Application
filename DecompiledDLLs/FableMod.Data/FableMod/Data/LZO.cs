// Decompiled with JetBrains decompiler
// Type: FableMod.Data.LZO
// Assembly: FableMod.Data, Version=1.0.4918.427, Culture=neutral, PublicKeyToken=null
// MVID: 4E10122A-6FC9-49D4-9087-7FA415462296
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Data.dll

using System;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.Data;

public class LZO
{
  public static uint[,] FindChunks(byte[] data)
  {
    \u003CModule\u003E.__lzo_init_v2(8240U, 2, 4, 4, 4, 8, 8, 8, 8, 48 /*0x30*/);
    uint[,] chunks = new uint[0, 2];
    uint index1 = 0;
    while (true)
    {
      int length = data.Length;
      if (index1 < (uint) (length - 7))
      {
        ushort count1 = (ushort) ((int) data[(int) index1 + 1] * 256 /*0x0100*/ + (int) data[(int) index1]);
        switch (count1)
        {
          case 0:
          case 1:
          case 2:
          case 3:
            ++index1;
            continue;
          case ushort.MaxValue:
            uint count2 = (uint) (((int) data[(int) index1 + 5] << 24) + ((int) data[(int) index1 + 4] << 16 /*0x10*/) + ((int) data[(int) index1 + 3] << 8)) + (uint) data[(int) index1 + 2];
            if ((uint) ((int) count2 + (int) index1 + 6) <= (uint) length && data[(int) count2 + (int) index1 + 3] == (byte) 17 && data[(int) count2 + (int) index1 + 4] == (byte) 0)
            {
              if (data[(int) count2 + (int) index1 + 5] == (byte) 0)
              {
                try
                {
                  LZO.DecompressRaw(data, (int) index1 + 6, (int) count2);
                }
                catch (Exception ex)
                {
                  goto case 0;
                }
                uint[,] numArray1 = new uint[chunks.GetLength(0) + 1, 2];
                int index2 = 0;
                if (0 < chunks.GetLength(0))
                {
                  do
                  {
                    numArray1[index2, 0] = chunks[index2, 0];
                    numArray1[index2, 1] = chunks[index2, 1];
                    ++index2;
                  }
                  while (index2 < chunks.GetLength(0));
                }
                uint[,] numArray2 = numArray1;
                numArray2[numArray2.GetLength(0) - 1, 0] = index1;
                uint[,] numArray3 = numArray1;
                numArray3[numArray3.GetLength(0) - 1, 1] = count2 + 6U;
                chunks = numArray1;
                index1 += count2 + 6U;
                goto case 0;
              }
              goto case 0;
            }
            goto case 0;
          default:
            if ((uint) ((int) count1 + (int) index1 + 2) <= (uint) length)
            {
              uint index3 = (uint) count1 + index1;
              if (data[(int) index3 - 1] == (byte) 17 && data[(int) index3] == (byte) 0)
              {
                if (data[(int) index3 + 1] == (byte) 0)
                {
                  try
                  {
                    LZO.DecompressRaw(data, (int) index1 + 2, (int) count1);
                  }
                  catch (Exception ex)
                  {
                    goto case 0;
                  }
                  uint[,] numArray4 = new uint[chunks.GetLength(0) + 1, 2];
                  int index4 = 0;
                  if (0 < chunks.GetLength(0))
                  {
                    do
                    {
                      numArray4[index4, 0] = chunks[index4, 0];
                      numArray4[index4, 1] = chunks[index4, 1];
                      ++index4;
                    }
                    while (index4 < chunks.GetLength(0));
                  }
                  uint[,] numArray5 = numArray4;
                  numArray5[numArray5.GetLength(0) - 1, 0] = index1;
                  uint[,] numArray6 = numArray4;
                  numArray6[numArray6.GetLength(0) - 1, 1] = (uint) count1 + 2U;
                  chunks = numArray4;
                  index1 += (uint) count1 + 2U;
                  goto case 0;
                }
                goto case 0;
              }
              goto case 0;
            }
            goto case 0;
        }
      }
      else
        break;
    }
    return chunks;
  }

  public static unsafe uint[,] FindChunks(byte* data, uint length)
  {
    uint[,] chunks = new uint[0, 2];
    uint num1 = 0;
    uint num2 = length - 7U;
    if (0U < num2)
    {
      do
      {
        ushort num3 = (ushort) ((int) *(byte*) ((long) (num1 + 1U) + (IntPtr) data) * 256 /*0x0100*/ + (int) *(byte*) ((long) num1 + (IntPtr) data));
        switch (num3)
        {
          case 0:
          case 1:
          case 2:
          case 3:
            ++num1;
            continue;
          case ushort.MaxValue:
            uint num4 = (uint) (((int) *(byte*) ((long) (num1 + 5U) + (IntPtr) data) << 24) + ((int) *(byte*) ((long) (num1 + 4U) + (IntPtr) data) << 16 /*0x10*/) + ((int) *(byte*) ((long) (num1 + 3U) + (IntPtr) data) << 8)) + (uint) *(byte*) ((long) (num1 + 2U) + (IntPtr) data);
            uint num5 = (uint) ((int) num4 + (int) num1 + 6);
            if (num5 < length && *(byte*) ((long) (uint) ((int) num4 + (int) num1 + 3) + (IntPtr) data) == (byte) 17 && *(byte*) ((long) (uint) ((int) num4 + (int) num1 + 4) + (IntPtr) data) == (byte) 0 && *(byte*) ((long) (uint) ((int) num4 + (int) num1 + 5) + (IntPtr) data) == (byte) 0)
            {
              uint[,] numArray1 = new uint[chunks.GetLength(0) + 1, 2];
              int index = 0;
              if (0 < chunks.GetLength(0))
              {
                do
                {
                  numArray1[index, 0] = chunks[index, 0];
                  numArray1[index, 1] = chunks[index, 1];
                  ++index;
                }
                while (index < chunks.GetLength(0));
              }
              uint[,] numArray2 = numArray1;
              numArray2[numArray2.GetLength(0) - 1, 0] = num1;
              uint[,] numArray3 = numArray1;
              numArray3[numArray3.GetLength(0) - 1, 1] = num4 + 6U;
              chunks = numArray1;
              num1 = num5;
              goto case 0;
            }
            goto case 0;
          default:
            uint num6 = (uint) num3 + num1;
            if (num6 + 2U < length && *(byte*) ((long) (num6 - 1U) + (IntPtr) data) == (byte) 17 && *(byte*) ((long) num6 + (IntPtr) data) == (byte) 0 && *(byte*) ((long) (num6 + 1U) + (IntPtr) data) == (byte) 0)
            {
              uint[,] numArray4 = new uint[chunks.GetLength(0) + 1, 2];
              int index = 0;
              if (0 < chunks.GetLength(0))
              {
                do
                {
                  numArray4[index, 0] = chunks[index, 0];
                  numArray4[index, 1] = chunks[index, 1];
                  ++index;
                }
                while (index < chunks.GetLength(0));
              }
              uint[,] numArray5 = numArray4;
              numArray5[numArray5.GetLength(0) - 1, 0] = num1;
              uint[,] numArray6 = numArray4;
              numArray6[numArray6.GetLength(0) - 1, 1] = (uint) num3 + 2U;
              chunks = numArray4;
              num1 = num6 + 6U;
              goto case 0;
            }
            goto case 0;
        }
      }
      while (num1 < num2);
    }
    return chunks;
  }

  public static unsafe byte[] DecompressRaw(byte[] @in, int offset, int count)
  {
    fixed (byte* in1 = &@in[offset])
    {
      int num = count * 10;
      byte* numPtr = (byte*) \u003CModule\u003E.@new((ulong) num);
      uint length = (uint) num;
      int inlen = count;
      byte* @out = numPtr;
      ref uint local1 = ref length;
      LZO.DecompressRaw(in1, (uint) inlen, @out, (uint*) ref local1);
      byte[] numArray = new byte[(int) length];
      // ISSUE: variable of a reference type
      byte* local2 = ref numArray[0];
      // ISSUE: cpblk instruction
      __memcpy(local2, (IntPtr) numPtr, (long) length);
      \u003CModule\u003E.delete((void*) numPtr);
      return numArray;
    }
  }

  public static unsafe void DecompressRaw(byte* @in, uint inlen, byte* @out, uint* outlen)
  {
    \u003CModule\u003E.__lzo_init_v2(8240U, 2, 4, 4, 4, 8, 8, 8, 8, 48 /*0x30*/);
    int num = \u003CModule\u003E.lzo1x_decompress_safe(@in, (ulong) inlen, @out, (ulong*) outlen, (void*) 0L);
    if (num != 0)
      throw new Exception($"FableMod::Data::LZO: DecompressRaw failed {num}");
  }

  public static byte[] DecompressChunk(byte[] @in, int offset, int count)
  {
    ushort count1 = (ushort) ((int) @in[offset + 1] * 256 /*0x0100*/ + (int) @in[offset]);
    if (count1 == ushort.MaxValue)
    {
      uint count2 = (uint) (((int) @in[offset + 5] << 24) + ((int) @in[offset + 4] << 16 /*0x10*/) + ((int) @in[offset + 3] << 8)) + (uint) @in[offset + 2];
      byte[] numArray1 = LZO.DecompressRaw(@in, offset + 6, (int) count2);
      byte[] numArray2 = new byte[numArray1.Length + (count - (int) count2 - 6)];
      numArray1.CopyTo((Array) numArray2, 0);
      if (count2 + 6U < (uint) count)
      {
        uint num1 = count2 + 6U;
        uint num2 = 4294967290U - count2;
        do
        {
          numArray2[numArray1.Length + (int) num2 + (int) num1] = @in[(int) num1 + offset];
          ++num1;
        }
        while (num1 < (uint) count);
      }
      return numArray2;
    }
    byte[] numArray3 = LZO.DecompressRaw(@in, offset + 2, (int) count1);
    byte[] numArray4 = new byte[numArray3.Length + (count - (int) count1 - 2)];
    numArray3.CopyTo((Array) numArray4, 0);
    int num3 = (int) count1;
    if (num3 + 2 < count)
    {
      int num4 = num3 + 2;
      int num5 = -2 - num3;
      do
      {
        numArray4[numArray3.Length + num5 + num4] = @in[num4 + offset];
        ++num4;
      }
      while (num4 < count);
    }
    return numArray4;
  }

  public static unsafe void DecompressChunk(byte* @in, uint inlen, byte* @out, uint* outlen)
  {
    ushort inlen1 = (ushort) ((int) @in[1L] * 256 /*0x0100*/ + (int) *@in);
    switch (inlen1)
    {
      case 0:
        *outlen = 0U;
        break;
      case ushort.MaxValue:
        uint inlen2 = (uint) (((int) @in[5L] << 24) + ((int) @in[4L] << 16 /*0x10*/) + ((int) @in[3L] << 8)) + (uint) @in[2L];
        LZO.DecompressRaw(@in + 6L, inlen2, @out, outlen);
        int num1 = 0;
        long num2 = (long) (int) inlen2;
        if (inlen2 + 6U < inlen)
        {
          byte* numPtr = @in + num2 + 6L;
          do
          {
            *(sbyte*) ((long) (*outlen + (uint) num1) + (IntPtr) @out) = (sbyte) *numPtr;
            ++num1;
            ++numPtr;
          }
          while ((uint) ((int) inlen2 + 6 + num1) < inlen);
        }
        uint* numPtr1 = outlen;
        int num3 = (int) *numPtr1 + num1;
        *numPtr1 = (uint) num3;
        return;
      default:
        LZO.DecompressRaw(@in + 2L, (uint) inlen1, @out, outlen);
        break;
    }
    int num4 = 0;
    uint num5 = (uint) inlen1 + 2U;
    if (num5 < inlen)
    {
      byte* numPtr2 = @in + (long) inlen1 + 2L;
      do
      {
        *(sbyte*) ((long) (*outlen + (uint) num4) + (IntPtr) @out) = (sbyte) *numPtr2;
        ++num4;
        ++numPtr2;
      }
      while (num5 + (uint) num4 < inlen);
    }
    uint* numPtr3 = outlen;
    int num6 = (int) *numPtr3 + num4;
    *numPtr3 = (uint) num6;
  }

  public static unsafe byte[] CompressRaw(byte[] @in, int offset, int count)
  {
    fixed (byte* in1 = &@in[offset])
    {
      int num = count * 2;
      byte* numPtr = (byte*) \u003CModule\u003E.@new((ulong) num);
      uint length = (uint) num;
      int inlen = count;
      byte* @out = numPtr;
      ref uint local1 = ref length;
      LZO.CompressRaw(in1, (uint) inlen, @out, (uint*) ref local1);
      byte[] numArray = new byte[(int) length];
      // ISSUE: variable of a reference type
      byte* local2 = ref numArray[0];
      // ISSUE: cpblk instruction
      __memcpy(local2, (IntPtr) numPtr, (long) length);
      return numArray;
    }
  }

  public static unsafe void CompressRaw(byte* @in, uint inlen, byte* @out, uint* outlen)
  {
    \u003CModule\u003E.__lzo_init_v2(8240U, 2, 4, 4, 4, 8, 8, 8, 8, 48 /*0x30*/);
    byte* numPtr = (byte*) \u003CModule\u003E.@new(458752UL /*0x070000*/);
    int num = \u003CModule\u003E.lzo1x_999_compress(@in, (ulong) inlen, @out, (ulong*) outlen, (void*) numPtr);
    \u003CModule\u003E.delete((void*) numPtr);
    if (num != 0)
      throw new Exception("FableMod::Data::LZO: CompressRaw failed");
  }

  public static byte[] CompressChunk(byte[] @in, int offset, int count, int trailcount)
  {
    throw new Exception("FableMod::Data::LZO: Not implemented");
  }

  public static unsafe void CompressChunk(
    byte* @in,
    uint inlen,
    byte* @out,
    uint* outlen,
    int trailcount,
    [MarshalAs(UnmanagedType.U1)] bool forceDwordLength)
  {
    uint num = *outlen - (uint) trailcount;
    byte* out1 = @out + 6L;
    LZO.CompressRaw(@in, inlen - (uint) trailcount, out1, &num);
    if (num < (uint) ushort.MaxValue && !forceDwordLength)
    {
      *(short*) @out = (short) num;
      \u003CModule\u003E.memmove((void*) (@out + 2L), (void*) out1, (ulong) num);
      // ISSUE: cpblk instruction
      __memcpy((IntPtr) (@out + (long) num + 2L), (long) inlen - (long) trailcount + (IntPtr) @in, (long) trailcount);
      *outlen = (uint) ((int) num + trailcount + 2);
    }
    else
    {
      *(short*) @out = (short) -1;
      *(int*) (@out + 2L) = (int) num;
      // ISSUE: cpblk instruction
      __memcpy((IntPtr) (@out + (long) num + 6L), (long) inlen - (long) trailcount + (IntPtr) @in, (long) trailcount);
      *outlen = (uint) ((int) num + trailcount + 6);
    }
  }

  public static unsafe void CompressChunk(
    byte* @in,
    uint inlen,
    byte* @out,
    uint* outlen,
    int trailcount)
  {
    LZO.CompressChunk(@in, inlen, @out, outlen, trailcount, false);
  }
}
