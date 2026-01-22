// Decompiled with JetBrains decompiler
// Type: FableMod.TNG.Thing
// Assembly: FableMod.TNG, Version=1.0.4918.434, Culture=neutral, PublicKeyToken=null
// MVID: D9131661-A628-42D1-B5F7-4150ACB2CB8F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.TNG.dll

using FableMod.ContentManagement;
using System.Collections.ObjectModel;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.TNG;

public class Thing : TNGDefinitionType
{
  protected new string m_Graphic;
  protected string m_DefType;
  protected string m_UID;
  protected int m_Player;
  protected bool m_Modified = false;
  protected bool m_LockUpdate = false;
  protected ThingInterface m_Interface;
  protected Section m_Section;
  protected FableMod.ContentManagement.DefinitionType m_BINDefType;

  public Thing(string name)
  {
    // ISSUE: fault handler
    try
    {
      this.m_Name = name;
    }
    __fault
    {
      base.Dispose(true);
    }
  }

  private void \u007EThing()
  {
    this.Destroy();
    this.m_UID = (string) null;
    this.m_DefType = (string) null;
  }

  public override void Destroy()
  {
    this.Clear();
    ThingInterface thingInterface = this.m_Interface;
    if (thingInterface != null)
    {
      thingInterface.Destroy();
      this.m_Interface = (ThingInterface) null;
    }
    this.m_BINDefType = (FableMod.ContentManagement.DefinitionType) null;
  }

  public override void Save(TextWriter writer)
  {
    writer.WriteLine("NewThing {0};", (object) this.m_Name);
    writer.WriteLine("Player {0};", (object) this.m_Player);
    writer.WriteLine("UID {0};", (object) this.m_UID);
    writer.WriteLine("DefinitionType \"{0}\";", (object) this.m_DefType);
    base.Save(writer);
    writer.WriteLine("EndThing;");
    writer.WriteLine("");
  }

  public void Create(TNGDefinitions definitions, string defType)
  {
    this.BeginCreate(definitions, defType);
    FableMod.ContentManagement.DefinitionType binDefType = this.m_BINDefType;
    if (binDefType == null)
      return;
    this.AddDefinitionTypeCTCs(definitions, binDefType);
  }

  public void BeginCreate(TNGDefinitions definitions, string defType)
  {
    if (defType.StartsWith("\"") && defType.EndsWith("\""))
      defType = defType.Substring(1, defType.Length - 2);
    this.m_DefType = defType;
    this.m_BINDefType = ContentManager.Instance.FindObjectDefinitionType(this.m_DefType);
    TNGDefinitionType tngDefinitionType = (TNGDefinitionType) definitions.Find(this.m_DefType);
    if (tngDefinitionType == null)
    {
      FableMod.ContentManagement.DefinitionType binDefType = this.m_BINDefType;
      if (binDefType != null)
        tngDefinitionType = (TNGDefinitionType) definitions.Find(binDefType.Name);
      if (tngDefinitionType == null)
      {
        string name = this.m_Name;
        tngDefinitionType = (TNGDefinitionType) definitions.Find(name);
        if (tngDefinitionType == null)
          return;
      }
    }
    string name1 = this.m_Name;
    FableMod.ContentManagement.DefinitionType binDefType1 = this.m_BINDefType;
    tngDefinitionType.CopyTo((Element) this);
    this.m_Name = name1;
    this.m_BINDefType = binDefType1;
  }

  public CTCBlock ApplyCTC(TNGDefinitions definitions, string name)
  {
    if (definitions == null)
    {
      Section section = this.m_Section;
      if (section != null)
        definitions = section.TNGFile.Definitions;
    }
    CTCBlock ctcBlock1 = (CTCBlock) this.Find(name);
    if (ctcBlock1 != null)
      return ctcBlock1;
    CTCBlock ctcBlock2 = (CTCBlock) definitions.Find(name);
    CTCBlock type;
    if (ctcBlock2 != null)
    {
      type = (CTCBlock) ctcBlock2.Duplicate();
    }
    else
    {
      type = new CTCBlock();
      type.m_Name = name;
    }
    this.Add((Element) type);
    return type;
  }

  public void ApplyVariable(TNGDefinitions definitions, string argument, string value)
  {
    switch (argument)
    {
      case "UID":
        UIDManager.Add(value);
        this.m_UID = value;
        break;
      case "Player":
        this.m_Player = int.Parse(value);
        break;
      default:
        Element element1 = this.Find(argument);
        if (element1 != null)
        {
          if (element1.GetType() == typeof (ElementArray))
          {
            Element element2 = ((ElementArray) element1).Add();
            if (!(element2.GetType() == typeof (Variable)))
              break;
            ((Variable) element2).SetRawValue((object) value);
            break;
          }
          if (element1.GetType() == typeof (Variable))
          {
            ((Variable) element1).SetRawValue((object) value);
            break;
          }
        }
        Variable type = new Variable();
        type.m_Name = argument;
        type.SetRawValue((object) value);
        this.Add((Element) type);
        break;
    }
  }

