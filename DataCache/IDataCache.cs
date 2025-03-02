namespace DataCache
{
    public interface IDataCache
    {
        bool HasData { get; }
        bool HasError { get; }
        bool InitialLoadComplete { get; }
        bool IsLoading { get; set; }
        bool IsReady { get; }
    }
}
