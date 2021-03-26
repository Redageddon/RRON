using System;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static uint[] ConvertUInt32Array(this SplitEnumerator typeEnumerator, int count)
        {
            uint[] array = new uint[count];

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array[i++] = span.ParseUInt32();
            }

            return array;
        }
    }
}