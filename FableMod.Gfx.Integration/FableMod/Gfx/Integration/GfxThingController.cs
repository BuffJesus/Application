// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.GfxThingController
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using FableMod.TNG;
using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.Gfx.Integration;

public class GfxThingController : GfxController
{
  private Dictionary<uint, Thing> m_Things = new Dictionary<uint, Thing>();

  private void \u007EGfxThingController() => this.Destroy();

  public override void Destroy()
  {
    base.Destroy();
    this.ResetThings();
  }

  public override void ResetObjects()
  {
    this.ResetThings();
    base.ResetObjects();
  }

  public void ResetThings()
  {
    Dictionary<uint, Thing>.Enumerator enumerator = this.m_Things.GetEnumerator();
    if (enumerator.MoveNext())
    {
      do
      {
        enumerator.Current.Value.Dispose();
      }
      while (enumerator.MoveNext());
    }
    this.m_Things.Clear();
  }

  public unsafe void AddThing(uint id, Thing thing)
  {
    if (!this.m_Things.ContainsKey(id))
      this.m_Things[id] = thing;
    FableMod.Gfx.Node* root = this.GetRoot();
    // ISSUE: cast to a function pointer type
    // ISSUE: function pointer call
    __calli((__FnPtr<void (IntPtr, Spatial*)>) *(long*) (*(long*) root + 120L))((IntPtr) root, (Spatial*) ((GfxThingInterface) thing.Interface).GetNode());
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public bool RemoveThing(Thing thing)
  {
    Dictionary<uint, Thing>.Enumerator enumerator = this.m_Things.GetEnumerator();
    if (enumerator.MoveNext())
    {
      KeyValuePair<uint, Thing> current;
      do
      {
        current = enumerator.Current;
        if (current.Value == thing)
          goto label_3;
      }
      while (enumerator.MoveNext());
      goto label_4;
label_3:
      this.ThingRemove(current.Key, thing);
      return true;
    }
label_4:
    return false;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public bool RemoveThing(uint id)
  {
    Thing thing = (Thing) null;
    if (!this.m_Things.TryGetValue(id, out thing))
      return false;
    this.ThingRemove(id, thing);
    return true;
  }

  public Thing FindThing(uint id)
  {
    Thing thing = (Thing) null;
    return this.m_Things.TryGetValue(id, out thing) ? thing : (Thing) null;
  }

  public Thing FindThingUID(string uid)
  {
    Dictionary<uint, Thing>.Enumerator enumerator = this.m_Things.GetEnumerator();
    if (enumerator.MoveNext())
    {
      KeyValuePair<uint, Thing> current;
      do
      {
        current = enumerator.Current;
        if (current.Value.UID == uid)
          goto label_3;
      }
      while (enumerator.MoveNext());
      goto label_4;
label_3:
      return current.Value;
    }
label_4:
    return (Thing) null;
  }

  public void ToGroundEditMode([MarshalAs(UnmanagedType.U1)] bool disable)
  {
    Dictionary<uint, Thing>.Enumerator enumerator = this.m_Things.GetEnumerator();
    if (!enumerator.MoveNext())
      return;
    do
    {
      Thing thing = enumerator.Current.Value;
      if (!(thing.Name == "Marker") && !(thing.Name == "AICreature") && !(thing.Name == "Holy Site") && !(thing.Name == "Thing"))
      {
        ((GfxThingInterface) thing.Interface).GroundMode(disable);
      }
      else
      {
        byte num = (byte) !disable;
        ((GfxThingInterface) thing.Interface).Show((bool) num);
      }
    }
    while (enumerator.MoveNext());
  }

  public void FreezeAll([MarshalAs(UnmanagedType.U1)] bool freeze)
  {
    Dictionary<uint, Thing>.Enumerator enumerator = this.m_Things.GetEnumerator();
    if (!enumerator.MoveNext())
      return;
    do
    {
      ((GfxThingInterface) enumerator.Current.Value.Interface).Freezed = freeze;
    }
    while (enumerator.MoveNext());
  }

  public void ShowOfType(string objectType, [MarshalAs(UnmanagedType.U1)] bool show)
  {
    Dictionary<uint, Thing>.Enumerator enumerator = this.m_Things.GetEnumerator();
    if (!enumerator.MoveNext())
      return;
    do
    {
      Thing thing = enumerator.Current.Value;
      if (thing.Name == objectType)
        ((GfxThingInterface) thing.Interface).Show(show);
    }
    while (enumerator.MoveNext());
  }

  private void OwnerRemove(Thing ownerThing)
  {
    string uid = ownerThing.UID;
    Dictionary<uint, Thing>.Enumerator enumerator = this.m_Things.GetEnumerator();
    if (!enumerator.MoveNext())
      return;
    do
    {
      CTCBlock ctcBlock = enumerator.Current.Value.get_CTCs("CTCOwnedEntity");
      if (ctcBlock != null && ctcBlock.get_Variables("OwnerUID").StringValue == uid)
        ctcBlock.get_Variables("OwnerUID").Value = (object) "0";
    }
    while (enumerator.MoveNext());
  }

  private void ThingRemove(uint id, Thing thing)
  {
    ((GfxThingInterface) thing.Interface).Highlight = false;
    this.OwnerRemove(thing);
    this.m_Things.Remove(id);
    thing.Detach();
    thing.Destroy();
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EGfxThingController();
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
