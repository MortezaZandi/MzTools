using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Telerik.WinControls.UI;

namespace OLTMockServer.DataStructures
{
    [XmlInclude(typeof(Snap.SnapOrder))]
    [Serializable]
    public abstract class Order
    {
        public Order()
        {
            this.Items = new List<Item>();
            this.Customer = new Customer();
            this.Vendor = new Vendor();
            this.CreateDate = DateTime.Now;
            this.Code = Utils.GenerateCode(8);
        }

        [Browsable(false)]
        public List<Item> Items { get; set; }
        
        public string Code { get; set; }

        [Browsable(false)]
        public Customer Customer { get; set; }
        
        [Browsable(false)]
        public Vendor Vendor { get; set; }
        
        public DateTime CreateDate { get; set; }
        public string StatusCode { get; set; }
        public string StatusDescription { get; set; }
        public DateTime AcceptTime { get; set; }
        public DateTime PickTime { get; set; }
        public DateTime AckTime { get; set; }
        public DateTime RejectTime { get; set; }
        public string DeliveryMode { get; set; }


    }
}
