using System;
using RRON.StringDeconstructor;

namespace RRON.Setters
{
    public static partial class ValueSetter
    {
        public static void SetCollection<T>(this string match, ref T instance)
        {
            match.CollectionDeconstruction(out string name, out string[] values);
            property = Type.GetProperty(name);
            
            Type containedType = property.PropertyType.IsGenericType
                ? property.PropertyType.GetGenericArguments()[0]
                : property.PropertyType.GetElementType();

            Type.GetProperty(name).SetValue(instance, values.Convert(containedType, property.PropertyType));
        }
    }
}