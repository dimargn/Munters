using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Munters.Tools
{
    public static class CacheManager
    {
        public static IMemoryCache _memoryCache = new MemoryCache(new MemoryCacheOptions());
        public static void SetCache(object key, object obj, TimeSpan timeSpan)
        {
            _memoryCache.Set(key, obj, timeSpan);
        }

        public static void SetCache(object key, object obj)
        {
            _memoryCache.Set(key, obj);
        }
        /// 
        ///Get key value cache
        /// 
        /// 
        /// 
        public static object GetCache(object key)
        {
            return _memoryCache.Get(key);
        }
        /// 
        ///Get key value cache
        /// 
        /// 
        /// 
        /// 
        ///Does the key value cache exist
        /// 
        /// 
        /// 
        public static bool Exist(object key)
        {
            return _memoryCache.TryGetValue(key, out _);
        }

    }
}
