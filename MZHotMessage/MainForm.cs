using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

public partial class MainForm : Form
{
    private readonly List<SqlServerInfo> _servers = new List<SqlServerInfo>();
    private CancellationTokenSource _cts = new CancellationTokenSource();

    public MainForm()
    {
        InitializeComponent();

        //AppDomain.CurrentDomain.FirstChanceException += CurrentDomain_FirstChanceException;

        LoadServers();

        SetupControls();
    }

    private void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e)
    {
        var sb = new StringBuilder();
        ExtractExMessage(e.Exception, sb);
        var message = sb.ToString();
        MessageBox.Show(message + Environment.NewLine + e.Exception.StackTrace);
    }

    private void ExtractExMessage(Exception ex, StringBuilder sb)
    {
        if (ex != null)
        {
            sb.AppendLine(ex.Message);

            ExtractExMessage(ex.InnerException, sb);
        }
    }

    private void SetupControls()
    {
        // Configure CheckedListBox
        checkedListServers.CheckOnClick = true;

        // Configure Buttons
        btnSelectAll.Click += (s, e) =>
        {
            for (int i = 0; i < checkedListServers.Items.Count; i++)
                checkedListServers.SetItemChecked(i, true);
        };

        btnDeselectAll.Click += (s, e) =>
        {
            for (int i = 0; i < checkedListServers.Items.Count; i++)
                checkedListServers.SetItemChecked(i, false);
        };

        btnExecute.Click += BtnExecute_Click;
        btnCancel.Click += (s, e) => _cts.Cancel();

        // Configure TextBox
        txtQuery.Text = "SELECT TOP 1 ZQ_DN_VAL FROM ZQ_CO_STRGRP_CFG Where ZQ_DS_NM = 'CMS_VERSION'";
        txtQuery.Multiline = true;
        txtQuery.Height = 60;
    }

    private async void BtnExecute_Click(object sender, EventArgs e)
    {
        if (checkedListServers.CheckedItems.Count == 0)
        {
            MessageBox.Show("Please select at least one server.");
            return;
        }

        btnExecute.Enabled = false;
        btnCancel.Enabled = true;
        progressBar.Value = 0;
        listResults.Items.Clear();
        _cts = new CancellationTokenSource();

        try
        {
            var selectedServers = checkedListServers.CheckedItems.Cast<SqlServerInfo>().ToList();
            progressBar.Maximum = selectedServers.Count;

            // Process servers in batches of 10
            for (int i = 0; i < selectedServers.Count; i += 10)
            {
                var batch = selectedServers.Skip(i).Take(10);
                var tasks = batch.Select(server => ExecuteQueryAsync(server, txtQuery.Text, _cts.Token));

                var results = await Task.WhenAll(tasks);

                foreach (var result in results)
                {
                    AddResult(result);
                    progressBar.Value = Math.Min(progressBar.Value + 1, progressBar.Maximum);
                }
            }
        }
        catch (OperationCanceledException)
        {
            AddResult(new QueryResult { ServerName = "Operation", Message = "Query execution cancelled" });
        }
        finally
        {
            btnExecute.Enabled = true;
            btnCancel.Enabled = false;
        }
    }

    private void AddResult(QueryResult result)
    {
        if (listResults.InvokeRequired)
        {
            listResults.Invoke(new Action(() => AddResult(result)));
            return;
        }

        var item = new ListViewItem(result.ServerName);
        item.SubItems.Add(result.Version ?? "N/A");
        item.SubItems.Add(result.Message ?? "Success");
        try
        {
            item.SubItems.Add(result.ExecutionTime.ToString("mm:ss.fff"));
        }
        catch (Exception)
        {
        }
        listResults.Items.Add(item);
    }

    private async Task<QueryResult> ExecuteQueryAsync(SqlServerInfo server, string query, CancellationToken ct)
    {
        var result = new QueryResult { ServerName = server.ServerName };
        var sw = Stopwatch.StartNew();

        try
        {
            using (var connection = new SqlConnection(server.GetConnectionString()))
            {
                await connection.OpenAsync(ct);

                using (var command = new SqlCommand(query, connection))
                {
                    command.CommandTimeout = 3; // 3 seconds timeout
                    var value = await command.ExecuteScalarAsync(ct);

                    result.Version = value?.ToString();
                }
            }
        }
        catch (Exception ex)
        {
            result.Message = ex.Message;
        }
        finally
        {
            sw.Stop();
            result.ExecutionTime = sw.Elapsed;
        }

        return result;
    }

    private void LoadServers()
    {
        using (var connection = new SqlConnection("Data Source=172.17.0.21;Initial Catalog=CMSDB;Integrated Security=False;Persist Security Info=False;User ID=sa;Password=KY$0ftP@$$;Pooling=False;Max Pool Size=200;Packet Size=4096;Workstation ID=KYAN-CON-BL"))
        {
            using (var command = new SqlCommand("SELECT ServerName, Instance, CatalogName, UserID, Password FROM ConnectionString", connection))
            {
                connection.Open();
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var server = new SqlServerInfo
                        {
                            ServerName = reader.GetString(0),
                            Instance = reader.IsDBNull(1) ? null : reader.GetString(1),
                            CatalogName = reader.GetString(2),
                            UserId = reader.GetString(3),
                            Password = reader.GetString(4)
                        };

                        _servers.Add(server);
                        checkedListServers.Items.Add(server);
                    }
                }
            }
        }
    }
}