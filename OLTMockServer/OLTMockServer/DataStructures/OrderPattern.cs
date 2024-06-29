using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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

        /// <summary>
        /// It is a helper data filled runtime and used by pattern designer, not to be saved with the pattern
        /// </summary>
        [XmlIgnore]
        public List<OrderPattern> PredifinedOrderPatterns = new List<OrderPattern>();

        public string GetUniqueString()
        {
            StringBuilder sb = new StringBuilder();

            foreach (var item in this.PatternItems)
            {
                sb.Append($"{item.PropertyName}:{item.PropertyType}:{item.GenerateType}");
            }

            return sb.ToString();
        }
    }
}
