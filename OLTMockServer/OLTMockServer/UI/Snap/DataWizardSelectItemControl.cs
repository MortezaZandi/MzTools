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
using Telerik.WinControls;
using static Telerik.WinControls.UI.ValueMapper;

namespace OLTMockServer.UI
{
    public partial class DataWizardSelectItemControl : DataWizardBaseControl
    {
        private readonly UIOperation nextOperation = new UIOperation("Next");
        private readonly UIOperation backOperation = new UIOperation("Back");
        private readonly UIOperation cancelOperation = new UIOperation("Cancel");
        private IWizardDialog wizard;
        private IUIFactory uiFactory;
        private List<Item> items;
        private Definitions.KnownOnlineShops onlineShop;

        public event EventHandler OnItemsChanged;

        public DataWizardSelectItemControl() : this(null, Definitions.KnownOnlineShops.None)
        {
        }

        public DataWizardSelectItemControl(IWizardDialog wizard, Definitions.KnownOnlineShops onlineShop) : base()
        {
            this.wizard = wizard;
            this.onlineShop = onlineShop;

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
                var selectedVendors = this.GetSelectetVendors;

                var vendorsOfSelectedItems = this.Items.Select(i => i.VendorCode).Distinct();

                var vendorsWithNoItemSelected = selectedVendors.Where(v => !vendorsOfSelectedItems.Contains(v.Code)).ToList();

                if (vendorsWithNoItemSelected.Count > 0)
                {
                    Utils.ShowError("No item selected for these vendors:" + Environment.NewLine + string.Join(" , ", vendorsWithNoItemSelected.Select(v => $"{v.Code}:{v.Name}")));

                    return;
                }

                wizard?.GoToNextPage();
            }

            if (uIOperation.Id == backOperation.Id)
            {
                wizard.GoToPreviousPage();
            }

            if (uIOperation.Id == cancelOperation.Id)
            {
                wizard?.Cancel();
            }
        }

        private void btnAddNewItem_Click(object sender, EventArgs e)
        {
            var itemControl = this.UIFactory.CreateItemDetailsControl(null);
            var newItemDialog = new DataDialog(itemControl, "Add New Item");
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
                    OnItemsChanged?.Invoke(this, EventArgs.Empty);
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
                OnItemsChanged?.Invoke(this, EventArgs.Empty);
                ResetDataSource();
            }
        }

        private void btnCleanItems_Click(object sender, EventArgs e)
        {
            items.Clear();
            OnItemsChanged?.Invoke(this, EventArgs.Empty);
            ResetDataSource();
        }

        private void btnEditItem_Click(object sender, EventArgs e)
        {
            if (radGridView.SelectedRows.Count > 0)
            {

                var selectedItem = radGridView.SelectedRows[0].DataBoundItem as Item;

                var itemControl = this.UIFactory.CreateItemDetailsControl(null);
                var newItemDialog = new DataDialog(itemControl, "Edit Item");
                itemControl.ParentDialog = newItemDialog;
                itemControl.Item = (Item)selectedItem.Clone();

                //itemControl.ShowQuantity = this.showQuantityColumn;

                if (newItemDialog.ShowDialog() == DialogResult.OK)
                {
                    var index = items.IndexOf(selectedItem);
                    items.Remove(selectedItem);
                    Items.Insert(index, itemControl.Item);
                    OnItemsChanged?.Invoke(this, EventArgs.Empty);
                    ResetDataSource();
                }
            }
        }

        private void btnSelectItemFromList_Click(object sender, EventArgs e)
        {
            var itemSelectControl = uiFactory.CreateItemSelectControl(null);
            var dataDialog = new DataDialog(itemSelectControl, "Select Item");
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
                    OnItemsChanged?.Invoke(this, EventArgs.Empty);
                    ResetDataSource();
                }
            }
        }

        private bool IsItemExistsInList(Item item)
        {
            foreach (var existItem in items)
            {
                if (existItem.Id == item.Id && existItem.VendorCode == item.VendorCode)
                {
                    return true;
                }
            }

            return false;
        }

        private void btnImportItemFromRMC_Click(object sender, EventArgs e)
        {
            try
            {
                if (wizard == null)
                {
                    throw new ApplicationException("This function is not available in the edit mode.");
                }

                if (GetSelectetVendors.Count == 0)
                {
                    throw new ApplicationException("No vendor selected");
                }

                PerformDBActionWithDBCheck(() =>
                {
                    var items = SelectItemsFromCMSDB();

                    if (items?.Count > 0)
                    {
                        var existsItems = items.Where(i => IsItemExistsInList(i)).ToList();

                        if (existsItems.Any())
                        {
                            StringBuilder sb = new StringBuilder("These items already exists in the list:").AppendLine();

                            foreach (var item in existsItems)
                            {
                                sb.AppendLine($"{item.Barcode}    :   {item.Name}");
                                items.Remove(item);
                            }

                            Utils.ShowError(sb.ToString());
                        }

                        if (items.Count > 0)
                        {
                            Items.AddRange(items);
                        }

                        OnItemsChanged?.Invoke(this, EventArgs.Empty);
                        ResetDataSource();
                    }
                });
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex.Message);
            }
        }

        private List<Vendor> GetSelectetVendors
        {
            get
            {
                return this.wizard.GetPageOfType<DataWizardSelectVendorControl>().Vendors;
            }
        }

        private List<Item> SelectItemsFromCMSDB()
        {
            var itemSelectControl =
                    new SelectDBItemsControl(
                        null,
                        this.onlineShop,
                        GetSelectetVendors);

            var dataDialog = new DataDialog(itemSelectControl, "Select Item From DB");

            itemSelectControl.ParentDialog = dataDialog;
            dataDialog.ClientSize = new Size(800, 650);
            if (dataDialog.ShowDialog() == DialogResult.OK)
            {
                return itemSelectControl.Items;
            }

            return null;
        }
    }
}
