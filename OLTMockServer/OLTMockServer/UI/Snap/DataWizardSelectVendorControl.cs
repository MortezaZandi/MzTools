﻿using OLTMockServer.DataStructures;
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

            SetColWidth(nameof(Vendor.Id), 100);
            SetColWidth(nameof(Vendor.Name), 150);
            SetColWidth(nameof(Vendor.BaseUrl), 200);
            SetColWidth(nameof(Vendor.IsActive), 50);
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

        private void btnAddNewVendor_Click(object sender, EventArgs e)
        {
            var vendorControl = this.wizard.UIFactory.CreateVendorDetailsControl(null);
            var dataDialog = new DataDialog(vendorControl);
            vendorControl.ParentDialog = dataDialog;
            vendorControl.Vendor = new Vendor();
            if (dataDialog.ShowDialog() == DialogResult.OK)
            {
                vendors.Add(vendorControl.Vendor);

                ResetDataSource();
            }
        }
    }
}