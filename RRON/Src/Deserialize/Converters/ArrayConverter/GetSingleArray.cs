using System;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static float[] ConvertSingleArray(this SplitEnumerator typeEnumerator, int count)
        {
            float[] array = new float[count];

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array[i++] = span.ParseSingle();
            }

            return array;
        }
    }
}