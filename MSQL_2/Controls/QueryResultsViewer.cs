using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data.SqlTypes;
using MSQLExecuter.Models;  // Add this line

public class QueryResultsViewer : UserControl
{
    private DataGridView _gridView;

    public QueryResultsViewer()
    {
        InitializeComponents();
    }

    private void InitializeComponents()
    {
        _gridView = new DataGridView
        {
            Dock = DockStyle.Fill,
            AllowUserToAddRows = false,
            AllowUserToDeleteRows = false,
            ReadOnly = true,
            AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells,
            AutoGenerateColumns = false,
            ClipboardCopyMode = DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText
        };

        this.Controls.Add(_gridView);
    }

    public void DisplayResults(QueryResult results)
    {
        if (results == null)
        {
            _gridView.DataSource = null;
            return;
        }

        try
        {
            _gridView.Columns.Clear();

            // Add only non-binary columns
            for (int i = 0; i < results.Columns.Count; i++)
            {
                var column = results.Columns[i];
                if (!IsBinaryColumn(column.Type))
                {
                    _gridView.Columns.Add(new DataGridViewTextBoxColumn
                    {
                        Name = column.Name,
                        HeaderText = column.Name,
                        DataPropertyName = column.Name
                    });
                }
            }

            // Create a DataTable to bind the results
            var dataTable = new DataTable();
            
            // Add columns
            foreach (var column in results.Columns)
            {
                if (!IsBinaryColumn(column.Type))
                {
                    dataTable.Columns.Add(new DataColumn(column.Name, column.Type));
                }
            }
            
            // Add rows
            foreach (var row in results.Rows)
            {
                var dataRow = dataTable.NewRow();
                int dataColumnIndex = 0;
                
                for (int i = 0; i < results.Columns.Count; i++)
                {
                    if (!IsBinaryColumn(results.Columns[i].Type))
                    {
                        dataRow[dataColumnIndex++] = row[i] ?? DBNull.Value;
                    }
                }
                
                dataTable.Rows.Add(dataRow);
            }

            _gridView.DataSource = dataTable;

            // Auto-size columns but with a maximum width
            const int maxColumnWidth = 300;
            foreach (DataGridViewColumn column in _gridView.Columns)
            {
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
                if (column.Width > maxColumnWidth)
                {
                    column.Width = maxColumnWidth;
                }
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Error displaying results: {ex.Message}",
                "Display Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private bool IsBinaryColumn(Type columnType)
    {
        return columnType == typeof(byte[]) ||
               columnType == typeof(Image) ||
               columnType == typeof(Bitmap) ||
               columnType == typeof(SqlBinary);
    }
} 