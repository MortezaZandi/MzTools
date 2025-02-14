using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using System.Threading.Tasks;

public class DatabaseExplorer
{
    private TreeView _treeView;
    private ConnectionInfo _connection;

    public DatabaseExplorer(TreeView treeView)
    {
        _treeView = treeView;
        _treeView.NodeMouseDoubleClick += TreeView_NodeMouseDoubleClick;
    }

    public async Task LoadDatabases(ConnectionInfo connection)
    {
        _connection = connection;
        _treeView.Nodes.Clear();

        try
        {
            using (var conn = new SqlConnection(_connection.GetConnectionString()))
            {
                await conn.OpenAsync();
                
                // Get databases
                using (var cmd = new SqlCommand(
                    "SELECT name FROM sys.databases WHERE state = 0 ORDER BY name", conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            var dbNode = new TreeNode(reader.GetString(0))
                            {
                                Tag = "Database",
                                ImageIndex = 0
                            };
                            dbNode.Nodes.Add(new TreeNode("Loading..."));
                            _treeView.Nodes.Add(dbNode);
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading databases: {ex.Message}",
                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void TreeView_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
    {
        if (e.Node.Tag?.ToString() == "Database" && e.Node.Nodes.Count == 1 && 
            e.Node.Nodes[0].Text == "Loading...")
        {
            await LoadDatabaseObjects(e.Node);
        }
    }

    private async Task LoadDatabaseObjects(TreeNode dbNode)
    {
        try
        {
            dbNode.Nodes.Clear();

            using (var conn = new SqlConnection(_connection.GetConnectionString()))
            {
                await conn.OpenAsync();
                
                // Create main category nodes
                var tablesNode = dbNode.Nodes.Add("Tables");
                var viewsNode = dbNode.Nodes.Add("Views");
                var procsNode = dbNode.Nodes.Add("Stored Procedures");
                
                // Load Tables
                using (var cmd = new SqlCommand(
                    $"USE [{dbNode.Text}]; SELECT TABLE_NAME FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE = 'BASE TABLE' ORDER BY TABLE_NAME", conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            tablesNode.Nodes.Add(reader.GetString(0));
                        }
                    }
                }

                // Load Views
                using (var cmd = new SqlCommand(
                    $"USE [{dbNode.Text}]; SELECT TABLE_NAME FROM INFORMATION_SCHEMA.VIEWS ORDER BY TABLE_NAME", conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            viewsNode.Nodes.Add(reader.GetString(0));
                        }
                    }
                }

                // Load Stored Procedures
                using (var cmd = new SqlCommand(
                    $"USE [{dbNode.Text}]; SELECT ROUTINE_NAME FROM INFORMATION_SCHEMA.ROUTINES WHERE ROUTINE_TYPE = 'PROCEDURE' ORDER BY ROUTINE_NAME", conn))
                {
                    using (var reader = await cmd.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            procsNode.Nodes.Add(reader.GetString(0));
                        }
                    }
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error loading database objects: {ex.Message}",
                "Database Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}