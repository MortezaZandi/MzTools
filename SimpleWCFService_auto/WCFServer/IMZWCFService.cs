using System;
using System.ServiceModel;
using System.ServiceModel.Web;
using WCFServer.Requests;

namespace WCFServer
{
    [ServiceContract]
    public interface IMZWCFService
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "CheckConnection", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        string CheckConnection(CheckConnection request);

        [OperationContract]
        [WebInvoke(UriTemplate = "FileList", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        FileListResponse FileList(FileListRequest request);
        
        [OperationContract]
        [WebInvoke(UriTemplate = "Download", Method = "POST", RequestFormat = WebMessageFormat.Json, ResponseFormat = WebMessageFormat.Json)]
        DownloadResponse Download(DownloadRequest request);
    }
}