using System;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WCFServer
{
    [ServiceContract]
    public interface IMZWCFService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "CheckConnection", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string CheckConnection(WCFRequests_CheckConnection request);
    }
}