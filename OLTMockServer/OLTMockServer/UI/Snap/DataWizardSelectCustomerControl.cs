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
                ResetDataSource();
            }
        }

        private void ResetDataSource()
        {
            radGridView.DataSource = null;
            radGridView.DataSource = customers;

            SetColWidth(nameof(Customer.Id), 100);
            SetColWidth(nameof(Customer.Name), 150);
            SetColWidth(nameof(Customer.Address), 150);
            SetColWidth(nameof(Customer.DeliveryType), 100);
            SetColWidth(nameof(Customer.IsActive), 50);
        }

        private void SetColWidth(string text, int width)
        {
            if (!string.IsNullOrEmpty(text))
            {
                foreach (var col in radGridView.Columns)
                {
                    if (col.HeaderText.ToLower() == text.ToLower())
                    {
                        col.Width = width;
                    }
                }
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
            var dataDialog = new DataDialog(customerControl);
            customerControl.ParentDialog = dataDialog;
            customerControl.Customer = new Customer();
            if (dataDialog.ShowDialog() == DialogResult.OK)
            {
                customers.Add(customerControl.Customer);

                ResetDataSource();
            }
        }
    }
}
