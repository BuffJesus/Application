// Decompiled with JetBrains decompiler
// Type: FableMod.LEV.LEVFile
// Assembly: FableMod.LEV, Version=1.0.4918.429, Culture=neutral, PublicKeyToken=null
// MVID: 267777C9-1C4D-4C46-87D1-7AC25AEC038B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.LEV.dll

using FableMod.CLRCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;

#nullable disable
namespace FableMod.LEV;

public class LEVFile : IDisposable
{
  protected string m_FileName;
  protected unsafe LEVHeader* m_pHeader = (LEVHeader*) \u003CModule\u003E.@new(25UL);
  protected unsafe LEVMapHeader* m_pMapHeader = (LEVMapHeader*) \u003CModule\u003E.@new(22UL);
  protected string[] m_Themes;
  protected unsafe sbyte** m_apszGroundThemeNames = (sbyte**) 0L;
  protected unsafe uint* m_auiGroundThemeValues = (uint*) 0L;
  protected unsafe byte* m_pucPalette2 = (byte*) 0L;
  protected unsafe LEVCellHeader* m_aCells = (LEVCellHeader*) 0L;
  protected unsafe LEVSoundCellHeader* m_aSoundCells = (LEVSoundCellHeader*) 0L;
  protected uint m_uiCellsOffset;
  protected unsafe byte* m_pucData = (byte*) 0L;
  protected uint m_uiDataSize;
  protected unsafe byte* m_pucNavData = (byte*) 0L;
  protected uint m_uiNavDataSize;
  protected List<NavSection> m_Sections = new List<NavSection>();
  protected BufferStream m_File;
  protected bool m_Modified = false;
  protected uint m_uiVersion;

  public unsafe LEVFile()
  {
    LEVMapHeader* pMapHeader = this.m_pMapHeader;
    *(int*) ((IntPtr) pMapHeader + 13L) = 0;
    *(int*) ((IntPtr) pMapHeader + 17L) = 0;
  }

  private void \u007ELEVFile() => this.Destroy();

  private void \u0021LEVFile()
  {
  }

  public unsafe void Destroy()
  {
    LEVCellHeader* aCells = this.m_aCells;
    if ((IntPtr) aCells != IntPtr.Zero)
    {
      \u003CModule\u003E.delete((void*) aCells);
      this.m_aCells = (LEVCellHeader*) 0L;
    }
    LEVSoundCellHeader* aSoundCells = this.m_aSoundCells;
    if ((IntPtr) aSoundCells != IntPtr.Zero)
    {
      \u003CModule\u003E.delete((void*) aSoundCells);
      this.m_aCells = (LEVCellHeader*) 0L;
    }
    byte* pucData = this.m_pucData;
    if ((IntPtr) pucData != IntPtr.Zero)
    {
      \u003CModule\u003E.delete((void*) pucData);
      this.m_pucData = (byte*) 0L;
    }
    byte* pucNavData = this.m_pucNavData;
    if ((IntPtr) pucNavData != IntPtr.Zero)
    {
      \u003CModule\u003E.delete((void*) pucNavData);
      this.m_pucNavData = (byte*) 0L;
    }
    LEVHeader* pHeader = this.m_pHeader;
    if ((IntPtr) pHeader != IntPtr.Zero)
    {
      \u003CModule\u003E.delete((void*) pHeader);
      this.m_pHeader = (LEVHeader*) 0L;
    }
    LEVMapHeader* pMapHeader = this.m_pMapHeader;
    if ((IntPtr) pMapHeader != IntPtr.Zero)
    {
      \u003CModule\u003E.delete((void*) pMapHeader);
      this.m_pMapHeader = (LEVMapHeader*) 0L;
    }
    byte* pucPalette2 = this.m_pucPalette2;
    if ((IntPtr) pucPalette2 != IntPtr.Zero)
    {
      \u003CModule\u003E.delete((void*) pucPalette2);
      this.m_pucPalette2 = (byte*) 0L;
    }
    List<NavSection> sections1 = this.m_Sections;
    if (sections1 != null)
    {
      int index = 0;
      if (0 < sections1.Count)
      {
        do
        {
          this.m_Sections[index]?.Dispose();
          ++index;
        }
        while (index < this.m_Sections.Count);
      }
      if (this.m_Sections is IDisposable sections2)
        sections2.Dispose();
      this.m_Sections = (List<NavSection>) null;
    }
    this.m_Themes = (string[]) null;
    if ((IntPtr) this.m_apszGroundThemeNames != IntPtr.Zero)
    {
      long num = 0;
      do
      {
        \u003CModule\u003E.delete((void*) *(long*) ((IntPtr) this.m_apszGroundThemeNames + num));
        num += 8L;
      }
      while (num < 2048L /*0x0800*/);
      \u003CModule\u003E.delete((void*) this.m_apszGroundThemeNames);
      \u003CModule\u003E.delete((void*) this.m_auiGroundThemeValues);
      this.m_apszGroundThemeNames = (sbyte**) 0L;
      this.m_auiGroundThemeValues = (uint*) 0L;
    }
    BufferStream file = this.m_File;
    if (file == null)
      return;
    file.Dispose();
    this.m_File = (BufferStream) null;
  }

