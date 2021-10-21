using System;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static ulong ParseUInt64(this in ReadOnlySpan<char> parse)
        {
            ulong totalValue = 0;

            foreach (char t in parse)
            {
                totalValue = (totalValue * 10) + t - '0';
            }

            return totalValue;
        }

        public static uint ParseUInt32(this in ReadOnlySpan<char> parse)
        {
            uint totalValue = 0;

            foreach (char t in parse)
            {
                totalValue = (totalValue * 10) + t - '0';
            }

            return totalValue;
        }

        public static ushort ParseUInt16(this in ReadOnlySpan<char> parse) => (ushort)ParseUInt32(parse);

        public static byte ParseByte(this in ReadOnlySpan<char> parse) => (byte)ParseUInt32(parse);
    }
}