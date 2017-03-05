using Evorine.Data.Abstractions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Data.Cache
{
    public class MemoryCache : ICache
    {
        readonly IDictionary<string, object> caches;
        readonly IDictionary<string, HashSet<string>> relatedCacheKeys;

        public IEnumerable<string> CacheKeys
        {
            get
            {
                return caches.Keys;
            }
        }

        public MemoryCache()
        {
            caches = new Dictionary<string, object>();
            relatedCacheKeys = new Dictionary<string, HashSet<string>>();
        }

        public T Get<T>(string key)
        {
            return (T)caches[key];
        }
        public T GetListItem<T>(string cacheKey, string listKey)
        {
            return ((Dictionary<string, T>)caches[cacheKey])[listKey];
        }

        public object Get(string key)
        {
            return caches[key];
        }
        public object GetListItem(string cacheKey, string listKey)
        {
            return ((IDictionary)caches[cacheKey])[listKey];
        }

        public bool IsSet(string key)
        {
            return caches.ContainsKey(key);
        }

#warning Find better solution
        public bool IsListItemSet(string cacheKey, string listKey)
        {
            if (caches.ContainsKey(cacheKey))
            {
                dynamic dict = caches[cacheKey];
                foreach (var pair in dict)
                    if (pair.Key == listKey) return true;
            }
            return false;
        }

        public void Reset(string key)
        {
            lock (typeof(MemoryCache))
            {
                resetRecursively(key, null);
            }
        }

        private void resetRecursively(string key, HashSet<string> resetContext)
        {
            if (resetContext == null) resetContext = new HashSet<string>();

            if (resetContext.Contains(key)) return;
            resetContext.Add(key);
            caches.Remove(key);

            if (relatedCacheKeys.ContainsKey(key))
                foreach (var related in relatedCacheKeys[key])
                    resetRecursively(related, resetContext);
        }

        public void ClearCache()
        {
            caches.Clear();
        }

        public void Set<T>(string key, T value)
        {
            caches[key] = value;
        }

        public void SetListItem<T>(string cacheKey, string listKey, T value)
        {
            if (!caches.ContainsKey(cacheKey)) caches[cacheKey] = new Dictionary<string, T>();

            ((Dictionary<string, T>)caches[cacheKey]).Add(listKey, value);
        }


        public void SetRelation(string key, params string[] relatedKeys)
        {
            throw new NotImplementedException();
        }
    }
}
