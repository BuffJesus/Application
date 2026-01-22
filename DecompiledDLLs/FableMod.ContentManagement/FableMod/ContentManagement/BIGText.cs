// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.BIGText
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using FableMod.BIG;
using System;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.ContentManagement;

public class BIGText : IDisposable
{
  private unsafe char* m_Content;
  private unsafe sbyte* m_SoundFile;
  private unsafe sbyte* m_Speaker;
  private unsafe sbyte* m_Identifier;
  private unsafe sbyte** m_Modifiers;
  private uint m_ModCount;

  public unsafe BIGText(AssetEntry ent)
  {
    sbyte* numPtr1 = ent.Length != 0U ? ent.GetData() : throw new Exception("invalid entry");
    sbyte* numPtr2 = numPtr1;
    if (*(short*) numPtr1 != (short) 0)
    {
      do
      {
        numPtr2 += 2L;
      }
      while (*(short*) numPtr2 != (short) 0);
    }
    uint num1 = (uint) ((IntPtr) numPtr2 - (IntPtr) numPtr1 >> 1);
    ulong num2 = (ulong) (num1 + 1U) * 2UL;
    this.m_Content = (char*) \u003CModule\u003E.@new(num2);
    sbyte* data = ent.GetData();
    long num3 = (long) ((IntPtr) this.m_Content - (IntPtr) data);
    short num4;
    do
    {
      num4 = *(short*) data;
      *(short*) (num3 + (IntPtr) data) = num4;
      data += 2L;
    }
    while (num4 != (short) 0);
    int num5 = (int) num2;
    // ISSUE: cpblk instruction
    __memcpy(ref num1, (IntPtr) (ent.GetData() + (long) num5), 4);
    int num6 = num5 + 4;
    this.m_SoundFile = (sbyte*) \u003CModule\u003E.@new((ulong) (num1 + 1U));
    sbyte* soundFile = this.m_SoundFile;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) soundFile, (IntPtr) (ent.GetData() + (long) num6), (long) num1);
    int num7 = num6 + (int) num1;
    *(sbyte*) ((long) num1 + (IntPtr) soundFile) = (sbyte) 0;
    // ISSUE: cpblk instruction
    __memcpy(ref num1, (IntPtr) (ent.GetData() + (long) num7), 4);
    int num8 = num7 + 4;
    this.m_Speaker = (sbyte*) \u003CModule\u003E.@new((ulong) (num1 + 1U));
    sbyte* speaker = this.m_Speaker;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) speaker, (IntPtr) (ent.GetData() + (long) num8), (long) num1);
    int num9 = num8 + (int) num1;
    *(sbyte*) ((long) num1 + (IntPtr) speaker) = (sbyte) 0;
    // ISSUE: cpblk instruction
    __memcpy(ref num1, (IntPtr) (ent.GetData() + (long) num9), 4);
    int num10 = num9 + 4;
    this.m_Identifier = (sbyte*) \u003CModule\u003E.@new((ulong) (num1 + 1U));
    sbyte* identifier = this.m_Identifier;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) identifier, (IntPtr) (ent.GetData() + (long) num10), (long) num1);
    int num11 = num10 + (int) num1;
    *(sbyte*) ((long) num1 + (IntPtr) identifier) = (sbyte) 0;
    // ISSUE: cpblk instruction
    __memcpy(ref num1, (IntPtr) (ent.GetData() + (long) num11), 4);
    int num12 = num11 + 4;
    this.m_ModCount = num1;
    this.m_Modifiers = (sbyte**) \u003CModule\u003E.@new((ulong) num1 * 8UL);
    int num13 = 0;
    if (0U >= this.m_ModCount)
      return;
    long num14 = 0;
    do
    {
      int num15 = num12 + 4;
      long num16 = (long) num15;
      sbyte* numPtr3 = ent.GetData() + num16;
      sbyte* numPtr4 = numPtr3;
      if (*numPtr3 != (sbyte) 0)
      {
        do
        {
          ++numPtr4;
        }
        while (*numPtr4 != (sbyte) 0);
      }
      uint num17 = (uint) ((UIntPtr) numPtr4 - (UIntPtr) numPtr3) + 1U;
      ulong num18 = (ulong) num17;
      sbyte* numPtr5 = (sbyte*) \u003CModule\u003E.@new(num18);
      *(long*) ((IntPtr) this.m_Modifiers + num14) = (long) numPtr5;
      // ISSUE: cpblk instruction
      __memcpy(*(long*) ((IntPtr) this.m_Modifiers + num14), (IntPtr) (ent.GetData() + num16), (long) num18);
      num12 = num15 + (int) num17;
      ++num13;
      num14 += 8L;
    }
    while ((uint) num13 < this.m_ModCount);
  }

  public unsafe BIGText()
  {
    void* voidPtr1 = \u003CModule\u003E.@new(1UL);
    this.m_Identifier = (sbyte*) voidPtr1;
    *(sbyte*) voidPtr1 = (sbyte) 0;
    void* voidPtr2 = \u003CModule\u003E.@new(2UL);
    this.m_Content = (char*) voidPtr2;
    *(short*) voidPtr2 = (short) 0;
  }

  private unsafe void \u007EBIGText()
  {
    \u003CModule\u003E.delete((void*) this.m_Content);
    \u003CModule\u003E.delete((void*) this.m_SoundFile);
    \u003CModule\u003E.delete((void*) this.m_Speaker);
    \u003CModule\u003E.delete((void*) this.m_Identifier);
    int num1 = 0;
    if (0U < this.m_ModCount)
    {
      long num2 = 0;
      do
      {
        \u003CModule\u003E.delete((void*) *(long*) ((IntPtr) this.m_Modifiers + num2));
        ++num1;
        num2 += 8L;
      }
      while ((uint) num1 < this.m_ModCount);
    }
    \u003CModule\u003E.delete((void*) this.m_Modifiers);
  }

  public unsafe void ApplyToEntry(AssetEntry entry)
  {
    char* content = this.m_Content;
    char* chPtr = content;
    if ((short) *content != (short) 0)
    {
      do
      {
        chPtr += 2L;
      }
      while ((short) *chPtr != (short) 0);
    }
    int num1 = (int) ((((IntPtr) chPtr - (IntPtr) content >> 1) + 1L) * 2L);
    sbyte* soundFile = this.m_SoundFile;
    sbyte* numPtr1 = soundFile;
    if (*soundFile != (sbyte) 0)
    {
      do
      {
        ++numPtr1;
      }
      while (*numPtr1 != (sbyte) 0);
    }
    uint num2 = (uint) ((UIntPtr) numPtr1 - (UIntPtr) soundFile);
    sbyte* speaker = this.m_Speaker;
    sbyte* numPtr2 = speaker;
    if (*speaker != (sbyte) 0)
    {
      do
      {
        ++numPtr2;
      }
      while (*numPtr2 != (sbyte) 0);
    }
    uint num3 = (uint) ((UIntPtr) numPtr2 - (UIntPtr) speaker);
    sbyte* identifier = this.m_Identifier;
    sbyte* numPtr3 = identifier;
    if (*identifier != (sbyte) 0)
    {
      do
      {
        ++numPtr3;
      }
      while (*numPtr3 != (sbyte) 0);
    }
    uint num4 = (uint) ((UIntPtr) numPtr3 - (UIntPtr) identifier);
    uint num5 = 4;
    uint modCount1 = this.m_ModCount;
    if (0U < modCount1)
    {
      sbyte** modifiers = this.m_Modifiers;
      uint num6 = modCount1;
      do
      {
        long num7 = *(long*) modifiers;
        long num8 = num7;
        if (*(sbyte*) num7 != (sbyte) 0)
        {
          do
          {
            ++num8;
          }
          while (*(sbyte*) num8 != (sbyte) 0);
        }
        num5 = (uint) ((ulong) (num8 - num7) + (ulong) num5 + 5UL);
        modifiers += 8L;
        num6 += uint.MaxValue;
      }
      while (num6 > 0U);
    }
    uint len = (uint) ((int) num5 + (int) num4 + (int) num3 + (int) num2 + num1 + 12);
    sbyte* data = (sbyte*) \u003CModule\u003E.@new((ulong) len);
    ulong num9 = (ulong) num1;
    // ISSUE: cpblk instruction
    __memcpy((IntPtr) data, (IntPtr) this.m_Content, (long) num9);
    // ISSUE: cpblk instruction
    __memcpy((long) num1 + (IntPtr) data, ref num2, 4);
    int num10 = (int) ((long) num9 + 4L);
    // ISSUE: cpblk instruction
    __memcpy((long) num10 + (IntPtr) data, (IntPtr) this.m_SoundFile, (long) num2);
    int num11 = num10 + (int) num2;
    // ISSUE: cpblk instruction
    __memcpy((long) num11 + (IntPtr) data, ref num3, 4);
    int num12 = (int) ((long) num11 + 4L);
    // ISSUE: cpblk instruction
    __memcpy((long) num12 + (IntPtr) data, (IntPtr) this.m_Speaker, (long) num3);
    int num13 = num12 + (int) num3;
    // ISSUE: cpblk instruction
    __memcpy((long) num13 + (IntPtr) data, ref num4, 4);
    int num14 = (int) ((long) num13 + 4L);
    // ISSUE: cpblk instruction
    __memcpy((long) num14 + (IntPtr) data, (IntPtr) this.m_Identifier, (long) num4);
    int num15 = num14 + (int) num4;
    uint modCount2 = this.m_ModCount;
    // ISSUE: cpblk instruction
    __memcpy((long) num15 + (IntPtr) data, ref modCount2, 4);
    int num16 = (int) ((long) num15 + 4L);
    int num17 = 0;
    if (0U < this.m_ModCount)
    {
      do
      {
        // ISSUE: cpblk instruction
        __memcpy((long) num16 + (IntPtr) data, ref num17, 4);
        int num18 = (int) ((long) num16 + 4L);
        long num19 = *(long*) ((long) num17 * 8L + (IntPtr) this.m_Modifiers);
        long num20 = num19;
        long num21 = num20;
        if (*(sbyte*) num20 != (sbyte) 0)
        {
          do
          {
            ++num21;
          }
          while (*(sbyte*) num21 != (sbyte) 0);
        }
        int num22 = (int) (num21 - num20) + 1;
        // ISSUE: cpblk instruction
        __memcpy((long) num18 + (IntPtr) data, num19, (long) num22);
        num16 = num18 + num22;
        ++num17;
      }
      while ((uint) num17 < this.m_ModCount);
    }
    entry.SetData(data, len);
    \u003CModule\u003E.delete((void*) data);
  }

  public unsafe string Content
  {
    get
    {
      return Marshal.PtrToStringUni((IntPtr) (void*) this.m_Content).Replace("\n", Environment.NewLine);
    }
    set
    {
      IntPtr hglobalUni = Marshal.StringToHGlobalUni(value.Replace("\r", nameof ()));
      \u003CModule\u003E.delete((void*) this.m_Content);
      ulong num1 = (ulong) (value.Length + 1);
      this.m_Content = (char*) \u003CModule\u003E.@new(num1 > (ulong) long.MaxValue ? ulong.MaxValue : num1 * 2UL);
      void* pointer = hglobalUni.ToPointer();
      char* content = this.m_Content;
      short num2;
      do
      {
        num2 = *(short*) pointer;
        *content = (char) num2;
        pointer += 2L;
        content += 2L;
      }
      while (num2 != (short) 0);
      Marshal.FreeHGlobal(hglobalUni);
    }
  }

  public unsafe string SoundFile
  {
    get => Marshal.PtrToStringAnsi((IntPtr) (void*) this.m_SoundFile);
    set
    {
      IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi(value);
      \u003CModule\u003E.delete((void*) this.m_SoundFile);
      this.m_SoundFile = (sbyte*) \u003CModule\u003E.@new((ulong) (value.Length + 1));
      void* pointer = hglobalAnsi.ToPointer();
      sbyte* soundFile = this.m_SoundFile;
      sbyte num;
      do
      {
        num = *(sbyte*) pointer;
        *soundFile = num;
        ++pointer;
        ++soundFile;
      }
      while (num != (sbyte) 0);
      Marshal.FreeHGlobal(hglobalAnsi);
    }
  }

  public unsafe string Speaker
  {
    get => Marshal.PtrToStringAnsi((IntPtr) (void*) this.m_Speaker);
    set
    {
      IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi(value);
      \u003CModule\u003E.delete((void*) this.m_Speaker);
      this.m_Speaker = (sbyte*) \u003CModule\u003E.@new((ulong) (value.Length + 1));
      void* pointer = hglobalAnsi.ToPointer();
      sbyte* speaker = this.m_Speaker;
      sbyte num;
      do
      {
        num = *(sbyte*) pointer;
        *speaker = num;
        ++pointer;
        ++speaker;
      }
      while (num != (sbyte) 0);
      Marshal.FreeHGlobal(hglobalAnsi);
    }
  }

  public unsafe string Identifier
  {
    get => Marshal.PtrToStringAnsi((IntPtr) (void*) this.m_Identifier);
    set
    {
      IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi(value);
      \u003CModule\u003E.delete((void*) this.m_Identifier);
      this.m_Identifier = (sbyte*) \u003CModule\u003E.@new((ulong) (value.Length + 1));
      void* pointer = hglobalAnsi.ToPointer();
      sbyte* identifier = this.m_Identifier;
      sbyte num;
      do
      {
        num = *(sbyte*) pointer;
        *identifier = num;
        ++pointer;
        ++identifier;
      }
      while (num != (sbyte) 0);
      Marshal.FreeHGlobal(hglobalAnsi);
    }
  }

  public unsafe string[] Modifiers
  {
    get
    {
      uint modCount = this.m_ModCount;
      string[] modifiers = new string[(int) modCount];
      int index = 0;
      if (0U < modCount)
      {
        long num = 0;
        do
        {
          IntPtr ptr = (IntPtr) (void*) *(long*) ((IntPtr) this.m_Modifiers + num);
          modifiers[index] = Marshal.PtrToStringAnsi(ptr);
          ++index;
          num += 8L;
        }
        while ((uint) index < this.m_ModCount);
      }
      return modifiers;
    }
    set
    {
      int num1 = 0;
      if (0U < this.m_ModCount)
      {
        long num2 = 0;
        do
        {
          \u003CModule\u003E.delete((void*) *(long*) ((IntPtr) this.m_Modifiers + num2));
          ++num1;
          num2 += 8L;
        }
        while ((uint) num1 < this.m_ModCount);
      }
      \u003CModule\u003E.delete((void*) this.m_Modifiers);
      int length = value.Length;
      this.m_ModCount = (uint) length;
      this.m_Modifiers = (sbyte**) \u003CModule\u003E.@new((ulong) (uint) length * 8UL);
      int index = 0;
      if (0 >= value.Length)
        return;
      long num3 = 0;
      do
      {
        IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi(value[index]);
        sbyte* numPtr = (sbyte*) \u003CModule\u003E.@new((ulong) (value[index].Length + 1));
        *(long*) ((IntPtr) this.m_Modifiers + num3) = (long) numPtr;
        void* pointer = hglobalAnsi.ToPointer();
        long num4 = *(long*) ((IntPtr) this.m_Modifiers + num3);
        sbyte num5;
        do
        {
          num5 = *(sbyte*) pointer;
          *(sbyte*) num4 = num5;
          ++pointer;
          ++num4;
        }
        while (num5 != (sbyte) 0);
        Marshal.FreeHGlobal(hglobalAnsi);
        ++index;
        num3 += 8L;
      }
      while (index < value.Length);
    }
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EBIGText();
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
