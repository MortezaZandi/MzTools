using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DataCache
{
    public class BasicData
    {
        public BasicData()
        {
            dbContext = new DBContext();
        }

        public DBContext dbContext;
    }

    public class DBContext
    {
        public object connectionString;

        public List<T> ReadDataWithDapper<T>(string query, object connectionString, string name)
        {
            RandomDelay(name);

            return new List<T>();
        }


        public void RandomDelay(string name)
        {
            var random = new Random();
            int delayMilliseconds = random.Next(1000, 15001);
            Logger.Log($"{name}_Delay {TimeSpan.FromMilliseconds(delayMilliseconds).TotalSeconds:N0} second.");
            Logger.Log($"{name}_DelayStart");
            System.Threading.Thread.Sleep(delayMilliseconds);
            Logger.Log($"{name}_DelayStop");
        }
    }

}