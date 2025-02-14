using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

public partial class MainForm : Form
{
    private MenuStrip _menuStrip;
    private TabControl _tabControl;
    private ToolStrip _toolStrip;
    private StatusStrip _statusStrip;
    private ToolStripStatusLabel _statusLabel;

    private void InitializeComponent()
    {
        this.Size = new Size(1024, 768);
        this.MinimumSize = new Size(800, 600);

        // Initialize MenuStrip
        _menuStrip = new MenuStrip();
        var fileMenu = new ToolStripMenuItem("&File");
        var editMenu = new ToolStripMenuItem("&Edit");
        var viewMenu = new ToolStripMenuItem("&View");
        var queryMenu = new ToolStripMenuItem("&Query");
        var helpMenu = new ToolStripMenuItem("&Help");

        _menuStrip.Items.AddRange(new ToolStripItem[] 
        {
            fileMenu,
            editMenu,
            viewMenu,
            queryMenu,
            helpMenu
        });

        // Initialize ToolStrip
        _toolStrip = new ToolStrip();

        // Initialize TabControl
        _tabControl = new TabControl
        {
            Dock = DockStyle.Fill
        };

        _tabControl.SelectedIndexChanged += (s, e) => UpdateTitle();

        // Initialize StatusStrip
        _statusStrip = new StatusStrip();
        _statusLabel = new ToolStripStatusLabel("Ready");
        _statusStrip.Items.Add(_statusLabel);

        // Layout
        var mainContainer = new TableLayoutPanel
        {
            Dock = DockStyle.Fill,
            RowCount = 4,
            ColumnCount = 1
        };

        mainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));  // MenuStrip
        mainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));  // ToolStrip
        mainContainer.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));  // TabControl
        mainContainer.RowStyles.Add(new RowStyle(SizeType.AutoSize));  // StatusStrip

        mainContainer.Controls.Add(_menuStrip, 0, 0);
        mainContainer.Controls.Add(_toolStrip, 0, 1);
        mainContainer.Controls.Add(_tabControl, 0, 2);
        mainContainer.Controls.Add(_statusStrip, 0, 3);

        this.Controls.Add(mainContainer);
        this.MainMenuStrip = _menuStrip;
    }

    private void InitializeMenuItems()
    {
        // File Menu
        var fileMenu = _menuStrip.Items[0] as ToolStripMenuItem;
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
        var editMenu = _menuStrip.Items[1] as ToolStripMenuItem;
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

        // View Menu
        var viewMenu = _menuStrip.Items[2] as ToolStripMenuItem;
        viewMenu.DropDownItems.AddRange(new ToolStripItem[]
        {
            new ToolStripMenuItem("Zoom &In", null, ZoomIn_Click, Keys.Control | Keys.Add),
            new ToolStripMenuItem("Zoom &Out", null, ZoomOut_Click, Keys.Control | Keys.Subtract),
            new ToolStripMenuItem("&Reset Zoom", null, ResetZoom_Click, Keys.Control | Keys.D0),
            new ToolStripSeparator(),
            new ToolStripMenuItem("&Word Wrap", null, ToggleWordWrap_Click, Keys.Control | Keys.W)
        });

        // Query Menu
        var queryMenu = _menuStrip.Items[3] as ToolStripMenuItem;
        queryMenu.DropDownItems.AddRange(new ToolStripItem[]
        {
            new ToolStripMenuItem("&Execute", null, ExecuteQuery_Click, Keys.F5),
            new ToolStripMenuItem("Execute &Selected", null, ExecuteSelectedQuery_Click, Keys.F6),
            new ToolStripMenuItem("&Cancel Executing Query", null, CancelQuery_Click, Keys.F7),
            new ToolStripSeparator(),
            new ToolStripMenuItem("&Comment Selection", null, CommentSelection_Click, Keys.Control | Keys.K),
            new ToolStripMenuItem("&Uncomment Selection", null, UncommentSelection_Click, Keys.Control | Keys.U)
        });

        // Help Menu
        var helpMenu = _menuStrip.Items[4] as ToolStripMenuItem;
        helpMenu.DropDownItems.AddRange(new ToolStripItem[]
        {
            new ToolStripMenuItem("&About", null, About_Click)
        });
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
} 