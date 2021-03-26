using System;
using System.Collections.Generic;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static List<uint> ConvertUInt32List(this SplitEnumerator typeEnumerator, int count)
        {
            List<uint> list = new(count);

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(span.ParseUInt32());
            }

            return list;
        }
    }
}