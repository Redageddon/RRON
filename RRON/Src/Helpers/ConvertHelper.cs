﻿using System;
using System.ComponentModel;

namespace RRON.Helpers
{
    internal static class ConvertHelper
    {
        internal static object AdvancedStringConvert(this Type type, string value)
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
    }
}