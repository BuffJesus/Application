// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.ModPackage
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using FableMod.BIG;
using FableMod.BIN;
using FableMod.CLRCore;
using System;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization.Formatters.Binary;

#nullable disable
namespace FableMod.ContentManagement;

public class ModPackage : IDisposable
{
  private BIGFile m_Big;

  public ModPackage()
  {
    this.m_Big = new BIGFile((int) \u003CModule\u003E.\u003FA0xe3a94609\u002EFMP_VERSION);
    this.New();
  }

  private void \u007EModPackage() => this.m_Big.Destroy();

  public void Load(string fileName, ProgressInterface progress)
  {
    this.m_Big = new BIGFile();
    progress?.Begin(4);
    this.m_Big.Load(fileName, progress);
    if (this.m_Big.ContentType != 510 && this.m_Big.ContentType != (int) \u003CModule\u003E.\u003FA0xe3a94609\u002EFMP_VERSION)
      throw new Exception("Invalid mod package type.");
    if (this.m_Big.ContentType == 510)
    {
      this.m_Big.ContentType = (int) \u003CModule\u003E.\u003FA0xe3a94609\u002EFMP_VERSION;
      this.m_Big.get_Banks("graphicsLinkMetaData").Name = "AssetLinks";
      this.m_Big.get_Banks("GameBINLinkMetaData").Name = "ObjectLinks";
      this.m_Big.get_Banks("maintextures").Name = "MainTextures";
      this.m_Big.get_Banks("graphics").Name = "Graphics";
      this.m_Big.get_Banks("GameBINEntries").Name = "Objects";
      this.m_Big.get_Banks("text").Name = "Text";
      this.m_Big.get_Banks("frontendtextures").Name = "FrontEndTextures";
      this.m_Big.get_Banks("guitextures").Name = "GUITextures";
      this.m_Big.get_Banks("ScriptBINEntries").Name = "Scripts";
    }
    progress?.End();
  }

  public void Merge(ModPackage package)
  {
    if (package.GetSettings("Description") != "")
    {
      string settings = package.GetSettings("Description");
      this.SetSettings("Description", this.GetSettings("Description") + ("\r\n" + settings));
    }
    this.MergeBank(package, ContentType.MainTextures, false);
    this.MergeBank(package, ContentType.GUITextures, false);
    this.MergeBank(package, ContentType.FrontEndTextures, false);
    this.MergeBank(package, ContentType.Graphics, false);
    this.MergeBank(package, ContentType.Text, false);
    this.MergeBank(package, ContentType.Objects, false);
    this.MergeBank(package, ContentType.Scripts, false);
    this.MergeBank(package, ContentType.Graphics, true);
    this.MergeBank(package, ContentType.Objects, true);
  }

  public void Apply()
  {
    ContentManager instance = ContentManager.Instance;
    this.ApplyBank(ContentType.MainTextures);
    this.ApplyBank(ContentType.GUITextures);
    this.ApplyBank(ContentType.FrontEndTextures);
    this.ApplyBank(ContentType.Graphics);
    this.ApplyBank(ContentType.Text);
    Collection<BINEntry> entries1 = new Collection<BINEntry>();
    Collection<BINEntry> entries2 = new Collection<BINEntry>();
    this.ApplyBINBank(ContentType.Objects, entries1);
    this.ApplyBINBank(ContentType.Scripts, entries2);
    this.ApplyAssetLinks(ContentType.Graphics);
    this.ApplyBINLinks(ContentType.Objects, entries1);
    this.ApplyBINLinks(ContentType.Scripts, entries2);
  }

  public void Save(string fileName, ProgressInterface progress)
  {
    this.m_Big.Save(fileName, progress);
  }

