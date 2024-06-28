using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.DataStructures
{
    [Serializable]
    public class LogInfo
    {
        public LogInfo()
        {
        }

        public LogInfo(string logDetails, Definitions.LogTypes logType)
            : this(time: DateTime.Now, logTitle: null, logDetails: logDetails, logType: logType)
        {
        }

        public LogInfo(string logTitle, string logDetails, Definitions.LogTypes logType)
            : this(time: DateTime.Now, logTitle: logTitle, logDetails: logDetails, logType: logType)
        {
        }


        public LogInfo(DateTime time, string logTitle, string logDetails, Definitions.LogTypes logType)
        {
            this.LogTime = time;
            this.LogTitle = logTitle;
            this.LogDetails = logDetails;
            this.LogType = logType;
        }

        public DateTime LogTime { get; set; }
        public string LogTitle { get; set; }
        public string LogDetails { get; set; }
        public Definitions.LogTypes LogType { get; set; }

        public bool IsEmpty
        {
            get
            {
                return string.IsNullOrEmpty(LogTitle) && string.IsNullOrEmpty(LogDetails);
            }
        }

    }
}
