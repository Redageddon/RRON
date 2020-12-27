using RRON.SpanAddons;

namespace RRON.Deserialize
{
    using System;
    using System.Collections;
    using FastMember;

    internal static class ValueSetter
    {
        internal static object GetComplexCollection(
            SplitEnumerator propertyNames,
            Type propertyType,
            int propertyTotal,
            ref ValueStringReader valueStringReader)
        {
            var containedType = propertyType.GetContainedType();

            if (propertyType.IsArray)
            {
                var i = 0;

                var array = Array.CreateInstance(containedType, propertyTotal);
                foreach (var splitEnumerator in valueStringReader.ReadToBlockEnd)
                {
                    array.SetValue(containedType.CreateComplex(propertyNames, splitEnumerator), i++);
                }

                return array;
            }
            else
            {
                var list = (IList)Activator.CreateInstance(propertyType)!;
                foreach (var splitEnumerator in valueStringReader.ReadToBlockEnd)
                {
                    list.Add(containedType.CreateComplex(propertyNames, splitEnumerator));
                }

                return list;
            }
        }
        
        private static object ConvertCollection(this TypeSplitEnumerator source, Type containedType, Type collectionType, int count)
        {
            if (collectionType.IsArray)
            {
                var array = Array.CreateInstance(containedType, count);

                var i = 0;
                foreach (var o in source)
                {
                    array.SetValue(o, i++);
                }

                return array;
            }

            var list = (IList)Activator.CreateInstance(collectionType)!;

            foreach (var o in source)
            {
                list.Add(o);
            }

            return list;
        }

        internal static object GetCollection(Type propertyType, ReadOnlySpan<char> propertyValues)
        {
            Type containedType = propertyType.GetContainedType();

            return propertyValues
                   .SplitWithType(containedType)
                   .ConvertCollection(containedType, propertyType, propertyValues.Count(',') + 1);
        }

        internal static object GetComplex(Type propertyType, ReadOnlySpan<char> propertyNames, ReadOnlySpan<char> propertyValues) =>
            propertyType.CreateComplex(propertyNames.Split(), propertyValues.Split());

        internal static object GetSingle(Type propertyType, ReadOnlySpan<char> value) =>
            propertyType.ConvertSpan(value);

        internal static Type GetContainedType(this Type propertyType) =>
            (propertyType.IsArray
                ? propertyType.GetElementType()
                : propertyType.GetGenericArguments()[0])!;

        private static object CreateComplex(this Type propertyType, SplitEnumerator propertyNames, SplitEnumerator valueEnumerator)
        {
            var semiAccessor = ObjectAccessor.Create(Activator.CreateInstance(propertyType)!);

            foreach (var readOnlySpan in valueEnumerator)
            {
                propertyNames.MoveNext();
                var currentName = propertyNames.Current.ToString();
                var type = propertyType.GetProperty(currentName)!.PropertyType;
                semiAccessor[currentName] = type.ConvertSpan(readOnlySpan);
            }

            return semiAccessor.Target!;
        }
    }
}