// Decompiled with JetBrains decompiler
// Type: FableMod.Content.Forms.FormDataViewer
// Assembly: FableMod.Content.Forms, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: A575F982-23D4-4DAE-A571-3D769462C59B
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\FableMod.Content.Forms.dll

using FableMod.ContentManagement;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

#nullable disable
namespace FableMod.Content.Forms;

public class FormDataViewer : Form
{
  private Panel pnlTop;
  private Splitter spltTop;
  private Splitter spltMiddle;
  private TextBox txtCharData;
  private TextBox txtHexData;
  private NumericUpDown nudLineLength;
  private Label lblLineLength;
  private byte[] m_Data;
  private SortedList<uint, string> m_CRCList;
  private Splitter spltDataDisplay;
  private Panel pnlDataDisplay;
  private Label lblByte;
  private Label lblUInt;
  private Label lblShort;
  private Label lblUShort;
  private Label lblChar;
  private Label lblInt;
  private Label lblString;
  private Label lblDouble;
  private Label lblFloat;
  private Label lblResults;
  private Button btnRunSearch;
  private TextBox txtSearchID;
  private ListView lvResults;
  private Splitter spltDataResults;
  private Panel pnlDataValueDisplay;
  private Button btnLoadCRCList;
  private System.ComponentModel.Container components;

  public FormDataViewer(byte[] data)
  {
    this.InitializeComponent();
    this.m_Data = data;
    this.RefreshDisplay();
  }

