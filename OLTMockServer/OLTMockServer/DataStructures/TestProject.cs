using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.DataStructures
{
    [Serializable]
    public class TestProject
    {
        public TestProject()
        {
            this.ID = Guid.NewGuid();
            this.Items = new List<Item>();
            this.Vendors = new List<Vendor>();
            this.Customers = new List<Customer>();
            this.Orders = new List<Order>();
            this.TestOptions = new TestOptions();
            this.OrderPattern = new OrderPattern();
            this.CreateDate = DateTime.Now;
            TempFilePath = Path.GetTempFileName();
        }

        public Guid ID { get; set; }
        public DateTime CreateDate { get; set; }
        public List<Item> Items { get; set; }
        public List<Vendor> Vendors { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Order> Orders { get; set; }
        public TestOptions TestOptions { get; set; }
        public OrderPattern OrderPattern { get; set; }
        public Definitions.KnownOnlineShops OnlineShop { get; set; }


        public bool IsTemp
        {
            get
            {
                return string.IsNullOrEmpty(SaveFilePath);
            }
        }
        public string SaveFilePath { get; set; }
        public string TempFilePath { get; set; }
    }
}

