using OLTMockServer.DataStructures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace OLTMockServer.UI
{
    public partial class DataWizardSelectOrderPatternControl : DataWizardBaseControl, IDataControl
    {
        private readonly UIOperation nextOperation = new UIOperation("Next");
        private readonly UIOperation backOperation = new UIOperation("Back");
        private readonly UIOperation cancelOperation = new UIOperation("Cancel");
        private IWizardDialog wizard;
        private Type orderType;
        private OrderPattern orderPattern;
        private IOrderPatternImporter orderPatternImporter;

        public DataWizardSelectOrderPatternControl(IWizardDialog wizard, Type orderType, IOrderPatternImporter orderPatternImporter) : base()
        {
            this.wizard = wizard;
            this.orderType = orderType;
            this.orderPatternImporter = orderPatternImporter;

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

        public bool ShowCommands
        {
            get
            {
                return radCommandBar1.Visible;
            }
            set
            {
                radCommandBar1.Visible = value;
            }
        }

        private IConfirmableDialog parentDialog;
        public IConfirmableDialog ParentDialog
        {
            get { return parentDialog; }
            set
            {
                nextOperation.Text = "OK";
                backOperation.Enabled = false;
                nextOperation.Enabled = true;
                parentDialog = value;
            }
        }

        private void ResetDataSource()
        {
            radGridView.DataSource = null;
            radGridView.DataSource = orderPattern.PatternItems.OrderBy(i => i.PropertyName);

            var gtc = radGridView.Columns["clmGeneratorType"] as GridViewComboBoxColumn;
            gtc.DataSource = Enum.GetValues(typeof(Definitions.PropertyValueGeneratorTypes));

            if (this.orderPattern.PredifinedOrderPatterns.Count > 0)
            {
                if (this.orderPattern.PredifinedOrderPatterns[0].PatternName != string.Empty)
                {
                    this.orderPattern.PredifinedOrderPatterns.Insert(0, new OrderPattern());
                }
            }

            cmbPatterns.DisplayMember = nameof(OrderPattern.PatternName);
            cmbPatterns.SelectedIndexChanged += CmbPatterns_SelectedIndexChanged;
            cmbPatterns.DataSource = this.orderPattern.PredifinedOrderPatterns;
        }

        private void CmbPatterns_SelectedIndexChanged(object sender, Telerik.WinControls.UI.Data.PositionChangedEventArgs e)
        {

        }

        private void OnOperationSelected(object sender, UIOperation uIOperation)
        {
            if (uIOperation.Id == nextOperation.Id)
            {
                //if ok
                foreach (var item in orderPattern.PatternItems)
                {
                    if (item.GenerateType == Definitions.PropertyValueGeneratorTypes.FixedValue && item.Value == null)
                    {
                        Utils.ShowError($"Fixed types need a value, '{item.PropertyName}'");
                        return;
                    }
                }

                wizard?.GoToNextPage();
                ParentDialog?.OK();
            }

            if (uIOperation.Id == backOperation.Id)
            {
                //if ok
                wizard?.GoToPreviousPage();
                ParentDialog?.OK();
            }

            if (uIOperation.Id == cancelOperation.Id)
            {
                wizard?.Cancel();
                ParentDialog?.Cancel();
            }
        }

        private void btnAddProperty_Click(object sender, EventArgs e)
        {
            var propControl = new PropertyListControl(null);

            var dataDialog = new DataDialog(propControl, "Add Property");

            propControl.ParentDialog = dataDialog;

            propControl.SetObjectType(this.orderType, orderPattern.PatternItems.Select(pi => pi.PropertyName).ToList());

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
                            Value = orderType.GetProperty(propControl.SelectedItem.PropertyName).GetValue(Activator.CreateInstance(orderType))?.ToString()
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
                    ResetDataSource();
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
                    ResetDataSource();
                }
            }
        }

        private void btnApplyPattern_Click(object sender, EventArgs e)
        {
            if (cmbPatterns.SelectedIndex > 0)
            {
                var selectedItem = cmbPatterns.SelectedItem.DataBoundItem as OrderPattern;

                if (selectedItem.PatternItems.Count > 0)
                {
                    if (selectedItem.GetUniqueString() != this.orderPattern.GetUniqueString())
                    {
                        bool confirmed = (orderPattern.PatternItems.Count == 0);

                        if (!confirmed)
                        {
                            var control = new DataWizardSelectOrderPatternControl(null, null, null);
                            var dataDialog = new DataDialog(control, "Replace Alert");
                            control.ParentDialog = dataDialog;
                            control.OrderPattern = selectedItem;
                            control.Title = "Check Before Replace";
                            control.ShowCommands = false;

                            if (dataDialog.ShowDialog() == DialogResult.OK)
                            {
                                confirmed = true;
                            }
                        }

                        if (confirmed)
                        {
                            orderPattern.PatternItems.Clear();
                            orderPattern.PatternItems.AddRange(selectedItem.PatternItems);
                            ResetDataSource();
                        }
                    }
                    else
                    {
                        Utils.ShowInfo("Nothing changed.");
                    }
                }
                else
                {
                    Utils.ShowInfo("Pattern is empty.");
                }
            }
        }

        private void btnImportFromFile_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.orderPatternImporter != null)
                {
                    var pattern = this.orderPatternImporter.ImportPatternFromFile();

                    if (pattern != null)
                    {
                        this.orderPattern.PatternItems.Clear();

                        this.orderPattern.PatternItems.AddRange(pattern.PatternItems);

                        ResetDataSource();
                    }
                }
                else
                {
                    throw new ApplicationException("Pattern importer is null.");
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex.Message);
            }
        }
    }
}
