// Decompiled with JetBrains decompiler
// Type: FableMod.Gfx.Integration.ButtonConfiguration
// Assembly: FableMod.Gfx.Integration, Version=1.0.4918.443, Culture=neutral, PublicKeyToken=null
// MVID: 11191760-BFE8-4917-AB7A-ED7AB3A5A394
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Gfx.Integration.dll

using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

#nullable disable
namespace FableMod.Gfx.Integration;

public class ButtonConfiguration
{
  private List<CfgButton> m_Buttons;
  private static Dictionary<Buttons, Keys> m_Keys;
  private static Dictionary<Buttons, int> m_VKeys;

  public ButtonConfiguration()
  {
    this.m_Buttons = new List<CfgButton>();
    if (ButtonConfiguration.m_VKeys == null)
    {
      ButtonConfiguration.m_VKeys = new Dictionary<Buttons, int>();
      ButtonConfiguration.m_VKeys[Buttons.Left] = 37;
      ButtonConfiguration.m_VKeys[Buttons.Up] = 38;
      ButtonConfiguration.m_VKeys[Buttons.Right] = 39;
      ButtonConfiguration.m_VKeys[Buttons.Down] = 40;
      ButtonConfiguration.m_VKeys[Buttons.A] = 65;
      ButtonConfiguration.m_VKeys[Buttons.W] = 87;
      ButtonConfiguration.m_VKeys[Buttons.S] = 83;
      ButtonConfiguration.m_VKeys[Buttons.D] = 68;
      ButtonConfiguration.m_VKeys[Buttons.Tab] = 9;
      ButtonConfiguration.m_VKeys[Buttons.Escape] = 27;
      ButtonConfiguration.m_VKeys[Buttons.Space] = 32 /*0x20*/;
      ButtonConfiguration.m_VKeys[Buttons.Delete] = 46;
      ButtonConfiguration.m_VKeys[Buttons.Backspace] = 8;
      ButtonConfiguration.m_VKeys[Buttons.Enter] = 13;
    }
    if (ButtonConfiguration.m_Keys != null)
      return;
    ButtonConfiguration.m_Keys = new Dictionary<Buttons, Keys>();
    ButtonConfiguration.m_Keys[Buttons.Left] = Keys.Left;
    ButtonConfiguration.m_Keys[Buttons.Up] = Keys.Up;
    ButtonConfiguration.m_Keys[Buttons.Right] = Keys.Right;
    ButtonConfiguration.m_Keys[Buttons.Down] = Keys.Down;
    ButtonConfiguration.m_Keys[Buttons.A] = Keys.A;
    ButtonConfiguration.m_Keys[Buttons.W] = Keys.W;
    ButtonConfiguration.m_Keys[Buttons.S] = Keys.S;
    ButtonConfiguration.m_Keys[Buttons.D] = Keys.D;
    ButtonConfiguration.m_Keys[Buttons.Tab] = Keys.Tab;
    ButtonConfiguration.m_Keys[Buttons.Escape] = Keys.Escape;
    ButtonConfiguration.m_Keys[Buttons.Space] = Keys.Space;
    ButtonConfiguration.m_Keys[Buttons.Delete] = Keys.Delete;
    ButtonConfiguration.m_Keys[Buttons.Backspace] = Keys.Back;
    ButtonConfiguration.m_Keys[Buttons.Enter] = Keys.Return;
    ButtonConfiguration.m_Keys[Buttons.NumPad0] = Keys.NumPad0;
    ButtonConfiguration.m_Keys[Buttons.NumPad1] = Keys.NumPad1;
    ButtonConfiguration.m_Keys[Buttons.NumPad2] = Keys.NumPad2;
    ButtonConfiguration.m_Keys[Buttons.NumPad3] = Keys.NumPad3;
    ButtonConfiguration.m_Keys[Buttons.NumPad4] = Keys.NumPad4;
    ButtonConfiguration.m_Keys[Buttons.NumPad5] = Keys.NumPad5;
    ButtonConfiguration.m_Keys[Buttons.NumPad6] = Keys.NumPad6;
    ButtonConfiguration.m_Keys[Buttons.NumPad7] = Keys.NumPad7;
    ButtonConfiguration.m_Keys[Buttons.NumPad8] = Keys.NumPad8;
    ButtonConfiguration.m_Keys[Buttons.NumPad9] = Keys.NumPad9;
  }

