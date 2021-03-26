using System;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static ulong ParseUInt64(this ReadOnlySpan<char> parse)
        {
            ulong totalValue = 0;

            foreach (char t in parse)
            {
                totalValue *= 10;
                totalValue += t;
                totalValue -= '0';
            }

            return totalValue;
        }

        public static uint ParseUInt32(this ReadOnlySpan<char> parse)
        {
            uint totalValue = 0;

            foreach (char t in parse)
            {
                totalValue *= 10;
                totalValue += t;
                totalValue -= '0';
            }

            return totalValue;
        }

        public static ushort ParseUInt16(this ReadOnlySpan<char> parse) => (ushort)ParseInt32(parse);

        public static byte ParseByte(this ReadOnlySpan<char> parse) => (byte)ParseInt32(parse);
    }
}