using System;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static double[] ConvertDoubleArray(this SplitEnumerator typeEnumerator, int count)
        {
            double[] array = new double[count];

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array[i++] = span.ParseDouble();
            }

            return array;
        }
    }
}