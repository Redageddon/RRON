using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RRON
{
    internal static class SerializeHelper
    {
        internal static bool IsIn(this string source, IEnumerable<string> checker)
        {
            foreach (string value in checker)
            {
                if (source == value)
                {
                    return true;
                }
            }

            return false;
        }

        internal static IEnumerable<string> GetPropertyNames(this Type type)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                yield return propertyInfo.Name;
            }
        }
        
        internal static IEnumerable<string> GetPropertyValues(this Type type, object source)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                yield return propertyInfo.GetValue(source)?.ToString() ?? "";
            }
        }
        
        internal static IEnumerable<string> GetCollectionValues(this object source)
        {
            foreach (object? variable in (IList)source)
            {
                yield return variable?.ToString() ?? "";
            }
        }

        internal static string Join(this IEnumerable<object> values, string separator = ", ")
        {
            StringBuilder builder = new StringBuilder();
            foreach (string value in values)
            {
                builder.Append(value + separator);
            }
            
            builder.Remove(builder.Length - separator.Length, separator.Length);
            return builder.ToString();
        }
    }
}