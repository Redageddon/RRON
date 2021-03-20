using System;

namespace RRON.Extensions
{
    public static partial class Extensions
    {
        private static ulong ParseUInt64(ReadOnlySpan<char> parse)
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

        private static uint ParseUInt32(ReadOnlySpan<char> parse) => (uint)ParseInt64(parse);

        private static ushort ParseUInt16(ReadOnlySpan<char> parse) => (ushort)ParseInt64(parse);

        private static byte ParseByte(ReadOnlySpan<char> parse) => (byte)ParseInt64(parse);
    }
}