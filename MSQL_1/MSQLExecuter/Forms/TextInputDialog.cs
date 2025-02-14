using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;

public class TextInputDialog : Form
{
    private ComboBox _inputComboBox;
    private Button _okButton;
    private Button _findNextButton;
    private Button _replaceAllButton;
    private Button _cancelButton;
    private CheckBox _caseSensitiveCheckBox;
    private CheckBox _wholeWordCheckBox;
    private CheckBox _regexCheckBox;
    private CheckBox _selectionOnlyCheckBox;
    private RadioButton _searchUpRadio;
    private RadioButton _searchDownRadio;
    private Timer _incrementalSearchTimer;
    
    private const int MAX_HISTORY = 20;
    private static List<string> _searchHistory = new List<string>();
    private static List<string> _replaceHistory = new List<string>();
    
    public string InputText => _inputComboBox.Text;
    public bool CaseSensitive => _caseSensitiveCheckBox.Checked;
    public bool WholeWord => _wholeWordCheckBox.Checked;
    public bool UseRegex => _regexCheckBox.Checked;
    public bool SearchUp => _searchUpRadio.Checked;
    public bool SelectionOnly => _selectionOnlyCheckBox.Checked;
    
    public event EventHandler<string> IncrementalSearch;
    public event EventHandler FindNext;
    public event EventHandler<ReplaceAllEventArgs> ReplaceAll;
    
    public TextInputDialog(string title, string prompt, string defaultText = "", bool isReplace = false)
    {
        InitializeComponents(title, prompt, defaultText, isReplace);
        LoadHistory(isReplace);
    }
    
    private void InitializeComponents(string title, string prompt, string defaultText, bool isReplace)
    {
        this.Text = title;
        this.Size = new Size(500, isReplace ? 280 : 240);
        this.FormBorderStyle = FormBorderStyle.FixedDialog;
        this.StartPosition = FormStartPosition.CenterParent;
        this.MaximizeBox = false;
        this.MinimizeBox = false;
        
        var layout = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(10),
            RowCount = isReplace ? 6 : 5,
            ColumnCount = 2
        };
        
        // Search input
        layout.Controls.Add(new Label { Text = prompt, AutoSize = true }, 0, 0);
        
        _inputComboBox = new ComboBox
        {
            Width = 350,
            Text = defaultText,
            AutoCompleteMode = AutoCompleteMode.Suggest,
            AutoCompleteSource = AutoCompleteSource.ListItems
        };
        layout.Controls.Add(_inputComboBox, 1, 0);
        
