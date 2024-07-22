using OLTMockServer.DataStructures;
using OLTMockServer.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Telerik.WinControls.UI;
using static System.Net.Mime.MediaTypeNames;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace OLTMockServer
{
    public partial class MainDialog : RadForm
    {
        private readonly AppManager appManager;
        private TestTabPage activeTestTab;
        private TestManager activeTest;

        public MainDialog()
        {
            InitializeComponent();

            this.appManager = new AppManager();

            InitUI();
            //new CodeTestDialog().ShowDialog();
        }

        private void InitUI()
        {
            radPageView1.SelectedPageChanged += RadPageView1_SelectedPageChanged;

            foreach (var serverType in appManager.AvailableServerTypes)
            {
                var newMenuItem = new Telerik.WinControls.UI.RadMenuItem()
                {
                    Text = $"New test for {serverType}",
                    Tag = serverType
                };

                this.radMenuItem4.Items.Add(newMenuItem);

                newMenuItem.Click += new System.EventHandler(this.mnuCreateNewTest_Click);
            }
        }

        private void RadPageView1_SelectedPageChanged(object sender, EventArgs e)
        {
            TestTabPage selectedTestTab = this.radPageView1.SelectedPage as TestTabPage;

            TestManager selectedTest = null;

            if (selectedTestTab != null)
            {
                selectedTest = selectedTestTab.TestManager;
            }

            this.activeTestTab = selectedTestTab;

            this.activeTest = selectedTest;
            UpdatePlayButtons();

            UpdateStatistics(this.activeTest, true);
        }

        private void UpdatePlayButtons()
        {
            var playEnabled = this.activeTest?.TestPlayStatuse != Definitions.TestPlayStatuses.Playing;

            var pauseEnabled = this.activeTest?.TestPlayStatuse == Definitions.TestPlayStatuses.Playing;

            var stopEnabled =
                this.activeTest?.TestPlayStatuse == Definitions.TestPlayStatuses.Playing ||
                this.activeTest?.TestPlayStatuse == Definitions.TestPlayStatuses.Paused;

            //...

            btnPlayTest.Enabled = playEnabled;

            btnPauseTest.Enabled = pauseEnabled;

            btnStopTest.Enabled = stopEnabled;

            this.lblPlayStatus.Text = $"{activeTest?.TestPlayStatuse}";

            this.playProgressbar.Parent.Visible = activeTest?.TestPlayStatuse != Definitions.TestPlayStatuses.Stoped;
        }

        private void MainDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.appManager.Dispose();
        }

        private void AddTestPage(TestManager test)
        {
            var newTabPage = new TestTabPage(test);
            newTabPage.Text = test.TestProject.TestOptions.TestName;
            if (test.TestProject.IsTemp)
            {
                newTabPage.Text += " *";
            }

            newTabPage.Title = newTabPage.Text;
            radPageView1.ThemeName = "Windows7";
            radPageView1.Controls.Add(newTabPage);
            radPageView1.SelectedPage = newTabPage;

            var orderControl = new UI.TestContainerControl(test);
            orderControl.Dock = DockStyle.Fill;
            newTabPage.Controls.Add(orderControl);
            orderControl.OnTestStatusChanged += TestControl_OnTestStatusChanged;
            newTabPage.TestContainer = orderControl;
            UpdateStatistics(test, true);
        }

        private void TestControl_OnTestStatusChanged(TestContainerControl testContainer, int totalSteps, int doneSteps)
        {
            UpdatePlayButtons();

            if (testContainer == activeTestTab.TestContainer)
            {
                if (totalSteps > 0)
                {
                    int progressValue = (int)((100f / totalSteps) * doneSteps);

                    if (progressValue > 100)
                    {
                        progressValue = 100;
                    }

                    playProgressbar.Maximum = 100;
                    playProgressbar.Value1 = progressValue;
                    playProgressbar.Text = $"%{progressValue}";
                }
            }

            UpdateStatistics(testContainer.TestManager);
        }

        private void btnCreateNewTest_Click(object sender, EventArgs e)
        {
            AddTest(Definitions.KnownOnlineShops.Snap);
        }

        private void mnuCreateNewTest_Click(object sender, EventArgs e)
        {
            var requestedServerType = (Definitions.KnownOnlineShops)(sender as RadMenuItem).Tag;

            AddTest(requestedServerType);
        }

        private void AddTest(Definitions.KnownOnlineShops type)
        {
            var tempTest = this.appManager.AddTest(type, true);

            tempTest.TestProject.OrderPattern.PatternName = tempTest.TestProject.TestOptions.TestName;
            tempTest.TestProject.OrderPattern.PredifinedOrderPatterns = TestContainerControl.LoadPredifinedPatterns(type);

            var wizard = new DataWizardDialog(tempTest, tempTest.TestProject);

            wizard.Text = "New Test";

            if (wizard.ShowDialog() == DialogResult.OK)
            {
                this.appManager.AddTest(tempTest);

                AddTestPage(tempTest);

                tempTest.SaveTestProject();

                appManager.UpdateTestList();

                //Save patterns to predefined patterns:
                tempTest.TestProject.OrderPattern.PatternName = tempTest.TestProject.TestOptions.TestName;
                TestContainerControl.SavePatternToPredifinedPatterns(tempTest.TestProject.OrderPattern, type);
            }
        }

        private void btnPlayTest_Click(object sender, EventArgs e)
        {
            if (this.activeTest != null)
            {
                try
                {
                    this.activeTest.Start();

                    UpdatePlayButtons();
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        private void btnPauseTest_Click(object sender, EventArgs e)
        {
            if (this.activeTest != null)
            {
                try
                {
                    this.activeTest.Pause();

                    UpdatePlayButtons();
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        private void btnStopTest_Click(object sender, EventArgs e)
        {
            if (this.activeTest != null)
            {
                try
                {
                    this.activeTest.Stop();

                    UpdatePlayButtons();
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        private void ShowError(string message)
        {
            Utils.ShowError(message);
        }

        private void btnSaveTestToFile_Click(object sender, EventArgs e)
        {
            if (this.activeTest != null)
            {
                try
                {
                    var testProj = activeTest.TestProject;

                    var saveFilePath = testProj.SaveFilePath;

                    if (testProj.IsTemp)
                    {
                        var sdlg = new SaveFileDialog();
                        sdlg.Filter = "Olt test file|*.dtf";
                        if (sdlg.ShowDialog() == DialogResult.OK)
                        {
                            saveFilePath = sdlg.FileName;
                        }
                        else
                        {
                            return;
                        }

                        testProj.SaveFilePath = saveFilePath;
                    }

                    activeTest.TestProject.TestOptions.TestName = Path.GetFileNameWithoutExtension(saveFilePath);

                    activeTestTab.Text = activeTest.TestProject.TestOptions.TestName;

                    activeTest.SaveTestProject(testProj, saveFilePath);

                    appManager.UpdateTestList();
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        private void btnOpenTestFromFile_Click(object sender, EventArgs e)
        {
            try
            {
                var odlg = new OpenFileDialog();

                odlg.Filter = "Olt test file|*.dtf";

                if (odlg.ShowDialog() == DialogResult.OK)
                {
                    var projFilePath = odlg.FileName;

                    var fullProj = this.appManager.ImportTestFromFile(projFilePath);

                    if (fullProj != null)
                    {
                        AddTestPage(fullProj);
                    }
                }
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex.Message);
            }
        }

        private void mnuSaveTestToFile_Click(object sender, EventArgs e)
        {
            btnSaveTestToFile.PerformClick();
        }

        private void mnuOpenTestFromFile_Click(object sender, EventArgs e)
        {
            btnOpenTestFromFile.PerformClick();
        }

        private void radPageView1_PageRemoving(object sender, RadPageViewCancelEventArgs e)
        {
            if (!RemoveTab(this.activeTestTab))
            {
                e.Cancel = true;
            }
        }

        private bool RemoveTab(TestTabPage tab)
        {
            if (tab == null)
            {
                return false;
            }

            var testNotSaved = tab.TestManager.TestProject.IsTemp;

            if (testNotSaved)
            {
                var resp = Utils.AskQuestion($"Delete '{tab.TestManager.TestProject.TestOptions.TestName}'?{Environment.NewLine}You cannot undo this action.");

                if (resp != DialogResult.Yes)
                {
                    return false;
                }
            }

            try
            {
                appManager.RemoveTest(tab.TestManager);
            }
            catch (Exception ex)
            {
                Utils.ShowError(ex.Message);

                return false;
            }

            return true;
        }

        private void btnDuplicateTestProject_Click(object sender, EventArgs e)
        {
            if (activeTest != null)
            {
                var newTest = this.activeTest.TestProject.Clone() as TestProject;

                newTest.Orders.Clear();

                var testProj = appManager.CreateTestManager(newTest.OnlineShop);

                testProj.TestProject = newTest;

                testProj.TestProject.SaveFilePath = null;

                testProj.TestProject.TempFilePath = Path.GetTempFileName();

                this.appManager.AddTest(testProj);

                AddTestPage(testProj);

                testProj.SaveTestProject();

                appManager.UpdateTestList();
            }
        }

        private void MainDialog_Load(object sender, EventArgs e)
        {
        }

        protected override void OnShown(EventArgs e)
        {
            try
            {
                System.Windows.Forms.Application.DoEvents();

                foreach (var testProject in this.appManager.LoadLastOpenedTestProjects())
                {
                    this.AddTestPage(testProject);

                    System.Windows.Forms.Application.DoEvents();
                }
            }
            finally
            {
                radWaitingBar1.Hide();
            }
        }

        private DataDialog incommintMessageLogs;

        private void commandBarButton2_Click(object sender, EventArgs e)
        {
            if (incommintMessageLogs == null || incommintMessageLogs.IsDisposed)
            {
                if (incommintMessageLogs != null && !incommintMessageLogs.IsDisposed)
                {
                    incommintMessageLogs.Dispose();
                }

                var logControl = new ServerLogsControl(null);
                incommintMessageLogs = new DataDialog(logControl, "Server Incomming Messages");
                logControl.ParentDialog = incommintMessageLogs;
                incommintMessageLogs.SingleInstance = true;
                incommintMessageLogs.Show();
            }
            else
            {
                incommintMessageLogs.TopMost = true;
                incommintMessageLogs.TopMost = false;
                incommintMessageLogs.Show();
            }
        }

        private void btnDBConnection_Click(object sender, EventArgs e)
        {
            appManager.EditDBConnectionUsingUI();
        }

        private void UpdateStatistics(TestManager test, bool forceUpdate = false)
        {
            if (test == null)
            {
                lblStatistics_OrderCount.Text = "Orders: N/A";
                lblStatistics_ACKCount.Text = "ACK: N/A";
                lblStatistics_PickCount.Text = "Pick: N/A";
                lblStatistics_RejectCount.Text = "Reject: N/A";
                lblStatistics_EditCount.Text = "Edit: N/A";
                lblStatistics_SendCount.Text = "Sent: N/A";
            }
            else if (test == this.activeTest || forceUpdate)
            {
                lblStatistics_OrderCount.Text = "Orders: " + test.TestProject.Orders.Count.ToString("N0");
                lblStatistics_ACKCount.Text = "ACK: " + test.TestProject.Orders.Where(o => o.AckTime != DateTime.MinValue).Count().ToString("N0");
                lblStatistics_PickCount.Text = "Pick: " + test.TestProject.Orders.Where(o => o.PickTime != DateTime.MinValue).Count().ToString("N0");
                lblStatistics_RejectCount.Text = "Reject: " + test.TestProject.Orders.Where(o => o.Rejected || o.RejectedByVendor).Count().ToString("N0");
                lblStatistics_EditCount.Text = "Edit: " + test.TestProject.Orders.Where(o => o.EditActivity != null).Count().ToString("N0");
                lblStatistics_SendCount.Text = "Sent: " + test.TestProject.Orders.Where(o => o.SendSuccessActivity != null).Count().ToString("N0");
            }
            else
            {
                lblStatistics_OrderCount.Text = "N/A";
            }
        }

    }
}
