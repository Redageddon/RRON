using System;
using RRON.Helpers;

namespace RRON.Deserializer.Setters
{
    internal static partial class ValueSetter
    {
        internal static void SetComplex(string name, Span<string> propertyNames, Span<string> propertyValues)
        {
            Property = propertyTypeAccessor[name] ?? throw new NullReferenceException($"{nameof(SetComplex)}: {nameof(Property)} should not be null");
            
            Accessor[Instance, name] = Property.PropertyType.CreateComplex(propertyNames, propertyValues);
        }
    }
}