  public unsafe void Open(string filename, ProgressInterface progress)
  {
    FileStream file = File.Open(filename, FileMode.Open, FileAccess.ReadWrite, FileShare.Read);
    Log.Open();
    Log.WriteLine("Reading file {0}", (object) filename);
    this.m_File = new BufferStream(file);
    file.Close();
    file?.Dispose();
    this.m_File.Read((void*) this.m_pHeader, 25);
    LEVHeader* pHeader = this.m_pHeader;
    if (*(ushort*) ((IntPtr) pHeader + 4L) != (ushort) 6404)
      throw new Exception("FABLEMOD_LEV_INVALID_VERSION");
    Log.WriteLine("HeaderSize: {0}", (object) (uint) *(int*) pHeader);
    Log.WriteLine("Version: {0}", (object) *(ushort*) ((IntPtr) this.m_pHeader + 4L));
    Log.WriteLine("Pad: {0}, {1}, {2}", (object) *(byte*) ((IntPtr) this.m_pHeader + 6L), (object) *(byte*) ((IntPtr) this.m_pHeader + 7L), (object) *(byte*) ((IntPtr) this.m_pHeader + 8L));
    Log.WriteLine("Reserved1: {0}", (object) (uint) *(int*) ((IntPtr) this.m_pHeader + 9L));
    Log.WriteLine("ObsOffset: {0}", (object) (uint) *(int*) ((IntPtr) this.m_pHeader + 13L));
    Log.WriteLine("Reserved2: {0}", (object) (uint) *(int*) ((IntPtr) this.m_pHeader + 17L));
    Log.WriteLine("NavOffset: {0}", (object) (uint) *(int*) ((IntPtr) this.m_pHeader + 21L));
    this.m_File.Read((void*) this.m_pMapHeader, 22);
    LEVMapHeader* pMapHeader1 = this.m_pMapHeader;
    if (*(byte*) ((IntPtr) pMapHeader1 + 1L) != (byte) 8)
      throw new Exception("FABLEMOD_LEV_INVALID_MAP_VERSION");
    switch (*(byte*) ((IntPtr) pMapHeader1 + 4L))
    {
      case 8:
      case 9:
        Log.WriteLine("MapHeaderSize: {0}", (object) *(byte*) pMapHeader1);
        Log.WriteLine("MapVersion: {0}", (object) *(byte*) ((IntPtr) this.m_pMapHeader + 1L));
        Log.WriteLine("MapPad: {0}, {1}, {2}", (object) *(byte*) ((IntPtr) this.m_pMapHeader + 2L), (object) *(byte*) ((IntPtr) this.m_pMapHeader + 3L), (object) *(byte*) ((IntPtr) this.m_pMapHeader + 4L));
        Log.WriteLine("UsedID Lo, Hi: {0}, {1}", (object) (uint) *(int*) ((IntPtr) this.m_pMapHeader + 5L), (object) (uint) *(int*) ((IntPtr) this.m_pMapHeader + 9L));
        Log.WriteLine("MapSize: {0}x{1}", (object) (uint) *(int*) ((IntPtr) this.m_pMapHeader + 13L), (object) (uint) *(int*) ((IntPtr) this.m_pMapHeader + 17L));
        Log.WriteLine("Boolean: {0}", (object) *(byte*) ((IntPtr) this.m_pMapHeader + 21L));
        this.m_apszGroundThemeNames = (sbyte**) \u003CModule\u003E.@new(2048UL /*0x0800*/);
        this.m_auiGroundThemeValues = (uint*) \u003CModule\u003E.@new(1024UL /*0x0400*/);
        long num1 = 0;
        long num2 = 0;
        uint num3 = 256 /*0x0100*/;
        do
        {
          sbyte* numPtr = (sbyte*) \u003CModule\u003E.@new(128UL /*0x80*/);
          *(long*) ((IntPtr) this.m_apszGroundThemeNames + num2) = (long) numPtr;
          this.m_File.Read((void*) *(long*) ((IntPtr) this.m_apszGroundThemeNames + num2), 128 /*0x80*/);
          *(int*) ((IntPtr) this.m_auiGroundThemeValues + num1) = (int) this.m_File.ReadUInt32();
          num2 += 8L;
          num1 += 4L;
          num3 += uint.MaxValue;
        }
        while (num3 > 0U);
        uint num4;
        this.m_File.Read((void*) &num4, 4);
        uint num5;
        this.m_File.Read((void*) &num5, 4);
        this.m_uiVersion = num4;
        Log.WriteLine("Version: {0}", (object) num4);
        void* pData1 = \u003CModule\u003E.@new(33792UL);
        this.m_pucPalette2 = (byte*) pData1;
        this.m_File.Read(pData1, 33792);
        if (*(byte*) ((IntPtr) this.m_pMapHeader + 4L) == (byte) 9)
          this.m_File.Read((void*) &0U, 4);
        if (num5 > 0U)
        {
          this.m_Themes = new string[(int) num5 - 1];
          sbyte* pData2 = (sbyte*) \u003CModule\u003E.@new(512UL /*0x0200*/);
          uint index = 0;
          if (0U < num5 - 1U)
          {
            do
            {
              uint iCount = 0;
              this.m_File.Read((void*) &iCount, 4);
              this.m_File.Read((void*) pData2, (int) iCount);
              *(sbyte*) ((long) iCount + (IntPtr) pData2) = (sbyte) 0;
              this.m_Themes[(int) index] = new string(pData2);
              ++index;
            }
            while (index < num5 - 1U);
          }
          \u003CModule\u003E.delete((void*) pData2);
        }
        LEVFile levFile = this;
        levFile.m_uiCellsOffset = (uint) levFile.m_File.Tell();
        LEVMapHeader* pMapHeader2 = this.m_pMapHeader;
        ulong iCount1 = (ulong) (uint) ((*(int*) ((IntPtr) pMapHeader2 + 17L) + 1) * (*(int*) ((IntPtr) pMapHeader2 + 13L) + 1)) * 21UL;
        this.m_aCells = (LEVCellHeader*) \u003CModule\u003E.@new(iCount1);
        Log.WriteLine("CellsOffset: {0}", (object) this.m_File.Tell());
        this.m_File.Read((void*) this.m_aCells, (int) iCount1);
        Log.WriteLine("AfterCellsOffset: {0}", (object) this.m_File.Tell());
        uint num6 = (uint) (*(int*) ((IntPtr) this.m_pHeader + 21L) - this.m_File.Tell());
        this.m_uiDataSize = num6;
        void* pData3 = \u003CModule\u003E.@new((ulong) num6);
        this.m_pucData = (byte*) pData3;
        this.m_File.Read(pData3, (int) this.m_uiDataSize);
        Log.WriteLine("UnknownDataSize: {0}", (object) this.m_uiDataSize);
        this.LoadNavigation();
        this.m_FileName = filename;
        Log.Close();
        break;
      default:
        throw new Exception("FABLEMOD_LEV_INVALID_MAP_PAD");
    }
  }

