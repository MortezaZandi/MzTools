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
    public partial class BasicInformationPageControl : BasePageControl
    {
        public BasicInformationPageControl()
        {
            InitializeComponent();
        }

        public override bool ValidateData()
        {
            //if entered data is ok return true.
            if (textBox1.Text.Length > 0)
            {
                throw new Exception("database name is not valid.");
            }

            return base.ValidateData();
        }
    }
}
