// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.ContentManager
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using FableMod.BIG;
using FableMod.BIN;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.ContentManagement;

public abstract class ContentManager : IDisposable
{
  private static ContentManager sm_Instance;
  private Dictionary<string, DefinitionType> m_ObjectDefTypes = new Dictionary<string, DefinitionType>();
  private BINFile m_Objects;
  private BINFile m_Scripts;
  private NamesBINFile m_Names;
  private BIGBank m_GraphicsBank;
  private BIGFile m_Textures;
  private BIGBank m_TextBank;
  private BIGBank m_FrontEndTextureBank;

  private void \u007EContentManager()
  {
    this.m_ObjectDefTypes = (Dictionary<string, DefinitionType>) null;
  }

  public virtual ContentObject FindEntry(LinkDestination dst, object @object)
  {
    switch (dst)
    {
      case LinkDestination.ModelID:
        if (this.AutoLoadGraphics())
        {
          AssetEntry entryById = this.m_GraphicsBank.FindEntryByID(uint.Parse(@object.ToString()));
          if (entryById != null)
            return new ContentObject(entryById.DevSymbolName, (object) entryById, ContentType.Graphics);
          break;
        }
        break;
      case LinkDestination.ModelName:
        if (this.AutoLoadGraphics())
        {
          AssetEntry entryBySymbolName = this.m_GraphicsBank.FindEntryBySymbolName(@object.ToString());
          if (entryBySymbolName != null)
            return new ContentObject(entryBySymbolName.DevSymbolName, (object) entryBySymbolName, ContentType.Graphics);
          break;
        }
        break;
      case LinkDestination.NamesBINOffset:
        if (this.m_Names == null)
        {
          ContentManager contentManager = this;
          contentManager.m_Names = contentManager.LoadNames();
        }
        if ((this.m_Names != null ? 1 : 0) == 0)
          return (ContentObject) null;
        NamesBINEntry entryByOffset = this.m_Names.GetEntryByOffset(uint.Parse(@object.ToString()));
        if (entryByOffset != null)
          return new ContentObject(entryByOffset.Name, (object) entryByOffset, ContentType.Names);
        break;
      case LinkDestination.NamesBINEnum:
        if (!this.AutoLoadNames())
          return (ContentObject) null;
        NamesBINEntry entryByEnum = this.m_Names.GetEntryByEnum(uint.Parse(@object.ToString()));
        if (entryByEnum != null)
          return new ContentObject(entryByEnum.Name, (object) entryByEnum, ContentType.Names);
        break;
      case LinkDestination.GameBINEntryID:
        if (this.AutoLoadObjects())
        {
          int index = int.Parse(@object.ToString());
          if (index >= 0)
          {
            BINEntry binEntry = this.m_Objects.get_Entries(index);
            if (binEntry != null)
              return new ContentObject(binEntry.Name, (object) binEntry, ContentType.Objects);
            break;
          }
          break;
        }
        break;
      case LinkDestination.GameBINEntryName:
        if (this.AutoLoadObjects())
        {
          BINEntry entryByName = this.m_Objects.GetEntryByName(@object.ToString());
          if (entryByName != null)
            return new ContentObject(entryByName.Name, (object) entryByName, ContentType.Objects);
          break;
        }
        break;
      case LinkDestination.ScriptBINEntryID:
        if (this.AutoLoadScripts())
        {
          int index = int.Parse(@object.ToString());
          if (index >= 0)
          {
            BINEntry binEntry = this.m_Scripts.get_Entries(index);
            if (binEntry != null)
              return new ContentObject(binEntry.Name, (object) binEntry, ContentType.Scripts);
            break;
          }
          break;
        }
        break;
      case LinkDestination.MainTextureID:
        if (this.AutoLoadTextures())
        {
          AssetEntry assetEntry = (AssetEntry) null;
          try
          {
            assetEntry = this.MainTextureBank.FindEntryByID(uint.Parse(@object.ToString()));
          }
          catch (Exception ex)
          {
          }
          if (assetEntry != null)
            return new ContentObject(assetEntry.DevSymbolName, (object) assetEntry, ContentType.MainTextures);
          break;
        }
        break;
      case LinkDestination.MainTextureName:
        if (this.AutoLoadTextures())
        {
          AssetEntry entryBySymbolName = this.MainTextureBank.FindEntryBySymbolName(@object.ToString());
          if (entryBySymbolName != null)
            return new ContentObject(entryBySymbolName.DevSymbolName, (object) entryBySymbolName, ContentType.MainTextures);
          break;
        }
        break;
      case LinkDestination.FrontEndTextureID:
        if (this.AutoLoadFrontEndTextures())
        {
          AssetEntry assetEntry = (AssetEntry) null;
          try
          {
            assetEntry = this.m_FrontEndTextureBank.FindEntryByID(uint.Parse(@object.ToString()));
          }
          catch (Exception ex)
          {
          }
          if (assetEntry != null)
            return new ContentObject(assetEntry.DevSymbolName, (object) assetEntry, ContentType.FrontEndTextures);
          break;
        }
        break;
      case LinkDestination.TextID:
      case LinkDestination.TextGroupID:
        if (this.AutoLoadText())
        {
          AssetEntry entryById = this.m_TextBank.FindEntryByID(uint.Parse(@object.ToString()));
          if (entryById != null)
            return new ContentObject(entryById.DevSymbolName, (object) entryById, ContentType.Text);
          break;
        }
        break;
    }
    return (ContentObject) null;
  }

