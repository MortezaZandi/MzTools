using OLTMockServer.DataStructures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace OLTMockServer.UI
{
    public partial class CustomerDetailsControl : DataWizardBaseControl, IDataControl
    {
        private readonly UIOperation okOperation = new UIOperation("OK");
        private readonly UIOperation cancelOperation = new UIOperation("Cancel");
        private IConfirmableDialog parentDialog;
        private Customer customer;

        public CustomerDetailsControl(IConfirmableDialog parent) : base()
        {
            this.parentDialog = parent;
            InitializeComponent();
            InitOperations();
        }

        public Customer Customer
        {
            get
            {
                return customer;
            }
            set
            {
                customer = value;

                txtCustomerCode.DataBindings.Clear();
                txtCustomerCode.DataBindings.Add(new Binding(nameof(RadTextBox.Text), customer, nameof(Customer.Code)));

                txtCustomerName.DataBindings.Clear();
                txtCustomerName.DataBindings.Add(new Binding(nameof(RadTextBox.Text), customer, nameof(Customer.Name)));

                txtCustomerAddress.DataBindings.Clear();
                txtCustomerAddress.DataBindings.Add(new Binding(nameof(RadTextBox.Text), customer, nameof(Customer.Address)));

                txtCustomerDeliveryType.DataBindings.Clear();
                txtCustomerDeliveryType.DataBindings.Add(new Binding(nameof(RadTextBox.Text), customer, nameof(Customer.DeliveryType)));

                chkIsActive.DataBindings.Clear();
                chkIsActive.DataBindings.Add(new Binding(nameof(RadCheckBox.Checked), customer, nameof(Customer.IsActive)));

            }
        }

        public IConfirmableDialog ParentDialog
        {
            get
            {
                return parentDialog;
            }
            set
            {
                parentDialog = value;
            }
        }

        private void InitOperations()
        {
            okOperation.OnSelected += OnOperationSelected;
            cancelOperation.OnSelected += OnOperationSelected;

            SetOperationButtons(okOperation, cancelOperation);

            //okOperation.Enabled = false;
        }

        private void OnOperationSelected(object sender, UIOperation uIOperation)
        {
            if (uIOperation.Id == okOperation.Id)
            {
                //if ok
                parentDialog?.OK();
            }

            if (uIOperation.Id == cancelOperation.Id)
            {
                parentDialog?.Cancel();
            }
        }

    }
}
