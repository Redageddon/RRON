using System;
using RRON.Deserializer.ReaderTemps;

namespace RRON.Deserializer.Setters
{
    public static partial class ValueSetter
    {
        public static void SetCollection<T>(this Collection collection, ref T instance)
        {
            string name = collection.Name;
            string[] values = collection.Values;
            
            property = Type.GetProperty(name) ?? throw new NullReferenceException($"{nameof(SetCollection)}: {nameof(property)} should not be null");
            
            Type containedType = (property.PropertyType.IsGenericType
                ? property.PropertyType.GetGenericArguments()[0]
                : property.PropertyType.GetElementType()) ?? throw new NullReferenceException($"{nameof(SetCollection)}: {nameof(containedType)} should not be null");

            property.SetValue(instance, values.Convert(containedType, property.PropertyType));
        }
    }
}