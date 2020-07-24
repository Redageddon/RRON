using System;

namespace RRON.Deserializer.Setters
{
    public static partial class ValueSetter
    {
        public static void SetCollection<T>(T instance, string name, string[] values)
        {
            property = Type.GetProperty(name) ?? throw new NullReferenceException($"{nameof(SetCollection)}: {nameof(property)} should not be null");
            
            Type containedType = (property.PropertyType.IsGenericType
                ? property.PropertyType.GetGenericArguments()[0]
                : property.PropertyType.GetElementType()) ?? throw new NullReferenceException($"{nameof(SetCollection)}: {nameof(containedType)} should not be null");

            property.SetValue(instance, values.Convert(containedType, property.PropertyType));
        }
    }
}