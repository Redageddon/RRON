using System;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static int[] ConvertInt32Array(this SplitEnumerator typeEnumerator, int count)
        {
            int[] array = new int[count];

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array[i++] = span.ParseInt32();
            }

            return array;
        }
    }
}