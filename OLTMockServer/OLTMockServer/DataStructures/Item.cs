using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.DataStructures
{
    [Serializable]
    public class Item
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public string Barcode { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public bool IsActive { get; set; }
        public decimal VAT { get; set; }

        /// <summary>
        /// Each item is defined for a specific vendor
        /// </summary>
        public string VendorCode { get; set; }
    }
}
