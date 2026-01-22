// Decompiled with JetBrains decompiler
// Type: FableMod.Forms.Messages
// Assembly: FableMod.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 910E5594-6600-4712-BB52-1327761AD253
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Forms.dll

using System;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Forms;

public class Messages
{
  public static Form myForm;

  public static void SetForm(Form form) => Messages.myForm = form;

  public static void Info(string message)
  {
    int num = (int) MessageBox.Show((IWin32Window) null, message, Messages.myForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
  }

  public static void Error(string message)
  {
    int num = (int) MessageBox.Show((IWin32Window) null, message, Messages.myForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  public static void Error(Exception ex)
  {
    int num = (int) MessageBox.Show((IWin32Window) null, ex.Message, Messages.myForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Hand);
  }

  public static void Warning(string message)
  {
    int num = (int) MessageBox.Show((IWin32Window) null, message, Messages.myForm.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
  }
}
