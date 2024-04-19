using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SideMenues
{
    public partial class MenueListEditorDialog : Form
    {
        private MenueListInfo menueList;

        public MenueListEditorDialog()
        {
            InitializeComponent();

            this.menueList = new MenueListInfo();
        }

        public MenueListInfo MenuListInfo
        {
            get
            {
                var result = new MenueListInfo();

                foreach (ListViewItem item in this.listView1.Items)
                {
                    result.MenueList.Add(item.Tag as MenueItemInfo);
                }

                return result;
            }
            set
            {
                this.menueList = value;
                RefreshList();
            }
        }

        private void RefreshList()
        {
            this.listView1.Items.Clear();

            foreach (var item in this.menueList.MenueList.OrderBy(i => i.Index))
            {
                this.listView1.Items.Add(new ListViewItem()
                {
                    Name = item.IsSeparator ? new string('-', 10) : item.Name,
                    Text = item.Name,
                    Tag = item,
                    BackColor = item.BackColor.ToColor(),
                    ForeColor = item.TextColor.ToColor(),
                });
            }
        }

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var si = SelectedItem;

            if (si != null)
            {
                this.txtSelectedPath.Text = si.Path;
                this.btnBackColor.BackColor = si.BackColor.ToColor();
                this.btnTextColor.BackColor = si.TextColor.ToColor();
                this.txtName.Text = si.Name;
                this.chkIsSeparator.Checked = si.IsSeparator;
            }
            else
            {
                this.txtSelectedPath.Text = string.Empty;
                this.btnBackColor.BackColor = this.BackColor;
                this.btnTextColor.BackColor = this.BackColor;
                this.chkIsSeparator.Checked = false;
                this.txtName.Clear();
            }
        }


        private MenueItemInfo SelectedItem
        {
            get
            {
                if (listView1.SelectedItems.Count > 0)
                {
                    return listView1.SelectedItems[0].Tag as MenueItemInfo;
                }
                else
                {
                    return null;
                }
            }
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            var si = SelectedItem;
            if (si != null)
            {
                si.Index--;

                RefreshList();

                SetSelectedItem(si);
            }

        }

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            var si = SelectedItem;
            if (si != null)
            {
                si.Index++;

                RefreshList();

                SetSelectedItem(si);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var si = SelectedItem;
            if (si != null)
            {
                this.menueList.MenueList.Remove(si);

                RefreshList();
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (ValidateUserInputs(true))
            {
                var biggerIndex = 0;

                if (this.menueList.MenueList.Count > 0)
                {
                    biggerIndex = this.menueList.MenueList.Max(i => i.Index);
                }

                this.menueList.MenueList.Add(new MenueItemInfo
                {
                    Name = txtName.Text,
                    Path = this.txtSelectedPath.Text,
                    Index = biggerIndex + 1,
                    BackColor = btnBackColor.BackColor.ToArgb(),
                    TextColor = btnTextColor.BackColor.ToArgb(),
                    IsSeparator = chkIsSeparator.Checked,
                });

                txtName.Clear();
                txtSelectedPath.Text = string.Empty;

                RefreshList();
            }
        }

        private bool ValidateUserInputs(bool isNew)
        {
            if (chkIsSeparator.Checked)
            {
                return true;
            }

            if (txtName.Text == string.Empty)
            {
                Error("Name is empty");

                return false;
            }

            if (this.txtSelectedPath.Text == string.Empty)
            {
                Error("Path is not selected");

                return false;
            }

            var similarName = this.menueList.MenueList.FirstOrDefault(i => i.Name.ToLower() == this.txtName.Text.ToLower());
            var similarPath = this.menueList.MenueList.FirstOrDefault(i => i.Path.ToLower() == this.txtSelectedPath.Text.ToLower());

            if (similarName != null)
            {
                if (isNew || similarName != SelectedItem)
                {
                    Error("This name already exists in list");

                    return false;
                }
            }

            if (isNew || similarPath != SelectedItem)
            {
                if (similarPath != null)
                {
                    Error("This path already exists in lists");

                    return false;
                }
            }

            if (!Directory.Exists(this.txtSelectedPath.Text) && !File.Exists(this.txtSelectedPath.Text))
            {
                Error("Path not found");

                return false;
            }

            return true;
        }

        public static void Error(string ms, Exception ex = null)
        {
            if (ex == null)
            {
                MessageBox.Show(ms, "Side Menue", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                MenueListEditorDialog.Error(ms + Environment.NewLine + ex.Message);
            }
        }
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var fdlg = new FolderBrowserDialog();

            if (this.txtSelectedPath.Text.Length > 0)
            {
                fdlg.SelectedPath = this.txtSelectedPath.Text;
            }

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                txtSelectedPath.Text = fdlg.SelectedPath;

                if (txtName.TextLength == 0)
                {
                    txtName.Text = Path.GetFileNameWithoutExtension(fdlg.SelectedPath);
                }
            }
        }

        private void SetSelectedItem(MenueItemInfo menueItem)
        {
            foreach (ListViewItem item in listView1.Items)
            {
                if (item.Text == menueItem.Name)
                {
                    listView1.Focus();
                    item.Selected = true;
                    break;
                }
            }
        }

        private void btnBackColor_Click(object sender, EventArgs e)
        {
            var newBackColor = EditColor(btnBackColor.BackColor);

            btnBackColor.BackColor = newBackColor;
        }

        private Color EditColor(Color currentColor)
        {
            var cdlg = new ColorDialog();
            cdlg.Color = currentColor;
            cdlg.AllowFullOpen = true;

            if (cdlg.ShowDialog() == DialogResult.OK)
            {
                return cdlg.Color;
            }

            return currentColor;
        }

        private void btnTextColor_Click(object sender, EventArgs e)
        {
            var newTextColor = EditColor(btnTextColor.BackColor);

            btnTextColor.BackColor = newTextColor;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            var selectedItem = SelectedItem;

            if (selectedItem != null)
            {
                if (ValidateUserInputs(false))
                {
                    selectedItem.Name = txtName.Text;
                    selectedItem.Path = txtSelectedPath.Text;
                    selectedItem.BackColor = btnBackColor.BackColor.ToArgb();
                    selectedItem.TextColor = btnTextColor.BackColor.ToArgb();
                    selectedItem.IsSeparator = chkIsSeparator.Checked;
                    RefreshList();

                    SetSelectedItem(selectedItem);
                }
            }
        }
    }
}
