using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FastMember;

namespace RRON.Helpers
{
    /// <summary>
    ///     An extension methods class for helping with value setting.
    /// </summary>
    internal static class SetterHelper
    {
        private static readonly MethodInfo CastMethod    = typeof(Enumerable).GetMethod(nameof(Enumerable.Cast)) !;
        private static readonly MethodInfo ToListMethod  = typeof(Enumerable).GetMethod(nameof(Enumerable.ToList)) !;
        private static readonly MethodInfo ToArrayMethod = typeof(Enumerable).GetMethod(nameof(Enumerable.ToArray)) !;

        /// <summary>
        ///     Changes an <see cref="IEnumerable{T}(object)"/> to a collection of a type.
        /// </summary>
        /// <param name="items"> Collection values. </param>
        /// <param name="containedType"> The type inside of the array or generic. Ex:  <see cref="T:int[]" />, <see cref="List{T}(int)"/>. </param>
        /// <param name="collectionType"> The actual collection type. Ex: Array, List, IEnumerable. </param>
        /// <returns> A boxed representation of the output collection. </returns>
        internal static object Convert(this IEnumerable<object> items, Type containedType, Type collectionType)
        {
            object castedItems = CastMethod.MakeGenericMethod(containedType)
                                           .Invoke(null, new object[] { items is IEnumerable<string> itemsAsStrings ? itemsAsStrings.Select(containedType.AdvancedStringConvert) : items });

            return collectionType.IsArray
                ? ToArrayMethod.MakeGenericMethod(containedType).Invoke(null, new[] { castedItems })
                : ToListMethod.MakeGenericMethod(containedType).Invoke(null, new[] { castedItems });
        }

        /// <summary>
        ///     Creates a new complex. Complexes are classes and structs.
        /// </summary>
        /// <param name="propertyType"> The type of this complex. </param>
        /// <param name="propertyNames"> The contained property names of this complex. </param>
        /// <param name="propertyValues"> The contained property values of this complex. </param>
        /// <returns> A new instance of a complex. </returns>
        internal static object CreateComplex(this Type propertyType, Span<string> propertyNames, Span<string> propertyValues)
        {
            object semiInstance = Activator.CreateInstance(propertyType);

            TypeAccessor semiAccessor = TypeAccessor.Create(propertyType);

            for (int i = 0; i < propertyNames.Length; i++)
            {
                PropertyInfo semiProperty = propertyType.GetProperty(propertyNames[i]);

                object value = semiProperty.PropertyType.AdvancedStringConvert(propertyValues[i]);

                semiAccessor[semiInstance, propertyNames[i]] = value;
            }

            return semiInstance;
        }

        /// <summary>
        ///     Gets the base type of an array or generic collection
        /// </summary>
        /// <param name="property"> The property being searched. </param>
        /// <returns> The contained type. </returns>
        internal static Type GetContainedType(this PropertyInfo property) =>
            property.PropertyType.IsArray
                ? property.PropertyType.GetElementType()
                : property.PropertyType.GetGenericArguments()[0];
    }
}