  public void New()
  {
    this.m_Big.Destroy();
    this.m_Big.AddBank((AssetBank) new BIGBank("Settings", 0U, 1U, (AssetArchive) this.m_Big));
    this.m_Big.AddBank((AssetBank) new BIGBank("AssetLinks", 1U, 1U, (AssetArchive) this.m_Big));
    this.m_Big.AddBank((AssetBank) new BIGBank("ObjectLinks", 2U, 1U, (AssetArchive) this.m_Big));
    this.m_Big.AddBank((AssetBank) new BIGBank("Text", 3U, 1U, (AssetArchive) this.m_Big));
    this.m_Big.AddBank((AssetBank) new BIGBank("MainTextures", 4U, 1U, (AssetArchive) this.m_Big));
    this.m_Big.AddBank((AssetBank) new BIGBank("Objects", 5U, 1U, (AssetArchive) this.m_Big));
    this.m_Big.AddBank((AssetBank) new BIGBank("Graphics", 6U, 1U, (AssetArchive) this.m_Big));
    this.m_Big.AddBank((AssetBank) new BIGBank("Scripts", 7U, 1U, (AssetArchive) this.m_Big));
    this.m_Big.AddBank((AssetBank) new BIGBank("GUITextures", 8U, 1U, (AssetArchive) this.m_Big));
    this.m_Big.AddBank((AssetBank) new BIGBank("FrontEndTextures", 9U, 1U, (AssetArchive) this.m_Big));
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public unsafe bool Add(ContentType contentType, object @object)
  {
    if ((contentType & ContentType.BIG) != ContentType.Unknown)
    {
      this.AddAssetEntry(contentType, (AssetEntry) @object);
      return true;
    }
    if ((contentType & ContentType.BIN) == ContentType.Unknown)
      return false;
    BINEntry entry1 = (BINEntry) @object;
    if (entry1.Name == "")
      return false;
    BIGBank bank1 = this.m_Big.get_Banks(this.GetBankName(contentType));
    AssetEntry entry2 = new AssetEntry(entry1.Name, (uint) entry1.ID, 0U, (AssetBank) bank1);
    bank1.AddEntry(entry2);
    entry2.Data = entry1.Data;
    IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi(entry1.Definition);
    entry2.SetSubHeader((sbyte*) hglobalAnsi.ToPointer(), (uint) (entry1.Definition.Length + 1));
    Marshal.FreeHGlobal(hglobalAnsi);
    NameValueCollection nameValueCollection = new NameValueCollection();
    DefinitionType definition = ContentManager.Instance.Definitions.GetDefinition(entry1.Definition);
    if (definition != null)
    {
      definition.ReadIn(entry1);
      int index = 0;
      if (0 < definition.Controls.Count)
      {
        do
        {
          // ISSUE: explicit non-virtual call
          uint id = __nonvirtual (definition.Controls[index]).ID;
          // ISSUE: explicit non-virtual call
          this.CreateLinks(nameValueCollection, id.ToString(), (object) __nonvirtual (definition.Controls[index]).Members);
          ++index;
        }
        while (index < definition.Controls.Count);
      }
      if (definition.HasCDefListing)
        this.CreateLinks(nameValueCollection, "", (object) definition.CDefs);
    }
    MemoryStream serializationStream = new MemoryStream();
    new BinaryFormatter().Serialize((Stream) serializationStream, (object) nameValueCollection);
    fixed (byte* data = &serializationStream.GetBuffer()[0])
    {
      // ISSUE: variable of a reference type
      byte* local;
      // ISSUE: fault handler
      try
      {
        BIGBank bank2 = this.m_Big.get_Banks(this.GetLinkBankName(contentType));
        AssetEntry entry3 = new AssetEntry(entry1.Name, bank2.GetNewID(), 0U, (AssetBank) bank2);
        bank2.AddEntry(entry3);
        entry3.SetData((sbyte*) data, (uint) (int) serializationStream.Length);
        serializationStream.Close();
      }
      __fault
      {
        // ISSUE: cast to a reference type
        local = (byte*) 0L;
      }
      // ISSUE: cast to a reference type
      local = (byte*) 0L;
      return true;
    }
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public bool Remove(ContentType entryType, string entryName)
  {
    return this.RemoveFromBank(entryType, entryName);
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public bool IsValid(ContentType contentType, object @object)
  {
    BIGBank bigBank = this.m_Big.get_Banks(this.GetBankName(contentType));
    if ((contentType & ContentType.BIG) != ContentType.Unknown)
    {
      AssetEntry assetEntry = (AssetEntry) @object;
      return bigBank.FindEntryBySymbolName(assetEntry.DevSymbolName) == null;
    }
    if ((contentType & ContentType.BIN) == ContentType.Unknown)
      return false;
    BINEntry binEntry = (BINEntry) @object;
    return !(binEntry.Name == "") && bigBank.FindEntryBySymbolName(binEntry.Name) == null;
  }

  public string Description
  {
    get => this.GetSettings(nameof (Description));
    set => this.SetSettings(nameof (Description), value);
  }

  public Collection<ContentObject> Items
  {
    get
    {
      Collection<ContentObject> c = new Collection<ContentObject>();
      this.ItemsAddBank(c, ContentType.MainTextures);
      this.ItemsAddBank(c, ContentType.GUITextures);
      this.ItemsAddBank(c, ContentType.FrontEndTextures);
      this.ItemsAddBank(c, ContentType.Graphics);
      this.ItemsAddBank(c, ContentType.Objects);
      this.ItemsAddBank(c, ContentType.Scripts);
      this.ItemsAddBank(c, ContentType.Text);
      return c;
    }
  }

  private void ApplyBank(ContentType entryType)
  {
    BIGBank bigBank = this.m_Big.get_Banks(this.GetBankName(entryType));
    if (bigBank == null)
      return;
    int index = 0;
    if (0 >= bigBank.EntryCount)
      return;
    do
    {
      Console.WriteLine("ModPackage::ApplyEntry({0})", (object) bigBank.get_Entries(index).DevSymbolName);
      ContentManager.Instance.UpdateEntry(entryType, bigBank.get_Entries(index));
      ++index;
    }
    while (index < bigBank.EntryCount);
  }

  private unsafe void ApplyBINBank(ContentType entryType, Collection<BINEntry> entries)
  {
    ContentManager instance = ContentManager.Instance;
    BIGBank bigBank = this.m_Big.get_Banks(this.GetBankName(entryType));
    BINFile binFile = (BINFile) null;
    switch (entryType)
    {
      case ContentType.Objects:
        binFile = instance.Objects;
        break;
      case ContentType.Scripts:
        binFile = instance.Scripts;
        break;
    }
    Console.WriteLine("ModPackage::ApplyBINBank({0})...", (object) bigBank.Name);
    int index = 0;
    if (0 >= bigBank.EntryCount)
      return;
    do
    {
      AssetEntry assetEntry = bigBank.get_Entries(index);
      Console.WriteLine(" BIN [{0}] {1}", (object) index, (object) assetEntry.DevSymbolName);
      string Definition = new string(assetEntry.GetSubHeader());
      BINEntry binEntry = !(assetEntry.DevSymbolName != "") ? binFile.get_Entries((int) assetEntry.ID) : binFile.GetEntryByName(assetEntry.DevSymbolName);
      if (binEntry != null)
      {
        binEntry.SetData(assetEntry.GetData(), assetEntry.Length);
      }
      else
      {
        binEntry = binFile.AddEntry(assetEntry.DevSymbolName, Definition, assetEntry.GetData(), assetEntry.Length);
        binEntry.Modified = true;
      }
      entries?.Add(binEntry);
      ++index;
    }
    while (index < bigBank.EntryCount);
  }

  private void AddAssetEntry(ContentType entryType, AssetEntry entry)
  {
    Console.WriteLine("ModPackage::AddAssetEntry({0})", (object) entry.DevSymbolName);
    BIGBank bank = this.m_Big.get_Banks(this.GetBankName(entryType));
    AssetEntry entryBySymbolName1 = bank.FindEntryBySymbolName(entry.DevSymbolName);
    if (entryBySymbolName1 != null)
    {
      entryBySymbolName1.Copy(entry);
      string devSymbolName = entry.DevSymbolName;
      BIGBank bigBank = this.m_Big.get_Banks(this.GetLinkBankName(entryType));
      AssetEntry entryBySymbolName2 = bigBank.FindEntryBySymbolName(devSymbolName);
      if (entryBySymbolName2 != null)
        bigBank.RemoveEntry(entryBySymbolName2);
    }
    else
    {
      AssetEntry entry1 = new AssetEntry(entry, (AssetBank) bank);
      bank.AddEntry(entry1);
    }
    this.AddAssetLinks(entryType, entry);
  }

  private void ItemsAddBank(Collection<ContentObject> c, ContentType contentType)
  {
    BIGBank bigBank = this.m_Big.get_Banks(this.GetBankName(contentType));
    int index = 0;
    if (0 >= bigBank.EntryCount)
      return;
    do
    {
      AssetEntry assetEntry = bigBank.get_Entries(index);
      c.Add(new ContentObject(assetEntry.DevSymbolName, (object) assetEntry, contentType));
      ++index;
    }
    while (index < bigBank.EntryCount);
  }

  private string GetBankName(ContentType contentType)
  {
    if ((int) contentType <= 8194)
    {
      switch (contentType)
      {
        case ContentType.Graphics:
          return "Graphics";
        case ContentType.Text:
          return "Text";
        case ContentType.MainTextures:
          return "MainTextures";
        case ContentType.GUITextures:
          return "GUITextures";
      }
    }
    else
    {
      switch (contentType)
      {
        case ContentType.FrontEndTextures:
          return "FrontEndTextures";
        case ContentType.Objects:
          return "Objects";
        case ContentType.Scripts:
          return "Scripts";
      }
    }
    Debug.Assert(false);
    return (string) null;
  }

  private string GetLinkBankName(ContentType contentType)
  {
    if ((int) contentType <= 4098)
    {
      switch (contentType)
      {
        case ContentType.BIG:
        case ContentType.Graphics:
        case ContentType.Text:
        case ContentType.MainTextures:
          goto label_5;
        case ContentType.BIN:
          goto label_4;
      }
    }
    else
    {
      switch (contentType)
      {
        case ContentType.GUITextures:
        case ContentType.FrontEndTextures:
          goto label_5;
        case ContentType.Objects:
        case ContentType.Scripts:
          goto label_4;
      }
    }
    Debug.Assert(false);
    return (string) null;
label_4:
    return "ObjectLinks";
label_5:
    return "AssetLinks";
  }

  private unsafe string GetSettings(string key)
  {
    BIGBank bigBank = this.m_Big.get_Banks("Settings");
    if (bigBank == null)
      return "";
    AssetEntry entryBySymbolName = bigBank.FindEntryBySymbolName(key);
    return entryBySymbolName == null ? "" : new string((char*) entryBySymbolName.GetData());
  }

  private BIGBank GetBank(ContentType contentType)
  {
    return this.m_Big.get_Banks(this.GetBankName(contentType));
  }

  private BIGBank GetLinkBank(ContentType contentType)
  {
    return this.m_Big.get_Banks(this.GetLinkBankName(contentType));
  }

  private unsafe void SetSettings(string key, string val)
  {
    int length = val.Length;
    IntPtr hglobalUni = Marshal.StringToHGlobalUni(val);
    BIGBank bigBank = this.m_Big.get_Banks("Settings");
    if (bigBank == null)
    {
      BIGFile big = this.m_Big;
      bigBank = new BIGBank("Settings", (uint) big.BankCount, 1U, (AssetArchive) big);
      this.m_Big.AddBank((AssetBank) bigBank);
    }
    AssetEntry entry = bigBank.FindEntryBySymbolName(key);
    if (entry == null)
    {
      entry = new AssetEntry("Description", 0U, 0U, (AssetBank) bigBank);
      bigBank.AddEntry(entry);
    }
    entry.SetData((sbyte*) hglobalUni.ToPointer(), (uint) ((length + 1) * 2));
    Marshal.FreeHGlobal(hglobalUni);
  }

  private unsafe void AddAssetLinks(ContentType entryType, AssetEntry entry)
  {
    AssetLinker assetLinker = ContentManager.Instance.CreateAssetLinker(entryType, entry);
    if (assetLinker != null)
    {
      NameValueCollection links = assetLinker.CreateLinks();
      if (links == null)
        return;
      MemoryStream serializationStream = new MemoryStream();
      new BinaryFormatter().Serialize((Stream) serializationStream, (object) links);
      fixed (byte* data = &serializationStream.GetBuffer()[0])
      {
        // ISSUE: variable of a reference type
        byte* local;
        // ISSUE: fault handler
        try
        {
          BIGBank bank = this.m_Big.get_Banks("AssetLinks");
          AssetEntry entry1 = bank.FindEntryBySymbolName(entry.DevSymbolName);
          if (entry1 == null)
          {
            entry1 = new AssetEntry(entry.DevSymbolName, bank.GetNewID(), 0U, (AssetBank) bank);
            bank.AddEntry(entry1);
          }
          entry1.SetData((sbyte*) data, (uint) (int) serializationStream.Length);
          serializationStream.Close();
        }
        __fault
        {
          // ISSUE: cast to a reference type
          local = (byte*) 0L;
        }
        // ISSUE: cast to a reference type
        local = (byte*) 0L;
      }
    }
    else
      Console.WriteLine("No Asset linker for {0}", (object) entry.DevSymbolName);
  }

  private void RemoveLinks(ContentType entryType, string entryName)
  {
    BIGBank bigBank = this.m_Big.get_Banks(this.GetLinkBankName(entryType));
    AssetEntry entryBySymbolName = bigBank.FindEntryBySymbolName(entryName);
    if (entryBySymbolName == null)
      return;
    bigBank.RemoveEntry(entryBySymbolName);
  }

  private void CreateLinks(NameValueCollection links, string path, object member)
  {
    if (member.GetType() == typeof (Member))
    {
      Member member1 = (Member) member;
      if (member1.Link == null)
        return;
      try
      {
        ContentObject entry = ContentManager.Instance.FindEntry(member1.Link.To, member1.Value);
        if (entry == null)
          return;
        BINEntry binEntry = (BINEntry) entry.Object;
        if (!(binEntry.Name != ""))
          return;
        links.Add(path + "." + member1.Name, binEntry.Name);
      }
      catch (Exception ex)
      {
      }
    }
    else if (member.GetType() == typeof (ArrayMember))
    {
      ArrayMember arrayMember = (ArrayMember) member;
      path += "." + arrayMember.Name;
      int index = 0;
      if (0 >= arrayMember.Elements.Count)
        return;
      do
      {
        int num = index;
        this.CreateLinks(links, path + "." + num.ToString(), (object) arrayMember.Elements[index]);
        ++index;
      }
      while (index < arrayMember.Elements.Count);
    }
    else
    {
      if (!(member.GetType() == typeof (MemberCollection)))
        return;
      MemberCollection memberCollection = (MemberCollection) member;
      int index = 0;
      if (0 >= memberCollection.Count)
        return;
      do
      {
        this.CreateLinks(links, path, (object) memberCollection[index]);
        ++index;
      }
      while (index < memberCollection.Count);
    }
  }

  private void ApplyLinks(NameValueCollection links, string path, object member)
  {
    if (member.GetType() == typeof (Member))
    {
      Member member1 = (Member) member;
      if (member1.Link == null)
        return;
      string link = links[path + "." + member1.Name];
      if (link == (string) null)
      {
        Console.WriteLine("Link {0} is null.", (object) (path + "." + member1.Name));
      }
      else
      {
        Console.WriteLine("  Link {0} to {1}.", (object) (path + "." + member1.Name), (object) link);
        ContentManager instance = ContentManager.Instance;
        try
        {
          object obj = (object) null;
          switch (member1.Link.To)
          {
            case LinkDestination.ModelID:
              AssetEntry entryBySymbolName1 = instance.GraphicsBank.FindEntryBySymbolName(link);
              if (entryBySymbolName1 != null)
              {
                obj = (object) (int) entryBySymbolName1.ID;
                break;
              }
              break;
            case LinkDestination.NamesBINOffset:
              NamesBINEntry entryByName1 = instance.Names.GetEntryByName(link);
              if (entryByName1 != null)
              {
                obj = (object) entryByName1.Offset;
                break;
              }
              break;
            case LinkDestination.NamesBINEnum:
              NamesBINEntry entryByName2 = instance.Names.GetEntryByName(link);
              if (entryByName2 != null)
              {
                obj = (object) entryByName2.Enum;
                break;
              }
              break;
            case LinkDestination.GameBINEntryID:
              BINEntry entryByName3 = instance.Objects.GetEntryByName(link);
              if (entryByName3 != null)
              {
                obj = (object) entryByName3.ID;
                break;
              }
              break;
            case LinkDestination.MainTextureID:
              AssetEntry entryBySymbolName2 = instance.MainTextureBank.FindEntryBySymbolName(link);
              if (entryBySymbolName2 != null)
              {
                obj = (object) (int) entryBySymbolName2.ID;
                break;
              }
              break;
            case LinkDestination.GUITextureID:
              AssetEntry entryBySymbolName3 = instance.GUITextureBank.FindEntryBySymbolName(link);
              if (entryBySymbolName3 != null)
              {
                obj = (object) (int) entryBySymbolName3.ID;
                break;
              }
              break;
            case LinkDestination.FrontEndTextureID:
              AssetEntry entryBySymbolName4 = instance.FrontEndTextureBank.FindEntryBySymbolName(link);
              if (entryBySymbolName4 != null)
              {
                obj = (object) (int) entryBySymbolName4.ID;
                break;
              }
              break;
            case LinkDestination.TextID:
              AssetEntry entryBySymbolName5 = instance.TextBank.FindEntryBySymbolName(link);
              if (entryBySymbolName5 != null)
              {
                obj = (object) (int) entryBySymbolName5.ID;
                break;
              }
              break;
          }
          if (obj == null)
            Console.WriteLine("No value.");
          else
            member1.Value = obj;
        }
        catch (Exception ex)
        {
        }
      }
    }
    else if (member.GetType() == typeof (ArrayMember))
    {
      ArrayMember arrayMember = (ArrayMember) member;
      path += "." + arrayMember.Name;
      int index = 0;
      if (0 >= arrayMember.Elements.Count)
        return;
      do
      {
        int num = index;
        this.ApplyLinks(links, path + "." + num.ToString(), (object) arrayMember.Elements[index]);
        ++index;
      }
      while (index < arrayMember.Elements.Count);
    }
    else
    {
      if (!(member.GetType() == typeof (MemberCollection)))
        return;
      MemberCollection memberCollection = (MemberCollection) member;
      int index = 0;
      if (0 >= memberCollection.Count)
        return;
      do
      {
        this.ApplyLinks(links, path, (object) memberCollection[index]);
        ++index;
      }
      while (index < memberCollection.Count);
    }
  }

  private void ApplyAssetLinks(ContentType contentType)
  {
    ContentManager instance = ContentManager.Instance;
    BIGBank bigBank1 = this.m_Big.get_Banks("Graphics");
    BIGBank bigBank2 = this.m_Big.get_Banks("Graphics");
    Console.WriteLine("ModPackage::ApplyAssetLinks({0},{1})...", (object) bigBank1.Name, (object) bigBank2.Name);
    int index = 0;
    if (0 >= bigBank1.EntryCount)
      return;
    do
    {
      AssetEntry entryBySymbolName = bigBank2.FindEntryBySymbolName(bigBank1.get_Entries(index).DevSymbolName);
      Console.WriteLine(" AssetLink [{0}] {1}", (object) index, (object) bigBank1.get_Entries(index).DevSymbolName);
      if (entryBySymbolName != null)
      {
        AssetLinker assetLinker = instance.CreateAssetLinker(ContentType.Graphics, bigBank1.get_Entries(index));
        if (assetLinker != null)
        {
          NameValueCollection c = (NameValueCollection) new BinaryFormatter().Deserialize((Stream) new MemoryStream(entryBySymbolName.Data));
          assetLinker.ApplyLinks(c);
          Console.WriteLine("  Links applied.");
        }
      }
      ++index;
    }
    while (index < bigBank1.EntryCount);
  }

  private void ApplyBINLinks(ContentType contentType, Collection<BINEntry> entries)
  {
    if (entries.Count == 0)
      return;
    BIGBank bigBank1 = this.m_Big.get_Banks(this.GetBankName(contentType));
    BIGBank bigBank2 = this.m_Big.get_Banks(this.GetLinkBankName(contentType));
    Console.WriteLine("ModPackage::ApplyBINLinks({0},{1})...", (object) bigBank1.Name, (object) bigBank2.Name);
    Debug.Assert(bigBank2.EntryCount == entries.Count);
    int index1 = 0;
    if (0 >= bigBank1.EntryCount)
      return;
    do
    {
      Console.WriteLine(" BINLink [{0}] {1}", (object) index1, (object) bigBank1.get_Entries(index1).DevSymbolName);
      AssetEntry entryBySymbolName = bigBank2.FindEntryBySymbolName(bigBank1.get_Entries(index1).DevSymbolName);
      if (entryBySymbolName != null)
      {
        MemoryStream serializationStream = new MemoryStream(entryBySymbolName.Data);
        NameValueCollection links = (NameValueCollection) new BinaryFormatter().Deserialize((Stream) serializationStream);
        DefinitionType definition = ContentManager.Instance.Definitions.GetDefinition(entries[index1].Definition);
        if (definition != null && links != null)
        {
          definition.ReadIn(entries[index1]);
          int index2 = 0;
          if (0 < definition.Controls.Count)
          {
            do
            {
              // ISSUE: explicit non-virtual call
              uint id = __nonvirtual (definition.Controls[index2]).ID;
              // ISSUE: explicit non-virtual call
              this.ApplyLinks(links, id.ToString(), (object) __nonvirtual (definition.Controls[index2]).Members);
              ++index2;
            }
            while (index2 < definition.Controls.Count);
          }
          if (definition.HasCDefListing)
            this.ApplyLinks(links, "", (object) definition.CDefs);
          definition.Write(entries[index1]);
        }
        serializationStream.Close();
      }
      ++index1;
    }
    while (index1 < bigBank1.EntryCount);
  }

  private void MergeBank(ModPackage package, ContentType entryType, [MarshalAs(UnmanagedType.U1)] bool bLink)
  {
    BIGBank bigBank;
    BIGBank bank;
    if (bLink)
    {
      bigBank = package.m_Big.get_Banks(package.GetLinkBankName(entryType));
      bank = this.m_Big.get_Banks(this.GetLinkBankName(entryType));
    }
    else
    {
      bigBank = package.m_Big.get_Banks(package.GetBankName(entryType));
      bank = this.m_Big.get_Banks(this.GetBankName(entryType));
    }
    Console.WriteLine("ModPackage::MergeBank({0},{1})", (object) entryType.ToString(), (object) bLink);
    int index = 0;
    if (0 >= bigBank.EntryCount)
      return;
    do
    {
      AssetEntry assetEntry = bigBank.get_Entries(index);
      Console.WriteLine(" [{0}] {1}", (object) index, (object) assetEntry.DevSymbolName);
      AssetEntry entryBySymbolName = bank.FindEntryBySymbolName(assetEntry.DevSymbolName);
      if (entryBySymbolName != null)
      {
        entryBySymbolName.Copy(assetEntry);
      }
      else
      {
        AssetEntry entry = new AssetEntry(assetEntry, (AssetBank) bank);
        bank.AddEntry(entry);
      }
      ++index;
    }
    while (index < bigBank.EntryCount);
  }

  [return: MarshalAs(UnmanagedType.U1)]
  private bool RemoveFromBank(ContentType entryType, string entryName)
  {
    BIGBank bigBank1 = this.m_Big.get_Banks(this.GetBankName(entryType));
    AssetEntry entryBySymbolName1 = bigBank1.FindEntryBySymbolName(entryName);
    if (entryBySymbolName1 == null)
      return false;
    BIGBank bigBank2 = this.m_Big.get_Banks(this.GetLinkBankName(entryType));
    AssetEntry entryBySymbolName2 = bigBank2.FindEntryBySymbolName(entryName);
    if (entryBySymbolName2 != null)
      bigBank2.RemoveEntry(entryBySymbolName2);
    return bigBank1.RemoveEntry(entryBySymbolName1);
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.m_Big.Destroy();
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
