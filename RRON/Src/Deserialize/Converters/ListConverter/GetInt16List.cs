using System;
using System.Collections.Generic;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static List<short> ConvertInt16List(this SplitEnumerator typeEnumerator, int count)
        {
            List<short> list = new(count);

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(span.ParseInt16());
            }

            return list;
        }
    }
}