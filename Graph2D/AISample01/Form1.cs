using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test2DGraph
{
    public partial class Form1 : Form
    {
        private CustomChart chartControl;
        private Timer updateTimer;
        private List<PointF> orderData = new List<PointF>();
        private float timeCounter = 0;
        private int currentMinuteOrders = 0;

        public Form1()
        {
            InitializeComponent();
            InitializeChart();
            InitializeOrderMonitor();
            
            // Add initial point to make the chart visible immediately
            orderData.Add(new PointF(0, 0));
            chartControl.SetData(orderData);
        }

        private void InitializeOrderMonitor()
        {
            updateTimer = new Timer();
            updateTimer.Interval = 1000; // Update every second
            updateTimer.Tick += UpdateTimer_Tick;
            updateTimer.Start();
        }

        public void AddDataCount(int count)
        {
            currentMinuteOrders += count;
            orderData.Add(new PointF(timeCounter / 60, currentMinuteOrders));
            chartControl.SetData(orderData);
        }

        private void UpdateTimer_Tick(object sender, EventArgs e)
        {
            timeCounter++;

            // Every 60 seconds, add a new point
            if (timeCounter % 60 == 0)
            {
                // Add the completed minute's data
                orderData.Add(new PointF(timeCounter / 60, currentMinuteOrders));
                currentMinuteOrders = 0; // Reset counter for next minute

                // Keep last 60 minutes of data
                if (orderData.Count > 60)
                {
                    orderData.RemoveAt(0);
                }

                // Add a new point for the current minute
                orderData.Add(new PointF((timeCounter / 60) + 1, 0));
                chartControl.SetData(orderData);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            updateTimer.Stop();
            base.OnFormClosing(e);
        }

        private void InitializeChart()
        {
            chartControl = new CustomChart
            {
                Location = new Point(20, 20),
                Size = new Size(600, 400),
                BackColor = Color.White,
                GridColor = Color.LightGray,
                TextColor = Color.Black,
                BorderSize = 1,
                GridLinesCount = 10,
                BorderColor = Color.Black,
                Title = "Orders per Minute",
                YAxisLabel = "Orders",
                XAxisLabel = "Time (minutes)",
                Dock = DockStyle.Fill,
            };
            
            this.Controls.Add(chartControl);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentMinuteOrders++;
        }
    }
}
