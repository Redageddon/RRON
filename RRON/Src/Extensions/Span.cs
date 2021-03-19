using System;
using System.ComponentModel;
using RRON.SpanAddons;

namespace RRON.Extensions
{
    /// <summary>
    ///     Houses all extension methods.
    /// </summary>
    public static partial class Extensions
    {
        public static object ConvertSpan(this Type type, ReadOnlySpan<char> value)
        {
            if (type == typeof(bool))
            {
                return bool.Parse(value);
            }

            if (type == typeof(byte))
            {
                return byte.Parse(value);
            }

            if (type == typeof(sbyte))
            {
                return sbyte.Parse(value);
            }

            if (type == typeof(char))
            {
                return value[0];
            }

            if (type == typeof(decimal))
            {
                return decimal.Parse(value);
            }

            if (type == typeof(double))
            {
                return double.Parse(value);
            }

            if (type == typeof(float))
            {
                return float.Parse(value);
            }

            if (type == typeof(int))
            {
                return int.Parse(value);
            }

            if (type == typeof(uint))
            {
                return uint.Parse(value);
            }

            if (type == typeof(long))
            {
                return long.Parse(value);
            }

            if (type == typeof(ulong))
            {
                return ulong.Parse(value);
            }

            if (type == typeof(short))
            {
                return short.Parse(value);
            }

            if (type == typeof(ushort))
            {
                return ushort.Parse(value);
            }

            string stringedValue = value.ToString();

            if (type == typeof(string))
            {
                return stringedValue;
            }

            if (type.IsEnum)
            {
                return Enum.Parse(type, stringedValue, true);
            }

            return TypeDescriptor.GetConverter(type).ConvertFromString(stringedValue)!;
        }

        public static int Count(this ReadOnlySpan<char> value, char selector)
        {
            int count = 0;

            foreach (char c in value)
            {
                if (c == selector)
                {
                    count++;
                }
            }

            return count;
        }

        public static SplitEnumerator GetSplitEnumerator(this ReadOnlySpan<char> value) => new(value);
    }
}