using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace RRON
{
    public class TypeNameMap
    {
        private static readonly MemoryCacheEntryOptions CacheEntryOptions = new MemoryCacheEntryOptions()
                                                                            .SetSize(1)
                                                                            .SetPriority(CacheItemPriority.High)
                                                                            .SetSlidingExpiration(TimeSpan.FromSeconds(3))
                                                                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(15));

        private static readonly MemoryCache MemoryCache = new MemoryCache(new MemoryCacheOptions { SizeLimit = 16 });
        private readonly Dictionary<string, Type> cache = new Dictionary<string, Type>();

        private TypeNameMap(Type type)
        {
            foreach (var propertyInfo in type.GetProperties())
            {
                this.cache.Add(propertyInfo.Name, propertyInfo.PropertyType);
            }
        }

        internal static TypeNameMap GetOrCreate(Type type)
        {
            if (!MemoryCache.TryGetValue(type, out TypeNameMap cacheEntry))
            {
                cacheEntry = new TypeNameMap(type);
                MemoryCache.Set(type, cacheEntry, CacheEntryOptions);
            }

            return cacheEntry;
        }

        internal Type GetTypeByName(string name) => this.cache[name];
    }
}