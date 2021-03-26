using System;
using System.ComponentModel;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static object ConvertSpan(this Type type, ReadOnlySpan<char> value)
        {
            if (type == typeof(bool))
            {
                return value.ParseBool();
            }

            if (type == typeof(byte))
            {
                return value.ParseByte();
            }

            if (type == typeof(sbyte))
            {
                return value.ParseSByte();
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
                return value.ParseDouble();
            }

            if (type == typeof(float))
            {
                return value.ParseSingle();
            }

            if (type == typeof(int))
            {
                return value.ParseInt32();
            }

            if (type == typeof(uint))
            {
                return value.ParseUInt32();
            }

            if (type == typeof(long))
            {
                return value.ParseInt64();
            }

            if (type == typeof(ulong))
            {
                return value.ParseUInt64();
            }

            if (type == typeof(short))
            {
                return value.ParseInt16();
            }

            if (type == typeof(ushort))
            {
                return value.ParseUInt16();
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
    }
}