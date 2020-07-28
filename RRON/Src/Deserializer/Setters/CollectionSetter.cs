using System;
using RRON.Helpers;

namespace RRON.Deserializer.Setters
{
    internal static partial class ValueSetter
    {
        internal static void SetCollection(string name, Span<string> propertyValues)
        {
            Property = propertyTypeAccessor[name] ?? throw new NullReferenceException($"{nameof(SetCollection)}: {nameof(Property)} should not be null");
            
            Type containedType = (Property.PropertyType.IsGenericType
                ? Property.PropertyType.GetGenericArguments()[0]
                : Property.PropertyType.GetElementType()) ?? throw new NullReferenceException($"{nameof(SetCollection)}: {nameof(containedType)} should not be null");
            
            Accessor[Instance, name] = propertyValues.ToArray().Convert(containedType, Property.PropertyType);
        }
    }
}