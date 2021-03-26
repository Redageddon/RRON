using System;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static sbyte[] ConvertSByteArray(this SplitEnumerator typeEnumerator, int count)
        {
            sbyte[] array = new sbyte[count];

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array[i++] = span.ParseSByte();
            }

            return array;
        }
    }
}