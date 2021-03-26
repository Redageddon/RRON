using System;
using System.Collections;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static object ConvertArray(this SplitEnumerator typeEnumerator, Type containedType, Type collectionType, int count)
        {
            if (collectionType == typeof(int[]))
            {
                return typeEnumerator.ConvertInt32Array(count);
            }

            Array array = Array.CreateInstance(containedType, count);

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array.SetValue(containedType.ConvertSpan(span), i++);
            }

            return array;
        }

        public static object ConvertList(this SplitEnumerator typeEnumerator, Type containedType, Type collectionType, int count)
        {
            IList list = (IList)Activator.CreateInstance(collectionType)!;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(containedType.ConvertSpan(span));
            }

            return list;
        }
    }
}