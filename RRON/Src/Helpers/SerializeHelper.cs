using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;

namespace RRON.Helpers
{
    /// <summary>
    ///     An extension methods class for helping with serialization.
    /// </summary>
    internal static class SerializeHelper
    {
        /// <summary>
        ///     Gets all property names from a type.
        /// </summary>
        /// <param name="type"> The type that the properties are getting from. </param>
        /// <returns> An <see cref="IEnumerable{t}" /> of all property names. </returns>
        internal static IEnumerable<string> GetPropertyNames(this Type type)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                yield return propertyInfo.Name;
            }
        }

        /// <summary>
        ///     Get all property values from a type.
        /// </summary>
        /// <param name="type"> The type that the properties are getting from. </param>
        /// <param name="source"> The object that the values are pulled from. </param>
        /// <returns> An <see cref="IEnumerable{t}" /> of all property values. </returns>
        internal static IEnumerable<string> GetPropertyValues(this Type type, object source)
        {
            foreach (PropertyInfo propertyInfo in type.GetProperties())
            {
                yield return propertyInfo.GetValue(source)?.ToString() ?? string.Empty;
            }
        }

        /// <summary>
        ///     Gets all values from a boxed collection.
        /// </summary>
        /// <param name="source"> The boxed collection. </param>
        /// <returns> An <see cref="IEnumerable{t}" /> of all collection values. </returns>
        internal static IEnumerable<string> GetCollectionValues(this object source)
        {
            foreach (var variable in (IList)source)
            {
                yield return variable?.ToString() ?? string.Empty;
            }
        }
    }
}