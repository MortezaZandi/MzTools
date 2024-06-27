using OLTMockServer.DataStructures;
using OLTMockServer.DataStructures.Snap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls;
using Telerik.WinControls.UI;

namespace OLTMockServer.UI
{
    public partial class TestContainerControl : UserControl
    {
        private TestManager testManager;
        public event EventHandler OnTestStatusChanged;
        private GridViewRowInfo lastHighlightedRow;
        public TestContainerControl(TestManager testManager)
        {
            InitializeComponent();
            this.TestManager = testManager;
            this.testManager.OrderProcessingFeedback += TestManager_OrderProcessingFeedback;
        }

        private void TestManager_OrderProcessingFeedback(Order order, Definitions.OrderProcessingSteps processingStep, Definitions.OrderActivityTypes orderActivity = Definitions.OrderActivityTypes.None, Exception exception = null)
        {
            if (this.InvokeRequired)
            {
                var m = new Definitions.OrderProcessingFeedbackEventHandler(TestManager_OrderProcessingFeedback);

                this.Invoke(m, new object[] { order, processingStep, orderActivity, exception });
            }
            else if (processingStep == Definitions.OrderProcessingSteps.TestFinished)
            {
                OnTestStatusChanged?.Invoke(this, new EventArgs());
            }
            else
            {
                if (processingStep == Definitions.OrderProcessingSteps.NewOrderCreated)
                {
                    ResetDataSource();
                }

                var rowElement = GetRowOfOrder(order);

                SetRowEffect(rowElement, processingStep);

                if (lastHighlightedRow != null)
                {
                    SetRowHighlight(lastHighlightedRow, false, false);
                }

                SetRowHighlight(rowElement, true, true);

                lastHighlightedRow = rowElement;
            }

            Application.DoEvents();
        }

        private void SetRowHighlight(GridViewRowInfo row, bool set, bool bringToView)
        {
            if (set)
            {
                foreach (GridViewCellInfo cel in row.Cells)
                {
                    if (cel.ColumnInfo.Index > 0)
                    {
                        cel.Style.BackColor = Color.Yellow;
                        cel.Style.CustomizeFill = true;
                    }
                }

                if (bringToView)
                {
                    row.EnsureVisible();
                }
            }
            else
            {
                foreach (GridViewCellInfo cel in row.Cells)
                {
                    if (cel.ColumnInfo.Index > 0)
                    {
                        cel.Style.BackColor = Color.White;
                        cel.Style.CustomizeFill = false;
                    }
                }
            }
        }

        private void SetRowEffect(GridViewRowInfo row, Definitions.OrderProcessingSteps processingStep)
        {
            var effectCell = row.Cells[0];
            effectCell.Style.CustomizeFill = true;
            Color? backColor = Color.White;
            Color? foreColor = Color.Black;

            switch (processingStep)
            {
                case Definitions.OrderProcessingSteps.OrderSelectedForProcessing:
                    backColor = Color.Yellow;
                    break;

                case Definitions.OrderProcessingSteps.PerformingOrderAcivity:
                    backColor = Color.Orange;
                    break;

                case Definitions.OrderProcessingSteps.OrderAcivityPerformed:
                    backColor = Color.GreenYellow;
                    break;

                case Definitions.OrderProcessingSteps.OrderAcivityNotPerformed:
                    backColor = Color.Pink;
                    break;

                case Definitions.OrderProcessingSteps.NewOrderCreated:
                    backColor = Color.Green;
                    foreColor = Color.White;
                    break;

                case Definitions.OrderProcessingSteps.OrderProcessingError:
                    backColor = Color.Pink;
                    foreColor = Color.Red;
                    break;

                case Definitions.OrderProcessingSteps.TestFinished:
                    backColor = Color.White;
                    break;

                case Definitions.OrderProcessingSteps.None:
                default:
                    break;
            }

            row.Cells[0].Style.BackColor = backColor.Value;
            row.Cells[0].Style.ForeColor = foreColor.Value;
        }

        private GridViewRowInfo GetRowOfOrder(Order order)
        {
            foreach (var row in radGridView.Rows)
            {
                var rowOrder = row.DataBoundItem as Order;

                if (rowOrder.UId != Guid.Empty && rowOrder.UId == order.UId)
                {
                    return row;
                }
            }

            return null;
        }

        public TestManager TestManager
        {
            get { return testManager; }
            set
            {
                this.testManager = value;
                ResetDataSource();
            }
        }

        private void ResetDataSource()
        {
            radGridView.DataSource = null;
            radGridView.DataSource = this.testManager.TestProject.Orders;

            SetColWidth(nameof(Order.Code), 50);
            SetColWidth(nameof(Order.Vendor.Name), 80);
            SetColWidth(nameof(Order.CreateDate), 50);
            SetColWidth(nameof(Order.StatusCode), 50);
            SetColWidth(nameof(Order.StatusDescription), 100);
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

        private void btnShowTestOptions_Click(object sender, EventArgs e)
        {
            var tempTest = (TestProject)this.testManager.TestProject.Clone();

            var wizard = new DataWizardDialog(this.testManager, tempTest);

            wizard.Text = "Edit Test";

            if (wizard.ShowDialog() == DialogResult.OK)
            {
                //apply changes
                this.testManager.TestProject.Items = tempTest.Items;
                this.testManager.TestProject.Vendors = tempTest.Vendors;
                this.testManager.TestProject.Customers = tempTest.Customers;
                this.testManager.TestProject.TestOptions = tempTest.TestOptions;
                this.testManager.TestProject.OrderPattern = tempTest.OrderPattern;
            }
        }

        private void btnAddNewOrder_Click(object sender, EventArgs e)
        {
            Order newOrder = this.testManager.CreateNewOrderUsingUI();

            if (newOrder != null)
            {
                testManager.TestProject.Orders.Add(newOrder);
                ResetDataSource();
            }
        }

        private void btnEditOrder_Click(object sender, EventArgs e)
        {
            if (radGridView.SelectedRows.Count > 0)
            {
                var selectedOrder = radGridView.SelectedRows[0].DataBoundItem as Order;

                var editedOrder = this.testManager.EditOrderUnigUI(selectedOrder);

                if (editedOrder != null)
                {
                    ResetDataSource();
                }
            }
        }
    }
}