  public void ApplyCTCVariable(
    TNGDefinitions definitions,
    CTCBlock ctc,
    string argument,
    string value)
  {
    Variable variable = (Variable) ctc.Find(argument);
    if (variable != null)
    {
      variable.SetRawValue((object) value);
    }
    else
    {
      Variable type = new Variable();
      type.m_Name = argument;
      type.SetRawValue((object) value);
      ctc.Add((Element) type);
    }
  }

  public void EndCreate(TNGDefinitions definitions)
  {
    FableMod.ContentManagement.DefinitionType binDefType = this.m_BINDefType;
    if (binDefType == null)
      return;
    this.AddDefinitionTypeCTCs(definitions, binDefType);
  }

  public void LockUpdate() => this.m_LockUpdate = true;

  public void UnlockUpdate() => this.m_LockUpdate = false;

  public void Detach()
  {
    this.m_Section?.RemoveThing(this);
    this.m_Interface?.Update();
  }

  public override void CopyTo(Element element)
  {
    base.CopyTo(element);
    Thing thing = (Thing) element;
    thing.m_UID = UIDManager.Generate();
    thing.m_Player = this.m_Player;
    thing.m_DefType = this.m_DefType;
    thing.m_BINDefType = this.m_BINDefType;
    thing.m_Section = this.m_Section;
    thing.m_Modified = this.m_Modified;
    thing.m_Section?.AddThing(thing);
  }

  public Collection<CTCBlock> CTCs
  {
    get
    {
      Collection<CTCBlock> ctCs = new Collection<CTCBlock>();
      int index = 0;
      if (0 < this.ElementCount)
      {
        do
        {
          if (this.get_Elements(index).GetType() == typeof (CTCBlock))
            ctCs.Add((CTCBlock) this.get_Elements(index));
          ++index;
        }
        while (index < this.ElementCount);
      }
      return ctCs;
    }
  }

  public CTCBlock get_CTCs(string name) => (CTCBlock) this.Find(name);

  public string DefinitionType
  {
    get => this.m_DefType;
    set
    {
      if (value.StartsWith("\"") && value.EndsWith("\""))
        value = value.Substring(1, value.Length - 2);
      if (!(value != this.m_DefType))
        return;
      this.Modified = true;
      this.m_DefType = value;
    }
  }

  public string UID
  {
    get => this.m_UID;
    set
    {
      if (!(value != this.m_UID))
        return;
      this.Modified = true;
      this.m_UID = value;
    }
  }

  public ThingInterface Interface
  {
    get => this.m_Interface;
    set => this.m_Interface = value;
  }

  public int Player
  {
    get => this.m_Player;
    set
    {
      if (value == this.m_Player)
        return;
      this.Modified = true;
      this.m_Player = value;
    }
  }

  public override bool Modified
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_Modified || base.Modified;
    [param: MarshalAs(UnmanagedType.U1)] set
    {
      this.m_Modified = value;
      base.Modified = value;
    }
  }

  public Section Section
  {
    get => this.m_Section;
    set => this.m_Section = value;
  }

  public FableMod.ContentManagement.DefinitionType BinDefinitionType => this.m_BINDefType;

  protected void AddDefinitionTypeCTCs(TNGDefinitions definitions, FableMod.ContentManagement.DefinitionType defType)
  {
    ContentManager instance = ContentManager.Instance;
    Control control = defType.FindControl(1537122114U);
    if (control == null)
      return;
    ArrayMember member1 = (ArrayMember) control.Members["CTC List"];
    if (member1 == null)
      return;
    int index = 0;
    if (0 >= member1.Elements.Count)
      return;
    do
    {
      MemberCollection element = member1.Elements[index];
      if (element.Count > 0)
      {
        object obj = (object) element[0];
        if (obj.GetType() == typeof (Member))
        {
          Member member2 = (Member) obj;
          if (member2.Link != null)
          {
            ContentObject entry = instance.FindEntry(member2.Link.To, member2.Value);
            if (entry != null && this.Find(entry.Name) == null)
            {
              CTCBlock ctcBlock = (CTCBlock) definitions.Find(entry.Name);
              if (ctcBlock != null && this.Find(entry.Name) == null)
                this.Add(ctcBlock.Duplicate());
            }
          }
        }
      }
      ++index;
    }
    while (index < member1.Elements.Count);
  }

  protected override void HandleChange()
  {
    if (!this.m_LockUpdate)
      this.m_Interface?.Update();
    base.HandleChange();
  }

  protected override Element Factory() => (Element) new Thing(this.m_Name);

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EThing();
      }
      finally
      {
        base.Dispose(true);
      }
    }
    else
      base.Dispose(false);
  }
}
