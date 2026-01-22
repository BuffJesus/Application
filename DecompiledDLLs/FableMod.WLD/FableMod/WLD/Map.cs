// Decompiled with JetBrains decompiler
// Type: FableMod.WLD.Map
// Assembly: FableMod.WLD, Version=1.0.4918.436, Culture=neutral, PublicKeyToken=null
// MVID: C116F1D2-4A42-43FB-9046-16C428742204
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.WLD.dll

using System;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.WLD;

public class Map : IDisposable
{
  protected int m_ID;
  protected uint m_UID;
  protected int m_X;
  protected int m_Y;
  protected string m_LevelName;
  protected string m_LevelScriptName;
  protected bool m_IsSea;
  protected bool m_LoadedOnProximity;

  private void \u007EMap()
  {
  }

  public void Save(TextWriter writer)
  {
    writer.WriteLine("NewMap {0};", (object) this.m_ID);
    writer.WriteLine("MapX {0};", (object) this.m_X);
    writer.WriteLine("MapY {0};", (object) this.m_Y);
    writer.WriteLine("LevelName \"{0}\";", (object) this.m_LevelName);
    writer.WriteLine("LevelScriptName {0};", (object) this.m_LevelScriptName);
    writer.WriteLine("MapUID {0};", (object) this.m_UID);
    bool isSea = this.m_IsSea;
    writer.WriteLine("IsSea {0};", (object) isSea.ToString().ToUpper());
    bool loadedOnProximity = this.m_LoadedOnProximity;
    writer.WriteLine("LoadedOnPlayerProximity {0};", (object) loadedOnProximity.ToString().ToUpper());
    writer.WriteLine("EndMap;");
    writer.WriteLine("");
  }

  public int ID
  {
    get => this.m_ID;
    set => this.m_ID = value;
  }

  public uint UID
  {
    get => this.m_UID;
    set => this.m_UID = value;
  }

  public int X
  {
    get => this.m_X;
    set => this.m_X = value;
  }

  public int Y
  {
    get => this.m_Y;
    set => this.m_Y = value;
  }

  public string LevelName
  {
    get => this.m_LevelName;
    set => this.m_LevelName = value;
  }

  public string LevelScriptName
  {
    get => this.m_LevelScriptName;
    set => this.m_LevelScriptName = value;
  }

  public bool IsSea
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_IsSea;
    [param: MarshalAs(UnmanagedType.U1)] set => this.m_IsSea = value;
  }

  public bool LoadedOnProximity
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_LoadedOnProximity;
    [param: MarshalAs(UnmanagedType.U1)] set => this.m_LoadedOnProximity = value;
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
