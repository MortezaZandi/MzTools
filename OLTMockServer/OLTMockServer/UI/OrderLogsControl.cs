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
        private readonly UIOperation okOperation = new UIOperation("OK");
        private IConfirmableDialog parentDialog;
        private List<string> logs = new List<string>();

        public OrderLogsControl(IConfirmableDialog parent) : base()
        {
            this.parentDialog = parent;
            InitializeComponent();
            InitOperations();
        }

        public List<string> Logs
        {
            get
            {
                return this.logs;
            }
            set
            {
                this.logs = value;

                List<Log> logs = new List<Log>();

                foreach (var item in value)
                {
                    logs.Add(new Log(item));
                }

                radGridView.DataSource = logs;
            }
        }

        public IConfirmableDialog ParentDialog { get => parentDialog; set => parentDialog = value; }

        private void InitOperations()
        {
            okOperation.OnSelected += OnOperationSelected;

            SetOperationButtons(okOperation);
        }

        private void OnOperationSelected(object sender, UIOperation uIOperation)
        {
            if (uIOperation.Id == okOperation.Id)
            {
                parentDialog?.OK();
            }
        }
    }
}
