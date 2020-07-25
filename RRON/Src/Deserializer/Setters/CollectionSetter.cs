using System;

namespace RRON.Deserializer.Setters
{
    internal static partial class ValueSetter
    {
        internal static void SetCollection<T>(T instance, string name, string[] values)
        {
            Property = Type.GetProperty(name) ?? throw new NullReferenceException($"{nameof(SetCollection)}: {nameof(Property)} should not be null");
            
            Type containedType = (Property.PropertyType.IsGenericType
                ? Property.PropertyType.GetGenericArguments()[0]
                : Property.PropertyType.GetElementType()) ?? throw new NullReferenceException($"{nameof(SetCollection)}: {nameof(containedType)} should not be null");

            Property.SetValue(instance, values.Convert(containedType, Property.PropertyType));
        }
    }
}