using System;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static string[] ConvertStringArray(this SplitEnumerator typeEnumerator, int count)
        {
            string[] array = new string[count];

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array[i++] = span.ToString();
            }

            return array;
        }
    }
}