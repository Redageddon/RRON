using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace RRON.Helpers
{
    internal static class SerializeHelper
    {
        internal static IEnumerable<string> GetPropertyValues(this Type type, object source)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                yield return propertyInfo.GetValue(source)?.ToString() ?? string.Empty;
            }
        }

        internal static IEnumerable<string> GetCollectionValues(this object source)
        {
            foreach (object? variable in (IList)source)
            {
                yield return variable?.ToString() ?? string.Empty;
            }
        }
    }
}