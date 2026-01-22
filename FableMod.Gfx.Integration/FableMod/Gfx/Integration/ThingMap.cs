// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.ThingMap
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using FableMod.LEV;
using FableMod.STB;
using FableMod.TNG;
using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.Gfx.Integration;

public class ThingMap : Map
{
  protected TNGFile m_Tng;
  protected Level m_STBLevel;

  private void \u007EThingMap() => this.Destroy();

  public override void Destroy()
  {
    base.Destroy();
    TNGFile tng = this.m_Tng;
    if (tng == null)
      return;
    tng.Destroy();
    this.m_Tng = (TNGFile) null;
  }

  public void Create(
    GfxThingController controller,
    string name,
    int mapX,
    int mapY,
    Level stbLevel,
    TNGFile tng,
    LEVFile lev)
  {
    this.Create((GfxController) controller, name, mapX, mapY, lev);
    this.m_Tng = tng;
    int index1 = 0;
    if (0 < tng.SectionCount)
    {
      do
      {
        Section section = this.m_Tng.get_Sections(index1);
        int index2 = 0;
        if (0 < section.Things.Count)
        {
          do
          {
            Thing thing = section.Things[index2];
            if (this.ValidObject(thing))
            {
              GfxThingInterface gfxThingInterface = new GfxThingInterface(this, thing);
              if (!gfxThingInterface.Create(controller))
                gfxThingInterface?.Dispose();
              else
                gfxThingInterface.UpdateInterface();
            }
            ++index2;
          }
          while (index2 < section.Things.Count);
        }
        ++index1;
      }
      while (index1 < this.m_Tng.SectionCount);
    }
    int index3 = 0;
    if (0 >= this.m_Tng.SectionCount)
      return;
    do
    {
      Section section = this.m_Tng.get_Sections(index3);
      int index4 = 0;
      if (0 < section.Things.Count)
      {
        do
        {
          ((GfxThingInterface) section.Things[index4].Interface)?.Update();
          ++index4;
        }
        while (index4 < section.Things.Count);
      }
      ++index3;
    }
    while (index3 < this.m_Tng.SectionCount);
  }

  public void AddThing(GfxThingController controller, Thing thing, string section)
  {
    if (this.m_Tng.get_Sections(section) == null)
      this.m_Tng.AddSection(new Section(section));
    this.m_Tng.get_Sections(section).AddThing(thing);
    if (!this.ValidObject(thing))
      return;
    GfxThingInterface gfxThingInterface = new GfxThingInterface(this, thing);
    if (!gfxThingInterface.Create(controller))
    {
      gfxThingInterface?.Dispose();
      throw new System.Exception($"Failed to create interface for {thing.DefinitionType}");
    }
    gfxThingInterface.Update();
  }

  public unsafe void AddThing(
    GfxThingController controller,
    Thing thing,
    string section,
    float x,
    float y,
    float z,
    float nx,
    float ny,
    float nz)
  {
    // ISSUE: untyped stack allocation
    long num1 = (long) __untypedstackalloc(\u003CModule\u003E.__CxxQueryExceptionSize());
    if (this.m_Tng.get_Sections(section) == null)
      this.m_Tng.AddSection(new Section(section));
    this.m_Tng.get_Sections(section).AddThing(thing);
    if (!this.ValidObject(thing))
      return;
    CTCBlock ctcBlock = !(thing.Name == "AICreature") ? thing.get_CTCs("CTCPhysicsStandard") : thing.get_CTCs("CTCPhysicsNavigator");
    if (ctcBlock != null)
    {
      try
      {
        ctcBlock.get_Variables("PositionX").Value = (object) (x - this.X);
        ctcBlock.get_Variables("PositionY").Value = (object) (y - this.Y);
        ctcBlock.get_Variables("PositionZ").Value = (object) z;
        float num2 = (float) Math.Abs((double) ny);
        if ((double) num2 > Math.Abs((double) nx))
        {
          if ((double) num2 > Math.Abs((double) nz))
          {
            ctcBlock.get_Variables("RHSetForwardX").Value = (object) 1f;
            ctcBlock.get_Variables("RHSetForwardY").Value = (object) 0.0f;
            ctcBlock.get_Variables("RHSetForwardZ").Value = (object) 0.0f;
          }
        }
        ctcBlock.get_Variables("RHSetUpX").Value = (object) nx;
        ctcBlock.get_Variables("RHSetUpY").Value = (object) ny;
        ctcBlock.get_Variables("RHSetUpZ").Value = (object) nz;
      }
      catch (System.Exception ex1) when (
      {
        // ISSUE: unable to correctly present filter
        uint exceptionCode = (uint) Marshal.GetExceptionCode();
        if (\u003CModule\u003E.__CxxExceptionFilter((void*) Marshal.GetExceptionPointers(), (void*) 0L, 0, (void*) 0L) != 0)
        {
          SuccessfulFiltering;
        }
        else
          throw;
      }
      )
      {
        uint num3 = 0;
        \u003CModule\u003E.__CxxRegisterExceptionObject((void*) Marshal.GetExceptionPointers(), (void*) num1);
        try
        {
          try
          {
          }
          catch (System.Exception ex2) when (
          {
            // ISSUE: unable to correctly present filter
            num3 = (uint) \u003CModule\u003E.__CxxDetectRethrow((void*) Marshal.GetExceptionPointers());
            if (num3 != 0U)
            {
              SuccessfulFiltering;
            }
            else
              throw;
          }
          )
          {
          }
          goto label_17;
          if (num3 != 0U)
            throw;
        }
        finally
        {
          \u003CModule\u003E.__CxxUnregisterExceptionObject((void*) num1, (int) num3);
        }
      }
    }
label_17:
    if (thing.Name == "AICreature")
    {
      thing.get_Variables("InitialPosX").Value = (object) x;
      thing.get_Variables("InitialPosY").Value = (object) y;
      thing.get_Variables("InitialPosZ").Value = (object) z;
    }
    GfxThingInterface gfxThingInterface = new GfxThingInterface(this, thing);
    if (!gfxThingInterface.Create(controller))
    {
      gfxThingInterface?.Dispose();
      throw new System.Exception($"Failed to create interface for {thing.DefinitionType}");
    }
    gfxThingInterface.Update();
  }

  public TNGFile TNG => this.m_Tng;

  public override bool Modified
  {
    [return: MarshalAs(UnmanagedType.U1)] get => base.Modified || this.m_Tng.Modified;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  protected bool ValidObject(Thing thing)
  {
    return thing.Name == "Object" || thing.Name == "Building" || thing.Name == "Holy Site" || thing.Name == "Thing" || thing.Name == "Marker" || thing.Name == "AICreature";
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EThingMap();
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
