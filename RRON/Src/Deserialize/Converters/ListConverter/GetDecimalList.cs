using System;
using System.Collections.Generic;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static List<decimal> ConvertDecimalList(this SplitEnumerator typeEnumerator, int count)
        {
            List<decimal> list = new(count);

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(span.ParseDecimal());
            }

            return list;
        }
    }
}