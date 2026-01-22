// Decompiled with JetBrains decompiler
// Type: FableMod.Script.Parser
// Assembly: FableMod.Script, Version=1.0.4918.426, Culture=neutral, PublicKeyToken=null
// MVID: 875C6290-EFF7-4C90-A972-C4C46B8CCDEB
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Script.dll

using System;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.Script;

public class Parser : IDisposable
{
  protected TextReader m_Reader;

  private void \u007EParser()
  {
  }

  public virtual void Load(string fileName)
  {
    FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
    byte[] numArray = new byte[(int) fileStream.Length];
    fileStream.Read(numArray, 0, numArray.Length);
    fileStream.Close();
    fileStream?.Dispose();
    MemoryStream memoryStream = new MemoryStream(numArray);
    this.m_Reader = (TextReader) new StreamReader((Stream) memoryStream);
    while (true)
    {
      string line = this.m_Reader.ReadLine();
      if (line != (string) null)
        line = line.Trim();
      if (line != (string) null)
        this.ParseLine(line);
      else
        break;
    }
    memoryStream.Close();
    this.m_Reader.Close();
    this.m_Reader = (TextReader) null;
  }

  protected string NextLine()
  {
    string str = this.m_Reader.ReadLine();
    if (str != (string) null)
      str = str.Trim();
    return str;
  }

  protected virtual void ParseLine(string line)
  {
    if (line.Length == 0)
      return;
    int length = line.IndexOf(" ");
    if (length > 0)
      this.ParseArgument(line.Substring(0, length), line.Substring(length + 1, line.Length + (-2 - length)));
    else
      this.ParseCommand(line.Substring(0, line.Length - 1));
  }

  protected virtual void ParseArgument(string argument, string value)
  {
  }

  protected virtual void ParseCommand(string command)
  {
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
      return;
    // ISSUE: explicit finalizer call
    this.Finalize();
  }

  public virtual void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }
}
