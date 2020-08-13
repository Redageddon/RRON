using System;
using System.Collections.Generic;
using System.Linq;
using RRON.Helpers;

namespace RRON.Deserializer.Chunks
{
    public class StringTransformers
    {
        public static Func<Type, object> GetComplexCollection(IEnumerable<string> propertyNames, IEnumerable<IEnumerable<string>> propertyValues) =>
            propertyType =>
            {
                Type containedType = propertyType.GetContainedType();

                return propertyValues.Select(values => containedType.CreateComplex(propertyNames, values))
                                     .CollectionConverter(containedType, propertyType);
            };

        public static Func<Type, object> GetComplex(IEnumerable<string> propertyNames, IEnumerable<string> propertyValues) =>
            propertyType => propertyType.CreateComplex(propertyNames, propertyValues);

        public static Func<Type, object> GetCollection(IEnumerable<string> propertyValues) =>
            propertyType => propertyValues.CollectionConverter(propertyType.GetContainedType(), propertyType);

        public static Func<Type, object> GetSingle(string value) =>
            propertyType => propertyType.StringTypeConverter(value);
    }
}