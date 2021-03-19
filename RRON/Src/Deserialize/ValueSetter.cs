using System;
using System.Collections;
using System.Collections.Generic;
using FastMember;
using RRON.Extensions;
using RRON.SpanAddons;

namespace RRON.Deserialize
{
    internal static class ValueSetter
    {
        private static object ConvertCollection(this SplitEnumerator typeEnumerator, Type containedType, Type collectionType, int count)
        {
            if (collectionType.IsArray)
            {
                Array array = Array.CreateInstance(containedType, count);

                int i = 0;

                foreach (ReadOnlySpan<char> span in typeEnumerator)
                {
                    array.SetValue(containedType.ConvertSpan(span), i++);
                }

                return array;
            }

            IList list = (IList)Activator.CreateInstance(collectionType)!;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(containedType.ConvertSpan(span));
            }

            return list;
        }

        private static object CreateComplex(this Type propertyType, SplitEnumerator propertyNameEnumerator, SplitEnumerator valueEnumerator)
        {
            ObjectAccessor? semiAccessor = ObjectAccessor.Create(Activator.CreateInstance(propertyType)!);
            Dictionary<string, Type> typeMap = TypeNameMap.GetOrCreate(propertyType);

            foreach (ReadOnlySpan<char> readOnlySpan in valueEnumerator)
            {
                propertyNameEnumerator.MoveNext();
                string currentName = propertyNameEnumerator.Current.ToString();
                semiAccessor[currentName] = typeMap[currentName].ConvertSpan(readOnlySpan);
            }

            return semiAccessor.Target!;
        }

        internal static object GetCollection(Type propertyType, ReadOnlySpan<char> propertyValues, int count)
        {
            Type containedType = propertyType.GetContainedType();

            return propertyValues
                   .GetSplitEnumerator()
                   .ConvertCollection(containedType, propertyType, count);
        }

        internal static object GetComplex(Type propertyType, ReadOnlySpan<char> propertyNames, ReadOnlySpan<char> propertyValues) =>
            propertyType.CreateComplex(propertyNames.GetSplitEnumerator(), propertyValues.GetSplitEnumerator());

        internal static object GetComplexCollection(ReadOnlySpan<char> propertyNames,
                                                    Type propertyType,
                                                    ref ValueStringReader valueStringReader,
                                                    int count)
        {
            SplitEnumerator propertyNameEnumerator = propertyNames.GetSplitEnumerator();
            Type containedType = propertyType.GetContainedType();

            if (propertyType.IsArray)
            {
                Array array = Array.CreateInstance(containedType, count);

                for (int i = 0; i < count; i++)
                {
                    array.SetValue(containedType.CreateComplex(propertyNameEnumerator, valueStringReader.ReadLine().GetSplitEnumerator()), i);
                }

                // Skips over the last closing bracket
                valueStringReader.ReadLine();

                return array;
            }

            IList list = (IList)Activator.CreateInstance(propertyType)!;

            for (int i = 0; i < count; i++)
            {
                list.Add(containedType.CreateComplex(propertyNameEnumerator, valueStringReader.ReadLine().GetSplitEnumerator()));
            }

            // Skips over the last closing bracket
            valueStringReader.ReadLine();

            return list;
        }

        internal static object GetSingle(Type propertyType, ReadOnlySpan<char> value) => propertyType.ConvertSpan(value);
    }
}