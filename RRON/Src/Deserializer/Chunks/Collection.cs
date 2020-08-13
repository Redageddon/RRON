using System.Collections.Generic;
using System.Reflection;
using RRON.Helpers;

namespace RRON.Deserializer.Chunks
{
    public readonly struct Collection : ITypeAcessable
    {
        public Collection(string name, IEnumerable<string> propertyValues)
        {
            this.Name = name;
            this.PropertyValues = propertyValues;
        }

        public string Name { get; }

        private IEnumerable<string> PropertyValues { get; }

        public object GetObject<T>()
        {
            PropertyInfo property = typeof(T).GetProperty(this.Name);
            return this.PropertyValues.CollectionConverter(property.GetContainedType(), property.PropertyType);
        }
    }
}