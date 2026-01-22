// Decompiled with JetBrains decompiler
// Type: FableMod.TNG.Variable
// Assembly: FableMod.TNG, Version=1.0.4918.434, Culture=neutral, PublicKeyToken=null
// MVID: D9131661-A628-42D1-B5F7-4150ACB2CB8F
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.TNG.dll

using System;
using System.Globalization;
using System.IO;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using System.Xml;

#nullable disable
namespace FableMod.TNG;

public class Variable : Element
{
  protected static CultureInfo m_StaticCulture = (CultureInfo) null;
  protected VariableType m_Type = VariableType.Unknown;
  protected string m_Restriction;
  protected object m_Value = (object) "";
  protected object m_Default = (object) null;
  protected bool m_Modified = false;

  public Variable()
  {
    // ISSUE: fault handler
    try
    {
      if (Variable.m_StaticCulture != null)
        return;
      Variable.m_StaticCulture = new CultureInfo("en-US");
      Variable.m_StaticCulture.NumberFormat.NumberDecimalDigits = 6;
      Variable.m_StaticCulture.NumberFormat.NumberGroupSeparator = "";
    }
    __fault
    {
      base.Dispose(true);
    }
  }

  private void \u007EVariable()
  {
    this.m_Value = (object) null;
    this.m_Default = (object) null;
  }

  public override void Load(TNGDefinitions definitions, XmlNode node)
  {
    base.Load(definitions, node);
    XmlAttribute attribute1 = node.Attributes["type"];
    if (attribute1 != null)
      this.m_Type = (VariableType) Enum.Parse(typeof (VariableType), attribute1.InnerText, true);
    XmlAttribute attribute2 = node.Attributes["restriction"];
    if (attribute2 != null)
      this.m_Restriction = attribute2.InnerText;
    this.SetRawValue((object) node.Attributes["value"].InnerText);
    Variable variable = this;
    variable.m_Default = variable.m_Value;
  }

  public override void Save(TextWriter writer)
  {
    if (this.IsDefault() && !this.SaveDefault)
      return;
    string str1 = this.StringValue;
    switch (this.m_Type)
    {
      case VariableType.QuoteString:
      case VariableType.GameEnum:
        string str2 = "\"";
        str1 = str2 + str1 + str2;
        break;
    }
    writer.WriteLine("{0} {1};", (object) this.m_Name, (object) str1);
  }

  public override void CopyTo(Element element)
  {
    base.CopyTo(element);
    Variable variable = (Variable) element;
    variable.m_Type = this.m_Type;
    variable.m_Value = this.m_Value;
    variable.m_Default = this.m_Default;
    variable.m_Restriction = this.m_Restriction;
  }

  public VariableType Type => this.m_Type;

  public string Restriction
  {
    get => this.m_Restriction;
    set => this.m_Restriction = value;
  }

  public object Value
  {
    get => this.m_Value;
    set
    {
      if (this.m_Value.Equals(value))
        return;
      this.SetRawValue(value);
      this.Modified = true;
      Variable variable = this;
      variable.OnChanged((Element) variable);
    }
  }

  public string StringValue
  {
    get
    {
      switch (this.m_Type)
      {
        case VariableType.Float:
          return ((float) this.m_Value).ToString("N5", (IFormatProvider) Variable.m_StaticCulture);
        case VariableType.Boolean:
          return this.m_Value.ToString().ToUpper();
        default:
          return this.m_Value.ToString();
      }
    }
  }

  public override bool Modified
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_Modified;
    [param: MarshalAs(UnmanagedType.U1)] set => this.m_Modified = value;
  }

  public override void ToDefault()
  {
    object obj1 = this.m_Default;
    if (obj1 == null)
      return;
    object obj2 = obj1;
    if (this.m_Value.Equals(obj2))
      return;
    this.SetRawValue(obj2);
    this.Modified = true;
    Variable variable = this;
    variable.OnChanged((Element) variable);
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public override bool IsDefault()
  {
    object obj = this.m_Default;
    return obj != null && obj.Equals(this.m_Value);
  }

  public override bool HasDefault
  {
    [return: MarshalAs(UnmanagedType.U1)] get => this.m_Default != null;
  }

  internal void SetRawValue(object value)
  {
    if (this.m_Type == VariableType.Unknown)
    {
      string str = value.ToString();
      this.m_Type = !str.StartsWith("\"") || !str.EndsWith("\"") ? (str == "TRUE" || str == "FALSE" ? VariableType.Boolean : VariableType.String) : VariableType.QuoteString;
    }
    switch (this.m_Type)
    {
      case VariableType.String:
      case VariableType.QuoteString:
      case VariableType.GameEnum:
        string str1 = value.ToString();
        if (str1.StartsWith("\"") && str1.EndsWith("\""))
          str1 = str1.Substring(1, str1.Length - 2);
        this.m_Value = (object) str1;
        break;
      case VariableType.Float:
        if (value.GetType() == typeof (string))
        {
          this.m_Value = (object) float.Parse((string) value, (IFormatProvider) Variable.m_StaticCulture);
          break;
        }
        this.m_Value = value;
        break;
      case VariableType.Integer:
        this.m_Value = (object) int.Parse(value.ToString());
        break;
      case VariableType.Boolean:
        this.m_Value = (object) bool.Parse(value.ToString());
        break;
      case VariableType.UID:
        this.m_Value = (object) value.ToString();
        break;
    }
  }

  protected override Element Factory() => (Element) new Variable();

  [HandleProcessCorruptedStateExceptions]
  protected override void Dispose([MarshalAs(UnmanagedType.U1)] bool _param1)
  {
    if (_param1)
    {
      try
      {
        this.\u007EVariable();
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
