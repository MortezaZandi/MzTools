using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KyanSetup
{
    public partial class InstallationPageControl : BasePageControl
    {
        public InstallationPageControl()
        {
            InitializeComponent();
        }

        internal override void OnPageBeignViewed()
        {
            base.OnPageBeignViewed();

            InProgress = true;
        }

        public override void CancelCurrentOperation()
        {
            base.CancelCurrentOperation();

            //..
            Thread.Sleep(5000);
            InProgress = false;

            //in case of conditions not met for cancel
            throw new ApplicationException("Setup is in progress and cannot be cancelled.");
            //----------------

            MessageBox.Show("Installation cancelled.");
        }
    }
}
