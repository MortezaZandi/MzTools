using OLTMockServer.DataStructures;
using OLTMockServer.DataStructures.Snap;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
        public event Definitions.OnTestStausChangedEventHandler OnTestStatusChanged;
        private GridViewRowInfo lastHighlightedRow;
        public TestContainerControl(TestManager testManager)
        {
            InitializeComponent();
            this.TestManager = testManager;
            this.testManager.OrderProcessingFeedback += TestManager_OrderProcessingFeedback;
            this.radGridView.CellDoubleClick += RadGridView_CellDoubleClick;
        }

        private void RadGridView_CellDoubleClick(object sender, GridViewCellEventArgs e)
        {
            this.btnShowOrderLogs.PerformClick();
        }

        private void TestManager_OrderProcessingFeedback(Order order, Definitions.OrderProcessingSteps processingStep, int totalSteps, int doneSteps, Definitions.OrderActivityTypes orderActivity = Definitions.OrderActivityTypes.None, Exception exception = null)
        {
            if (this.InvokeRequired)
            {
                var m = new Definitions.OrderProcessingFeedbackEventHandler(TestManager_OrderProcessingFeedback);

                this.Invoke(m, new object[] { order, processingStep, totalSteps, doneSteps, orderActivity, exception });
            }
            else if (processingStep == Definitions.OrderProcessingSteps.TestFinished)
            {
                OnTestStatusChanged?.Invoke(this, totalSteps, doneSteps);
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
                    HighlightGridRow(lastHighlightedRow, false, false);
                }

                HighlightGridRow(rowElement, true, true);

                lastHighlightedRow = rowElement;

                OnTestStatusChanged?.Invoke(this, totalSteps, doneSteps);
            }

            Application.DoEvents();
        }

        private void HighlightGridRow(GridViewRowInfo row, bool set, bool bringToView)
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

            //effectCell.Style.CustomizeFill = true;
            Color? backColor = Color.White;
            Color? foreColor = Color.Black;

            switch (processingStep)
            {
                case Definitions.OrderProcessingSteps.OrderSelectedForProcessing:
                    foreColor = Color.Yellow;
                    break;

                case Definitions.OrderProcessingSteps.PerformingOrderAcivity:
                    foreColor = Color.Orange;
                    break;

                case Definitions.OrderProcessingSteps.OrderAcivityPerformed:
                    foreColor = Color.Green;
                    break;

                case Definitions.OrderProcessingSteps.OrderAcivityNotPerformed:
                    foreColor = Color.Red;
                    break;

                case Definitions.OrderProcessingSteps.NewOrderCreated:
                    foreColor = Color.Green;
                    break;

                case Definitions.OrderProcessingSteps.OrderProcessingError:
                    foreColor = Color.Pink;
                    break;

                case Definitions.OrderProcessingSteps.TestFinished:
                    foreColor = Color.White;
                    break;

                case Definitions.OrderProcessingSteps.None:
                default:
                    break;
            }

            //row.Cells[0].Style.BackColor = backColor.Value;
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
        }

        private void btnShowTestOptions_Click(object sender, EventArgs e)
        {
            var tempTest = (TestProject)this.testManager.TestProject.Clone();

            tempTest.OrderPattern.PatternName = tempTest.TestOptions.TestName;
            tempTest.OrderPattern.PredifinedOrderPatterns = LoadPredifinedPatterns();

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

                //Save patterns to predefined patterns:
                this.testManager.TestProject.OrderPattern.PatternName = this.testManager.TestProject.TestOptions.TestName;
                SavePatternToPredifinedPatterns(tempTest.OrderPattern);
            }
        }

        private void SavePatternToPredifinedPatterns(OrderPattern orderPattern)
        {
            var allPatterns = LoadPredifinedPatterns();

            foreach (var pattern in allPatterns)
            {
                if (pattern.GetUniqueString() == orderPattern.GetUniqueString())
                {
                    //Already exists
                    return;
                }
            }

            var dataDir = Path.GetTempPath();

            dataDir = Path.Combine(dataDir, "OltMockServer");

            if (!Directory.Exists(dataDir))
            {
                Directory.CreateDirectory(dataDir);
            }

            var filePath = Path.Combine(dataDir, orderPattern.PatternName + ".ptrn");

            XMLDataSerializer.Serialize(orderPattern, filePath);
        }

        private List<OrderPattern> LoadPredifinedPatterns()
        {
            var result = new List<OrderPattern>();
            var dataDir = Path.GetTempPath();

            dataDir = Path.Combine(dataDir, "OltMockServer");

            if (!Directory.Exists(dataDir))
            {
                return result;
            }

            try
            {
                var patternFiles = Directory.GetFiles(dataDir, "*.ptrn");

                foreach (var patternFile in patternFiles)
                {
                    try
                    {
                        var pattern = XMLDataSerializer.Deserialize<OrderPattern>(patternFile);
                        result.Add(pattern);
                    }
                    catch (Exception)
                    {
                        //log...
                    }
                }
            }
            catch (Exception ex)
            {
                //log...
            }

            return result;
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
            if (testManager.TestPlayStatuse == Definitions.TestPlayStatuses.Playing)
            {
                Utils.ShowError("Test is running");
                return;
            }

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

        private void radGridView_CellFormatting(object sender, CellFormattingEventArgs e)
        {
        }

        private void btnShowOrderLogs_Click(object sender, EventArgs e)
        {
            if (radGridView.SelectedRows.Count > 0)
            {
                var selectedOrder = radGridView.SelectedRows[0].DataBoundItem as Order;

                var logControl = new OrderLogsControl(null);
                var dataDialog = new DataDialog(logControl);
                logControl.ParentDialog = dataDialog;
                logControl.Order = selectedOrder;
                dataDialog.ClientSize = new Size(1200, 600);
                dataDialog.ShowDialog();
            }
        }
    }
}
