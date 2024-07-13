using OLTMockServer.DataStructures;
using OLTMockServer.DataStructures.Snap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace OLTMockServer.UI.Snap
{
    public partial class SnapOrderDetailsControl : DataWizardBaseControl, IDataControl
    {
        private readonly UIOperation okOperation = new UIOperation("OK");
        private readonly UIOperation cancelOperation = new UIOperation("Cancel");
        private IConfirmableDialog parentDialog;
        private SnapOrder order;

        public SnapOrderDetailsControl() : this(null, null)
        {
        }

        public SnapOrderDetailsControl(IConfirmableDialog parent, IUIFactory uiFactory) : base()
        {
            this.parentDialog = parent;
            InitializeComponent();
            InitOperations();
            dataWizardSelectItemControl1.UIFactory = uiFactory;
        }

        public SnapOrder Order
        {
            get
            {
                return order;
            }
            set
            {
                order = value;

                dataWizardSelectItemControl1.Items = order.Items;
                radPropertyGrid1.SelectedObject = order;
                //txtOrderCode.DataBindings.Clear();
                //txtOrderCode.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.Code)));

                //txtOrderStatus.DataBindings.Clear();
                //txtOrderStatus.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.StatusCode)));

                //txtCreateDate.DataBindings.Clear();
                //txtCreateDate.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.CreateDate)));

                //txtVendorID.DataBindings.Clear();
                //txtVendorID.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order.Vendor, nameof(Order.Vendor.Id)));

                //txtCustomerID.DataBindings.Clear();
                //txtCustomerID.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order.Customer, nameof(Order.Customer.Id)));

                //txtDeliveryMode.DataBindings.Clear();
                //txtDeliveryMode.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.DeliveryMode)));
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
        }

        private void OnOperationSelected(object sender, UIOperation uIOperation)
        {
            if (uIOperation.Id == okOperation.Id)
            {
                //if ok
                if (this.order.Items.Count == 0)
                {
                    Utils.ShowError("Item list is empty.");
                    return;
                }

                parentDialog?.OK();
            }

            if (uIOperation.Id == cancelOperation.Id)
            {
                parentDialog?.Cancel();
            }
        }

        private void radPropertyGrid1_EditorRequired(object sender, PropertyGridEditorRequiredEventArgs e)
        {
            if (e.Item.Name == nameof(SnapOrder.AcceptTime))
            {
                e.EditorType = typeof(DateTimeEditor);
            }
        }
    }
}
