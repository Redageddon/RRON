namespace RRON
{
    using System;
    using System.Data;
    using Collections.Pooled;
    using Microsoft.Extensions.Caching.Memory;

    /// <summary>
    ///     Caches a mapping of type names to their type.
    /// </summary>
    public class TypeNameMap
    {
        private static readonly MemoryCacheEntryOptions CacheEntryOptions = new MemoryCacheEntryOptions()
                                                                            .SetSize(1)?
                                                                            .SetPriority(CacheItemPriority.High)?
                                                                            .SetSlidingExpiration(TimeSpan.FromSeconds(3))?
                                                                            .SetAbsoluteExpiration(TimeSpan.FromSeconds(15))!;

        private static readonly MemoryCache MemoryCache = new (new MemoryCacheOptions { SizeLimit = 16 });
        private readonly PooledDictionary<string, Type> cache = new ();

        private TypeNameMap(Type type)
        {
            for (var i = 0; i < type.GetProperties().Length; i++)
            {
                var propertyInfo = type.GetProperties()[i];
                this.cache.Add(propertyInfo.Name, propertyInfo.PropertyType);
            }
        }

        /// <summary>
        ///     Gets a map based upon a type.
        /// </summary>
        /// <param name="type"> The type being created for. </param>
        /// <returns> A map based upon <param name="type"/>. </returns>
        public static TypeNameMap GetOrCreate(Type type)
        {
            if (!MemoryCache.TryGetValue(type, out TypeNameMap cacheEntry))
            {
                cacheEntry = new TypeNameMap(type);
                MemoryCache.Set(type, cacheEntry, CacheEntryOptions);
            }

            return cacheEntry!;
        }

        internal Type GetTypeByName(string name) => this.cache[name];
    }
}