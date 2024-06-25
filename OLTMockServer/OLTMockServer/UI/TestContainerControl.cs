using OLTMockServer.DataStructures;
using OLTMockServer.DataStructures.Snap;
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
using Telerik.WinControls.UI;

namespace OLTMockServer.UI
{
    public partial class TestContainerControl : UserControl
    {
        private TestManager testManager;
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
            else
            {
                bool processed = false;
                foreach (var row in radGridView.Rows)
                {
                    var rowOrder = row.DataBoundItem as Order;

                    if (rowOrder == null)
                    {
                        ResetDataSource();

                        var m = new Definitions.OrderProcessingFeedbackEventHandler(TestManager_OrderProcessingFeedback);

                        this.Invoke(m, new object[] { order, processingStep, orderActivity });

                        return;
                    }

                    if (rowOrder.Code == order.Code)
                    {
                        if (processed)
                        {
                            throw new ApplicationException($"More than one order found in the list with code '{order.Code}'");
                        }

                        processed = true;

                        switch (processingStep)
                        {
                            case Definitions.OrderProcessingSteps.OrderSelectedForProcessing:
                                row.Cells[0].Style.BackColor = Color.Yellow;
                                break;


                            case Definitions.OrderProcessingSteps.PerformingOrderAcivity:
                                row.Cells[0].Style.BackColor = Color.Orange;
                                break;
                            case Definitions.OrderProcessingSteps.OrderAcivityPerformed:
                                row.Cells[0].Style.BackColor = Color.GreenYellow;
                                break;
                            case Definitions.OrderProcessingSteps.OrderAcivityNotPerformed:
                                row.Cells[0].Style.BackColor = Color.Pink;
                                break;
                            case Definitions.OrderProcessingSteps.NewOrderCreated:
                                row.Cells[0].Style.BackColor = Color.LightGreen;
                                row.Cells[0].Style.ForeColor = Color.Green;

                                break;
                            case Definitions.OrderProcessingSteps.OrderProcessingError:
                                row.Cells[0].Style.BackColor = Color.Pink;
                                row.Cells[0].Style.ForeColor = Color.Red;
                                break;
                            case Definitions.OrderProcessingSteps.TestFinished:
                                row.Cells[0].Style.BackColor = Color.White;

                                break;
                            case Definitions.OrderProcessingSteps.None:
                            default:
                                break;
                        }

                        radGridView.Invalidate();
                    }
                }
            }

            Application.DoEvents();
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
