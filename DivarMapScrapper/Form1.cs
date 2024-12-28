using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Drawing;
using System.Net;
using System.Runtime.InteropServices;
using Microsoft.Web.WebView2.Core;
using Microsoft.Web.WebView2.WinForms;
using System.Web;
using DivarMapScrapper.Services;    // Changed from Services to DivarMapScrapper.Services
using DivarMapScrapper.Models;      // Added for DivarAd model

namespace DivarMapScrapper
{
    public partial class Form1 : Form
    {
        private TextBox txtAddress;
        private Button btnShowOnMap;
        private ChromeDriver driver;
        private Label labelLocation;
        private WebView2 mapView;
        private readonly DivarScraper scraper;
        private readonly MapDisplayer mapDisplayer;

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindowEx(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszWindow);

        public Form1()
        {
            InitializeComponent();

            // Set form properties first
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.Sizable;

            InitializeControls();
            InitializeSelenium();
            scraper = new DivarScraper();
            mapDisplayer = new MapDisplayer(mapView);
        }

        private async void InitializeControls()
        {
            // Calculate dimensions based on screen size
            int padding = 12;
            int topBarHeight = 45;
            int bottomBarHeight = 50;

            txtAddress = new TextBox
            {
                Location = new Point(padding, padding),
                Width = Screen.PrimaryScreen.WorkingArea.Width - 150 - (padding * 2),
                Height = 25,
                Text = "Enter Divar URL here...",
                ForeColor = Color.Gray,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            btnShowOnMap = new Button
            {
                Location = new Point(txtAddress.Right + 10, padding),
                Width = 100,
                Height = 25,
                Text = "Show On Map",
                Anchor = AnchorStyles.Top | AnchorStyles.Right
            };

            mapView = new WebView2
            {
                Location = new Point(padding, topBarHeight),
                Width = Screen.PrimaryScreen.WorkingArea.Width - (padding * 2),
                Height = Screen.PrimaryScreen.WorkingArea.Height - topBarHeight - bottomBarHeight,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            labelLocation = new Label
            {
                Location = new Point(padding, Screen.PrimaryScreen.WorkingArea.Height - bottomBarHeight),
                Width = Screen.PrimaryScreen.WorkingArea.Width - (padding * 2),
                Height = 40,
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right
            };

            // Add event handlers
            txtAddress.GotFocus += (s, e) =>
            {
                if (txtAddress.Text == "Enter Divar URL here...")
                {
                    txtAddress.Text = "https://divar.ir/s/tehran/buy-apartment/shemiran-now";
                    txtAddress.ForeColor = Color.Black;
                }
            };

            txtAddress.LostFocus += (s, e) =>
            {
                if (string.IsNullOrWhiteSpace(txtAddress.Text))
                {
                    txtAddress.Text = "Enter Divar URL here...";
                    txtAddress.ForeColor = Color.Gray;
                }
            };

            btnShowOnMap.Click += BtnShowOnMap_Click;

            // Handle form resize
            this.Resize += (s, e) =>
            {
                int currentWidth = this.ClientSize.Width;
                int currentHeight = this.ClientSize.Height;

                txtAddress.Width = currentWidth - 150 - (padding * 2);
                btnShowOnMap.Left = txtAddress.Right + 10;

                mapView.Width = currentWidth - (padding * 2);
                mapView.Height = currentHeight - topBarHeight - bottomBarHeight;

                labelLocation.Top = currentHeight - bottomBarHeight;
                labelLocation.Width = currentWidth - (padding * 2);
            };

            // Add controls to form
            Controls.Add(txtAddress);
            Controls.Add(btnShowOnMap);
            Controls.Add(mapView);
            Controls.Add(labelLocation);

            // Initialize WebView2
            await mapView.EnsureCoreWebView2Async();
        }

        private void InitializeSelenium()
        {
            try
            {
                var options = new ChromeOptions();
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--no-sandbox");

                var service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                driver = new ChromeDriver(service, options);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(30);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing WebDriver: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void BtnShowOnMap_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtAddress.Text) || txtAddress.Text == "Enter Divar URL here...")
            {
                MessageBox.Show("Please enter a valid URL", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                btnShowOnMap.Enabled = false;
                var ads = await scraper.ScrapeAdsAsync(txtAddress.Text);
                ads.Add(new DivarAd
                {
                    //"latitude":"35.748714739059","longitude":"51.500870325395"
                    Latitude = 35.748714739059,
                    Longitude = 51.500870325395,
                    Title = "Test",
                    Url = "www.divar.ir",
                    Price = "0",
                });

                if (ads.Any())
                {
                    await mapDisplayer.DisplayAdsOnMap(ads);
                    MessageBox.Show(
                        $"Found {ads.Count} locations\n" +
                        $"In Upper Right Quadrant: {ads.Count(a => a.IsInUpperRightQuadrant)}\n" +
                        $"Outside Quadrant: {ads.Count(a => !a.IsInUpperRightQuadrant)}",
                        "Location Summary",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    MessageBox.Show("No locations found.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                btnShowOnMap.Enabled = true;
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            scraper?.Dispose();
        }
    }
}
