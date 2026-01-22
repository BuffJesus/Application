// Decompiled with JetBrains decompiler
// Type: FableMod.TNG.Section
// Assembly: FableMod.TNG, Version=1.0.4918.434, Culture=neutral, PublicKeyToken=null
// MVID: D9131661-A628-42D1-B5F7-4150ACB2CB8F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.TNG.dll

using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.TNG;

public class Section(string name) : IDisposable
{
  protected string m_Name = name;
  protected Collection<Thing> m_Things = new Collection<Thing>();
  protected TNGFile m_File;
  protected bool m_Modified = false;

  private void \u007ESection()
  {
    this.Destroy();
    this.m_Things = (Collection<Thing>) null;
    this.m_Name = (string) null;
  }

  public void Destroy()
  {
    Collection<Thing> things = this.m_Things;
    if (things == null)
      return;
    int index = 0;
    if (0 < things.Count)
    {
      do
      {
        this.m_Things[index].Section = (Section) null;
        this.m_Things[index]?.Dispose();
        ++index;
      }
      while (index < this.m_Things.Count);
    }
    this.m_Things.Clear();
  }

  public void Save(TextWriter writer)
  {
    writer.WriteLine("XXXSectionStart {0};", (object) this.m_Name);
    writer.WriteLine("");
    int index = 0;
    if (0 < this.m_Things.Count)
    {
      do
      {
        this.m_Things[index].Save(writer);
        ++index;
      }
      while (index < this.m_Things.Count);
    }
    writer.WriteLine("XXXSectionEnd;");
    writer.WriteLine("");
  }

  public void AddThing(Thing thing)
  {
    this.m_Things.Add(thing);
    thing.Section = this;
    this.m_Modified = true;
  }

  public void RemoveThing(Thing thing)
  {
    thing.Section = (Section) null;
    int index = this.m_Things.IndexOf(thing);
    if (index < 0)
      return;
    Console.WriteLine("Section({0})::RemoveThing({1})", (object) this.m_Name, (object) thing.DefinitionType);
    this.m_Things.RemoveAt(index);
    this.m_Modified = true;
  }

  public Thing FindThing(string uid)
  {
    int index = 0;
    if (0 < this.m_Things.Count)
    {
      while (!(this.m_Things[index].UID == uid))
      {
        ++index;
        if (index >= this.m_Things.Count)
          goto label_4;
      }
      return this.m_Things[index];
    }
label_4:
    return (Thing) null;
  }

  public Collection<Thing> Things => this.m_Things;

  public string Name => this.m_Name;

  public virtual bool Modified
  {
    [return: MarshalAs(UnmanagedType.U1)] get
    {
      if (this.m_Modified)
        return true;
      int index = 0;
      if (0 < this.m_Things.Count)
      {
        while (!this.m_Things[index].Modified)
        {
          ++index;
          if (index >= this.m_Things.Count)
            goto label_6;
        }
        return true;
      }
label_6:
      return false;
    }
    [param: MarshalAs(UnmanagedType.U1)] set
    {
      this.m_Modified = value;
      int index = 0;
      if (0 >= this.m_Things.Count)
        return;
      do
      {
        this.m_Things[index].Modified = value;
        ++index;
      }
      while (index < this.m_Things.Count);
    }
  }

  public TNGFile TNGFile
  {
    get => this.m_File;
    set => this.m_File = value;
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.Destroy();
      this.m_Things = (Collection<Thing>) null;
      this.m_Name = (string) null;
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
