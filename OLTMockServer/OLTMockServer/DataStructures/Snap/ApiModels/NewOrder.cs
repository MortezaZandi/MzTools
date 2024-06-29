using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.DataStructures.Snap.ApiModels
{
    public class SnapOrderDto
    {
        public string Code { get; set; }

        public string UserCode { get; set; }

        public string FullName { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string UserAddressCode { get; set; }

        public string Phone { get; set; }

        public string Price { get; set; }

        public string Comment { get; set; }

        public string StatusCode { get; set; }

        public string DeliverAddress { get; set; }

        public string OrderDate { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public string DeliveryPrice { get; set; }

        public string PackingPrice { get; set; }

        public string DeliveryTime { get; set; }

        public string PreparationTime { get; set; }

        public string TaxCoefficient { get; set; }

        public string Tax { get; set; }

        public string Vat { get; set; }

        public string ExpeditionType { get; set; }

        public string DiscountType { get; set; }

        public string DiscountValue { get; set; }

        public string NewOrderDate { get; set; }

        public string OrderPaymentTypeCode { get; set; }

        public string VendorMaxPreparationTime { get; set; }

        public string VendorCode { get; set; }

        public List<SnappProductDto> Products { get; set; }
    }

    public class SnappProductDto
    {
        public string Quantity { get; set; }

        public string Price { get; set; }

        public string Title { get; set; }

        public string Discount { get; set; }

        public string Vat { get; set; }

        public string Barcode { get; set; }

        public string BundleId { get; set; }
    }
}
