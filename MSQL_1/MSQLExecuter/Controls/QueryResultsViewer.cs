using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public class QueryResultsViewer : TabControl
{
    private readonly List<DataGridView> _grids = new List<DataGridView>();
    private TextBox _messageBox;
    
    public QueryResultsViewer()
    {
        InitializeComponent();
    }
    
    private void InitializeComponent()
    {
        this.Dock = DockStyle.Fill;
        _messageBox = new TextBox
        {
            Multiline = true,
            ReadOnly = true,
            Dock = DockStyle.Fill,
            Font = new Font("Consolas", 10)
        };
    }
    
    public void DisplayResults(QueryResult result)
    {
        this.TabPages.Clear();
        _grids.Clear();
        
        if (result.HasError)
        {
            var errorPage = new TabPage("Error");
            _messageBox.Text = result.Error.ToString();
            errorPage.Controls.Add(_messageBox);
            this.TabPages.Add(errorPage);
            return;
        }
        
        for (int i = 0; i < result.ResultSets.Count; i++)
        {
            var grid = new DataGridView
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells
            };
            
            grid.DataSource = result.ResultSets[i];
            _grids.Add(grid);
            
            var page = new TabPage($"Result {i + 1} ({result.ResultSets[i].Rows.Count} rows)");
            page.Controls.Add(grid);
            this.TabPages.Add(page);
        }
        
        var messagePage = new TabPage("Messages");
        _messageBox.Text = $"Query completed in {result.ExecutionTime.TotalSeconds:F2} seconds.";
        messagePage.Controls.Add(_messageBox);
        this.TabPages.Add(messagePage);
    }
    
    public void ExportToCsv(int resultSetIndex)
    {
        if (resultSetIndex >= 0 && resultSetIndex < _grids.Count)
        {
            using (var dialog = new SaveFileDialog
            {
                Filter = "CSV files (*.csv)|*.csv",
                DefaultExt = "csv"
            })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    var grid = _grids[resultSetIndex];
                    using (var writer = new StreamWriter(dialog.FileName))
                    {
                        // Write headers
                        for (int i = 0; i < grid.Columns.Count; i++)
                        {
                            writer.Write(grid.Columns[i].HeaderText);
                            writer.Write(i < grid.Columns.Count - 1 ? "," : "\n");
                        }
                        
                        // Write data
                        foreach (DataGridViewRow row in grid.Rows)
                        {
                            for (int i = 0; i < grid.Columns.Count; i++)
                            {
                                writer.Write(row.Cells[i].Value?.ToString() ?? "");
                                writer.Write(i < grid.Columns.Count - 1 ? "," : "\n");
                            }
                        }
                    }
                }
            }
        }
    }
} 