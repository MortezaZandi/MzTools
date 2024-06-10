using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer
{
    [ServiceBehavior(ConcurrencyMode = ConcurrencyMode.Multiple, InstanceContextMode = InstanceContextMode.Single)]
    public class MZWCFService : IMZWCFService
    {
        public string CheckConnection(WCFRequests_CheckConnection request)
        {
            OnRequest?.Invoke($"{DateTime.Now:HH:mm:ss}  Request to CheckConnection from {request.Name}");

            return $"You are connected to the WCF servcie as {request.Name} at {DateTime.Now:HH:mm:ss}";
        }

        public delegate void OnRequestEventHandler(string message);
        public event OnRequestEventHandler OnRequest;
    }
}
