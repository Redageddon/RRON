using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace RRON
{
    public static class SetterHelper
    {
        private static readonly MethodInfo CastMethod    = typeof(Enumerable).GetMethod(nameof(Enumerable.Cast))!;
        private static readonly MethodInfo ToListMethod  = typeof(Enumerable).GetMethod(nameof(Enumerable.ToList))!;
        private static readonly MethodInfo ToArrayMethod = typeof(Enumerable).GetMethod(nameof(Enumerable.ToArray))!;

        private static TypeConverter converter = null!;

        public static object Convert(this IEnumerable<object> items, Type containedType, Type collectionType, bool cast = true)
        {
            object castedItems = CastMethod.MakeGenericMethod(containedType).Invoke(null, new object[] { cast ? items.Cast(containedType) : items}) 
                                 ?? throw new NullReferenceException($"{nameof(Convert)}: {nameof(castedItems)} should not be null");

            return collectionType.GetTypeType() switch
            {
                TypeType.Array   => ToArrayMethod.MakeGenericMethod(containedType).Invoke(null, new[] {castedItems}),
                TypeType.Generic => ToListMethod.MakeGenericMethod(containedType).Invoke(null, new[] {castedItems}),
                _                => throw new ArgumentOutOfRangeException()
            } ?? throw new NullReferenceException($"{nameof(Convert)}: {nameof(collectionType)} should not be null");
        }

        public static IEnumerable<object> Cast(this IEnumerable<object> source, Type collectionType)
        {
            converter = TypeDescriptor.GetConverter(collectionType);
            foreach (object item in source)
            {
                yield return converter.ConvertFrom(item);
            }
        }

        public static TypeType GetTypeType(this Type type)
        {
            if (type.IsArray)
            {
                return TypeType.Array;
            }

            if (type.IsGenericType)
            {
                return TypeType.Generic;
            }

            if (type.IsEnum)
            {
                return TypeType.Enum;
            }

            if (type.IsClass)
            {
                return TypeType.Class;
            }

            if (type.IsInterface)
            {
                return TypeType.Interface;
            }

            if (type.IsPrimitive)
            {
                return TypeType.Primitive;
            }

            throw new NotImplementedException();
        }

        public static object CreateComplex(this Type propertyType, string[] propertyNames, string[] propertyValues)
        {
            object semiInstance = Activator.CreateInstance(propertyType) ?? throw new NullReferenceException($"{nameof(CreateComplex)}: {nameof(semiInstance)} should not be null");
            for (int i = 0; i < propertyNames.Length; i++)
            {
                PropertyInfo semiProperty = propertyType.GetProperty(propertyNames[i]) ?? throw new NullReferenceException($"{nameof(CreateComplex)}: {nameof(semiProperty)} should not be null");
                object       value        = TypeDescriptor.GetConverter(semiProperty.PropertyType).ConvertFromString(propertyValues[i]);
                semiInstance.GetType().GetProperty(semiProperty.Name)?.SetValue(semiInstance, value);   
            }

            return semiInstance;
        }
    }
}