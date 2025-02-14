using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KyanSetup
{
    public partial class InstallationTypePageControl : BasePageControl
    {
        public InstallationTypePageControl()
        {
            InitializeComponent();
        }

        public override bool ValidateData()
        {
            var valid = base.ValidateData();

            if (valid)
            {
                if (rdInstallHeadquarter.Checked)
                {

                }
                else if (rdInstallRetailStore.Checked)
                {

                }
                else if (rdInstallPOS.Checked)
                {

                }
                else
                {
                    valid = false;
                   // throw new Exception("Select installation type");
                }
            }

            return valid;
        }
    }
}
