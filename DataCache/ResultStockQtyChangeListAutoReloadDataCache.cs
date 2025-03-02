using System;

namespace DataCache
{
    public class ResultStockQtyChangeListAutoReloadDataCache : AutoReloadDataCache<ResultStockQtyChangeList>
    {
        public ResultStockQtyChangeListAutoReloadDataCache(BasicData basicData, TimeSpan interval, TimeSpan initialDelay) : base(basicData, interval, initialDelay)
        {
        }

        protected override void InternalReloadData()
        {
            var query = "SELECT * FROM SnappFood_ResultStockQtyChangeList (NoLock)";

            Data = basicData.dbContext.ReadDataWithDapper<ResultStockQtyChangeList>(query, basicData.dbContext.connectionString, "StockCache");
        }
    }
}
