using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MBase.UI.WinForm
{
    public partial class Form2 : BaseDialog
    {
        public Form2() : base()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var f1 = Resolve<Form1>();
            f1.Show();
        }
    }
}
