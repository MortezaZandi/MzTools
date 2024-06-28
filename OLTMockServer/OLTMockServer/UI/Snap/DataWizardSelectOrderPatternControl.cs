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
    public partial class DataWizardSelectOrderPatternControl : DataWizardBaseControl
    {
        private readonly UIOperation nextOperation = new UIOperation("Next");
        private readonly UIOperation backOperation = new UIOperation("Back");
        private readonly UIOperation cancelOperation = new UIOperation("Cancel");
        private IWizardDialog wizard;
        private Type orderType;
        private OrderPattern orderPattern;

        public DataWizardSelectOrderPatternControl(IWizardDialog wizard, Type orderType) : base()
        {
            this.wizard = wizard;
            this.orderType = orderType;
            InitializeComponent();
            InitOperations();
        }

        private void InitOperations()
        {
            nextOperation.OnSelected += OnOperationSelected;
            backOperation.OnSelected += OnOperationSelected;
            cancelOperation.OnSelected += OnOperationSelected;

            SetOperationButtons(nextOperation, backOperation, cancelOperation);
        }

        public OrderPattern OrderPattern
        {
            get
            {
                return this.orderPattern;
            }
            set
            {
                this.orderPattern = value;

                ResetDataSource();
            }
        }


        private void ResetDataSource()
        {
            radGridView.DataSource = null;
            radGridView.DataSource = orderPattern.PatternItems;
        }

        private void OnOperationSelected(object sender, UIOperation uIOperation)
        {
            if (uIOperation.Id == nextOperation.Id)
            {
                //if ok
                wizard?.GoToNextPage();
            }

            if (uIOperation.Id == backOperation.Id)
            {
                //if ok
                wizard?.GoToPreviousPage();
            }

            if (uIOperation.Id == cancelOperation.Id)
            {
                wizard?.Cancel();
            }
        }

        private void btnAddProperty_Click(object sender, EventArgs e)
        {
            var propControl = new PropertyListControl(null);
            var dataDialog = new DataDialog(propControl);
            propControl.ParentDialog = dataDialog;
            propControl.ObjectType = this.orderType;
            if (dataDialog.ShowDialog() == DialogResult.OK)
            {
                if (propControl.SelectedItem != null)
                {
                    var found =
                        orderPattern.PatternItems.Any(x =>
                            x.PropertyName == propControl.SelectedItem.PropertyName &&
                            x.PropertyType == propControl.SelectedItem.PropertyType);

                    if (!found)
                    {
                        orderPattern.PatternItems.Add(new OrderPatternItem()
                        {
                            PropertyName = propControl.SelectedItem.PropertyName,
                            PropertyType = propControl.SelectedItem.PropertyType,
                        });

                        ResetDataSource();
                    }
                    else
                    {
                        Utils.ShowError($"Property '{propControl.SelectedItem.PropertyName}' alreadt exists in list.");
                    }
                }
            }
        }

        private void btnDeleteProperty_Click(object sender, EventArgs e)
        {
            if (radGridView.SelectedRows.Count > 0)
            {
                var selectedPatternItem = radGridView.SelectedRows[0].DataBoundItem as OrderPatternItem;

                if (Utils.AskQuestion($"Delete '{selectedPatternItem.PropertyName}' ?") == DialogResult.Yes)
                {
                    orderPattern.PatternItems.Remove(selectedPatternItem);
                }
            }
        }

        private void btnDeleteAllProperties_Click(object sender, EventArgs e)
        {
            if (radGridView.Rows.Count > 0)
            {
                if (Utils.AskQuestion($"Delete all patterns?") == DialogResult.Yes)
                {
                    orderPattern.PatternItems.Clear();
                }
            }
        }
    }
}
