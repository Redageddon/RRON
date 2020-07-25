using System;
using System.ComponentModel;

namespace RRON.Deserializer.Setters
{
    internal static partial class ValueSetter
    {
        internal static void SetProperty<T>(T instance, string name, string value)
        {
            Property = Type.GetProperty(name) ?? throw new NullReferenceException($"{nameof(SetProperty)}: {nameof(Property)} should not be null");
            
            Property.SetValue(instance, TypeDescriptor.GetConverter(Property.PropertyType).ConvertFromString(value));
        }
    }
}