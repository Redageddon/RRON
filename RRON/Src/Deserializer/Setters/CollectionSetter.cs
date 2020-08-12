using System;
using System.Collections.Generic;
using System.Reflection;
using RRON.Helpers;

namespace RRON.Deserializer.Setters
{
    /// <summary>
    ///     The part of ValueSetter responsible for setting collections.
    /// </summary>
    internal static partial class ValueSetter
    {
        /// <summary>
        ///     The method responsible for filling a collection with rron data.
        /// </summary>
        /// <param name="name"> The name of the Collection being set to. </param>
        /// <param name="propertyValues"> All values of the Collection being set to. </param>
        internal static void SetCollection(string name, IEnumerable<string> propertyValues)
        {
            PropertyInfo property = PropertyTypeAccessor[name];
            Type containedType = property.GetContainedType();

            Accessor[Instance, name] = propertyValues.Convert(containedType, property.PropertyType);
        }
    }
}