using System;
using RRON.StringDestructors;

namespace RRON.Setters
{
    public static partial class ValueSetter
    {
        public static void SetCollection<T>(this string match, ref T instance)
        {
            match.CollectionDeconstruction(out string name, out string[] values);
            property = Type.GetProperty(name) ?? throw new NullReferenceException($"{nameof(SetCollection)}: {nameof(property)} should not be null");
            
            Type containedType = (property.PropertyType.IsGenericType
                ? property.PropertyType.GetGenericArguments()[0]
                : property.PropertyType.GetElementType()) ?? throw new NullReferenceException($"{nameof(SetCollection)}: {nameof(containedType)} should not be null");

            property.SetValue(instance, values.Convert(containedType, property.PropertyType));
        }
    }
}