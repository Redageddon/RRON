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
                return ParseBool(value);
            }

            if (type == typeof(byte))
            {
                return ParseByte(value);
            }

            if (type == typeof(sbyte))
            {
                return ParseSByte(value);
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
                return ParseInt32(value);
            }

            if (type == typeof(uint))
            {
                return ParseUInt32(value);
            }

            if (type == typeof(long))
            {
                return ParseInt64(value);
            }

            if (type == typeof(ulong))
            {
                return ParseUInt64(value);
            }

            if (type == typeof(short))
            {
                return ParseInt16(value);
            }

            if (type == typeof(ushort))
            {
                return ParseUInt16(value);
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

        public static SplitEnumerator GetSplitEnumerator(this ReadOnlySpan<char> value) => new(value);
    }
}