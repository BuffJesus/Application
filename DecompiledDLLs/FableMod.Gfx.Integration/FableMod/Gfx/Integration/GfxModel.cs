// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.GfxModel
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using FableMod.BIG;
using FableMod.ContentManagement;
using System;
using System.Collections.Specialized;
using System.Diagnostics;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.Gfx.Integration;

public class GfxModel : GfxBaseModel
{
  private GfxModelLOD[] m_LODs;
  private unsafe ModelSubHeader* m_SubHeader;

  public unsafe GfxModel(AssetEntry entry)
    : base(entry)
  {
    // ISSUE: fault handler
    try
    {
      ModelSubHeader* modelSubHeaderPtr1 = (ModelSubHeader*) \u003CModule\u003E.@new(72UL);
      ModelSubHeader* modelSubHeaderPtr2;
      // ISSUE: fault handler
      try
      {
        modelSubHeaderPtr2 = (IntPtr) modelSubHeaderPtr1 == IntPtr.Zero ? (ModelSubHeader*) 0L : \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EModelSubHeader\u002E\u007Bctor\u007D(modelSubHeaderPtr1, entry.GetSubHeader());
      }
      __fault
      {
        \u003CModule\u003E.delete((void*) modelSubHeaderPtr1);
      }
      this.m_SubHeader = modelSubHeaderPtr2;
      GfxModel gfxModel = this;
      gfxModel.m_LODs = new GfxModelLOD[(int) gfxModel.LODCount];
      int num1 = 0;
      int index = 0;
      if (0U < this.LODCount - 1U)
      {
        long num2 = *(long*) ((IntPtr) modelSubHeaderPtr2 + 48L /*0x30*/);
        long num3 = 0;
        do
        {
          this.m_LODs[index] = new GfxModelLOD(entry.GetData() + (long) num1, (uint) *(int*) (num3 + num2));
          num2 = *(long*) ((IntPtr) this.m_SubHeader + 48L /*0x30*/);
          num1 += *(int*) (num3 + num2);
          ++index;
          num3 += 4L;
        }
        while ((uint) index < this.LODCount - 1U);
      }
      this.m_LODs[(int) this.LODCount - 1] = new GfxModelLOD(entry.GetData() + (long) num1, entry.Length - (uint) num1);
      if (entry.Modified)
        return;
      entry.Purge();
    }
    __fault
    {
      base.Dispose(true);
    }
  }

  private void \u007EGfxModel()
  {
    GfxModelLOD[] loDs = this.m_LODs;
    if (loDs != null)
    {
      int index = 0;
      if (0 < loDs.Length)
      {
        do
        {
          this.m_LODs[index].Dispose();
          ++index;
        }
        while (index < this.m_LODs.Length);
      }
      this.m_LODs = (GfxModelLOD[]) null;
    }
    this.\u0021GfxModel();
  }

  private unsafe void \u0021GfxModel()
  {
    ModelSubHeader* subHeader = this.m_SubHeader;
    if ((IntPtr) subHeader == IntPtr.Zero)
      return;
    ModelSubHeader* modelSubHeaderPtr = subHeader;
    \u003CModule\u003E.delete\u005B\u005D((void*) *(long*) ((IntPtr) modelSubHeaderPtr + 48L /*0x30*/));
    \u003CModule\u003E.delete\u005B\u005D((void*) *(long*) ((IntPtr) modelSubHeaderPtr + 64L /*0x40*/));
    \u003CModule\u003E.delete((void*) modelSubHeaderPtr);
    this.m_SubHeader = (ModelSubHeader*) 0L;
  }

