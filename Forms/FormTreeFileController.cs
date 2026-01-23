// Decompiled with JetBrains decompiler
// Type: ChocolateBox.FormTreeFileController
// Assembly: ChocolateBox, Version=1.3.8.150, Culture=neutral, PublicKeyToken=null
// MVID: 2BF4E5FB-25BF-4031-BC83-D8C6B8D9B45E
// Assembly location: C:\Users\Cornelio\Desktop\Chocolate Box\ChocolateBox.exe

using FableMod.ContentManagement;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

#nullable disable
namespace ChocolateBox;

public class FormTreeFileController : FormFileController
{
  private IContainer components;
  protected TreeView treeView;
  private static bool myStaticFullNames = Settings.GetBool("TreeView", "FullNames", false);
  private bool myUnderscoreSeparator = true;
  private int myMinTextLength = 3;
  private int myCurrentFind = -1;
  protected List<TreeNode> myFindNodes = new List<TreeNode>();
  protected List<TreeNode> myAllNodes = new List<TreeNode>();
  protected Panel panelSearch;
  protected TextBox textBoxSearch;
  protected Label labelSearch;
  protected TextBox textBoxExclude;
  protected Label labelExclude;
  protected TextBox textBoxID;
  protected Label labelID;
  protected Button btnSelectAll;
  protected Button btnClearAll;
  protected Button btnExportSelected;

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.treeView = new TreeView();
    this.panelSearch = new Panel();
    this.textBoxSearch = new TextBox();
    this.labelSearch = new Label();
    this.textBoxExclude = new TextBox();
    this.labelExclude = new Label();
    this.textBoxID = new TextBox();
    this.labelID = new Label();
    this.btnSelectAll = new Button();
    this.btnClearAll = new Button();
    this.btnExportSelected = new Button();
    this.SuspendLayout();
    this.panelSearch.Controls.Add(this.textBoxSearch);
    this.panelSearch.Controls.Add(this.labelSearch);
    this.panelSearch.Controls.Add(this.textBoxExclude);
    this.panelSearch.Controls.Add(this.labelExclude);
    this.panelSearch.Controls.Add(this.textBoxID);
    this.panelSearch.Controls.Add(this.labelID);
    this.panelSearch.Controls.Add(this.btnSelectAll);
    this.panelSearch.Controls.Add(this.btnClearAll);
    this.panelSearch.Controls.Add(this.btnExportSelected);
    this.panelSearch.Dock = DockStyle.Top;
    this.panelSearch.Height = 70;
    this.panelSearch.Padding = new Padding(5);
    this.labelSearch.Text = "Search";
    this.labelSearch.Location = new Point(10, 12);
    this.labelSearch.AutoSize = true;
    this.textBoxSearch.Location = new Point(60, 10);
    this.textBoxSearch.Width = 120;
    this.textBoxSearch.Anchor = AnchorStyles.Top | AnchorStyles.Left;
    this.textBoxSearch.TextChanged += new EventHandler(this.textBoxSearch_TextChanged);
    this.labelExclude.Text = "Exclude";
    this.labelExclude.Location = new Point(190, 12);
    this.labelExclude.AutoSize = true;
    this.textBoxExclude.Location = new Point(245, 10);
    this.textBoxExclude.Width = 120;
    this.textBoxExclude.Anchor = AnchorStyles.Top | AnchorStyles.Left;
    this.textBoxExclude.TextChanged += new EventHandler(this.textBoxSearch_TextChanged);
    this.labelID.Text = "ID";
    this.labelID.Location = new Point(375, 12);
    this.labelID.AutoSize = true;
    this.textBoxID.Location = new Point(400, 10);
    this.textBoxID.Width = 70;
    this.textBoxID.Anchor = AnchorStyles.Top | AnchorStyles.Left;
    this.textBoxID.TextChanged += new EventHandler(this.textBoxSearch_TextChanged);
    this.btnSelectAll.Text = "Select All Filtered";
    this.btnSelectAll.Location = new Point(10, 40);
    this.btnSelectAll.Width = 110;
    this.btnSelectAll.Click += new EventHandler(this.btnSelectAll_Click);
    this.btnClearAll.Text = "Clear All Filtered";
    this.btnClearAll.Location = new Point(130, 40);
    this.btnClearAll.Width = 110;
    this.btnClearAll.Click += new EventHandler(this.btnClearAll_Click);
    this.btnExportSelected.Text = "Export Selected Assets";
    this.btnExportSelected.Location = new Point(250, 40);
    this.btnExportSelected.Width = 140;
    this.btnExportSelected.Visible = false;
    this.btnExportSelected.Click += new EventHandler(this.btnExportSelected_Click);
    this.treeView.CheckBoxes = true;
    this.treeView.Dock = DockStyle.Fill;
    this.treeView.HideSelection = false;
    this.treeView.Location = new Point(0, 70);
    this.treeView.Name = "treeView";
    this.treeView.Size = new Size(482, 320);
    this.treeView.TabIndex = 0;
    this.treeView.DoubleClick += new EventHandler(this.treeView_DoubleClick);
    this.treeView.KeyUp += new KeyEventHandler(this.treeView_KeyUp);
    this.treeView.ItemDrag += new ItemDragEventHandler(this.treeView_ItemDrag);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(482, 390);
    this.Controls.Add((System.Windows.Forms.Control) this.treeView);
    this.Controls.Add((System.Windows.Forms.Control) this.panelSearch);
    this.Name = nameof (FormTreeFileController);
    this.Text = nameof (FormTreeFileController);
    this.ResumeLayout(false);
  }

  private void textBoxSearch_TextChanged(object sender, EventArgs e)
  {
    this.ApplyFilter(this.textBoxSearch.Text, this.textBoxExclude.Text, this.textBoxID.Text);
  }

  private void btnSelectAll_Click(object sender, EventArgs e)
  {
    this.SetCheckState(this.treeView.Nodes, true);
  }

  private void btnClearAll_Click(object sender, EventArgs e)
  {
    this.SetCheckState(this.treeView.Nodes, false);
  }

  protected virtual void btnExportSelected_Click(object sender, EventArgs e)
  {
      this.OnExportSelected();
  }

  protected virtual void OnExportSelected()
  {
  }

  private void SetCheckState(TreeNodeCollection nodes, bool isChecked)
  {
    foreach (TreeNode node in nodes)
    {
      node.Checked = isChecked;
      if (node.Nodes.Count > 0)
        this.SetCheckState(node.Nodes, isChecked);
    }
  }

  protected void ApplyFilter(string pattern, string excludePattern = null, string idPattern = null)
  {
    this.treeView.BeginUpdate();
    this.treeView.Nodes.Clear();
    if (string.IsNullOrEmpty(pattern) && string.IsNullOrEmpty(excludePattern) && string.IsNullOrEmpty(idPattern))
    {
      foreach (TreeNode node in this.myAllNodes)
        this.treeView.Nodes.Add(node);
    }
    else
    {
      List<Regex> regexes = this.CreateRegexList(pattern);
      List<Regex> excludeRegexes = this.CreateRegexList(excludePattern);
      uint? id = new uint?();
      uint result;
      if (!string.IsNullOrEmpty(idPattern) && uint.TryParse(idPattern, out result))
        id = new uint?(result);
      foreach (TreeNode node in this.myAllNodes)
      {
        TreeNode treeNode = this.FilterNode(node, regexes, excludeRegexes, id);
        if (treeNode != null)
          this.treeView.Nodes.Add(treeNode);
      }
    }
    this.treeView.EndUpdate();
    if (string.IsNullOrEmpty(pattern) && string.IsNullOrEmpty(excludePattern) && string.IsNullOrEmpty(idPattern))
      return;
    this.treeView.ExpandAll();
  }

  private List<Regex> CreateRegexList(string pattern)
  {
    if (string.IsNullOrEmpty(pattern))
      return null;
    List<Regex> regexList = new List<Regex>();
    string[] parts = pattern.Split(',');
    foreach (string part in parts)
    {
      string trimmed = part.Trim();
      if (!string.IsNullOrEmpty(trimmed))
      {
        try
        {
          regexList.Add(new Regex(trimmed, RegexOptions.IgnoreCase));
        }
        catch (ArgumentException)
        {
          // Ignore invalid regex parts
        }
      }
    }
    return regexList.Count > 0 ? regexList : null;
  }

  private TreeNode FilterNode(TreeNode node, List<Regex> regexes, List<Regex> excludeRegexes, uint? id)
  {
    bool flag = true;
    if (regexes != null)
    {
      foreach (Regex regex in regexes)
      {
        if (!regex.IsMatch(node.Text))
        {
          flag = false;
          break;
        }
      }
    }
    if (flag && excludeRegexes != null)
    {
      foreach (Regex excludeRegex in excludeRegexes)
      {
        if (excludeRegex.IsMatch(node.Text))
        {
          flag = false;
          break;
        }
      }
    }
    if (flag && id.HasValue)
    {
      int objectId = this.GetObjectID(node.Tag);
      if (node.Tag == null || objectId == -1 || (long) (uint) objectId != (long) id.Value)
        flag = false;
    }
    List<TreeNode> treeNodeList = new List<TreeNode>();
    for (int index = 0; index < node.Nodes.Count; ++index)
    {
      TreeNode treeNode = this.FilterNode(node.Nodes[index], regexes, excludeRegexes, id);
      if (treeNode != null)
        treeNodeList.Add(treeNode);
    }
    if (!flag && treeNodeList.Count <= 0)
      return (TreeNode) null;
    TreeNode treeNode1 = (TreeNode) node.Clone();
    treeNode1.Nodes.Clear();
    foreach (TreeNode node1 in treeNodeList)
      treeNode1.Nodes.Add(node1);
    return treeNode1;
  }

  public FormTreeFileController()
  {
    this.InitializeComponent();
    ThemeManager.ApplyTheme(this);

    ToolTip toolTip = new ToolTip();
    toolTip.SetToolTip(this.textBoxSearch, "Search for terms separated by commas (matches items containing ALL terms)");
    toolTip.SetToolTip(this.textBoxExclude, "Exclude items containing ANY terms separated by commas");
    toolTip.SetToolTip(this.btnSelectAll, "Check all currently visible (filtered) items in the tree");
    toolTip.SetToolTip(this.btnClearAll, "Uncheck all currently visible (filtered) items in the tree");

    ImageList imageList = new ImageList();
    try
    {
      string str = Settings.DataDirectory + "icons\\";
      imageList.Images.Add(Image.FromFile(str + "object.bmp"));
      imageList.Images.Add(Image.FromFile(str + "objectpart.bmp"));
      imageList.ImageSize = new Size(16 /*0x10*/, 16 /*0x10*/);
      imageList.TransparentColor = Color.Magenta;
    }
    catch (Exception ex)
    {
    }
    this.treeView.ImageList = imageList;
  }

  [Description("Separate tree nodes with underscores")]
  [Category("Appearance")]
  [Browsable(true)]
  public bool UnderscoreSeparator
  {
    get => this.myUnderscoreSeparator;
    set => this.myUnderscoreSeparator = value;
  }

  [Category("Appearance")]
  [Browsable(true)]
  [Description("Minimum length of a tree node text")]
  public int MinTextLength
  {
    get => this.myMinTextLength;
    set => this.myMinTextLength = value;
  }

  protected virtual void ShowObject(object o)
  {
  }

  protected virtual bool DeleteObject(object o) => false;

  protected void SelectNodesByID(uint id, List<TreeNode> nodes)
  {
    int index = 0;
    while (index < this.treeView.Nodes.Count && !this.SelectNodesByID(this.treeView.Nodes[index], id, nodes))
      ++index;
  }

  protected bool SelectNodesByID(TreeNode node, uint id, List<TreeNode> nodes)
  {
    if (node.Tag != null && (long) id == (long) this.GetObjectID(node.Tag))
    {
      nodes.Add(node);
      return true;
    }
    for (int index = 0; index < node.Nodes.Count; ++index)
    {
      if (this.SelectNodesByID(node.Nodes[index], id, nodes))
        return true;
    }
    return false;
  }

  protected void LocateNodes(TreeNode root, Regex regex, List<TreeNode> nodes)
  {
    if (regex.IsMatch(root.Name))
      nodes.Add(root);
    for (int index = 0; index < root.Nodes.Count; ++index)
      this.LocateNodes(root.Nodes[index], regex, nodes);
  }

  protected void AddNode(TreeNode parent, TreeNode node)
  {
    if (this.InvokeRequired)
    {
      this.Invoke((Delegate) new FormTreeFileController.AddNodeDelegate(this.AddNode), (object) parent, (object) node);
    }
    else
    {
      if (parent == null)
      {
          this.treeView.Nodes.Add(node);
          this.myAllNodes.Add(node);
      }
      else
      {
          parent.Nodes.Add(node);
      }
    }
  }

  protected void AddNodes(TreeNode parent, TreeNode[] nodes)
  {
      if (this.InvokeRequired)
      {
          this.Invoke(new Action(() => this.AddNodes(parent, nodes)));
      }
      else
      {
          this.treeView.BeginUpdate();
          try
          {
              if (parent == null)
              {
                  this.treeView.Nodes.AddRange(nodes);
                  this.myAllNodes.AddRange(nodes);
              }
              else
              {
                  parent.Nodes.AddRange(nodes);
              }
          }
          finally
          {
              this.treeView.EndUpdate();
          }
      }
  }

  protected TreeNode AddToTreeRec(TreeNode root, string name, object o)
  {
    bool dev = false;
    if (name.IndexOf("[\\") == 0)
    {
      name = name.Substring(2);
      dev = true;
    }
    return this.AddToTreeRec(root, name, name, o, dev);
  }

  private TreeNode AddEntryToTree(TreeNode parent, string fullName, string name, object o)
  {
    int objectId = this.GetObjectID(o);
    StringBuilder stringBuilder = new StringBuilder(name);
    if (objectId > 0)
      stringBuilder.AppendFormat(" [{0}]", (object) objectId);
    TreeNode node = new TreeNode();
    node.Name = fullName;
    node.Text = stringBuilder.ToString();
    node.Tag = o;
    node.ImageIndex = 0;
    node.SelectedImageIndex = 0;
    
    // Only invoke if parent is already attached to treeView
    if (parent != null && parent.TreeView != null)
        this.AddNode(parent, node);
    else if (parent == null)
        this.AddNode(null, node);
    else
        parent.Nodes.Add(node);

    return node;
  }

  private Dictionary<TreeNode, Dictionary<string, TreeNode>> _nodeCache = new Dictionary<TreeNode, Dictionary<string, TreeNode>>();

  protected void ClearNodeCache()
  {
      _nodeCache.Clear();
  }

  protected TreeNode AddToTree(TreeNode parent, string name, object o)
  {
    return FormTreeFileController.myStaticFullNames ? this.AddEntryToTree(parent, name, name, o) : this.AddToTreeRec(parent, name, name, o, false);
  }

  private TreeNode AddToTreeRec(TreeNode root, string fullName, string name, object o, bool dev)
  {
    if (name.Length == 0)
      name = "<UNNAMED>";
    int length = 0;
    if (dev)
      length = name.IndexOf("\\");
    else if (this.UnderscoreSeparator)
      length = name.IndexOf("_");
    if (length <= 0 || this.myMinTextLength != 0 && length < this.myMinTextLength || length >= name.Length - this.myMinTextLength)
      return this.AddEntryToTree(root, fullName, name, o);
    
    string str = name.Substring(0, length);
    
    TreeNode treeNode = null;
    
    if (!_nodeCache.TryGetValue(root, out var subNodes))
    {
        subNodes = new Dictionary<string, TreeNode>();
        _nodeCache[root] = subNodes;
        // Populate cache with existing nodes if any
        foreach (TreeNode existingNode in root.Nodes)
        {
            if (existingNode.Tag == null) // Only cache structural nodes
                subNodes[existingNode.Text] = existingNode;
        }
    }

    if (!subNodes.TryGetValue(str, out treeNode))
    {
        treeNode = new TreeNode();
        treeNode.Name = str;
        treeNode.Text = str;
        treeNode.Tag = (object) null;
        treeNode.ImageIndex = 1;
        treeNode.SelectedImageIndex = 1;

        if (root != null && root.TreeView != null)
            this.AddNode(root, treeNode);
        else if (root == null)
            this.AddNode(null, treeNode);
        else
            root.Nodes.Add(treeNode);

        subNodes[str] = treeNode;
    }

    return this.AddToTreeRec(treeNode, fullName, name.Substring(length + 1), o, dev);
  }

  protected virtual int GetObjectID(object o) => -1;

  protected virtual void FindNodes(string pattern, List<TreeNode> nodes)
  {
    Regex regex = new Regex(pattern, RegexOptions.IgnoreCase);
    for (int index = 0; index < this.treeView.Nodes.Count; ++index)
      this.LocateNodes(this.treeView.Nodes[index], regex, nodes);
  }

  protected virtual bool ObjectIsModified(object o) => false;

  protected void ClearFound()
  {
    for (int index = 0; index < this.myFindNodes.Count; ++index)
      this.myFindNodes[index].BackColor = this.treeView.BackColor;
    this.myFindNodes.Clear();
  }

  protected void SetupFound()
  {
    if (this.myFindNodes.Count <= 0)
      return;
    for (int index = 0; index < this.myFindNodes.Count; ++index)
      this.myFindNodes[index].BackColor = Color.LightGray;
    this.myCurrentFind = 0;
    this.treeView.SelectedNode = this.myFindNodes[0];
    this.treeView.SelectedNode.EnsureVisible();
  }

  protected void FindByName()
  {
    FormFind formFind = new FormFind();
    if (formFind.ShowDialog() == DialogResult.OK)
    {
      this.ClearFound();
      this.FindNodes(formFind.textBoxName.Text, this.myFindNodes);
      this.SetupFound();
      this.treeView.Focus();
    }
    formFind.Dispose();
  }

  protected void FindByID()
  {
    FormFind formFind = new FormFind();
    formFind.labelInfo.Text = "ID:";
    if (formFind.ShowDialog() == DialogResult.OK)
    {
      this.ClearFound();
      this.SelectNodesByID(uint.Parse(formFind.textBoxName.Text), this.myFindNodes);
      this.SetupFound();
      this.treeView.Focus();
    }
    formFind.Dispose();
  }

  private void LocateModified(TreeNode node)
  {
    if (node.Tag != null && this.ObjectIsModified(node.Tag))
      this.myFindNodes.Add(node);
    for (int index = 0; index < node.Nodes.Count; ++index)
      this.LocateModified(node.Nodes[index]);
  }

  protected void FindModified()
  {
    this.ClearFound();
    for (int index = 0; index < this.treeView.Nodes.Count; ++index)
      this.LocateModified(this.treeView.Nodes[index]);
    if (this.myFindNodes.Count > 0)
    {
      int num = (int) FormMain.Instance.InfoMessage($"{this.myFindNodes.Count} object(s) found.");
      this.SetupFound();
    }
    else
    {
      int num1 = (int) FormMain.Instance.InfoMessage("No objects found.");
    }
  }

  private void treeView_DoubleClick(object sender, EventArgs e)
  {
    TreeNode selectedNode = this.treeView.SelectedNode;
    if (selectedNode == null || selectedNode.Tag == null)
      return;
    this.ShowObject(selectedNode.Tag);
  }

  private void treeView_KeyUp(object sender, KeyEventArgs e)
  {
    if (e.KeyCode == Keys.F3)
    {
      this.FindNext();
      e.Handled = true;
    }
    else if (e.KeyCode == Keys.Delete)
    {
      this.DeleteSelected();
      e.Handled = true;
    }
    else
      e.Handled = false;
  }

  protected void FindNext()
  {
    if (this.myFindNodes.Count <= 0)
      return;
    ++this.myCurrentFind;
    if (this.myCurrentFind == this.myFindNodes.Count)
      this.myCurrentFind = 0;
    this.treeView.SelectedNode = this.myFindNodes[this.myCurrentFind];
  }

  protected void DeleteSelected()
  {
    TreeNode selectedNode = this.treeView.SelectedNode;
    if (selectedNode == null || selectedNode.Tag == null || !this.DeleteObject(selectedNode.Tag))
      return;
    TreeNode nextNode = selectedNode.NextNode;
    this.treeView.SelectedNode = selectedNode.NextNode;
    selectedNode.Remove();
  }

  protected ContentObject CreateDragObject(TreeNode node)
  {
    return FileDatabase.Instance.GetContentObject(node.Tag);
  }

  private void treeView_ItemDrag(object sender, ItemDragEventArgs e)
  {
    if (this.treeView.SelectedNode == null || this.treeView.SelectedNode.Tag == null)
      return;
    ContentObject dragObject = this.CreateDragObject(this.treeView.SelectedNode);
    if (dragObject == null)
      return;
    int num = (int) this.DoDragDrop((object) dragObject, DragDropEffects.Copy);
  }

  protected class NodeSorter : IComparer
  {
    public int Compare(object x, object y)
    {
      TreeNode treeNode = x as TreeNode;
      return string.Compare((y as TreeNode).Name, treeNode.Name);
    }
  }

  private delegate void AddNodeDelegate(TreeNode parent, TreeNode node);
}
