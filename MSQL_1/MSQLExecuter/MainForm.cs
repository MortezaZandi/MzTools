using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public partial class MainForm : Form
{
    private const string DefaultTitle = "SQL Query Editor";
    private const string FileFilter = "SQL files (*.sql)|*.sql|All files (*.*)|*.*";

    public MainForm()
    {
        InitializeComponent();
        InitializeMenuItems();
        InitializeToolbar();
        UpdateTitle();
    }

    private void UpdateTitle()
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        var title = DefaultTitle;
        
        if (queryTab != null)
        {
            title = $"{(string.IsNullOrEmpty(queryTab.FilePath) ? "Untitled" : Path.GetFileName(queryTab.FilePath))} - {DefaultTitle}";
            if (queryTab.HasChanges)
                title = "*" + title;
        }
        
        this.Text = title;
    }

    // File menu handlers
    private void NewQuery_Click(object sender, EventArgs e)
    {
        var connectionDialog = new ConnectionDialog();
        if (connectionDialog.ShowDialog() == DialogResult.OK)
        {
            var queryTab = new QueryTab(connectionDialog.ConnectionInfo);
            _tabControl.TabPages.Add(queryTab);
            _tabControl.SelectedTab = queryTab;
            UpdateTitle();
        }
    }

    private void OpenFile_Click(object sender, EventArgs e)
    {
        using (var dialog = new OpenFileDialog
        {
            Filter = FileFilter,
            Multiselect = false
        })
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                var connectionDialog = new ConnectionDialog();
                if (connectionDialog.ShowDialog() == DialogResult.OK)
                {
                    var queryTab = new QueryTab(connectionDialog.ConnectionInfo);
                    queryTab.SetContent(File.ReadAllText(dialog.FileName));
                    queryTab.SetFilePath(dialog.FileName);
                    _tabControl.TabPages.Add(queryTab);
                    _tabControl.SelectedTab = queryTab;
                    UpdateTitle();
                }
            }
        }
    }

    private void SaveFile_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        if (queryTab == null) return;

        if (string.IsNullOrEmpty(queryTab.FilePath))
        {
            SaveFileAs_Click(sender, e);
            return;
        }

        File.WriteAllText(queryTab.FilePath, queryTab.GetContent());
        queryTab.HasChanges = false;
        UpdateTitle();
    }

    private void SaveFileAs_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        if (queryTab == null) return;

        using (var dialog = new SaveFileDialog
        {
            Filter = FileFilter,
            DefaultExt = "sql"
        })
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                File.WriteAllText(dialog.FileName, queryTab.GetContent());
                queryTab.SetFilePath(dialog.FileName);
                queryTab.HasChanges = false;
                UpdateTitle();
            }
        }
    }

    private void Exit_Click(object sender, EventArgs e)
    {
        Close();
    }

    // Edit menu handlers
    private void Undo_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        queryTab?.Undo();
    }

    private void Redo_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        queryTab?.Redo();
    }

    private void Cut_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        queryTab?.Cut();
    }

    private void Copy_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        queryTab?.Copy();
    }

    private void Paste_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        queryTab?.Paste();
    }

    private void Find_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        queryTab?.Find();
    }

    private void Replace_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        queryTab?.Replace();
    }

    // Query menu handlers
    private async void ExecuteQuery_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        if (queryTab != null)
        {
            _statusLabel.Text = "Executing query...";
            await queryTab.ExecuteQueryAsync();
            _statusLabel.Text = "Ready";
        }
    }

    private async void ExecuteSelectedQuery_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        if (queryTab != null)
        {
            _statusLabel.Text = "Executing selected query...";
            await queryTab.ExecuteQueryAsync(true);
            _statusLabel.Text = "Ready";
        }
    }

    private void CancelQuery_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        if (queryTab != null)
        {
            queryTab.CancelExecution();
            _statusLabel.Text = "Query cancelled";
        }
    }

    private void CommentSelection_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        queryTab?.CommentSelection();
    }

    private void UncommentSelection_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        queryTab?.UncommentSelection();
    }

    // View menu handlers
    private void ZoomIn_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        queryTab?.ZoomIn();
    }

    private void ZoomOut_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        queryTab?.ZoomOut();
    }

    private void ResetZoom_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        queryTab?.ResetZoom();
    }

    private void ToggleWordWrap_Click(object sender, EventArgs e)
    {
        var queryTab = _tabControl.SelectedTab as QueryTab;
        queryTab?.ToggleWordWrap();
    }

    // Help menu handlers
    private void About_Click(object sender, EventArgs e)
    {
        MessageBox.Show(
            "SQL Query Editor\nVersion 1.0\n\nA simple SQL query editor with execution capabilities.",
            "About SQL Query Editor",
            MessageBoxButtons.OK,
            MessageBoxIcon.Information);
    }

    // Form event handlers
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        base.OnFormClosing(e);

        // Check for unsaved changes
        foreach (QueryTab tab in _tabControl.TabPages)
        {
            if (tab.HasChanges)
            {
                var result = MessageBox.Show(
                    $"Save changes to {(string.IsNullOrEmpty(tab.FilePath) ? "untitled" : Path.GetFileName(tab.FilePath))}?",
                    "Save Changes",
                    MessageBoxButtons.YesNoCancel,
                    MessageBoxIcon.Question);

                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                if (result == DialogResult.Yes)
                {
                    _tabControl.SelectedTab = tab;
                    SaveFile_Click(this, EventArgs.Empty);
                }
            }
        }
    }
}