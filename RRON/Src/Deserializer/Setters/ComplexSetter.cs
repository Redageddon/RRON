using System.Collections.Generic;
using System.Reflection;
using RRON.Helpers;

namespace RRON.Deserializer.Setters
{
    /// <summary>
    ///     The part of ValueSetter responsible for setting Complex's.
    /// </summary>
    internal static partial class ValueSetter
    {
        /// <summary>
        ///     The method responsible for filling a Complex with rron data.
        /// </summary>
        /// <param name="name"> The name of the Complex being set to. </param>
        /// <param name="propertyNames"> All of the property names that this Complex contains. </param>
        /// <param name="propertyValues"> All of the property names' values being set to. </param>
        internal static void SetComplex(string name, IEnumerable<string> propertyNames, IEnumerable<string> propertyValues)
        {
            PropertyInfo property = PropertyTypeAccessor[name];

            Accessor[Instance, name] = property.PropertyType.CreateComplex(propertyNames, propertyValues);
        }
    }
}