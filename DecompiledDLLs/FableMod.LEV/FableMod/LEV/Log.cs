// Decompiled with JetBrains decompiler
// Type: FableMod.LEV.Log
// Assembly: FableMod.LEV, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 267777C9-1C4D-4C46-87D1-7AC25AEC038B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.LEV.dll

using System;
using System.IO;

#nullable disable
namespace FableMod.LEV;

internal class Log
{
  private static int m_iRefs = 0;
  private static TextWriter m_Writer = (TextWriter) null;

  public static void Open()
  {
    ++Log.m_iRefs;
    if (Log.m_iRefs != 1)
      return;
    Log.m_Writer = (TextWriter) new StreamWriter(AppDomain.CurrentDomain.BaseDirectory + "FableMod.LEV.Log");
  }

  public static void Close()
  {
    --Log.m_iRefs;
    if (Log.m_iRefs != 0)
      return;
    Log.m_Writer.Close();
    Log.m_Writer = (TextWriter) null;
  }

  public static void WriteLine(string format, params object[] args)
  {
    Log.Open();
    if (Log.m_Writer != null)
      Log.m_Writer.WriteLine(format, args);
    Log.Close();
  }
}