  public virtual void ShowEntry(LinkDestination destination, object @object, [MarshalAs(UnmanagedType.U1)] bool dialog)
  {
    ContentObject entry = this.FindEntry(destination, @object);
    if (entry == null)
      return;
    this.ShowEntry(entry.Object, dialog);
  }

  public abstract void ShowEntry(object @object, [MarshalAs(UnmanagedType.U1)] bool dialog);

  public abstract object SelectEntry(Link link, object current);

  public virtual AssetEntry UpdateEntry(ContentType contentType, AssetEntry entry)
  {
    switch (contentType)
    {
      case ContentType.Graphics:
        if (this.AutoLoadGraphics())
        {
          ContentManager contentManager = this;
          return contentManager.UpdateBIGEntry(contentManager.GraphicsBank, entry);
        }
        break;
      case ContentType.Text:
        if (this.AutoLoadText())
        {
          ContentManager contentManager = this;
          return contentManager.UpdateBIGEntry(contentManager.TextBank, entry);
        }
        break;
      case ContentType.MainTextures:
        if (this.AutoLoadTextures())
        {
          ContentManager contentManager = this;
          return contentManager.UpdateBIGEntry(contentManager.MainTextureBank, entry);
        }
        break;
      case ContentType.GUITextures:
        if (this.AutoLoadTextures())
        {
          ContentManager contentManager = this;
          return contentManager.UpdateBIGEntry(contentManager.GUITextureBank, entry);
        }
        break;
      case ContentType.FrontEndTextures:
        if (this.AutoLoadFrontEndTextures())
        {
          ContentManager contentManager = this;
          return contentManager.UpdateBIGEntry(contentManager.FrontEndTextureBank, entry);
        }
        break;
    }
    return (AssetEntry) null;
  }

  public virtual AssetLinker CreateAssetLinker(ContentType contentType, AssetEntry entry)
  {
    return (AssetLinker) null;
  }

  public DefinitionType FindObjectDefinitionType(string objectName)
  {
    ContentObject entry1 = this.FindEntry(LinkDestination.GameBINEntryName, (object) objectName);
    if (entry1 == null)
      return (DefinitionType) null;
    BINEntry entry2 = (BINEntry) entry1.Object;
    DefinitionType objectDefinitionType = (DefinitionType) null;
    if (this.m_ObjectDefTypes.TryGetValue(objectName, out objectDefinitionType))
      return objectDefinitionType;
    objectDefinitionType = this.Definitions.GetDefinition(entry2.Definition);
    if (objectDefinitionType != null)
    {
      objectDefinitionType.ReadIn(entry2);
      this.m_ObjectDefTypes[objectName] = objectDefinitionType;
    }
    return objectDefinitionType;
  }

  public abstract DefinitionDB Definitions { get; }

  public abstract string DataDirectory { get; }

  public static ContentManager Instance => ContentManager.sm_Instance;

  public BIGFile Textures
  {
    get
    {
      this.AutoLoadTextures();
      return this.m_Textures;
    }
  }

  public BIGBank MainTextureBank
  {
    get
    {
      this.AutoLoadTextures();
      ContentManager contentManager = this;
      return contentManager.GetMainTextureBank(contentManager.m_Textures);
    }
  }

