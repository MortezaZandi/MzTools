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
using System.Xml.Linq;
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
            dataWizardSelectItemControl1.OnItemsChanged += OnOrderItemsChanged;

            txtDiscount.ValueChanged += PriceFieldsChanged_ValueChanged;
            txtPackingPrice.ValueChanged += PriceFieldsChanged_ValueChanged;
            txtDeliveryPrice.ValueChanged += PriceFieldsChanged_ValueChanged;
            txtTaxAmount.ValueChanged += PriceFieldsChanged_ValueChanged;
            txtVatAmount.ValueChanged += PriceFieldsChanged_ValueChanged;
        }

        private void PriceFieldsChanged_ValueChanged(object sender, EventArgs e)
        {
            order.DeliveryPrice = txtDeliveryPrice.Value;
            order.PackingPrice = txtPackingPrice.Value;
            order.DiscountValue = txtDiscount.Value;
            order.Tax = txtTaxAmount.Value;
            order.Vat = txtVatAmount.Value;
            order.RecalculatePrices();
            txtTotaPrice.Value = order.Price;
        }

        private void OnOrderItemsChanged(object sender, EventArgs e)
        {
            order.RecalculatePrices();
            RebindControls();
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

                RebindControls();
            }
        }

        private void RebindControls()
        {
            txtOrderCode.DataBindings.Clear();
            txtOrderCode.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.Code), false, DataSourceUpdateMode.OnPropertyChanged));

            txtNewOrderDate.DataBindings.Clear();
            txtNewOrderDate.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.NewOrderDate), false, DataSourceUpdateMode.OnPropertyChanged));

            txtOrderDate.DataBindings.Clear();
            txtOrderDate.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.OrderDate), false, DataSourceUpdateMode.OnPropertyChanged));

            txtStatusCode.DataBindings.Clear();
            txtStatusCode.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.StatusCode), false, DataSourceUpdateMode.OnPropertyChanged));

            txtVendorCode.DataBindings.Clear();
            txtVendorCode.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.VendorCode), false, DataSourceUpdateMode.OnPropertyChanged));

            txtTotaPrice.DataBindings.Clear();
            txtTotaPrice.DataBindings.Add(new Binding(nameof(RadSpinEditor.Value), order, nameof(Order.Price), true, DataSourceUpdateMode.OnPropertyChanged));

            txtDeliveryAddres.DataBindings.Clear();
            txtDeliveryAddres.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.DeliverAddress), false, DataSourceUpdateMode.OnPropertyChanged));

            txtDeliveryMode.DataBindings.Clear();
            txtDeliveryMode.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.DeliveryMode), false, DataSourceUpdateMode.OnPropertyChanged));

            txtDeliveryTime.DataBindings.Clear();
            txtDeliveryTime.DataBindings.Add(new Binding(nameof(RadSpinEditor.Value), order, nameof(Order.DeliveryTime), false, DataSourceUpdateMode.OnPropertyChanged));

            txtDeliveryPrice.DataBindings.Clear();
            txtDeliveryPrice.DataBindings.Add(new Binding(nameof(RadSpinEditor.Value), order, nameof(Order.DeliveryPrice), false, DataSourceUpdateMode.OnPropertyChanged));

            txtPackingPrice.DataBindings.Clear();
            txtPackingPrice.DataBindings.Add(new Binding(nameof(RadSpinEditor.Value), order, nameof(Order.PackingPrice), false, DataSourceUpdateMode.OnPropertyChanged));

            txtDiscountType.DataBindings.Clear();
            txtDiscountType.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.DiscountType), false, DataSourceUpdateMode.OnPropertyChanged));

            txtDiscount.DataBindings.Clear();
            txtDiscount.DataBindings.Add(new Binding(nameof(RadSpinEditor.Value), order, nameof(Order.DiscountValue), false, DataSourceUpdateMode.OnPropertyChanged));

            txtVendorMaxTime.DataBindings.Clear();
            txtVendorMaxTime.DataBindings.Add(new Binding(nameof(RadSpinEditor.Value), order, nameof(Order.VendorMaxPreparationTime), false, DataSourceUpdateMode.OnPropertyChanged));

            txtPreparationTime.DataBindings.Clear();
            txtPreparationTime.DataBindings.Add(new Binding(nameof(RadSpinEditor.Value), order, nameof(Order.PreparationTime), false, DataSourceUpdateMode.OnPropertyChanged));

            txtFirstName.DataBindings.Clear();
            txtFirstName.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.FirstName), false, DataSourceUpdateMode.OnPropertyChanged));

            txtLastName.DataBindings.Clear();
            txtLastName.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.LastName), false, DataSourceUpdateMode.OnPropertyChanged));

            txtLatitude.DataBindings.Clear();
            txtLatitude.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.Latitude), false, DataSourceUpdateMode.OnPropertyChanged));

            txtLongitude.DataBindings.Clear();
            txtLongitude.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.Longitude), false, DataSourceUpdateMode.OnPropertyChanged));

            txtPhone.DataBindings.Clear();
            txtPhone.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.Phone), false, DataSourceUpdateMode.OnPropertyChanged));

            txtAddressCode.DataBindings.Clear();
            txtAddressCode.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.UserAddressCode), false, DataSourceUpdateMode.OnPropertyChanged));

            txtUserCode.DataBindings.Clear();
            txtUserCode.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.UserCode), false, DataSourceUpdateMode.OnPropertyChanged));

            txtExpeditionType.DataBindings.Clear();
            txtExpeditionType.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.ExpeditionType), false, DataSourceUpdateMode.OnPropertyChanged));

            txtTaxAmount.DataBindings.Clear();
            txtTaxAmount.DataBindings.Add(new Binding(nameof(RadSpinEditor.Value), order, nameof(Order.Tax), false, DataSourceUpdateMode.OnPropertyChanged));

            txtTaxCoeff.DataBindings.Clear();
            txtTaxCoeff.DataBindings.Add(new Binding(nameof(RadTextBox.Text), order, nameof(Order.TaxCoeff), false, DataSourceUpdateMode.OnPropertyChanged));

            txtVatAmount.DataBindings.Clear();
            txtVatAmount.DataBindings.Add(new Binding(nameof(RadSpinEditor.Value), order, nameof(Order.Vat), false, DataSourceUpdateMode.OnPropertyChanged));
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
    }
}
