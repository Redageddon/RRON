using System;
using System.Collections.Generic;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static List<string> ConvertStringList(this SplitEnumerator typeEnumerator, int count)
        {
            List<string> list = new(count);

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(span.ToString());
            }

            return list;
        }
    }
}