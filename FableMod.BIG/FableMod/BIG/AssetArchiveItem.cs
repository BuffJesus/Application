// Decompiled with JetBrains decompiler
// Type: FableMod.BIG.AssetArchiveItem
// Assembly: FableMod.BIG, Version=1.0.4918.425, Culture=neutral, PublicKeyToken=null
// MVID: 88942552-073F-4D63-ADC6-04A8B51D93E5
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.BIG.dll

#nullable disable
namespace FableMod.BIG;

public class AssetArchiveItem
{
  protected AssetArchive m_Archive;
  protected uint m_SourceStartOffset;
  protected uint m_SourceLength;

  public uint SourceStartOffset => this.m_SourceStartOffset;

  public uint SourceLength => this.m_SourceLength;

  public AssetArchive Archive => this.m_Archive;
}
