using System;
using System.Collections.Generic;

namespace DataCache
{
    public class DeactiveItemAutoReloadDataCache : AutoReloadDataCache<DeactiveItemRoot>
    {
        private CommonAPLMethods commonAPLMethods;
        public DeactiveItemAutoReloadDataCache(TimeSpan interval, TimeSpan initialDelay, BasicData basicData, CommonAPLMethods commonAPLMethods) : base(basicData, interval, initialDelay)
        {
            this.commonAPLMethods = commonAPLMethods;
        }

        protected override void InternalReloadData()
        {
            if (IsWCFServiceAvailable())
            {
                try
                {
                    Data = GetLastDeactivedItemsFromWCFService();
                }
                catch (Exception ex)
                {
                    try
                    {
                        //log
                        Data = commonAPLMethods.GetDeactiveRoot(false, true);
                    }
                    catch (Exception exx)
                    {
                        //log
                        throw;
                    }
                }
            }
            else
            {
                Data = commonAPLMethods.GetDeactiveRoot(false, true);
            }
        }

        private List<DeactiveItemRoot> GetLastDeactivedItemsFromWCFService()
        {
            throw new NotImplementedException();
            //create a rest client
            //call wcf service
            //extract data
            //return data
        }

        private bool IsWCFServiceAvailable()
        {
            return false;
        }
    }
}
