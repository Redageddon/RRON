using System;
using RRON.StringDeconstructor;

namespace RRON.Setters
{
    public static partial class ValueSetter
    {
        public static void SetClassCollection<T>(this string match, ref T instance)
        {
            match.ClassCollectionDeconstruction(out string name, out string[] propertyNames, out string[][] propertyValues);
            property = Type.GetProperty(name);
            
            Type containedType = property.PropertyType.IsGenericType
                ? property.PropertyType.GetGenericArguments()[0]
                : property.PropertyType.GetElementType();
            
            object[] classCollection = new object[propertyValues.Length];
            for (int i = 0; i < propertyValues.Length; i++)
            {
                classCollection[i] = containedType.CreateClass(propertyNames, propertyValues[i]);
            }

            property.SetValue(instance, classCollection.Convert(containedType, property.PropertyType, false));
        }
    }
}