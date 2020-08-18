using System;
using System.ComponentModel;

namespace RRON
{
    /// <summary>
    ///     The class responsible for string type conversion.
    /// </summary>
    internal static class StringConverter
    {
        /// <summary>
        ///     Method responsible for converting a string value to a value of a type.
        /// </summary>
        /// <param name="type"> The type being converted to. </param>
        /// <param name="value"> The string value of the type. </param>
        /// <returns> An object of type
        ///     <param name="type" />
        ///     with the value of
        ///     <param name="value" />
        ///     .
        /// </returns>
        internal static object ConvertString(this Type type, string value)
        {
            if (type == typeof(string))
            {
                return value;
            }

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
                return char.Parse(value);
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

            if (type.IsEnum)
            {
                return Enum.Parse(type, value);
            }

            return TypeDescriptor.GetConverter(type).ConvertFromString(value);
        }
    }
}