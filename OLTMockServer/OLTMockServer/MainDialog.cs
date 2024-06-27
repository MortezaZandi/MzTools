using OLTMockServer.DataStructures;
using OLTMockServer.UI;
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
            newTabPage.Text = $"{test.TestProject.TestOptions.TestName}{this.appManager.GetNextTestNumber()}";
            newTabPage.Title = newTabPage.Text;
            radPageView1.ThemeName = "Windows7";
            radPageView1.Controls.Add(newTabPage);
            radPageView1.SelectedPage = newTabPage;

            var orderControl = new UI.TestContainerControl(test);
            orderControl.Dock = DockStyle.Fill;
            newTabPage.Controls.Add(orderControl);
            orderControl.OnTestStatusChanged += TestControl_OnTestStatusChanged;
        }

        private void TestControl_OnTestStatusChanged(object sender, EventArgs e)
        {
            UpdatePlayButtons();
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
            var newTest = this.appManager.AddTest(type, true);

            var wizard = new DataWizardDialog(newTest, newTest.TestProject);

            wizard.Text = "New Test";

            if (wizard.ShowDialog() == DialogResult.OK)
            {
                this.appManager.AddTest(newTest);

                AddTestPage(newTest);
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

                    activeTest.SaveTestProject(testProj, saveFilePath);
                }
                catch (Exception ex)
                {
                    ShowError(ex.Message);
                }
            }
        }

        private void btnOpenTestFromFile_Click(object sender, EventArgs e)
        {
            var odlg = new OpenFileDialog();

            odlg.Filter = "Olt test file|*.dtf";

            if (odlg.ShowDialog() == DialogResult.OK)
            {
                var projFilePath = odlg.FileName;

                var defaultProj = XMLDataSerializer.Deserialize<TestProject>(projFilePath);

                var fullProj = appManager.CreateTestManager(defaultProj.OnlineShop);

                fullProj.ImportFromFile(projFilePath);

                if (fullProj != null)
                {
                    appManager.AddTest(fullProj);

                    AddTestPage(fullProj);
                }
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
            var resp = Utils.AskQuestion($"Are you sure you want to delete '{radPageView1.SelectedPage.Text}'?{Environment.NewLine}You cannot undo this action.");

            if (resp != DialogResult.Yes)
            {
                e.Cancel = true;
            }
        }
    }
}
