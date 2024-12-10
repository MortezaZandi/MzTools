using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Graph2D
{
    public class GraphDataCollection<T>
    {
        private List<GraphData<T>> data = new List<GraphData<T>>();

        public GraphDataCollection()
        {
        }

        public GraphDataCollection(IEnumerable<GraphData<T>> source)
        {
            this.data.AddRange(source);
        }

        public IEnumerable<GraphData<T>> Data
        {
            get
            {
                return data.AsEnumerable();
            }
        }

        public void Add(T value)
        {
            data.Add(new GraphData<T>
            {
                Value = value,
                Time = DateTime.Now
            });
        }

        public int Count
        {
            get
            {
                return data.Count;
            }
        }

        public void Clear()
        {
            data.Clear();
        }

        public GraphDataCollection<T> TakeLast(int count)
        {
            if (count <= Count)
            {
                return new GraphDataCollection<T>(data.GetRange(Count - count, count));
            }
            else
            {
                return new GraphDataCollection<T>(data.AsEnumerable());
            }
        }

        public GraphDataCollection<T> GetData(DateTime from)
        {
            return new GraphDataCollection<T>(data.Where(x => x.Time >= from));
        }

        public GraphDataCollection<T> GetLastSecondData()
        {
            return GetData(DateTime.Now.AddSeconds(-1));
        }

        public GraphDataCollection<T> GetLastMinuteData()
        {
            return GetData(DateTime.Now.AddMinutes(-1));
        }

        public GraphDataCollection<T> GetLastHourData()
        {
            return GetData(DateTime.Now.AddHours(-1));
        }

        public GraphDataCollection<T> GetLastDayData()
        {

            return GetData(DateTime.Now.AddDays(-1));
        }

        public GraphData<T> GetMaxValue()
        {
            if (Count > 0)
            {
                var maxvalue = data.Max(w => w.Value);
                var maxitems = data.FirstOrDefault(w => w.Value.Equals(maxvalue));
                return maxitems;
            }
            else
            {
                return default;
            }
        }

        public GraphData<T> GetMinValue()
        {
            if (Count > 0)
            {
                var maxvalue = data.Min(w => w.Value);
                var maxitems = data.FirstOrDefault(w => w.Value.Equals(maxvalue));
                return maxitems;
            }
            else
            {
                return default;
            }
        }

        public GraphData<T> Get(int index)
        {
            if (Count > 0 && index < Count)
            {
                return data[index];
            }
            else
            {
                return default;
            }
        }
    }

    public class GraphData<T>
    {
        public T Value { get; set; }
        public DateTime Time { get; set; }

        public override string ToString()
        {
            return $"{Value}";
        }
    }
}
