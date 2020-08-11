using System.Reflection;
using RRON.Helpers;

namespace RRON.Deserializer.Setters
{
    /// <summary>
    ///     The part of ValueSetter responsible for setting Properties.
    /// </summary>
    internal static partial class ValueSetter
    {
        /// <summary>
        ///     The method responsible for filling a Property with rron data.
        /// </summary>
        /// <param name="name"> The name of the property being set to. </param>
        /// <param name="value"> The value of the property being set to. </param>
        internal static void SetProperty(string name, string value)
        {
            PropertyInfo property = PropertyTypeAccessor[name];

            Accessor[Instance, name] = property.PropertyType.AdvancedStringConvert(value);
        }
    }
}