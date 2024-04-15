using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FastFolder
{
    public partial class ShortcutInfoDialog : Form
    {
        public ShortcutInfoDialog()
        {
            InitializeComponent();
        }

        private Shortcut shortcut;


        public Shortcut ShortcutInfo
        {
            get { return this.shortcut; }
            set
            {
                this.shortcut = value;
                this.lblPath.Text = value.Path;
                this.txtName.Text = value.Name;
            }
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            var odlg = new FolderBrowserDialog();

            if (odlg.ShowDialog() == DialogResult.OK)
            {
                this.lblPath.Text = odlg.SelectedPath;
            }
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txtName.TextLength == 0)
            {
                MessageBox.Show("Enter shortcut name");
                return;
            }

            if (this.lblPath.Text.Length == 0)
            {
                MessageBox.Show("Enter shortcut path");
                return;
            }

            this.shortcut.Path = lblPath.Text;
            this.shortcut.Name = txtName.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
