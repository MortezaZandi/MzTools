using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using MSQLExecuter.Managers;

public partial class MainForm : Form
{
    private MenuStrip _menuStrip;
    private ToolStrip _toolStrip;
    private StatusStrip _statusStrip;
    private ToolStripStatusLabel _statusLabel;

    // Main split containers
    private SplitContainer _mainSplitContainer;
    private SplitContainer _rightSplitContainer;

    // Script Explorer (left panel)
    private TreeView _scriptExplorer;
    private ToolStrip _scriptExplorerToolStrip;

    // Query Editor (center panel)
    private TabControl _tabControl;

    // Database Explorer (right panel)
    private TreeView _databaseExplorer;
    private ToolStrip _databaseExplorerToolStrip;

    private ConnectionManager _connectionManager;
    private ToolStripComboBox _connectionsComboBox;

    private void InitializeComponent()
    {
        this.Size = new Size(1200, 800);
        this.MinimumSize = new Size(800, 600);

        // Initialize MenuStrip, ToolStrip, and StatusStrip
        InitializeTopControls();

        // Initialize Split Containers
        InitializeSplitContainers();

        // Initialize Script Explorer (left panel)
        InitializeScriptExplorer();

        // Initialize Query Editor (center panel)
        InitializeQueryEditor();

        // Initialize Database Explorer (right panel)
        InitializeDatabaseExplorer();

        // Initialize Menus
        InitializeMenuItems();

        // Set up the layout
        SetupMainLayout();

    }

    private void InitializeTopControls()
    {
        _menuStrip = new MenuStrip();
        _toolStrip = new ToolStrip();
        _statusStrip = new StatusStrip();
        _statusLabel = new ToolStripStatusLabel("Ready");
        _statusStrip.Items.Add(_statusLabel);

        // Add connection combobox to toolbar
        var connectionsLabel = new ToolStripLabel("Connection: ");
        _connectionsComboBox = new ToolStripComboBox
        {
            Width = 250,
            DropDownStyle = ComboBoxStyle.DropDownList
        };

        var newConnectionButton = new ToolStripButton
        {
            Text = "New Connection",
            Image = MSQLExecuter.Properties.Resources.NewConnection, // Add this icon to your resources
            DisplayStyle = ToolStripItemDisplayStyle.ImageAndText
        };
        newConnectionButton.Click += NewConnection_Click;

        _toolStrip.Items.AddRange(new ToolStripItem[]
        {
            connectionsLabel,
            _connectionsComboBox,
            newConnectionButton,
            new ToolStripSeparator()
        });

        // Initialize connection manager
        _connectionManager = new ConnectionManager();
        _connectionManager.ConnectionsChanged += ConnectionManager_ConnectionsChanged;
        UpdateConnectionsList();

        InitializeToolbar();

        _connectionsComboBox.SelectedIndexChanged += ConnectionComboBox_SelectedIndexChanged;
    }

    private void UpdateConnectionsList()
    {
        _connectionsComboBox.SelectedIndexChanged -= ConnectionComboBox_SelectedIndexChanged;

        _connectionsComboBox.Items.Clear();
        _connectionsComboBox.Items.AddRange(_connectionManager.Connections.ToArray());

        if (_connectionManager.CurrentConnection != null)
        {
            if (_connectionsComboBox.SelectedItem != _connectionManager.CurrentConnection)
            {
                _connectionsComboBox.SelectedItem = _connectionManager.CurrentConnection;
            }
        }
    }

    private void ConnectionManager_ConnectionsChanged(object sender, EventArgs e)
    {
        UpdateConnectionsList();
    }

