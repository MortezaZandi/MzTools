using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.DataStructures
{
    [Serializable]
    public class TestOptions
    {
        public TestOptions()
        {
            TestName = "untitled-test";
            DelayBeforeSendNextOrder = 1000;
            MaxOrderCount = 100;
            UseRandomDelay = true;
        }

        public string TestName { get; set; }

        public int DelayBeforeSendNextOrder { get; set; }

        public int MaxOrderCount { get; set; }

        public bool UseRandomDelay { get; set; }
        public bool GenerateOrdersAutomatically { get; set; }
    }
}