  public void RefreshDisplay()
  {
    int length = 1 + this.m_Data.Length / (int) this.nudLineLength.Value;
    string[] strArray1 = new string[length];
    string[] strArray2 = new string[length];
    string str1 = "";
    string str2 = "";
    int index1 = 0;
    for (int index2 = 0; index2 < this.m_Data.Length; ++index2)
    {
      if (this.m_Data[index2] < (byte) 16 /*0x10*/)
        str1 += "0";
      str1 = $"{str1}{this.m_Data[index2].ToString("X")} ";
      str2 = this.m_Data[index2] >= (byte) 32 /*0x20*/ ? $"{str2}{new string((char) this.m_Data[index2], 1)} " : str2 + ". ";
      if ((index2 + 1) % (int) this.nudLineLength.Value == 0)
      {
        strArray1[index1] = str1;
        strArray2[index1] = str2;
        str1 = "";
        str2 = "";
        ++index1;
      }
    }
    strArray1[index1] = str1;
    strArray2[index1] = str2;
    this.txtHexData.Lines = strArray1;
    this.txtCharData.Lines = strArray2;
  }

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.pnlTop = new Panel();
    this.lblLineLength = new Label();
    this.nudLineLength = new NumericUpDown();
    this.spltTop = new Splitter();
    this.spltMiddle = new Splitter();
    this.txtCharData = new TextBox();
    this.txtHexData = new TextBox();
    this.spltDataDisplay = new Splitter();
    this.pnlDataDisplay = new Panel();
    this.lvResults = new ListView();
    this.spltDataResults = new Splitter();
    this.pnlDataValueDisplay = new Panel();
    this.btnLoadCRCList = new Button();
    this.txtSearchID = new TextBox();
    this.lblByte = new Label();
    this.lblChar = new Label();
    this.btnRunSearch = new Button();
    this.lblUShort = new Label();
    this.lblShort = new Label();
    this.lblResults = new Label();
    this.lblUInt = new Label();
    this.lblString = new Label();
    this.lblInt = new Label();
    this.lblDouble = new Label();
    this.lblFloat = new Label();
    this.pnlTop.SuspendLayout();
    this.nudLineLength.BeginInit();
    this.pnlDataDisplay.SuspendLayout();
    this.pnlDataValueDisplay.SuspendLayout();
    this.SuspendLayout();
    this.pnlTop.Controls.Add((System.Windows.Forms.Control) this.lblLineLength);
    this.pnlTop.Controls.Add((System.Windows.Forms.Control) this.nudLineLength);
    this.pnlTop.Dock = DockStyle.Top;
    this.pnlTop.Location = new Point(4, 4);
    this.pnlTop.Name = "pnlTop";
    this.pnlTop.Size = new Size(702, 56);
    this.pnlTop.TabIndex = 0;
    this.lblLineLength.Location = new Point(8, 32 /*0x20*/);
    this.lblLineLength.Name = "lblLineLength";
    this.lblLineLength.Size = new Size(80 /*0x50*/, 16 /*0x10*/);
    this.lblLineLength.TabIndex = 1;
    this.lblLineLength.Text = "Byte Per Line:";
    this.nudLineLength.Location = new Point(88, 32 /*0x20*/);
    this.nudLineLength.Maximum = new Decimal(new int[4]
    {
      1024 /*0x0400*/,
      0,
      0,
      0
    });
    this.nudLineLength.Minimum = new Decimal(new int[4]
    {
      1,
      0,
      0,
      0
    });
    this.nudLineLength.Name = "nudLineLength";
    this.nudLineLength.Size = new Size(48 /*0x30*/, 20);
    this.nudLineLength.TabIndex = 0;
    this.nudLineLength.Value = new Decimal(new int[4]
    {
      16 /*0x10*/,
      0,
      0,
      0
    });
    this.nudLineLength.ValueChanged += new EventHandler(this.nudLineLength_ValueChanged);
    this.spltTop.Dock = DockStyle.Top;
    this.spltTop.Enabled = false;
    this.spltTop.Location = new Point(4, 60);
    this.spltTop.Name = "spltTop";
    this.spltTop.Size = new Size(702, 3);
    this.spltTop.TabIndex = 2;
    this.spltTop.TabStop = false;
    this.spltMiddle.Dock = DockStyle.Right;
    this.spltMiddle.Location = new Point(416, 63 /*0x3F*/);
    this.spltMiddle.Name = "spltMiddle";
    this.spltMiddle.Size = new Size(3, 240 /*0xF0*/);
    this.spltMiddle.TabIndex = 3;
    this.spltMiddle.TabStop = false;
    this.txtCharData.Dock = DockStyle.Right;
    this.txtCharData.Font = new Font("Courier New", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.txtCharData.HideSelection = false;
    this.txtCharData.Location = new Point(419, 63 /*0x3F*/);
    this.txtCharData.MaxLength = 4194304 /*0x400000*/;
    this.txtCharData.Multiline = true;
    this.txtCharData.Name = "txtCharData";
    this.txtCharData.ReadOnly = true;
    this.txtCharData.ScrollBars = ScrollBars.Both;
    this.txtCharData.Size = new Size(287, 240 /*0xF0*/);
    this.txtCharData.TabIndex = 4;
    this.txtCharData.WordWrap = false;
    this.txtHexData.Dock = DockStyle.Fill;
    this.txtHexData.Font = new Font("Courier New", 9.75f, FontStyle.Regular, GraphicsUnit.Point, (byte) 0);
    this.txtHexData.HideSelection = false;
    this.txtHexData.Location = new Point(4, 63 /*0x3F*/);
    this.txtHexData.MaxLength = 4194304 /*0x400000*/;
    this.txtHexData.Multiline = true;
    this.txtHexData.Name = "txtHexData";
    this.txtHexData.ReadOnly = true;
    this.txtHexData.ScrollBars = ScrollBars.Both;
    this.txtHexData.Size = new Size(412, 240 /*0xF0*/);
    this.txtHexData.TabIndex = 1;
    this.txtHexData.WordWrap = false;
    this.txtHexData.MouseClick += new MouseEventHandler(this.txtHexData_MouseClick);
    this.spltDataDisplay.Dock = DockStyle.Bottom;
    this.spltDataDisplay.Location = new Point(4, 303);
    this.spltDataDisplay.Name = "spltDataDisplay";
    this.spltDataDisplay.Size = new Size(702, 4);
    this.spltDataDisplay.TabIndex = 5;
    this.spltDataDisplay.TabStop = false;
    this.pnlDataDisplay.Controls.Add((System.Windows.Forms.Control) this.lvResults);
    this.pnlDataDisplay.Controls.Add((System.Windows.Forms.Control) this.spltDataResults);
    this.pnlDataDisplay.Controls.Add((System.Windows.Forms.Control) this.pnlDataValueDisplay);
    this.pnlDataDisplay.Dock = DockStyle.Bottom;
    this.pnlDataDisplay.Location = new Point(4, 307);
    this.pnlDataDisplay.Name = "pnlDataDisplay";
    this.pnlDataDisplay.Size = new Size(702, 144 /*0x90*/);
    this.pnlDataDisplay.TabIndex = 6;
    this.lvResults.Dock = DockStyle.Fill;
    this.lvResults.Location = new Point(239, 0);
    this.lvResults.Name = "lvResults";
    this.lvResults.Size = new Size(463, 144 /*0x90*/);
    this.lvResults.TabIndex = 12;
    this.lvResults.UseCompatibleStateImageBehavior = false;
    this.lvResults.View = View.List;
    this.lvResults.MouseDown += new MouseEventHandler(this.lvResults_MouseDown);
    this.spltDataResults.Location = new Point(236, 0);
    this.spltDataResults.Name = "spltDataResults";
    this.spltDataResults.Size = new Size(3, 144 /*0x90*/);
    this.spltDataResults.TabIndex = 13;
    this.spltDataResults.TabStop = false;
    this.pnlDataValueDisplay.Controls.Add((System.Windows.Forms.Control) this.btnLoadCRCList);
    this.pnlDataValueDisplay.Controls.Add((System.Windows.Forms.Control) this.txtSearchID);
    this.pnlDataValueDisplay.Controls.Add((System.Windows.Forms.Control) this.lblByte);
    this.pnlDataValueDisplay.Controls.Add((System.Windows.Forms.Control) this.lblChar);
    this.pnlDataValueDisplay.Controls.Add((System.Windows.Forms.Control) this.btnRunSearch);
    this.pnlDataValueDisplay.Controls.Add((System.Windows.Forms.Control) this.lblUShort);
    this.pnlDataValueDisplay.Controls.Add((System.Windows.Forms.Control) this.lblShort);
    this.pnlDataValueDisplay.Controls.Add((System.Windows.Forms.Control) this.lblResults);
    this.pnlDataValueDisplay.Controls.Add((System.Windows.Forms.Control) this.lblUInt);
    this.pnlDataValueDisplay.Controls.Add((System.Windows.Forms.Control) this.lblString);
    this.pnlDataValueDisplay.Controls.Add((System.Windows.Forms.Control) this.lblInt);
    this.pnlDataValueDisplay.Controls.Add((System.Windows.Forms.Control) this.lblDouble);
    this.pnlDataValueDisplay.Controls.Add((System.Windows.Forms.Control) this.lblFloat);
    this.pnlDataValueDisplay.Dock = DockStyle.Left;
    this.pnlDataValueDisplay.Location = new Point(0, 0);
    this.pnlDataValueDisplay.Name = "pnlDataValueDisplay";
    this.pnlDataValueDisplay.Size = new Size(236, 144 /*0x90*/);
    this.pnlDataValueDisplay.TabIndex = 14;
    this.btnLoadCRCList.Location = new Point(130, 112 /*0x70*/);
    this.btnLoadCRCList.Name = "btnLoadCRCList";
    this.btnLoadCRCList.Size = new Size(100, 23);
    this.btnLoadCRCList.TabIndex = 12;
    this.btnLoadCRCList.Text = "Load CRC List";
    this.btnLoadCRCList.UseVisualStyleBackColor = true;
    this.btnLoadCRCList.Click += new EventHandler(this.btnLoadCRCList_Click);
    this.txtSearchID.Location = new Point(130, 6);
    this.txtSearchID.Name = "txtSearchID";
    this.txtSearchID.Size = new Size(100, 20);
    this.txtSearchID.TabIndex = 10;
    this.lblByte.AutoSize = true;
    this.lblByte.Location = new Point(5, 7);
    this.lblByte.Name = "lblByte";
    this.lblByte.Size = new Size(34, 13);
    this.lblByte.TabIndex = 0;
    this.lblByte.Text = "Byte: ";
    this.lblChar.AutoSize = true;
    this.lblChar.Location = new Point(5, 22);
    this.lblChar.Name = "lblChar";
    this.lblChar.Size = new Size(35, 13);
    this.lblChar.TabIndex = 1;
    this.lblChar.Text = "Char: ";
    this.btnRunSearch.Location = new Point(130, 27);
    this.btnRunSearch.Name = "btnRunSearch";
    this.btnRunSearch.Size = new Size(100, 23);
    this.btnRunSearch.TabIndex = 11;
    this.btnRunSearch.Text = "Run Ref Search";
    this.btnRunSearch.UseVisualStyleBackColor = true;
    this.btnRunSearch.Click += new EventHandler(this.btnRunSearch_Click);
    this.lblUShort.AutoSize = true;
    this.lblUShort.Location = new Point(5, 37);
    this.lblUShort.Name = "lblUShort";
    this.lblUShort.Size = new Size(46, 13);
    this.lblUShort.TabIndex = 2;
    this.lblUShort.Text = "UShort: ";
    this.lblShort.AutoSize = true;
    this.lblShort.Location = new Point(5, 52);
    this.lblShort.Name = "lblShort";
    this.lblShort.Size = new Size(38, 13);
    this.lblShort.TabIndex = 3;
    this.lblShort.Text = "Short: ";
    this.lblResults.AutoSize = true;
    this.lblResults.Location = new Point(185, 67);
    this.lblResults.Name = "lblResults";
    this.lblResults.Size = new Size(45, 13);
    this.lblResults.TabIndex = 9;
    this.lblResults.Text = "Results:";
    this.lblUInt.AutoSize = true;
    this.lblUInt.Location = new Point(5, 67);
    this.lblUInt.Name = "lblUInt";
    this.lblUInt.Size = new Size(33, 13);
    this.lblUInt.TabIndex = 4;
    this.lblUInt.Text = "UInt: ";
    this.lblString.AutoEllipsis = true;
    this.lblString.AutoSize = true;
    this.lblString.Location = new Point(5, (int) sbyte.MaxValue);
    this.lblString.Name = "lblString";
    this.lblString.Size = new Size(37, 13);
    this.lblString.TabIndex = 8;
    this.lblString.Text = "String:";
    this.lblInt.AutoSize = true;
    this.lblInt.Location = new Point(5, 82);
    this.lblInt.Name = "lblInt";
    this.lblInt.Size = new Size(25, 13);
    this.lblInt.TabIndex = 5;
    this.lblInt.Text = "Int: ";
    this.lblDouble.AutoSize = true;
    this.lblDouble.Location = new Point(5, 112 /*0x70*/);
    this.lblDouble.Name = "lblDouble";
    this.lblDouble.Size = new Size(44, 13);
    this.lblDouble.TabIndex = 7;
    this.lblDouble.Text = "Double:";
    this.lblFloat.AutoSize = true;
    this.lblFloat.Location = new Point(5, 97);
    this.lblFloat.Name = "lblFloat";
    this.lblFloat.Size = new Size(33, 13);
    this.lblFloat.TabIndex = 6;
    this.lblFloat.Text = "Float:";
    this.AutoScaleBaseSize = new Size(5, 13);
    this.ClientSize = new Size(710, 455);
    this.Controls.Add((System.Windows.Forms.Control) this.txtHexData);
    this.Controls.Add((System.Windows.Forms.Control) this.spltMiddle);
    this.Controls.Add((System.Windows.Forms.Control) this.txtCharData);
    this.Controls.Add((System.Windows.Forms.Control) this.spltTop);
    this.Controls.Add((System.Windows.Forms.Control) this.pnlTop);
    this.Controls.Add((System.Windows.Forms.Control) this.spltDataDisplay);
    this.Controls.Add((System.Windows.Forms.Control) this.pnlDataDisplay);
    this.Name = nameof (FormDataViewer);
    this.Padding = new Padding(4);
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Data Viewer";
    this.Load += new EventHandler(this.frmDataView_Load);
    this.pnlTop.ResumeLayout(false);
    this.nudLineLength.EndInit();
    this.pnlDataDisplay.ResumeLayout(false);
    this.pnlDataValueDisplay.ResumeLayout(false);
    this.pnlDataValueDisplay.PerformLayout();
    this.ResumeLayout(false);
    this.PerformLayout();
  }

