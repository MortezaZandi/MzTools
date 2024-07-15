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
    public partial class SelectDBItemsControl : DataWizardBaseControl, IDataControl
    {
        private readonly UIOperation selectOperation = new UIOperation("Select");
        private readonly UIOperation selectAllOperation = new UIOperation("Select All");
        private readonly UIOperation closeOperation = new UIOperation("Close");
        private readonly UIOperation refreshOperation = new UIOperation("Refresh");
        private IConfirmableDialog parentDialog;
        private List<Item> items;
        private Definitions.KnownOnlineShops onlineShops;
        private List<Vendor> selectedVendors;

        public SelectDBItemsControl() : base()
        {
            InitializeComponent();
        }

        public SelectDBItemsControl(IConfirmableDialog parent, Definitions.KnownOnlineShops onlineShops, List<Vendor> selectedVendors) : base()
        {
            this.parentDialog = parent;
            this.onlineShops = onlineShops;
            this.selectedVendors = selectedVendors;

            InitializeComponent();
            InitOperations();

            this.radGridView.PageSize = 50;
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
            selectOperation.OnSelected += OnOperationSelected;
            selectAllOperation.OnSelected += OnOperationSelected;
            closeOperation.OnSelected += OnOperationSelected;
            refreshOperation.OnSelected += OnOperationSelected;

            SetOperationButtons(closeOperation, selectOperation, selectAllOperation, refreshOperation);
        }

        private void OnOperationSelected(object sender, UIOperation uIOperation)
        {
            if (uIOperation.Id == selectOperation.Id)
            {
                parentDialog?.OK();
            }
            else if (uIOperation.Id == selectAllOperation.Id)
            {
                radGridView.SelectAll();
            }
            else if (uIOperation.Id == closeOperation.Id)
            {
                parentDialog?.Cancel();
            }
            else if (uIOperation.Id == refreshOperation.Id)
            {
                ReloadData();
            }
            else
            {
                throw new ApplicationException($"Operation '{uIOperation.Text}' not handled in {this.GetType().Name}");
            }
        }

        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            base.OnInvalidated(e);

            if (this.radGridView.DataSource == null)
            {
                ReloadData();
            }
        }

        private void ReloadData()
        {
            try
            {
                Cursor = Cursors.WaitCursor;

                LoadItemsFromDB();
            }
            catch (Exception ex)
            {
                Utils.ShowError($"Error in reading items from db: {ex.Message}");
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void LoadItemsFromDB()
        {
            var command = AppManager.Current.AppData.GetQueryCommand(Definitions.Query_Name_ReadItemsFromCMSDB);

            if (string.IsNullOrEmpty(command))
            {
                throw new ApplicationException("Item read query command in empty.");
            }

            if (this.selectedVendors.Count == 0)
            {
                throw new ApplicationException("You need to select vendors first.");
            }

            var allowedVendorCodes = string.Join("','", this.selectedVendors.Select(v => v.Code));

            command = string.Format(command, Utils.GetAppTypeNumber(this.onlineShops), $"'{allowedVendorCodes}'");

            var data = DataProvider.Instance.ReadData(command);

            if (data.Rows.Count > 0)
            {
                var readItems = new List<Item>();

                foreach (DataRow row in data.Rows)
                {
                    readItems.Add(new Item
                    {
                        Id = (int)row["ItemID"],
                        Name = (string)row["ItemName"],
                        Barcode = (string)row["Barcode"],
                        Price = (decimal)row["Price"],
                        Discount = (decimal)row["Discount"],
                        VendorCode = (string)row["VendorCode"],
                        Quantity = 0,
                        VAT = 0,
                        IsActive = true,
                    });
                }

                this.radGridView.DataSource = readItems;
            }
        }

        public List<Item> Items
        {
            get
            {
                return radGridView.SelectedRows.Select(r => r.DataBoundItem as Item).ToList();
            }
            set
            {
                this.items = value;
            }
        }
    }
}
