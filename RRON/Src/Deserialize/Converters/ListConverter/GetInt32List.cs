using System;
using System.Collections.Generic;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static List<int> ConvertInt32List(this SplitEnumerator typeEnumerator, int count)
        {
            List<int> list = new(count);

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(span.ParseInt32());
            }

            return list;
        }
    }
}