  private void nudLineLength_ValueChanged(object sender, EventArgs e) => this.RefreshDisplay();

  private void frmDataView_Load(object sender, EventArgs e)
  {
  }

  private void txtHexData_MouseClick(object sender, MouseEventArgs e)
  {
    Member member1 = new Member(MemberType.BYTE, "byte", (string) null, (FableMod.ContentManagement.Link) null);
    Member member2 = new Member(MemberType.CHAR, "char", (string) null, (FableMod.ContentManagement.Link) null);
    Member member3 = new Member(MemberType.USHORT, "ushort", (string) null, (FableMod.ContentManagement.Link) null);
    Member member4 = new Member(MemberType.SHORT, "short", (string) null, (FableMod.ContentManagement.Link) null);
    Member member5 = new Member(MemberType.UINT, "uint", (string) null, (FableMod.ContentManagement.Link) null);
    Member member6 = new Member(MemberType.INT, "int", (string) null, (FableMod.ContentManagement.Link) null);
    Member member7 = new Member(MemberType.FLOAT, "float", (string) null, (FableMod.ContentManagement.Link) null);
    Member member8 = new Member(MemberType.DOUBLE, "double", (string) null, (FableMod.ContentManagement.Link) null);
    Member member9 = new Member(MemberType.STRING, "string", (string) null, (FableMod.ContentManagement.Link) null);
    int num = this.txtHexData.SelectionStart / ((int) this.nudLineLength.Value * 3 + Environment.NewLine.Length);
    int offset = (int) this.nudLineLength.Value * num + (this.txtHexData.SelectionStart - num * ((int) this.nudLineLength.Value * 3 + Environment.NewLine.Length)) / 3;
    try
    {
      member1.ReadIn(this.m_Data, offset);
      this.lblByte.Text = "Byte: " + member1.Value.ToString();
    }
    catch (Exception ex)
    {
      this.lblByte.Text = "Byte: Error";
    }
    try
    {
      member2.ReadIn(this.m_Data, offset);
      this.lblChar.Text = "Char: " + member2.Value.ToString();
    }
    catch (Exception ex)
    {
      this.lblChar.Text = "Char: Error";
    }
    try
    {
      member3.ReadIn(this.m_Data, offset);
      this.lblUShort.Text = "UShort: " + member3.Value.ToString();
    }
    catch (Exception ex)
    {
      this.lblUShort.Text = "UShort: Error";
    }
    try
    {
      member4.ReadIn(this.m_Data, offset);
      this.lblShort.Text = "Short: " + member4.Value.ToString();
    }
    catch (Exception ex)
    {
      this.lblShort.Text = "Short: Error";
    }
    try
    {
      member5.ReadIn(this.m_Data, offset);
      this.lblUInt.Text = "UInt: " + member5.Value.ToString();
      this.txtSearchID.Text = member5.Value.ToString();
    }
    catch (Exception ex)
    {
      this.lblUInt.Text = "UInt: Error";
    }
    try
    {
      member6.ReadIn(this.m_Data, offset);
      this.lblInt.Text = "Int: " + member6.Value.ToString();
    }
    catch (Exception ex)
    {
      this.lblInt.Text = "Int: Error";
    }
    try
    {
      member7.ReadIn(this.m_Data, offset);
      this.lblFloat.Text = "Float: " + member7.Value.ToString();
    }
    catch (Exception ex)
    {
      this.lblFloat.Text = "Float: Error";
    }
    try
    {
      member8.ReadIn(this.m_Data, offset);
      this.lblDouble.Text = "Double: " + member8.Value.ToString();
    }
    catch (Exception ex)
    {
      this.lblDouble.Text = "Double: Error";
    }
    try
    {
      member9.ReadIn(this.m_Data, offset);
      this.lblString.Text = "String: " + member9.Value.ToString();
    }
    catch (Exception ex)
    {
      this.lblString.Text = "String: Error";
    }
  }

