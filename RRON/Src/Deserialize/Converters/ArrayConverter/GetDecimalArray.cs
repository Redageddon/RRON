using System;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static decimal[] ConvertDecimalArray(this SplitEnumerator typeEnumerator, int count)
        {
            decimal[] array = new decimal[count];

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array[i++] = span.ParseDecimal();
            }

            return array;
        }
    }
}