using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel.Description;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace spag
{
    public class WCFServiceManager
    {
        private readonly Dictionary<string, ServiceHost> registeredServices;

        public WCFServiceManager()
        {
            registeredServices = new Dictionary<string, ServiceHost>();
        }

        public void RegisterService(string serviceName, string baseUrl, object serviceClassInstance, Type servericeInterfaceType)
        {
            if (this.registeredServices.ContainsKey(serviceName))
            {
                throw new InvalidOperationException($"Service '{serviceName}' already registered.");
            }

            var baseAddress = new Uri(baseUrl);

            var host = new WebServiceHost(serviceClassInstance, baseAddress);

            AddDescriptionBehavior(host);

            AddServiceEndpoint(host, servericeInterfaceType);

            //host.Authorization.ServiceAuthorizationManager = new WCFServiceAuthorizationManager();

            this.registeredServices.Add(serviceName, host);
        }

        public object CreateServiceInstance(Type serviceType)
        {
            return Activator.CreateInstance(serviceType);
        }

        private static void AddServiceEndpoint(ServiceHost host, Type serviceInterfaceType)
        {
            ServiceEndpoint restEndpoint = host.AddServiceEndpoint(serviceInterfaceType, new WebHttpBinding() { MaxReceivedMessageSize = 1073741824 }, "");
            WebHttpBehavior restWebHttpBehavior = new WebHttpBehavior();
            restWebHttpBehavior.HelpEnabled = true;
            restEndpoint.EndpointBehaviors.Add(restWebHttpBehavior);
        }

        private static void AddDescriptionBehavior(ServiceHost host)
        {
            host.Description.Behaviors.Add(new ServiceMetadataBehavior { HttpGetEnabled = true });

            ServiceDebugBehavior debug = host.Description.Behaviors.Find<ServiceDebugBehavior>();
            // if not found - add behavior with setting turned on
            if (debug == null)
            {
                host.Description.Behaviors.Add(new ServiceDebugBehavior() { IncludeExceptionDetailInFaults = true });
            }
            else
            {
                // make sure setting is turned ON
                if (!debug.IncludeExceptionDetailInFaults)
                {
                    debug.IncludeExceptionDetailInFaults = true;
                }
            }

        }

        public void StartAll()
        {
            foreach (string serviceName in this.registeredServices.Keys)
            {
                try
                {
                    this.registeredServices[serviceName].Open();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error in starting service '{serviceName}'", ex);
                }
            }
        }

        public void StopAll()
        {
            foreach (string serviceName in this.registeredServices.Keys)
            {
                this.registeredServices[serviceName].Close();
            }
        }

        public bool IsAnyServiceStarted()
        {
            return this.registeredServices.Any(s => s.Value.State == CommunicationState.Opened);
        }
    }
}
