using System.Collections.Concurrent;
using System.Diagnostics;
using System;
using System.Linq;
using System.ServiceModel.Configuration;
using System.Threading;
using System.Security.Cryptography;
using System.Collections.Generic;

namespace MMF_IPC
{
    public class IPCDiag
    {
        private ConcurrentDictionary<string, IPCDiagLogger> loggers = new ConcurrentDictionary<string, IPCDiagLogger>();

        private ConcurrentDictionary<string, IPCDiagActionMonitor> actionMonitors = new ConcurrentDictionary<string, IPCDiagActionMonitor>();

        public event IPCDiagLogger.NewLogAvailableEventHandler OnReportAvailable;

        private ConcurrentDictionary<string, string> statuses = new ConcurrentDictionary<string, string>();

        private Thread autoReportThread;

        private bool autoReport;

        public bool AutoReport
        {
            get => autoReport; set
            {
                autoReport = value;

                if (autoReport)
                {
                    autoReportThread.Start();
                }
            }
        }

        public int ReportInterval
        {
            get; set;
        }

        public void RegisterAction(string name, bool readerMode)
        {
            if (loggers.ContainsKey(name))
            {
                throw new InvalidOperationException($"Logger already registered. '{name}'");
            }

            loggers[name] = new IPCDiagLogger(name);
            actionMonitors[name] = new IPCDiagActionMonitor(name);

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

            loggers[name] = new IPCDiagLogger(name);

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

        public IPCDiag()
        {
            autoReport = false;
            ReportInterval = 1000;

            this.autoReportThread = new Thread(() =>
            {
                while (autoReport)
                {
                    var sdt = DateTime.Now;

                    lock (this)
                    {
                        DoReport();
                    }

                    var edt = DateTime.Now;

                    var dif = (int)(edt - sdt).TotalMilliseconds;

                    if (dif < ReportInterval)
                    {
                        Thread.Sleep(ReportInterval - dif);
                    }
                }
            });

            this.autoReportThread.IsBackground = true;
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

                logger.WriteNewLog($"Status,NM:{status.Key}, VAL:{status.Value}");
            }
        }
    }
}
