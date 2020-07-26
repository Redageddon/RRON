using System;
using System.Collections.Generic;

namespace RRON.Deserializer.Setters
{
    internal static partial class ValueSetter
    {
        internal static void SetComplexCollection<T>(T instance, string name, string[] propertyNames, List<string[]> propertyValues)
        {
            Property = Type.GetProperty(name) ?? throw new NullReferenceException($"{nameof(SetComplexCollection)}: {nameof(Property)} should not be null");
            
            Type containedType = (Property.PropertyType.IsGenericType
                ? Property.PropertyType.GetGenericArguments()[0]
                : Property.PropertyType.GetElementType()) ?? throw new NullReferenceException($"{nameof(SetComplexCollection)}: {nameof(containedType)} should not be null");
            
            object[] classCollection = new object[propertyValues.Count];
            for (int i = 0; i < propertyValues.Count; i++)
            {
                classCollection[i] = containedType.CreateComplex(propertyNames, propertyValues[i]);
            }

            Property.SetValue(instance, classCollection.Convert(containedType, Property.PropertyType, false));
        }
    }
}