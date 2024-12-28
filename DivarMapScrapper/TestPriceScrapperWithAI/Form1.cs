using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using OpenQA.Selenium.Support.UI;

namespace TestPriceScrapperWithAI
{
    public partial class Form1 : Form
    {
        private readonly BindingList<ProductItem> products = new BindingList<ProductItem>();
        private DataGridView dgvProducts;
        private bool isMonitoring = false;
        private System.Windows.Forms.Timer monitorTimer;
        private NotifyIcon notifyIcon;
        private ChromeDriver driver;
        private ContextMenuStrip contextMenu;
        private Button deleteButton;
        private readonly string saveFilePath = Path.Combine(Application.StartupPath, "products.json");

        public Form1()
        {
            InitializeComponent();
            LoadProducts();
            SetupForm();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            SetupNotifyIcon();
            InitializeWebDriver();
            LoadProducts();
        }

        private void InitializeWebDriver()
        {
            try
            {
                var options = new ChromeOptions();

                // Performance optimizations
                options.AddArgument("--headless");
                options.AddArgument("--disable-gpu");
                options.AddArgument("--no-sandbox");
                options.AddArgument("--disable-dev-shm-usage");
                options.AddArgument("--disable-extensions");
                options.AddArgument("--disable-notifications");
                options.AddArgument("--disable-default-apps");
                options.AddArgument("--disable-popup-blocking");
                options.AddArgument("--blink-settings=imagesEnabled=false"); // Disable images
                options.AddArgument("--disable-javascript"); // Disable JavaScript if not needed

                // SSL settings
                options.AddArgument("--ignore-certificate-errors");
                options.AddArgument("--ignore-ssl-errors");

                var service = ChromeDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;
                service.SuppressInitialDiagnosticInformation = true;

                // Add performance logging
                options.SetLoggingPreference("performance", LogLevel.Off);
                options.SetLoggingPreference("browser", LogLevel.Off);

                driver = new ChromeDriver(service, options);

                // Set aggressive timeouts
                driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error initializing WebDriver: {ex.Message}");
                throw;
            }
        }

        private async Task AddProduct(string url)
        {
            try
            {
                var product = new ProductItem { Url = url };
                await UpdateProductInfo(product);
                products.Add(product);
                SaveProducts();
            }
            catch (Exception ex)
            {
            }
        }

