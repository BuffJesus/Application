// Decompiled with JetBrains decompiler
// Type: FableMod.ContentManagement.Member
// Assembly: FableMod.ContentManagement, Version=1.0.4918.432, Culture=neutral, PublicKeyToken=null
// MVID: D9A08E19-393A-4912-B5CC-AB850956F587
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.ContentManagement.dll

using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Xml;

#nullable disable
namespace FableMod.ContentManagement;

public class Member : BaseMember
{
  private MemberType m_Type;
  private Link m_Link;
  private object m_Value;

  public Member(Member member)
  {
    this.m_Type = member.m_Type;
    this.m_Link = member.m_Link;
    this.m_Name = member.m_Name;
    this.m_Comments = member.m_Comments;
    // ISSUE: explicit constructor call
    base.\u002Ector();
    // ISSUE: fault handler
    try
    {
      if (this.m_Type == MemberType.STRING)
        this.m_Value = (object) "";
      else
        this.Value = (object) "0";
    }
    __fault
    {
      base.Dispose(true);
    }
  }

  public Member(MemberType type, string name, string comments, Link link)
  {
    this.m_Type = type;
    this.m_Link = link;
    this.m_Name = name;
    // ISSUE: explicit constructor call
    base.\u002Ector();
    // ISSUE: fault handler
    try
    {
      if (this.m_Type == MemberType.STRING)
        this.m_Value = (object) "";
      else
        this.m_Value = (object) "0";
    }
    __fault
    {
      base.Dispose(true);
    }
  }

  public Member()
  {
  }

  private void \u007EMember() => this.m_Value = (object) null;

  public void Load(XmlNode memberNode)
  {
    XmlAttribute attribute1 = memberNode.Attributes["name"];
    XmlAttribute attribute2 = memberNode.Attributes["type"];
    XmlAttribute attribute3 = memberNode.Attributes["comments"];
    this.m_Type = (MemberType) Enum.Parse(typeof (MemberType), attribute2.InnerText, true);
    this.m_Name = attribute1.InnerText;
    if (attribute3 != null)
      this.m_Comments = attribute3.InnerText;
    else
      this.m_Comments = "";
    XmlNode xmlNode = (XmlNode) memberNode["Link"];
    if (xmlNode == null)
      return;
    XmlAttribute attribute4 = xmlNode.Attributes["to"];
    XmlAttribute attribute5 = xmlNode.Attributes["restriction"];
    if (attribute5 != null)
      this.m_Link = new Link((LinkDestination) Enum.Parse(typeof (LinkDestination), attribute4.InnerText, true), attribute5.InnerText);
    else
      this.m_Link = new Link((LinkDestination) Enum.Parse(typeof (LinkDestination), attribute4.InnerText, true), (string) null);
  }

  public override void FixLinks(LinkDestination link, object oldValue, object newValue)
  {
    Link link1 = this.m_Link;
    if (link1 == null || link1.To != link || !this.m_Value.Equals(oldValue))
      return;
    this.Value = newValue;
  }

  public override void Print(StringBuilder sb, string tab)
  {
    object[] objArray = new object[5]
    {
      (object) tab,
      (object) this.m_Type.ToString().ToLower(),
      (object) this.m_Name,
      (object) this.m_Value.ToString(),
      (object) Environment.NewLine
    };
    sb.AppendFormat("{0}({1}){2}: {3}{4}", objArray);
  }

  public override unsafe int ReadIn(sbyte* data, int length)
  {
    if (length < this.Size)
      throw new Exception("Buffer underrun reading " + this.m_Name + ".");
    switch (this.m_Type)
    {
      case MemberType.INT:
        this.m_Value = (object) *(int*) data;
        break;
      case MemberType.UINT:
        this.m_Value = (object) (uint) *(int*) data;
        break;
      case MemberType.SHORT:
        this.m_Value = (object) *(short*) data;
        break;
      case MemberType.USHORT:
        this.m_Value = (object) *(ushort*) data;
        break;
      case MemberType.CHAR:
        this.m_Value = (object) *data;
        break;
      case MemberType.BYTE:
        this.m_Value = (object) (byte) *data;
        break;
      case MemberType.STRING:
        this.m_Value = (object) new string(data);
        break;
      case MemberType.FLOAT:
        this.m_Value = (object) *(float*) data;
        break;
      case MemberType.DOUBLE:
        this.m_Value = (object) *(double*) data;
        break;
    }
    return this.Size;
  }

