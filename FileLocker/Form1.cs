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

namespace FileLocker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    txtFilePath.Text = dlg.FileName;
                }
            }
        }

        private FileStream currentFileStream;

        private void btnLock_Click(object sender, EventArgs e)
        {
            if (currentFileStream == null)
            {
                currentFileStream = new FileStream(txtFilePath.Text, FileMode.Open, FileAccess.ReadWrite, FileShare.None);
            }
            else
            {
                MessageBox.Show("File already locked");
            }
        }

        private void btnUnlock_Click(object sender, EventArgs e)
        {
            if (this.currentFileStream != null)
            {
                currentFileStream.Close();
            }
            else
            {
                MessageBox.Show("No locked file found");
            }

        }
    }
}
