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
    public partial class DataWizardBaseControl : UserControl
    {
        public DataWizardBaseControl()
        {
            InitializeComponent();
        }

        protected void SetOperationButtons(params UIOperation[] operations)
        {
            foreach (var opr in operations)
            {
                CreateOperationButton(opr);
            }
        }

        private void CreateOperationButton(UIOperation operation)
        {
            OperationButton oprButton = new OperationButton()
            {
                Size = new Size(100, 24),
                Text = operation.Text,
                UIOperation = operation,
                Enabled = operation.Enabled,
            };

            oprButton.Click += (s, e) =>
            {
                var oprBtn = s as OperationButton;
                oprBtn.UIOperation.RaisOnSelect();
            };

            operation.OnEnableChanged += (s, e) =>
            {
                var selectedOpr = s as UIOperation;

                foreach (OperationButton operationButton in flowLayoutPanel1.Controls)
                {
                    if (operationButton.UIOperation.Id == selectedOpr.Id)
                    {
                        operationButton.Enabled = selectedOpr.Enabled;
                        operationButton.Text = selectedOpr.Text;
                        break;
                    }
                }
            };

            flowLayoutPanel1.Controls.Add(oprButton);
        }

        public string Title
        {
            get
            {
                return lblTitle.Text;
            }
            set
            {
                lblTitle.Text = value;
            }
        }

        public bool ShowOperationCommands
        {
            get
            {
                return pnlOperations.Visible;
            }
            set
            {
                pnlOperations.Visible = value;
            }
        }

        public bool ShowTitleBar
        {
            get
            {
                return pnlTitlebar.Visible;
            }
            set
            {
                pnlTitlebar.Visible = value;
            }
        }

        public virtual void PerformDBActionWithDBCheck(Action action)
        {
            if (!AppManager.Current.TestDBConnection(true))
            {
                if (AppManager.Current.EditDBConnectionUsingUI() == DialogResult.Cancel)
                {
                    return;
                }
                else if (!AppManager.Current.TestDBConnection(true))
                {
                    return;
                }
            }
            else
            {
                action();
            }
        }
    }


    internal class OperationButton : RadButton
    {
        public UIOperation UIOperation { get; set; }
    }
}
