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
    public partial class VendorDetailsControl : DataWizardBaseControl, IDataControl
    {
        private readonly UIOperation okOperation = new UIOperation("OK");
        private readonly UIOperation cancelOperation = new UIOperation("Cancel");
        private IConfirmableDialog parentDialog;
        private Vendor vendor;

        public VendorDetailsControl(IConfirmableDialog parent) : base()
        {
            this.parentDialog = parent;
            InitializeComponent();
            InitOperations();
        }

        public Vendor Vendor
        {
            get
            {
                return vendor;
            }
            set
            {
                vendor = value;

                txtVendorID.DataBindings.Clear();
                txtVendorID.DataBindings.Add(new Binding(nameof(RadTextBox.Text), vendor, nameof(vendor.Code)));

                txtVendorName.DataBindings.Clear();
                txtVendorName.DataBindings.Add(new Binding(nameof(RadTextBox.Text), vendor, nameof(Vendor.Name)));

                txtVendorBaseUrl.DataBindings.Clear();
                txtVendorBaseUrl.DataBindings.Add(new Binding(nameof(RadTextBox.Text), vendor, nameof(Vendor.BaseUrl)));

                chkIsActive.DataBindings.Clear();
                chkIsActive.DataBindings.Add(new Binding(nameof(RadCheckBox.Checked), vendor, nameof(Vendor.IsActive)));

            }
        }

        public IConfirmableDialog ParentDialog { get => parentDialog; set => parentDialog = value; }

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
