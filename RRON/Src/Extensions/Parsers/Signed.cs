using System;

namespace RRON.Extensions
{
    public static partial class Extensions
    {
        private static long ParseInt64(ReadOnlySpan<char> parse)
        {
            long totalValue = 0;

            foreach (char t in parse)
            {
                if (t == '-')
                {
                    totalValue *= -1;

                    break;
                }

                totalValue *= 10;
                totalValue += t - '0';
            }

            return totalValue;
        }

        private static int ParseInt32(ReadOnlySpan<char> parse) => (int)ParseInt64(parse);

        private static short ParseInt16(ReadOnlySpan<char> parse) => (short)ParseInt64(parse);

        private static sbyte ParseSByte(ReadOnlySpan<char> parse) => (sbyte)ParseInt64(parse);
    }
}