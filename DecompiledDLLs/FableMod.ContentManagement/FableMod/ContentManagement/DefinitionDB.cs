// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.DefinitionDB
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace FableMod.ContentManagement;

public class DefinitionDB : IDisposable
{
  private Collection<Control> m_Controls = new Collection<Control>();
  private Dictionary<string, DefinitionType> m_Definitions = new Dictionary<string, DefinitionType>();
  private string m_Version;
  private static bool s_DeveloperMode = false;

  private void \u007EDefinitionDB()
  {
    this.m_Controls.Clear();
    this.m_Definitions.Clear();
  }

  public void Clear()
  {
    this.m_Controls.Clear();
    this.m_Definitions.Clear();
  }

  public void Load(string filename)
  {
    XmlDocument defdoc = new XmlDocument();
    defdoc.Load(filename);
    this.LoadDefs(defdoc);
  }

  public DefinitionType GetDefinition(string name)
  {
    DefinitionType deftype = (DefinitionType) null;
    return this.m_Definitions.TryGetValue(name, out deftype) ? new DefinitionType(deftype) : (DefinitionType) null;
  }

  public Control get_Controls(string name)
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
      return new Control(this.m_Controls[index]);
    }
label_4:
    return (Control) null;
  }

  public Control get_Controls(uint index)
  {
    int index1 = 0;
    if (0 < this.m_Controls.Count)
    {
      while ((int) this.m_Controls[index1].ID != (int) index)
      {
        ++index1;
        if (index1 >= this.m_Controls.Count)
          goto label_4;
      }
      return new Control(this.m_Controls[index1]);
    }
label_4:
    return (Control) null;
  }

  public DefinitionType[] GetDefinitions()
  {
    DefinitionType[] definitions = new DefinitionType[this.m_Definitions.Count];
    int index = 0;
    Dictionary<string, DefinitionType>.Enumerator enumerator = this.m_Definitions.GetEnumerator();
    if (enumerator.MoveNext())
    {
      do
      {
        KeyValuePair<string, DefinitionType> current = enumerator.Current;
        definitions[index] = current.Value;
        ++index;
      }
      while (enumerator.MoveNext());
    }
    return definitions;
  }

  public string Version => this.m_Version;

  public static void EnableDeveloperMode([MarshalAs(UnmanagedType.U1)] bool on)
  {
    DefinitionDB.s_DeveloperMode = on;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public static bool DeveloperModeEnabled() => DefinitionDB.s_DeveloperMode;

  private void LoadDefs(XmlDocument defdoc)
  {
    XmlNode root = (XmlNode) defdoc["DefinitionListing"];
    XmlAttribute attribute = root.Attributes["version"];
    this.m_Version = attribute == null ? "UNKNOWN VERSION" : attribute.InnerText;
    if (root == null)
      return;
    this.LoadControlStructures(root);
    this.LoadDefinitionTypes(root);
  }

  private void LoadMemberCollection(XmlNode firstmem, MemberCollection mc)
  {
    XmlNode memberNode = firstmem;
    if (firstmem == null)
      return;
    do
    {
      if (memberNode.Name == "Member")
      {
        Member member = new Member();
        member.Load(memberNode);
        mc.Add((BaseMember) member);
      }
      else if (memberNode.Name == "Array")
      {
        XmlAttribute attribute1 = memberNode.Attributes["name"];
        XmlAttribute attribute2 = memberNode.Attributes["comments"];
        XmlAttribute attribute3 = memberNode.Attributes["elementcount"];
        string comments = (string) null;
        if (attribute2 != null)
          comments = attribute2.InnerText;
        Member memberByName = mc.GetMemberByName(attribute3.InnerText);
        ArrayMember member = new ArrayMember(attribute1.InnerText, comments, memberByName);
        this.LoadMemberCollection(memberNode.FirstChild, member.ElementMembers);
        mc.Add((BaseMember) member);
      }
      memberNode = memberNode.NextSibling;
    }
    while (memberNode != null);
  }

  private void LoadControlStructures(XmlNode root)
  {
    XmlNode xmlNode1 = (XmlNode) root["ControlStructures"];
    if (xmlNode1 == null)
      return;
    XmlNode xmlNode2 = xmlNode1.FirstChild;
    if (xmlNode2 == null)
      return;
    do
    {
      if (xmlNode2.Name == "Control")
      {
        XmlAttribute attribute = xmlNode2.Attributes["name"];
        Control control = new Control(uint.Parse(xmlNode2.Attributes["id"].InnerText, NumberStyles.AllowHexSpecifier), attribute.InnerText);
        this.LoadMemberCollection(xmlNode2.FirstChild, control.Members);
        this.m_Controls.Add(control);
      }
      xmlNode2 = xmlNode2.NextSibling;
    }
    while (xmlNode2 != null);
  }

  private void LoadDefinitionTypes(XmlNode root)
  {
    XmlNode xmlNode = (XmlNode) root["DefinitionTypes"];
    if (xmlNode == null)
      return;
    XmlNode defNode = xmlNode.FirstChild;
    if (defNode == null)
      return;
    do
    {
      if (defNode.Name == "DefinitionType")
      {
        DefinitionType definitionType = new DefinitionType();
        definitionType.Load(this, defNode);
        this.m_Definitions[definitionType.Name] = definitionType;
      }
      defNode = defNode.NextSibling;
    }
    while (defNode != null);
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EDefinitionDB();
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
