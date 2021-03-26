using System;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static ushort[] ConvertUInt16Array(this SplitEnumerator typeEnumerator, int count)
        {
            ushort[] array = new ushort[count];

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array[i++] = span.ParseUInt16();
            }

            return array;
        }
    }
}