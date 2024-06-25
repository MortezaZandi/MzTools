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
    public partial class DataWizardTestOptionsControl : DataWizardBaseControl
    {
        private readonly UIOperation finishOperation = new UIOperation("Finish");
        private readonly UIOperation backOperation = new UIOperation("Back");
        private readonly UIOperation cancelOperation = new UIOperation("Cancel");
        private IWizardDialog wizard;
        private TestOptions options;

        public DataWizardTestOptionsControl(IWizardDialog wizard) : base()
        {
            this.wizard = wizard;
            InitializeComponent();
            InitOperations();
        }

        public TestOptions Options
        {
            get
            {
                return options;
            }
            set
            {
                options = value;

                txtTestName.DataBindings.Clear();
                txtTestName.DataBindings.Add(new Binding(nameof(RadTextBox.Text), options, nameof(TestOptions.TestName)));

                txtDelay.DataBindings.Clear();
                txtDelay.DataBindings.Add(new Binding(nameof(RadTextBox.Text), options, nameof(TestOptions.DelayBeforeSendNextOrder)));
                
                txtMaxOrderCount.DataBindings.Clear();
                txtMaxOrderCount.DataBindings.Add(new Binding(nameof(RadSpinEditor.Value), options, nameof(TestOptions.MaxOrderCount)));
                
                chkUseRandomDelay.DataBindings.Clear();
                chkUseRandomDelay.DataBindings.Add(new Binding(nameof(RadCheckBox.Checked), options, nameof(TestOptions.UseRandomDelay)));
            }
        }

        private void InitOperations()
        {
            finishOperation.OnSelected += OnOperationSelected;
            backOperation.OnSelected += OnOperationSelected;
            cancelOperation.OnSelected += OnOperationSelected;

            SetOperationButtons(finishOperation, backOperation, cancelOperation);
        }

        private void OnOperationSelected(object sender, UIOperation uIOperation)
        {
            if (uIOperation.Id == finishOperation.Id)
            {
                //if ok
                wizard.OK();
            }

            if (uIOperation.Id == backOperation.Id)
            {
                //if ok
                wizard.GoToPreviousPage();
            }

            if (uIOperation.Id == cancelOperation.Id)
            {
                wizard.Cancel();
            }
        }
    }
}
