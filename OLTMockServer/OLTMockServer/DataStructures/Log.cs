using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.DataStructures
{
    public class Log
    {
        public Log(string logDetails)
        {
            LogDetails = logDetails;
        }

        public string LogDetails { get; set; }
    }
}
