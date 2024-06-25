using OLTMockServer.DataStructures;
using OLTMockServer.MockServers;
using OLTMockServer.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer
{
    public abstract class TestManager : IUIFactory
    {
        private TestProject testProject;

        public TestManager(MockServer server)
        {
            this.testProject = new TestProject();
            this.Server = server;
            this.testProject.OnlineShop = server.OnlineShopType;
            this.testProject.TestOptions.TestName = server.GetDefaultTestTitle;
        }

        public TestProject TestProject
        {
            get
            {
                return testProject;
            }
            set
            {
                testProject = value;
            }
        }

        public void Start()
        {
            var hasItem = TestProject.Items.Count > 0;
            var hasVendor = TestProject.Vendors.Count > 0;
            var hasCustomer = TestProject.Customers.Count > 0;

            if (hasItem && hasVendor && hasCustomer)
            {
                if (TestPlayStatuse == Definitions.TestPlayStatuses.Paused)
                {
                    //resume...
                    //throw...
                }
                else if (TestPlayStatuse == Definitions.TestPlayStatuses.Stoped)
                {
                    //start...
                    //throw...
                }
                else
                {
                    throw new ApplicationException($"Cannot go to start mode from {TestPlayStatuse}, salid states to start are Stoped and Paused.");
                }

                TestPlayStatuse = Definitions.TestPlayStatuses.Playing;
            }
            else
            {
                if (!hasItem)
                {
                    throw new InvalidOperationException("No item defined for this test.");
                }

                if (!hasVendor)
                {
                    throw new InvalidOperationException("No vendor selected for this test.");
                }

                if (!hasCustomer)
                {
                    throw new InvalidOperationException("No customer defined for this test.");
                }
            }
        }

        public Definitions.TestPlayStatuses TestPlayStatuse
        {
            get; set;
        }

        public void CreatNewTestProject()
        {
            this.testProject = new TestProject();
        }

        public void SaveTestProject()
        {
            var tempPath = System.IO.Path.GetTempFileName();
            SaveTestProject(this.testProject, tempPath);
        }

        public void SaveTestProject(TestProject project, string filePath)
        {
            try
            {
                XMLDataSerializer.Serialize<TestProject>(project, filePath);
            }
            catch (Exception ex)
            {
                //log...unalbe to save test project
                throw ex;
            }
        }

        public void ImportFromFile(string filePath)
        {
            try
            {
                this.testProject = ImportTestProject(filePath);
            }
            catch (Exception ex)
            {
                //log... unalbe to load test project
                throw;
            }
        }

        public TestProject ImportTestProject(string filePath)
        {
            try
            {
                return XMLDataSerializer.Deserialize<TestProject>(filePath);
            }
            catch (Exception ex)
            {
                //log... unalbe to load test project
                throw;
            }
        }



        public abstract DataWizardSelectItemControl CreateDataWizardSelectItemControl(DataWizardDialog dataWizardDialog);
        public abstract DataWizardSelectCustomerControl CreateDataWizardSelectCustomerControl(DataWizardDialog dataWizardDialog);
        public abstract DataWizardSelectVendorControl CreateDataWizardSelectVendorControl(DataWizardDialog dataWizardDialog);
        public abstract DataWizardTestOptionsControl CreateDataWizardTestOptionsControl(DataWizardDialog dataWizardDialog);
        public abstract DataWizardSelectOrderPatternControl CreateDataWizardSelectOrderPatternControl(DataWizardDialog dataWizardDialog);
        public abstract ItemDetailsControl CreateItemDetailsControl(IConfirmableDialog parentDialog);
        //public abstract IDataControl CreateOrderDetailsControl(IConfirmableDialog parentDialog);
        public abstract CustomerDetailsControl CreateCustomerDetailsControl(IConfirmableDialog parentDialog);
        public abstract VendorDetailsControl CreateVendorDetailsControl(IConfirmableDialog parentDialog);
        public abstract ItemSelectControl CreateItemSelectControl(IConfirmableDialog parentDialog);
        internal void Pause()
        {
            if (this.TestPlayStatuse != Definitions.TestPlayStatuses.Playing)
            {
                throw new ApplicationException("Test is not started.");
            }

            //pause test engine...


            this.TestPlayStatuse = Definitions.TestPlayStatuses.Paused;
        }

        internal void Stop()
        {
            if (this.TestPlayStatuse != Definitions.TestPlayStatuses.Playing && this.TestPlayStatuse != Definitions.TestPlayStatuses.Paused)
            {
                throw new ApplicationException("Test is not started.");
            }

            //stop test engine...

            this.TestPlayStatuse = Definitions.TestPlayStatuses.Stoped;
        }

        public abstract Order CreateNewOrderUsingUI();
        public abstract object EditOrderUnigUI(Order selectedOrder);

        public MockServer Server { get; set; }
    }
}
