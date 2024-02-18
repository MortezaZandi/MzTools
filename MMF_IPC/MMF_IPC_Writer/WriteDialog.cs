using System;
using System.Collections.Generic;
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
        private readonly OLTDiag diag;

        public WriteDialog()
        {
            InitializeComponent();

            this.diag = new OLTDiag();
        }

        private void btnActionA_Click(object sender, EventArgs e)
        {
            this.diag.OnActionAStart();
            //Thread.Sleep(1000);
            this.diag.OnActionAStop();
        }

        private void btnActionB_Click(object sender, EventArgs e)
        {
            this.diag.OnActionBStart();
            //Thread.Sleep(500);
            this.diag.OnActionBStop();
        }

        private void btnActionC_Click(object sender, EventArgs e)
        {
            this.diag.OnActionCStart();
            //Thread.Sleep(250);
            this.diag.OnActionCStop();
        }
    }
}
