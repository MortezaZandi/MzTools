using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCache
{
    internal class Logger
    {
        public delegate void OnNewLogEventHanlder(string logMessage);

        public static event OnNewLogEventHanlder OnNewLog;
        public static void Log(string message, params object[] args)
        {
            message = string.Format(message, args);
            message = $"[{DateTime.Now:HH:mm:ss.fff}]\t{message}";
            OnNewLog?.Invoke(message);
        }
    }
}
