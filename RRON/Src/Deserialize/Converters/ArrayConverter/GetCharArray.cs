using System;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static char[] ConvertCharArray(this SplitEnumerator typeEnumerator, int count)
        {
            char[] array = new char[count];

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array[i++] = span[0];
            }

            return array;
        }
    }
}