using OLTMockServer.DataStructures;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace OLTMockServer.UI
{
    public partial class ServerLogsControl : DataWizardBaseControl, IDataControl, IDataWriter
    {
        private readonly UIOperation closeOperation = new UIOperation("Close");
        private readonly CustomTraceListener listener;
        private readonly List<LogDetails> logs;
        private IConfirmableDialog parentDialog;

        public ServerLogsControl() : base()
        {
            InitializeComponent();
        }

        public ServerLogsControl(IConfirmableDialog parent) : base()
        {
            this.parentDialog = parent;
            InitializeComponent();
            InitOperations();

            this.logs = new List<LogDetails>();

            this.listener = new CustomTraceListener(this);

            System.Diagnostics.Debug.Listeners.Add(listener);

            this.customGridView1.DataSource = this.logs;

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
            closeOperation.OnSelected += OnOperationSelected;

            SetOperationButtons(closeOperation);
        }

        private void OnOperationSelected(object sender, UIOperation uIOperation)
        {
            if (uIOperation.Id == closeOperation.Id)
            {
                parentDialog?.OK();
            }
        }

        ~ServerLogsControl()
        {
            this.listener.Close();
            this.listener.Dispose();
        }

        public void Write(string data)
        {
            WriteLine(data);
        }

        public void WriteLine(string data)
        {
            lock (this.logs)
            {
                this.logs.Add(new LogDetails($"{DateTime.Now:yyyy-MM-dd   HH:mm:ss}", data));

                this.customGridView1.ResetDataSource();

                if (this.customGridView1.RowCount > 0)
                {
                    this.customGridView1.Rows[this.customGridView1.RowCount - 1].EnsureVisible(false);
                }
            }
        }

        private void commandBarButton1_Click(object sender, EventArgs e)
        {
            lock (this.logs)
            {
                this.logs.Clear();
                this.customGridView1.ResetDataSource();
            }
        }

        private void chkTopMost_ToggleStateChanged(object sender, StateChangedEventArgs args)
        {
            this.FindForm().TopMost = this.chkTopMost.ToggleState == Telerik.WinControls.Enumerations.ToggleState.On;
        }
    }

    public interface IDataWriter
    {
        void Write(string data);

        void WriteLine(string data);
    }

    public class CustomTraceListener : TraceListener
    {
        private readonly IDataWriter dataWriter;

        public CustomTraceListener(IDataWriter dataWriter) : base()
        {
            this.dataWriter = dataWriter;
        }

        public override void Write(string message)
        {
            this.dataWriter.Write(message);
        }

        public override void WriteLine(string message)
        {
            this.dataWriter.WriteLine(message);
        }
    }

    public class LogDetails
    {
        public LogDetails(string time, string log)
        {
            Time = time;
            Log = log;
        }

        public string Time { get; set; }
        public string Log { get; set; }
    }
}
