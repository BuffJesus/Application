// Decompiled with JetBrains decompiler
// Type: FableMod.Content.Forms.ControlText
// Assembly: FableMod.Content.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A575F982-23D4-4DAE-A571-3D769462C59B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Content.Forms.dll

using FableMod.BIG;
using FableMod.ContentManagement;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Content.Forms;

public class ControlText : ControlBIGEntry
{
  private TextBox txtContent;
  private IContainer components;
  private TextBox txtIdentifier;
  private Label lblIdentifier;
  private TextBox txtSoundFile;
  private Label lblSoundFile;
  private TextBox txtSpeaker;
  private Label lblSpeaker;
  private TextBox txtModifiers;
  private Label lblModifiers;
  private Label lblContent;
  protected BIGText m_Text;

  public ControlText() => this.InitializeComponent();

  public override void ApplyChanges()
  {
    if (this.m_Text == null)
      return;
    this.m_Text.Identifier = this.txtIdentifier.Text;
    this.m_Text.Content = this.txtContent.Text;
    this.m_Text.Modifiers = this.txtModifiers.Lines;
    this.m_Text.SoundFile = this.txtSoundFile.Text;
    this.m_Text.Speaker = this.txtSpeaker.Text;
    this.m_Text.ApplyToEntry(this.m_Entry);
  }

  public override AssetEntry BIGEntry
  {
    get => base.BIGEntry;
    set
    {
      base.BIGEntry = value;
      this.m_Text = new BIGText(this.m_Entry);
      this.txtIdentifier.Text = this.m_Text.Identifier;
      this.txtModifiers.Lines = this.m_Text.Modifiers;
      this.txtSoundFile.Text = this.m_Text.SoundFile;
      this.txtSpeaker.Text = this.m_Text.Speaker;
      this.txtContent.Text = this.m_Text.Content;
    }
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.txtIdentifier = new TextBox();
    this.lblIdentifier = new Label();
    this.txtContent = new TextBox();
    this.lblContent = new Label();
    this.txtSoundFile = new TextBox();
    this.lblSoundFile = new Label();
    this.txtModifiers = new TextBox();
    this.lblModifiers = new Label();
    this.txtSpeaker = new TextBox();
    this.lblSpeaker = new Label();
    this.SuspendLayout();
    this.txtIdentifier.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtIdentifier.Location = new Point(74, 28);
    this.txtIdentifier.Name = "txtIdentifier";
    this.txtIdentifier.Size = new Size(379, 20);
    this.txtIdentifier.TabIndex = 2;
    this.lblIdentifier.AutoSize = true;
    this.lblIdentifier.Location = new Point(8, 31 /*0x1F*/);
    this.lblIdentifier.Name = "lblIdentifier";
    this.lblIdentifier.Size = new Size(50, 13);
    this.lblIdentifier.TabIndex = 3;
    this.lblIdentifier.Text = "Identifier:";
    this.txtContent.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.txtContent.Location = new Point(74, 124);
    this.txtContent.Multiline = true;
    this.txtContent.Name = "txtContent";
    this.txtContent.ScrollBars = ScrollBars.Both;
    this.txtContent.Size = new Size(379, 93);
    this.txtContent.TabIndex = 4;
    this.lblContent.AutoSize = true;
    this.lblContent.Location = new Point(8, (int) sbyte.MaxValue);
    this.lblContent.Name = "lblContent";
    this.lblContent.Size = new Size(47, 13);
    this.lblContent.TabIndex = 14;
    this.lblContent.Text = "Content:";
    this.txtSoundFile.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtSoundFile.Location = new Point(74, 52);
    this.txtSoundFile.Name = "txtSoundFile";
    this.txtSoundFile.Size = new Size(379, 20);
    this.txtSoundFile.TabIndex = 6;
    this.lblSoundFile.AutoSize = true;
    this.lblSoundFile.Location = new Point(8, 56);
    this.lblSoundFile.Name = "lblSoundFile";
    this.lblSoundFile.Size = new Size(60, 13);
    this.lblSoundFile.TabIndex = 7;
    this.lblSoundFile.Text = "Sound File:";
    this.txtModifiers.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtModifiers.Location = new Point(74, 100);
    this.txtModifiers.Name = "txtModifiers";
    this.txtModifiers.Size = new Size(379, 20);
    this.txtModifiers.TabIndex = 12;
    this.lblModifiers.AutoSize = true;
    this.lblModifiers.Location = new Point(8, 103);
    this.lblModifiers.Name = "lblModifiers";
    this.lblModifiers.Size = new Size(52, 13);
    this.lblModifiers.TabIndex = 13;
    this.lblModifiers.Text = "Modifiers:";
    this.txtSpeaker.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.txtSpeaker.Location = new Point(74, 76);
    this.txtSpeaker.Name = "txtSpeaker";
    this.txtSpeaker.Size = new Size(379, 20);
    this.txtSpeaker.TabIndex = 10;
    this.lblSpeaker.AutoSize = true;
    this.lblSpeaker.Location = new Point(8, 79);
    this.lblSpeaker.Name = "lblSpeaker";
    this.lblSpeaker.Size = new Size(50, 13);
    this.lblSpeaker.TabIndex = 11;
    this.lblSpeaker.Text = "Speaker:";
    this.Controls.Add((System.Windows.Forms.Control) this.txtSpeaker);
    this.Controls.Add((System.Windows.Forms.Control) this.lblSpeaker);
    this.Controls.Add((System.Windows.Forms.Control) this.txtModifiers);
    this.Controls.Add((System.Windows.Forms.Control) this.lblModifiers);
    this.Controls.Add((System.Windows.Forms.Control) this.txtSoundFile);
    this.Controls.Add((System.Windows.Forms.Control) this.lblSoundFile);
    this.Controls.Add((System.Windows.Forms.Control) this.txtContent);
    this.Controls.Add((System.Windows.Forms.Control) this.lblContent);
    this.Controls.Add((System.Windows.Forms.Control) this.txtIdentifier);
    this.Controls.Add((System.Windows.Forms.Control) this.lblIdentifier);
    this.EntryType = "Text";
    this.Name = nameof (ControlText);
    this.Size = new Size(465, 229);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblIdentifier, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.txtIdentifier, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblContent, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.txtContent, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblSoundFile, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.txtSoundFile, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblModifiers, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.txtModifiers, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.lblSpeaker, 0);
    this.Controls.SetChildIndex((System.Windows.Forms.Control) this.txtSpeaker, 0);
    this.ResumeLayout(false);
    this.PerformLayout();
  }
}
