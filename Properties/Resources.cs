// Decompiled with JetBrains decompiler
// Type: ChocolateBox.Properties.Resources
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using System.CodeDom.Compiler;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;

#nullable disable
namespace ChocolateBox.Properties;

[GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
[DebuggerNonUserCode]
[CompilerGenerated]
internal class Resources
{
  private static ResourceManager resourceMan;
  private static CultureInfo resourceCulture;

  internal Resources()
  {
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static ResourceManager ResourceManager
  {
    get
    {
      if (object.ReferenceEquals((object) ChocolateBox.Properties.Resources.resourceMan, (object) null))
        ChocolateBox.Properties.Resources.resourceMan = new ResourceManager("ChocolateBox.Properties.Resources", typeof (ChocolateBox.Properties.Resources).Assembly);
      return ChocolateBox.Properties.Resources.resourceMan;
    }
  }

  [EditorBrowsable(EditorBrowsableState.Advanced)]
  internal static CultureInfo Culture
  {
    get => ChocolateBox.Properties.Resources.resourceCulture;
    set => ChocolateBox.Properties.Resources.resourceCulture = value;
  }

  internal static Icon ChocolateBox
  {
    get
    {
      return (Icon) ChocolateBox.Properties.Resources.ResourceManager.GetObject(nameof (ChocolateBox), ChocolateBox.Properties.Resources.resourceCulture);
    }
  }
}
