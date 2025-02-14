using System;
using System.Collections.Generic;
using System.Windows.Forms;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.WinForms;
using LiveChartsCore.Defaults;
using System.Collections.ObjectModel;
using System.Drawing;
using LiveChartsCore.SkiaSharpView.Painting;
using SkiaSharp;
using LiveChartsCore.Drawing;
using System.Linq;

namespace LiveChartExamples
{
    public partial class Form1 : Form, IDiag
    {
        // Observable collections for real-time updates
        private ObservableCollection<ObservablePoint> receivedOrdersData;
        private ObservableCollection<ObservablePoint> processedOrdersData;
        private ObservableCollection<ObservablePoint> dbWriteTimeData;
        private ObservableCollection<ObservablePoint> totalProcessedData;
        private ObservableCollection<ObservablePoint> queuedOrdersData;
        private ObservableCollection<ObservablePoint> apiCallTimeData;

        // Chart controls
        private CartesianChart ordersChart;
        //private CartesianChart queueChart;

        // Time tracking
        private DateTime startTime;

        // Add these properties to track the time window
        private double currentStartHour = -1;  // -1 indicates no data yet

        public Form1()
        {
            InitializeComponent();
            InitializeChartData();
            SetupCharts();
            startTime = DateTime.Now;
        }

        private void InitializeChartData()
        {
            receivedOrdersData = new ObservableCollection<ObservablePoint>();
            processedOrdersData = new ObservableCollection<ObservablePoint>();
            dbWriteTimeData = new ObservableCollection<ObservablePoint>();
            totalProcessedData = new ObservableCollection<ObservablePoint>();
            queuedOrdersData = new ObservableCollection<ObservablePoint>();
            apiCallTimeData = new ObservableCollection<ObservablePoint>();
        }

        private void SetupCharts()
        {
            var commonXAxis = new Axis
            {
                Labeler = value => $"{(int)value:00}:{((int)((value - (int)value) * 60)):00}",
                MinStep = 0.25, // Show marks every 15 minutes
                UnitWidth = 1,
            };

            // Orders Chart (Received vs Processed)
            ordersChart = new CartesianChart
            {
                Series = new ISeries[]
                {
                    new LineSeries<ObservablePoint>
                    {
                        Name = "Received Orders",
                        Values = receivedOrdersData,
                        Fill = null,
                        GeometrySize = 0,
                    },
                    new LineSeries<ObservablePoint>
                    {
                        Name = "Processed Orders",
                        Values = processedOrdersData,
                        Fill = null,
                        GeometrySize = 0,
                    },
                    new LineSeries<ObservablePoint>
                    {
                        Name = "Total Processed Today",
                        Values = totalProcessedData,
                        Fill = null,
                        GeometrySize = 0,
                    },
                    new LineSeries<ObservablePoint>
                    {
                        Name = "Queued Orders",
                        Values = queuedOrdersData,
                        Fill = null,
                        GeometrySize = 0,
                    },
                    new LineSeries<ObservablePoint>
                    {
                        Name = "DB Write Time (ms)",
                        Values = dbWriteTimeData,
                        Fill = null,
                        GeometrySize = 0,
                    },
                    new LineSeries<ObservablePoint>
                    {
                        Name = "API Call Time (ms)",
                        Values = apiCallTimeData,
                        Fill = null,
                        GeometrySize = 0,
                    },
                },
                XAxes = new Axis[] { commonXAxis }
            };

            // Layout the charts
            TableLayoutPanel chartLayout = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                RowCount = 2,
                ColumnCount = 2
            };

            chartLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 50F));
            chartLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50F));

            ordersChart.Dock = DockStyle.Fill;
            //queueChart.Dock = DockStyle.Fill;

            chartLayout.Controls.Add(ordersChart, 0, 0);

            this.Controls.Add(chartLayout);
        }

        // Methods to update the charts
        public void UpdateReceivedOrders(int count)
        {
            DateTime now = DateTime.Now;
            double hourPosition = now.Hour + (now.Minute / 60.0);
            this.Invoke((MethodInvoker)delegate
            {
                receivedOrdersData.Add(new ObservablePoint(hourPosition, count));
                UpdateChartLimits(hourPosition);

                // Remove points that are too old (more than 24 hours)
                while (receivedOrdersData.Count > 0 &&
                       receivedOrdersData[0].X < hourPosition - 24)
                {
                    receivedOrdersData.RemoveAt(0);
                }
            });
        }

        public void UpdateProcessedOrders(int count)
        {
            DateTime now = DateTime.Now;
            double hourPosition = now.Hour + (now.Minute / 60.0);
            this.Invoke((MethodInvoker)delegate
            {
                processedOrdersData.Add(new ObservablePoint(hourPosition, count));
                UpdateChartLimits(hourPosition);

                // Remove points that are too old (more than 24 hours)
                while (processedOrdersData.Count > 0 &&
                       processedOrdersData[0].X < hourPosition - 24)
                {
                    processedOrdersData.RemoveAt(0);
                }
            });
        }

        public void UpdateDbWriteTime(double milliseconds)
        {
            DateTime now = DateTime.Now;
            double hourPosition = now.Hour + (now.Minute / 60.0);
            this.Invoke((MethodInvoker)delegate
            {
                dbWriteTimeData.Add(new ObservablePoint(hourPosition, milliseconds));
                if (dbWriteTimeData.Count > 1440)
                    dbWriteTimeData.RemoveAt(0);
            });
        }

        public void UpdateTotalProcessedToday(int count)
        {
            DateTime now = DateTime.Now;
            double hourPosition = now.Hour + (now.Minute / 60.0);
            this.Invoke((MethodInvoker)delegate
            {
                totalProcessedData.Add(new ObservablePoint(hourPosition, count));
                if (totalProcessedData.Count > 1440)
                    totalProcessedData.RemoveAt(0);
            });
        }

        public void UpdateQueuedOrders(int count)
        {
            DateTime now = DateTime.Now;
            double hourPosition = now.Hour + (now.Minute / 60.0);
            this.Invoke((MethodInvoker)delegate
            {
                queuedOrdersData.Add(new ObservablePoint(hourPosition, count));
                if (queuedOrdersData.Count > 1440)
                    queuedOrdersData.RemoveAt(0);
            });
        }

        public void UpdateApiCallTime(double milliseconds)
        {
            DateTime now = DateTime.Now;
            double hourPosition = now.Hour + (now.Minute / 60.0);
            this.Invoke((MethodInvoker)delegate
            {
                apiCallTimeData.Add(new ObservablePoint(hourPosition, milliseconds));
                if (apiCallTimeData.Count > 1440)
                    apiCallTimeData.RemoveAt(0);
            });
        }

        private MainLoop mainLoop;

        private void button1_Click(object sender, EventArgs e)
        {
            UpdateApiCallTime(0);
            UpdateDbWriteTime(0);
            UpdateProcessedOrders(0);
            UpdateTotalProcessedToday(0);
            UpdateQueuedOrders(0);
            UpdateReceivedOrders(0);

            mainLoop = new MainLoop(this);
            mainLoop.Start();
        }


        public void ClearDailyData()
        {
            this.Invoke((MethodInvoker)delegate
            {
                receivedOrdersData.Clear();
                processedOrdersData.Clear();
                dbWriteTimeData.Clear();
                totalProcessedData.Clear();
                queuedOrdersData.Clear();
                apiCallTimeData.Clear();
                startTime = DateTime.Now;
            });
        }

        // Add this method to update the X-axis limits
        private void UpdateChartLimits(double hourPosition)
        {
            if (currentStartHour == -1)
            {
                currentStartHour = hourPosition;
            }

            // Update all charts' X-axis limits
            foreach (var chart in new[] { ordersChart })
            {
                var xAxis = chart.XAxes.First();
                xAxis.MinLimit = currentStartHour;      // Start from first data point
                xAxis.MaxLimit = hourPosition + 0.1;    // End at current time (adding small padding)
            }
        }

        private int receivedOrders;
        private DateTime lastReceiveReportTime;
        public void OnOrdersReceived(int count)
        {
            receivedOrders += count;
            if (DateTime.Now - lastReceiveReportTime > TimeSpan.FromSeconds(60))
            {
                lastReceiveReportTime = DateTime.Now;
                UpdateReceivedOrders(receivedOrders);
                receivedOrders = 0;
            }
        }

        private int processedCount;
        private DateTime lastProcessedReportTime;
        public void OnOrderProcessed(int count)
        {
            processedCount += count;
            if ((DateTime.Now - lastProcessedReportTime) > TimeSpan.FromSeconds(60))
            {
                lastProcessedReportTime = DateTime.Now;
                UpdateProcessedOrders(processedCount);
                processedCount = 0;
            }
        }

        private int notProcessedCount;
        private DateTime lastNotProcessedReportTime;
        public void OnNotProcessedDetected(int count)
        {
            notProcessedCount = count;
            if ((DateTime.Now - lastNotProcessedReportTime) > TimeSpan.FromSeconds(60))
            {
                lastNotProcessedReportTime = DateTime.Now;
                UpdateQueuedOrders(notProcessedCount);
                notProcessedCount = 0;
            }
        }

        private List<int> dbWriteTimes = new List<int>();
        private DateTime lastWriteToDbReportTime;
        public void OnOrderWroteToDb(int totalWriteTime)
        {
            dbWriteTimes.Add(totalWriteTime);
            if ((DateTime.Now - lastWriteToDbReportTime) > TimeSpan.FromSeconds(60))
            {
                lastWriteToDbReportTime = DateTime.Now;
                UpdateDbWriteTime((int)(dbWriteTimes.Average()));
                dbWriteTimes.Clear();
            }
        }

        private List<int> apiCallTimes = new List<int>();
        private DateTime lastApiCallReportTime;
        public void OnOrderSendToApi(int delay)
        {
            apiCallTimes.Add(delay);
            if ((DateTime.Now - lastApiCallReportTime) > TimeSpan.FromSeconds(60))
            {
                lastApiCallReportTime = DateTime.Now;
                UpdateApiCallTime((int)(apiCallTimes.Average()));
                apiCallTimes.Clear();
            }
        }
    }
}
