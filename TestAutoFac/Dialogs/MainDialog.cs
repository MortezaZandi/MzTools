using Autofac;
using Autofac.Core;
using Autofac.Core.Lifetime;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TestAutoFac
{
    public partial class MainDialog : Form
    {
        private readonly IFramework framework;

        public MainDialog(IFramework framework)
        {
            InitializeComponent();
            this.framework = framework;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            framework.ShowServiceDialog<PersonDialog>();
        }
    }
}
