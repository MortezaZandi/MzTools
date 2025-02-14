using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;

public class DatabaseExplorer : TreeView
{
    private ConnectionInfo _connection;
    private ImageList _imageList;
    
    public DatabaseExplorer()
    {
        InitializeComponent();
    }
    
    private void InitializeComponent()
    {
        _imageList = new ImageList();
        // Add your icons here
        // _imageList.Images.Add("database", Resources.DatabaseIcon);
        // _imageList.Images.Add("table", Resources.TableIcon);
        // _imageList.Images.Add("procedure", Resources.ProcedureIcon);
        
        this.ImageList = _imageList;
        this.Dock = DockStyle.Fill;
        this.HideSelection = false;
        
        this.BeforeExpand += DatabaseExplorer_BeforeExpand;
    }
    
    public async Task SetConnection(ConnectionInfo connection)
    {
        _connection = connection;
        await RefreshDatabases();
    }
    
    public async Task RefreshDatabases()
    {
        this.Nodes.Clear();
        
        if (_connection == null) return;
        
        try
        {
            using (var conn = _connection.CreateConnection())
            {
                await conn.OpenAsync();
                
                // Get all databases
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "SELECT name FROM sys.databases WHERE database_id > 4 ORDER BY name";
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var dbNode = new TreeNode(reader.GetString(0))
                            {
                                ImageKey = "database",
                                SelectedImageKey = "database"
                            };
                            
                            // Add placeholder node to enable expansion
                            dbNode.Nodes.Add(new TreeNode("Loading..."));
                            this.Nodes.Add(dbNode);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading databases: {ex.Message}", "Error", 
                          MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
    
    private async void DatabaseExplorer_BeforeExpand(object sender, TreeViewCancelEventArgs e)
    {
        var node = e.Node;
        
        if (node.Nodes.Count == 1 && node.Nodes[0].Text == "Loading...")
        {
            node.Nodes.Clear();
            await LoadDatabaseObjects(node);
        }
    }
    
    private async Task LoadDatabaseObjects(TreeNode dbNode)
    {
        try
        {
            using (var conn = _connection.CreateConnection())
            {
                await conn.OpenAsync();
                
                // Change database context
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = $"USE [{dbNode.Text}]";
                    await cmd.ExecuteNonQueryAsync();
                }
                
                // Add Tables node
                var tablesNode = new TreeNode("Tables")
                {
                    ImageKey = "folder",
                    SelectedImageKey = "folder"
                };
                dbNode.Nodes.Add(tablesNode);
                
                // Get tables
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT TABLE_NAME 
                        FROM INFORMATION_SCHEMA.TABLES 
                        WHERE TABLE_TYPE = 'BASE TABLE'
                        ORDER BY TABLE_NAME";
                    
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tablesNode.Nodes.Add(new TreeNode(reader.GetString(0))
                            {
                                ImageKey = "table",
                                SelectedImageKey = "table"
                            });
                        }
                    }
                }
                
                // Add Stored Procedures node
                var procsNode = new TreeNode("Stored Procedures")
                {
                    ImageKey = "folder",
                    SelectedImageKey = "folder"
                };
                dbNode.Nodes.Add(procsNode);
                
                // Get stored procedures
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = @"
                        SELECT ROUTINE_NAME 
                        FROM INFORMATION_SCHEMA.ROUTINES 
                        WHERE ROUTINE_TYPE = 'PROCEDURE'
                        ORDER BY ROUTINE_NAME";
                    
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            procsNode.Nodes.Add(new TreeNode(reader.GetString(0))
                            {
                                ImageKey = "procedure",
                                SelectedImageKey = "procedure"
                            });
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading database objects: {ex.Message}", 
                          "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}