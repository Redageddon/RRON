﻿using System;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public const int FloatingPointPrecision = 6;

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

        public static long ParseInt64(this ReadOnlySpan<char> value)
        {
            int i = 0;
            long result = 0;
            bool isNegative = false;

            if (value[0] == '-')
            {
                isNegative = true;
                i++;
            }

            for (; i < value.Length; i++)
            {
                result = (result * 10) + (value[i] - '0');
            }

            return isNegative
                ? -result
                : result;
        }

        public static int ParseInt32(this ReadOnlySpan<char> value)
        {
            int i = 0;
            int result = 0;
            bool isNegative = false;

            if (value[0] == '-')
            {
                isNegative = true;
                i++;
            }

            for (; i < value.Length; i++)
            {
                result = (result * 10) + (value[i] - '0');
            }

            return isNegative
                ? -result
                : result;
        }

        public static short ParseInt16(this ReadOnlySpan<char> parse) => (short)ParseInt32(parse);

        public static sbyte ParseSByte(this ReadOnlySpan<char> parse) => (sbyte)ParseInt32(parse);

        public static float ParseSingle(this ReadOnlySpan<char> value)
        {
            int i = 0;
            bool isNegative = false;

            if (value[0] == '-')
            {
                isNegative = true;
                i++;
            }

            int integerValue = 0;
            int decimalValue = 0;
            int decimalPlaceCount = 0;

            for (; i < value.Length && value[i] != '.'; i++)
            {
                integerValue = (integerValue * 10) + (value[i] - '0');
            }

            for (i++; i < value.Length && decimalPlaceCount < FloatingPointPrecision; i++)
            {
                decimalValue = (decimalValue * 10) + (value[i] - '0');
                decimalPlaceCount++;
            }

            if (isNegative)
            {
                return -(integerValue + (decimalValue / B10Xf[decimalPlaceCount]));
            }

            return integerValue + (decimalValue / B10Xf[decimalPlaceCount]);
        }

        public static double ParseDouble(this ReadOnlySpan<char> value)
        {
            int i = 0;
            bool isNegative = false;

            if (value[0] == '-')
            {
                isNegative = true;
                i++;
            }

            int integerValue = 0;
            int decimalValue = 0;
            int decimalPlaceCount = 0;

            for (; i < value.Length && value[i] != '.'; i++)
            {
                integerValue = (integerValue * 10) + (value[i] - '0');
            }

            for (i++; i < value.Length && decimalPlaceCount < FloatingPointPrecision * 2; i++)
            {
                decimalValue = (decimalValue * 10) + (value[i] - '0');
                decimalPlaceCount++;
            }

            if (isNegative)
            {
                return -(integerValue + (decimalValue / B10Xd[decimalPlaceCount]));
            }

            return integerValue + (decimalValue / B10Xd[decimalPlaceCount]);
        }

        // why a decimal would ever be used in rhythm games is beyond me
        public static decimal ParseDecimal(this ReadOnlySpan<char> value)
        {
            int i = 0;
            bool isNegative = false;

            if (value[0] == '-')
            {
                isNegative = true;
                i++;
            }

            int integerValue = 0;
            int decimalValue = 0;
            int decimalPlaceCount = 0;

            for (; i < value.Length && value[i] != '.'; i++)
            {
                integerValue = (integerValue * 10) + (value[i] - '0');
            }

            for (i++; i < value.Length && decimalPlaceCount < FloatingPointPrecision * 3; i++)
            {
                decimalValue = (decimalValue * 10) + (value[i] - '0');
                decimalPlaceCount++;
            }

            if (isNegative)
            {
                return -(integerValue + (decimalValue / B10Xm[decimalPlaceCount]));
            }

            return integerValue + (decimalValue / B10Xm[decimalPlaceCount]);
        }
    }
}