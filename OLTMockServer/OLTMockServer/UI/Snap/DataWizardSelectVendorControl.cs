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
    public partial class DataWizardSelectVendorControl : DataWizardBaseControl
    {
        private readonly UIOperation nextOperation = new UIOperation("Next");
        private readonly UIOperation backOperation = new UIOperation("Back");
        private readonly UIOperation cancelOperation = new UIOperation("Cancel");
        private IWizardDialog wizard;
        private List<Vendor> vendors;

        public DataWizardSelectVendorControl(IWizardDialog wizard) : base()
        {
            this.wizard = wizard;
            InitializeComponent();
            InitOperations();
        }

        public List<Vendor> Vendors
        {
            get
            {
                return vendors;
            }
            set
            {
                vendors = value;
                ResetDataSource();
            }
        }

        private void ResetDataSource()
        {
            radGridView.DataSource = null;
            radGridView.DataSource = vendors;
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

        private void btnAddNewVendor_Click(object sender, EventArgs e)
        {
            var vendorControl = this.wizard.UIFactory.CreateVendorDetailsControl(null);
            var dataDialog = new DataDialog(vendorControl, "Add New Vendor");
            vendorControl.ParentDialog = dataDialog;
            vendorControl.Vendor = new Vendor();
            if (dataDialog.ShowDialog() == DialogResult.OK)
            {
                vendors.Add(vendorControl.Vendor);

                ResetDataSource();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (radGridView.SelectedRows.Count > 0)
            {
                var selectedVendor = radGridView.SelectedRows[0].DataBoundItem as Vendor;

                if (Utils.AskQuestion($"Do you want to delete this vendor '{selectedVendor.Name}' vendor code '{selectedVendor.Code}'") == DialogResult.Yes)
                {
                    this.vendors.Remove(selectedVendor);

                    ResetDataSource();
                }
            }
        }

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            if (
                this.vendors.Count > 0 &&
                Utils.AskQuestion($"Do you want to delete all vendors") == DialogResult.Yes)
            {
                this.vendors.Clear();

                ResetDataSource();
            }
        }

        private void btnEditVendor_Click(object sender, EventArgs e)
        {
            if (radGridView.SelectedRows.Count > 0)
            {
                var selectedVendor = radGridView.SelectedRows[0].DataBoundItem as Vendor;

                var vendorControl = new VendorDetailsControl(null);
                var dataDialog = new DataDialog(vendorControl, "Edit Vendor");
                vendorControl.ParentDialog = dataDialog;
                vendorControl.Vendor = selectedVendor.Clone() as Vendor;

                if (dataDialog.ShowDialog() == DialogResult.OK)
                {
                    var index = this.vendors.IndexOf(selectedVendor);

                    this.vendors.Remove(selectedVendor);

                    this.vendors.Insert(index, vendorControl.Vendor);

                    ResetDataSource();
                }
            }
        }
    }
}