  private void btnRunSearch_Click(object sender, EventArgs e)
  {
    uint key;
    try
    {
      key = uint.Parse(this.txtSearchID.Text);
    }
    catch (Exception ex)
    {
      int num = (int) MessageBox.Show((IWin32Window) this, "Error parsing search id.", "Invalid ID");
      this.lvResults.Clear();
      return;
    }
    this.lvResults.Clear();
    ContentObject contentObject = (ContentObject) null;
    if (contentObject != null)
      this.lvResults.Items.Add(new ListViewItem(contentObject.Name)
      {
        Tag = contentObject.Object
      });
    string str;
    if (this.m_CRCList == null || !this.m_CRCList.TryGetValue(key, out str))
      return;
    this.lvResults.Items.Add(new ListViewItem($"CRC Match: \"{str}\""));
  }

  private void lvResults_MouseDown(object sender, MouseEventArgs e)
  {
    if (e.Button != MouseButtons.Left || e.Clicks != 2)
      return;
    ListViewItem itemAt = this.lvResults.GetItemAt(e.X, e.Y);
    if (itemAt == null || itemAt.Tag == null)
      return;
    ContentManager.Instance.ShowEntry(itemAt.Tag, false);
  }

  private void btnLoadCRCList_Click(object sender, EventArgs e)
  {
    OpenFileDialog openFileDialog = new OpenFileDialog();
    openFileDialog.Filter = "*.txt|*.txt";
    openFileDialog.InitialDirectory = AppDomain.CurrentDomain.BaseDirectory;
    if (openFileDialog.ShowDialog() != DialogResult.OK)
      return;
    this.m_CRCList = new SortedList<uint, string>();
    StreamReader streamReader = File.OpenText(openFileDialog.FileName);
    string str;
    while ((str = streamReader.ReadLine()) != null)
    {
      string[] strArray = str.Split("\t".ToCharArray());
      this.m_CRCList.Add(uint.Parse(strArray[0]), strArray[1]);
    }
    streamReader.Close();
  }
}
