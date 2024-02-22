using System.Collections.Concurrent;
using System.Diagnostics;
using System;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Threading;
using System.Security.Cryptography;

namespace MMF_IPC
{
    public class OLTDiag
    {
        private ConcurrentDictionary<string, MLogger> loggers = new ConcurrentDictionary<string, MLogger>();

        private ConcurrentDictionary<string, ActionMonitor> actionMonitors = new ConcurrentDictionary<string, ActionMonitor>();

        public event MLogger.NewLogAvailableEventHandler OnReportAvailable;

        private ConcurrentDictionary<string, string> statuses = new ConcurrentDictionary<string, string>();

        private Thread autoReportThread;

        public void RegisterAction(string name, bool readerMode)
        {
            if (loggers.ContainsKey(name))
            {
                throw new InvalidOperationException($"Logger already registered. '{name}'");
            }

            loggers[name] = new MLogger(name);
            actionMonitors[name] = new ActionMonitor(name);

            if (readerMode)
            {
                loggers[name].Open();
                loggers[name].OnNewLogAvailable += OLTDiag_OnNewLogAvailable;
            }
            else
            {
                loggers[name].Create();
            }
        }

        public void RegisterStatus(string name, string initialValue, bool readerMode)
        {
            if (loggers.ContainsKey(name))
            {
                throw new InvalidOperationException($"Logger already registered. '{name}'");
            }

            loggers[name] = new MLogger(name);

            statuses[name] = initialValue;

            if (readerMode)
            {
                loggers[name].Open();
                loggers[name].OnNewLogAvailable += OLTDiag_OnNewLogAvailable;
            }
            else
            {
                loggers[name].Create();
            }
        }

        private void OLTDiag_OnNewLogAvailable(string log)
        {
            OnReportAvailable?.Invoke(log);
        }

        public OLTDiag()
        {
            this.autoReportThread = new Thread(() =>
            {
                while (true)
                {
                    var sdt = DateTime.Now;

                    lock (this)
                    {
                        DoReport();
                    }

                    var edt = DateTime.Now;

                    var dif = (int)(edt - sdt).TotalMilliseconds;

                    if (dif < 1000)
                    {
                        Thread.Sleep(1000 - dif);
                    }
                }
            });

            this.autoReportThread.IsBackground = true;
            //this.autoReportThread.Start();
        }

        public Guid OnActionStart(string actionName)
        {
            return actionMonitors[actionName].Start();
        }

        public void OnActionStop(string actionName, Guid call_Id)
        {
            actionMonitors[actionName].Stop(call_Id);
        }

        public void SetStatus(string name, string value)
        {
            statuses[name] = value;
        }

        public void DoReport()
        {
            foreach (var action in actionMonitors.Values)
            {
                var logger = loggers[action.ActionName];

                logger.WriteNewLog($"Action,{action.ReportString()}");
            }

            foreach (var status in statuses)
            {
                var logger = loggers[status.Key];

                logger.WriteNewLog($"Status,{status.Key}:{status.Value}");
            }
        }
    }

    public class ActionMonitor
    {
        private int callCount = 0;
        private string actionName;
        private int finishedCalls;
        private double totalDurration = 0;
        private DateTime firstCall_dt;
        private DateTime lastCall_dt;
        private ConcurrentQueue<Guid> doneSps = new ConcurrentQueue<Guid>();
        private ConcurrentDictionary<Guid, Stopwatch> callSPs = new ConcurrentDictionary<Guid, Stopwatch>();

        public ActionMonitor(string actionName)
        {
            this.actionName = actionName;
        }

        public Guid Start()
        {
            if (firstCall_dt == DateTime.MinValue)
            {
                firstCall_dt = DateTime.Now;
            }

            lastCall_dt = DateTime.Now;

            callCount++;

            Guid callId = Guid.Empty;
            Stopwatch sp;

            while (doneSps.Count > 0 && !doneSps.TryDequeue(out callId)) ;

            if (callId == Guid.Empty)
            {
                callId = Guid.NewGuid();
                callSPs[callId] = Stopwatch.StartNew();
            }
            else
            {
                var stw = callSPs[callId];

                if (stw.IsRunning)
                {
                    throw new InvalidOperationException("Trying to use a running stopwatch for new call");
                }

                stw.Restart();

                stw.Start();
            }

            return callId;
        }

        public void Stop(Guid call_id)
        {
            if (callSPs.ContainsKey(call_id))
            {
                var stw = callSPs[call_id];

                stw.Stop();

                var durrMS = stw.Elapsed.TotalMilliseconds;

                totalDurration += durrMS;

                if (!doneSps.Contains(call_id))
                {
                    doneSps.Enqueue(call_id);
                }
            }

            finishedCalls++;
        }

        public string ActionName
        {
            get
            {
                return actionName;
            }
        }

        /// <summary>
        /// Call per second
        /// </summary>
        public double CPS
        {
            get
            {
                var secsSinceFirstCall = (DateTime.Now - firstCall_dt).TotalSeconds;
                return callCount / (float)secsSinceFirstCall;
            }
        }

        /// <summary>
        /// Execution durration
        /// </summary>
        public double EXD
        {
            get
            {
                return (totalDurration / callCount);
            }
        }

        /// <summary>
        /// Count of calls
        /// </summary>
        public double CNT
        {
            get
            {
                return callCount;
            }
        }

        /// <summary>
        /// Count of call counters(stopwatchs)
        /// </summary>
        public double CCC
        {
            get
            {
                return callSPs.Count;
            }
        }

        /// <summary>
        /// Active call count
        /// </summary>
        public double ACC
        {
            get
            {
                return callCount - finishedCalls;
            }
        }

        public void Reset()
        {
            foreach (var item in callSPs)
            {
                item.Value.Stop();
            }

            callSPs.Clear();

            doneSps = new ConcurrentQueue<Guid>();

            callCount = 0;

            totalDurration = 0;

            finishedCalls = 0;

            firstCall_dt = DateTime.MinValue;

            lastCall_dt = DateTime.MinValue;
        }

        public string ReportString()
        {
            return $"NM:{actionName}, CPS:{CPS.ToString("N2")}, EXD:{EXD.ToString("N2")}, CNT:{CNT.ToString("N2")}, CCC:{CCC.ToString("N2")}, ACC:{ACC.ToString("N2")}, FDT:{firstCall_dt}, EDT{lastCall_dt}";
        }
    }
}
