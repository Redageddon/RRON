using System;
using System.ComponentModel;

namespace RRON.Deserializer.Setters
{
    public static partial class ValueSetter
    {
        public static void SetProperty<T>(T instance, string name, string value)
        {
            property = Type.GetProperty(name) ?? throw new NullReferenceException($"{nameof(SetProperty)}: {nameof(property)} should not be null");
            
            property.SetValue(instance, TypeDescriptor.GetConverter(property.PropertyType).ConvertFromString(value));
        }
    }
}