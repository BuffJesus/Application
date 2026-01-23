// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormDayTip
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormDayTip : Form
{
  private IContainer components;
  private RichTextBox richTextBoxTip;
  private Button buttonClose;
  private Button buttonNext;
  private PictureBox pictureBox;
  private string[] myTips = new string[5];
  private int myTipIndex;
  public Process myProcess = new Process();

  protected override void Dispose(bool disposing)
  {
    if (disposing)
    {
      if (this.components != null)
        this.components.Dispose();
      if (this.pictureBox.Image != null)
        this.pictureBox.Image.Dispose();
    }
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof (FormDayTip));
    this.richTextBoxTip = new RichTextBox();
    this.buttonClose = new Button();
    this.buttonNext = new Button();
    this.pictureBox = new PictureBox();
    ((ISupportInitialize) this.pictureBox).BeginInit();
    this.SuspendLayout();
    this.richTextBoxTip.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.richTextBoxTip.BackColor = SystemColors.Window;
    this.richTextBoxTip.Cursor = Cursors.Default;
    this.richTextBoxTip.Location = new Point(158, 12);
    this.richTextBoxTip.Name = "richTextBoxTip";
    this.richTextBoxTip.ReadOnly = true;
    this.richTextBoxTip.Size = new Size(315, 260);
    this.richTextBoxTip.TabIndex = 0;
    this.richTextBoxTip.TabStop = false;
    this.richTextBoxTip.Text = "";
    this.richTextBoxTip.WordWrap = true;
    this.richTextBoxTip.ScrollBars = RichTextBoxScrollBars.Vertical;
    this.richTextBoxTip.LinkClicked += new LinkClickedEventHandler(this.richTextBoxTip_LinkClicked);
    this.buttonClose.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonClose.DialogResult = DialogResult.Cancel;
    this.buttonClose.Location = new Point(398, 278);
    this.buttonClose.Name = "buttonClose";
    this.buttonClose.Size = new Size(75, 23);
    this.buttonClose.TabIndex = 1;
    this.buttonClose.Text = "Close";
    this.buttonClose.UseVisualStyleBackColor = true;
    this.buttonClose.Click += new EventHandler(this.buttonClose_Click);
    this.buttonNext.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
    this.buttonNext.Location = new Point(317, 278);
    this.buttonNext.Name = "buttonNext";
    this.buttonNext.Size = new Size(75, 23);
    this.buttonNext.TabIndex = 2;
    this.buttonNext.Text = "Next Tip";
    this.buttonNext.UseVisualStyleBackColor = true;
    this.buttonNext.Click += new EventHandler(this.buttonNext_Click);
    this.pictureBox.BorderStyle = BorderStyle.Fixed3D;
    this.pictureBox.Image = null; // (Image) componentResourceManager.GetObject("pictureBox.Image");
    this.pictureBox.InitialImage = (Image) null;
    this.pictureBox.Location = new Point(12, 12);
    this.pictureBox.Name = "pictureBox";
    this.pictureBox.Size = new Size(140, 260);
    this.pictureBox.TabIndex = 3;
    this.pictureBox.TabStop = false;
    this.CancelButton = (IButtonControl) this.buttonClose;
    this.ClientSize = new Size(485, 313);
    this.Controls.Add((Control) this.pictureBox);
    this.Controls.Add((Control) this.buttonNext);
    this.Controls.Add((Control) this.buttonClose);
    this.Controls.Add((Control) this.richTextBoxTip);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FormDayTip);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Tip of the Day";
    ((ISupportInitialize) this.pictureBox).EndInit();
    this.ResumeLayout(false);
  }

  public FormDayTip()
  {
    this.InitializeComponent();
    ThemeManager.ApplyTheme(this);
    this.NextTip();
  }

  private void ParseLine(RichTextBox rtb, string line)
  {
    string[] strArray = new Regex("<([a-z]+|/[a-z]+)>").Split(line);
    Color foreColor = ThemeManager.ForegroundColor;
    Font font = rtb.Font;
    foreach (string str in strArray)
    {
      switch (str)
      {
        case "":
          continue;
        case "title":
          font = new Font(rtb.Font, FontStyle.Bold);
          continue;
        case "/title":
          foreColor = ThemeManager.ForegroundColor;
          font = rtb.Font;
          continue;
        default:
          rtb.SelectionFont = font;
          rtb.SelectionColor = foreColor;
          rtb.SelectedText = str;
          continue;
      }
    }
    rtb.SelectedText = "\n";
  }

  private bool IsNewTip(string tip)
  {
    for (int index = 0; index < 5; ++index)
    {
      if (this.myTips[index] == tip)
        return false;
    }
    return true;
  }

  private void NextTip()
  {
    string tipsPath = Settings.DataDirectory + "tips";
    if (!Directory.Exists(tipsPath))
    {
      this.richTextBoxTip.Text = "Tips directory not found: " + tipsPath;
      return;
    }
    string[] files = Directory.GetFiles(tipsPath, "*.txt");
    this.richTextBoxTip.Clear();
    if (files.Length == 0)
    {
      this.richTextBoxTip.Text = "No tips available";
    }
    else
    {
      Random random = new Random(DateTime.Now.Millisecond);
      int num = 0;
      int length = files.Length;
      string str;
      for (str = files[random.Next(length)]; !this.IsNewTip(str) && num < 64; ++num)
        str = files[random.Next(length)];
      this.myTips[this.myTipIndex] = str;
      this.myTipIndex = (this.myTipIndex + 1) % 5;
      using (TextReader textReader = new StreamReader(str))
      {
        string line;
        while ((line = textReader.ReadLine()) != null)
          this.ParseLine(this.richTextBoxTip, line);
      }

            // Load a random image from data\loading
            string loadingPath = Settings.DataDirectory + "loading";
            if (Directory.Exists(loadingPath))
            {
                string[] images = Directory.GetFiles(loadingPath, "*.png");
                if (images.Length > 0)
                {
                    try
                    {
                        if (this.pictureBox.Image != null)
                            this.pictureBox.Image.Dispose();

                        Image img = Image.FromFile(images[random.Next(images.Length)]);
                        this.pictureBox.Image = img;

                        // Calculate new dimensions to maintain aspect ratio
                        int targetMaxHeight = 260;
                        int targetMaxWidth = 400;

                        float aspectRatio = (float)img.Width / img.Height;

                        int newWidth = (int)(targetMaxHeight * aspectRatio);
                        int newHeight = targetMaxHeight;

                        if (newWidth > targetMaxWidth)
                        {
                            // If it's too wide, cap width and shrink height
                            newWidth = targetMaxWidth;
                            newHeight = (int)(newWidth / aspectRatio);
                        }

                        if (newWidth < 100)
                        {
                            newWidth = 100;
                            newHeight = (int)(newWidth / aspectRatio);
                        }

                        // Center the picture box vertically within the 260px area if height was shrunk
                        int verticalOffset = (targetMaxHeight - newHeight) / 2;

                        int oldWidth = this.pictureBox.Width;
                        this.pictureBox.Width = newWidth;
                        this.pictureBox.Height = newHeight;
                        this.pictureBox.Top = 12 + verticalOffset;
                        this.pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;

                        int widthDiff = newWidth - oldWidth;
                        this.Width += widthDiff;

                        int rightMargin = 12;
                        int gap = 6;
                        this.richTextBoxTip.Left = this.pictureBox.Right + gap;
                        int tipWidth = this.ClientSize.Width - this.richTextBoxTip.Left - rightMargin;
                        if (tipWidth < 200)
                        {
                            this.ClientSize = new Size(this.richTextBoxTip.Left + rightMargin + 200, this.ClientSize.Height);
                            tipWidth = 200;
                        }
                        this.richTextBoxTip.Width = tipWidth;
                    }
                    catch { }
                }
            }
    }
  }

  private void buttonNext_Click(object sender, EventArgs e) => this.NextTip();

  private void buttonClose_Click(object sender, EventArgs e) => this.Close();

  private void richTextBoxTip_LinkClicked(object sender, LinkClickedEventArgs e)
  {
    this.myProcess = Process.Start("IExplore.exe", e.LinkText);
  }
}
