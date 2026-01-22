// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.SubMesh
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using System;

#nullable disable
namespace FableMod.Gfx.Integration;

public class SubMesh
{
  private unsafe SUBM* m_Submesh;

  public unsafe SubMesh(SUBM* submesh) => this.m_Submesh = submesh;

  public unsafe uint MaterialID
  {
    get => (uint) *(int*) this.m_Submesh;
    set => *(int*) this.m_Submesh = (int) value;
  }

  public unsafe uint DestroyableMeshLevel => (uint) *(int*) ((IntPtr) this.m_Submesh + 4L);

  public unsafe uint VertexCount => (uint) *(int*) ((IntPtr) this.m_Submesh + 36L);

  public unsafe uint FaceCount => (uint) *(int*) ((IntPtr) this.m_Submesh + 40L);
}
