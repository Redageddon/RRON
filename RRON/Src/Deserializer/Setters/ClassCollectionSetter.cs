using System;
using System.Collections.Generic;
using System.Linq;
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
        internal static void SetComplexCollection(string name, IEnumerable<string> propertyNames, IEnumerable<IEnumerable<string>> propertyValues)
        {
            PropertyInfo property = PropertyTypeAccessor[name];

            Type containedType = property.GetContainedType();

            int propertyValuesCount = propertyValues.Count();

            object[] classCollection = new object[propertyValuesCount];

            for (int i = 0; i < propertyValuesCount; i++)
            {
                classCollection[i] = containedType.CreateComplex(propertyNames, propertyValues.ElementAt(i));
            }

            Accessor[Instance, name] = classCollection.Convert(containedType, property.PropertyType);
        }
    }
}