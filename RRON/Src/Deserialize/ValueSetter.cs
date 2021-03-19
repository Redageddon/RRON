using System;
using System.Collections;
using System.Collections.Generic;
using FastMember;
using RRON.SpanAddons;

namespace RRON.Deserialize
{
    internal static class ValueSetter
    {
        private static object ConvertCollection(this TypeSplitEnumerator typeEnumerator, Type containedType, Type collectionType, int count)
        {
            if (collectionType.IsArray)
            {
                Array array = Array.CreateInstance(containedType, count);

                int i = 0;

                foreach (object? o in typeEnumerator)
                {
                    array.SetValue(o, i++);
                }

                return array;
            }

            IList list = (IList)Activator.CreateInstance(collectionType)!;

            foreach (object? o in typeEnumerator)
            {
                list.Add(o);
            }

            return list;
        }

        private static object CreateComplex(this Type propertyType, SplitEnumerator propertyNames, SplitEnumerator valueEnumerator)
        {
            ObjectAccessor? semiAccessor = ObjectAccessor.Create(Activator.CreateInstance(propertyType)!);
            Dictionary<string, Type> typeMap = TypeNameMap.GetOrCreate(propertyType);

            foreach (ReadOnlySpan<char> readOnlySpan in valueEnumerator)
            {
                propertyNames.MoveNext();
                string currentName = propertyNames.Current.ToString();
                semiAccessor[currentName] = typeMap[currentName].ConvertSpan(readOnlySpan);
            }

            return semiAccessor.Target!;
        }

        internal static object GetCollection(Type propertyType, ReadOnlySpan<char> propertyValues)
        {
            Type containedType = propertyType.GetContainedType();

            return propertyValues
                   .SplitWithType(containedType)
                   .ConvertCollection(containedType, propertyType, propertyValues.Count(',') + 1);
        }

        internal static object GetComplex(Type propertyType, ReadOnlySpan<char> propertyNames, ReadOnlySpan<char> propertyValues) =>
            propertyType.CreateComplex(propertyNames.GetSplitEnumerator(), propertyValues.GetSplitEnumerator());

        internal static object GetComplexCollection(SplitEnumerator propertyNames,
                                                    Type propertyType,
                                                    int propertyTotal,
                                                    ref ValueStringReader valueStringReader)
        {
            Type containedType = propertyType.GetContainedType();

            if (propertyType.IsArray)
            {
                int i = 0;

                Array array = Array.CreateInstance(containedType, propertyTotal);

                foreach (SplitEnumerator splitEnumerator in valueStringReader.ReadToBlockEnd)
                {
                    array.SetValue(containedType.CreateComplex(propertyNames, splitEnumerator), i++);
                }

                return array;
            }

            IList list = (IList)Activator.CreateInstance(propertyType)!;

            foreach (SplitEnumerator splitEnumerator in valueStringReader.ReadToBlockEnd)
            {
                list.Add(containedType.CreateComplex(propertyNames, splitEnumerator));
            }

            return list;
        }

        internal static Type GetContainedType(this Type propertyType) =>
            (propertyType.IsArray
                ? propertyType.GetElementType()
                : propertyType.GetGenericArguments()[0])!;

        internal static object GetSingle(Type propertyType, ReadOnlySpan<char> value) => propertyType.ConvertSpan(value);
    }
}