using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using MZCryptoTools;

namespace MMF_IPC
{
    public partial class WriteDialog : Form
    {
        private MMF mmf;
        public WriteDialog()
        {
            InitializeComponent();
            mmf = new MMF("Logs", 10);
            mmf.Create();
        }

        private bool IsLogReaderAvailable()
        {
            EventWaitHandle ewh;
            bool ev = EventWaitHandle.TryOpenExisting("LogReaderActivated", out ewh);
            return ewh != null && ewh.WaitOne(1);
        }

        private void btnSend_Click(object sender, EventArgs e)
        {
            if (IsLogReaderAvailable())
            {
                var log = new Log(textBox1.Text);

                mmf.Write(XmlDataSerializer.SerializeToXMLString(log));
            }
            else
            {
                MessageBox.Show("Reader is not activated", "MMF-Writer", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
