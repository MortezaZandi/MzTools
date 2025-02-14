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
    public partial class PersonDialog : Form
    {
        private readonly IPersonService personService;
        private readonly IFramework framework;

        public PersonDialog()
        {
            InitializeComponent();
        }

        public PersonDialog(IFramework dialogService, IPersonService personService)
        {
            InitializeComponent();
            this.personService = personService;
            this.framework = dialogService;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            framework.ShowDialog<AddressDialog>();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            personService.SavePerson();
        }
    }
}
