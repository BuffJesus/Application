// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormTextureBIG
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIG;
using FableMod.Content.Forms;
using FableMod.Forms;
using FableMod.Gfx.Integration;
using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormTextureBIG : FormBIG
{
  private IContainer components;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.SuspendLayout();
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.ClientSize = new Size(514, 394);
    this.Name = nameof (FormTextureBIG);
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  public FormTextureBIG() => this.InitializeComponent();

  public override void Build(BIGFile big, Progress progress)
  {
    base.Build(big, progress);
    this.addEntryToolStripMenuItem.Text = "Add Texture";
  }

  protected override AssetEntry CreateEntry(BIGBank bank)
  {
    FormOpenTexture formOpenTexture = new FormOpenTexture();
    AssetEntry entry = (AssetEntry) null;
    if (formOpenTexture.ShowDialog((IWin32Window) this) == DialogResult.OK)
    {
      GfxTexture gfxTexture;
      try
      {
        gfxTexture = new GfxTexture(formOpenTexture.FileName, formOpenTexture.Format);
      }
      catch (Exception ex)
      {
        int num = (int) FormMain.Instance.ErrorMessage(ex.Message);
        return (AssetEntry) null;
      }
      string withoutExtension = Path.GetFileNameWithoutExtension(formOpenTexture.FileName);
      withoutExtension.Replace(' ', '_');
      withoutExtension.Replace('-', '_');
      withoutExtension.Replace('.', '_');
      string upper = withoutExtension.ToUpper();
      FormTextBox formTextBox = new FormTextBox();
      formTextBox.Text = "New Texture";
      formTextBox.labelInput.Text = "Texture Symbol Name:";
      while (true)
      {
        do
        {
          formTextBox.textBoxInput.Text = upper;
        }
        while (formTextBox.ShowDialog() != DialogResult.OK);
        if (bank.FindEntryBySymbolName(formTextBox.textBoxInput.Text) != null)
        {
          int num = (int) FormMain.Instance.ErrorMessage("Symbol already exists. Try again.");
        }
        else
          break;
      }
      string text = formTextBox.textBoxInput.Text;
      formTextBox.Dispose();
      entry = new AssetEntry(text, bank.GetNewID(), 0U, (AssetBank) bank);
      gfxTexture.ApplyToEntry(entry);
    }
    formOpenTexture.Dispose();
    return entry;
  }
}
