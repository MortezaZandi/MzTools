using OLTMockServer.DataStructures;
using OLTMockServer.DataStructures.Snap;
using OLTMockServer.spag;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.MockServers
{
    public class SnapMockServer : MockServer
    {
        public SnapMockServer(IOrderRepository orderRepository) : base(orderRepository, new SnapOrderFactory())
        {
        }

        public override List<MockServerServiceInfo> GetServices
        {
            get
            {
                var services = new List<MockServerServiceInfo>();

                services.Add(new MockServerServiceInfo
                {
                    ServiceName = nameof(SnappService),
                    BaseUrl = "http://localhost:5022/SnapExpress/",
                    ServiceClassInstance = new SnappService(this),
                    ServiceInterfaceType = typeof(ISnappService),
                });

                services.Add(new MockServerServiceInfo
                {
                    ServiceName = "snap_mock_auth",
                    BaseUrl = "http://localhost:5030/snap_mock_auth/",
                    ServiceClassInstance = new SnapMockAuth(this),
                    ServiceInterfaceType = typeof(ISnapMockAuth)
                });

                return services;
            }
        }

        public override Definitions.KnownOnlineShops OnlineShopType
        {
            get
            {
                return Definitions.KnownOnlineShops.Snap;
            }
        }

        public override string GetAPIMethodName(Definitions.APINames apiName)
        {
            switch (apiName)
            {
                case Definitions.APINames.NewOrder:
                    return "add-snapp-order";
                default:
                    throw new NotImplementedException($"Api name resolver not implemented. API:{apiName}");
            }
        }

        internal void ClientRequest_Ack(string orderCode)
        {
            Debug.WriteLine($"Ack message received for '{orderCode}'");

            //find order
            var order = orderRepository.FindOrder(orderCode);

            //operation for ack
            order.AckTime = DateTime.Now;

            //add log to order logs
            order.AddLog("Ack", $"Ack message received");

            orderRepository.SaveOrder(order);

            WebOperationContext ctx = WebOperationContext.Current;
            var sc = ctx.OutgoingResponse.StatusCode;
        }

        internal void ClientRequest_Pick(string orderCode)
        {
            Debug.WriteLine($"Pick message received for '{orderCode}'");

            //find order
            var order = orderRepository.FindOrder(orderCode);

            //operation for ack
            order.PickTime = DateTime.Now;

            //add log to order logs
            order.AddLog("Pick", $"Pick message received");

            orderRepository.SaveOrder(order);

            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
        }

        internal void ClientRequest_Accept(string orderCode, DataStructures.Snap.olt.AcceptModel acceptModel)
        {
            Debug.WriteLine($"Accept message received for '{orderCode}'");

            //find order
            var order = orderRepository.FindOrder(orderCode);

            //operation for ack
            order.AcceptTime = DateTime.Now;

            //add log to order logs
            order.AddLog("Accept", $"Accept message received");

            orderRepository.SaveOrder(order);

            WebOperationContext.Current.OutgoingResponse.StatusCode = System.Net.HttpStatusCode.NoContent;
        }

        internal void ClientRequest_Reject(string orderCode, DataStructures.Snap.olt.RejectModel rejectModel)
        {
            Debug.WriteLine($"Reject message received for '{orderCode}'");

            //find order
            var order = orderRepository.FindOrder(orderCode);

            //operation for ack
            order.RejectTime = DateTime.Now;
            order.RejectedByVendor = true;

            //add log to order logs
            order.AddLog("Reject", $"Reject request received, RejectReasonID= {rejectModel.ReasonId}, RejectReason={rejectModel.ReasonTitle}");

            orderRepository.SaveOrder(order);
        }

        internal Stream ClientRequest_Token()
        {
            Debug.WriteLine($"Token request.");

            var model = new DataStructures.Snap.olt.TokenModel()
            {
                AccessToken = "123412342134klhjsdfk",
                ExpiresIn = 60,
            };

            var jsonResult = Newtonsoft.Json.JsonConvert.SerializeObject(model, Newtonsoft.Json.Formatting.Indented);

            return new MemoryStream(Encoding.UTF8.GetBytes(jsonResult));
        }

        protected override void AddDefaultActivitiesToNewOrder(Order newOrder)
        {
            newOrder.AddActivity(Definitions.OrderActivityTypes.Send, false);
        }

        public override bool SendOrder(Order order, Vendor targetVendor)
        {
            var methodNameInVendor = GetAPIMethodName(Definitions.APINames.NewOrder);

            var objectToSend = (DataStructures.Snap.ApiModels.SnapOrderDto)GetSendModel(order);

            var keyvalues = objectToSend.GetType().GetProperties()
            .ToList().Where(p => p.Name != nameof(DataStructures.Snap.ApiModels.SnapOrderDto.Products))
            .Select(p => new KeyValuePair<string, string>(p.Name, p.GetValue(objectToSend)?.ToString())).ToList();

            for (int i = 0; i < objectToSend.Products.Count; i++)
            {
                var item = objectToSend.Products[i];

                var itemKeyValues = item.GetType().GetProperties()
                    .ToList()
                    .Select(p => new KeyValuePair<string, string>($"products[{i}][{p.Name}]", p.GetValue(item)?.ToString())).ToList();

                keyvalues.AddRange(itemKeyValues);
            }

            if (APIUtil.CallApiForFormUrlEncodedContent(methodNameInVendor, keyvalues, RestSharp.Method.POST, targetVendor.BaseUrl))
            {
                return true;
            }

            //log
            return false;
        }

        protected override object GetSendModel(Order order)
        {
            SnapOrder snapOrder = (SnapOrder)order;

            //create real snap-order-model
            var newOrderModel = new DataStructures.Snap.ApiModels.SnapOrderDto()
            {
                Code = snapOrder.Code,
                Comment = snapOrder.Comment,
                DeliverAddress = snapOrder.DeliverAddress,
                DeliveryPrice = snapOrder.DeliveryPrice.ToString(),
                DeliveryTime = snapOrder.DeliveryTime.ToString(),
                DiscountType = snapOrder.DiscountType,
                DiscountValue = snapOrder.DiscountValue.ToString(),
                ExpeditionType = snapOrder.ExpeditionType,
                FirstName = snapOrder.FirstName,
                FullName = snapOrder.FullName,
                LastName = snapOrder.LastName,
                Latitude = snapOrder.Latitude,
                Longitude = snapOrder.Longitude,
                NewOrderDate = snapOrder.NewOrderDate.ToString(),
                OrderDate = snapOrder.OrderDate,
                OrderPaymentTypeCode = snapOrder.OrderPaymentTypeCode,
                PackingPrice = snapOrder.PackingPrice.ToString(),
                Phone = snapOrder.Phone,
                PreparationTime = snapOrder.PreparationTime.ToString(),
                Price = snapOrder.Price.ToString(),
                StatusCode = snapOrder.StatusCode,
                Tax = snapOrder.Tax.ToString(),
                TaxCoefficient = snapOrder.TaxCoeff.ToString(),
                UserAddressCode = snapOrder.UserAddressCode,
                UserCode = snapOrder.UserCode,
                Vat = snapOrder.Vat.ToString(),
                VendorCode = snapOrder.VendorCode,
                VendorMaxPreparationTime = snapOrder.VendorMaxPreparationTime.ToString(),
                Products = new List<DataStructures.Snap.ApiModels.SnappProductDto>(),
            };

            foreach (var item in snapOrder.Items)
            {
                newOrderModel.Products.Add(new DataStructures.Snap.ApiModels.SnappProductDto()
                {
                    Barcode = item.Barcode,
                    Discount = item.Discount.ToString(),
                    Price = item.Price.ToString(),
                    Quantity = item.Quantity.ToString(),
                    Title = item.Name,
                    Vat = item.VAT.ToString(),
                });
            }

            return newOrderModel;
        }

        public override string GetActivityStatusCode(Definitions.OrderActivityTypes activityType)
        {
            switch (activityType)
            {
                case Definitions.OrderActivityTypes.Send:
                    return "56";
                case Definitions.OrderActivityTypes.Edit:
                    return "56";
                case Definitions.OrderActivityTypes.Reject:
                    return "54";
                case Definitions.OrderActivityTypes.None:
                default:
                    throw new ApplicationException($"No status code provided for activity '{activityType}' in {this.GetType().Name}.");
            }
        }
    }


    [ServiceContract]
    public interface ISnappService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "va/v1/order/{orderCode}/ack", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void Ack(string orderCode);

        [OperationContract]
        [WebInvoke(UriTemplate = "va/v1/order/{orderCode}/pick", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void Pick(string orderCode);

        [OperationContract]
        [WebInvoke(UriTemplate = "va/v1/order/{orderCode}/accept", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void Accept(string orderCode, DataStructures.Snap.olt.AcceptModel acceptModel);

        [OperationContract]
        [WebInvoke(UriTemplate = "va/v1/order/{orderCode}/reject", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        void Reject(string orderCode, DataStructures.Snap.olt.RejectModel rejectModel);
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, AddressFilterMode = AddressFilterMode.Any)]
    public class SnappService : ISnappService
    {
        private readonly SnapMockServer mockServer;

        public SnappService()
        {
        }

        public SnappService(SnapMockServer mockServer)
        {
            this.mockServer = mockServer;
        }

        public void Ack(string orderCode)
        {
            this.mockServer.ClientRequest_Ack(orderCode);
        }

        public void Pick(string orderCode)
        {
            this.mockServer.ClientRequest_Pick(orderCode);
        }

        public void Accept(string orderCode, DataStructures.Snap.olt.AcceptModel acceptModel)
        {
            this.mockServer.ClientRequest_Accept(orderCode, acceptModel);
        }

        public void Reject(string orderCode, DataStructures.Snap.olt.RejectModel rejectModel)
        {
            this.mockServer.ClientRequest_Reject(orderCode, rejectModel);
        }
    }

    [ServiceContract]
    public interface ISnapMockAuth
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "token", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        Stream GetTocken();
    }

    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single, AddressFilterMode = AddressFilterMode.Any)]
    public class SnapMockAuth : ISnapMockAuth
    {
        private SnapMockServer snapMockServer;

        public SnapMockAuth(SnapMockServer snapMockServer)
        {
            this.snapMockServer = snapMockServer;
        }

        public Stream GetTocken()
        {
            return this.snapMockServer.ClientRequest_Token();
        }
    }
}
