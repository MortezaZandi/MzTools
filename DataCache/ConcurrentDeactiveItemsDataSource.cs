using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;

namespace DataCache
{
    public class ConcurrentDataSource<T> where T : IComparable<T>
    {
        private ConcurrentBag<T> data = new ConcurrentBag<T>();

        public void ClearItems()
        {
            data = new ConcurrentBag<T>();
        }

        public void AddItem(T item)
        {
            data.Add(item);
        }

        public void AddItems(List<T> items)
        {
            foreach (var item in items)
            {
                data.Add(item);
            }
        }

        public bool TryRemoveItem(out T item)
        {
            return data.TryTake(out item);
        }

        public T[] GetAllItems()
        {
            T[] copy = new T[data.Count];
            int i = 0;
            foreach (var item in data)
            {
                copy[i++] = item;
            }
            return copy;
        }

        public T FindItem(T itemToFind)
        {
            foreach (var item in data)
            {
                if (item.CompareTo(itemToFind) == 0)
                {
                    return item;
                }
            }

            return default(T);
        }

        public IEnumerable<T> FindItems(T itemToFind)
        {
            foreach (var item in data)
            {
                if (item.CompareTo(itemToFind) == 0)
                {
                    yield return item;
                }
            }
        }

        public IEnumerable<T> Where(Func<T, bool> predicate)
        {
            return data.Where(predicate);
        }

        public T FirstOrDefault(Func<T, bool> predicate)
        {
            return data.FirstOrDefault(predicate);
        }
    }
}
