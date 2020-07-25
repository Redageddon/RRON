using System;

namespace RRON.Deserializer.Setters
{
    internal static partial class ValueSetter
    {
        internal static void SetComplex<T>(T instance, string name, string[] propertyNames, string[] propertyValues)
        {
           Property = Type.GetProperty(name) ?? throw new NullReferenceException($"{nameof(SetComplex)}: {nameof(Property)} should not be null");
            
            Property.SetValue(instance, Property.PropertyType.CreateComplex(propertyNames, propertyValues));
        }
    }
}