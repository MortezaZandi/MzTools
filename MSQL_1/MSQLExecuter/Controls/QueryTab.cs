using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using ScintillaNET;

public class QueryTab : TabPage
{
    private ConnectionInfo _connection;
    private SplitContainer _splitContainer;
    private Scintilla _queryEditor;
    private QueryResultsViewer _resultsViewer;
    private string _queryId;
    private bool _isExecuting;
    public string FilePath { get; private set; }
    public bool HasChanges { get; set; }

    public QueryTab(ConnectionInfo connection)
    {
        _connection = connection;
        _queryId = Guid.NewGuid().ToString();
        InitializeComponents();
        InitializeQueryEditor();
    }
    
    private void InitializeComponents()
    {
        _splitContainer = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Horizontal
        };
        
        _resultsViewer = new QueryResultsViewer();
        _splitContainer.Panel2.Controls.Add(_resultsViewer);
        
        this.Controls.Add(_splitContainer);
    }
    
    private void InitializeQueryEditor()
    {
        _queryEditor = new Scintilla
        {
            Dock = DockStyle.Fill
        };

        // Configure SQL styling
        _queryEditor.LexerName = "sql";
        _queryEditor.StyleResetDefault();
        _queryEditor.Styles[Style.Default].Font = "Consolas";
        _queryEditor.Styles[Style.Default].Size = 10;
        
        // Configure SQL keywords
        _queryEditor.SetKeywords(0, 
            "SELECT INSERT UPDATE DELETE FROM WHERE GROUP BY HAVING ORDER " +
            "INNER JOIN LEFT JOIN RIGHT JOIN OUTER JOIN CROSS JOIN " +
            "CREATE ALTER DROP TRUNCATE TABLE VIEW PROCEDURE FUNCTION " +
            "AND OR NOT IN EXISTS BETWEEN LIKE IS NULL " +
            "COUNT SUM AVG MIN MAX " +
            "BEGIN END DECLARE SET " +
            "TRIGGER IDENTITY PRIMARY KEY FOREIGN REFERENCES " +
            "TRANSACTION COMMIT ROLLBACK");

        _queryEditor.SetKeywords(1,
            "int varchar nvarchar char datetime bit decimal float " +
            "money text image xml uniqueidentifier varbinary " +
            "bigint smallint tinyint date time");
        
        // Style SQL elements
        _queryEditor.Styles[Style.Sql.Comment].ForeColor = Color.Green;
        _queryEditor.Styles[Style.Sql.CommentLine].ForeColor = Color.Green;
        _queryEditor.Styles[Style.Sql.CommentDoc].ForeColor = Color.Green;
        _queryEditor.Styles[Style.Sql.Number].ForeColor = Color.Maroon;
        _queryEditor.Styles[Style.Sql.Word].ForeColor = Color.Blue;
        _queryEditor.Styles[Style.Sql.Word2].ForeColor = Color.Purple;
        _queryEditor.Styles[Style.Sql.String].ForeColor = Color.Red;
        _queryEditor.Styles[Style.Sql.Character].ForeColor = Color.Red;
        _queryEditor.Styles[Style.Sql.Operator].ForeColor = Color.Gray;
        _queryEditor.Styles[Style.Sql.Identifier].ForeColor = Color.Black;

        // Configure line numbers
        _queryEditor.Margins[0].Width = 30;
        _queryEditor.Margins[0].Type = MarginType.Number;
        _queryEditor.Margins[0].BackColor = Color.FromArgb(240, 240, 240);

        // Configure folding
        _queryEditor.Margins[2].Type = MarginType.Symbol;
        _queryEditor.Margins[2].Mask = Marker.MaskFolders;
        _queryEditor.Margins[2].Sensitive = true;
        _queryEditor.Margins[2].Width = 20;
        _queryEditor.AutomaticFold = AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change;

        // Configure selection and caret
        _queryEditor.CaretLineVisible = true;
        _queryEditor.CaretLineBackColorAlpha = 50;
        _queryEditor.CaretLineBackColor = Color.LightBlue;
        
        // Configure indentation
        _queryEditor.IndentationGuides = IndentView.LookBoth;
        _queryEditor.TabWidth = 4;
        _queryEditor.UseTabs = false;

        // Configure zooming
        _queryEditor.Zoom = 0;
        _queryEditor.ZoomChanged += (s, e) => 
        {
            _queryEditor.Margins[0].Width = 30 + (_queryEditor.Zoom * 2);
        };

        // Configure auto-complete
        _queryEditor.AutoCIgnoreCase = true;
        _queryEditor.AutoCMaxHeight = 10;
        _queryEditor.AutoCAutoHide = false;

        // Configure multiple selection
        _queryEditor.MultipleSelection = true;
        _queryEditor.AdditionalSelectionTyping = true;

        // Configure drag & drop
        _queryEditor.AllowDrop = true;

        // Configure word wrap
        _queryEditor.WrapMode = WrapMode.None;
        _queryEditor.WrapIndentMode = WrapIndentMode.Fixed;
        _queryEditor.WrapStartIndent = 4;

        // Configure scrolling
        _queryEditor.ScrollWidth = 1;
        _queryEditor.ScrollWidthTracking = true;
        _queryEditor.HScrollBar = true;
        _queryEditor.VScrollBar = true;
        
        // Track changes
        _queryEditor.TextChanged += (s, e) => 
        {
            HasChanges = true;
            UpdateLineNumbers();
        };
        
        _splitContainer.Panel1.Controls.Add(_queryEditor);

        // Initial update
        UpdateLineNumbers();
    }

    private void UpdateLineNumbers()
    {
        var lineCount = _queryEditor.Lines.Count;
        var maxLineNumberWidth = _queryEditor.TextWidth(Style.LineNumber, lineCount.ToString());
        _queryEditor.Margins[0].Width = maxLineNumberWidth + 10;
    }
    
   public async Task ExecuteQueryAsync(bool selectedOnly = false)
    {
        if (_isExecuting) return;
        
        try
        {
            _isExecuting = true;
            this.Text += " (Executing...)";
            
            var queryManager = new QueryExecutionManager();
            var queryText = selectedOnly && !string.IsNullOrWhiteSpace(_queryEditor.SelectedText) 
                ? _queryEditor.SelectedText 
                : _queryEditor.Text;
            
            var result = await queryManager.ExecuteQueryAsync(
                queryText, _connection, _queryId);
            
            _resultsViewer.DisplayResults(result);
        }
        catch (Exception ex)
        {
            LogManager.LogException(ex, "Query execution failed");
            MessageBox.Show($"Error executing query: {ex.Message}", 
                "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        finally
        {
            _isExecuting = false;
            this.Text = this.Text.Replace(" (Executing...)", "");
        }
    }
    
    public void CancelExecution()
    {
        if (_isExecuting)
        {
            var queryManager = new QueryExecutionManager();
            queryManager.CancelQuery(_queryId);
        }
    }
    
    public void CommentSelection()
    {
        var selStart = _queryEditor.SelectionStart;
        var selEnd = _queryEditor.SelectionEnd;
        var startLine = _queryEditor.LineFromPosition(selStart);
        var endLine = _queryEditor.LineFromPosition(selEnd);
        
        _queryEditor.BeginUndoAction();
        for (var i = startLine; i <= endLine; i++)
        {
            var lineStart = _queryEditor.Lines[i].Position;
            _queryEditor.InsertText(lineStart, "--");
        }
        _queryEditor.EndUndoAction();
    }
    
    public void UncommentSelection()
    {
        var selStart = _queryEditor.SelectionStart;
        var selEnd = _queryEditor.SelectionEnd;
        var startLine = _queryEditor.LineFromPosition(selStart);
        var endLine = _queryEditor.LineFromPosition(selEnd);
        
        _queryEditor.BeginUndoAction();
        for (var i = startLine; i <= endLine; i++)
        {
            var lineText = _queryEditor.Lines[i].Text;
            if (lineText.TrimStart().StartsWith("--"))
            {
                var lineStart = _queryEditor.Lines[i].Position;
                var commentPos = lineText.IndexOf("--");
                _queryEditor.DeleteRange(lineStart + commentPos, 2);
            }
        }
        _queryEditor.EndUndoAction();
    }
    
    public void SetContent(string content)
    {
        _queryEditor.Text = content;
        HasChanges = false;
        UpdateLineNumbers();
    }
    
    public string GetContent()
    {
        return _queryEditor.Text;
    }
    
    public void SetFilePath(string path)
    {
        FilePath = path;
        this.Text = Path.GetFileName(path);
    }

    public void ZoomIn()
    {
        _queryEditor.ZoomIn();
    }

    public void ZoomOut()
    {
        _queryEditor.ZoomOut();
    }

    public void ResetZoom()
    {
        _queryEditor.Zoom = 0;
    }

    public void ToggleWordWrap()
    {
        _queryEditor.WrapMode = _queryEditor.WrapMode == WrapMode.None ? 
            WrapMode.Word : WrapMode.None;
    }

    // Edit operations
    public void Undo()
    {
        if (_queryEditor.CanUndo)
            _queryEditor.Undo();
    }

    public void Redo()
    {
        if (_queryEditor.CanRedo)
            _queryEditor.Redo();
    }

    public void Cut()
    {
        if (!string.IsNullOrEmpty(_queryEditor.SelectedText))
            _queryEditor.Cut();
    }

    public void Copy()
    {
        if (!string.IsNullOrEmpty(_queryEditor.SelectedText))
            _queryEditor.Copy();
    }

    public void Paste()
    {
        if (_queryEditor.CanPaste)
            _queryEditor.Paste();
    }

    public void Find()
    {
        var findDialog = new TextInputDialog("Find", "Enter text to find:", _lastSearchText);
        if (findDialog.ShowDialog() == DialogResult.OK)
        {
            _lastSearchText = findDialog.InputText;
            
            // Set the target range for the search
            _queryEditor.TargetStart = _queryEditor.CurrentPosition;
            _queryEditor.TargetEnd = _queryEditor.TextLength;
            
            // Configure search flags
            var searchFlags = SearchFlags.None;
            if (findDialog.CaseSensitive)
                searchFlags |= SearchFlags.MatchCase;
            if (findDialog.WholeWord)
                searchFlags |= SearchFlags.WholeWord;
            if (findDialog.UseRegex)
                searchFlags |= SearchFlags.Regex;
            
            // Perform the search
            var pos = _queryEditor.SearchInTarget(_lastSearchText);
            
            if (pos >= 0)
            {
                _queryEditor.SetSelection(_queryEditor.TargetStart, _queryEditor.TargetEnd);
                _queryEditor.ScrollCaret();
            }
            else
            {
                MessageBox.Show("Text not found.", "Find",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }

    public void Replace()
    {
        var findDialog = new TextInputDialog("Replace", "Enter text to find:", _lastSearchText);
        if (findDialog.ShowDialog() == DialogResult.OK)
        {
            _lastSearchText = findDialog.InputText;
            var replaceDialog = new TextInputDialog("Replace", "Enter replacement text:", _lastReplaceText);
            
            if (replaceDialog.ShowDialog() == DialogResult.OK)
            {
                _lastReplaceText = replaceDialog.InputText;
                
                // Set the target range for the search
                _queryEditor.TargetStart = _queryEditor.CurrentPosition;
                _queryEditor.TargetEnd = _queryEditor.TextLength;
                
                // Configure search flags
                var searchFlags = SearchFlags.None;
                if (findDialog.CaseSensitive)
                    searchFlags |= SearchFlags.MatchCase;
                if (findDialog.WholeWord)
                    searchFlags |= SearchFlags.WholeWord;
                if (findDialog.UseRegex)
                    searchFlags |= SearchFlags.Regex;
                
                // Perform the search
                var pos = _queryEditor.SearchInTarget(_lastSearchText);
                
                if (pos >= 0)
                {
                    _queryEditor.ReplaceTarget(_lastReplaceText);
                    _queryEditor.SetSelection(_queryEditor.TargetStart, _queryEditor.TargetStart + _lastReplaceText.Length);
                    _queryEditor.ScrollCaret();
                }
                else
                {
                    MessageBox.Show("Text not found.", "Replace",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }

    private string _lastSearchText = "";
    private string _lastReplaceText = "";
}