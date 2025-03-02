using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataCache
{

    public abstract class DataCache<TModel> : IDataCache
    {
        protected readonly BasicData basicData;

        public DataCache(BasicData basicData)
        {
            this.basicData = basicData;
        }

        public List<TModel> Data { get; set; }

        public bool IsLoading { get; set; }
        public bool HasError { get; protected set; }
        public bool InitialLoadComplete { get; protected set; }
        public bool IsReady { get { return (IsLoading == false && HasError == false && InitialLoadComplete == true); } }

        public bool HasData
        {
            get
            {
                return Data.Count > 0;
            }
        }

        public virtual void ReloadData()
        {
            try
            {
                IsLoading = true;

                Logger.Log($"");
                if (!InitialLoadComplete)
                {
                    Logger.Log($"{this.GetType().Name} start initialreloadData()");
                    //log
                    InitialReloadData();
                    InitialLoadComplete = true;
                    //log
                }
                else
                {
                    Logger.Log($"{this.GetType().Name} start reloadData()");
                    //log
                    InternalReloadData();
                    //log
                }
                Logger.Log($"{this.GetType().Name} finished reloadData()");
            }
            catch (Exception ex)
            {
                //log
                Logger.Log($"{this.GetType().Name} has error in reloadData()");
                HasError = true;
            }
            finally
            {
                IsLoading = false;
            }
        }

        protected abstract void InternalReloadData();

        public virtual void InitialReloadData()
        {
            //by default initial reload is same as InternalReloadData
            InternalReloadData();
        }
    }
}
