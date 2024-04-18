using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
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
        private readonly IPCDiag diag;
        public WriteDialog()
        {
            InitializeComponent();

            this.diag = new IPCDiag();
            this.diag.RegisterAction("a", false);
            this.diag.RegisterAction("b", false);
            this.diag.RegisterAction("c", false);
            this.diag.RegisterStatus("LastAction", "NaN", false);
            this.diag.RegisterStatus("OPT1", "NaN", false);

            this.diag.AutoReport = true;
        }

        private void btnActionA_Click(object sender, EventArgs e)
        {
            SimulateAction("a", 1000);
            diag.SetStatus("LastAction", "a");
        }

        private void btnActionB_Click(object sender, EventArgs e)
        {
            SimulateAction("b", 500);
            diag.SetStatus("LastAction", "b");
        }

        private void btnActionC_Click(object sender, EventArgs e)
        {
            SimulateAction("c", 250);
            diag.SetStatus("LastAction", "c");
        }

        private void SimulateAction(string name, int durration)
        {
            Task.Run(() =>
            {
                var cid = this.diag.OnActionStart(name);
                Thread.Sleep(durration);
                this.diag.OnActionStop(name, cid);
            });
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            diag.SetStatus("OPT1", checkBox1.Checked ? "ON" : "OFF");
        }

        private void lnkOpenReader_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "MMF_IPC_Reader.exe");
            if (File.Exists(path))
            {
                Process.Start(path);
            }
            else
            {
                MessageBox.Show("Reader not found");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            diag.DoReport();
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            timer1.Enabled = checkBox2.Checked;
        }
    }
}
