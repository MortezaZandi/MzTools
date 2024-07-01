using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.DataStructures.Snap.olt
{
    [JsonObject]
    public class RejectModel
    {
        [JsonProperty("reasonId")]
        public int ReasonId { get; set; }

        [JsonProperty("reasonTitle", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string ReasonTitle { get; set; }

        [JsonProperty("comment")]
        public string Comment { get; set; }

        [JsonProperty("nonExistentProducts", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<NonExistentProduct> NonExistentProducts { get; set; }

        [JsonProperty("order_code", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string order_code { get; set; }

        [JsonProperty("status", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public string status { get; set; }

        [JsonIgnore]
        public DateTime? RejectReqDateTime { get; set; }
    }

    [JsonObject]
    public class NonExistentProduct
    {
        [JsonProperty("barcode")]
        public string Barcode { get; set; }

        [JsonProperty("suggestedProductBarcodes", DefaultValueHandling = DefaultValueHandling.Ignore)]
        public List<string> SuggestedProductBarcodes { get; set; }
    }
}
