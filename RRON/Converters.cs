namespace RRON
{
    using System;
    using System.Collections;
    using System.ComponentModel;

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
                return Enum.Parse(type, value, true);
            }

            return TypeDescriptor.GetConverter(type).ConvertFromString(value)!;
        }

        internal static object ConvertCollection(this object[] source, Type containedType, Type collectionType)
        {
            source = source.ConvertStrings(containedType);

            if (collectionType.IsArray)
            {
                var array = Array.CreateInstance(containedType, source.Length);

                for (var i = 0; i < source.Length; i++)
                {
                    array.SetValue(source[i], i);
                }

                return array;
            }
            else
            {
                var list = (IList)Activator.CreateInstance(collectionType)!;

                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < source.Length; i++)
                {
                    list.Add(source[i]);
                }

                return list;
            }
        }

        internal static object[] ConvertStrings(this object[] source, Type containedType)
        {
            if (source is string[] itemsAsStrings)
            {
                var tempList = new object[itemsAsStrings.Length];

                for (var i = 0; i < itemsAsStrings.Length; i++)
                {
                    tempList[i] = containedType.ConvertString(itemsAsStrings[i]);
                }

                return tempList;
            }

            return source;
        }
    }
}