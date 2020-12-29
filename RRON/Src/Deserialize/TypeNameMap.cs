using System.Collections;

namespace RRON.Deserialize
{
    using System;

    /// <summary>
    ///     Caches a mapping of type names to their type.
    /// </summary>
    public class TypeNameMap
    {
        private static readonly Hashtable MemoryCache = new();
        private readonly Hashtable cache = new();

        private TypeNameMap(Type type)
        {
            var properties = type.GetProperties();
            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < properties.Length; i++)
            {
                var propertyInfo = properties[i];
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
            TypeNameMap entry;
            if (!MemoryCache.ContainsKey(type))
            {
                MemoryCache.Add(type, entry = new TypeNameMap(type));
            }
            else
            {
                entry = (TypeNameMap)MemoryCache[type]!;
            }

            return entry;
        }

        internal Type GetTypeByName(string name) => (Type)this.cache[name]!;
    }
}