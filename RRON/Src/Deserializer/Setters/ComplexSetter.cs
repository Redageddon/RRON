using System;

namespace RRON.Deserializer.Setters
{
    public static partial class ValueSetter
    {
        public static void SetComplex<T>(T instance, string name, string[] propertyNames, string[] propertyValues)
        {
           property = Type.GetProperty(name) ?? throw new NullReferenceException($"{nameof(SetComplex)}: {nameof(property)} should not be null");
            
            property.SetValue(instance, property.PropertyType.CreateComplex(propertyNames, propertyValues));
        }
    }
}