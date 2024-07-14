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
    public partial class DataWizardDialog : RadForm, IWizardDialog
    {
        private int pageIndex;
        private List<DataWizardBaseControl> pages;
        private DataWizardSelectItemControl selectItemControl;
        private TestProject testProject;
        private DataWizardSelectCustomerControl selectCustomerControl;
        private DataWizardSelectVendorControl selectVendorControl;
        private DataWizardSelectOrderPatternControl selectOrderPatternControl;
        private DataWizardTestOptionsControl testOptionsControl;
        private IUIFactory uiFactory;

        public DataWizardDialog(IUIFactory uiFactory, TestProject testProject = null)
        {
            this.uiFactory = uiFactory;
            this.testProject = testProject;

            InitializeComponent();
            InitWizardPages();
        }

        private void InitWizardPages()
        {
            pages = new List<DataWizardBaseControl>();
            selectItemControl = UIFactory.CreateDataWizardSelectItemControl(this);
            selectItemControl.Dock = DockStyle.Fill;

            selectCustomerControl = UIFactory.CreateDataWizardSelectCustomerControl(this);
            selectCustomerControl.Dock = DockStyle.Fill;

            selectVendorControl = UIFactory.CreateDataWizardSelectVendorControl(this);
            selectVendorControl.Dock = DockStyle.Fill;

            selectOrderPatternControl = UIFactory.CreateDataWizardSelectOrderPatternControl(this);
            selectOrderPatternControl.Dock = DockStyle.Fill;

            testOptionsControl = UIFactory.CreateDataWizardTestOptionsControl(this);
            testOptionsControl.Dock = DockStyle.Fill;

            pages.Add(selectVendorControl);
            pages.Add(selectItemControl);
            pages.Add(selectCustomerControl);
            pages.Add(selectOrderPatternControl);
            pages.Add(testOptionsControl);

            selectItemControl.Items = testProject.Items;
            selectCustomerControl.Customers = testProject.Customers;
            selectVendorControl.Vendors = testProject.Vendors;
            testOptionsControl.Options = testProject.TestOptions;
            selectOrderPatternControl.OrderPattern = testProject.OrderPattern;
        }

        public IUIFactory UIFactory
        {
            get
            {
                return this.uiFactory;
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            ShowPage(0);
        }


        public void GoToNextPage()
        {
            if (pageIndex < pages.Count - 1)
            {
                pageIndex++;
                ShowPage(pageIndex);
            }
        }
        
        public T GetPageOfType<T>()
            where T : DataWizardBaseControl
        {
            foreach (var page in pages)
            {
                if (page.GetType() == typeof(T))
                {
                    return (T)page;
                }
            }

            return null;
        }

        public void ShowPage(int index)
        {
            this.Controls.Clear();
            this.Controls.Add(pages[pageIndex]);
            //var newSize = pageOriginalSizes[pages[pageIndex].GetHashCode()];
            //this.ClientSize = newSize;

            //foreach (Screen screen in Screen.AllScreens)
            //{
            //    if (screen.Bounds.IntersectsWith(this.Bounds))
            //    {
            //        this.Left = screen.Bounds.Left + (screen.WorkingArea.Width / 2 - this.Width / 2);
            //        this.Top = screen.Bounds.Top + (screen.WorkingArea.Height / 2 - this.Height / 2);
            //    }
            //}
        }

        public void GoToPreviousPage()
        {
            if (pageIndex > 0)
            {
                pageIndex--;
                ShowPage(pageIndex);
            }
        }

        public void Cancel()
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        public void OK()
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
