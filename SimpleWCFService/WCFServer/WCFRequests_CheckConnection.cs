using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace WCFServer
{
    [DataContract]
    [Serializable]
    public class WCFRequests_CheckConnection
    {
        [DataMember]
        public string Name { get; set; }
    }
}
