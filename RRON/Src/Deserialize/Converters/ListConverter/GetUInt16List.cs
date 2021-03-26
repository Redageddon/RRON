using System;
using System.Collections.Generic;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static List<ushort> ConvertUInt16List(this SplitEnumerator typeEnumerator, int count)
        {
            List<ushort> list = new(count);

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(span.ParseUInt16());
            }

            return list;
        }
    }
}