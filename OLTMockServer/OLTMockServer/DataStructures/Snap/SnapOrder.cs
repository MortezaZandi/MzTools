using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.DataStructures.Snap
{
    [Serializable]
    public class SnapOrder : Order
    {
        public SnapOrder() : base()
        {
        }

        public string OrderDate { get; set; } //	> "1719069161",
        public string VendorCode { get; set; } //	> "31qo57",
        public string Comment { get; set; } //	> null,
        public string UserCode { get; set; } //	> "q9w5my",
        public string FullName { get; set; } //	> "mehdi heydari",
        public string FirstName { get; set; } //	> "mehdi",
        public string LastName { get; set; } //	> "heydari",
        public string UserAddressCode { get; set; } //	> "xryndzy",
        public string Phone { get; set; } //	> "NTJlMTg5OTQxND",
        public decimal Price { get; set; } //	> "174335",
        public string DeliverAddress { get; set; } //	> "تحویل در محل",
        public string Latitude { get; set; } //	> "29.687904053144873",
        public string Longitude { get; set; } //	> "52.47329123114612",
        public decimal DeliveryPrice { get; set; } //	> "0",
        public decimal PackingPrice { get; set; } //	> "2700",
        public int DeliveryTime { get; set; } //	> "45",
        public int PreparationTime { get; set; } //	> "15",
        public int TaxCoeff { get; set; } //	> "0",
        public decimal Tax { get; set; } //	> "0",
        public decimal Vat { get; set; } //	> "0",
        public string ExpeditionType { get; set; } //	> "ZF_EXPRESS",
        public string DiscountType { get; set; } //	> null,
        public decimal DiscountValue { get; set; } //	> "0",
        public DateTime NewOrderDate { get; set; } //	> "2024/06/22 18:42:41",
        public string OrderPaymentTypeCode { get; set; } //	> "ONLINE",
        public int VendorMaxPreparationTime { get; set; } //	> "15",

        public override Order LightClone(bool keepId)
        {
            var clone = this.Clone() as SnapOrder;

            clone.Activities.Clear();
            clone.Logs.Clear();

            if (!keepId)
            {
                clone.UId = Guid.NewGuid();
            }

            return clone;
        }
    }
}
