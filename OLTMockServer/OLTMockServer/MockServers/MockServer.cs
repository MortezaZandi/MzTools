using OLTMockServer.DataStructures;
using OLTMockServer.spag;
using spag;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.MockServers
{
    public abstract class MockServer
    {
        protected readonly WCFServiceManager serviceManager;
        protected readonly OrderFactory orderFactory;

        protected MockServer(OrderFactory orderFactory)
        {
            this.serviceManager = new WCFServiceManager();
            this.orderFactory = orderFactory;

            var services = GetServices;

            if (services != null)
            {
                foreach (var serviceInfo in services)
                {
                    //log...
                    this.serviceManager.RegisterService(
                        serviceInfo.ServiceName,
                        serviceInfo.BaseUrl,
                        serviceInfo.ServiceClassInstance,
                        serviceInfo.ServiceInterfaceType);
                }
            }
        }

        public bool IsServerStarted
        {
            get { return this.serviceManager.IsAnyServiceStarted(); }
        }

        public bool StartServer()
        {
            try
            {
                if (IsServerStarted)
                {
                    throw new InvalidOperationException("Cannot start server, Server already started");
                }

                this.serviceManager.StartAll();

                return true;
            }
            catch (Exception ex)
            {
                //log
                return false;
            }
        }

        public bool StopServer()
        {
            try
            {
                if (!IsServerStarted)
                {
                    throw new InvalidOperationException("Cannot stop server, Server is not started.");
                }

                this.serviceManager.StopAll();

                return true;
            }
            catch (Exception ex)
            {
                //log
                return false;
            }
        }

        public bool SendOrder(object order, Vendor targetVendor)
        {
            var methodNameInVendor = GetAPIMethodName(Definitions.APINames.NewOrder);

            if (APIUtil.CallApi(methodNameInVendor, order, RestSharp.Method.POST, targetVendor.BaseUrl))
            {
                //update statistics

                //write log

                return true;
            }

            //log
            return false;
        }

        public Order CreateNewOrder(OrderPattern pattern)
        {
            return orderFactory.CreateNewOrder( pattern);
        }
        public string GetDefaultTestTitle
        {
            get
            {
                return $"Test-{OnlineShopType}";
            }
        }

        public abstract List<MockServerServiceInfo> GetServices { get; }
        public abstract string GetAPIMethodName(Definitions.APINames apiName);
        public abstract Definitions.KnownOnlineShops OnlineShopType { get; }
    }
}
