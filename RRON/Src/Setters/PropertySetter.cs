using System;
using System.ComponentModel;
using RRON.StringDestructors;

namespace RRON.Setters
{
    public static partial class ValueSetter
    {
        public static void SetProperty<T>(this string match, ref T instance)
        {
            match.PropertyDeconstruction(out string name, out string value);
            property = Type.GetProperty(name) ?? throw new NullReferenceException($"{nameof(SetProperty)}: {nameof(property)} should not be null");
            
            property.SetValue(instance, TypeDescriptor.GetConverter(property.PropertyType).ConvertFromString(value));
        }
    }
}