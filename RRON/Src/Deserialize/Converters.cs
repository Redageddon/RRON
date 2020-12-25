using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace RRON.Deserialize
{
    internal static class Converters
    {
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

            if (type.IsEnum)
            {
                return Enum.Parse(type, value, true);
            }

            return TypeDescriptor.GetConverter(type).ConvertFromString(value)!;
        }

        internal static object ConvertSpan(this Type type, ReadOnlySpan<char> value)
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

            Console.WriteLine(type);
            return TypeDescriptor.GetConverter(type).ConvertFromString(stringedValue)!;
        }

        internal static object ConvertCollection(this object[] source, Type containedType, Type collectionType)
        {
            if (collectionType.IsArray)
            {
                var array = Array.CreateInstance(containedType, source.Length);

                for (var i = 0; i < source.Length; i++)
                {
                    array.SetValue(source[i], i);
                }

                return array;
            }

            var list = (IList)Activator.CreateInstance(collectionType)!;

            // ReSharper disable once ForCanBeConvertedToForeach
            for (var i = 0; i < source.Length; i++)
            {
                list.Add(source[i]);
            }

            return list;
        }
    }
}