using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Data.Abstractions
{
    public interface ICache
    {
        void ClearCache();

        void Reset(string key);
        
        bool IsSet(string key);
        bool IsListItemSet(string cacheKey, string listKey);

        void Set<T>(string key, T value);
        void SetListItem<T>(string cacheKey, string listKey, T value);

        T Get<T>(string key);
        T GetListItem<T>(string cacheKey, string listKey);

        object Get(string key);
        object GetListItem(string cacheKey, string listKey);

        IEnumerable<string> CacheKeys { get; }

        void SetRelation(string key, params string[] relatedKeys);
    }
}
