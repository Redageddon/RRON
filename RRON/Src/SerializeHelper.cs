using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace RRON
{
    public static class SerializeHelper
    {
        public static bool IsIn(this string source, IEnumerable<string> checker)
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

        public static IEnumerable<string> GetPropertyNames(this Type type)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                yield return propertyInfo.Name;
            }
        }
        
        public static IEnumerable<string> GetPropertyValues(this Type type, object source)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                yield return propertyInfo.GetValue(source).ToString();
            }
        }
        
        public static IEnumerable<string> GetValuesDynamically(this object source)
        {
            foreach (dynamic variable in (dynamic)source)
            {
                yield return variable.ToString();
            }
        }

        public static string Join(this IEnumerable<string> values, string separator = ", ")
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