  public override unsafe int Write(sbyte* data, int length)
  {
    if (length < this.Size)
      throw new Exception("Buffer underrun writing " + this.m_Name + ".");
    switch (this.m_Type)
    {
      case MemberType.INT:
        *(int*) data = (int) this.m_Value;
        break;
      case MemberType.UINT:
        *(int*) data = (int) (uint) this.m_Value;
        break;
      case MemberType.SHORT:
        *(short*) data = (short) this.m_Value;
        break;
      case MemberType.USHORT:
        *(short*) data = (short) (ushort) this.m_Value;
        break;
      case MemberType.CHAR:
        *data = (sbyte) this.m_Value;
        break;
      case MemberType.BYTE:
        *data = (sbyte) (byte) this.m_Value;
        break;
      case MemberType.STRING:
        IntPtr hglobalAnsi = Marshal.StringToHGlobalAnsi((string) this.m_Value);
        void* pointer = hglobalAnsi.ToPointer();
        long num1 = (long) ((IntPtr) data - (IntPtr) pointer);
        sbyte num2;
        do
        {
          num2 = *(sbyte*) pointer;
          *(sbyte*) (num1 + (IntPtr) pointer) = num2;
          ++pointer;
        }
        while (num2 != (sbyte) 0);
        Marshal.FreeHGlobal(hglobalAnsi);
        break;
      case MemberType.FLOAT:
        if (float.IsNaN((float) this.m_Value))
        {
          *(int*) data = -1;
          break;
        }
        *(float*) data = (float) this.m_Value;
        break;
      case MemberType.DOUBLE:
        *(double*) data = (double) this.m_Value;
        break;
    }
    return this.Size;
  }

  public int Size
  {
    get
    {
      switch (this.m_Type)
      {
        case MemberType.INT:
        case MemberType.UINT:
        case MemberType.FLOAT:
          return 4;
        case MemberType.SHORT:
        case MemberType.USHORT:
          return 2;
        case MemberType.CHAR:
        case MemberType.BYTE:
          return 1;
        case MemberType.STRING:
          object obj = this.m_Value;
          return obj != null ? ((string) obj).Length + 1 : 1;
        case MemberType.DOUBLE:
          return 8;
        default:
          return 0;
      }
    }
  }

  public object Value
  {
    get => this.m_Value;
    set
    {
      string s = value as string;
      switch (this.m_Type)
      {
        case MemberType.INT:
          if (s != (string) null)
          {
            this.m_Value = (object) int.Parse(s);
            break;
          }
          this.m_Value = (object) (int) value;
          break;
        case MemberType.UINT:
          if (s != (string) null)
          {
            this.m_Value = (object) uint.Parse(s);
            break;
          }
          this.m_Value = (object) (uint) value;
          break;
        case MemberType.SHORT:
          if (s != (string) null)
          {
            this.m_Value = (object) short.Parse(s);
            break;
          }
          this.m_Value = (object) (short) value;
          break;
        case MemberType.USHORT:
          if (s != (string) null)
          {
            this.m_Value = (object) ushort.Parse(s);
            break;
          }
          this.m_Value = (object) (ushort) value;
          break;
        case MemberType.CHAR:
          if (s != (string) null)
          {
            this.m_Value = (object) sbyte.Parse(s);
            break;
          }
          this.m_Value = (object) (sbyte) value;
          break;
        case MemberType.BYTE:
          if (s != (string) null)
          {
            this.m_Value = (object) byte.Parse(s);
            break;
          }
          this.m_Value = (object) (byte) value;
          break;
        case MemberType.STRING:
          this.m_Value = (object) s;
          break;
        case MemberType.FLOAT:
          if (s != (string) null)
          {
            this.m_Value = (object) float.Parse(s);
            break;
          }
          this.m_Value = (object) (float) value;
          break;
        case MemberType.DOUBLE:
          if (s != (string) null)
          {
            this.m_Value = (object) double.Parse(s);
            break;
          }
          this.m_Value = (object) (double) value;
          break;
      }
    }
  }

  public MemberType Type => this.m_Type;

  public Link Link => this.m_Link;

  public override string ToString()
  {
    StringBuilder stringBuilder = new StringBuilder(64 /*0x40*/);
    string name = this.m_Name;
    stringBuilder.AppendFormat("({0}){1}: {2}", (object) this.m_Type.ToString().ToLower(), (object) name, (object) this.m_Value.ToString());
    return stringBuilder.ToString();
  }

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EMember();
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
