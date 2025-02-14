using Autofac;
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
    public partial class AddressDialog : Form
    {
        private readonly IFramework framework;
        private readonly IAddressService addressService;

        public AddressDialog()
        {
            InitializeComponent();
        }

        public AddressDialog(IFramework framework, IAddressService addressService)
        {
            InitializeComponent();
            this.framework = framework;
            this.addressService = addressService;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            addressService.Save();
        }
    }
}
