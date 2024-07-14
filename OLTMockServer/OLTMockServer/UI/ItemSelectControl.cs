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
    public partial class ItemSelectControl : DataWizardBaseControl, IDataControl
    {
        private readonly UIOperation okOperation = new UIOperation("OK");
        private readonly UIOperation cancelOperation = new UIOperation("Cancel");
        private IConfirmableDialog parentDialog;

        public ItemSelectControl(IUIFactory uiFactory, IConfirmableDialog parent) : base()
        {
            this.parentDialog = parent;
            InitializeComponent();
            InitOperations();
            dataWizardSelectItemControl1.UIFactory = uiFactory;
        }

        public List<Item> Items
        {
            get
            {
                return dataWizardSelectItemControl1.Items;
            }
            set
            {
                dataWizardSelectItemControl1.Items = value;
            }
        }

        public Item SelectedItem
        {
            get
            {
                return dataWizardSelectItemControl1.SelectedItem;
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
                if (dataWizardSelectItemControl1.SelectedItem == null)
                {
                    Utils.ShowError("No item selected.");
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
