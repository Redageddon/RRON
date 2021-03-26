using System;
using System.Collections.Generic;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static List<bool> ConvertBoolList(this SplitEnumerator typeEnumerator, int count)
        {
            List<bool> list = new(count);

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(span.ParseBool());
            }

            return list;
        }
    }
}