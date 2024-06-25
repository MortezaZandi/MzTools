using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.DataStructures
{
    [Serializable]
    public class OrderPatternItem
    {
        public OrderPatternItem()
        {
            AvailableValues = new List<object>();
        }

        public string PropertyName { get; set; }
        public string PropertyType { get; set; }
        public Definitions.PropertyValueGeneratorTypes GenerateType { get; set; }
        public string Value { get; set; }
        public object MinValue { get; set; }
        public object MaxValue { get; set; }
        public List<object> AvailableValues { get; set; }
        public bool Unique { get; set; }
    }
}
