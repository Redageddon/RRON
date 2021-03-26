using System;
using System.Collections.Generic;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static List<double> ConvertDoubleList(this SplitEnumerator typeEnumerator, int count)
        {
            List<double> list = new(count);

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(span.ParseDouble());
            }

            return list;
        }
    }
}