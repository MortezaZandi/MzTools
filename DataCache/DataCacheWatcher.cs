using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DataCache
{
    public class DataCacheWatcher
    {
        private readonly IDataCache[] dataCaches;
        private System.Timers.Timer timer;
        public event EventHandler OnDataReady;
        public DataCacheWatcher(TimeSpan interval, params IDataCache[] dataCaches)
        {
            this.dataCaches = dataCaches;
            timer = new System.Timers.Timer(interval.TotalMilliseconds);
            timer.Elapsed += OnTimerTicks;
            timer.Start();
        }

        private void OnTimerTicks(object sender, ElapsedEventArgs e)
        {
            try
            {
                timer.Stop();

                if (dataCaches.All(dc => dc.IsReady))
                {
                    Logger.Log("");
                    Logger.Log("All data are ready");
                    OnDataReady?.Invoke(this, EventArgs.Empty);
                }
                else
                {
                    foreach (var item in dataCaches)
                    {
                        if (!item.IsReady)
                        {
                            Logger.Log($"{item.GetType().Name} is not ready");
                        }
                    }
                }
            }
            catch (Exception)
            {
                //log
            }
            finally
            {
                timer.Start();
            }
        }
    }
}
