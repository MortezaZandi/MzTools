using System;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFolder
{
    public partial class Form1 : Form
    {
        private ShortcutsContainer shortcutsContainer;
        private Button newButton;
        private bool expanded;

        public Form1()
        {
            InitializeComponent();

            this.Click += Form1_Click;
            this.MouseLeave += Form1_MouseLeave;
        }

        private void Form1_MouseLeave(object sender, EventArgs e)
        {
            if (!this.ClientRectangle.Contains(this.PointToClient(MousePosition)))
            {
                Colapse();
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            Expand();
        }

        private void Colapse()
        {
            ShowHideControls(false);
            this.BackColor = Color.Peru;
            this.ClientSize = new Size(this.ClientSize.Width, 2);
            this.expanded = false;
        }

        private void Expand()
        {
            ShowHideControls(true);
            ResizeControls();
            this.BackColor = Color.Peru;
            this.ClientSize = this.ExpandedSize;
            this.expanded = true;
        }

        private void ShowHideControls(bool show)
        {
            foreach (Control c in this.Controls)
            {
                if (c is Button)
                {
                    c.Visible = show;
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            LoadShortcutsFromFile();

            ShowShortcutList();

            SetLocation();

            ResizeControls();

            ShowShortcutList();

            Colapse();

            TopMost = true;
        }


        private void SetLocation()
        {
            var location = new Point(Screen.PrimaryScreen.WorkingArea.Width / 4 - this.Width, 0); ;

            if (location.X < 0)
            {
                location = new Point(100, 0);
            }

            this.Location = location;
        }

        private void ResizeControls()
        {
            this.ExpandedSize = new Size(200, 30);

            if (this.shortcutsContainer.Shortcuts.Count > 0)
            {
                this.ExpandedSize = new Size(200, this.shortcutsContainer.Shortcuts.Count * (30 + 6) + 10);
            }

            this.ClientSize = this.ExpandedSize;
        }

        private Size DefaultShortcutControlSize
        {
            get
            {
                return new Size(this.Width - 6, 25);
            }
        }

        private Font DefaultShortcutControlFont
        {
            get
            {
                return new Font("Tahoma", 10);
            }
        }

        private string GetDefaultSaveFilePath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "shortcuts.dat");
            }
        }

        public Size ExpandedSize { get; private set; } = new Size(200, 30);

        private void LoadShortcutsFromFile()
        {
            try
            {
                this.shortcutsContainer = new ShortcutsContainer();

                if (File.Exists(GetDefaultSaveFilePath))
                {
                    this.shortcutsContainer = XMLSerializer.Deserialize<ShortcutsContainer>(GetDefaultSaveFilePath);
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        private void SaveShortcutsToFile()
        {
            try
            {
                XMLSerializer.Serialize(this.shortcutsContainer, GetDefaultSaveFilePath);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddShortcutToContainer(string name, string path)
        {
            if (!string.IsNullOrWhiteSpace(path))
            {
                if (string.IsNullOrEmpty(name))
                {
                    name = Path.GetFileNameWithoutExtension(path);
                }

                int index = this.shortcutsContainer.Shortcuts.Count;

                this.shortcutsContainer.Shortcuts.Add(new Shortcut()
                {
                    Name = name,
                    Path = path,
                    SortIndex = index,
                    UsageCount = 0
                });
            }
            else
            {
                throw new ApplicationException("Path is empty");
            }
        }

        private void ShowShortcutList()
        {
            this.Controls.Clear();
            int top = 5;

            if (this.shortcutsContainer.Shortcuts.Count > 0)
            {
                foreach (var item in this.shortcutsContainer.Shortcuts)
                {
                    Control c = CreateShortcutControl(item);
                    c.Top = top;
                    c.Left = 3;
                    c.Width = this.Width - 6;
                    top += c.Height + 5;
                    this.Controls.Add(c);
                }
            }
            else
            {
                newButton = new Button
                {
                    Location = new Point(3, 5),
                    Text = "Create New Shortcut",
                    Size = DefaultShortcutControlSize,
                    Font = DefaultShortcutControlFont,
                };

                newButton.Click += (s, e) =>
                {
                    OnNewShortcutCliecked();
                };

                this.Controls.Add(newButton);
            }

            this.ExpandedSize = new Size(this.Width, top - 30);
            this.ClientSize = this.ExpandedSize;
        }

        private Control CreateShortcutControl(Shortcut item)
        {
            var btn = new Button()
            {
                FlatStyle = FlatStyle.Flat,
                Text = item.Name,
                Tag = item,
                Font = DefaultShortcutControlFont,
                ForeColor = Color.Black,
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(255, 215, 160, 105);

            btn.Click += (s, e) =>
            {
                var b = (Button)s;

                OnShortcutClicked(b.Tag as Shortcut);
            };

            btn.ContextMenuStrip = this.shortcutsMenuStrip;

            return btn;
        }

        private void OnShortcutClicked(Shortcut shortcut)
        {
            Colapse();

            var psi = new ProcessStartInfo();
            psi.FileName = "Explorer.exe";
            psi.Arguments = shortcut.Path;
            psi.WindowStyle = ProcessWindowStyle.Maximized;

            Process.Start(psi);
        }

        public enum LocationTypes
        {
            None,
            Top,
            Bottom,
            Left,
            Right,
        }

        private void ShortcutsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var clickedItem = (ToolStripMenuItem)sender;

            if (clickedItem == newToolStripMenuItem)
            {
                OnNewShortcutCliecked();
            }
            else if (clickedItem == openToolStripMenuItem)
            {

            }
            else if (clickedItem == deleteToolStripMenuItem)
            {

            }
            else if (clickedItem == moveUpToolStripMenuItem)
            {

            }
            else if (clickedItem == moveDownToolStripMenuItem)
            {

            }
            else
            {
                throw new ApplicationException("menue item was not handled");
            }
        }

        private void OnNewShortcutCliecked()
        {
            Shortcut newShortcut = GetNewShortcutInfoFromUser();

            if (newShortcut != null)
            {
                AddShortcutToContainer(newShortcut.Name, newShortcut.Path);

                SaveShortcutsToFile();

                ShowShortcutList();
            }
        }

        private Shortcut GetNewShortcutInfoFromUser()
        {
            var sdlg = new ShortcutInfoDialog();

            sdlg.ShortcutInfo = new Shortcut();

            if (sdlg.ShowDialog() == DialogResult.OK)
            {
                return sdlg.ShortcutInfo;
            }

            return null;
        }

        private void flowLayoutPanel1_Click(object sender, EventArgs e)
        {
            Colapse();
        }

        private void tmrCursorWatcher_Tick(object sender, EventArgs e)
        {
            if (!this.ClientRectangle.Contains(this.PointToClient(Cursor.Position)))
            {
                if (this.expanded)
                {
                    Colapse();
                }
            }
        }
    }
}
