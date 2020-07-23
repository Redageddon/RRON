using System;
using System.ComponentModel;
using RRON.Deserializer.ReaderTemps;

namespace RRON.Deserializer.Setters
{
    public static partial class ValueSetter
    {
        public static void SetProperty<T>(this Property prop, ref T instance)
        {
            string name = prop.Name;
            string value = prop.Value;
            
            property = Type.GetProperty(name) ?? throw new NullReferenceException($"{nameof(SetProperty)}: {nameof(property)} should not be null");
            
            property.SetValue(instance, TypeDescriptor.GetConverter(property.PropertyType).ConvertFromString(value));
        }
    }
}