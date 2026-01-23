// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormObjectBuilder
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.BIG;
using FableMod.BIN;
using FableMod.ContentManagement;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormObjectBuilder : Form
{
  private ObjectTemplate myTemplate = new ObjectTemplate();
  private Dictionary<int, ComboBox> myComboBoxes = new Dictionary<int, ComboBox>();
  private Dictionary<string, ComboBox> myComboBoxPool = new Dictionary<string, ComboBox>();
  private Dictionary<int, CheckBox> myRefCheckBoxes = new Dictionary<int, CheckBox>();
  private BINFile myGameBin;
  private IContainer components;
  private ComboBox comboBoxDefs;
  private Panel panelTop;
  private Label label1;
  private Panel panelBottom;
  private Button buttonCreate;
  private Button buttonClose;
  private Panel panelTemplate;
  private TextBox textBoxName;
  private Label label2;
  private CheckBox checkBoxNamed;
  private ComboBox comboBoxTemplate;
  private Label labelTemplate;
  private Panel panelProgress;
  private ProgressBar progressBar;
  private ToolTip toolTip;

  public FormObjectBuilder()
  {
    this.InitializeComponent();
    ThemeManager.ApplyTheme(this);
    this.myGameBin = ContentManager.Instance.Objects;
  }

  private ComboBox NewComboBox(string name)
  {
    ComboBox comboBox;
    if (!this.myComboBoxPool.TryGetValue(name, out comboBox))
      return new ComboBox();
    this.myComboBoxPool.Remove(name);
    return comboBox;
  }

  private void AddToPool(string name, ComboBox box)
  {
    if (this.myComboBoxPool.ContainsKey(name))
      return;
    this.myComboBoxPool.Add(name, box);
  }

  private void FormItemBuilder_Load(object sender, EventArgs e)
  {
    foreach (DefinitionType definition in ContentManager.Instance.Definitions.GetDefinitions())
      this.comboBoxDefs.Items.Add((object) definition.Name);
  }

  private void AssetFill(ComboBox box, AssetTemplate item)
  {
    if (item.Type == ContentType.Text)
    {
      BIGBank textBank = ContentManager.Instance.TextBank;
      for (int index = 0; index < textBank.EntryCount; ++index)
      {
        AssetEntry entry = textBank.get_Entries(index);
        if (entry.Type == 0U)
          box.Items.Add((object) new FormObjectBuilder.AssetComboBoxItem(entry));
      }
    }
    else if (item.Type == ContentType.Graphics)
    {
      BIGBank graphicsBank = ContentManager.Instance.GraphicsBank;
      for (int index = 0; index < graphicsBank.EntryCount; ++index)
      {
        AssetEntry entry = graphicsBank.get_Entries(index);
        switch (entry.Type)
        {
          case 1:
          case 2:
          case 4:
          case 5:
            box.Items.Add((object) new FormObjectBuilder.AssetComboBoxItem(entry));
            break;
        }
      }
    }
    else
    {
      if (item.Type != ContentType.MainTextures)
        return;
      BIGBank mainTextureBank = ContentManager.Instance.MainTextureBank;
      for (int index = 0; index < mainTextureBank.EntryCount; ++index)
        box.Items.Add((object) new FormObjectBuilder.AssetComboBoxItem(mainTextureBank.get_Entries(index)));
    }
  }

  private void UpdateLinks(ComboBox comboBox)
  {
    BaseTemplate baseTemplate1 = this.myTemplate.get_Items(int.Parse(comboBox.Tag.ToString()));
    if (!(baseTemplate1.GetType() == typeof (DefTypeTemplate)))
      return;
    DefTypeTemplate defTypeTemplate1 = baseTemplate1 as DefTypeTemplate;
    foreach (KeyValuePair<int, ComboBox> comboBox1 in this.myComboBoxes)
    {
      if (comboBox1.Key != defTypeTemplate1.ID)
      {
        BaseTemplate baseTemplate2 = this.myTemplate.get_Items(int.Parse(comboBox1.Value.Tag.ToString()));
        if (baseTemplate2.LinkTo == baseTemplate1)
        {
          if (baseTemplate2.GetType() == typeof (DefTypeTemplate))
          {
            DefTypeTemplate defTypeTemplate2 = baseTemplate2 as DefTypeTemplate;
            string cdefDataId = this.GetCDefDataID(comboBox.SelectedItem.ToString(), defTypeTemplate2.Type);
            if (cdefDataId != null)
            {
              comboBox1.Value.Enabled = true;
              comboBox1.Value.SelectedIndex = comboBox1.Value.Items.IndexOf((object) cdefDataId);
            }
          }
          else
          {
            AssetTemplate assetTemplate = baseTemplate2 as AssetTemplate;
            string assetLink = this.GetAssetLink(comboBox.SelectedItem.ToString(), assetTemplate.ControlID, assetTemplate.Element);
            if (assetLink != null)
            {
              comboBox1.Value.Enabled = true;
              comboBox1.Value.SelectedIndex = comboBox1.Value.Items.IndexOf((object) assetLink);
            }
          }
        }
      }
    }
  }

  private void itemComboBox_SelectedIndexChanged(object sender, EventArgs e)
  {
    this.UpdateLinks(sender as ComboBox);
  }

  private void DefTypeLink(BINEntry entryDst, BINEntry entrySrc, string cdef)
  {
    DefinitionType definition = FileDatabase.Instance.Definitions.GetDefinition(entryDst.Definition);
    definition.ReadIn(entryDst);
    if (definition.CDefs == null)
      return;
    CDefLink cdefLink = definition.get_CDefLinks(cdef);
    if (cdefLink != null)
    {
      cdefLink.DataID = (object) entrySrc.ID.ToString();
      definition.Write(entryDst);
    }
  }

  private string GetCDefDataID(string defName, string cdef)
  {
    BINEntry entryByName = this.myGameBin.GetEntryByName(defName);
    if (entryByName != null)
    {
      DefinitionType definition = FileDatabase.Instance.Definitions.GetDefinition(entryByName.Definition);
      definition.ReadIn(entryByName);
      if (definition.CDefs != null)
      {
        CDefLink cdefLink = definition.get_CDefLinks(cdef);
        if (cdefLink != null)
          return cdefLink.DataID.ToString();
      }
    }
    return (string) null;
  }

  private string GetAssetLink(string defName, uint control, int element)
  {
    BINEntry binEntry = this.GetBINEntry(defName);
    if (binEntry != null)
    {
      DefinitionType definition = FileDatabase.Instance.Definitions.GetDefinition(binEntry.Definition);
      definition.ReadIn(binEntry);
      FableMod.ContentManagement.Control control1 = definition.FindControl(control);
      if (control1 != null)
      {
        Member member = (Member) control1.Members[element];
        ContentObject entry = ContentManager.Instance.FindEntry(member.Link.To, member.Value);
        if (entry != null)
          return entry.Name;
      }
    }
    return (string) null;
  }

  private void AssetLink(BINEntry entryDst, AssetTemplate assetItem, uint assetId)
  {
    DefinitionType definition = ContentManager.Instance.Definitions.GetDefinition(entryDst.Definition);
    definition.ReadIn(entryDst);
    FableMod.ContentManagement.Control control = definition.FindControl(assetItem.ControlID);
    if (control != null)
    {
      ((Member) control.Members[assetItem.Element]).Value = (object) assetId.ToString();
      definition.Write(entryDst);
    }
  }

  private string GetItemName(string item)
  {
    return item.ToUpper().Replace(" ", "_").Replace("-", "_").Replace(":", "_");
  }

  private BINEntry GetBINEntry(string comboItem)
  {
    BINEntry binEntry = this.myGameBin.GetEntryByName(comboItem);
    if (binEntry == null)
    {
      try
      {
        binEntry = this.myGameBin.get_Entries(int.Parse(comboItem.ToString()));
      }
      catch (Exception ex)
      {
      }
    }
    return binEntry;
  }

  private string GetAssetName(AssetBank bank, string name)
  {
    if (bank.FindEntryBySymbolName(name) == null)
      return name;
    for (int index = 1; index <= 99; ++index)
    {
      string name1 = $"{name}_{index:D2}";
      if (bank.FindEntryBySymbolName(name1) == null)
        return name1;
    }
    return (string) null;
  }

  private void FixMainEntry(BINEntry entry, int originalId)
  {
    DefinitionType definition = ContentManager.Instance.Definitions.GetDefinition(entry.Definition);
    definition.ReadIn(entry);
    definition.FixLinks(LinkDestination.GameBINEntryID, (object) originalId, (object) entry.ID);
    definition.Write(entry);
  }

  private void buttonOk_Click(object sender, EventArgs e)
  {
    if (this.textBoxName.Text.Length == 0)
    {
      int num1 = (int) FormMain.Instance.ErrorMessage("Invalid base name. It can not be empty.");
    }
    else
    {
      string text = this.textBoxName.Text;
      Dictionary<int, BINEntry> dictionary1 = new Dictionary<int, BINEntry>();
      Dictionary<int, AssetEntry> dictionary2 = new Dictionary<int, AssetEntry>();
      StringBuilder stringBuilder = new StringBuilder();
      stringBuilder.AppendLine("Created objects:");
      for (int index = 0; index < this.myTemplate.ItemCount; ++index)
      {
        BaseTemplate baseTemplate = this.myTemplate.get_Items(index);
        bool flag = this.myRefCheckBoxes[baseTemplate.ID].Checked;
        object selectedItem = this.myComboBoxes[baseTemplate.ID].SelectedItem;
        if (selectedItem != null)
        {
          if (baseTemplate.GetType() == typeof (DefTypeTemplate))
          {
            DefTypeTemplate defTypeTemplate = baseTemplate as DefTypeTemplate;
            BINEntry binEntry1 = this.GetBINEntry(selectedItem.ToString());
            if (binEntry1 != null)
            {
              string Name = $"{defTypeTemplate.Type}_{text}";
              if (!this.checkBoxNamed.Checked && !baseTemplate.Named)
                Name = "";
              BINEntry binEntry2;
              if (flag)
              {
                binEntry2 = binEntry1;
              }
              else
              {
                binEntry2 = this.myGameBin.AddEntry(Name, defTypeTemplate.Type, binEntry1.Data);
                binEntry2.Modified = true;
                if (!string.IsNullOrEmpty(binEntry2.Name))
                  stringBuilder.AppendFormat("  {0}", (object) binEntry2.Name);
                else
                  stringBuilder.AppendFormat("  {0}:{1}", (object) binEntry2.Definition, (object) binEntry2.ID);
                stringBuilder.AppendLine("");
              }
              dictionary1.Add(baseTemplate.ID, binEntry2);
            }
            else
            {
              int num2 = (int) FormMain.Instance.ErrorMessage("Entry not found");
            }
          }
          else
          {
            AssetEntry entry = ((FormObjectBuilder.AssetComboBoxItem) selectedItem).Entry;
            if (entry != null)
            {
              AssetEntry assetEntry;
              if (flag)
              {
                assetEntry = entry;
              }
              else
              {
                AssetTemplate assetTemplate = baseTemplate as AssetTemplate;
                assetEntry = entry.Bank.NewEntry(this.GetAssetName(entry.Bank, $"{assetTemplate.Prefix}_{text}"), entry.Type);
                assetEntry.SubHeader = entry.SubHeader;
                assetEntry.Data = entry.Data;
                stringBuilder.AppendFormat("  {0}", (object) assetEntry.DevSymbolName);
                stringBuilder.AppendLine("");
              }
              dictionary2.Add(baseTemplate.ID, assetEntry);
            }
          }
        }
      }
      for (int index = 0; index < this.myTemplate.ItemCount; ++index)
      {
        BaseTemplate assetItem = this.myTemplate.get_Items(index);
        if (assetItem.LinkTo != null)
        {
          DefTypeTemplate linkTo = assetItem.LinkTo;
          if (assetItem.GetType() == typeof (DefTypeTemplate))
          {
            this.DefTypeLink(dictionary1[linkTo.ID], dictionary1[assetItem.ID], assetItem.Name);
          }
          else
          {
            AssetEntry assetEntry = (AssetEntry) null;
            if (dictionary2.TryGetValue(assetItem.ID, out assetEntry))
              this.AssetLink(dictionary1[linkTo.ID], (AssetTemplate) assetItem, assetEntry.ID);
          }
        }
      }
      DefTypeTemplate defTypeTemplate1 = (DefTypeTemplate) this.myTemplate.get_Items(0);
      this.FixMainEntry(dictionary1[defTypeTemplate1.ID], defTypeTemplate1.OriginalID);
      int num3 = (int) FormMain.Instance.InfoMessage(stringBuilder.ToString());
    }
  }

  private void buttonCancel_Click(object sender, EventArgs e) => this.Close();

  private void comboBoxTemplate_DropDownClosed(object sender, EventArgs e)
  {
    if (this.comboBoxTemplate.SelectedIndex < 0)
    {
      this.panelTemplate.Visible = false;
      this.buttonCreate.Enabled = false;
    }
    else
    {
      this.myTemplate.Build(this.myGameBin.GetEntryByName(this.comboBoxTemplate.SelectedItem.ToString()));
      this.panelProgress.Visible = true;
      this.panelProgress.Update();
      this.panelTemplate.Visible = false;
      this.panelTemplate.Controls.Clear();
      this.panelTemplate.SuspendLayout();
      this.myComboBoxes.Clear();
      this.myRefCheckBoxes.Clear();
      int left = this.labelTemplate.Left;
      int num1 = 5;
      int num2 = 0;
      int num3 = 0;
      int num4 = 10;
      CheckBox checkBox1 = new CheckBox();
      this.myComboBoxes.Add(this.myTemplate.get_Items(0).ID, this.comboBoxTemplate);
      this.myRefCheckBoxes.Add(this.myTemplate.get_Items(0).ID, checkBox1);
      this.comboBoxTemplate.Tag = (object) 0;
      for (int index = 1; index < this.myTemplate.ItemCount; ++index)
      {
        Label label = new Label();
        label.AutoSize = true;
        label.Text = this.myTemplate.get_Items(index).Name + ":";
        this.panelTemplate.Controls.Add((System.Windows.Forms.Control) label);
        label.Left = left;
        label.Top = num1;
        num1 += label.Height + num4;
        if (label.Width > num2)
          num2 = label.Width;
        if (label.Height > num3)
          num3 = label.Height;
      }
      int num5 = 5;
      int width = this.panelTop.Width;
      int right = this.comboBoxDefs.Right;
      this.progressBar.Value = 0;
      this.progressBar.Maximum = this.myTemplate.ItemCount;
      this.progressBar.Update();
      for (int index = 1; index < this.myTemplate.ItemCount; ++index)
      {
        BaseTemplate baseTemplate = this.myTemplate.get_Items(index);
        ComboBox box = this.NewComboBox(baseTemplate.Name);
        this.panelTemplate.Controls.Add((System.Windows.Forms.Control) box);
        box.Left = num2 + left + 5;
        box.Top = num5;
        box.Width = this.comboBoxDefs.Right - box.Left - 80 /*0x50*/;
        box.Anchor |= AnchorStyles.Right;
        box.DropDownStyle = ComboBoxStyle.DropDownList;
        box.AutoCompleteSource = AutoCompleteSource.ListItems;
        box.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        box.Tag = (object) index;
        box.Sorted = true;
        if (baseTemplate.GetType() == typeof (DefTypeTemplate))
        {
          DefTypeTemplate defTypeTemplate = baseTemplate as DefTypeTemplate;
          if (box.Items.Count == 0)
          {
            foreach (BINEntry binEntry in this.myGameBin.GetEntriesByDefinition(defTypeTemplate.Type))
              box.Items.Add(binEntry.Name.Length > 0 ? (object) binEntry.Name : (object) binEntry.ID.ToString());
          }
        }
        else if (box.Items.Count == 0)
          this.AssetFill(box, (AssetTemplate) baseTemplate);
        if (box.Items.Count > 0)
          box.SelectedIndex = 0;
        box.SelectedIndexChanged += new EventHandler(this.itemComboBox_SelectedIndexChanged);
        num5 += num3 + num4;
        this.myComboBoxes.Add(baseTemplate.ID, box);
        ++this.progressBar.Value;
        this.progressBar.Update();
      }
      for (int index = 1; index < this.myTemplate.ItemCount; ++index)
      {
        BaseTemplate baseTemplate = this.myTemplate.get_Items(index);
        CheckBox checkBox2 = new CheckBox();
        this.panelTemplate.Controls.Add((System.Windows.Forms.Control) checkBox2);
        checkBox2.Left = this.myComboBoxes[baseTemplate.ID].Right + 5;
        checkBox2.Top = this.myComboBoxes[baseTemplate.ID].Top;
        checkBox2.Text = "Ref";
        checkBox2.Checked = baseTemplate.Ref;
        checkBox2.Anchor = AnchorStyles.Top | AnchorStyles.Right;
        this.toolTip.SetToolTip((System.Windows.Forms.Control) checkBox2, "Use this object as a reference");
        this.myRefCheckBoxes.Add(baseTemplate.ID, checkBox2);
      }
      for (int index = 1; index < this.myTemplate.ItemCount; ++index)
      {
        BaseTemplate baseTemplate = this.myTemplate.get_Items(index);
        this.AddToPool(baseTemplate.Name, this.myComboBoxes[baseTemplate.ID]);
      }
      this.UpdateLinks(this.comboBoxTemplate);
      this.panelTemplate.ResumeLayout();
      this.panelTemplate.Visible = true;
      this.panelTemplate.Visible = true;
      this.buttonCreate.Enabled = true;
      this.panelProgress.Visible = false;
      this.panelProgress.Update();
    }
  }

  private void comboBoxDefs_DropDownClosed(object sender, EventArgs e)
  {
    this.panelTemplate.Visible = false;
    this.comboBoxTemplate.Enabled = this.comboBoxDefs.SelectedIndex >= 0;
    this.comboBoxTemplate.Items.Clear();
    if (this.comboBoxDefs.SelectedIndex < 0)
      return;
    foreach (BINEntry binEntry in this.myGameBin.GetEntriesByDefinition(this.comboBoxDefs.SelectedItem.ToString()))
    {
      if (!string.IsNullOrEmpty(binEntry.Name))
        this.comboBoxTemplate.Items.Add((object) binEntry.Name);
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
    this.comboBoxDefs = new ComboBox();
    this.panelTop = new Panel();
    this.comboBoxTemplate = new ComboBox();
    this.labelTemplate = new Label();
    this.label1 = new Label();
    this.panelBottom = new Panel();
    this.checkBoxNamed = new CheckBox();
    this.textBoxName = new TextBox();
    this.label2 = new Label();
    this.buttonCreate = new Button();
    this.buttonClose = new Button();
    this.panelTemplate = new Panel();
    this.panelProgress = new Panel();
    this.progressBar = new ProgressBar();
    this.toolTip = new ToolTip(this.components);
    this.panelTop.SuspendLayout();
    this.panelBottom.SuspendLayout();
    this.panelProgress.SuspendLayout();
    this.SuspendLayout();
    this.comboBoxDefs.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.comboBoxDefs.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    this.comboBoxDefs.AutoCompleteSource = AutoCompleteSource.ListItems;
    this.comboBoxDefs.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBoxDefs.FormattingEnabled = true;
    this.comboBoxDefs.Location = new Point(96 /*0x60*/, 6);
    this.comboBoxDefs.Name = "comboBoxDefs";
    this.comboBoxDefs.Size = new Size(301, 21);
    this.comboBoxDefs.Sorted = true;
    this.comboBoxDefs.TabIndex = 0;
    this.toolTip.SetToolTip((System.Windows.Forms.Control) this.comboBoxDefs, "Select object base type");
    this.comboBoxDefs.DropDownClosed += new EventHandler(this.comboBoxDefs_DropDownClosed);
    this.panelTop.Controls.Add((System.Windows.Forms.Control) this.comboBoxTemplate);
    this.panelTop.Controls.Add((System.Windows.Forms.Control) this.labelTemplate);
    this.panelTop.Controls.Add((System.Windows.Forms.Control) this.label1);
    this.panelTop.Controls.Add((System.Windows.Forms.Control) this.comboBoxDefs);
    this.panelTop.Dock = DockStyle.Top;
    this.panelTop.Location = new Point(0, 0);
    this.panelTop.Name = "panelTop";
    this.panelTop.Size = new Size(409, 64 /*0x40*/);
    this.panelTop.TabIndex = 1;
    this.comboBoxTemplate.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.comboBoxTemplate.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
    this.comboBoxTemplate.AutoCompleteSource = AutoCompleteSource.ListItems;
    this.comboBoxTemplate.DropDownStyle = ComboBoxStyle.DropDownList;
    this.comboBoxTemplate.Enabled = false;
    this.comboBoxTemplate.FormattingEnabled = true;
    this.comboBoxTemplate.Location = new Point(96 /*0x60*/, 36);
    this.comboBoxTemplate.Name = "comboBoxTemplate";
    this.comboBoxTemplate.Size = new Size(301, 21);
    this.comboBoxTemplate.Sorted = true;
    this.comboBoxTemplate.TabIndex = 3;
    this.toolTip.SetToolTip((System.Windows.Forms.Control) this.comboBoxTemplate, "Select object template");
    this.comboBoxTemplate.DropDownClosed += new EventHandler(this.comboBoxTemplate_DropDownClosed);
    this.labelTemplate.AutoSize = true;
    this.labelTemplate.Location = new Point(12, 39);
    this.labelTemplate.Name = "labelTemplate";
    this.labelTemplate.Size = new Size(54, 13);
    this.labelTemplate.TabIndex = 2;
    this.labelTemplate.Text = "Template:";
    this.label1.AutoSize = true;
    this.label1.Location = new Point(12, 9);
    this.label1.Name = "label1";
    this.label1.Size = new Size(78, 13);
    this.label1.TabIndex = 1;
    this.label1.Text = "DefinitionType:";
    this.panelBottom.Controls.Add((System.Windows.Forms.Control) this.checkBoxNamed);
    this.panelBottom.Controls.Add((System.Windows.Forms.Control) this.textBoxName);
    this.panelBottom.Controls.Add((System.Windows.Forms.Control) this.label2);
    this.panelBottom.Controls.Add((System.Windows.Forms.Control) this.buttonCreate);
    this.panelBottom.Controls.Add((System.Windows.Forms.Control) this.buttonClose);
    this.panelBottom.Dock = DockStyle.Bottom;
    this.panelBottom.Location = new Point(0, 237);
    this.panelBottom.Name = "panelBottom";
    this.panelBottom.Size = new Size(409, 91);
    this.panelBottom.TabIndex = 2;
    this.checkBoxNamed.AutoSize = true;
    this.checkBoxNamed.Location = new Point(15, 6);
    this.checkBoxNamed.Name = "checkBoxNamed";
    this.checkBoxNamed.Size = new Size(124, 17);
    this.checkBoxNamed.TabIndex = 4;
    this.checkBoxNamed.Text = "CDef Name Override";
    this.checkBoxNamed.TextAlign = ContentAlignment.TopRight;
    this.toolTip.SetToolTip((System.Windows.Forms.Control) this.checkBoxNamed, "Give names to all created CDef objects");
    this.checkBoxNamed.UseVisualStyleBackColor = true;
    this.textBoxName.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.textBoxName.Location = new Point(90, 29);
    this.textBoxName.MaxLength = 48 /*0x30*/;
    this.textBoxName.Name = "textBoxName";
    this.textBoxName.Size = new Size(307, 20);
    this.textBoxName.TabIndex = 3;
    this.toolTip.SetToolTip((System.Windows.Forms.Control) this.textBoxName, "Base identifier for the created object(s). Names are not checked!");
    this.label2.AutoSize = true;
    this.label2.Location = new Point(12, 32 /*0x20*/);
    this.label2.Name = "label2";
    this.label2.Size = new Size(65, 13);
    this.label2.TabIndex = 2;
    this.label2.Text = "Base Name:";
    this.buttonCreate.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonCreate.DialogResult = DialogResult.OK;
    this.buttonCreate.Enabled = false;
    this.buttonCreate.Location = new Point(241, 55);
    this.buttonCreate.Name = "buttonCreate";
    this.buttonCreate.Size = new Size(75, 23);
    this.buttonCreate.TabIndex = 1;
    this.buttonCreate.Text = "Create";
    this.toolTip.SetToolTip((System.Windows.Forms.Control) this.buttonCreate, "Create the object(s)");
    this.buttonCreate.UseVisualStyleBackColor = true;
    this.buttonCreate.Click += new EventHandler(this.buttonOk_Click);
    this.buttonClose.Anchor = AnchorStyles.Top | AnchorStyles.Right;
    this.buttonClose.DialogResult = DialogResult.Cancel;
    this.buttonClose.Location = new Point(322, 55);
    this.buttonClose.Name = "buttonClose";
    this.buttonClose.Size = new Size(75, 23);
    this.buttonClose.TabIndex = 0;
    this.buttonClose.Text = "Close";
    this.buttonClose.UseVisualStyleBackColor = true;
    this.buttonClose.Click += new EventHandler(this.buttonCancel_Click);
    this.panelTemplate.AutoScroll = true;
    this.panelTemplate.BorderStyle = BorderStyle.Fixed3D;
    this.panelTemplate.Dock = DockStyle.Fill;
    this.panelTemplate.Location = new Point(0, 99);
    this.panelTemplate.Name = "panelTemplate";
    this.panelTemplate.Size = new Size(409, 138);
    this.panelTemplate.TabIndex = 3;
    this.panelTemplate.Visible = false;
    this.panelProgress.Controls.Add((System.Windows.Forms.Control) this.progressBar);
    this.panelProgress.Dock = DockStyle.Top;
    this.panelProgress.Location = new Point(0, 64 /*0x40*/);
    this.panelProgress.Name = "panelProgress";
    this.panelProgress.Size = new Size(409, 35);
    this.panelProgress.TabIndex = 4;
    this.panelProgress.Visible = false;
    this.progressBar.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
    this.progressBar.Location = new Point(15, 6);
    this.progressBar.Name = "progressBar";
    this.progressBar.Size = new Size(382, 23);
    this.progressBar.TabIndex = 0;
    this.toolTip.IsBalloon = true;
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(409, 328);
    this.Controls.Add((System.Windows.Forms.Control) this.panelTemplate);
    this.Controls.Add((System.Windows.Forms.Control) this.panelProgress);
    this.Controls.Add((System.Windows.Forms.Control) this.panelTop);
    this.Controls.Add((System.Windows.Forms.Control) this.panelBottom);
    this.Name = nameof (FormObjectBuilder);
    this.ShowInTaskbar = false;
    this.StartPosition = FormStartPosition.CenterParent;
    this.Text = "Object Builder";
    this.Load += new EventHandler(this.FormItemBuilder_Load);
    this.panelTop.ResumeLayout(false);
    this.panelTop.PerformLayout();
    this.panelBottom.ResumeLayout(false);
    this.panelBottom.PerformLayout();
    this.panelProgress.ResumeLayout(false);
    this.ResumeLayout(false);
  }

  private class AssetComboBoxItem : IComparable
  {
    public AssetEntry Entry;

    public AssetComboBoxItem(AssetEntry entry) => this.Entry = entry;

    public override string ToString() => this.Entry.DevSymbolName;

    public override bool Equals(object obj)
    {
      return obj.GetType() == typeof (string) ? this.Entry.DevSymbolName.Equals(obj) : base.Equals(obj);
    }

    public override int GetHashCode()
    {
      return this.Entry.DevSymbolName.GetHashCode();
    }

    public int CompareTo(object o)
    {
      return this.Entry.DevSymbolName.CompareTo((o as FormObjectBuilder.AssetComboBoxItem).Entry.DevSymbolName);
    }
  }
}
