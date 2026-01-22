// Decompiled with JetBrains decompiler
// Type: ChocolateBox.BINDumber
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIN;
using System.IO;

#nullable disable
namespace ChocolateBox;

public class BINDumber : Processor
{
  private BINFile myBIN;
  private string myFolder;

  public BINDumber(BINFile bin, string folder)
  {
    this.myBIN = bin;
    this.myFolder = folder;
  }

  public override void Run(Progress progress)
  {
    string str1 = Settings.GetString("BIN", "DumpFormat", "ID-NAME.dat");
    progress.Begin(this.myBIN.EntryCount);
    for (int index = 0; index < this.myBIN.EntryCount; ++index)
    {
      BINEntry binEntry = this.myBIN.get_Entries(index);
      string newValue = "UNTITLED";
      if (binEntry.Name != "")
        newValue = binEntry.Name;
      string str2 = str1.Replace("ID", $"{binEntry.ID:D5}").Replace("NAME", newValue).Replace("DEFINITION", binEntry.Definition);
      progress.Info = str2;
      using (BinaryWriter binaryWriter = new BinaryWriter((Stream) new FileStream($"{this.myFolder}\\{str2}", FileMode.Create)))
      {
        binaryWriter.Write(binEntry.Data, 0, binEntry.Data.Length);
        binaryWriter.Close();
      }
      progress.Update();
    }
    progress.End();
  }
}
