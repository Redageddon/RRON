using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RRON.Helpers;

namespace RRON.Deserializer.Chunks
{
    public readonly struct ComplexCollection : ITypeAcessable
    {
        public ComplexCollection(string name, IEnumerable<string> propertyNames, IEnumerable<IEnumerable<string>> propertyValues)
        {
            this.Name = name;
            this.PropertyNames = propertyNames;
            this.PropertyValues = propertyValues;
        }

        public string Name { get; }

        private IEnumerable<string> PropertyNames { get; }

        private IEnumerable<IEnumerable<string>> PropertyValues { get; }

        public object GetObject<T>()
        {
            PropertyInfo property = typeof(T).GetProperty(this.Name);
            Type containedType = property.GetContainedType();

            IEnumerable<string> capturedNames = this.PropertyNames;
            return this.PropertyValues.Select(values => containedType.CreateComplex(capturedNames, values))
                       .CollectionConverter(containedType, property.PropertyType);
        }
    }
}