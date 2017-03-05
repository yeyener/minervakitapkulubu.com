using Evorine.Data.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Evorine.Data.Cache
{
    public class CacheProvider : ICacheProvider
    {
        ICache memoryCache;
        ICache remoteCache;

        public ICache GetCache(string key)
        {
            throw new NotImplementedException();
        }

        public ICache GetMemoryCache()
        {
            if (memoryCache == null)
            {
                throw new NotSupportedException("Memory cache system is not configured!");
            }
            return memoryCache;
        }

        public ICache GetRemoteCache()
        {
            if (remoteCache == null)
            {
                throw new NotSupportedException("Remote cache system is not configured!");
            }
            return remoteCache;
        }


        public void UseMemoryCache()
        {
            memoryCache = new MemoryCache();
        }
        public void UseRemoteCache()
        {
            throw new NotImplementedException();
        }
    }
}
