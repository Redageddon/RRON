using System;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static short[] ConvertInt16Array(this SplitEnumerator typeEnumerator, int count)
        {
            short[] array = new short[count];

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array[i++] = span.ParseInt16();
            }

            return array;
        }
    }
}