  public void Load(string fileName)
  {
    XmlDocument xmlDocument = new XmlDocument();
    xmlDocument.Load(fileName);
    XmlNode xmlNode = ((XmlNode) xmlDocument["buttonconfiguration"] ?? throw new System.Exception("ButtonConfiguration::Load: invalid root")).FirstChild;
    if (xmlNode == null)
      return;
    do
    {
      if (xmlNode.Name == "button")
      {
        string innerText1 = xmlNode.Attributes["value"].InnerText;
        string innerText2 = xmlNode.Attributes["name"].InnerText;
        char[] chArray1 = new char[1]{ '+' };
        Buttons button = Buttons.None;
        char[] chArray2 = chArray1;
        string[] strArray = innerText1.Split(chArray2);
        int index = 0;
        if (0 < strArray.Length)
        {
          do
          {
            string btnString = strArray[index];
            button |= ButtonConfiguration.ToButton(btnString);
            ++index;
          }
          while (index < strArray.Length);
        }
        this.Add(innerText2, button);
      }
      xmlNode = xmlNode.NextSibling;
    }
    while (xmlNode != null);
  }

  public void Save(string fileName)
  {
  }

  public void Add(string name, string btnString)
  {
    char[] chArray = new char[1]{ '+' };
    Buttons button = Buttons.None;
    string[] strArray = btnString.Split(chArray);
    int index = 0;
    if (0 < strArray.Length)
    {
      do
      {
        string btnString1 = strArray[index];
        button |= ButtonConfiguration.ToButton(btnString1);
        ++index;
      }
      while (index < strArray.Length);
    }
    this.Add(name, button);
  }

  public void Add(string name, Buttons button)
  {
    List<CfgButton>.Enumerator enumerator = this.m_Buttons.GetEnumerator();
    if (enumerator.MoveNext())
    {
      CfgButton current;
      do
      {
        current = enumerator.Current;
        if (current.Name == name)
          goto label_3;
      }
      while (enumerator.MoveNext());
      goto label_4;
label_3:
      current.Button = button;
      goto label_5;
    }
label_4:
    this.m_Buttons.Add(new CfgButton(name, button));
label_5:
    this.m_Buttons.Sort((IComparer<CfgButton>) new ButtonComparer());
  }

  public void Enable(string name, [MarshalAs(UnmanagedType.U1)] bool enable)
  {
    Regex regex = new Regex(name);
    List<CfgButton>.Enumerator enumerator = this.m_Buttons.GetEnumerator();
    if (!enumerator.MoveNext())
      return;
    do
    {
      CfgButton current = enumerator.Current;
      if (regex.IsMatch(current.Name))
        current.Enabled = enable;
    }
    while (enumerator.MoveNext());
  }

  public void Attach(Control c)
  {
    c.MouseDown += new MouseEventHandler(this.Control_MouseDown);
    c.MouseUp += new MouseEventHandler(this.Control_MouseUp);
    c.KeyDown += new KeyEventHandler(this.Control_KeyDown);
    c.KeyUp += new KeyEventHandler(this.Control_KeyUp);
  }

  [return: MarshalAs(UnmanagedType.U1)]
  public bool IsDown(string name)
  {
    List<CfgButton>.Enumerator enumerator = this.m_Buttons.GetEnumerator();
    if (enumerator.MoveNext())
    {
      CfgButton current;
      do
      {
        current = enumerator.Current;
        if (current.Enabled && current.Name == name)
          goto label_3;
      }
      while (enumerator.MoveNext());
      goto label_9;
label_3:
      int num;
      if (ButtonConfiguration.m_VKeys.TryGetValue(current.Button & ~Buttons.ModifierKeys, out num))
      {
        if (this.ModifiersOk(current.Button) && (ushort) ((uint) \u003CModule\u003E.GetAsyncKeyState(num) & 32768U /*0x8000*/) != (ushort) 0)
          return true;
      }
      else if ((current.Button & ~Buttons.ModifierKeys) == Buttons.None)
        return this.ModifiersOk(current.Button);
      return false;
    }
label_9:
    return false;
  }

