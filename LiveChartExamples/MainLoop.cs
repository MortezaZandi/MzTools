using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace LiveChartExamples
{

    internal interface IDiag
    {
        void OnOrdersReceived(int count);
        void OnOrderProcessed(int count);
        void OnNotProcessedDetected(int count);
        void OnOrderWroteToDb(int totalWriteTime);
        void OnOrderSendToApi(int delay);
    }

    internal class MainLoop
    {
        private Timer tmrReceiveOrder = new Timer() { Interval = 1000 };
        private Timer tmrProcess = new Timer() { Interval = 1000 };

        private Random rnd = new Random();
        private Queue<int> orders = new Queue<int>();

        private IDiag diag;
        public MainLoop(IDiag diag)
        {
            this.diag = diag;
        }

        public void Start()
        {
            tmrReceiveOrder.Tick += tmrReceiveOrderr_Tick;
            tmrProcess.Tick += TmrProcess_Tick;
            tmrReceiveOrder.Start();
            tmrProcess.Start();
        }

        private void TmrProcess_Tick(object sender, EventArgs e)
        {
            tmrProcess.Stop();

            if (orders.Count > 0)
            {
                List<int> orders = GetAllOrders();

                for (int i = 0; i < orders.Count; i++)
                {
                    var nextOrder = orders[i];
                    WriteOrderToDb(nextOrder);
                    CallOnlineShop(nextOrder);
                    diag.OnOrderProcessed(1);
                    diag.OnNotProcessedDetected(orders.Count + (orders.Count - i - 1));
                    Application.DoEvents();
                }
            }


            tmrProcess.Start();
        }

        private void CallOnlineShop(int nextOrder)
        {
            var chance = rnd.Next(0, 1000);
            var delay = rnd.Next(0, 1);

            if (chance > 500 && chance < 550)
            {
                delay = rnd.Next(0, 2000);
            }

            Thread.Sleep(delay);
            Application.DoEvents();
            diag.OnOrderSendToApi(delay);
        }

        private void WriteOrderToDb(int nextOrder)
        {
            var totalWriteTime = 0;
            for (int j = 0; j < nextOrder; j++)
            {
                var chance = rnd.Next(0, 1000);
                var delay = rnd.Next(0, 1);

                if (chance > 500 && chance < 550)
                {
                    delay = rnd.Next(0, 2000);
                }

                totalWriteTime += delay;

                Thread.Sleep(delay);

                Application.DoEvents();
            }
            diag.OnOrderWroteToDb(totalWriteTime);
        }

        private List<int> GetAllOrders()
        {
            var orders = new List<int>();
            while (this.orders.Count > 0)
            {
                orders.Add(this.orders.Dequeue());
            }

            return orders;
        }

        private void tmrReceiveOrderr_Tick(object sender, EventArgs e)
        {
            tmrReceiveOrder.Stop();

            var orderCount = rnd.Next(0, 40);

            for (int i = 0; i < orderCount; i++)
            {
                var cost = rnd.Next(0, 2);
                orders.Enqueue(cost);
            }

            diag.OnOrdersReceived(orderCount);

            tmrReceiveOrder.Start();
        }

        private int SendAck()
        {
            return rnd.Next(0, 5);
        }
    }
}
