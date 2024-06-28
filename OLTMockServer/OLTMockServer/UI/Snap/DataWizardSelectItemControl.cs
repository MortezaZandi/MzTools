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
using static Telerik.WinControls.UI.ValueMapper;

namespace OLTMockServer.UI
{
    public partial class DataWizardSelectItemControl : DataWizardBaseControl
    {
        private readonly UIOperation okOperation = new UIOperation("Next");
        private readonly UIOperation cancelOperation = new UIOperation("Cancel");
        private IWizardDialog wizard;
        private IUIFactory uiFactory;
        private List<Item> items;

        public DataWizardSelectItemControl() : this(null)
        {
        }

        public DataWizardSelectItemControl(IWizardDialog wizard) : base()
        {
            this.wizard = wizard;

            if (wizard != null)
            {
                this.UIFactory = wizard.UIFactory;
            }

            InitializeComponent();
            InitOperations();
        }

        public List<Item> Items
        {
            get
            {
                return items;
            }
            set
            {
                items = value;

                ResetDataSource();
            }
        }

        public IUIFactory UIFactory
        {
            get
            {
                return uiFactory;
            }
            set
            {
                uiFactory = value;
            }
        }

        public Item SelectedItem
        {
            get
            {
                if (radGridView.SelectedRows.Count > 0)
                {
                    return radGridView.SelectedRows[0].DataBoundItem.Clone() as Item;
                }
                else
                {
                    return null;
                }
            }
        }

        public bool AllowSelectItem
        {
            get
            {
                return btnSelectItemFromList.Enabled;
            }
            set
            {
                btnSelectItemFromList.Enabled = value;
            }
        }

        public bool AllowEditItem
        {
            get
            {
                return btnEditItem.Enabled;
            }
            set
            {
                btnEditItem.Enabled = value;
            }
        }

        public bool AllowDeleteItem
        {
            get
            {
                return btnDeleteItem.Enabled;
            }
            set
            {
                btnDeleteItem.Enabled = value;
                btnCleanItems.Enabled = value;
            }
        }

        public bool ReadOnly
        {
            get
            {
                return !radCommandBar1.Enabled;
            }
            set
            {
                radCommandBar1.Enabled = !value;
            }
        }

        private bool showQuantityColumn;

        public bool ShowQuantityColumn
        {
            get
            {
                return showQuantityColumn;
            }
            set
            {
                showQuantityColumn = value;
            }
        }

        private void ResetDataSource()
        {
            radGridView.DataSource = null;
            radGridView.DataSource = items;

            SetColWidth(nameof(Item.Id), 50);
            SetColWidth(nameof(Item.Barcode), 150);
            SetColWidth(nameof(Item.Name), 150);
            SetColWidth(nameof(Item.Price), 80);
            SetColWidth(nameof(Item.Discount), 80);
            SetColWidth(nameof(Item.IsActive), 50);

            if (radGridView.Columns.Count > 0)
            {
                radGridView.Columns["clmQuantity"].IsVisible = this.showQuantityColumn;
            }
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
                wizard?.GoToNextPage();
            }

            if (uIOperation.Id == cancelOperation.Id)
            {
                wizard?.Cancel();
            }
        }

        private void btnAddNewItem_Click(object sender, EventArgs e)
        {
            var itemControl = this.UIFactory.CreateItemDetailsControl(null);
            var newItemDialog = new DataDialog(itemControl);
            itemControl.ParentDialog = newItemDialog;
            itemControl.Item = new Item();
            if (newItemDialog.ShowDialog() == DialogResult.OK)
            {
                if (IsItemExistsInList(itemControl.Item))
                {
                    Utils.ShowError($"Item id {itemControl.Item.Id} already exists in list.");
                }
                else
                {
                    Items.Add(itemControl.Item);
                    ResetDataSource();
                }
            }
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            if (radGridView.SelectedRows.Count > 0)
            {
                var selectedRow = radGridView.SelectedRows[0];
                var data = selectedRow.DataBoundItem as Item;
                this.items.Remove(data);
                ResetDataSource();
            }
        }

        private void btnCleanItems_Click(object sender, EventArgs e)
        {
            items.Clear();
            ResetDataSource();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (radGridView.SelectedRows.Count > 0)
            {

                var selectedItem = radGridView.SelectedRows[0].DataBoundItem as Item;

                var itemControl = this.UIFactory.CreateItemDetailsControl(null);
                var newItemDialog = new DataDialog(itemControl);
                itemControl.ParentDialog = newItemDialog;
                itemControl.Item = (Item)selectedItem.Clone();

                itemControl.ShowQuantity = this.showQuantityColumn;

                if (newItemDialog.ShowDialog() == DialogResult.OK)
                {
                    var index = items.IndexOf(selectedItem);
                    items.Remove(selectedItem);
                    Items.Insert(index, itemControl.Item);
                    ResetDataSource();
                }
            }
        }

        private void btnSelectItemFromList_Click(object sender, EventArgs e)
        {
            var itemSelectControl = uiFactory.CreateItemSelectControl(null);
            var dataDialog = new DataDialog(itemSelectControl);
            itemSelectControl.ParentDialog = dataDialog;
            if (dataDialog.ShowDialog() == DialogResult.OK)
            {
                if (IsItemExistsInList(itemSelectControl.SelectedItem))
                {
                    Utils.ShowError($"Item id {itemSelectControl.SelectedItem.Id} already exists in list.");
                }
                else
                {
                    items.Add(itemSelectControl.SelectedItem);
                    ResetDataSource();
                }
            }
        }

        private bool IsItemExistsInList(Item item)
        {
            foreach (var existItem in items)
            {
                if (existItem.Id == item.Id)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
