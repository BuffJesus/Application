// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.Control
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using System;
using System.Runtime.InteropServices;
using System.Text;

#nullable disable
namespace FableMod.ContentManagement;

public class Control : IDisposable
{
  private uint m_ID;
  private string m_Name;
  private MemberCollection m_Members;

  public Control(uint ID, string name)
  {
    this.m_ID = ID;
    this.m_Name = name;
    this.m_Members = new MemberCollection();
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  public Control(Control control)
  {
    this.m_ID = control.m_ID;
    this.m_Name = control.m_Name;
    this.m_Members = new MemberCollection(control.m_Members);
    // ISSUE: explicit constructor call
    base.\u002Ector();
  }

  private void \u007EControl()
  {
    this.m_Name = (string) null;
    this.m_Members = (MemberCollection) null;
  }

  public void Print(StringBuilder sb, string tab)
  {
    uint id = this.m_ID;
    string name = this.m_Name;
    sb.AppendFormat("{0}Control: {1}{2}", (object) tab, (object) (id.ToString("X") + " - " + name), (object) Environment.NewLine);
    string tab1 = tab + "  ";
    int index = 0;
    if (0 >= this.m_Members.Count)
      return;
    MemberCollection members;
    do
    {
      sb.AppendFormat("{0} Index {1}{2}", (object) tab, (object) index, (object) Environment.NewLine);
      this.m_Members[index].Print(sb, tab1);
      ++index;
      members = this.m_Members;
    }
    while (index < members.Count);
  }

  public unsafe int ReadIn(sbyte* data, int length, [MarshalAs(UnmanagedType.U1)] bool IsXBox)
  {
    int num1;
    if (IsXBox)
    {
      byte num2 = (byte) *data;
      num1 = 1;
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      uint num3 = (uint) ^(byte&) (ref this.m_ID + 3L);
      if ((int) num3 != (int) num2)
        throw new Exception("Control byte mismatch.  Byte=" + num3.ToString("X"));
    }
    else
    {
      uint num4 = (uint) *(int*) data;
      num1 = 4;
      uint id = this.m_ID;
      if (((int) ((id & 16711680U /*0xFF0000*/ | id >> 16 /*0x10*/) >> 8) | ((int) id << 16 /*0x10*/ | (int) id & 65280) << 8) != (int) num4)
        throw new Exception("Control bytes mismatch. ID=" + id.ToString("X"));
    }
    try
    {
      return num1 + this.m_Members.ReadIn((sbyte*) ((long) num1 + (IntPtr) data), length - num1);
    }
    catch (Exception ex)
    {
      throw new Exception("Control ID=" + this.m_ID.ToString("X") + Environment.NewLine + ex.Message);
    }
  }

  public unsafe int Write(sbyte* data, int length, [MarshalAs(UnmanagedType.U1)] bool IsXBox)
  {
    int num;
    if (IsXBox)
    {
      // ISSUE: cast to a reference type
      // ISSUE: explicit reference operation
      *data = (sbyte) ^(byte&) (ref this.m_ID + 3L);
      num = 1;
    }
    else
    {
      uint id = this.m_ID;
      *(int*) data = (int) ((id & 16711680U /*0xFF0000*/ | id >> 16 /*0x10*/) >> 8) | ((int) id << 16 /*0x10*/ | (int) id & 65280) << 8;
      num = 4;
    }
    try
    {
      return num + this.m_Members.Write((sbyte*) ((long) num + (IntPtr) data), length - num);
    }
    catch (Exception ex)
    {
      throw new Exception("Control ID=" + this.m_ID.ToString("X") + Environment.NewLine + ex.Message);
    }
  }

  public uint ID => this.m_ID;

  public string Name => this.m_Name;

  public MemberCollection Members => this.m_Members;

  public override string ToString()
  {
    if (!DefinitionDB.DeveloperModeEnabled())
      return this.m_Name;
    uint id = this.m_ID;
    string name = this.m_Name;
    return id.ToString("X") + " - " + name;
  }

  protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      this.\u007EControl();
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
