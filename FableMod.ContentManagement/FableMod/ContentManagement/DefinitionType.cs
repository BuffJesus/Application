// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.DefinitionType
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using FableMod.BIN;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

#nullable disable
namespace FableMod.ContentManagement;

public class DefinitionType : IDisposable
{
  private bool \u003Cbacking_store\u003EBool1;
  private bool \u003Cbacking_store\u003EBool2;
  private bool \u003Cbacking_store\u003EBool3;
  private Member m_CDefCount;
  private ArrayMember m_CDefs;
  private bool m_HasCDefListing;
  private string m_Name;
  private Collection<Control> m_Controls;
  private ReadOnlyCollection<Control> m_ReadOnlyControls;

  public DefinitionType([MarshalAs(UnmanagedType.U1)] bool hascdeflisting, string name)
  {
    this.m_HasCDefListing = hascdeflisting;
    this.m_Name = name;
    Collection<Control> list = new Collection<Control>();
    this.m_Controls = list;
    this.m_ReadOnlyControls = new ReadOnlyCollection<Control>((IList<Control>) list);
    // ISSUE: explicit constructor call
    base.\u002Ector();
    this.CreateCDefs();
  }

  public DefinitionType(DefinitionType deftype)
  {
    this.m_HasCDefListing = deftype.m_HasCDefListing;
    this.m_Name = deftype.m_Name;
    Collection<Control> list = new Collection<Control>();
    this.m_Controls = list;
    this.m_ReadOnlyControls = new ReadOnlyCollection<Control>((IList<Control>) list);
    // ISSUE: explicit constructor call
    base.\u002Ector();
    int index = 0;
    if (0 < deftype.m_ReadOnlyControls.Count)
    {
      ReadOnlyCollection<Control> readOnlyControls;
      do
      {
        // ISSUE: explicit non-virtual call
        this.m_Controls.Add(new Control(__nonvirtual (deftype.m_ReadOnlyControls[index])));
        ++index;
        readOnlyControls = deftype.m_ReadOnlyControls;
      }
      while (index < readOnlyControls.Count);
    }
    if (!this.m_HasCDefListing)
      return;
    Member count = new Member(MemberType.USHORT, "CDefCount", "", (Link) null);
    this.m_CDefCount = count;
    this.m_CDefs = new ArrayMember(nameof (CDefs), (string) null, count);
    this.m_CDefs.ElementMembers.Add((BaseMember) new Member(MemberType.UINT, "CDefName", "", new Link(LinkDestination.NamesBINEnum, "^C.*Def")));
    this.m_CDefs.ElementMembers.Add((BaseMember) new Member(MemberType.INT, "CDefDataEntry", "", new Link(LinkDestination.GameBINEntryID, "^C.*Def")));
    this.m_CDefs.ElementMembers.Add((BaseMember) new Member(MemberType.INT, "InheritedEntryID", "", new Link(LinkDestination.GameBINEntryID, "^" + this.m_Name)));
  }

