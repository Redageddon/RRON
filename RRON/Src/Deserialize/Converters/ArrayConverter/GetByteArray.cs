using System;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static byte[] ConvertByteArray(this SplitEnumerator typeEnumerator, int count)
        {
            byte[] array = new byte[count];

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array[i++] = span.ParseByte();
            }

            return array;
        }
    }
}