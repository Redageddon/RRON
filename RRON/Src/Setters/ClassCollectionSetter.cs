using System;
using RRON.StringDestructors;

namespace RRON.Setters
{
    public static partial class ValueSetter
    {
        public static void SetComplexCollection<T>(this string match, ref T instance)
        {
            match.ComplexCollectionDeconstruction(out string name, out string[] propertyNames, out string[][] propertyValues);
            property = Type.GetProperty(name) ?? throw new NullReferenceException($"{nameof(SetComplexCollection)}: {nameof(property)} should not be null");
            
            Type containedType = (property.PropertyType.IsGenericType
                ? property.PropertyType.GetGenericArguments()[0]
                : property.PropertyType.GetElementType()) ?? throw new NullReferenceException($"{nameof(SetComplexCollection)}: {nameof(containedType)} should not be null");
            
            object[] classCollection = new object[propertyValues.Length];
            for (int i = 0; i < propertyValues.Length; i++)
            {
                classCollection[i] = containedType.CreateComplex(propertyNames, propertyValues[i]);
            }

            property.SetValue(instance, classCollection.Convert(containedType, property.PropertyType, false));
        }
    }
}