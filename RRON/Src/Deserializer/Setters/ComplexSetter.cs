using System;
using RRON.Deserializer.ReaderTemps;

namespace RRON.Deserializer.Setters
{
    public static partial class ValueSetter
    {
        public static void SetComplex<T>(this Complex complex, ref T instance)
        {
            string name = complex.Name;
            string[] propertyNames = complex.PropertyNames;
            string[] propertyValues = complex.PropertyValues;
            
            property = Type.GetProperty(name) ?? throw new NullReferenceException($"{nameof(SetComplex)}: {nameof(property)} should not be null");
            
            property.SetValue(instance, property.PropertyType.CreateComplex(propertyNames, propertyValues));
        }
    }
}