  public DefinitionType()
  {
    Collection<Control> list = new Collection<Control>();
    this.m_Controls = list;
    this.m_ReadOnlyControls = new ReadOnlyCollection<Control>((IList<Control>) list);
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  private void \u007EDefinitionType()
  {
    this.m_Controls = (Collection<Control>) null;
    this.m_ReadOnlyControls = (ReadOnlyCollection<Control>) null;
    this.m_CDefCount = (Member) null;
    this.m_CDefs = (ArrayMember) null;
  }

  private void \u0021DefinitionType()
  {
  }

  public void Load(DefinitionDB database, XmlNode defNode)
  {
    XmlAttribute attribute1 = defNode.Attributes["name"];
    this.m_HasCDefListing = bool.Parse(defNode.Attributes["hasCDefListing"].InnerText);
    this.m_Name = attribute1.InnerText;
    XmlNode xmlNode = defNode.FirstChild;
    if (xmlNode != null)
    {
      XmlAttribute attribute2;
      XmlAttribute attribute3;
      do
      {
        if (xmlNode.Name == "ControlInstance")
        {
          attribute2 = xmlNode.Attributes["id"];
          attribute3 = xmlNode.Attributes["name"];
          Control control = attribute2 == null ? database.get_Controls(attribute3.InnerText) : database.get_Controls(uint.Parse(attribute2.InnerText, NumberStyles.AllowHexSpecifier));
          if (control != null)
            this.m_Controls.Add(control);
          else
            goto label_6;
        }
        xmlNode = xmlNode.NextSibling;
      }
      while (xmlNode != null);
      return;
label_6:
      throw new Exception("Error reading DefinitionType[" + this.m_Name + "].  ControlInstance referenced non-existing Control structure.  " + (attribute2 == null ? "Name = " + attribute3.InnerText : "ID = " + attribute2.InnerText));
    }
  }

  public void AddControl(Control control) => this.m_Controls.Add(control);

  public void FixLinks(LinkDestination link, object oldValue, object newValue)
  {
    this.m_CDefs?.FixLinks(link, oldValue, newValue);
    int index1 = 0;
    if (0 >= this.m_Controls.Count)
      return;
    do
    {
      int index2 = 0;
      if (0 < this.m_Controls[index1].Members.Count)
      {
        do
        {
          this.m_Controls[index1].Members[index2].FixLinks(link, oldValue, newValue);
          ++index2;
        }
        while (index2 < this.m_Controls[index1].Members.Count);
      }
      ++index1;
    }
    while (index1 < this.m_Controls.Count);
  }

  public unsafe void ReadIn(BINEntry entry)
  {
    this.ReadIn(entry.GetData(), entry.Length, entry.IsXBox);
  }

  public unsafe int ReadIn(sbyte* data, int length, [MarshalAs(UnmanagedType.U1)] bool IsXBox)
  {
    this.\u003Cbacking_store\u003EBool1 = *data != (sbyte) 0;
    this.\u003Cbacking_store\u003EBool2 = data[1L] != (sbyte) 0;
    this.\u003Cbacking_store\u003EBool3 = data[2L] != (sbyte) 0;
    int num1 = 3;
    if (this.m_HasCDefListing)
    {
      this.m_CDefs.Elements.Clear();
      int num2 = this.m_CDefCount.ReadIn(data + 3L, length - 3) + 3;
      num1 = num2 + this.m_CDefs.ReadIn((sbyte*) ((long) num2 + (IntPtr) data), length - num2);
    }
    int index = 0;
    if (0 < this.m_Controls.Count)
    {
      do
      {
        num1 += this.m_Controls[index].ReadIn((sbyte*) ((long) num1 + (IntPtr) data), length - num1, IsXBox);
        ++index;
      }
      while (index < this.m_Controls.Count);
    }
    return num1;
  }

  public unsafe void Write(BINEntry entry)
  {
    sbyte* data = (sbyte*) \u003CModule\u003E.@new(1048576UL /*0x100000*/);
    int length = this.Write(data, 1048576 /*0x100000*/, entry.IsXBox);
    entry.SetData(data, (uint) length);
    \u003CModule\u003E.delete((void*) data);
  }

  public unsafe int Write(sbyte* data, int length, [MarshalAs(UnmanagedType.U1)] bool IsXBox)
  {
    *data = this.\u003Cbacking_store\u003EBool1 ? (sbyte) 1 : (sbyte) 0;
    data[1L] = !this.\u003Cbacking_store\u003EBool2 ? (sbyte) 0 : (sbyte) 1;
    data[2L] = !this.\u003Cbacking_store\u003EBool3 ? (sbyte) 0 : (sbyte) 1;
    int num1 = 3;
    if (this.m_HasCDefListing)
    {
      this.m_CDefs.UpdateCount();
      int num2 = this.m_CDefCount.Write(data + 3L, length - 3) + 3;
      num1 = num2 + this.m_CDefs.Write((sbyte*) ((long) num2 + (IntPtr) data), length - num2);
    }
    int index = 0;
    if (0 < this.m_Controls.Count)
    {
      do
      {
        num1 += this.m_Controls[index].Write((sbyte*) ((long) num1 + (IntPtr) data), length - num1, IsXBox);
        ++index;
      }
      while (index < this.m_Controls.Count);
    }
    return num1;
  }

  public unsafe string Print(BINEntry entry)
  {
    this.ReadIn(entry.GetData(), entry.Length, entry.IsXBox);
    StringBuilder sb = new StringBuilder();
    sb.AppendFormat("DefinitionType: {0} ({1}){2}", (object) entry.Name, (object) entry.Definition, (object) Environment.NewLine);
    int index = 0;
    if (0 < this.m_ReadOnlyControls.Count)
    {
      ReadOnlyCollection<Control> readOnlyControls;
      do
      {
        // ISSUE: explicit non-virtual call
        __nonvirtual (this.m_ReadOnlyControls[index]).Print(sb, "  ");
        ++index;
        readOnlyControls = this.m_ReadOnlyControls;
      }
      while (index < readOnlyControls.Count);
    }
    return sb.ToString();
  }

  public Control FindControl(string name)
  {
    int index = 0;
    if (0 < this.m_Controls.Count)
    {
      while (!(this.m_Controls[index].Name == name))
      {
        ++index;
        if (index >= this.m_Controls.Count)
          goto label_4;
      }
      return this.m_Controls[index];
    }
label_4:
    return (Control) null;
  }

  public Control FindControl(uint id)
  {
    int index = 0;
    if (0 < this.m_Controls.Count)
    {
      while ((int) this.m_Controls[index].ID != (int) id)
      {
        ++index;
        if (index >= this.m_Controls.Count)
          goto label_4;
      }
      return this.m_Controls[index];
    }
label_4:
    return (Control) null;
  }

  public bool HasCDefListing
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_HasCDefListing;
  }

