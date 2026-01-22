// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.ObjectTemplate
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using FableMod.BIN;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.ContentManagement;

public class ObjectTemplate : IDisposable
{
  private List<BaseTemplate> m_Items = new List<BaseTemplate>();

  private void \u007EObjectTemplate()
  {
  }

  private unsafe void Build(BINEntry entry, DefTypeTemplate link, int* pId)
  {
    DefinitionType definition = ContentManager.Instance.Definitions.GetDefinition(entry.Definition);
    if (definition == null)
      return;
    definition.ReadIn(entry);
    ArrayMember cdefs = definition.CDefs;
    DefTypeTemplate link1 = new DefTypeTemplate();
    link1.Name = entry.Definition;
    link1.Type = entry.Definition;
    int* numPtr = pId;
    int num1 = *numPtr + 1;
    *numPtr = num1;
    int num2 = *pId;
    link1.ID = num2;
    link1.OriginalID = entry.ID;
    link1.LinkTo = link;
    byte num3 = (byte) (link == null);
    link1.Named = (bool) num3;
    this.m_Items.Add((BaseTemplate) link1);
    if (cdefs != null)
    {
      int index = 0;
      if (0 < cdefs.Elements.Count)
      {
        do
        {
          Member member1 = (Member) cdefs.Elements[index][0];
          if (ContentManager.Instance.FindEntry(member1.Link.To, member1.Value) != null)
          {
            Member member2 = (Member) cdefs.Elements[index][1];
            if (ContentManager.Instance.FindEntry(member2.Link.To, member2.Value).Object is BINEntry entry1)
              this.Build(entry1, link1, pId);
          }
          ++index;
        }
        while (index < cdefs.Elements.Count);
      }
    }
    int index1 = 0;
    if (0 < definition.Controls.Count)
    {
      do
      {
        // ISSUE: explicit non-virtual call
        Control control = __nonvirtual (definition.Controls[index1]);
        this.AddControlMembers(link1, control.Name, control.ID, control.Members, pId);
        ++index1;
      }
      while (index1 < definition.Controls.Count);
    }
    link1.Ref = true;
    int index2 = 0;
    if (0 >= this.m_Items.Count)
      return;
    do
    {
      BaseTemplate baseTemplate = this.m_Items[index2];
      if (baseTemplate.LinkTo != link1 || baseTemplate.Ref)
        ++index2;
      else
        goto label_13;
    }
    while (index2 < this.m_Items.Count);
    return;
label_13:
    link1.Ref = false;
  }

  public unsafe void Build(BINEntry entry)
  {
    this.m_Items.Clear();
    int num = 0;
    this.Build(entry, (DefTypeTemplate) null, &num);
  }

  public int ItemCount => this.m_Items.Count;

  public BaseTemplate get_Items(int index) => this.m_Items[index];

  private unsafe void AddControlMembers(
    DefTypeTemplate link,
    string controlName,
    uint control,
    MemberCollection c,
    int* pId)
  {
    int index = 0;
    if (0 >= c.Count)
      return;
    do
    {
      if (c[index].GetType() == typeof (Member))
      {
        Member member = (Member) c[index];
        if (member.Link != null)
        {
          ContentType contentType;
          if (member.Link.To == LinkDestination.ModelID)
            contentType = ContentType.Graphics;
          else if (member.Link.To == LinkDestination.MainTextureID)
            contentType = ContentType.MainTextures;
          else if (member.Link.To == LinkDestination.TextID)
            contentType = ContentType.Text;
          else
            goto label_10;
          AssetTemplate assetTemplate = new AssetTemplate();
          assetTemplate.Name = link.Name + "." + member.Name;
          assetTemplate.ControlID = control;
          assetTemplate.Element = index;
          int* numPtr = pId;
          int num1 = *numPtr + 1;
          *numPtr = num1;
          int num2 = *pId;
          assetTemplate.ID = num2;
          assetTemplate.LinkTo = link;
          assetTemplate.Named = true;
          assetTemplate.Type = contentType;
          byte num3 = (byte) (contentType == ContentType.MainTextures);
          assetTemplate.Ref = (bool) num3;
          this.m_Items.Add((BaseTemplate) assetTemplate);
        }
      }
label_10:
      ++index;
    }
    while (index < c.Count);
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
