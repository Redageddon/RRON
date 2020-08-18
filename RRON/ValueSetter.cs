#nullable disable
using System;
using System.Collections;
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

            return classCollection.CollectionConverter(containedType, propertyType);
        }

        internal static object GetCollection(Type propertyType, IReadOnlyList<string> propertyValues) =>
            propertyValues.CollectionConverter(propertyType.GetContainedType(), propertyType);

        internal static object GetComplex(Type propertyType, IReadOnlyList<string> propertyNames, IReadOnlyList<string> propertyValues) =>
            propertyType.CreateComplex(propertyNames, propertyValues);

        internal static object GetSingle(Type propertyType, string value) =>
            propertyType.AdvancedStringConvert(value);

        private static object CreateComplex(this Type propertyType, IReadOnlyList<string> propertyNames, IReadOnlyList<string> propertyValues)
        {
            var semiAccessor = ObjectAccessor.Create(Activator.CreateInstance(propertyType));

            // Offset of 1 because the first is just name
            for (var i = 0; i < propertyNames.Count; i++)
            {
                var propertyNameAtIndex = propertyNames[i];
                object value;

                if (propertyType.GetProperty(propertyNameAtIndex) != null)
                {
                    var semiProperty = propertyType.GetProperty(propertyNameAtIndex);
                    value = semiProperty.PropertyType.AdvancedStringConvert(propertyValues[i]);
                }
                else
                {
                    // for legacy structs, or invalid data but sill allowed to pass
                    var semiField = propertyType.GetField(propertyNameAtIndex);
                    value = semiField.FieldType.AdvancedStringConvert(propertyValues[i]);
                }

                semiAccessor[propertyNameAtIndex] = value;
            }

            return semiAccessor.Target;
        }

        private static object CollectionConverter(this IReadOnlyList<object> source, Type containedType, Type collectionType)
        {
            if (source is IReadOnlyList<string> itemsAsStrings)
            {
                var tempList = new List<object>();

                foreach (var str in itemsAsStrings)
                {
                    tempList.Add(containedType.AdvancedStringConvert(str));
                }

                source = tempList;
            }

            if (collectionType.IsArray)
            {
                var array = Array.CreateInstance(containedType, source.Count);

                for (var i = 0; i < source.Count; i++)
                {
                    array.SetValue(source[i], i);
                }

                return array;
            }

            var list = (IList)Activator.CreateInstance(collectionType);

            foreach (var item in source)
            {
                list.Add(item);
            }

            return list;
        }

        private static Type GetContainedType(this Type propertyType) =>
            propertyType.IsArray
                ? propertyType.GetElementType()
                : propertyType.GetGenericArguments()[0];
    }
}