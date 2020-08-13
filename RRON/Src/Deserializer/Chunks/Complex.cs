using System.Collections.Generic;
using RRON.Helpers;

namespace RRON.Deserializer.Chunks
{
    public readonly struct Complex : ITypeAcessable
    {
        public Complex(string name, IEnumerable<string> propertyNames, IEnumerable<string> propertyValues)
        {
            this.Name = name;
            this.PropertyNames = propertyNames;
            this.PropertyValues = propertyValues;
        }

        public string Name { get; }

        private IEnumerable<string> PropertyNames { get; }

        private IEnumerable<string> PropertyValues { get; }

        public object GetObject<T>() =>
            typeof(T).GetProperty(this.Name).PropertyType.CreateComplex(this.PropertyNames, this.PropertyValues);
    }
}