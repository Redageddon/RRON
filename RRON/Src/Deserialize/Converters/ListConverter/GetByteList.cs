using System;
using System.Collections.Generic;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static List<byte> ConvertByteList(this SplitEnumerator typeEnumerator, int count)
        {
            List<byte> list = new(count);

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(span.ParseByte());
            }

            return list;
        }
    }
}