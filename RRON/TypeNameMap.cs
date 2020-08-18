using System;
using System.Collections.Generic;

namespace RRON
{
    public class TypeNameMap
    {
        private readonly Dictionary<string, Type> cache = new Dictionary<string, Type>();

        public TypeNameMap(Type type)
        {
            foreach (var propertyInfo in type.GetProperties())
            {
                this.cache.Add(propertyInfo.Name, propertyInfo.PropertyType);
            }
        }

        public Type GetTypeByName(string name) => this.cache[name];
    }
}