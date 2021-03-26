using System;
using System.Collections.Generic;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static List<float> ConvertSingleList(this SplitEnumerator typeEnumerator, int count)
        {
            List<float> list = new(count);

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(span.ParseSingle());
            }

            return list;
        }
    }
}