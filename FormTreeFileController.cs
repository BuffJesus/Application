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

  protected override void Dispose(bool disposing)
  {
    if (disposing && this.components != null)
      this.components.Dispose();
    base.Dispose(disposing);
  }

  private void InitializeComponent()
  {
    this.treeView = new TreeView();
    this.SuspendLayout();
    this.treeView.Dock = DockStyle.Fill;
    this.treeView.HideSelection = false;
    this.treeView.Location = new Point(0, 0);
    this.treeView.Name = "treeView";
    this.treeView.Size = new Size(482, 390);
    this.treeView.TabIndex = 0;
    this.treeView.DoubleClick += new EventHandler(this.treeView_DoubleClick);
    this.treeView.KeyUp += new KeyEventHandler(this.treeView_KeyUp);
    this.treeView.ItemDrag += new ItemDragEventHandler(this.treeView_ItemDrag);
    this.AutoScaleDimensions = new SizeF(6f, 13f);
    this.AutoScaleMode = AutoScaleMode.Font;
    this.ClientSize = new Size(482, 390);
    this.Controls.Add((System.Windows.Forms.Control) this.treeView);
    this.Name = nameof (FormTreeFileController);
    this.Text = nameof (FormTreeFileController);
    this.ResumeLayout(false);
  }

  public FormTreeFileController()
  {
    this.InitializeComponent();
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
      this.Invoke((Delegate) new FormTreeFileController.AddNodeDelegate(this.AddNode), (object) parent, (object) node);
    else if (parent == null)
      this.treeView.Nodes.Add(node);
    else
      parent.Nodes.Add(node);
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
    this.AddNode(parent, node);
    return node;
  }

  protected TreeNode AddToTree(TreeNode parent, string name, object o)
  {
    return FormTreeFileController.myStaticFullNames ? this.AddEntryToTree(parent, name, name, o) : this.AddToTreeRec(parent, name, o);
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
    for (int index = 0; index < root.Nodes.Count; ++index)
    {
      if (root.Nodes[index].Text == str && root.Nodes[index].Tag == null)
        return this.AddToTreeRec(root.Nodes[index], fullName, name.Substring(length + 1), o, dev);
    }
    TreeNode treeNode = new TreeNode();
    treeNode.Name = str;
    treeNode.Text = str;
    treeNode.Tag = (object) null;
    treeNode.ImageIndex = 1;
    treeNode.SelectedImageIndex = 1;
    this.AddNode(root, treeNode);
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
      if (this.myFindNodes.Count == 0)
        this.FindByName();
      else
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
