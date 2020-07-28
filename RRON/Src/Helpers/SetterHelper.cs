using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using FastMember;

namespace RRON.Helpers
{
    internal static class SetterHelper
    {
        private static readonly MethodInfo CastMethod    = typeof(Enumerable).GetMethod(nameof(Enumerable.Cast))!;
        private static readonly MethodInfo ToListMethod  = typeof(Enumerable).GetMethod(nameof(Enumerable.ToList))!;
        private static readonly MethodInfo ToArrayMethod = typeof(Enumerable).GetMethod(nameof(Enumerable.ToArray))!;

        private static TypeConverter converter = null!;

        internal static object Convert(this IEnumerable<object> items, Type containedType, Type collectionType, bool cast = true)
        {
            object castedItems = CastMethod.MakeGenericMethod(containedType).Invoke(null, new object[] { cast ? items.Cast(containedType) : items}) 
                                 ?? throw new NullReferenceException($"{nameof(Convert)}: {nameof(castedItems)} should not be null");

            return (collectionType.IsArray 
                    ? ToArrayMethod.MakeGenericMethod(containedType).Invoke(null, new[] {castedItems}) 
                    : ToListMethod.MakeGenericMethod(containedType).Invoke(null, new[] {castedItems}))
                   ?? throw new NullReferenceException($"{nameof(Convert)}: {nameof(collectionType)} should not be null");
        }

        internal static object CreateComplex(this Type propertyType, Span<string> propertyNames, Span<string> propertyValues)
        {
            object semiInstance = Activator.CreateInstance(propertyType) ?? throw new NullReferenceException($"{nameof(CreateComplex)}: {nameof(semiInstance)} should not be null");
            TypeAccessor semiAccessor = TypeAccessor.Create(propertyType);
            
            for (int i = 0; i < propertyNames.Length; i++)
            {
                PropertyInfo semiProperty = propertyType.GetProperty(propertyNames[i]) ?? throw new NullReferenceException($"{nameof(CreateComplex)}: {nameof(semiProperty)} should not be null");
                object value = semiProperty.PropertyType.AdvancedStringConvert(propertyValues[i]);

                semiAccessor[semiInstance, propertyNames[i]] = value;
            }
            
            return semiInstance;
        }

        private static IEnumerable<object> Cast(this IEnumerable<object> source, Type collectionType)
        {
            converter = TypeDescriptor.GetConverter(collectionType);
            foreach (object item in source)
            {
                yield return converter.ConvertFrom(item);
            }
        }
    }
}