using System;
using System.Collections.Generic;
using RRON.Helpers;

namespace RRON.Deserializer.Setters
{
    internal static partial class ValueSetter
    {
        internal static void SetComplexCollection(string name, Span<string> propertyNames, List<string[]> propertyValues)
        {
            Property = propertyTypeAccessor[name] ?? throw new NullReferenceException($"{nameof(SetComplexCollection)}: {nameof(Property)} should not be null");
            
            Type containedType = (Property.PropertyType.IsGenericType
                ? Property.PropertyType.GetGenericArguments()[0]
                : Property.PropertyType.GetElementType()) ?? throw new NullReferenceException($"{nameof(SetComplexCollection)}: {nameof(containedType)} should not be null");
            
            object[] classCollection = new object[propertyValues.Count];
            for (int i = 0; i < propertyValues.Count; i++)
            {
                classCollection[i] = containedType.CreateComplex(propertyNames, propertyValues[i]);
            }

            Accessor[Instance, name] = classCollection.Convert(containedType, Property.PropertyType, false);
        }
    }
}