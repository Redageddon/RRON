using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace RRON.Helpers
{
    /// <summary>
    ///     The class responsible for string type conversion.
    /// </summary>
    internal static class ConvertHelper
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
        internal static object StringTypeConverter(this Type type, string value)
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

            return TypeDescriptor.GetConverter(type).ConvertFromString(value);
        }

        public static object CollectionConverter(this IEnumerable<object> source, Type containedType, Type collectionType)
        {
            if (source is IEnumerable<string> itemsAsStrings)
            {
                source = itemsAsStrings.Select(containedType.StringTypeConverter);
            }

            if (collectionType.IsArray)
            {
                Array array = Array.CreateInstance(containedType, source.Count());

                for (int i = 0; i < source.Count(); i++)
                {
                    array.SetValue(source.ElementAt(i), i);
                }

                return array;
            }

            IList list = (IList)TypeInstanceFactory.GetInstanceOf(collectionType);

            foreach (object item in source)
            {
                list.Add(item);
            }

            return list;
        }
    }
}