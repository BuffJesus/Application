// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormProcess
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormProcess : Form
{
  private Progress myProgress;
  private Thread myThread;
  private List<Processor> myProcessors = new List<Processor>();
  private int myCurrentBitmap;
  private static List<Bitmap> myBitmaps = new List<Bitmap>();
  private IContainer components;
  private ProgressBar progressBar;
  private Label labelInfo;
  private Label labelStep;
  private System.Windows.Forms.Timer timerUpdate;
  private PictureBox pictureBoxImage;
  private System.Windows.Forms.Timer timerPicture;

  static FormProcess()
  {
    for (int index = 1; index <= 9; ++index)
    {
      try
      {
        Bitmap bitmap = new Bitmap(Settings.Directory + $"data\\loading\\loading0{index}.png");
        FormProcess.myBitmaps.Add(bitmap);
      }
      catch (Exception ex)
      {
      }
    }
  }

  public FormProcess(Processor processor)
  {
    this.InitializeComponent();
    ThemeManager.ApplyTheme(this);
    this.myProgress = new Progress();
    this.myProcessors.Add(processor);
    this.Text = FormMain.Instance.Title;
  }

  public FormProcess(List<Processor> processors)
  {
    this.InitializeComponent();
    ThemeManager.ApplyTheme(this);
    this.myProgress = new Progress();
    this.myProcessors.AddRange((IEnumerable<Processor>) processors);
    this.Text = FormMain.Instance.Title;
  }

  private void CloseForm()
  {
    if (this.InvokeRequired)
      this.Invoke((Delegate) new FormProcess.CloseDelegate(this.CloseForm));
    else
      this.Close();
  }

  private void ThreadFunc()
  {
    try
    {
      this.myProgress.Begin(this.myProcessors.Count);
      for (int index = 0; index < this.myProcessors.Count; ++index)
        this.myProcessors[index].Run(this.myProgress);
      this.myProgress.End();
    }
    catch (ThreadAbortException ex)
    {
    }
    this.CloseForm();
  }

  private void FormProcess_Shown(object sender, EventArgs e)
  {
    this.myThread = new Thread(new ThreadStart(this.ThreadFunc));
    this.myThread.Start();
    this.timerUpdate.Enabled = true;
  }

  private void FormProcess_FormClosing(object sender, FormClosingEventArgs e)
  {
    this.timerPicture.Enabled = false;
    this.timerUpdate.Enabled = false;
    if (this.myThread == null)
      return;
    this.myThread.Abort();
  }

  private void timerUpdate_Tick(object sender, EventArgs e)
  {
    if (this.labelInfo.Text != this.myProgress.Info)
        this.labelInfo.Text = this.myProgress.Info;

    if (this.labelStep.Text != this.myProgress.StepInfo)
        this.labelStep.Text = this.myProgress.StepInfo;
    
    int num = (int) ((double) this.progressBar.Maximum * (double) this.myProgress.Value);
    if (num > this.progressBar.Maximum)
      num = this.progressBar.Maximum;
    
    if (this.progressBar.Value != num)
        this.progressBar.Value = num;
  }

  private void FormProcess_Load(object sender, EventArgs e)
  {
    if (FormProcess.myBitmaps.Count > 0)
    {
        this.pictureBoxImage.Image = (Image) FormProcess.myBitmaps[this.myCurrentBitmap];
    }
  }

  private void timerPicture_Tick(object sender, EventArgs e)
  {
    if (FormProcess.myBitmaps.Count > 0)
    {
        ++this.myCurrentBitmap;
        if (this.myCurrentBitmap >= FormProcess.myBitmaps.Count)
          this.myCurrentBitmap = 0;
        this.pictureBoxImage.Image = (Image) FormProcess.myBitmaps[this.myCurrentBitmap];
        this.pictureBoxImage.Update();
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
    this.components = (IContainer) new System.ComponentModel.Container();
    this.progressBar = new ProgressBar();
    this.labelInfo = new Label();
    this.labelStep = new Label();
    this.timerUpdate = new System.Windows.Forms.Timer(this.components);
    this.pictureBoxImage = new PictureBox();
    this.timerPicture = new System.Windows.Forms.Timer(this.components);
    ((ISupportInitialize) this.pictureBoxImage).BeginInit();
    this.SuspendLayout();
    this.progressBar.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.progressBar.Location = new Point(12, 160);
    this.progressBar.Maximum = 1000;
    this.progressBar.Name = "progressBar";
    this.progressBar.Size = new Size(500, 25);
    this.progressBar.Step = 1;
    this.progressBar.Style = ProgressBarStyle.Continuous;
    this.progressBar.TabIndex = 3;
    this.labelInfo.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.labelInfo.Location = new Point(12, 115);
    this.labelInfo.Name = "labelInfo";
    this.labelInfo.Size = new Size(500, 20);
    this.labelInfo.TabIndex = 2;
    this.labelInfo.Text = "Please wait...";
    this.labelStep.Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.labelStep.Location = new Point(12, 138);
    this.labelStep.Name = "labelStep";
    this.labelStep.Size = new Size(500, 20);
    this.labelStep.TabIndex = 5;
    this.labelStep.Text = "";
    this.timerUpdate.Interval = 50;
    this.timerUpdate.Tick += new EventHandler(this.timerUpdate_Tick);
    this.pictureBoxImage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
    this.pictureBoxImage.BorderStyle = BorderStyle.None;
    this.pictureBoxImage.Location = new Point(12, 12);
    this.pictureBoxImage.Name = "pictureBoxImage";
    this.pictureBoxImage.Size = new Size(500, 95);
    this.pictureBoxImage.TabIndex = 4;
    this.pictureBoxImage.TabStop = false;
    this.timerPicture.Enabled = true;
    this.timerPicture.Interval = 2500;
    this.timerPicture.Tick += new EventHandler(this.timerPicture_Tick);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(524, 200);
    this.ControlBox = false;
    this.Controls.Add((Control) this.labelInfo);
    this.Controls.Add((Control) this.labelStep);
    this.Controls.Add((Control) this.pictureBoxImage);
    this.Controls.Add((Control) this.progressBar);
    this.FormBorderStyle = FormBorderStyle.FixedDialog;
    this.MaximizeBox = false;
    this.MinimizeBox = false;
    this.Name = nameof (FormProcess);
    this.ShowInTaskbar = false;
    this.SizeGripStyle = SizeGripStyle.Hide;
    this.StartPosition = FormStartPosition.CenterScreen;
    this.Text = "Processing...";
    this.Load += new EventHandler(this.FormProcess_Load);
    this.Shown += new EventHandler(this.FormProcess_Shown);
    this.FormClosing += new FormClosingEventHandler(this.FormProcess_FormClosing);
    ((ISupportInitialize) this.pictureBoxImage).EndInit();
    this.ResumeLayout(false);
  }

  private delegate void CloseDelegate();
}
