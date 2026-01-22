// Decompiled with JetBrains decompiler
// Type: ChocolateBox.RegionLoader
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.CLRCore;
using FableMod.Gfx.Integration;
using FableMod.LEV;
using FableMod.STB;
using FableMod.TNG;
using FableMod.WLD;
using System.Collections.ObjectModel;

#nullable disable
namespace ChocolateBox;

internal class RegionLoader : Processor
{
  private string myBaseDirectory;
  private GfxThingController myController;
  private Region myRegion;
  private STBFile mySTB;
  private Collection<ThingMap> myMaps;
  private bool myAddSees;

  public RegionLoader(
    string baseDirectory,
    GfxThingController controller,
    Region region,
    STBFile stb,
    bool addSees)
  {
    this.myBaseDirectory = baseDirectory;
    this.myRegion = region;
    this.myController = controller;
    this.myMaps = new Collection<ThingMap>();
    this.mySTB = stb;
    this.myAddSees = addSees;
  }

  public override void Run(Progress progress)
  {
    FableMod.WLD.Map[] mapArray = new FableMod.WLD.Map[this.myRegion.ContainsMaps.Count + this.myRegion.SeesMaps.Count];
    int num = 0;
    for (int index = 0; index < this.myRegion.ContainsMaps.Count; ++index)
      mapArray[num++] = this.myRegion.ContainsMaps[index];
    if (this.myAddSees)
    {
      for (int index = 0; index < this.myRegion.SeesMaps.Count; ++index)
        mapArray[num++] = this.myRegion.SeesMaps[index];
    }
    progress.Begin(num * 4);
    for (int index = 0; index < num; ++index)
    {
      FableMod.WLD.Map map = mapArray[index];
      string filename = this.myBaseDirectory + map.LevelName;
      string fileName = filename.Substring(0, filename.Length - 4) + ".tng";
      TNGFile tng = new TNGFile(FileDatabase.Instance.TNGDefinitions);
      LEVFile lev = new LEVFile();
      progress.Info = $"Loading {map.LevelScriptName} objects...";
      progress.Begin(1);
      tng.Load(fileName);
      progress.End();
      progress.Info = $"Loading {map.LevelScriptName} level...";
      progress.Begin(1);
      lev.Open(filename, (ProgressInterface) null);
      progress.End();
      string str = filename;
      if (str.StartsWith(Settings.FableDirectory))
        str.Substring(Settings.FableDirectory.Length);
      Level stbLevel = (Level) null;
      ThingMap thingMap = new ThingMap();
      progress.Begin(1);
      thingMap.Create(this.myController, map.LevelScriptName, map.X, map.Y, stbLevel, tng, lev);
      progress.End();
      this.myMaps.Add(thingMap);
    }
    progress.End();
    base.Run(progress);
  }

  public Collection<ThingMap> Maps => this.myMaps;
}
