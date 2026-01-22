// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.ThemeManager
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using FableMod.BIN;
using FableMod.ContentManagement;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.Gfx.Integration;

public class ThemeManager(TextureManager textureManager, ModelManager modelManager) : IDisposable
{
  private Dictionary<string, Theme> m_Themes;
  private TextureManager m_TextureManager = textureManager;
  private ModelManager m_ModelManager = modelManager;

  private void \u007EThemeManager()
  {
    this.m_Themes.Clear();
    this.m_Themes = (Dictionary<string, Theme>) null;
    this.m_TextureManager = (TextureManager) null;
    this.m_ModelManager = (ModelManager) null;
  }

  public void Clear() => this.m_Themes.Clear();

  public Theme get_Themes(string name)
  {
    Theme themes = (Theme) null;
    if (this.m_Themes.TryGetValue(name, out themes))
      return themes;
    ContentManager instance = ContentManager.Instance;
    ContentObject entry1 = instance.FindEntry(LinkDestination.GameBINEntryName, (object) name);
    if (entry1 != null)
    {
      BINEntry entry2 = (BINEntry) entry1.Object;
      DefinitionType definition = instance.Definitions.GetDefinition(entry2.Definition);
      if (definition != null)
      {
        Theme theme = new Theme();
        this.m_Themes[name] = theme;
        definition.ReadIn(entry2);
        Control control1 = definition.FindControl(1064670001U);
        if (control1 != null)
        {
          Member member = (Member) control1.Members[0];
          theme.Texture = this.m_TextureManager.Get(uint.Parse(member.Value.ToString()));
        }
        Control control2 = definition.FindControl(3899355852U);
        if (control2 != null && uint.Parse(((Member) control2.Members[0]).Value.ToString()) == 1799U)
          this.FindModel(theme, 1799U);
        definition.Dispose();
        return theme;
      }
    }
    return (Theme) null;
  }

  private void FindModel(Theme theme, uint entry)
  {
    ContentManager instance = ContentManager.Instance;
    ContentObject entry1 = instance.FindEntry(LinkDestination.GameBINEntryID, (object) entry);
    if (entry1 == null)
      return;
    BINEntry entry2 = (BINEntry) entry1.Object;
    DefinitionType definition = instance.Definitions.GetDefinition(entry2.Definition);
    if (definition == null)
      return;
    definition.ReadIn(entry2);
    Control control = definition.FindControl(3309954640U);
    if (control != null)
    {
      uint id = uint.Parse(((Member) ((ArrayMember) ((ArrayMember) control.Members[1]).Elements[0][5]).Elements[0][1]).Value.ToString());
      theme.Model = this.m_ModelManager.Get(id);
    }
    definition.Dispose();
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EThemeManager();
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
