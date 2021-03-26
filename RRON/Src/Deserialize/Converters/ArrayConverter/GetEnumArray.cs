using System;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static Enum[] ConvertEnumArray(this SplitEnumerator typeEnumerator, int count, Type enumType)
        {
            Enum[] array = new Enum[count];

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array[i++] = span.ParseEnum(enumType);
            }

            return array;
        }
    }
}