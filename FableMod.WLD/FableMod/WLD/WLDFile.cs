// Decompiled with JetBrains decompiler
// Type: FableMod.WLD.WLDFile
// Assembly: FableMod.WLD, Version=1.0.4918.436, Culture=neutral, PublicKeyToken=null
// MVID: C116F1D2-4A42-43FB-9046-16C428742204
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.WLD.dll

using FableMod.Script;
using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.WLD;

public class WLDFile : Parser
{
  protected Collection<Map> m_Maps = new Collection<Map>();
  protected Collection<Region> m_Regions = new Collection<Region>();
  protected Collection<string> m_Quests = new Collection<string>();
  protected int m_MapUIDCount;
  protected int m_ThingManagerUIDCount;
  protected WLDFile.ParserState m_ParserState = WLDFile.ParserState.Basic;
  protected CultureInfo m_Culture = new CultureInfo("en-US");

  private void \u007EWLDFile()
  {
  }

  public int RegionCount => this.m_Regions.Count;

  public Region get_Regions(string name)
  {
    int index = 0;
    if (0 < this.m_Regions.Count)
    {
      while (!(this.m_Regions[index].RegionName == name))
      {
        ++index;
        if (index >= this.m_Regions.Count)
          goto label_4;
      }
      return this.m_Regions[index];
    }
label_4:
    return (Region) null;
  }

  public Region get_Regions(int index) => this.m_Regions[index];

  protected Map FindMap(string levelName)
  {
    int index = 0;
    if (0 < this.m_Maps.Count)
    {
      while (!(this.m_Maps[index].LevelName == levelName))
      {
        ++index;
        if (index >= this.m_Maps.Count)
          goto label_4;
      }
      return this.m_Maps[index];
    }
label_4:
    return (Map) null;
  }

  protected override void ParseArgument(string argument, string value)
  {
    if (value.StartsWith("\"") && value.EndsWith("\""))
      value = value.Substring(1, value.Length - 2);
    switch (this.m_ParserState)
    {
      case WLDFile.ParserState.Basic:
        switch (argument)
        {
          case "MapUIDCount":
            this.m_MapUIDCount = int.Parse(value);
            return;
          case "ThingManagerUIDCount":
            this.m_ThingManagerUIDCount = int.Parse(value);
            return;
          case "NewMap":
            this.m_Maps.Add(new Map()
            {
              ID = int.Parse(value)
            });
            this.m_ParserState = WLDFile.ParserState.Map;
            return;
          case "NewRegion":
            this.m_Regions.Add(new Region()
            {
              ID = int.Parse(value)
            });
            this.m_ParserState = WLDFile.ParserState.Region;
            return;
          default:
            return;
        }
      case WLDFile.ParserState.Map:
        this.ParseMapArgument(argument, value);
        break;
      case WLDFile.ParserState.Region:
        this.ParseRegionArgument(argument, value);
        break;
    }
  }

  protected override void ParseCommand(string command)
  {
    switch (this.m_ParserState)
    {
      case WLDFile.ParserState.Basic:
        if (!(command == "START_INITIAL_QUESTS"))
          break;
        this.m_ParserState = WLDFile.ParserState.Quests;
        break;
      case WLDFile.ParserState.Quests:
        if (command == "END_INITIAL_QUESTS")
        {
          this.m_ParserState = WLDFile.ParserState.Basic;
          break;
        }
        this.m_Quests.Add(command);
        break;
      case WLDFile.ParserState.Map:
        if (!(command == "EndMap"))
          break;
        this.m_ParserState = WLDFile.ParserState.Basic;
        break;
      case WLDFile.ParserState.Region:
        if (!(command == "EndRegion"))
          break;
        this.m_ParserState = WLDFile.ParserState.Basic;
        break;
    }
  }

  protected void ParseMapArgument(string argument, string value)
  {
    Map map = this.m_Maps[this.m_Maps.Count - 1];
    switch (argument)
    {
      case "MapX":
        map.X = int.Parse(value);
        break;
      case "MapY":
        map.Y = int.Parse(value);
        break;
      case "LevelName":
        map.LevelName = value;
        break;
      case "LevelScriptName":
        map.LevelScriptName = value;
        break;
      case "MapUID":
        map.UID = uint.Parse(value);
        break;
      case "IsSea":
        map.IsSea = bool.Parse(value);
        break;
      case "LoadedOnPlayerProximity":
        map.LoadedOnProximity = bool.Parse(value);
        break;
    }
  }

  protected void ParseRegionArgument(string argument, string value)
  {
    Region region = this.m_Regions[this.m_Regions.Count - 1];
    switch (argument)
    {
      case "RegionName":
        region.RegionName = value;
        break;
      case "NewDisplayName":
        region.NewDisplayName = value;
        break;
      case "RegionDef":
        region.RegionDef = value;
        break;
      case "MiniMapGraphic":
        region.MiniMap = value;
        break;
      case "MiniMapScale":
        region.MiniMapScale = float.Parse(value, (IFormatProvider) this.m_Culture);
        break;
      case "AppearOnWorldMap":
        region.AppearOnWorldMap = bool.Parse(value);
        break;
      case "MiniMapOffsetX":
        region.MiniMapOffsetX = float.Parse(value, (IFormatProvider) this.m_Culture);
        break;
      case "MiniMapOffsetY":
        region.MiniMapOffsetY = float.Parse(value, (IFormatProvider) this.m_Culture);
        break;
      case "WorldMapOffsetX":
        region.WorldMapOffsetX = float.Parse(value, (IFormatProvider) this.m_Culture);
        break;
      case "WorldMapOffsetY":
        region.WorldMapOffsetY = float.Parse(value, (IFormatProvider) this.m_Culture);
        break;
      case "NameGraphicOffsetX":
        region.NameGraphicOffsetX = float.Parse(value, (IFormatProvider) this.m_Culture);
        break;
      case "NameGraphicOffsetY":
        region.NameGraphicOffsetY = float.Parse(value, (IFormatProvider) this.m_Culture);
        break;
      case "ContainsMap":
        region.ContainsMaps.Add(this.FindMap(value));
        break;
      case "SeesMap":
        region.SeesMaps.Add(this.FindMap(value));
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
    Quests,
    Map,
    Region,
  }
}