        private async Task UpdateProductInfo(ProductItem product)
        {
            int rowIndex = products.IndexOf(product);

            if (dgvProducts.InvokeRequired)
            {
                dgvProducts.Invoke(new Action(() => HighlightRow(rowIndex, true)));
            }
            else
            {
                HighlightRow(rowIndex, true);
            }

            try
            {
                driver.Manage().Timeouts().PageLoad = TimeSpan.FromSeconds(10);
                driver.Navigate().GoToUrl(product.Url);

                var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

                // Extract seller information
                await Task.Delay(2000);
                product.Seller = ExtractSeller(driver.PageSource);

                // Try to find price without waiting for specific element
                var priceText = ExtractPrice(driver.PageSource);

                if (!string.IsNullOrEmpty(priceText))
                {
                    var oldPrice = product.CurrentPrice;
                    product.CurrentPrice = ParsePersianPrice(priceText);
                    product.LastCheck = DateTime.Now;

                    if (oldPrice != product.CurrentPrice)
                    {
                        if (dgvProducts.InvokeRequired)
                        {
                            dgvProducts.Invoke(new Action(() =>
                            {
                                dgvProducts.Refresh();
                                dgvProducts.InvalidateRow(rowIndex);
                            }));
                        }
                        else
                        {
                            dgvProducts.Refresh();
                            dgvProducts.InvalidateRow(rowIndex);
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"No price found for product: {product.Url}");
                }

                try
                {
                    var nameElement = wait.Until(d => d.FindElement(By.CssSelector("h1")));
                    if (nameElement != null)
                    {
                        product.Name = nameElement.Text.Trim();
                    }
                }
                catch (WebDriverTimeoutException)
                {
                    Console.WriteLine("Name element not found within timeout");
                }
                catch (NoSuchElementException)
                {
                    Console.WriteLine("Name element not found on the page");
                }

                // Save products after successful update
                SaveProducts();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in UpdateProductInfo: {ex.Message}");
            }
            finally
            {
                if (dgvProducts.InvokeRequired)
                {
                    dgvProducts.Invoke(new Action(() => HighlightRow(rowIndex, false)));
                }
                else
                {
                    HighlightRow(rowIndex, false);
                }
            }
        }

        private string ExtractPrice(string pageSource)
        {
            try
            {
                // Array of possible price element patterns
                string[] patterns = new[]
                {
                    @"<span class=""text-h4 ml-1 text-neutral-800"">([^<]+)</span>",
                    @"<span class=""text-h4 ml-1 text-neutral-800"" data-testid=""price-no-discount"">([^<]+)</span>",
                    @"<span class=""text-neutral-800 ml-1 text-h4"" data-testid=""price-final"">([^<]+)</span>"
                    // Add more patterns if needed
                };

                foreach (var pattern in patterns)
                {
                    var regex = new System.Text.RegularExpressions.Regex(pattern);
                    var match = regex.Match(pageSource);

                    if (match.Success)
                    {
                        string priceText = match.Groups[1].Value.Trim();
                        return priceText;
                    }
                }

                Console.WriteLine("No price pattern matched");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting price with regex: {ex.Message}");
            }

            return string.Empty;
        }

        private async Task CheckPrices()
        {
            foreach (var product in products)
            {
                try
                {
                    var oldPrice = product.CurrentPrice;
                    await UpdateProductInfo(product);

                    if (product.CurrentPrice != oldPrice && oldPrice != 0)
                    {
                        NotifyPriceChange(product, oldPrice);
                    }
                }
                catch (Exception ex)
                {
                    ShowBalloonTip(
                        "Error",
                        $"Error checking price for {product.Name}: {ex.Message}",
                        ToolTipIcon.Error
                    );
                }
                await Task.Delay(2000); // Delay between products
            }
            SaveProducts();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            if (!e.Cancel)
            {
                driver?.Quit(); // Clean up Selenium WebDriver
            }
        }

        private void SetupForm()
        {
            // Set form properties
            this.Size = new Size(800, 600);

            // Create main container panel
            var mainPanel = new Panel
            {
                Dock = DockStyle.Fill
            };

            // Create Controls Panel
            var controlsPanel = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                Padding = new Padding(10)
            };

            // Create URL TextBox
            var txtUrl = new TextBox
            {
                Width = 300,
                Location = new Point(10, 20),
                Anchor = AnchorStyles.Left | AnchorStyles.Top
            };

            // Create Add Button
            var btnAdd = new Button
            {
                Text = "Add Product",
                Location = new Point(320, 20),
                Anchor = AnchorStyles.Left | AnchorStyles.Top
            };
            btnAdd.Click += async (s, e) => await AddProduct(txtUrl.Text);

            // Create Monitor Toggle Button
            var btnToggleMonitor = new Button
            {
                Text = "Start Monitoring",
                Location = new Point(420, 20),
                Anchor = AnchorStyles.Left | AnchorStyles.Top
            };
            btnToggleMonitor.Click += (s, e) => ToggleMonitoring(btnToggleMonitor);

            // Create DataGridView Panel
            var gridPanel = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(10),
                RightToLeft = RightToLeft.Yes,
            };

