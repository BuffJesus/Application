// Decompiled with JetBrains decompiler
// Type: ChocolateBox.ModEnvironmentManager
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BBB;
using FableMod.CLRCore;
using System;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

internal class ModEnvironmentManager
{
  private void ErrorMessage(string message)
  {
    int num = (int) FormMain.Instance.ErrorMessage(message);
  }

  private void InfoMessage(string message)
  {
    int num = (int) FormMain.Instance.InfoMessage(message);
  }

  private bool CheckUserStIni()
  {
    string path = Settings.FableDirectory + "userst.ini";
    try
    {
      StreamReader streamReader = new StreamReader(path);
      string str;
      while ((str = streamReader.ReadLine()) != null)
      {
        if (str.ToUpper() == "USELEVELWAD FALSE;")
          return true;
      }
      streamReader.Close();
    }
    catch (Exception)
    {
    }
    return false;
  }

  private bool FixUserStIni()
  {
    string fableDirectory = Settings.FableDirectory;
    string str1 = fableDirectory + "backup_userst.ini";
    string str2 = fableDirectory + "userst.ini";
    try
    {
      if (!File.Exists(str1))
        File.Copy(str2, str1, true);
      FileStream fileStream = new FileStream(str2, FileMode.Create);
      StreamReader streamReader = new StreamReader(str1);
      StreamWriter streamWriter = new StreamWriter((Stream) fileStream);
      string str3;
      while ((str3 = streamReader.ReadLine()) != null)
      {
        if (str3 != "UseLevelWAD TRUE;")
          streamWriter.WriteLine(str3);
        else
          streamWriter.WriteLine("UseLevelWAD FALSE;");
      }
      streamReader.Close();
      streamWriter.Close();
      fileStream.Close();
      return true;
    }
    catch (Exception ex)
    {
      this.ErrorMessage("userst.ini Error: " + ex.Message);
    }
    return false;
  }

  private bool CheckTngLev()
  {
    string str = Settings.FableDirectory + "data\\Levels\\";
    return File.Exists(str + "creature_hub.tng") && File.Exists(str + "creature_hub.tng") && File.Exists(str + "creature_hub.tng") && File.Exists(str + "FinalAlbion\\lookoutpoint.tng") && File.Exists(str + "FinalAlbion\\lookoutpoint.lev");
  }

  private bool Fix()
  {
    string fableDirectory = Settings.FableDirectory;
    string str1 = fableDirectory + "data\\Levels\\";
    try
    {
      string str2 = "";
      if (!this.CheckTngLev())
      {
        BBBFile bbb = new BBBFile();
        bbb.Open(str1 + "FinalAlbion.wad", (ProgressInterface) null);
        FormProcess formProcess = new FormProcess((Processor) new BBBExtractor(bbb, fableDirectory));
        formProcess.Text = "Extracting...";
        int num = (int) formProcess.ShowDialog();
        formProcess.Dispose();
        bbb.Close();
        str2 += "\tExtracted FinalAlbion.WAD\r\n";
      }
      if (File.Exists(str1 + "FinalAlbion.wad"))
      {
        File.Delete(str1 + "_FinalAlbion.wad");
        File.Move(str1 + "FinalAlbion.wad", str1 + "_FinalAlbion.wad");
        str2 += "\tRenamed FinalAlbion.WAD\r\n";
      }
      if (!this.CheckUserStIni())
      {
        if (!this.FixUserStIni())
          throw new Exception("Failed to update userst.ini");
        str2 += "\tPatched userst.ini\r\n";
      }
      this.InfoMessage("Modding environment successfully updated!\r\nFollowing tasks were completed:\r\n" + str2);
      return true;
    }
    catch (Exception ex)
    {
      this.ErrorMessage(ex.ToString());
    }
    return false;
  }

  public bool Check()
  {
    string fableDirectory = Settings.FableDirectory;
    string str1 = fableDirectory + "data\\Levels\\";
    try
    {
      string str2 = "";
      if (File.Exists(fableDirectory + "userst.ini"))
      {
        if (!this.CheckUserStIni())
          str2 = "\tInvalid userst.ini\r\n";
      }
      else
        str2 = "\tFile userst.ini not found\r\n";
      if (File.Exists(str1 + "FinalAlbion.wad"))
        str2 += "\tFinalAlbion.WAD exists\r\n";
      if (!this.CheckTngLev())
        str2 += "\tUnable to locate TNG and LEV files\r\n";
      if (!(str2 != ""))
        return true;
      return MessageBox.Show($"Your modding environment has the following problems:\r\n{str2}\r\nDo you want ChocolateBox to fix the issues?", FormMain.Instance.Title, MessageBoxButtons.YesNo) == DialogResult.Yes && this.Fix();
    }
    catch (Exception ex)
    {
      this.ErrorMessage(ex.ToString());
    }
    return false;
  }
}
