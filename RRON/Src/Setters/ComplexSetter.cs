using System;
using RRON.StringDestructors;

namespace RRON.Setters
{
    public static partial class ValueSetter
    {
        public static void SetComplex<T>(this string match, ref T instance)
        {
            match.ComplexDeconstruction(out string name, out string[] propertyNames, out string[] propertyValues);
            property = Type.GetProperty(name) ?? throw new NullReferenceException($"{nameof(SetComplex)}: {nameof(property)} should not be null");
            
            property.SetValue(instance, property.PropertyType.CreateComplex(propertyNames, propertyValues));
        }
    }
}