using System;
using System.Collections.Generic;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static List<ulong> ConvertUInt64List(this SplitEnumerator typeEnumerator, int count)
        {
            List<ulong> list = new(count);

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(span.ParseUInt64());
            }

            return list;
        }
    }
}