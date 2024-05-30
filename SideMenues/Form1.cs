using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SideMenues
{
    public partial class Form1 : Form
    {
        private MenueListInfo menueList;
        private bool loadFinished;

        public Form1()
        {
            InitializeComponent();

            this.moveItselfWithMouse();

            this.FastFolderMenue.ItemClicked += FastFolderMenue_ItemClicked;
            this.LocationChanged += Form1_LocationChanged;
        }

        private void Form1_LocationChanged(object sender, EventArgs e)
        {
            if (loadFinished)
            {
                menueList.Location = this.Location;
                SaveData();
            }
        }

        private void FastFolderMenue_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Tag != null)
            {
                var menueInfo = e.ClickedItem.Tag as MenueItemInfo;

                if (menueInfo != null)
                {
                    OpenFolder(menueInfo);
                }
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            this.Top = 0;
            this.Left = Screen.PrimaryScreen.WorkingArea.Width / 6;
            this.Height = 2;
            this.Width = 200;

            LoadData();

            RefreshMenueItems();

            if (this.menueList.Location != Point.Empty)
            {
                this.Location = this.menueList.Location;
            }

            loadFinished = true;
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var editDialog = new MenueListEditorDialog();

            editDialog.MenuListInfo = this.menueList.CreateCopy();

            if (editDialog.ShowDialog() == DialogResult.OK)
            {
                var sortedList = editDialog.MenuListInfo.MenueList.OrderBy(i => i.Index).ToList();

                for (int i = 0; i < sortedList.Count; i++)
                {
                    sortedList[i].Index = i;
                }

                this.menueList = editDialog.MenuListInfo;

                SaveData();

                RefreshMenueItems();
            }
        }

        private void RefreshMenueItems()
        {
            this.FastFolderMenue.Items.Clear();
            foreach (var item in this.menueList.MenueList.OrderBy(i => i.Index))
            {
                if (item.IsSeparator)
                {
                    this.FastFolderMenue.Items.Add(new ToolStripSeparator());
                }
                else
                {
                    this.FastFolderMenue.Items.Add(new ToolStripMenuItem
                    {
                        Text = item.Name,
                        Tag = item,
                        BackColor = item.BackColor.ToColor(),
                        ForeColor = item.TextColor.ToColor(),
                    });
                }
            }

            this.FastFolderMenue.Items.Add(this.toolStripSeparator1);
            this.FastFolderMenue.Items.Add(this.editToolStripMenuItem);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //this.TopMost = true;
        }

        private void OpenFolder(MenueItemInfo menueItemInfo)
        {
            if (menueItemInfo != null && !menueItemInfo.IsSeparator)
            {
                var psi = new ProcessStartInfo();
                psi.FileName = "Explorer";
                psi.Arguments = menueItemInfo.Path;
                psi.WindowStyle = ProcessWindowStyle.Maximized;

                try
                {
                    Process.Start(psi);
                }
                catch (Exception ex)
                {
                    MenueListEditorDialog.Error($"Failed to open {menueItemInfo.Name} ({menueItemInfo.Path})", ex);
                }
            }
        }

        private string DefaultSaveFilePath
        {
            get
            {
                return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data.dat");
            }
        }

        private void SaveData()
        {
            try
            {
                XMLDataSerializer.Serialize(this.menueList, DefaultSaveFilePath);
            }
            catch (Exception ex)
            {
                MenueListEditorDialog.Error("Failed to save data", ex);
            }
        }

        private void LoadData()
        {
            try
            {
                this.menueList = XMLDataSerializer.Deserialize<MenueListInfo>(DefaultSaveFilePath);
            }
            catch (Exception ex)
            {
                MenueListEditorDialog.Error("Failed to load saved data", ex);
            }

            if (this.menueList == null)
            {
                this.menueList = new MenueListInfo();
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                FastFolderMenue.Show(PointToScreen(e.Location));
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            //ToggleExpansion();

        }

        private void ToggleExpansion(bool? forceExpanded = null)
        {
            this.expanded = !this.expanded;

            if (forceExpanded.HasValue)
            {
                this.expanded = forceExpanded.Value;
            }

            if (this.expanded)
            {
                this.Height = 200;
            }
            else
            {
                this.Height = 2;
            }
        }

        bool expanded;
    }
}