  public override unsafe void CompileToEntry(AssetEntry entry)
  {
    sbyte* data = (sbyte*) \u003CModule\u003E.new\u005B\u005D(1048576UL /*0x100000*/);
    int len1 = 0;
    ModelSubHeader* subHeader = this.m_SubHeader;
    if ((IntPtr) subHeader != IntPtr.Zero)
    {
      int index = 0;
      if (0U < this.LODCount - 1U)
      {
        long num1 = *(long*) ((IntPtr) subHeader + 48L /*0x30*/);
        long num2 = 0;
        do
        {
          this.m_LODs[index].Save((sbyte*) ((long) len1 + (IntPtr) data), (uint*) ((long) index * 4L + num1));
          num1 = *(long*) ((IntPtr) this.m_SubHeader + 48L /*0x30*/);
          len1 += *(int*) (num2 + num1);
          ++index;
          num2 += 4L;
        }
        while ((uint) index < this.LODCount - 1U);
      }
      uint num;
      this.m_LODs[(int) this.LODCount - 1].Save((sbyte*) ((long) len1 + (IntPtr) data), &num);
      len1 += (int) num;
    }
    entry.SetData(data, (uint) len1);
    uint len2 = 1048576 /*0x100000*/;
    if ((IntPtr) this.m_SubHeader == IntPtr.Zero)
      return;
    uint* numPtr = (uint*) \u003CModule\u003E.new\u005B\u005D(1024UL /*0x0400*/);
    int num3 = 0;
    int index1 = 0;
    long num4 = 0;
    if (0U < this.LODCount)
    {
      do
      {
        CompiledModel* compiledModel = this.get_LODs(index1).m_CompiledModel;
        int num5 = 0;
        if (0U < (uint) *(int*) ((IntPtr) compiledModel + 95L))
        {
          long num6 = 0;
          do
          {
            bool flag1 = false;
            bool flag2 = false;
            bool flag3 = false;
            long num7 = num6 + *(long*) ((IntPtr) compiledModel + 204L);
            uint num8 = (uint) *(int*) (num7 + 16L /*0x10*/);
            if (num8 != 0U)
            {
              long num9 = 0;
              if (0L < num4)
              {
                do
                {
                  flag1 = *(int*) (num9 * 4L + (IntPtr) numPtr) == (int) num8 || flag1;
                  ++num9;
                }
                while (num9 < num4);
              }
            }
            else
              flag1 = true;
            uint num10 = (uint) *(int*) (num7 + 20L);
            if (num10 != 0U)
            {
              long num11 = 0;
              if (0L < num4)
              {
                do
                {
                  flag3 = *(int*) (num11 * 4L + (IntPtr) numPtr) == (int) num10 || flag3;
                  ++num11;
                }
                while (num11 < num4);
              }
            }
            else
              flag3 = true;
            uint num12 = (uint) *(int*) (num7 + 24L);
            if (num12 != 0U)
            {
              long num13 = 0;
              if (0L < num4)
              {
                do
                {
                  flag2 = *(int*) (num13 * 4L + (IntPtr) numPtr) == (int) num12 || flag2;
                  ++num13;
                }
                while (num13 < num4);
              }
            }
            else
              flag2 = true;
            if (!flag1)
            {
              *(int*) (num4 * 4L + (IntPtr) numPtr) = (int) num8;
              ++num3;
              ++num4;
            }
            if (!flag3)
            {
              *(int*) (num4 * 4L + (IntPtr) numPtr) = *(int*) (*(long*) ((IntPtr) compiledModel + 204L) + num6 + 20L);
              ++num3;
              ++num4;
            }
            if (!flag2)
            {
              *(int*) (num4 * 4L + (IntPtr) numPtr) = *(int*) (*(long*) ((IntPtr) compiledModel + 204L) + num6 + 24L);
              ++num3;
              ++num4;
            }
            ++num5;
            num6 += 45L;
          }
          while ((uint) num5 < (uint) *(int*) ((IntPtr) compiledModel + 95L));
        }
        ++index1;
      }
      while ((uint) index1 < this.LODCount);
    }
    \u003CModule\u003E.delete\u005B\u005D((void*) *(long*) ((IntPtr) this.m_SubHeader + 64L /*0x40*/));
    *(long*) ((IntPtr) this.m_SubHeader + 64L /*0x40*/) = (long) numPtr;
    *(int*) ((IntPtr) this.m_SubHeader + 60L) = num3;
    \u003CModule\u003E.FableMod\u002EGfx\u002EIntegration\u002EModelSubHeader\u002EWriteToBuffer(this.m_SubHeader, data, &len2);
    entry.SetSubHeader(data, len2);
  }

  public override NameValueCollection CreateLinks()
  {
    NameValueCollection links = new NameValueCollection();
    ContentObject entry1 = ContentManager.Instance.FindEntry(LinkDestination.ModelID, (object) this.Physics);
    if (entry1 != null)
      links["PhysicsModel"] = ((AssetEntry) entry1.Object).DevSymbolName;
    int index1 = 0;
    if (0U < this.LODCount)
    {
      do
      {
        int index2 = 0;
        if (0 < this.get_LODs(index1).MaterialCount)
        {
          do
          {
            Material material = this.get_LODs(index1).get_Materials((uint) index2);
            int num = index2;
            string str = "LOD" + index1.ToString() + ".MAT" + num.ToString();
            if (material.BaseTextureID != 0U)
            {
              ContentObject entry2 = ContentManager.Instance.FindEntry(LinkDestination.MainTextureID, (object) material.BaseTextureID);
              AssetEntry assetEntry;
              if (entry2 != null)
              {
                assetEntry = (AssetEntry) entry2.Object;
              }
              else
              {
                Debug.Assert(false);
                assetEntry = (AssetEntry) null;
              }
              links.Add(str + ".BaseTexture", assetEntry.DevSymbolName);
            }
            if (material.BumpMapTextureID != 0U)
            {
              ContentObject entry3 = ContentManager.Instance.FindEntry(LinkDestination.MainTextureID, (object) material.BumpMapTextureID);
              AssetEntry assetEntry;
              if (entry3 != null)
              {
                assetEntry = (AssetEntry) entry3.Object;
              }
              else
              {
                Debug.Assert(false);
                assetEntry = (AssetEntry) null;
              }
              links.Add(str + ".BumpMapTexture", assetEntry.DevSymbolName);
            }
            if (material.ReflectionTextureID != 0U)
            {
              ContentObject entry4 = ContentManager.Instance.FindEntry(LinkDestination.MainTextureID, (object) material.ReflectionTextureID);
              AssetEntry assetEntry;
              if (entry4 != null)
              {
                assetEntry = (AssetEntry) entry4.Object;
              }
              else
              {
                Debug.Assert(false);
                assetEntry = (AssetEntry) null;
              }
              links.Add(str + ".SpecularMapTexture", assetEntry.DevSymbolName);
            }
            ++index2;
          }
          while (index2 < this.get_LODs(index1).MaterialCount);
        }
        ++index1;
      }
      while ((uint) index1 < this.LODCount);
    }
    return links;
  }

