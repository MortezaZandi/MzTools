using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLTMockServer.DataStructures
{
    [Serializable]
    public class AppData
    {
        public AppData()
        {
            this.QueryCommands = new List<QueryCommandInfo>();
            LoadDefaultQueries();
        }

        public string MasterDBConnection { get; set; }

        public List<QueryCommandInfo> QueryCommands;

        private void LoadDefaultQueries()
        {
            QueryCommands.Add(new QueryCommandInfo()
            {
                Name = Definitions.Query_Name_ReadItemsFromCMSDB,
                Description = "Read items from CMSDB",
                QueryCommand = @"

SELECT sp.ItemID, sp.Barcode, sp.ItemName, sp.UnitPrice Price, sp.TempPrice Discount , v.VendorCode
FROM SnappFood_StockPrice sp
JOIN SnappFood_BasicInformation b ON b.RetaileStoreID = sp.RetailStoreID
JOIN SnappFood_RetailStoreVendorCodes v ON v.BasicInfoId = b.ID 
WHERE v.AppTypeNumber = {0} AND v.VendorCode IN ({1})
"
            }); ;


        }

        public string GetQueryCommand(string name)
        {
            return QueryCommands.FirstOrDefault(c => c.Name == name)?.QueryCommand;
        }
    }
}