  public event ButtonEventHandler OnButtonDown;

  [SpecialName]
  protected void raise_OnButtonDown(string value0, MouseEventArgs value1)
  {
    ButtonEventHandler storeOnButtonDown = this.\u003Cbacking_store\u003EOnButtonDown;
    if (storeOnButtonDown == null)
      return;
    storeOnButtonDown(value0, value1);
  }

  public event ButtonEventHandler OnButtonUp;

  [SpecialName]
  protected void raise_OnButtonUp(string value0, MouseEventArgs value1)
  {
    ButtonEventHandler backingStoreOnButtonUp = this.\u003Cbacking_store\u003EOnButtonUp;
    if (backingStoreOnButtonUp == null)
      return;
    backingStoreOnButtonUp(value0, value1);
  }

  public static Buttons ToButton(string btnString)
  {
    FieldInfo[] fields = typeof (Buttons).GetFields();
    int index = 0;
    if (0 < fields.Length)
    {
      FieldInfo fieldInfo;
      do
      {
        fieldInfo = fields[index];
        if (!(fieldInfo.Name == btnString))
          ++index;
        else
          goto label_3;
      }
      while (index < fields.Length);
      goto label_4;
label_3:
      return (Buttons) fieldInfo.GetValue((object) null);
    }
label_4:
    return Buttons.None;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  private bool ModifiersOk(Buttons button)
  {
    if ((button & Buttons.ModifierKeys) != Buttons.None)
    {
      if ((button & Buttons.Ctrl) == Buttons.Ctrl && (Control.ModifierKeys & Keys.Control) != Keys.Control || (button & Buttons.Alt) == Buttons.Alt && (Control.ModifierKeys & Keys.Alt) != Keys.Alt || (button & Buttons.Shift) == Buttons.Shift && (Control.ModifierKeys & Keys.Shift) != Keys.Shift)
        return false;
    }
    else if (Control.ModifierKeys != Keys.None)
      return false;
    return true;
  }

  [return: MarshalAs(UnmanagedType.U1)]
  private bool MouseOk(Buttons button, MouseEventArgs mouse)
  {
    if ((button & Buttons.Mouse) == Buttons.None || mouse == null || (button & Buttons.Double) == Buttons.Double && mouse.Clicks != 2)
      return false;
    MouseButtons button1 = mouse.Button;
    return ((button & Buttons.LButton) != Buttons.LButton || (button1 & MouseButtons.Left) == MouseButtons.Left) && ((button & Buttons.RButton) != Buttons.RButton || (button1 & MouseButtons.Right) == MouseButtons.Right) && ((button & Buttons.MButton) != Buttons.MButton || (button1 & MouseButtons.Middle) == MouseButtons.Middle);
  }

  [return: MarshalAs(UnmanagedType.U1)]
  private bool ButtonOk(Buttons button, KeyEventArgs keys)
  {
    if ((button & Buttons.NormalKeys) == Buttons.None || keys == null)
      return false;
    Buttons buttons = Buttons.None;
    Dictionary<Buttons, Keys>.Enumerator enumerator = ButtonConfiguration.m_Keys.GetEnumerator();
    if (enumerator.MoveNext())
    {
      KeyValuePair<Buttons, Keys> current;
      do
      {
        current = enumerator.Current;
        if ((current.Key & button) == current.Key && keys.KeyCode == current.Value)
          goto label_4;
      }
      while (enumerator.MoveNext());
      goto label_5;
label_4:
      buttons = current.Key;
    }
label_5:
    return buttons == (button & ~Buttons.ModifierKeys);
  }

  private void Control_MouseDown(object sender, MouseEventArgs e)
  {
    List<CfgButton>.Enumerator enumerator = this.m_Buttons.GetEnumerator();
    if (!enumerator.MoveNext())
      return;
    do
    {
      CfgButton current = enumerator.Current;
      if (current.Enabled && !current.Pressed && this.ModifiersOk(current.Button) && this.MouseOk(current.Button, e))
      {
        current.Pressed = true;
        ButtonEventHandler storeOnButtonDown = this.\u003Cbacking_store\u003EOnButtonDown;
        if (storeOnButtonDown != null)
          storeOnButtonDown(current.Name, e);
      }
    }
    while (enumerator.MoveNext());
  }

  private void Control_MouseUp(object sender, MouseEventArgs e)
  {
    List<CfgButton>.Enumerator enumerator = this.m_Buttons.GetEnumerator();
    if (!enumerator.MoveNext())
      return;
    do
    {
      CfgButton current = enumerator.Current;
      if (current.Enabled && current.Pressed && this.MouseOk(current.Button & ~Buttons.Double, e))
      {
        current.Pressed = false;
        ButtonEventHandler backingStoreOnButtonUp = this.\u003Cbacking_store\u003EOnButtonUp;
        if (backingStoreOnButtonUp != null)
          backingStoreOnButtonUp(current.Name, e);
      }
    }
    while (enumerator.MoveNext());
  }

  private void Control_MouseClick(object sender, MouseEventArgs e)
  {
    List<CfgButton>.Enumerator enumerator = this.m_Buttons.GetEnumerator();
    if (!enumerator.MoveNext())
      return;
    do
    {
      CfgButton current = enumerator.Current;
      if (current.Enabled && !current.Pressed && this.ModifiersOk(current.Button) && this.MouseOk(current.Button, e))
      {
        current.Pressed = true;
        ButtonEventHandler storeOnButtonDown = this.\u003Cbacking_store\u003EOnButtonDown;
        if (storeOnButtonDown != null)
          storeOnButtonDown(current.Name, e);
        ButtonEventHandler backingStoreOnButtonUp = this.\u003Cbacking_store\u003EOnButtonUp;
        if (backingStoreOnButtonUp != null)
          backingStoreOnButtonUp(current.Name, e);
        current.Pressed = false;
      }
    }
    while (enumerator.MoveNext());
  }

  private void Control_KeyDown(object sender, KeyEventArgs e)
  {
    List<CfgButton>.Enumerator enumerator = this.m_Buttons.GetEnumerator();
    if (!enumerator.MoveNext())
      return;
    CfgButton current;
    do
    {
      current = enumerator.Current;
      if (current.Enabled && !current.Pressed && this.ModifiersOk(current.Button) && this.ButtonOk(current.Button, e))
        goto label_4;
    }
    while (enumerator.MoveNext());
    return;
label_4:
    current.Pressed = true;
    ButtonEventHandler storeOnButtonDown = this.\u003Cbacking_store\u003EOnButtonDown;
    if (storeOnButtonDown == null)
      return;
    storeOnButtonDown(current.Name, (MouseEventArgs) null);
  }

  private void Control_KeyUp(object sender, KeyEventArgs e)
  {
    List<CfgButton>.Enumerator enumerator = this.m_Buttons.GetEnumerator();
    if (!enumerator.MoveNext())
      return;
    do
    {
      CfgButton current = enumerator.Current;
      if (current.Enabled && current.Pressed && this.ModifiersOk(current.Button) && this.ButtonOk(current.Button, e))
      {
        current.Pressed = false;
        ButtonEventHandler backingStoreOnButtonUp = this.\u003Cbacking_store\u003EOnButtonUp;
        if (backingStoreOnButtonUp != null)
          backingStoreOnButtonUp(current.Name, (MouseEventArgs) null);
      }
    }
    while (enumerator.MoveNext());
  }
}