        // Search options group
        var optionsGroup = new GroupBox { Text = "Search Options", Dock = DockStyle.Fill };
        var optionsLayout = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.LeftToRight };
        
        _caseSensitiveCheckBox = new CheckBox { Text = "Case sensitive", AutoSize = true };
        _wholeWordCheckBox = new CheckBox { Text = "Whole word", AutoSize = true };
        _regexCheckBox = new CheckBox { Text = "Regular expression", AutoSize = true };
        _selectionOnlyCheckBox = new CheckBox { Text = "Selection only", AutoSize = true };
        
        optionsLayout.Controls.AddRange(new Control[] 
        {
            _caseSensitiveCheckBox,
            _wholeWordCheckBox,
            _regexCheckBox,
            _selectionOnlyCheckBox
        });
        
        optionsGroup.Controls.Add(optionsLayout);
        layout.Controls.Add(optionsGroup, 0, 1);
        layout.SetColumnSpan(optionsGroup, 2);
        
        // Direction group
        var directionGroup = new GroupBox { Text = "Direction", Dock = DockStyle.Fill };
        var directionLayout = new FlowLayoutPanel { Dock = DockStyle.Fill };
        
        _searchUpRadio = new RadioButton { Text = "Up", AutoSize = true };
        _searchDownRadio = new RadioButton { Text = "Down", AutoSize = true, Checked = true };
        
        directionLayout.Controls.AddRange(new Control[] { _searchUpRadio, _searchDownRadio });
        directionGroup.Controls.Add(directionLayout);
        layout.Controls.Add(directionGroup, 0, 2);
        layout.SetColumnSpan(directionGroup, 2);
        
        // Buttons
        var buttonPanel = new FlowLayoutPanel
        {
            FlowDirection = FlowDirection.RightToLeft,
            Dock = DockStyle.Fill
        };
        
        _okButton = new Button
        {
            Text = isReplace ? "Replace" : "Find",
            DialogResult = DialogResult.OK
        };
        
        _findNextButton = new Button
        {
            Text = "Find Next",
            Width = 75
        };
        
        _cancelButton = new Button
        {
            Text = "Cancel",
            DialogResult = DialogResult.Cancel
        };
        
        var buttons = new List<Button> { _cancelButton, _okButton, _findNextButton };
        
        if (isReplace)
        {
            _replaceAllButton = new Button
            {
                Text = "Replace All",
                Width = 75
            };
            buttons.Insert(1, _replaceAllButton);
        }
        
        buttonPanel.Controls.AddRange(buttons.ToArray());
        layout.Controls.Add(buttonPanel, 0, isReplace ? 5 : 4);
        layout.SetColumnSpan(buttonPanel, 2);
        
        this.Controls.Add(layout);
        this.AcceptButton = _okButton;
        this.CancelButton = _cancelButton;
        
        // Events
        _inputComboBox.TextChanged += InputComboBox_TextChanged;
        _findNextButton.Click += (s, e) => FindNext?.Invoke(this, EventArgs.Empty);
        if (_replaceAllButton != null)
        {
            _replaceAllButton.Click += ReplaceAllButton_Click;
        }
        
        _incrementalSearchTimer = new Timer { Interval = 300 };
        _incrementalSearchTimer.Tick += IncrementalSearchTimer_Tick;
        
        _regexCheckBox.CheckedChanged += (s, e) =>
        {
            _wholeWordCheckBox.Enabled = !_regexCheckBox.Checked;
        };
    }
    
    private void InputComboBox_TextChanged(object sender, EventArgs e)
    {
        _incrementalSearchTimer.Stop();
        _incrementalSearchTimer.Start();
    }
    
    private void IncrementalSearchTimer_Tick(object sender, EventArgs e)
    {
        _incrementalSearchTimer.Stop();
        if (!string.IsNullOrEmpty(_inputComboBox.Text))
        {
            IncrementalSearch?.Invoke(this, _inputComboBox.Text);
        }
    }
    
    private void ReplaceAllButton_Click(object sender, EventArgs e)
    {
        var args = new ReplaceAllEventArgs
        {
            SearchText = _inputComboBox.Text,
            CaseSensitive = _caseSensitiveCheckBox.Checked,
            WholeWord = _wholeWordCheckBox.Checked,
            UseRegex = _regexCheckBox.Checked,
            SelectionOnly = _selectionOnlyCheckBox.Checked
        };
        
        ReplaceAll?.Invoke(this, args);
    }
    
    private void LoadHistory(bool isReplace)
    {
        var history = isReplace ? _replaceHistory : _searchHistory;
        _inputComboBox.Items.AddRange(history.ToArray());
    }
    
    public void AddToHistory(bool isReplace)
    {
        var history = isReplace ? _replaceHistory : _searchHistory;
        var text = _inputComboBox.Text;
        
        history.Remove(text); // Remove if exists
        history.Insert(0, text); // Add to top
        
        while (history.Count > MAX_HISTORY)
        {
            history.RemoveAt(history.Count - 1);
        }
    }
    
    public bool ValidateRegex()
    {
        if (_regexCheckBox.Checked)
        {
            try
            {
                new Regex(_inputComboBox.Text);
                return true;
            }
            catch (ArgumentException)
            {
                MessageBox.Show("Invalid regular expression pattern.",
                    "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        return true;
    }
}

public class ReplaceAllEventArgs : EventArgs
{
    public string SearchText { get; set; }
    public bool CaseSensitive { get; set; }
    public bool WholeWord { get; set; }
    public bool UseRegex { get; set; }
    public bool SelectionOnly { get; set; }
} 