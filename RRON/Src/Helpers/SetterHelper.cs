using System;
using System.Collections;
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
        /// <summary>
        ///     Changes an <see cref="IEnumerable{T}"/> to a collection of a type.
        /// </summary>
        /// <param name="source"> Collection values. </param>
        /// <param name="containedType"> The type inside of the array or generic. Ex:  <see cref="T:int[]" />, <see cref="List{T}(int)"/>. </param>
        /// <param name="collectionType"> The actual collection type. Ex: Array, List, IEnumerable. </param>
        /// <returns> A boxed representation of the output collection. </returns>
        public static object Convert(this IEnumerable<object> source, Type containedType, Type collectionType)
        {
            if (source is IEnumerable<string> itemsAsStrings)
            {
                source = itemsAsStrings.Select(containedType.AdvancedStringConvert);
            }

            if (collectionType.IsArray)
            {
                Array array = Array.CreateInstance(containedType, source.Count());

                for (int i = 0; i < source.Count(); i++)
                {
                    array.SetValue(source.ElementAt(i), i);
                }

                return array;
            }

            IList list = (IList)Activator.CreateInstance(collectionType);
            foreach (object item in source)
            {
                list.Add(item);
            }

            return list;
        }

        /// <summary>
        ///     Creates a new complex. Complexes are classes and structs.
        /// </summary>
        /// <param name="propertyType"> The type of this complex. </param>
        /// <param name="propertyNames"> The contained property names of this complex. </param>
        /// <param name="propertyValues"> The contained property values of this complex. </param>
        /// <returns> A new instance of a complex. </returns>
        internal static object CreateComplex(this Type propertyType, IEnumerable<string> propertyNames, IEnumerable<string> propertyValues)
        {
            object semiInstance = Activator.CreateInstance(propertyType);

            TypeAccessor semiAccessor = TypeAccessor.Create(propertyType);

            // Offset of 1 because the first is just name
            for (int i = 1; i < propertyNames.Count(); i++)
            {
                string propertyNameAtIndex = propertyNames.ElementAt(i);
                PropertyInfo semiProperty = propertyType.GetProperty(propertyNameAtIndex);

                // '- 1' fixes offset
                object value = semiProperty.PropertyType.AdvancedStringConvert(propertyValues.ElementAt(i - 1));

                semiAccessor[semiInstance, propertyNameAtIndex] = value;
            }

            return semiInstance;
        }

        /// <summary>
        ///     Gets the base type of an array or generic collection.
        /// </summary>
        /// <param name="property"> The property being searched. </param>
        /// <returns> The contained type. </returns>
        internal static Type GetContainedType(this PropertyInfo property) =>
            property.PropertyType.IsArray
                ? property.PropertyType.GetElementType()
                : property.PropertyType.GetGenericArguments()[0];
    }
}