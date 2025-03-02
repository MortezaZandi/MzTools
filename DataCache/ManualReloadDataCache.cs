namespace DataCache
{
    public abstract class ManualReloadDataCache<TModel> : DataCache<TModel>
    {
        protected ManualReloadDataCache(BasicData basicData) : base(basicData)
        {
        }
    }
}
