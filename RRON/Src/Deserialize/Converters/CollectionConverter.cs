using System;
using System.Collections;
using RRON.SpanAddons;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static object ConvertArray(this SplitEnumerator typeEnumerator, Type containedType, int count)
        {
            if (containedType == typeof(bool))
            {
                return typeEnumerator.ConvertBoolArray(count);
            }

            if (containedType == typeof(byte))
            {
                return typeEnumerator.ConvertByteArray(count);
            }

            if (containedType == typeof(sbyte))
            {
                return typeEnumerator.ConvertSByteArray(count);
            }

            if (containedType == typeof(char))
            {
                return typeEnumerator.ConvertCharArray(count);
            }

            if (containedType == typeof(decimal))
            {
                return typeEnumerator.ConvertDecimalArray(count);
            }

            if (containedType == typeof(double))
            {
                return typeEnumerator.ConvertDoubleArray(count);
            }

            if (containedType == typeof(float))
            {
                return typeEnumerator.ConvertSingleArray(count);
            }

            if (containedType == typeof(int))
            {
                return typeEnumerator.ConvertInt32Array(count);
            }

            if (containedType == typeof(uint))
            {
                return typeEnumerator.ConvertUInt32Array(count);
            }

            if (containedType == typeof(long))
            {
                return typeEnumerator.ConvertInt64Array(count);
            }

            if (containedType == typeof(ulong))
            {
                return typeEnumerator.ConvertUInt64Array(count);
            }

            if (containedType == typeof(short))
            {
                return typeEnumerator.ConvertInt16Array(count);
            }

            if (containedType == typeof(ushort))
            {
                return typeEnumerator.ConvertUInt32Array(count);
            }

            if (containedType == typeof(string))
            {
                return typeEnumerator.ConvertStringArray(count);
            }

            Array array = Array.CreateInstance(containedType, count);

            int i = 0;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                array.SetValue(containedType.ConvertSpan(span), i++);
            }

            return array;
        }

        public static object ConvertList(this SplitEnumerator typeEnumerator, Type containedType, Type collectionType, int count)
        {
            if (containedType == typeof(bool))
            {
                return typeEnumerator.ConvertBoolList(count);
            }

            if (containedType == typeof(byte))
            {
                return typeEnumerator.ConvertByteList(count);
            }

            if (containedType == typeof(sbyte))
            {
                return typeEnumerator.ConvertSByteList(count);
            }

            if (containedType == typeof(char))
            {
                return typeEnumerator.ConvertCharList(count);
            }

            if (containedType == typeof(decimal))
            {
                return typeEnumerator.ConvertDecimalList(count);
            }

            if (containedType == typeof(double))
            {
                return typeEnumerator.ConvertDoubleList(count);
            }

            if (containedType == typeof(float))
            {
                return typeEnumerator.ConvertSingleList(count);
            }

            if (containedType == typeof(int))
            {
                return typeEnumerator.ConvertInt32List(count);
            }

            if (containedType == typeof(uint))
            {
                return typeEnumerator.ConvertUInt32List(count);
            }

            if (containedType == typeof(long))
            {
                return typeEnumerator.ConvertInt64List(count);
            }

            if (containedType == typeof(ulong))
            {
                return typeEnumerator.ConvertUInt64List(count);
            }

            if (containedType == typeof(short))
            {
                return typeEnumerator.ConvertInt16List(count);
            }

            if (containedType == typeof(ushort))
            {
                return typeEnumerator.ConvertUInt32List(count);
            }

            if (containedType == typeof(string))
            {
                return typeEnumerator.ConvertStringList(count);
            }

            IList list = (IList)Activator.CreateInstance(collectionType, count)!;

            foreach (ReadOnlySpan<char> span in typeEnumerator)
            {
                list.Add(containedType.ConvertSpan(span));
            }

            return list;
        }
    }
}