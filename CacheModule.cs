using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cache
{
    public class CacheItem<T>
    {
        private T data;
        private DateTime expirationTimeUtc;

        public CacheItem(T data, DateTime expirationTimeUtc)
        {
            this.data = data;
            this.expirationTimeUtc = expirationTimeUtc;
        }

        public T getData()
        {
            return data;
        }

        public void setData(T newData)
        {
            data = newData;
        }

        public DateTime getExpirationTimeUtc()
        {
            return expirationTimeUtc;
        }

        public void setExpirationTimeUtc(DateTime newExpirationTimeUtc)
        {
            expirationTimeUtc = newExpirationTimeUtc;
        }

    }

    public class CacheModule<T>
    {
        private Dictionary<string, CacheItem<T>> cacheStorage;

        public CacheModule()
        {
            this.cacheStorage = new Dictionary<string, CacheItem<T>>();
        }

        //Adds an element to the cache with the given key and expiration date
        public void add(string key, T data, TimeSpan expirationTime)
        {
            var expirationTimeUtc = DateTime.UtcNow.Add(expirationTime);
            var cacheItem = new CacheItem<T>(data, expirationTimeUtc);
            if (this.cacheStorage.ContainsKey(key))
            {
                this.cacheStorage[key] = cacheItem;
            }
            else
            {
                this.cacheStorage.Add(key, cacheItem);
            }
        }

        //updates the expiration date of the item with that key in the cache
        public void updateTime(string key, TimeSpan expirationTime)
        {
            var newExpirationTimeUtc = DateTime.UtcNow.Add(expirationTime);
            this.cacheStorage[key].setExpirationTimeUtc(newExpirationTimeUtc);
        }

        //removes the item with that key from the cache, does nothing otherwise
        public void remove(string key)
        {
            if (this.cacheStorage.ContainsKey(key))
            {
                this.cacheStorage.Remove(key);
            }
        }

        //returns the cache item with the given string
        //throws error if that key does not exist in the cache
        //if the key is for an expired item, that item is removed
        public T getCacheItem(string key)
        {
            if (this.cacheStorage.ContainsKey(key))
            {
                var cacheItem = this.cacheStorage[key];

                if (cacheItem.getExpirationTimeUtc() > DateTime.UtcNow)
                {
                    return cacheItem.getData();
                }
                else
                {
                    this.remove(key);
                }
            }
            throw new KeyNotFoundException("Key was not found");
        }


        ///iterates through the cache and removes any item past their expiry date
        public void removeExpiredKeys()
        {
            foreach(var key in this.cacheStorage.Keys)
            {
                if (this.cacheStorage[key].getExpirationTimeUtc() < DateTime.UtcNow)
                {
                    this.cacheStorage.Remove(key);
                }
            }
        }

        ///returns the key of every item in the cache
        public Dictionary<string, CacheItem<T>> getCacheKeys()
        {
            return this.cacheStorage;
        }

    }
}
