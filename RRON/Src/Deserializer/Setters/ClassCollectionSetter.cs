using System;

namespace RRON.Deserializer.Setters
{
    internal static partial class ValueSetter
    {
        internal static void SetComplexCollection<T>(T instance, string name, string[] propertyNames, string[][] propertyValues)
        {
            Property = Type.GetProperty(name) ?? throw new NullReferenceException($"{nameof(SetComplexCollection)}: {nameof(Property)} should not be null");
            
            Type containedType = (Property.PropertyType.IsGenericType
                ? Property.PropertyType.GetGenericArguments()[0]
                : Property.PropertyType.GetElementType()) ?? throw new NullReferenceException($"{nameof(SetComplexCollection)}: {nameof(containedType)} should not be null");
            
            object[] classCollection = new object[propertyValues.Length];
            for (int i = 0; i < propertyValues.Length; i++)
            {
                classCollection[i] = containedType.CreateComplex(propertyNames, propertyValues[i]);
            }

            Property.SetValue(instance, classCollection.Convert(containedType, Property.PropertyType, false));
        }
    }
}