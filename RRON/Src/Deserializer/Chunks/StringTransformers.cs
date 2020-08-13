using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FastMember;
using RRON.Helpers;

namespace RRON.Deserializer.Chunks
{
    public class StringTransformers<T>
    {
        private readonly ObjectAccessor accessor;
        private readonly Dictionary<string, Type> propertyCache = new Dictionary<string, Type>();

        public StringTransformers(T instance)
        {
            this.accessor = ObjectAccessor.Create(instance);

            foreach (PropertyInfo? propertyInfo in typeof(T).GetProperties())
            {
                this.propertyCache.Add(propertyInfo.Name, propertyInfo.PropertyType);
            }
        }

        public void SetComplexCollection(string name, IEnumerable<string> propertyNames, IEnumerable<IEnumerable<string>> propertyValues)
        {
            Type propertyType = this.propertyCache[name];
            Type containedType = propertyType.GetContainedType();

            this.accessor[name] = propertyValues.Select(values => containedType.CreateComplex(propertyNames, values))
                                           .CollectionConverter(containedType, propertyType);
        }

        public void SetComplex(string name, IEnumerable<string> propertyNames, IEnumerable<string> propertyValues) =>
            this.accessor[name] = this.propertyCache[name].CreateComplex(propertyNames, propertyValues);

        public void SetCollection(string name, IEnumerable<string> propertyValues)
        {
            Type propertyType = this.propertyCache[name];
            this.accessor[name] = propertyValues.CollectionConverter(propertyType.GetContainedType(), propertyType);
        }

        public void SetSingle(string name, string value) =>
            this.accessor[name] = this.propertyCache[name].StringTypeConverter(value);
    }
}