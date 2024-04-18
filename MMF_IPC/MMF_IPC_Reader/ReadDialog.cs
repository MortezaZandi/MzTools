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
using System.Diagnostics.Eventing.Reader;

namespace MMF_IPC_Reader
{
    public partial class ReadDialog : Form
    {
        private IPCDiag diag = new IPCDiag();

        public ReadDialog()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;

            diag.RegisterAction("a", true);
            diag.RegisterAction("b", true);
            diag.RegisterAction("c", true);
            diag.RegisterStatus("LastAction", "-", true);
            this.diag.RegisterStatus("OPT1", "NaN", true);

            diag.OnReportAvailable += Diag_OnReportAvailable;
        }

        private void Diag_OnReportAvailable(string reportText)
        {
            var elements = reportText.Split(',');

            var reportType = elements[0];

            switch (reportType)
            {
                case "Action":

                    var action_details = elements[1].Split(':');
                    var actionName = action_details[1];

                    switch (actionName)
                    {
                        case "a":
                            lblActionA.Text = reportText;
                            break;
                        case "b":
                            lblActionB.Text = reportText;
                            break;
                        case "c":
                            lblActionC.Text = reportText;
                            break;
                        default:
                            break;
                    }
                    break;

                case "Status":
                    var details = elements[1].Split(':');
                    var statusName = details[1];
                    details = elements[2].Split(':');
                    var statusValue= details[1];
                    
                    switch (statusName)
                    {
                        case "LastAction":
                            lblLastAction.Text = reportText;
                            break;

                        case "OPT1":

                            lblOption1.Text = reportText;

                            if (statusValue == "ON")
                            {
                                lblOption1.ForeColor = Color.Green;
                            }
                            else
                            {
                                lblOption1.ForeColor = Color.Red;
                            }
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }
    }
}