  public override void ApplyLinks(NameValueCollection c)
  {
    string str1 = c["PhysicsModel"];
    if (str1 != null)
    {
      ContentObject entry = ContentManager.Instance.FindEntry(LinkDestination.ModelName, (object) str1);
      if (entry != null)
        this.Physics = ((AssetEntry) entry.Object).ID;
    }
    int index1 = 0;
    if (0U >= this.LODCount)
      return;
    do
    {
      int index2 = 0;
      if (0 < this.get_LODs(index1).MaterialCount)
      {
        do
        {
          Material material = this.get_LODs(index1).get_Materials((uint) index2);
          int num = index2;
          string str2 = "LOD" + index1.ToString() + ".MAT" + num.ToString();
          string str3 = c[str2 + ".BaseTexture"];
          if (str3 != null)
          {
            ContentObject entry = ContentManager.Instance.FindEntry(LinkDestination.MainTextureName, (object) str3);
            AssetEntry assetEntry;
            if (entry != null)
            {
              assetEntry = (AssetEntry) entry.Object;
            }
            else
            {
              Debug.Assert(false);
              assetEntry = (AssetEntry) null;
            }
            if (assetEntry != null)
              material.BaseTextureID = assetEntry.ID;
          }
          string str4 = c[str2 + ".BumpMapTexture"];
          if (str4 != null)
          {
            ContentObject entry = ContentManager.Instance.FindEntry(LinkDestination.MainTextureName, (object) str4);
            AssetEntry assetEntry;
            if (entry != null)
            {
              assetEntry = (AssetEntry) entry.Object;
            }
            else
            {
              Debug.Assert(false);
              assetEntry = (AssetEntry) null;
            }
            if (assetEntry != null)
              material.BumpMapTextureID = assetEntry.ID;
          }
          string str5 = c[str2 + ".SpecularMapTexture"];
          if (str5 != null)
          {
            ContentObject entry = ContentManager.Instance.FindEntry(LinkDestination.MainTextureName, (object) str5);
            AssetEntry assetEntry;
            if (entry != null)
            {
              assetEntry = (AssetEntry) entry.Object;
            }
            else
            {
              Debug.Assert(false);
              assetEntry = (AssetEntry) null;
            }
            if (assetEntry != null)
              material.ReflectionTextureID = assetEntry.ID;
          }
          ++index2;
        }
        while (index2 < this.get_LODs(index1).MaterialCount);
      }
      ++index1;
    }
    while ((uint) index1 < this.LODCount);
  }

  public unsafe uint LODCount
  {
    get
    {
      ModelSubHeader* subHeader = this.m_SubHeader;
      if ((IntPtr) subHeader != IntPtr.Zero)
        return (uint) (*(int*) ((IntPtr) subHeader + 44L) + 1);
      GfxModelLOD[] loDs = this.m_LODs;
      return loDs != null ? (uint) loDs.Length : 0U;
    }
  }

  public GfxModelLOD get_LODs(int index)
  {
    return (uint) index < this.LODCount ? this.m_LODs[index] : (GfxModelLOD) null;
  }

  public void set_LODs(int index, GfxModelLOD lod)
  {
    if ((uint) index >= this.LODCount)
      return;
    this.m_LODs[index] = lod;
  }

  public unsafe uint Physics
  {
    get => (uint) *(int*) this.m_SubHeader;
    set => *(int*) this.m_SubHeader = (int) value;
  }

  protected override unsafe Node* BuildModel() => this.m_LODs[0].GetGfx();

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EGfxModel();
      }
      finally
      {
        base.Dispose(true);
      }
    }
    else
    {
      try
      {
        this.\u0021GfxModel();
      }
      finally
      {
        base.Dispose(false);
      }
    }
  }

  ~GfxModel() => this.Dispose(false);
}
