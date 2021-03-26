using System;
using System.Collections.Generic;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static List<sbyte> ConvertSByteList(this SplitEnumerator typeEnumerator, int count)
        {
            List<sbyte> list = new(count);

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(span.ParseSByte());
            }

            return list;
        }
    }
}