using System;
using RRON.Helpers;

namespace RRON.Deserializer.Setters
{
    internal static partial class ValueSetter
    {
        internal static void SetProperty(string name, string value)
        {
            Property = propertyTypeAccessor[name] ?? throw new NullReferenceException($"{nameof(SetProperty)}: {nameof(Property)} should not be null");

            Accessor[Instance, name] = Property.PropertyType.AdvancedStringConvert(value);
        }
    }
}