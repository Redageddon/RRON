using System;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static bool[] ConvertBoolArray(this SplitEnumerator typeEnumerator, int count)
        {
            bool[] array = new bool[count];

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array[i++] = span.ParseBool();
            }

            return array;
        }
    }
}