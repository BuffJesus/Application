// Decompiled with JetBrains decompiler
// Type: FableMod.TNG.TNGFile
// Assembly: FableMod.TNG, Version=1.0.4918.434, Culture=neutral, PublicKeyToken=null
// MVID: D9131661-A628-42D1-B5F7-4150ACB2CB8F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.TNG.dll

using FableMod.Script;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.TNG;

public class TNGFile(TNGDefinitions definitions) : Parser
{
  protected Collection<Section> m_Sections;
  protected TNGDefinitions m_Definitions = definitions;
  protected TNGFile.ParserState m_Parser = TNGFile.ParserState.Basic;
  protected CTCBlock m_CTC;
  protected Thing m_Thing;
  protected string m_FileName;

  private void \u007ETNGFile()
  {
    this.Destroy();
    this.m_Sections = (Collection<Section>) null;
  }

  public void Destroy()
  {
    Collection<Section> sections = this.m_Sections;
    if (sections == null)
      return;
    int index = 0;
    if (0 < sections.Count)
    {
      do
      {
        this.m_Sections[index]?.Dispose();
        ++index;
      }
      while (index < this.m_Sections.Count);
    }
    this.m_Sections.Clear();
  }

  public override void Load(string fileName)
  {
    base.Load(fileName);
    this.m_FileName = fileName;
    this.Modified = false;
  }

  public void Save(string fileName)
  {
    FileStream fileStream = File.Create(fileName);
    TextWriter writer = (TextWriter) new StreamWriter((Stream) fileStream);
    writer.WriteLine("Version 2;");
    writer.WriteLine("");
    int index = 0;
    if (0 < this.m_Sections.Count)
    {
      do
      {
        this.m_Sections[index].Save(writer);
        ++index;
      }
      while (index < this.m_Sections.Count);
    }
    writer.Close();
    fileStream.Close();
  }

  public void AddSection(Section section)
  {
    this.m_Sections.Add(section);
    section.TNGFile = this;
  }

  public int SectionCount => this.m_Sections.Count;

  public Section get_Sections(string name)
  {
    int index = 0;
    if (0 < this.m_Sections.Count)
    {
      while (!(this.m_Sections[index].Name == name))
      {
        ++index;
        if (index >= this.m_Sections.Count)
          goto label_4;
      }
      return this.m_Sections[index];
    }
label_4:
    return (Section) null;
  }

  public Section get_Sections(int index) => this.m_Sections[index];

  public TNGDefinitions Definitions => this.m_Definitions;

  public virtual bool Modified
  {
    [return: MarshalAs(UnmanagedType.U1)] get
    {
      int index = 0;
      if (0 < this.m_Sections.Count)
      {
        while (!this.m_Sections[index].Modified)
        {
          ++index;
          if (index >= this.m_Sections.Count)
            goto label_4;
        }
        return true;
      }
label_4:
      return false;
    }
    [param: MarshalAs(UnmanagedType.U1)] set
    {
      int index = 0;
      if (0 >= this.m_Sections.Count)
        return;
      do
      {
        this.m_Sections[index].Modified = value;
        ++index;
      }
      while (index < this.m_Sections.Count);
    }
  }

  public string FileName => this.m_FileName;

  public Thing FindThing(string uid)
  {
    int index = 0;
    if (0 < this.m_Sections.Count)
    {
      Thing thing;
      do
      {
        thing = this.m_Sections[index].FindThing(uid);
        if (thing == null)
          ++index;
        else
          goto label_3;
      }
      while (index < this.m_Sections.Count);
      goto label_4;
label_3:
      return thing;
    }
label_4:
    return (Thing) null;
  }

  protected override void ParseArgument(string argument, string value)
  {
    switch (this.m_Parser)
    {
      case TNGFile.ParserState.Basic:
        if (!(argument == "XXXSectionStart"))
          break;
        this.AddSection(new Section(value));
        this.m_Parser = TNGFile.ParserState.Section;
        break;
      case TNGFile.ParserState.Section:
        Section section = this.m_Sections[this.m_Sections.Count - 1];
        if (!(argument == "NewThing"))
          break;
        this.m_Thing = new Thing(value);
        this.m_Parser = TNGFile.ParserState.Thing;
        break;
      case TNGFile.ParserState.Thing:
        if (argument == "DefinitionType")
        {
          this.m_Thing.BeginCreate(this.m_Definitions, value);
          break;
        }
        this.m_Thing.ApplyVariable(this.m_Definitions, argument, value);
        break;
      case TNGFile.ParserState.CTC:
        CTCBlock ctc = this.m_CTC;
        if (ctc == null)
          break;
        this.m_Thing.ApplyCTCVariable(this.m_Definitions, ctc, argument, value);
        break;
    }
  }

  protected override void ParseCommand(string command)
  {
    switch (this.m_Parser)
    {
      case TNGFile.ParserState.Section:
        if (!(command == "XXXSectionEnd"))
          break;
        this.m_Parser = TNGFile.ParserState.Basic;
        break;
      case TNGFile.ParserState.Thing:
        if (command.StartsWith("StartCTC"))
        {
          TNGFile tngFile = this;
          tngFile.m_CTC = tngFile.m_Thing.ApplyCTC(this.m_Definitions, command.Substring(5));
          this.m_Parser = TNGFile.ParserState.CTC;
          break;
        }
        if (!(command == "EndThing"))
          break;
        Section section = this.m_Sections[this.m_Sections.Count - 1];
        this.m_Thing.EndCreate(this.m_Definitions);
        Thing thing = this.m_Thing;
        section.AddThing(thing);
        this.m_Parser = TNGFile.ParserState.Section;
        break;
      case TNGFile.ParserState.CTC:
        if (!command.StartsWith("EndCTC"))
          break;
        this.m_Parser = TNGFile.ParserState.Thing;
        break;
    }
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007ETNGFile();
      }
      finally
      {
        base.Dispose(true);
      }
    }
    else
      base.Dispose(false);
  }

  protected enum ParserState
  {
    Basic,
    Section,
    Thing,
    CTC,
  }
}
