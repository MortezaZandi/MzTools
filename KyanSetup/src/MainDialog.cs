using KyanSetup.UI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace KyanSetup
{
    public partial class MainDialog : Form
    {
        private List<Page> pages;
        private Page currentPage;

        public MainDialog()
        {
            SplashScreen.ShowSplashScreen();

            InitializeComponent();
        }

        private void MainDialog_Load(object sender, EventArgs e)
        {
            CreatePages();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            SplashScreen.HideSplashScreen();
        }

        private void CreatePages()
        {
            pages = new List<Page>();

            var homePage = new Page();
            homePage.PageTitle = "Kyan CMS Installation";
            homePage.PageDescription = "By using this installation wizard you can install a new CMS or Update existing CMS.";
            homePage.PageDetailControl = new HomePageControl();
            homePage.PageItemControl = new NavListItemControl()
            {
                Text = "Home",
            };

            var featureSelectionPage = new Page();
            featureSelectionPage.PageTitle = "Feature Selection";
            featureSelectionPage.PageDescription = "Select the tools and features you want to install within this setup.";
            featureSelectionPage.PageDetailControl = new FeatureSelectionPageControl();
            featureSelectionPage.PageItemControl = new NavListItemControl()
            {
                Text = featureSelectionPage.PageTitle,
            };

            var installationTypePage = new Page();
            installationTypePage.PageTitle = "Installation Type";
            installationTypePage.PageDescription = "Select the path of existing CMS or new path to install a new CMS.";
            installationTypePage.PageDetailControl = new InstallationTypePageControl();
            installationTypePage.PageItemControl = new NavListItemControl()
            {
                Text = installationTypePage.PageTitle,
            };


            var createDatabasePage = new Page();
            createDatabasePage.PageTitle = "Create Database";
            createDatabasePage.PageDescription = "Create new database or update an existing database to the last version.";
            createDatabasePage.PageDetailControl = new CreateDatabasePageControl();
            createDatabasePage.PageItemControl = new NavListItemControl()
            {
                Text = createDatabasePage.PageTitle,
            };

            var baseicInformationPage = new Page();
            baseicInformationPage.PageTitle = "Basic Information";
            baseicInformationPage.PageDescription = "Set Important primitive information to start using new CMS.";
            baseicInformationPage.PageDetailControl = new BasicInformationPageControl();
            baseicInformationPage.PageItemControl = new NavListItemControl()
            {
                Text = baseicInformationPage.PageTitle,
            };

            var defaultConfigPage = new Page();
            defaultConfigPage.PageTitle = "Default Configs";
            defaultConfigPage.PageDescription = "Customize your RMC with changing default config values.";
            defaultConfigPage.PageDetailControl = new DefaultConfigsPageControl();
            defaultConfigPage.PageItemControl = new NavListItemControl()
            {
                Text = defaultConfigPage.PageTitle,
            };

            var licensePage = new Page();
            licensePage.PageTitle = "License";
            licensePage.PageDescription = "Register this installation with your license.";
            licensePage.NextButtonTitle = "Install";
            licensePage.PageDetailControl = new LicensePageControl();
            licensePage.PageItemControl = new NavListItemControl()
            {
                Text = licensePage.PageTitle,
            };

            var installationPage = new Page();
            installationPage.PageTitle = "Installation";
            installationPage.PageDescription = "Installs the selected features.";
            installationPage.NextButtonTitle = "Finish";
            installationPage.PageDetailControl = new InstallationPageControl();
            installationPage.PageItemControl = new NavListItemControl()
            {
                Text = installationPage.PageTitle,
            };

            pages.Add(homePage);
            pages.Add(installationTypePage);
            pages.Add(featureSelectionPage);
            pages.Add(createDatabasePage);
            pages.Add(baseicInformationPage);
            pages.Add(defaultConfigPage);
            pages.Add(licensePage);
            pages.Add(installationPage);

            foreach (var page in pages)
            {
                pageNavigationList.Controls.Add(page.PageItemControl);
                page.PageItemControl.Dock = DockStyle.Top;
                page.PageItemControl.BringToFront();
                page.PageItemControl.Height = 40;
                page.PageItemControl.Padding = new Padding(20, 0, 0, 0);

                SplashScreen.UpdateStep($"Initializing {page.PageTitle}...");

                Thread.Sleep(1000);
            }

            CurrentPage = FirstPage;
        }

        private void UpdateNavigationButtons()
        {
            btnBack.Enabled = CanGoToPrevPage;
            btnNext.Enabled = CanGoToNextPage;
            btnNext.Text = currentPage.NextButtonTitle;
            btnBack.Text = currentPage.BackButtonTitle;
        }

        private bool CanGoToPrevPage
        {
            get
            {
                if (IsFirstPageReached)
                {
                    return false;
                }

                return currentPage.CanGotoBackPage;
            }
        }

        private bool CanGoToNextPage
        {
            get
            {
                if (IsLastPageReached)
                {
                    return false;
                }

                return currentPage.CanGotoNextPage;
            }
        }

        private Page CurrentPage
        {
            get
            {
                return currentPage;
            }
            set
            {
                if (currentPage != value)
                {
                    if (currentPage != null)
                    {
                        currentPage.PageItemControl.Selected = false;
                        currentPage.OnPageLeaves();
                    }

                    currentPage = value;
                    currentPage.PageItemControl.Selected = true;
                    currentPage.OnPageEntered();

                    pnlPageControl.Controls.Clear();
                    pnlPageControl.Controls.Add(currentPage.PageDetailControl);
                    currentPage.PageDetailControl.Dock = DockStyle.Fill;

                    lblPageTitle.Text = currentPage.PageTitle;
                    lblPageDescription.Text = currentPage.PageDescription;

                    UpdateNavigationButtons();
                }
            }
        }

        private bool IsLastPageReached
        {
            get
            {
                return currentPage == pages.Last();
            }
        }

        private bool IsFirstPageReached
        {
            get
            {
                return currentPage == pages.First();
            }
        }

        private Page FirstPage
        {
            get
            {
                return pages.First();
            }
        }

        private Page NextPage
        {
            get
            {
                if (CanGoToNextPage)
                {
                    return pages[pages.IndexOf(currentPage) + 1];
                }

                return currentPage;
            }
        }

        private Page PrevPage
        {
            get
            {
                if (CanGoToPrevPage)
                {
                    return pages[pages.IndexOf(currentPage) - 1];
                }

                return currentPage;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            try
            {
                if (CanGoToNextPage)
                {
                    CurrentPage = NextPage;
                }
                else
                {
                    throw new Exception("unhandled page state.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            try
            {
                if (CanGoToPrevPage)
                {
                    CurrentPage = PrevPage;
                }
                else
                {
                    throw new Exception("unhandled page state.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            //question
            try
            {
                currentPage.CancelOperation();
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Operation cannot be cancelled, please try again few moment later." + Environment.NewLine + ex.Message);
            }
        }

    }
}
