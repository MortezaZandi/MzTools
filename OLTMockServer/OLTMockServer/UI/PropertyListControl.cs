using OLTMockServer.DataStructures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;

namespace OLTMockServer.UI
{
    public partial class PropertyListControl : DataWizardBaseControl, IDataControl
    {
        private readonly UIOperation okOperation = new UIOperation("OK");
        private readonly UIOperation cancelOperation = new UIOperation("Cancel");
        private IConfirmableDialog parentDialog;
        private Type objectType;
        private bool colsSet;

        public PropertyListControl(IConfirmableDialog parent) : base()
        {
            this.parentDialog = parent;
            InitializeComponent();
            InitOperations();
            radGridView.Invalidated += RadGridView_Invalidated;
        }

        private void RadGridView_Invalidated(object sender, InvalidateEventArgs e)
        {
            if (!colsSet && radGridView.Columns.Count >= 2)
            {
                SetColWidth(nameof(PropertyItem.PropertyName), 200);
                SetColWidth(nameof(PropertyItem.PropertyType), 200);
                //radGridView.Columns[1].AllowFiltering = false;
                colsSet = true;
            }
        }

        public PropertyItem SelectedItem
        {
            get
            {
                if (radGridView.SelectedRows.Count > 0)
                {
                    var selectedRow = radGridView.SelectedRows[0].DataBoundItem as PropertyItem;

                    return selectedRow;
                }

                return null;
            }
            set
            {
                if (value != null)
                {
                    foreach (var row in radGridView.Rows)
                    {
                        var item = row.DataBoundItem as PropertyItem;

                        if (item.PropertyName == value.PropertyName)
                        {
                            row.IsSelected = true;
                        }
                    }
                }
            }
        }

        public void SetObjectType(Type objectType, List<string> skipList)
        {
            this.objectType = objectType;

            var items = new List<PropertyItem>();

            foreach (var prop in
                objectType.GetProperties().Where(p =>
                p.SetMethod != null && //It is not readonly
                p.GetCustomAttribute(typeof(BrowsableAttribute)) == null && // it is not marked as NotBrowsable
                p.GetCustomAttribute(typeof(Definitions.NotEditableAttribute)) == null)//it is not marked as NotEditable
                )
            {
                if (skipList == null || !skipList.Contains(prop.Name))
                {
                    items.Add(new PropertyItem(prop.Name, prop.PropertyType.Name));
                }
            }

            radGridView.DataSource = items;
        }

        protected void SetColWidth(string text, int width)
        {
            if (!string.IsNullOrEmpty(text))
            {
                foreach (var col in radGridView.Columns)
                {
                    if (col.HeaderText.ToLower() == text.ToLower())
                    {
                        if (col.Width != width)
                        {
                            col.Width = width;
                        }
                    }
                }
            }


        }
        public IConfirmableDialog ParentDialog { get => parentDialog; set => parentDialog = value; }

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
