using System;
using System.Collections.Generic;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static List<char> ConvertCharList(this SplitEnumerator typeEnumerator, int count)
        {
            List<char> list = new(count);

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(span[0]);
            }

            return list;
        }
    }
}