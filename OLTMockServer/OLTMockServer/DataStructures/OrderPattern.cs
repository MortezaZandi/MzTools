using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.DataStructures
{
    [Serializable]
    public class OrderPattern
    {
        public OrderPattern()
        {
            this.PatternItems = new List<OrderPatternItem>();
        }

        public string PatternName { get; set; }
        public List<OrderPatternItem> PatternItems { get; set; }
        
        public int lastInt;
        public decimal lastDecimal;
        public DateTime lastDate;
        public List<int> ints = new List<int>();
        public List<decimal> decimals = new List<decimal>();
        public List<DateTime> dates = new List<DateTime>();
        public List<string> codes8 = new List<string>();
        public List<string> codes6 = new List<string>();
        public List<string> codes = new List<string>();
    }
}
