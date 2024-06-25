using Microsoft.VisualBasic;
using OLTMockServer.DataStructures;
using OLTMockServer.MockServers;
using OLTMockServer.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace OLTMockServer
{
    public abstract class TestManager : IUIFactory
    {
        private TestProject testProject;
        public event Definitions.OrderProcessingFeedbackEventHandler OrderProcessingFeedback;
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
            //check current status:
            if (TestPlayStatuse != Definitions.TestPlayStatuses.Paused && TestPlayStatuse != Definitions.TestPlayStatuses.Stoped)
            {
                throw new ApplicationException($"Cannot go to start mode from {TestPlayStatuse}, salid states to start are Stoped and Paused.");
            }

            //check order exists:
            if (testProject.Orders.Count == 0 && !testProject.TestOptions.GenerateOrdersAutomatically)
            {
                throw new ApplicationException("No order defined. Neither custom order is defined nor automatic generation is selected.");
            }

            //check can generate order:
            if (testProject.TestOptions.GenerateOrdersAutomatically && testProject.TestOptions.MaxOrderCount > 0)
            {
                var hasItem = TestProject.Items.Count > 0;
                var hasVendor = TestProject.Vendors.Count > 0;
                var hasCustomer = TestProject.Customers.Count > 0;
                var hasPattenrDefined = testProject.OrderPattern.PatternItems.Count > 0;

                if (!hasItem)
                {
                    throw new InvalidOperationException("Cannot generate order, No item defined for this test. If you want to create orders manually then disable the auto generate in the test options.");
                }

                if (!hasVendor)
                {
                    throw new InvalidOperationException("Cannot generate order, No vendor selected for this test. If you want to create orders manually then disable the auto generate in the test options.");
                }

                if (!hasCustomer)
                {
                    throw new InvalidOperationException("Cannot generate order, No customer defined for this test. If you want to create orders manually then disable the auto generate in the test options.");
                }

                if (!hasPattenrDefined)
                {
                    throw new InvalidOperationException("Cannot generate order, No order pattern defined for this test. If you want to create orders manually then disable the auto generate in the test options.");
                }
            }

            //check old execution session finished:
            if (this.executionTask != null && !this.executionTask.IsCompleted)
            {
                throw new InvalidOperationException("The test is currently busy. Please try again in a few moments.");
            }

            bool isResumed = TestPlayStatuse == Definitions.TestPlayStatuses.Paused;
            this.executionCancellationToken = new CancellationTokenSource();
            executionTask = Task.Factory.StartNew(() =>
            {
                StartSending(this.executionCancellationToken.Token, isResumed);
            });

            TestPlayStatuse = Definitions.TestPlayStatuses.Playing;
        }

        private int lastProcessingOrderIndex;
        private CancellationTokenSource executionCancellationToken;
        private Task executionTask;

        private void StartSending(CancellationToken cancellationToken, bool resume)
        {
            if (resume)
            {
                if (testProject.Orders.Count <= lastProcessingOrderIndex)
                {
                    lastProcessingOrderIndex = 0;
                }
            }
            else
            {
                lastProcessingOrderIndex = 0;
            }

            bool continueExecution = true;
            Order nextOrder = null;
            while (continueExecution)
            {
                try
                {
                    nextOrder = testProject.Orders[lastProcessingOrderIndex];

                    OrderProcessingFeedback?.Invoke(nextOrder, Definitions.OrderProcessingSteps.OrderSelectedForProcessing);

                    foreach (var activity in nextOrder.Activities)
                    {
                        if (!activity.IsDone)
                        {
                            OrderProcessingFeedback?.Invoke(nextOrder, Definitions.OrderProcessingSteps.PerformingOrderAcivity, activity.ActivityType);

                            Vendor targetVendor = GetTargetVendorOfOrder(nextOrder);
                            activity.TryCount++;
                            if (Server.PerformOrderActivity(nextOrder, targetVendor, activity))
                            {
                                activity.ProcessDate = DateTime.Now;
                                OrderProcessingFeedback?.Invoke(nextOrder, Definitions.OrderProcessingSteps.OrderAcivityPerformed, activity.ActivityType);
                            }
                            else
                            {
                                OrderProcessingFeedback?.Invoke(nextOrder, Definitions.OrderProcessingSteps.OrderAcivityNotPerformed, activity.ActivityType);
                            }

                            Thread.Sleep(testProject.TestOptions.DelayBeforeSendNextOrder);
                        }

                        if (cancellationToken.IsCancellationRequested)
                        {
                            break;
                        }
                    }

                    lastProcessingOrderIndex++;

                    var reachEndOfList = lastProcessingOrderIndex >= testProject.Orders.Count;

                    if (reachEndOfList && !cancellationToken.IsCancellationRequested)
                    {
                        var countOfAutoGeneratedOrders = testProject.Orders.Count(o => o.IsAutoGenerated);

                        bool needMoreOrders =
                            testProject.TestOptions.GenerateOrdersAutomatically &&
                            (countOfAutoGeneratedOrders < testProject.TestOptions.MaxOrderCount);

                        if (needMoreOrders)
                        {
                            var newOrder = Server.CreateNewOrder(testProject.OrderPattern, true);
                            testProject.Orders.Add(newOrder);
                            OrderProcessingFeedback?.Invoke(newOrder, Definitions.OrderProcessingSteps.NewOrderCreated);
                        }
                    }

                    var notProcessedOrderExists = testProject.Orders.Any(o => o.HasNotPrcessedActivity);

                    continueExecution = notProcessedOrderExists;

                    if (lastProcessingOrderIndex >= testProject.Orders.Count)
                    {
                        lastProcessingOrderIndex = 0;
                    }

                    if (cancellationToken.IsCancellationRequested)
                    {
                        continueExecution = false;
                    }
                }
                catch (Exception ex)
                {
                    OrderProcessingFeedback?.Invoke(
                        nextOrder,
                        Definitions.OrderProcessingSteps.OrderProcessingError,
                        Definitions.OrderActivityTypes.None,
                        new ApplicationException($"Error in processing order. {ex.Message}", ex));
                }
                finally
                {
                    //Save changes:
                    SaveTestProject();
                }
            }

            TestPlayStatuse = Definitions.TestPlayStatuses.Stoped;

            OrderProcessingFeedback?.Invoke(
                        null,
                        Definitions.OrderProcessingSteps.TestFinished);
        }

        protected abstract Vendor GetTargetVendorOfOrder(Order nextOrder);

        public void Pause()
        {
            if (this.TestPlayStatuse != Definitions.TestPlayStatuses.Playing)
            {
                throw new InvalidOperationException($"Cannot Pause the test, test is {TestPlayStatuse}");
            }

            this.executionCancellationToken.Cancel();
            this.TestPlayStatuse = Definitions.TestPlayStatuses.Paused;
        }

        public void Stop()
        {
            if (this.TestPlayStatuse != Definitions.TestPlayStatuses.Playing && this.TestPlayStatuse != Definitions.TestPlayStatuses.Paused)
            {
                throw new InvalidOperationException($"Cannot Stop the test, test is {TestPlayStatuse}");
            }

            this.executionCancellationToken.Cancel();
            this.TestPlayStatuse = Definitions.TestPlayStatuses.Stoped;
        }

        //public void ForceStop()
        //{
        //    this.executionCancellationToken.Cancel();
        //    this.executionTask.Thread.Abort(); in try catch
        //    this.executionTask=null; let the nextrun to be executed
        //    this.TestPlayStatuse = Definitions.TestPlayStatuses.Stoped;
        //}

        public Definitions.TestPlayStatuses TestPlayStatuse
        {
            get; private set;
        }

        public void CreatNewTestProject()
        {
            this.testProject = new TestProject();
        }

        public void SaveTestProject()
        {
            var saveFilePath = testProject.SaveFilePath;

            if (testProject.IsTemp)
            {
                saveFilePath = testProject.TempFilePath;
            }

            SaveTestProject(this.testProject, saveFilePath);
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

        public abstract Order CreateNewOrderUsingUI();
        public abstract object EditOrderUnigUI(Order selectedOrder);

        public MockServer Server { get; set; }
    }
}
