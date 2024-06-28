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
    public partial class ItemDetailsControl : DataWizardBaseControl, IDataControl
    {
        private readonly UIOperation okOperation = new UIOperation("OK");
        private readonly UIOperation cancelOperation = new UIOperation("Cancel");
        private IConfirmableDialog parentDialog;
        private Item item;

        public ItemDetailsControl(IConfirmableDialog parent) : base()
        {
            this.parentDialog = parent;
            InitializeComponent();
            InitOperations();
        }

        public Item Item
        {
            get
            {
                return item;
            }
            set
            {
                item = value;

                txtItemID.DataBindings.Clear();
                txtItemID.DataBindings.Add(new Binding(nameof(RadTextBox.Text), item, nameof(Item.Id)));

                txtItemBarcode.DataBindings.Clear();
                txtItemBarcode.DataBindings.Add(new Binding(nameof(RadTextBox.Text), item, nameof(Item.Barcode)));

                txtItemName.DataBindings.Clear();
                txtItemName.DataBindings.Add(new Binding(nameof(RadTextBox.Text), item, nameof(Item.Name)));

                txtItemPrice.DataBindings.Clear();
                txtItemPrice.DataBindings.Add(new Binding(nameof(RadTextBox.Text), item, nameof(Item.Price)));

                txtItemDiscount.DataBindings.Clear();
                txtItemDiscount.DataBindings.Add(new Binding(nameof(RadTextBox.Text), item, nameof(Item.Discount)));

                chkIsActive.DataBindings.Clear();
                chkIsActive.DataBindings.Add(new Binding(nameof(RadCheckBox.Checked), item, nameof(Item.IsActive)));

                txtVatAmount.DataBindings.Clear();
                txtVatAmount.DataBindings.Add(new Binding(nameof(RadCheckBox.Text), item, nameof(Item.VAT)));

                txtQuantity.DataBindings.Clear();
                txtQuantity.DataBindings.Add(new Binding(nameof(RadSpinEditor.Value), item, nameof(Item.Quantity)));
            }
        }

        public IConfirmableDialog ParentDialog { get => parentDialog; set => parentDialog = value; }
        public bool ShowQuantity
        {
            get
            {
                return this.txtQuantity.Visible;
            }
            set
            {
                this.txtQuantity.Visible = value;
                this.lblqnt.Visible = value;
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
