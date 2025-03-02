using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DataCache
{
    public partial class Form1 : Form
    {
        private BasicData basicData;
        private CommonAPLMethods commonAplMethods;
        private ResultPriceChangeListAutoReloadDataCache priceAutoReloadDataCache;
        private ResultStockQtyChangeListAutoReloadDataCache StockAutoReloadDataCache;
        private DeactiveItemAutoReloadDataCache deactiveItemAutoReloadDataCache;
        private StockPriceManualReloadDataCache stockPriceManualReloadDataCache;
        private readonly DataCacheWatcher dataCacheWatcher;
        private bool showLogs = true;

        public Form1()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
            Logger.OnNewLog += Logger_OnNewLog;
            this.basicData = new BasicData();
            this.commonAplMethods = new CommonAPLMethods();
            this.priceAutoReloadDataCache = new ResultPriceChangeListAutoReloadDataCache(basicData, TimeSpan.FromHours(1), TimeSpan.FromSeconds(10));
            this.StockAutoReloadDataCache = new ResultStockQtyChangeListAutoReloadDataCache(basicData, TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10));
            this.deactiveItemAutoReloadDataCache = new DeactiveItemAutoReloadDataCache(TimeSpan.FromMinutes(1), TimeSpan.FromSeconds(10), basicData, commonAplMethods);
            this.stockPriceManualReloadDataCache = new StockPriceManualReloadDataCache(basicData);
            this.dataCacheWatcher = new DataCacheWatcher(TimeSpan.FromSeconds(30), priceAutoReloadDataCache, StockAutoReloadDataCache, deactiveItemAutoReloadDataCache);
            this.dataCacheWatcher.OnDataReady += DataCacheWatcher_OnDataReady;
        }

        private void Logger_OnNewLog(string logMessage)
        {
            if (showLogs)
            {
                textBox1.Text += logMessage + Environment.NewLine;
                textBox1.SelectionStart = textBox1.Text.Length;
                textBox1.ScrollToCaret();
            }
        }

        private int processCount;

        private void DataCacheWatcher_OnDataReady(object sender, EventArgs e)
        {
            var allRetailStoreIDs = new List<int>() {
                1, 2, 4, 8, 9,
                12, 15, 18, 19, 22,
                26, 29, 32, 38, 52,
                58, 66, 74 };

            for (int i = 0; i < allRetailStoreIDs.Count; i += 5)
            {
                Logger.Log("-------->");
                var nextRetailStoreIds = allRetailStoreIDs.Skip(i).Take(5);

                stockPriceManualReloadDataCache.RetailStoreIds = nextRetailStoreIds.ToList();

                stockPriceManualReloadDataCache.ReloadData();

                var allDataLoaded =
                    priceAutoReloadDataCache.IsReady &&
                    StockAutoReloadDataCache.IsReady &&
                    deactiveItemAutoReloadDataCache.IsReady &&
                    stockPriceManualReloadDataCache.IsReady;

                if (allDataLoaded)
                {

                }

                Logger.Log("<--------");
            }


            processCount++;
            label1.Text = processCount.ToString("N0");
            Logger.Log("==========");
        }

        private void lnkClearLogs_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            textBox1.Clear();
        }

        private void lnkStartStop_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            showLogs = !showLogs;
            lnkStartStop.Text = showLogs ? "Stop logs" : "Start logs";

        }
    }
}
