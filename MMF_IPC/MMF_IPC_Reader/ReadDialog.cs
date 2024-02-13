using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO.MemoryMappedFiles;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using MMF_IPC;

namespace MMF_IPC_Reader
{
    public partial class ReadDialog : Form
    {
        private MMF mmf;

        private EventWaitHandle activationSignal;

        public ReadDialog()
        {
            InitializeComponent();

            try
            {
                this.activationSignal = new EventWaitHandle(false, EventResetMode.ManualReset, "LogReaderActivated");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to create activation signal" + Environment.NewLine + ex.Message, "MMF-Reader", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            try
            {
                this.mmf = new MMF("Logs", 1024);
                this.mmf.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Failed to open memory log file" + Environment.NewLine + ex.Message, "MMF-Reader", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            textBox1.Text = mmf.Read();
        }

        private void chkActivateReader_CheckedChanged(object sender, EventArgs e)
        {
            if (chkActivateReader.Checked)
            {
                this.activationSignal.Set();
            }
            else
            {
                this.activationSignal.Reset();
            }
        }
    }
}
