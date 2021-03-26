using System;
using System.Collections.Generic;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static List<long> ConvertInt64List(this SplitEnumerator typeEnumerator, int count)
        {
            List<long> list = new(count);

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(span.ParseInt64());
            }

            return list;
        }
    }
}