            // Create DataGridView
            dgvProducts = new DataGridView
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                DataSource = products,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                DefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.White,
                    SelectionBackColor = SystemColors.Highlight
                },
                AlternatingRowsDefaultCellStyle = new DataGridViewCellStyle
                {
                    BackColor = Color.WhiteSmoke
                },
                EnableHeadersVisualStyles = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.Fixed3D
            };

            // Set up columns manually
            dgvProducts.AutoGenerateColumns = false;

            dgvProducts.Columns.AddRange(new DataGridViewColumn[]
            {
                new DataGridViewTextBoxColumn
                {
                    Name = "Name",
                    HeaderText = "نام محصول",
                    DataPropertyName = "Name",
                    AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "Seller",
                    HeaderText = "فروشنده",
                    DataPropertyName = "Seller",
                    Width = 120
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "CurrentPrice",
                    HeaderText = "قیمت فعلی",
                    DataPropertyName = "CurrentPrice",
                    Width = 150
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "OldPrice",
                    HeaderText = "قیمت قبلی",
                    DataPropertyName = "OldPrice",
                    Width = 150
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "ChangeAmount",
                    HeaderText = "مقدار تغییر",
                    DataPropertyName = "ChangeAmount",
                    Width = 150
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "ChangePercentage",
                    HeaderText = "درصد تغییر",
                    DataPropertyName = "ChangePercentage",
                    Width = 150
                },
                new DataGridViewTextBoxColumn
                {
                    Name = "LastCheck",
                    HeaderText = "آخرین بررسی",
                    DataPropertyName = "LastCheck",
                    Width = 150
                }
            });

            // Add CellFormatting event handler
            dgvProducts.CellFormatting += (s, e) =>
            {
                if (e.ColumnIndex == dgvProducts.Columns["CurrentPrice"]?.Index ||
                    e.ColumnIndex == dgvProducts.Columns["OldPrice"]?.Index ||
                    e.ColumnIndex == dgvProducts.Columns["ChangeAmount"]?.Index)
                {
                    if (e.Value is long price)
                    {
                        e.Value = string.Format("{0:N0} تومان", price);
                        e.FormattingApplied = true;

                        // Color the cell based on price change
                        if (e.ColumnIndex == dgvProducts.Columns["CurrentPrice"]?.Index)
                        {
                            var row = dgvProducts.Rows[e.RowIndex];
                            var currentPrice = (long)row.Cells["CurrentPrice"].Value;
                            var oldPrice = (long)row.Cells["OldPrice"].Value;

                            if (currentPrice > oldPrice && oldPrice != 0)
                            {
                                e.CellStyle.ForeColor = Color.Red;
                            }
                            else if (currentPrice < oldPrice && oldPrice != 0)
                            {
                                e.CellStyle.ForeColor = Color.Green;
                            }
                        }
                    }
                }
                else if (e.ColumnIndex == dgvProducts.Columns["ChangePercentage"]?.Index)
                {
                    if (e.Value is double percentage)
                    {
                        e.Value = string.Format("{0:F2}%", percentage);
                        e.FormattingApplied = true;
                    }
                }
                else if (e.ColumnIndex == dgvProducts.Columns["LastCheck"]?.Index)
                {
                    if (e.Value is DateTime dateTime)
                    {
                        e.Value = dateTime.ToString("HH:mm:ss");
                        e.FormattingApplied = true;
                    }
                }
            };

            // Create context menu
            contextMenu = new ContextMenuStrip();
            var deleteMenuItem = new ToolStripMenuItem("حذف", null, DeleteMenuItem_Click);
            contextMenu.Items.Add(deleteMenuItem);

            // Assign context menu to DataGridView
            dgvProducts.ContextMenuStrip = contextMenu;

            // Create delete button
            deleteButton = new Button
            {
                Text = "حذف انتخاب شده",
                Dock = DockStyle.Bottom
            };
            deleteButton.Click += DeleteButton_Click;

            // Add button to form
            Controls.Add(deleteButton);

            // Add controls to panels
            controlsPanel.Controls.AddRange(new Control[] { txtUrl, btnAdd, btnToggleMonitor });
            gridPanel.Controls.Add(dgvProducts);

            // Add panels to main panel
            mainPanel.Controls.Add(gridPanel);     // Add grid panel first
            mainPanel.Controls.Add(controlsPanel); // Add controls panel last (will be on top)

            // Add main panel to form
            this.Controls.Add(mainPanel);

            // Setup Timer
            monitorTimer = new System.Windows.Forms.Timer
            {
                Interval = 15000 // 1 hour
            };
            monitorTimer.Tick += async (s, e) => await CheckPrices();
        }

        private void SetupNotifyIcon()
        {
            notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Application, // You can use your own icon
                Visible = true,
                Text = "Price Monitor"
            };

            // Create context menu for notify icon
            var contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Open", null, (s, e) => { this.Show(); this.WindowState = FormWindowState.Normal; });
            contextMenu.Items.Add("Exit", null, (s, e) => Application.Exit());
            notifyIcon.ContextMenuStrip = contextMenu;

            // Double click to open app
            notifyIcon.DoubleClick += (s, e) => { this.Show(); this.WindowState = FormWindowState.Normal; };

            // Handle form minimize to tray
            this.FormClosing += (s, e) =>
            {
                if (isMonitoring)
                {
                    e.Cancel = true;
                    this.Hide();
                    ShowBalloonTip("Price Monitor", "Application is still monitoring prices in the background.", ToolTipIcon.Info);
                }
                else
                {
                    notifyIcon.Dispose();
                }
            };
        }

        private void ShowBalloonTip(string title, string message, ToolTipIcon icon)
        {
            notifyIcon.BalloonTipTitle = title;
            notifyIcon.BalloonTipText = message;
            notifyIcon.BalloonTipIcon = icon;
            notifyIcon.ShowBalloonTip(5000); // Show for 5 seconds
        }

        private long ParsePersianPrice(string priceText)
        {
            try
            {
                // Persian to English number mapping
                var persianToEnglish = new Dictionary<char, char>
                {
                    {'۰', '0'},
                    {'۱', '1'},
                    {'۲', '2'},
                    {'۳', '3'},
                    {'۴', '4'},
                    {'۵', '5'},
                    {'۶', '6'},
                    {'۷', '7'},
                    {'۸', '8'},
                    {'۹', '9'}
                };

                // Convert Persian numbers to English and remove non-digits
                var englishNumbers = new StringBuilder();
                foreach (char c in priceText)
                {
                    if (persianToEnglish.ContainsKey(c))
                    {
                        englishNumbers.Append(persianToEnglish[c]);
                    }
                    else if (char.IsDigit(c))
                    {
                        englishNumbers.Append(c);
                    }
                }

                var finalNumberString = englishNumbers.ToString();

                if (long.TryParse(finalNumberString, out long result))
                {
                    return result;
                }

                throw new Exception($"Could not parse final number string: {finalNumberString}");
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        private void NotifyPriceChange(ProductItem product, long oldPrice)
        {
            var message = $"{product.Name}\n" +
                         $"Old price: {oldPrice:N0} تومان\n" +
                         $"New price: {product.CurrentPrice:N0} تومان";

            ShowBalloonTip(
                "Price Change Detected!",
                message,
                product.CurrentPrice < oldPrice ? ToolTipIcon.Info : ToolTipIcon.Warning
            );

            File.AppendAllText("price_changes.log",
                $"{DateTime.Now}: {message}\n");
        }

        private void ToggleMonitoring(Button button)
        {
            isMonitoring = !isMonitoring;
            button.Text = isMonitoring ? "Stop Monitoring" : "Start Monitoring";

            if (isMonitoring)
            {
                monitorTimer.Start();
                ShowBalloonTip(
                    "Price Monitor",
                    "Price monitoring started. The application will run in the background.",
                    ToolTipIcon.Info
                );
            }
            else
            {
                monitorTimer.Stop();
                ShowBalloonTip(
                    "Price Monitor",
                    "Price monitoring stopped.",
                    ToolTipIcon.Warning
                );
            }
        }

        private void SaveProducts()
        {
            try
            {
                var json = JsonSerializer.Serialize(products.ToList(), new JsonSerializerOptions
                {
                    WriteIndented = true
                });
                File.WriteAllText(saveFilePath, json);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving products: {ex.Message}");
            }
        }

        private void LoadProducts()
        {
            try
            {
                if (File.Exists(saveFilePath))
                {
                    var json = File.ReadAllText(saveFilePath);
                    var loadedProducts = JsonSerializer.Deserialize<List<ProductItem>>(json);
                    products.Clear();
                    foreach (var product in loadedProducts)
                    {
                        products.Add(product);
                    }
                    
                    // Clear grid selection after loading
                    if (dgvProducts != null && dgvProducts.Rows.Count > 0)
                    {
                        dgvProducts.ClearSelection();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading products: {ex.Message}");
            }
        }

        private void CheckChromeVersion()
        {
            try
            {
                var chromeVersion = driver.Capabilities.GetCapability("browserVersion").ToString();
                var chromedriverVersion = driver.Capabilities.GetCapability("chrome").ToString();
                Console.WriteLine($"Chrome Version: {chromeVersion}");
                Console.WriteLine($"ChromeDriver Version: {chromedriverVersion}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking versions: {ex.Message}");
            }
        }

        private void HighlightRow(int rowIndex, bool highlight)
        {
            if (rowIndex >= 0 && rowIndex < dgvProducts.Rows.Count)
            {
                var row = dgvProducts.Rows[rowIndex];
                if (highlight)
                {
                    row.DefaultCellStyle.BackColor = Color.LightYellow;
                    row.DefaultCellStyle.SelectionBackColor = Color.Yellow;
                    row.DefaultCellStyle.SelectionForeColor = Color.Black;
                    row.DefaultCellStyle.ForeColor = Color.Black; // Ensure text is visible
                }
                else
                {
                    row.DefaultCellStyle.BackColor = dgvProducts.DefaultCellStyle.BackColor;
                    row.DefaultCellStyle.SelectionBackColor = dgvProducts.DefaultCellStyle.SelectionBackColor;
                    row.DefaultCellStyle.ForeColor = dgvProducts.DefaultCellStyle.ForeColor;
                }
            }
        }

        private string ExtractSeller(string pageSource)
        {
            try
            {
                // Regex pattern to match the seller in p tag with specific classes
                string pattern = @"<p class=""text-neutral-700 ml-2 text-subtitle"">([^<]+)</p>";

                var regex = new System.Text.RegularExpressions.Regex(pattern);
                var match = regex.Match(pageSource);

                if (match.Success)
                {
                    string seller = match.Groups[1].Value.Trim();
                    return seller;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error extracting seller: {ex.Message}");
            }

            return "نامشخص";
        }

        private void DeleteMenuItem_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvProducts.SelectedRows)
                {
                    var product = row.DataBoundItem as ProductItem;
                    if (product != null)
                    {
                        products.Remove(product);
                    }
                }
                SaveProducts(); // Save after deletion
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (dgvProducts.SelectedRows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvProducts.SelectedRows)
                {
                    var product = row.DataBoundItem as ProductItem;
                    if (product != null)
                    {
                        products.Remove(product);
                    }
                }
                SaveProducts(); // Save after deletion
            }
        }
    }

    public class ProductItem : INotifyPropertyChanged
    {
        private string name;
        private string url;
        private long currentPrice;
        private long oldPrice;
        private DateTime lastCheck;
        private string seller;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string Url
        {
            get => url;
            set
            {
                url = value;
                OnPropertyChanged(nameof(Url));
            }
        }

        public long CurrentPrice
        {
            get => currentPrice;
            set
            {
                if (currentPrice != value)
                {
                    oldPrice = currentPrice;
                    currentPrice = value;
                    OnPropertyChanged(nameof(CurrentPrice));
                    OnPropertyChanged(nameof(OldPrice));
                    OnPropertyChanged(nameof(ChangeAmount));
                }
            }
        }

        public long OldPrice
        {
            get => oldPrice;
            set
            {
                oldPrice = value;
                OnPropertyChanged(nameof(OldPrice));
            }
        }

        public long ChangeAmount => CurrentPrice - OldPrice;

        public DateTime LastCheck
        {
            get => lastCheck;
            set
            {
                lastCheck = value;
                OnPropertyChanged(nameof(LastCheck));
            }
        }

        public string Seller
        {
            get => seller;
            set
            {
                seller = value;
                OnPropertyChanged(nameof(Seller));
            }
        }

        public double ChangePercentage
        {
            get
            {
                if (oldPrice == 0) return 0;
                return ((double)(currentPrice - oldPrice) / oldPrice) * 100;
            }
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
