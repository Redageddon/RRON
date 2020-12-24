namespace RRON
{
    using System;
    using System.Collections.Generic;
    using FastMember;

    internal static class ValueSetter
    {
        internal static object GetComplexCollection(
            Type propertyType,
            string[] propertyNames,
            IReadOnlyList<string[]> propertyValues)
        {
            var containedType = propertyType.GetContainedType();

            var classCollection = new object[propertyValues.Count];

            for (var i = 0; i < propertyValues.Count; i++)
            {
                classCollection[i] = containedType.CreateComplex(propertyNames, propertyValues[i]);
            }

            return classCollection.ConvertCollection(containedType, propertyType);
        }

        internal static object GetCollection(Type propertyType, string[] propertyValues) =>
            propertyValues.ConvertCollection(propertyType.GetContainedType(), propertyType);

        internal static object GetComplex(Type propertyType, string[] propertyNames, string[] propertyValues) =>
            propertyType.CreateComplex(propertyNames, propertyValues);

        internal static object GetSingle(Type propertyType, string value) =>
            propertyType.ConvertString(value);

        internal static Type GetContainedType(this Type propertyType) =>
            (propertyType.IsArray
                ? propertyType.GetElementType()
                : propertyType.GetGenericArguments()[0])!;

        private static object CreateComplex(this Type propertyType, string[] propertyNames, string[] propertyValues)
        {
            var semiAccessor = ObjectAccessor.Create(Activator.CreateInstance(propertyType));

            for (var i = 0; i < propertyNames.Length; i++)
            {
                var propertyNameAtIndex = propertyNames[i];

                var semiProperty = propertyType.GetProperty(propertyNameAtIndex);
                semiAccessor[propertyNameAtIndex] = semiProperty.PropertyType.ConvertString(propertyValues[i]);
            }

            return semiAccessor.Target!;
        }
    }
}