using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer.Requests
{
    [DataContract]
    [Serializable]
    public class CheckConnection
    {
        [DataMember]
        public string Name { get; set; }
    }

    [DataContract]
    [Serializable]
    public class FileListRequest
    {
        [DataMember]
        public string Path { get; set; }
    }

    [DataContract]
    [Serializable]
    public class FileListResponse
    {
        [DataMember]
        public string Path { get; set; }
        [DataMember]
        public string[] Files { get; set; }
    }


    [DataContract]
    [Serializable]
    public class DownloadRequest
    {
        [DataMember]
        public string Path { get; set; }
        [DataMember]
        public int Position { get; set; }
        [DataMember]
        public int Length { get; set; }
    }

    [DataContract]
    [Serializable]
    public class DownloadResponse
    {
        [DataMember]
        public string Path { get; set; }
        [DataMember]
        public int Position { get; set; }
        [DataMember]
        public int Length { get; set; }
        [DataMember]
        public byte[] Bytes { get; set; }
        [DataMember]
        public long TotalLength { get; set; }
    }

    public static class Extensions
    {
        public static string ToJson(this object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