  public BIGBank GUITextureBank
  {
    get
    {
      this.AutoLoadTextures();
      ContentManager contentManager = this;
      return contentManager.GetGUITextureBank(contentManager.m_Textures);
    }
  }

  public BINFile Scripts
  {
    get
    {
      this.AutoLoadScripts();
      return this.m_Scripts;
    }
  }

  public BIGBank GraphicsBank
  {
    get
    {
      this.AutoLoadGraphics();
      return this.m_GraphicsBank;
    }
  }

  public BIGBank TextBank
  {
    get
    {
      this.AutoLoadText();
      return this.m_TextBank;
    }
  }

  public BIGBank FrontEndTextureBank
  {
    get
    {
      this.AutoLoadFrontEndTextures();
      return this.m_FrontEndTextureBank;
    }
  }

  public BINFile Objects
  {
    get
    {
      if (this.m_Objects == null)
      {
        ContentManager contentManager = this;
        contentManager.m_Objects = contentManager.LoadObjects();
      }
      return this.m_Objects;
    }
  }

  public NamesBINFile Names => this.m_Names;

  protected static void SetInstance(ContentManager manager) => ContentManager.sm_Instance = manager;

  protected void ResetBIG()
  {
    this.m_GraphicsBank = (BIGBank) null;
    this.m_TextBank = (BIGBank) null;
    this.m_Textures = (BIGFile) null;
    this.m_FrontEndTextureBank = (BIGBank) null;
  }

  protected abstract BIGBank LoadGraphics();

  protected abstract BIGFile LoadTextures();

  protected abstract NamesBINFile LoadNames();

  protected abstract BINFile LoadObjects();

  protected abstract BINFile LoadScripts();

  protected abstract BIGBank LoadText();

  protected abstract BIGBank LoadFrontEndTextures();

  protected abstract BIGBank GetMainTextureBank(BIGFile textures);

  protected abstract BIGBank GetGUITextureBank(BIGFile textures);

  [return: MarshalAs(UnmanagedType.U1)]
  protected bool AutoLoadGraphics()
  {
    if (this.m_GraphicsBank == null)
    {
      ContentManager contentManager = this;
      contentManager.m_GraphicsBank = contentManager.LoadGraphics();
    }
    return this.m_GraphicsBank != null;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected bool AutoLoadTextures()
  {
    if (this.m_Textures == null)
    {
      ContentManager contentManager = this;
      contentManager.m_Textures = contentManager.LoadTextures();
    }
    return this.m_Textures != null;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected bool AutoLoadNames()
  {
    if (this.m_Names == null)
    {
      ContentManager contentManager = this;
      contentManager.m_Names = contentManager.LoadNames();
    }
    return this.m_Names != null;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected bool AutoLoadObjects()
  {
    if (this.m_Objects == null)
    {
      ContentManager contentManager = this;
      contentManager.m_Objects = contentManager.LoadObjects();
    }
    return this.m_Objects != null;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected bool AutoLoadText()
  {
    if (this.m_TextBank == null)
    {
      ContentManager contentManager = this;
      contentManager.m_TextBank = contentManager.LoadText();
    }
    return this.m_TextBank != null;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected bool AutoLoadScripts()
  {
    if (this.m_Scripts == null)
    {
      ContentManager contentManager = this;
      contentManager.m_Scripts = contentManager.LoadScripts();
    }
    return this.m_Scripts != null;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected bool AutoLoadFrontEndTextures()
  {
    if (this.m_FrontEndTextureBank == null)
    {
      ContentManager contentManager = this;
      contentManager.m_FrontEndTextureBank = contentManager.LoadFrontEndTextures();
    }
    return this.m_FrontEndTextureBank != null;
  }

  private AssetEntry UpdateBIGEntry(BIGBank bank, AssetEntry newEntry)
  {
    AssetEntry entry = bank.FindEntryBySymbolName(newEntry.DevSymbolName);
    if (entry != null)
    {
      entry.Copy(newEntry);
    }
    else
    {
      entry = new AssetEntry(newEntry, (AssetBank) bank);
      entry.ID = bank.GetNewID();
      bank.AddEntry(entry);
    }
    return entry;
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EContentManager();
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
