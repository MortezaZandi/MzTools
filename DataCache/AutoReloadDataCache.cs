using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace DataCache
{
    public abstract class AutoReloadDataCache<TModel> : DataCache<TModel>
    {
        private TimeSpan interval;
        private TimeSpan retryInterval;
        private TimeSpan initialDelay;
        private System.Timers.Timer timer;
        private int maxReloadTryCount = 3;
        private int tryCount = 0;

        protected AutoReloadDataCache(BasicData basicData, TimeSpan interval, TimeSpan initialDelay) : base(basicData)
        {
            this.interval = interval;
            this.initialDelay = initialDelay;
            this.retryInterval = TimeSpan.FromSeconds(10);

            timer = new System.Timers.Timer(

                //set reload timer to start faster for first time
                this.initialDelay.TotalMilliseconds
                );

            timer.Elapsed += OnTimerTick;

            timer.Start();
        }

        private void OnTimerTick(object sender, ElapsedEventArgs e)
        {
            try
            {
                timer.Stop();
                ReloadData();
                tryCount = 0;

                //if it's not first load then work in normal speed
                if (InitialLoadComplete)
                {
                    timer.Interval = this.interval.TotalMilliseconds;
                }
            }
            catch (Exception ex)
            {
                //log

                //if failed then retry three times with small delay

                tryCount++;

                if (tryCount <= maxReloadTryCount)
                {
                    //set reload timer work faster
                    timer.Interval = retryInterval.TotalMinutes;
                }
                else
                {
                    //set reload timer work normal
                    timer.Interval = interval.TotalMilliseconds;

                    //throw exception if three times of retry couldn't resolve the problem.
                    throw;
                }
            }
            finally
            {
                timer.Start();
            }
        }
    }
}
