using System;
using System.Runtime.CompilerServices;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        private const int SinglePrecision = 6;
        private const int DoublePrecision = SinglePrecision * 2;
        private const int DecimalPrecision = SinglePrecision * 3;

        private static readonly float[] B10Xf =
        {
            1E0F,
            1E1F,
            1E2F,
            1E3F,
            1E4F,
            1E5F,
            1E6F,
        };

        private static readonly double[] B10Xd =
        {
            1E0D,
            1E1D,
            1E2D,
            1E3D,
            1E4D,
            1E5D,
            1E6D,
            1E7D,
            1E8D,
            1E9D,
            1E10D,
            1E11D,
            1E12D,
        };

        private static readonly decimal[] B10Xm =
        {
            1E0M,
            1E1M,
            1E2M,
            1E3M,
            1E4M,
            1E5M,
            1E6M,
            1E7M,
            1E8M,
            1E9M,
            1E10m,
            1E11m,
            1E12m,
            1E13m,
            1E14m,
            1E15m,
            1E16m,
            1E17m,
            1E18m,
        };

        public static long ParseInt64(this in ReadOnlySpan<char> value)
        {
            int i = 0;
            long result = 0;
            int isNegative = 1;

            if (value[0] == '-')
            {
                isNegative = -1;
                i++;
            }

            while (i < value.Length)
            {
                result = (result * 10) + value[i++] - '0';
            }

            return result * isNegative;
        }

        public static int ParseInt32(this in ReadOnlySpan<char> value)
        {
            int i = 0;
            int result = 0;
            int isNegative = 1;

            if (value[0] == '-')
            {
                isNegative = -1;
                i++;
            }

            while (i < value.Length)
            {
                result = (result * 10) + value[i++] - '0';
            }

            return result * isNegative;
        }

        public static short ParseInt16(this in ReadOnlySpan<char> parse) => (short)ParseInt32(parse);

        public static sbyte ParseSByte(this in ReadOnlySpan<char> parse) => (sbyte)ParseInt32(parse);

        public static float ParseSingle(this in ReadOnlySpan<char> value)
        {
            ParseInternal(value, SinglePrecision, out int integerValue, out int decimalValue, out int decimalPlaceCount, out int isNegative);

            return (integerValue + (decimalValue / B10Xf[decimalPlaceCount])) * isNegative;
        }

        public static double ParseDouble(this in ReadOnlySpan<char> value)
        {
            ParseInternal(value, DoublePrecision, out int integerValue, out int decimalValue, out int decimalPlaceCount, out int isNegative);

            return (integerValue + (decimalValue / B10Xd[decimalPlaceCount])) * isNegative;
        }

        // why a decimal would ever be used in rhythm games is beyond me
        public static decimal ParseDecimal(this in ReadOnlySpan<char> value)
        {
            ParseInternal(value, DecimalPrecision, out int integerValue, out int decimalValue, out int decimalPlaceCount, out int isNegative);

            return (integerValue + (decimalValue / B10Xm[decimalPlaceCount])) * isNegative;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static void ParseInternal(in ReadOnlySpan<char> value, int precision, out int integerValue, out int decimalValue, out int decimalPlaceCount, out int isNegative)
        {
            integerValue = 0;
            decimalValue = 0;
            decimalPlaceCount = 0;
            isNegative = 1;

            int i = 0;

            if (value[0] == '-')
            {
                isNegative = -1;
                i++;
            }

            while (i < value.Length && value[i] != '.')
            {
                integerValue = (integerValue * 10) + value[i++] - '0';
            }

            for (i++; i < value.Length && decimalPlaceCount < precision; i++, decimalPlaceCount++)
            {
                decimalValue = (decimalValue * 10) + value[i] - '0';
            }
        }
    }
}