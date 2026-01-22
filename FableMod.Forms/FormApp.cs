// Decompiled with JetBrains decompiler
// Type: FableMod.Forms.FormApp
// Assembly: FableMod.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 910E5594-6600-4712-BB52-1327761AD253
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Forms.dll

using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Forms;

public class FormApp : Form
{
  private IContainer components;
  private static FormApp myStaticMainForm;
  private string myRegistry;
  private string myTitle;

  protected override void Dispose(bool disposing)
  {
    if (this.myRegistry != null)
      this.SaveRegistry();
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.components = (IContainer) new System.ComponentModel.Container();
    this.AutoScaleMode = AutoScaleMode.Font;
    this.Text = nameof (FormApp);
  }

  public FormApp()
  {
    FormApp.myStaticMainForm = this;
    this.InitializeComponent();
    Messages.SetForm((Form) this);
  }

  public static FormApp Instance => FormApp.myStaticMainForm;

  public static string Folder => AppDomain.CurrentDomain.BaseDirectory;

  public static Stream GetEmbeddedFile(Assembly assembly, string fileName)
  {
    try
    {
      return assembly.GetManifestResourceStream($"{assembly.GetName().Name}.{fileName}") ?? throw new InvalidOperationException($"could not locate embedded resource '{fileName}'");
    }
    catch (Exception ex)
    {
      throw new InvalidOperationException(ex.Message);
    }
  }

  public static Bitmap GetEmbeddedBitmap(Assembly assembly, string fileName)
  {
    Stream embeddedFile = FormApp.GetEmbeddedFile(assembly, fileName);
    return embeddedFile == null ? (Bitmap) null : new Bitmap(embeddedFile);
  }

  protected override void OnLoad(EventArgs e)
  {
    if (this.Title == "")
      this.Title = this.Text;
    this.LoadRegistry();
    base.OnLoad(e);
  }

  [Category("Data")]
  [Browsable(true)]
  [Description("Registry key for the application")]
  public string RegistryKey
  {
    get => this.myRegistry;
    set => this.myRegistry = value;
  }

  [Description("Title aka. application name")]
  [Category("Appearance")]
  [Browsable(true)]
  public virtual string Title
  {
    get => this.myTitle;
    set
    {
      this.myTitle = value;
      this.Text = this.myTitle;
    }
  }

  public void SaveRegistry()
  {
    if (this.myRegistry == null || this.myRegistry == "")
      return;
    Microsoft.Win32.RegistryKey subKey = Registry.CurrentUser.CreateSubKey(this.myRegistry);
    if (subKey == null)
      return;
    this.SaveRegistryData(subKey);
    subKey.Close();
  }

  public void LoadRegistry()
  {
    if (this.myRegistry == null || this.myRegistry == "")
      return;
    Microsoft.Win32.RegistryKey Key = Registry.CurrentUser.OpenSubKey(this.myRegistry);
    if (Key == null)
      return;
    this.LoadRegistryData(Key);
    Key.Close();
  }

  protected virtual void SaveRegistryData(Microsoft.Win32.RegistryKey key)
  {
    Microsoft.Win32.RegistryKey subKey1 = key.CreateSubKey("Settings");
    if (subKey1 != null)
    {
      this.SaveRegistrySettings(subKey1);
      subKey1.Close();
    }
    Microsoft.Win32.RegistryKey subKey2 = key.CreateSubKey("Window");
    if (subKey2 == null)
      return;
    this.SaveWindowState(subKey2);
    subKey2.Close();
  }

  protected virtual void SaveRegistrySettings(Microsoft.Win32.RegistryKey key)
  {
  }

  protected virtual void SaveWindowState(Microsoft.Win32.RegistryKey WindowKey)
  {
    WindowKey.SetValue("Top", (object) this.Top);
    WindowKey.SetValue("Left", (object) this.Left);
    WindowKey.SetValue("Width", (object) this.Width);
    WindowKey.SetValue("Height", (object) this.Height);
    WindowKey.SetValue("Maximized", (object) (this.WindowState == FormWindowState.Maximized));
  }

  protected virtual void LoadRegistryData(Microsoft.Win32.RegistryKey Key)
  {
    Microsoft.Win32.RegistryKey Key1 = Key.OpenSubKey("Settings");
    if (Key1 != null)
    {
      this.LoadRegistrySettings(Key1);
      Key1.Close();
    }
    Microsoft.Win32.RegistryKey WindowKey = Key.OpenSubKey("Window");
    if (WindowKey == null)
      return;
    this.LoadWindowState(WindowKey);
    WindowKey.Close();
  }

  protected virtual void LoadRegistrySettings(Microsoft.Win32.RegistryKey Key)
  {
  }

  protected virtual void LoadWindowState(Microsoft.Win32.RegistryKey WindowKey)
  {
    this.StartPosition = FormStartPosition.Manual;
    if (((string) WindowKey.GetValue("Maximized", (object) "False")).Equals("True"))
    {
      this.WindowState = FormWindowState.Maximized;
    }
    else
    {
      this.Top = (int) WindowKey.GetValue("Top", (object) this.Top);
      this.Left = (int) WindowKey.GetValue("Left", (object) this.Left);
      this.Width = (int) WindowKey.GetValue("Width", (object) this.Width);
      this.Height = (int) WindowKey.GetValue("Height", (object) this.Bottom);
    }
  }

  public virtual DialogResult ErrorMessage(string message)
  {
    return MessageBox.Show((IWin32Window) null, message, this.myTitle, MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  public virtual DialogResult InfoMessage(string message)
  {
    return MessageBox.Show((IWin32Window) null, message, this.myTitle, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  public DialogResult Message(string message, MessageBoxButtons buttons, MessageBoxIcon icon)
  {
    return MessageBox.Show((IWin32Window) this, message, this.myTitle, buttons, icon);
  }
}
