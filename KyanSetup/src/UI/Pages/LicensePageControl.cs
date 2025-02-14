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
    public partial class LicensePageControl : BasePageControl
    {
        public LicensePageControl()
        {
            InitializeComponent();
        }

        public override bool ValidateData()
        {

            //if license is ok return true.

            return base.ValidateData();
        }
    }
}
