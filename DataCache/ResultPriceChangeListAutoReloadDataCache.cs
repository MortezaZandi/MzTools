using System;

namespace DataCache
{
    public class ResultPriceChangeListAutoReloadDataCache : AutoReloadDataCache<ResultPriceChangeList>
    {
        public ResultPriceChangeListAutoReloadDataCache(BasicData basicData, TimeSpan interval, TimeSpan initialDelay) : base(basicData, interval, initialDelay)
        {
        }

        protected override void InternalReloadData()
        {
            var query = "SELECT * FROM SnappFood_ResultPriceChangeList (NoLock)";

            Data = basicData.dbContext.ReadDataWithDapper<ResultPriceChangeList>(query, basicData.dbContext.connectionString, "PriceCache");
        }
    }
}
