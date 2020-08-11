using System;
using System.Collections.Generic;
using System.Reflection;
using RRON.Helpers;

namespace RRON.Deserializer.Setters
{
    /// <summary>
    ///     The part of ValueSetter responsible for setting complex collections.
    /// </summary>
    internal static partial class ValueSetter
    {
        /// <summary>
        ///     The method responsible for filling a complex collection with rron data.
        /// </summary>
        /// <param name="name"> The name of the Complex collection being set to. </param>
        /// <param name="propertyNames"> All of the property names that this ComplexCollection contains. </param>
        /// <param name="propertyValues"> All of the property names' values being set to. </param>
        internal static void SetComplexCollection(string name, Span<string> propertyNames, List<string[]> propertyValues)
        {
            PropertyInfo property = PropertyTypeAccessor[name];

            Type containedType = property.GetContainedType();

            object[] classCollection = new object[propertyValues.Count];

            for (int i = 0; i < propertyValues.Count; i++)
            {
                classCollection[i] = containedType.CreateComplex(propertyNames, propertyValues[i]);
            }

            Accessor[Instance, name] = classCollection.Convert(containedType, property.PropertyType);
        }
    }
}