  public unsafe void Save(string filename, ProgressInterface progress)
  {
    this.LoadNavigation();
    FileStream File = File.Create(filename);
    progress?.Begin(2);
    int num1 = (int) FileControl.Write(File, (void*) this.m_pHeader, 25U);
    int num2 = (int) FileControl.Write(File, (void*) this.m_pMapHeader, 22U);
    long num3 = 0;
    long num4 = 0;
    uint num5 = 256 /*0x0100*/;
    do
    {
      int num6 = (int) FileControl.Write(File, (void*) *(long*) ((IntPtr) this.m_apszGroundThemeNames + num4), 128U /*0x80*/);
      uint num7 = (uint) *(int*) ((IntPtr) this.m_auiGroundThemeValues + num3);
      int num8 = (int) FileControl.Write(File, (void*) &num7, 4U);
      num4 += 8L;
      num3 += 4L;
      num5 += uint.MaxValue;
    }
    while (num5 > 0U);
    uint uiVersion = this.m_uiVersion;
    int num9 = (int) FileControl.Write(File, (void*) &uiVersion, 4U);
    uint num10 = (uint) (this.m_Themes.Length + 1);
    int num11 = (int) FileControl.Write(File, (void*) &num10, 4U);
    int num12 = (int) FileControl.Write(File, (void*) this.m_pucPalette2, 33792U);
    if (*(byte*) ((IntPtr) this.m_pMapHeader + 4L) == (byte) 9)
    {
      uint num13 = 0;
      int num14 = (int) FileControl.Write(File, (void*) &num13, 4U);
    }
    int index = 0;
    string[] themes = this.m_Themes;
    if (0 < themes.Length)
    {
      do
      {
        IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi(themes[index]);
        sbyte* pointer = (sbyte*) hglobalAnsi.ToPointer();
        sbyte* numPtr = pointer;
        if (*pointer != (sbyte) 0)
        {
          do
          {
            ++numPtr;
          }
          while (*numPtr != (sbyte) 0);
        }
        uint uiCount = (uint) ((UIntPtr) numPtr - (UIntPtr) pointer);
        int num15 = (int) FileControl.Write(File, (void*) &uiCount, 4U);
        int num16 = (int) FileControl.Write(File, (void*) pointer, uiCount);
        Marshal.FreeHGlobal(hglobalAnsi);
        ++index;
        themes = this.m_Themes;
      }
      while (index < themes.Length);
    }
    LEVMapHeader* pMapHeader = this.m_pMapHeader;
    int num17 = (int) FileControl.Write(File, (void*) this.m_aCells, (uint) ((ulong) (uint) ((*(int*) ((IntPtr) pMapHeader + 17L) + 1) * (*(int*) ((IntPtr) pMapHeader + 13L) + 1)) * 21UL));
    int num18 = (int) FileControl.Write(File, (void*) this.m_pucData, this.m_uiDataSize);
    LEVHeader levHeader;
    // ISSUE: cpblk instruction
    __memcpy(ref levHeader, (IntPtr) this.m_pHeader, 25);
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &levHeader + 21) = (int) File.Position;
    progress?.Update();
    int num19 = (int) FileControl.Write(File, (void*) this.m_pucNavData, this.m_uiNavDataSize);
    File.Position = 0L;
    int num20 = (int) FileControl.Write(File, (void*) &levHeader, 25U);
    File.Close();
    progress?.End();
  }

  public void LoadNavigation()
  {
    BufferStream file = this.m_File;
    if (file == null || !file.IsOpen())
      return;
    LEVFile levFile = this;
    levFile.ReadNavigation(levFile.m_File);
    this.m_File.Close();
    this.m_File?.Dispose();
    this.m_File = (BufferStream) null;
  }

  public unsafe void ResetSections()
  {
    this.m_Sections.Clear();
    NavSection navSection = new NavSection();
    navSection.Name = "NULL";
    navSection.Width = (float) *(int*) ((IntPtr) this.m_pMapHeader + 13L);
    navSection.Height = (float) *(int*) ((IntPtr) this.m_pMapHeader + 17L);
    this.m_Sections.Add(navSection);
    navSection.CreateGrids();
  }

  public int SectionCount => this.m_Sections.Count;

  public NavSection get_Sections(string name)
  {
    int index = 0;
    if (0 < this.m_Sections.Count)
    {
      while (!(this.m_Sections[index].Name == name))
      {
        ++index;
        if (index >= this.m_Sections.Count)
          goto label_4;
      }
      return this.m_Sections[index];
    }
label_4:
    return (NavSection) null;
  }

  public NavSection get_Sections(int index) => this.m_Sections[index];

  public int ThemeCount => this.m_Themes.Length;

  public string get_Themes(int index) => this.m_Themes[index];

  public unsafe int Width => *(int*) ((IntPtr) this.m_pMapHeader + 13L);

  public unsafe int Height => *(int*) ((IntPtr) this.m_pMapHeader + 17L);

  public unsafe float get_Heights(int x, int y)
  {
    return *(float*) ((IntPtr) this.m_aCells + (((long) *(int*) ((IntPtr) this.m_pMapHeader + 13L) + 1L) * (long) y + (long) x) * 21L + 5L) * 2048f;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public unsafe bool get_Walks(int x, int y)
  {
    return *(byte*) ((IntPtr) this.m_aCells + (((long) *(int*) ((IntPtr) this.m_pMapHeader + 13L) + 1L) * (long) y + (long) x) * 21L + 15L) != (byte) 0;
  }

  public unsafe void set_Walks(int x, int y, [MarshalAs(UnmanagedType.U1)] bool value)
  {
    int num1 = (*(int*) ((IntPtr) this.m_pMapHeader + 13L) + 1) * y + x;
    int num2 = value ? 1 : 0;
    LEVCellHeader* levCellHeaderPtr = (LEVCellHeader*) ((long) num1 * 21L + (IntPtr) this.m_aCells + 15L);
    if ((int) *(byte*) levCellHeaderPtr != num2)
      this.m_Modified = true;
    *(sbyte*) levCellHeaderPtr = (sbyte) num2;
  }

  public int GroundThemeCount => 256 /*0x0100*/;

  public unsafe string get_GroundThemeNames(int index)
  {
    return new string((sbyte*) *(long*) ((IntPtr) this.m_apszGroundThemeNames + (long) index * 8L));
  }

  public unsafe uint get_GroundThemeValues(int index)
  {
    return (uint) *(int*) ((long) index * 4L + (IntPtr) this.m_auiGroundThemeValues);
  }

  public unsafe byte get_GroundThemeIndices(int x, int y, int index)
  {
    return *(byte*) ((long) index + ((((long) *(int*) ((IntPtr) this.m_pMapHeader + 13L) + 1L) * (long) y + (long) x) * 21L + (IntPtr) this.m_aCells) + 10L);
  }

  public unsafe byte get_GroundThemeStrengths(int x, int y, int index)
  {
    return *(byte*) ((long) index + ((((long) *(int*) ((IntPtr) this.m_pMapHeader + 13L) + 1L) * (long) y + (long) x) * 21L + (IntPtr) this.m_aCells) + 13L);
  }

  public bool Modified
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_Modified;
    [param: MarshalAs(UnmanagedType.U1)] set => this.m_Modified = value;
  }

  public string FileName => this.m_FileName;

  protected unsafe void ReadNavigation(BufferStream fileIn)
  {
    fileIn.Seek(*(int*) ((IntPtr) this.m_pHeader + 21L));
    uint num1 = (uint) (fileIn.GetSize() - *(int*) ((IntPtr) this.m_pHeader + 21L));
    this.m_uiNavDataSize = num1;
    byte* pData1 = (byte*) \u003CModule\u003E.@new((ulong) num1);
    this.m_pucNavData = pData1;
    fileIn.Read((void*) pData1, (int) this.m_uiNavDataSize);
    fileIn.Seek(*(int*) ((IntPtr) this.m_pHeader + 21L));
    LEVNavHeader levNavHeader;
    fileIn.Read((void*) &levNavHeader, 8);
    Log.Open();
    Log.WriteLine("Reading navigation data...");
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    Log.WriteLine("NumNavSections: {0}", (object) (uint) ^(int&) ((IntPtr) &levNavHeader + 4));
    sbyte* pData2 = (sbyte*) \u003CModule\u003E.@new(256UL /*0x0100*/);
    uint num2 = 0;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    if (0U < (uint) ^(int&) ((IntPtr) &levNavHeader + 4))
    {
      NavSection navSection;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      do
      {
        uint iCount = 0;
        fileIn.Read((void*) &iCount, 4);
        fileIn.Read((void*) pData2, (int) iCount);
        *(sbyte*) ((long) iCount + (IntPtr) pData2) = (sbyte) 0;
        navSection = new NavSection();
        navSection.Name = new string(pData2);
        Log.WriteLine(" [{0}] Name: {1}", (object) num2, (object) navSection.Name);
        uint iOffset1 = 0;
        fileIn.Read((void*) &iOffset1, 4);
        Log.WriteLine("SectionOffset[{0}] = {1}", (object) num2, (object) iOffset1);
        uint iOffset2 = (uint) fileIn.Tell();
        fileIn.Seek((int) iOffset1);
        if (navSection.Load(fileIn))
        {
          Log.WriteLine("SectionDataSize[{0}] = {1}", (object) num2, (object) (uint) (fileIn.Tell() - (int) iOffset1));
          fileIn.Seek((int) iOffset2);
          this.m_Sections.Add(navSection);
          ++num2;
        }
        else
          goto label_3;
      }
      while (num2 < (uint) ^(int&) ((IntPtr) &levNavHeader + 4));
      goto label_6;
label_3:
      \u003CModule\u003E.delete((void*) pData2);
      navSection?.Dispose();
      throw new Exception("FABLEMOD_LEV_SECTION_LOAD_FAILED");
    }
label_6:
    \u003CModule\u003E.delete((void*) pData2);
    if (this.m_Sections.Count == 0)
      this.m_Sections.Add(new NavSection()
      {
        Name = "NULL",
        Width = (float) *(int*) ((IntPtr) this.m_pMapHeader + 13L),
        Height = (float) *(int*) ((IntPtr) this.m_pMapHeader + 17L)
      });
    Log.Close();
  }

  protected unsafe void WriteNavigation(FileStream fileOut, ProgressInterface progress)
  {
    progress?.Begin(3);
    uint num1 = 0;
    progress?.Begin(this.m_Sections.Count);
    int index1 = 0;
    if (0 < this.m_Sections.Count)
    {
      do
      {
        this.m_Sections[index1].CreateNodes();
        NavSection section = this.m_Sections[index1];
        num1 += (uint) section.NodeCount;
        progress?.Update();
        ++index1;
      }
      while (index1 < this.m_Sections.Count);
    }
    progress?.End();
    LEVNavHeader levNavHeader;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ref levNavHeader = 0;
    // ISSUE: cast to a reference type
    // ISSUE: explicit reference operation
    ^(int&) ((IntPtr) &levNavHeader + 4) = 0;
    if (num1 == 0U)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref levNavHeader = (int) (uint) ((ulong) (uint) (int) fileOut.Position + 8UL);
      int num2 = (int) FileControl.Write(fileOut, (void*) &levNavHeader, 8U);
      if (progress != null)
        progress.Update();
      else
        goto label_53;
    }
    else
    {
      progress?.Begin(3);
      uint num3 = 0;
      uint index2 = 0;
      if (0U < (uint) this.m_Sections.Count)
      {
        do
        {
          NavSection section = this.m_Sections[(int) index2];
          if (section.NodeCount > 0)
          {
            uint index3 = 0;
            if (0U < (uint) section.NodeCount)
            {
              do
              {
                NavNode navNode = section.get_Nodes((int) index3);
                if (navNode.Type == NavNodeType.Navigation || navNode.Type == NavNodeType.Dynamic)
                {
                  NavigationNode navigationNode = (NavigationNode) navNode;
                  if (num3 < navigationNode.Level)
                    num3 = navigationNode.Level;
                }
                ++index3;
              }
              while (index3 < (uint) section.NodeCount);
            }
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ((IntPtr) &levNavHeader + 4) = ^(int&) ((IntPtr) &levNavHeader + 4) + 1;
          }
          ++index2;
        }
        while (index2 < (uint) this.m_Sections.Count);
      }
      int position1 = (int) fileOut.Position;
      int num4 = (int) FileControl.Write(fileOut, (void*) &levNavHeader, 8U);
      uint index4 = 0;
      if (0U < (uint) this.m_Sections.Count)
      {
        do
        {
          if (this.m_Sections[(int) index4].NodeCount > 0)
          {
            uint length = (uint) this.m_Sections[(int) index4].Name.Length;
            IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi(this.m_Sections[(int) index4].Name);
            int num5 = (int) FileControl.Write(fileOut, (void*) &length, 4U);
            int num6 = (int) FileControl.Write(fileOut, hglobalAnsi.ToPointer(), length);
            Marshal.FreeHGlobal(hglobalAnsi);
            uint num7 = 0;
            int num8 = (int) FileControl.Write(fileOut, (void*) &num7, 4U);
          }
          ++index4;
        }
        while (index4 < (uint) this.m_Sections.Count);
      }
      progress?.Update();
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      ^(int&) ref levNavHeader = (int) fileOut.Position;
      ulong count1 = (ulong) this.m_Sections.Count;
      uint* numPtr1 = (uint*) \u003CModule\u003E.@new(count1 > 4611686018427387903UL /*0x3FFFFFFFFFFFFFFF*/ ? ulong.MaxValue : count1 * 4UL);
      ulong count2 = (ulong) this.m_Sections.Count;
      uint* numPtr2 = (uint*) \u003CModule\u003E.@new(count2 > 4611686018427387903UL /*0x3FFFFFFFFFFFFFFF*/ ? ulong.MaxValue : count2 * 4UL);
      progress?.Begin(this.m_Sections.Count);
      uint index5 = 0;
      if (0U < (uint) this.SectionCount)
      {
        uint* numPtr3 = numPtr1;
        long num9 = (long) ((IntPtr) numPtr2 - (IntPtr) numPtr1);
        do
        {
          if (this.get_Sections((int) index5).NodeCount > 0)
          {
            uint position2 = (uint) (int) fileOut.Position;
            *numPtr3 = position2;
            LEVNavSectionHeader navSectionHeader;
            int num10 = (int) FileControl.Write(fileOut, (void*) &navSectionHeader, 20U);
            if (!this.get_Sections((int) index5).Save(fileOut))
              \u003CModule\u003E.delete((void*) numPtr1);
            *(int*) (num9 + (IntPtr) numPtr3) = (int) fileOut.Position + ((int) *numPtr3 - (int) position2) + 4;
          }
          progress?.Update();
          ++index5;
          numPtr3 += 4L;
        }
        while (index5 < (uint) this.SectionCount);
      }
      progress?.End();
      uint num11 = 2898128658;
      int num12 = (int) FileControl.Write(fileOut, (void*) &num11, 4U);
      long position3 = fileOut.Position;
      fileOut.Position = (long) position1;
      int num13 = (int) FileControl.Write(fileOut, (void*) &levNavHeader, 8U);
      uint index6 = 0;
      if (0U < (uint) this.SectionCount)
      {
        uint* numPtr4 = numPtr2;
        long num14 = (long) ((IntPtr) numPtr1 - (IntPtr) numPtr2);
        do
        {
          if (this.get_Sections((int) index6).NodeCount > 0)
          {
            long num15 = (long) (this.get_Sections((int) index6).Name.Length + 4);
            fileOut.Position += num15;
            int num16 = (int) FileControl.Write(fileOut, (void*) ((long) index6 * 4L + (IntPtr) numPtr1), 4U);
            int position4 = (int) fileOut.Position;
            fileOut.Position = (long) (uint) *(int*) (num14 + (IntPtr) numPtr4);
            LEVNavSectionHeader navSectionHeader;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ref navSectionHeader = (int) *numPtr4;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ((IntPtr) &navSectionHeader + 4) = 8;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(int&) ((IntPtr) &navSectionHeader + 16 /*0x10*/) = (int) num3 + 1;
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &navSectionHeader + 8) = (float) *(int*) ((IntPtr) this.m_pMapHeader + 13L);
            // ISSUE: cast to a reference type
            // ISSUE: explicit reference operation
            ^(float&) ((IntPtr) &navSectionHeader + 12) = (float) this.Height;
            int num17 = (int) FileControl.Write(fileOut, (void*) &navSectionHeader, 20U);
            fileOut.Position = (long) position4;
          }
          ++index6;
          numPtr4 += 4L;
        }
        while (index6 < (uint) this.SectionCount);
      }
      if (progress != null)
      {
        progress.Update();
        progress.End();
      }
      \u003CModule\u003E.delete((void*) numPtr1);
    }
    progress?.Begin(this.SectionCount);
label_53:
    int index7 = 0;
    if (0 < this.SectionCount)
    {
      do
      {
        this.m_Sections[index7].CreateGrids();
        progress?.Update();
        ++index7;
      }
      while (index7 < this.SectionCount);
    }
    if (progress == null)
      return;
    progress.End();
    progress.End();
  }

  [HandleProcessCorruptedStateExceptions]
  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.Destroy();
    }
    else
    {
      try
      {
      }
      finally
      {
        // ISSUE: explicit finalizer call
        base.Finalize();
      }
    }
  }

  public virtual void Dispose()
  {
    this.Dispose(true);
    GC.SuppressFinalize((object) this);
  }

  ~LEVFile() => this.Dispose(false);
}
