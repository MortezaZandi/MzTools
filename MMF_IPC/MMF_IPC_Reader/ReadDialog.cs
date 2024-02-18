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
using System.ServiceModel.Description;

namespace MMF_IPC_Reader
{
    public partial class ReadDialog : Form
    {
        private MLogger logg_action_a;
        private MLogger logg_action_b;
        private MLogger logg_action_c;

        private int actionCount_a = 0;
        private int actionCount_b = 0;
        private int actionCount_c = 0;

        public ReadDialog()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

        }

        private void btnRead_Click(object sender, EventArgs e)
        {
            this.logg_action_a = new MLogger("actiona");
            this.logg_action_b = new MLogger("actionb");
            this.logg_action_c = new MLogger("actionc");

            this.logg_action_a.Open();
            this.logg_action_b.Open();
            this.logg_action_c.Open();

            this.logg_action_a.OnNewLogAvailable += ActionALogs_OnNewLogAvailable;
            this.logg_action_b.OnNewLogAvailable += ActionBLogs_OnNewLogAvailable;
            this.logg_action_c.OnNewLogAvailable += ActionCLogs_OnNewLogAvailable;
        }


        private void ActionALogs_OnNewLogAvailable(string log)
        {
            txtActionAStatus.Text = log;

            if (log == "start")
            {
                actionCount_a++;
                txtActionAStatus.ForeColor = Color.Red;
            }
            else if (log == "stop")
            {
                txtActionAStatus.ForeColor = Color.Green;
            }

            txtActionACount.Text = actionCount_a.ToString("N0");
        }

        private void ActionBLogs_OnNewLogAvailable(string log)
        {
            txtActionBStatus.Text = log;

            if (log == "start")
            {
                actionCount_b++;
                txtActionBStatus.ForeColor = Color.Red;
            }
            else if (log == "stop")
            {
                txtActionBStatus.ForeColor = Color.Green;
            }

            txtActionBCount.Text = actionCount_b.ToString("N0");
        }

        private void ActionCLogs_OnNewLogAvailable(string log)
        {
            txtActionCStatus.Text = log;

            if (log == "start")
            {
                actionCount_c++;
                txtActionCStatus.ForeColor = Color.Red;
            }
            else if (log == "stop")
            {
                txtActionCStatus.ForeColor = Color.Green;
            }

            txtActionCCount.Text = actionCount_c.ToString("N0");
        }
    }
}