    private void NewConnection_Click(object sender, EventArgs e)
    {
        using (var dialog = new ConnectionDialog())
        {
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                _connectionManager.AddConnection(dialog.ConnectionInfo);
            }
        }
    }

    private void ConnectionComboBox_SelectedIndexChanged(object sender, EventArgs e)
    {
        var connection = _connectionsComboBox.SelectedItem as ConnectionInfo;
        if (connection != null)
        {
            _connectionManager.SetCurrentConnection(connection);

            // Enable/disable New Query button based on connection status
            var newQueryButton = _toolStrip.Items.OfType<ToolStripButton>()
                .FirstOrDefault(b => b.ToolTipText?.Contains("New Query") == true);

            if (newQueryButton != null)
            {
                newQueryButton.Enabled = true;
            }
        }
    }

    private void InitializeSplitContainers()
    {
        _mainSplitContainer = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Vertical,
            Panel1MinSize = 100,
            Panel2MinSize = 100
        };

        _rightSplitContainer = new SplitContainer
        {
            Dock = DockStyle.Fill,
            Orientation = Orientation.Vertical,
            Panel1MinSize = 100,
            Panel2MinSize = 100
        };

        // Add Load event handler to set splitter distances after form is shown
        this.Load += (s, e) =>
        {
            _mainSplitContainer.SplitterDistance = 250;
            _rightSplitContainer.SplitterDistance = this.Width - 250;
        };
    }

    private void InitializeMenuItems()
    {
        // File Menu
        var fileMenu = new ToolStripMenuItem("&File");
        fileMenu.DropDownItems.AddRange(new ToolStripItem[]
        {
            new ToolStripMenuItem("&New Query", null, NewQuery_Click, Keys.Control | Keys.N),
            new ToolStripMenuItem("&Open...", null, OpenFile_Click, Keys.Control | Keys.O),
            new ToolStripMenuItem("&Save", null, SaveFile_Click, Keys.Control | Keys.S),
            new ToolStripMenuItem("Save &As...", null, SaveFileAs_Click),
            new ToolStripSeparator(),
            new ToolStripMenuItem("E&xit", null, Exit_Click, Keys.Alt | Keys.F4)
        });

        // Edit Menu
        var editMenu = new ToolStripMenuItem("&Edit");
        editMenu.DropDownItems.AddRange(new ToolStripItem[]
        {
            new ToolStripMenuItem("&Undo", null, Undo_Click, Keys.Control | Keys.Z),
            new ToolStripMenuItem("&Redo", null, Redo_Click, Keys.Control | Keys.Y),
            new ToolStripSeparator(),
            new ToolStripMenuItem("Cu&t", null, Cut_Click, Keys.Control | Keys.X),
            new ToolStripMenuItem("&Copy", null, Copy_Click, Keys.Control | Keys.C),
            new ToolStripMenuItem("&Paste", null, Paste_Click, Keys.Control | Keys.V),
            new ToolStripSeparator(),
            new ToolStripMenuItem("&Find...", null, Find_Click, Keys.Control | Keys.F),
            new ToolStripMenuItem("&Replace...", null, Replace_Click, Keys.Control | Keys.H)
        });

        // Query Menu
        var queryMenu = new ToolStripMenuItem("&Query");
        queryMenu.DropDownItems.AddRange(new ToolStripItem[]
        {
            new ToolStripMenuItem("&Execute", null, ExecuteQuery_Click, Keys.F5),
            new ToolStripMenuItem("Execute &Selected", null, ExecuteSelectedQuery_Click, Keys.F6),
            new ToolStripMenuItem("&Cancel Executing Query", null, CancelQuery_Click, Keys.F7),
            new ToolStripSeparator(),
            new ToolStripMenuItem("&Comment Selection", null, CommentSelection_Click, Keys.Control | Keys.K),
            new ToolStripMenuItem("&Uncomment Selection", null, UncommentSelection_Click, Keys.Control | Keys.U)
        });

        // View Menu
        var viewMenu = new ToolStripMenuItem("&View");
        viewMenu.DropDownItems.AddRange(new ToolStripItem[]
        {
            new ToolStripMenuItem("Zoom &In", null, ZoomIn_Click, Keys.Control | Keys.Add),
            new ToolStripMenuItem("Zoom &Out", null, ZoomOut_Click, Keys.Control | Keys.Subtract),
            new ToolStripMenuItem("&Reset Zoom", null, ResetZoom_Click, Keys.Control | Keys.D0),
            new ToolStripSeparator(),
            new ToolStripMenuItem("&Word Wrap", null, ToggleWordWrap_Click, Keys.Control | Keys.W)
        });

        // Help Menu
        var helpMenu = new ToolStripMenuItem("&Help");
        helpMenu.DropDownItems.AddRange(new ToolStripItem[]
        {
            new ToolStripMenuItem("&About", null, About_Click)
        });

        _menuStrip.Items.AddRange(new ToolStripItem[]
        {
            fileMenu,
            editMenu,
            viewMenu,
            queryMenu,
            helpMenu
        });
    }

    private void SetupMainLayout()
    {
        var mainContainer = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 4,
            ColumnCount = 1
        };

        mainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));  // MenuStrip
        mainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));  // ToolStrip
        mainContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));  // SplitContainer
        mainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));  // StatusStrip

        mainContainer.Controls.Add(_menuStrip, 0, 0);
        mainContainer.Controls.Add(_toolStrip, 0, 1);
        mainContainer.Controls.Add(_mainSplitContainer, 0, 2);
        mainContainer.Controls.Add(_statusStrip, 0, 3);

        this.Controls.Add(mainContainer);
        this.MainMenuStrip = _menuStrip;
    }

    private void InitializeScriptExplorer()
    {
        var scriptPanel = new Panel { Dock = DockStyle.Fill };

        _scriptExplorerToolStrip = new ToolStrip();
        _scriptExplorerToolStrip.Items.AddRange(new ToolStripItem[]
        {
            new ToolStripButton("New Folder", null, ScriptExplorer_NewFolder),
            new ToolStripButton("Add Scripts", null, ScriptExplorer_AddScripts),
            new ToolStripButton("Refresh", null, ScriptExplorer_Refresh)
        });

        _scriptExplorer = new TreeView
        {
            Dock = DockStyle.Fill,
            ShowLines = true,
            ShowPlusMinus = true,
            HideSelection = false
        };

        scriptPanel.Controls.Add(_scriptExplorer);
        scriptPanel.Controls.Add(_scriptExplorerToolStrip);

        _mainSplitContainer.Panel1.Controls.Add(scriptPanel);
        _mainSplitContainer.SplitterDistance = 250;
    }

    private void InitializeQueryEditor()
    {
        _tabControl = new TabControl { Dock = DockStyle.Fill };
        _tabControl.SelectedIndexChanged += (s, e) => UpdateTitle();
        _rightSplitContainer.Panel1.Controls.Add(_tabControl);
        _mainSplitContainer.Panel2.Controls.Add(_rightSplitContainer);
    }

    private void InitializeDatabaseExplorer()
    {
        var databasePanel = new Panel { Dock = DockStyle.Fill };

        _databaseExplorerToolStrip = new ToolStrip();
        _databaseExplorerToolStrip.Items.AddRange(new ToolStripItem[]
        {
            new ToolStripButton("Refresh", null, DatabaseExplorer_Refresh),
            new ToolStripButton("Filter", null, DatabaseExplorer_Filter)
        });

        _databaseExplorer = new TreeView
        {
            Dock = DockStyle.Fill,
            ShowLines = true,
            ShowPlusMinus = true,
            HideSelection = false
        };

        databasePanel.Controls.Add(_databaseExplorer);
        databasePanel.Controls.Add(_databaseExplorerToolStrip);

        _rightSplitContainer.Panel2.Controls.Add(databasePanel);
        // _rightSplitContainer.SplitterDistance = 250;
    }


    private void InitializeToolbar()
    {
        _toolStrip.Items.AddRange(new ToolStripItem[]
        {
            new ToolStripButton("New Query", null, NewQuery_Click) { ToolTipText = "New Query (Ctrl+N)" },
            new ToolStripButton("Open", null, OpenFile_Click) { ToolTipText = "Open (Ctrl+O)" },
            new ToolStripButton("Save", null, SaveFile_Click) { ToolTipText = "Save (Ctrl+S)" },
            new ToolStripSeparator(),
            new ToolStripButton("Execute", null, ExecuteQuery_Click) { ToolTipText = "Execute Query (F5)" },
            new ToolStripButton("Execute Selected", null, ExecuteSelectedQuery_Click) { ToolTipText = "Execute Selected Query (F6)" },
            new ToolStripButton("Cancel", null, CancelQuery_Click) { ToolTipText = "Cancel Executing Query (F7)" }
        });
    }

    // Event handlers for Script Explorer
    private void ScriptExplorer_NewFolder(object sender, EventArgs e)
    {
        // TODO: Implement new folder creation
    }

    private void ScriptExplorer_AddScripts(object sender, EventArgs e)
    {
        // TODO: Implement script addition
    }

    private void ScriptExplorer_Refresh(object sender, EventArgs e)
    {
        // TODO: Implement refresh
    }

    // Event handlers for Database Explorer
    private void DatabaseExplorer_Refresh(object sender, EventArgs e)
    {
        // TODO: Implement database refresh
    }

    private void DatabaseExplorer_Filter(object sender, EventArgs e)
    {
        // TODO: Implement database filtering
    }
}