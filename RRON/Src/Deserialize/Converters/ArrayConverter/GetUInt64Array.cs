using System;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static ulong[] ConvertUInt64Array(this SplitEnumerator typeEnumerator, int count)
        {
            ulong[] array = new ulong[count];

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array[i++] = span.ParseUInt64();
            }

            return array;
        }
    }
}