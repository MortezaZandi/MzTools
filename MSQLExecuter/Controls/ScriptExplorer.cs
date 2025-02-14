using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Newtonsoft.Json;

public class ScriptExplorer : TreeView
{
    private List<ScriptGroup> _scriptGroups = new List<ScriptGroup>();
    private ContextMenuStrip _contextMenu;
    
    public event EventHandler<Script> ScriptSelected;
    
    public ScriptExplorer()
    {
        InitializeComponent();
    }
    
    private void InitializeComponent()
    {
        this.Dock = DockStyle.Fill;
        this.HideSelection = false;
        this.AllowDrop = true;
        
        CreateContextMenu();
        
        this.NodeMouseDoubleClick += ScriptExplorer_NodeMouseDoubleClick;
        this.DragDrop += ScriptExplorer_DragDrop;
        this.DragEnter += ScriptExplorer_DragEnter;
    }
    
    private void CreateContextMenu()
    {
        _contextMenu = new ContextMenuStrip();
        
        var addGroupMenuItem = new ToolStripMenuItem("Add Group");
        var addScriptMenuItem = new ToolStripMenuItem("Add Script");
        var editMenuItem = new ToolStripMenuItem("Edit");
        var deleteMenuItem = new ToolStripMenuItem("Delete");
        
        addGroupMenuItem.Click += (s, e) => AddNewGroup();
        addScriptMenuItem.Click += (s, e) => AddNewScript();
        editMenuItem.Click += (s, e) => EditSelectedNode();
        deleteMenuItem.Click += (s, e) => DeleteSelectedNode();
        
        _contextMenu.Items.AddRange(new ToolStripItem[]
        {
            addGroupMenuItem,
            addScriptMenuItem,
            new ToolStripSeparator(),
            editMenuItem,
            deleteMenuItem
        });
        
        this.ContextMenuStrip = _contextMenu;
    }
    
    public void LoadScriptGroups(string directoryPath)
    {
        _scriptGroups.Clear();
        this.Nodes.Clear();
        
        foreach (var file in Directory.GetFiles(directoryPath, "*.json"))
        {
            try
            {
                var group = ScriptGroup.LoadFromFile(file);
                AddScriptGroup(group);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading script group: {ex.Message}");
            }
        }
    }
    
    public void AddScriptGroup(ScriptGroup group)
    {
        _scriptGroups.Add(group);
        var groupNode = CreateGroupNode(group);
        this.Nodes.Add(groupNode);
    }
    
    private TreeNode CreateGroupNode(ScriptGroup group)
    {
        var groupNode = new TreeNode(group.Name) { Tag = group };
        
        foreach (var script in group.Scripts)
        {
            var scriptNode = new TreeNode(script.Name) { Tag = script };
            groupNode.Nodes.Add(scriptNode);
        }
        
        return groupNode;
    }
    
    private void AddNewGroup()
    {
        using (var dialog = new TextInputDialog("New Group", "Enter group name:"))
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var group = new ScriptGroup { Name = dialog.InputText };
                AddScriptGroup(group);
            }
        }
    }
    
    private void AddNewScript()
    {
        var selectedNode = this.SelectedNode;
        var group = selectedNode?.Tag as ScriptGroup ?? 
                   selectedNode?.Parent?.Tag as ScriptGroup;
        
        if (group == null) return;
        
        using (var dialog = new TextInputDialog("New Script", "Enter script name:"))
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var script = new Script 
                { 
                    Name = dialog.InputText,
                    Content = string.Empty
                };
                
                group.Scripts.Add(script);
                
                var parentNode = selectedNode?.Tag is ScriptGroup ? 
                    selectedNode : selectedNode?.Parent;
                parentNode?.Nodes.Add(new TreeNode(script.Name) { Tag = script });
            }
        }
    }
    
    private void ScriptExplorer_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
        if (e.Node.Tag is Script script)
        {
            ScriptSelected?.Invoke(this, script);
        }
    }
    
    private void ScriptExplorer_DragEnter(object sender, DragEventArgs e)
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
        {
            e.Effect = DragDropEffects.Copy;
        }
    }
    
    private void ScriptExplorer_DragDrop(object sender, DragEventArgs e)
    {
        var files = (string[])e.Data.GetData(DataFormats.FileDrop);
        foreach (var file in files)
        {
            if (Path.GetExtension(file).ToLower() == ".sql")
            {
                ImportSqlFile(file);
            }
        }
    }
    
    private void ImportSqlFile(string filePath)
    {
        var script = new Script
        {
            Name = Path.GetFileNameWithoutExtension(filePath),
            Content = File.ReadAllText(filePath)
        };
        
        // Add to "Imported Scripts" group
        var importGroup = _scriptGroups.FirstOrDefault(g => g.Name == "Imported Scripts");
        if (importGroup == null)
        {
            importGroup = new ScriptGroup { Name = "Imported Scripts" };
            AddScriptGroup(importGroup);
        }
        
        importGroup.Scripts.Add(script);
        var groupNode = this.Nodes.Cast<TreeNode>()
            .First(n => n.Tag == importGroup);
        groupNode.Nodes.Add(new TreeNode(script.Name) { Tag = script });
    }
    
    private void EditSelectedNode()
    {
        var node = this.SelectedNode;
        if (node == null) return;

        using (var dialog = new TextInputDialog("Edit Name", "Enter new name:", node.Text))
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                if (node.Tag is ScriptGroup group)
                {
                    group.Name = dialog.InputText;
                }
                else if (node.Tag is Script script)
                {
                    script.Name = dialog.InputText;
                }
                node.Text = dialog.InputText;
            }
        }
    }
    
    private void DeleteSelectedNode()
    {
        var node = this.SelectedNode;
        if (node == null) return;

        var message = $"Are you sure you want to delete '{node.Text}'?";
        if (MessageBox.Show(message, "Confirm Delete", 
            MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        {
            if (node.Tag is ScriptGroup group)
            {
                _scriptGroups.Remove(group);
            }
            else if (node.Tag is Script script && node.Parent?.Tag is ScriptGroup parentGroup)
            {
                parentGroup.Scripts.Remove(script);
            }
            node.Remove();
        }
    }
} 