// Decompiled with JetBrains decompiler
// Type: FableMod.TNG.UIDManager
// Assembly: FableMod.TNG, Version=1.0.4918.434, Culture=neutral, PublicKeyToken=null
// MVID: D9131661-A628-42D1-B5F7-4150ACB2CB8F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.TNG.dll

using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace FableMod.TNG;

public class UIDManager
{
  private static List<string> m_UIDs = new List<string>(16384 /*0x4000*/);
  private static string BASE = "18446741";
  private static int MAX_ITERATIONS = (int) ushort.MaxValue;
  private static int DEFAULT_LENGTH = 20;
  private static Random m_Rnd = new Random();
  private static bool m_BinarySearch = true;

  public static void Load(string directory)
  {
  }

  public static void LoadFromFile(string fileName)
  {
    FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
    TextReader textReader = (TextReader) new StreamReader((Stream) fileStream);
    UIDManager.m_UIDs.Clear();
    string str = textReader.ReadLine();
    if (str != (string) null)
    {
      do
      {
        UIDManager.m_UIDs.Add(str);
        str = textReader.ReadLine();
      }
      while (str != (string) null);
    }
    UIDManager.m_BinarySearch = true;
    textReader.Close();
    fileStream.Close();
  }

  public static void WriteToFile(string fileName)
  {
    if (UIDManager.m_UIDs.Count == 0)
      return;
    FileStream fileStream = File.Create(fileName);
    TextWriter textWriter = (TextWriter) new StreamWriter((Stream) fileStream);
    UIDManager.m_UIDs.Sort();
    UIDManager.m_BinarySearch = true;
    int index = 0;
    if (0 < UIDManager.m_UIDs.Count)
    {
      do
      {
        textWriter.WriteLine(UIDManager.m_UIDs[index]);
        ++index;
      }
      while (index < UIDManager.m_UIDs.Count);
    }
    textWriter.Close();
    fileStream.Close();
  }

  public static void Clear() => UIDManager.m_UIDs.Clear();

  public static void Add(string uid)
  {
    if ((uid == null || uid.Length < UIDManager.DEFAULT_LENGTH || !uid.StartsWith(UIDManager.BASE) ? 0 : 1) == 0 || UIDManager.Exists(uid))
      return;
    UIDManager.m_UIDs.Add(uid);
    UIDManager.m_BinarySearch = false;
  }

  public static void Remove(string uid)
  {
    UIDManager.m_UIDs.Remove(uid);
    UIDManager.m_BinarySearch = false;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public static bool Exists(string uid)
  {
    return UIDManager.m_BinarySearch ? UIDManager.m_UIDs.BinarySearch(uid) >= 0 : UIDManager.m_UIDs.IndexOf(uid) >= 0;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public static bool IsNormal(string uid)
  {
    return uid != null && uid.Length >= UIDManager.DEFAULT_LENGTH && uid.StartsWith(UIDManager.BASE);
  }

  private static string Generate(int length, string start)
  {
    int num1 = 0;
    if (start != null)
      num1 = start.Length;
    if (length <= num1)
      return start;
    int capacity = length - num1;
    StringBuilder stringBuilder = new StringBuilder(start, capacity);
    if (0 < capacity)
    {
      int num2 = capacity;
      do
      {
        stringBuilder.Append(UIDManager.m_Rnd.Next(1, 10));
        num2 += -1;
      }
      while (num2 != 0);
    }
    return stringBuilder.ToString();
  }

  public static string Generate()
  {
    int num1 = 0;
    string uid;
    do
    {
      uid = UIDManager.Generate(UIDManager.DEFAULT_LENGTH + 5, UIDManager.BASE);
      int num2 = num1;
      int maxIterations = UIDManager.MAX_ITERATIONS;
      ++num1;
      int num3 = maxIterations;
      if (num2 == num3)
        goto label_3;
    }
    while ((!UIDManager.m_BinarySearch ? (UIDManager.m_UIDs.IndexOf(uid) >= 0 ? 1 : 0) : (UIDManager.m_UIDs.BinarySearch(uid) >= 0 ? 1 : 0)) != 0);
    goto label_4;
label_3:
    throw new InvalidOperationException("FableMod::TNG::UIDManager: too many iterations");
label_4:
    UIDManager.Add(uid);
    return uid;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public static bool IsEmpty() => UIDManager.m_UIDs.Count == 0;

  private UIDManager()
  {
  }
}
