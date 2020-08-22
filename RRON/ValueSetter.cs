using System;
using System.Collections.Generic;
using FastMember;

namespace RRON
{
    internal static class ValueSetter
    {
        internal static object GetComplexCollection(
            Type propertyType,
            IReadOnlyList<string> propertyNames,
            IReadOnlyList<IReadOnlyList<string>> propertyValues)
        {
            var containedType = propertyType.GetContainedType();

            var propertyValuesCount = propertyValues.Count;

            var classCollection = new object[propertyValuesCount];

            for (var i = 0; i < propertyValuesCount; i++)
            {
                classCollection[i] = containedType.CreateComplex(propertyNames, propertyValues[i]);
            }

            return classCollection.ConvertCollection(containedType, propertyType);
        }

        internal static object GetCollection(Type propertyType, IReadOnlyList<string> propertyValues) =>
            propertyValues.ConvertCollection(propertyType.GetContainedType(), propertyType);

        internal static object GetComplex(Type propertyType, IReadOnlyList<string> propertyNames, IReadOnlyList<string> propertyValues) =>
            propertyType.CreateComplex(propertyNames, propertyValues);

        internal static object GetSingle(Type propertyType, string value) =>
            propertyType.ConvertString(value);

        private static object CreateComplex(this Type propertyType, IReadOnlyList<string> propertyNames, IReadOnlyList<string> propertyValues)
        {
            var semiAccessor = ObjectAccessor.Create(Activator.CreateInstance(propertyType));

            for (var i = 0; i < propertyNames.Count; i++)
            {
                var propertyNameAtIndex = propertyNames[i];

                var semiProperty = propertyType.GetProperty(propertyNameAtIndex);
                semiAccessor[propertyNameAtIndex] = semiProperty.PropertyType.ConvertString(propertyValues[i]);
            }

            return semiAccessor.Target;
        }

        internal static Type GetContainedType(this Type propertyType) =>
            propertyType.IsArray
                ? propertyType.GetElementType()
                : propertyType.GetGenericArguments()[0];
    }
}