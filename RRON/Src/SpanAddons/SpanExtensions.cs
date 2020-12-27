using System;
using System.ComponentModel;
using RRON.Deserialize;

namespace RRON.SpanAddons
{
    /// <summary>
    ///     Houses all extension methods for a ReadOnlySpan{char}
    /// </summary>
    public static class SpanExtensions
    {
        public static int Count(this ReadOnlySpan<char> value, char selector)
        {
            var i = 0;
            foreach (var c in value)
            {
                if (c.Equals(selector))
                {
                    i++;
                }
            }

            return i;
        }
        
        public static SplitEnumerator Split(this ReadOnlySpan<char> value) => new(value);
        
        public static TypeSplitEnumerator SplitWithType(this ReadOnlySpan<char> value, Type type) => new(value, type);

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

            var stringedValue = value.ToString();

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
    }
}