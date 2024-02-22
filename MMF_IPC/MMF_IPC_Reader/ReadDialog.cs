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
        private OLTDiag diag = new OLTDiag();

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

                    var actionName = elements[1];

                    switch (actionName)
                    {
                        case "NM:a":
                            lblActionA.Text = reportText;
                            break;
                        case "NM:b":
                            lblActionB.Text = reportText;
                            break;
                        case "NM:c":
                            lblActionC.Text = reportText;
                            break;
                        default:
                            break;
                    }
                    break;

                case "Status":
                    var details= elements[1].Split(':');
                    switch (details[0])
                    {
                        case "LastAction":
                            lblLastAction.Text = reportText;
                            break;

                        case "OPT1":
                            if (details[1] == "ON")
                            {
                                this.BackColor = Color.Green;
                            }
                            else
                            {
                                this.BackColor = Color.WhiteSmoke;
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
