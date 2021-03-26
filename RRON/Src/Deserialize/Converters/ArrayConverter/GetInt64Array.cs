using System;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static long[] ConvertInt64Array(this SplitEnumerator typeEnumerator, int count)
        {
            long[] array = new long[count];

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array[i++] = span.ParseInt64();
            }

            return array;
        }
    }
}