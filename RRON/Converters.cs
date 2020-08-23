namespace RRON
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
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

            return TypeDescriptor.GetConverter(type).ConvertFromString(value) ?? throw new InvalidOperationException();
        }

        internal static object ConvertCollection(this IReadOnlyList<object> source, Type containedType, Type collectionType)
        {
            if (source is IReadOnlyList<string> itemsAsStrings)
            {
                var tempList = new List<object>();

                foreach (var str in itemsAsStrings)
                {
                    tempList.Add(containedType.ConvertString(str));
                }

                source = tempList;
            }

            if (collectionType.IsArray)
            {
                var array = Array.CreateInstance(containedType, source.Count);

                for (var i = 0; i < source.Count; i++)
                {
                    array.SetValue(source[i], i);
                }

                return array;
            }

            var list = (IList)Activator.CreateInstance(collectionType);

            foreach (var item in source)
            {
                list.Add(item);
            }

            return list;
        }
    }
}