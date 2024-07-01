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
    public partial class OrderLogsControl : DataWizardBaseControl, IDataControl
    {
        private readonly UIOperation okOperation = new UIOperation("Close");
        private readonly UIOperation refreshOperation = new UIOperation("Reftesh");

        private IConfirmableDialog parentDialog;
        private Order order;

        public OrderLogsControl(IConfirmableDialog parent) : base()
        {
            this.parentDialog = parent;
            InitializeComponent();
            InitOperations();
        }

        public Order Order
        {
            get
            {
                return this.order;
            }
            set
            {
                this.order = value;

                SetStatusLabels();

                radGridView.Rows.Clear();

                foreach (var log in this.order.Logs)
                {
                    if (!string.IsNullOrEmpty(log.LogTitle))
                    {
                        radGridView.Rows.Add(new object[] { string.Empty, string.Empty, log.LogTitle });
                        var index = radGridView.Rows.Count;
                        radGridView.Rows.Add(new object[] { log.LogTime.ToString("yyyy-MM-dd HH:mm:ss"), log.LogTitle });

                        SetRowTextStyle(index, FontStyle.Bold);
                        SetRowTextColor(index, log.LogType);
                    }

                    if (!string.IsNullOrEmpty(log.LogDetails))
                    {
                        var index = radGridView.Rows.Count;
                        radGridView.Rows.Add(new object[] { log.LogTime.ToString("yyyy-MM-dd HH:mm:ss"), log.LogDetails });

                        SetRowTextColor(index, log.LogType);
                    }

                    if (string.IsNullOrEmpty(log.LogTitle) && string.IsNullOrEmpty(log.LogDetails))
                    {
                        radGridView.Rows.Add(new object[] { string.Empty, string.Empty });
                    }
                }
            }
        }

        private void SetStatusLabels()
        {
            var sendActivity = order.SendActivity;
            var editActivity = order.EditActivity;
            var successSendActivity = order.AnySuccessSendActivity;
            DateTime diffBaseTime = order.CreateDate;
            string baseString = "after create";

            //Send status
            if (successSendActivity != null)
            {
                diffBaseTime = successSendActivity.ProcessDate;
                baseString = "after send";

                lblSendStatus.Text = "Order Sent";
                lblSendStatusDescription.Text = $"Order was sent to vendor at {successSendActivity.ProcessDate:yyyy-MM-dd HH:mm:ss}, {(successSendActivity.ProcessDate - order.CreateDate).Round()} after create.";
                lblSendStatus.ForeColor = Color.Green;
            }
            else if (sendActivity != null)
            {
                if (sendActivity.TryCount > 0)
                {
                    lblSendStatus.Text = "Send Failed";
                    lblSendStatusDescription.Text = $"{sendActivity.TryCount}th send action faild at {sendActivity.ActivityDate:yyyy-MM-dd HH:mm:ss}";
                    lblSendStatus.ForeColor = Color.Red;
                }
                else
                {
                    lblSendStatus.Text = "Not Processed";
                    lblSendStatusDescription.Text = $"This order is not processed, Click play button to process new orders.";
                }
            }
            else if (editActivity != null)
            {
                if (editActivity.IsDone == true)
                {
                    successSendActivity = editActivity;
                    diffBaseTime = editActivity.ProcessDate;
                    baseString = "after send";

                    lblSendStatus.Text = "Edit Sent";
                    lblSendStatusDescription.Text = $"Order was sent to vendor at {editActivity.ProcessDate:yyyy-MM-dd HH:mm:ss}, {(editActivity.ProcessDate - order.CreateDate)} after create.";
                    lblSendStatus.ForeColor = Color.Green;
                }
                else
                {
                    if (editActivity.TryCount > 0)
                    {
                        lblSendStatus.Text = "Send Edit Failed";
                        lblSendStatusDescription.Text = $"{editActivity.TryCount}th send action faild at {editActivity.ActivityDate:yyyy-MM-dd HH:mm:ss}";
                        lblSendStatus.ForeColor = Color.Red;
                    }
                    else
                    {
                        lblSendStatus.Text = "Edit Not Processed";
                        lblSendStatusDescription.Text = $"This order is not processed, Click play button to process edited orders.";
                    }
                }
            }
            else
            {
                lblSendStatus.Text = "Unknown Send Status";
                lblSendStatusDescription.Text = $"This order is not processed or has error.";
            }

            //Ack status
            if (order.AckTime != DateTime.MinValue)
            {
                lblACKStatus.Text = "Ack done";
                lblACKStatusDescription.Text = $"ACK response received at {order.AckTime:yyyy-MM-dd HH:mm:ss}, {(order.AckTime - diffBaseTime)} {baseString}.";
                lblACKStatus.ForeColor = Color.Green;
            }
            else
            {
                if (successSendActivity != null)
                    lblACKStatusDescription.Text = $"Order sent, But Ack message not received from the target vendor.";
                else
                    lblACKStatusDescription.Text = $"The order not send yet.";
            }

            //Pick status
            if (order.PickTime != DateTime.MinValue)
            {
                lblPickStatus.Text = "Pick done";
                lblPickStatusDescription.Text = $"Pick response received at {order.PickTime:yyyy-MM-dd HH:mm:ss}, {(order.PickTime - diffBaseTime)} {baseString}.";
                lblPickStatus.ForeColor = Color.Green;
            }
            else
            {
                if (successSendActivity != null)
                    lblPickStatusDescription.Text = $"Order sent, But Pick message not received from the target vendor.";
                else
                    lblPickStatusDescription.Text = $"The order not send yet.";
            }

            //Accept status
            if (order.AcceptTime != DateTime.MinValue)
            {
                lblAcceptStatus.Text = "Accept done";
                lblAcceptStatusDescription.Text = $"Accept response received at {order.AcceptTime:yyyy-MM-dd HH:mm:ss}, {(order.AcceptTime - diffBaseTime)}  {baseString} .";
                lblAcceptStatus.ForeColor = Color.Green;
            }
            else
            {
                if (successSendActivity != null)
                    lblAcceptStatusDescription.Text = "Order sent, But Accept message not received from the target vendor.";
                else
                    lblAcceptStatusDescription.Text = "The order not send yet.";
            }

            //lblResponseStatus
            if (successSendActivity != null)
            {
                if ((order.AckTime == DateTime.MinValue && order.PickTime == DateTime.MinValue && order.AcceptTime == DateTime.MinValue))
                {
                    lblResponseStatusDescription.Text = $"The order has been sent, but no response has been received from the vendor.";
                }
                else
                {
                    lblResponseStatus.Hide();
                }
            }
            else
            {
                if ((order.AckTime != DateTime.MinValue || order.PickTime != DateTime.MinValue || order.AcceptTime != DateTime.MinValue))
                {
                    lblResponseStatus.Text = "Vendor Responsd";
                    lblResponseStatusDescription.Text = $"The order not sent yet, but some response been received from the vendor.";
                    lblResponseStatusDescription.ForeColor = Color.Orange;
                }
                else
                {
                    lblResponseStatusDescription.Text = $"The order not send yet.";
                }
            }
        }

        private void SetRowTextStyle(int index, FontStyle style)
        {
            var row = radGridView.Rows[index];

            foreach (GridViewCellInfo cel in row.Cells)
            {
                if (cel.ColumnInfo.Index > 0)//dont change the time column
                {
                    cel.Style.Font = new Font(this.Font.FontFamily, this.Font.Size + 2, style);
                }
            }
        }

        private void SetRowTextColor(int index, Definitions.LogTypes logType)
        {
            var row = radGridView.Rows[index];
            var textColor = Color.Black;

            if (logType == Definitions.LogTypes.Error)
            {
                textColor = Color.Red;
            }
            else if (logType == Definitions.LogTypes.Warining)
            {
                textColor = Color.Orange;
            }

            foreach (GridViewCellInfo cel in row.Cells)
            {
                cel.Style.ForeColor = textColor;
            }
        }


        public IConfirmableDialog ParentDialog { get => parentDialog; set => parentDialog = value; }

        private void InitOperations()
        {
            okOperation.OnSelected += OnOperationSelected;
            refreshOperation.OnSelected += OnOperationSelected;

            SetOperationButtons(okOperation, refreshOperation);
        }

        private void OnOperationSelected(object sender, UIOperation uIOperation)
        {
            if (uIOperation.Id == okOperation.Id)
            {
                parentDialog?.OK();
            }

            if (uIOperation.Id == refreshOperation.Id)
            {
                this.Order = this.order;
            }
        }
    }
}