  public string Name => this.m_Name;

  public ReadOnlyCollection<Control> Controls => this.m_ReadOnlyControls;

  public ArrayMember CDefs => this.m_CDefs;

  public CDefLink get_CDefLinks(string name)
  {
    ArrayMember cdefs = this.m_CDefs;
    if (cdefs == null)
      return (CDefLink) null;
    int index = 0;
    if (0 < cdefs.Elements.Count)
    {
      MemberCollection element;
      do
      {
        element = this.m_CDefs.Elements[index];
        if (element.Count == 3)
        {
          Member member = (Member) element[0];
          ContentObject entry = ContentManager.Instance.FindEntry(member.Link.To, member.Value);
          if (entry != null && entry.Name == name)
            goto label_6;
        }
        ++index;
      }
      while (index < this.m_CDefs.Elements.Count);
      goto label_7;
label_6:
      return new CDefLink((Member) element[1], (Member) element[2]);
    }
label_7:
    return (CDefLink) null;
  }

  public bool Bool1
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.\u003Cbacking_store\u003EBool1;
    [param: MarshalAs(UnmanagedType.U1)] set => this.\u003Cbacking_store\u003EBool1 = value;
  }

  public bool Bool2
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.\u003Cbacking_store\u003EBool2;
    [param: MarshalAs(UnmanagedType.U1)] set => this.\u003Cbacking_store\u003EBool2 = value;
  }

  public bool Bool3
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.\u003Cbacking_store\u003EBool3;
    [param: MarshalAs(UnmanagedType.U1)] set => this.\u003Cbacking_store\u003EBool3 = value;
  }

  private void CreateCDefs()
  {
    if (!this.m_HasCDefListing)
      return;
    Member count = new Member(MemberType.USHORT, "CDefCount", "", (Link) null);
    this.m_CDefCount = count;
    this.m_CDefs = new ArrayMember("CDefs", (string) null, count);
    this.m_CDefs.ElementMembers.Add((BaseMember) new Member(MemberType.UINT, "CDefName", "", new Link(LinkDestination.NamesBINEnum, "^C.*Def")));
    this.m_CDefs.ElementMembers.Add((BaseMember) new Member(MemberType.INT, "CDefDataEntry", "", new Link(LinkDestination.GameBINEntryID, "^C.*Def")));
    this.m_CDefs.ElementMembers.Add((BaseMember) new Member(MemberType.INT, "InheritedEntryID", "", new Link(LinkDestination.GameBINEntryID, "^" + this.m_Name)));
  }

  [HandleProcessCorruptedStateExceptions]
  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EDefinitionType();
    }
    else
    {
      try
      {
        this.\u0021DefinitionType();
      }
      finally
      {
        // ISSUE: explicit finalizer call
        base.Finalize();
      }
    }
  }

  public virtual void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }

  ~DefinitionType() => this.Dispose(false);
}
