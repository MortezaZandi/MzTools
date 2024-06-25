using System;

namespace OLTMockServer.DataStructures
{
    public class MockServerServiceInfo
    {
        public MockServerServiceInfo()
        {
        }
        public object ServiceClassInstance { get; set; }
        public string ServiceName { get; set; }
        public Type ServiceInterfaceType { get; set; }
        public string BaseUrl { get;  set; }
    }

}
