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

namespace OLTMockServer.UI
{
    public partial class DataWizardSelectCustomerControl : DataWizardBaseControl
    {
        private readonly UIOperation nextOperation = new UIOperation("Next");
        private readonly UIOperation backOperation = new UIOperation("Back");
        private readonly UIOperation cancelOperation = new UIOperation("Cancel");
        private IWizardDialog wizard;
        private List<Customer> customers;
        public DataWizardSelectCustomerControl(IWizardDialog wizard) : base()
        {
            this.wizard = wizard;
            InitializeComponent();
            InitOperations();
        }

        public List<Customer> Customers
        {
            get
            {
                return customers;
            }
            set
            {
                customers = value;
                radGridView.DataSource = customers;
            }
        }

        private void InitOperations()
        {
            nextOperation.OnSelected += OnOperationSelected;
            backOperation.OnSelected += OnOperationSelected;
            cancelOperation.OnSelected += OnOperationSelected;

            SetOperationButtons(nextOperation, backOperation, cancelOperation);
        }

        private void OnOperationSelected(object sender, UIOperation uIOperation)
        {
            if (uIOperation.Id == nextOperation.Id)
            {
                //if ok
                wizard.GoToNextPage();
            }

            if (uIOperation.Id == backOperation.Id)
            {
                //if ok
                wizard.GoToPreviousPage();
            }

            if (uIOperation.Id == cancelOperation.Id)
            {
                wizard.Cancel();
            }
        }

        private void btnAddCustomer_Click(object sender, EventArgs e)
        {
            var customerControl = wizard.UIFactory.CreateCustomerDetailsControl(null);
            var dataDialog = new DataDialog(customerControl, "New Customer");
            customerControl.ParentDialog = dataDialog;
            customerControl.Customer = new Customer();
            if (dataDialog.ShowDialog() == DialogResult.OK)
            {
                customers.Add(customerControl.Customer);

                radGridView.ResetDataSource();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (radGridView.SelectedRows.Count > 0)
            {
                var selectedCustomer = radGridView.SelectedRows[0].DataBoundItem as Customer;

                if (Utils.AskQuestion($"Delete selected customer '{selectedCustomer.Name}' ?") == DialogResult.Yes)
                {
                    customers.Remove(selectedCustomer);

                    radGridView.ResetDataSource();
                }
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (Utils.AskQuestion($"Delete all customer?") == DialogResult.Yes)
            {
                customers.Clear();

                radGridView.ResetDataSource();
            }
        }

        private void btnEditCustomer_Click(object sender, EventArgs e)
        {
            if (radGridView.HasSelection)
            {
                var selectedCustomer = radGridView.GetSelectedRowObject<Customer>();

                var customerControl = new CustomerDetailsControl(null);
                var dataDialog = new DataDialog(customerControl, "Edit Customer");
                customerControl.ParentDialog = dataDialog;
                customerControl.Customer = selectedCustomer.Clone() as Customer;

                if (dataDialog.ShowDialog() == DialogResult.OK)
                {
                    var index = customers.IndexOf(selectedCustomer);
                    customers.Remove(selectedCustomer);
                    customers.Insert(index, customerControl.Customer);
                    radGridView.ResetDataSource();
                }
            }
        }

        private void radGridView_CellDoubleClick(object sender, Telerik.WinControls.UI.GridViewCellEventArgs e)
        {
            if (radGridView.SelectedRows.Count > 0)
            {
                btnEditCustomer.PerformClick();
            }
        }
    }
}
