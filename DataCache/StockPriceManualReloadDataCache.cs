using System;
using System.Collections.Generic;

namespace DataCache
{
    public class StockPriceManualReloadDataCache : ManualReloadDataCache<SnappFood_StockPrice>
    {
        public StockPriceManualReloadDataCache(BasicData basicData) : base(basicData)
        {
        }

        public List<int> RetailStoreIds { get; set; }

        protected override void InternalReloadData()
        {
            if (this.RetailStoreIds.Count == 0)
            {
                throw new ApplicationException("No retail store supplied for StockPriceManualReloadDataCache reload method.");
            }

            var query = "SELECT * FROM SnappFood_StockPrice (NoLock) WHERE RetailStoreID IN ({0})";

            query = string.Format(query, string.Join(",", RetailStoreIds));

            Data = basicData.dbContext.ReadDataWithDapper<SnappFood_StockPrice>(query, basicData.dbContext.connectionString, "HistoryCache");
        }

    }
}
