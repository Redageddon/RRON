using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using FastMember;

namespace RRON.Helpers
{
    /// <summary>
    ///     An extension methods class for helping with value setting.
    /// </summary>
    internal static class SetterHelper
    {
        private static readonly Type                                       EnumerableType = typeof(Enumerable);
        private static readonly ParameterExpression                        Param          = Expression.Parameter(typeof(IEnumerable));
        private static readonly Dictionary<Type, Func<IEnumerable, IList>> ArrayCache     = new Dictionary<Type, Func<IEnumerable, IList>>();
        private static readonly Dictionary<Type, Func<IEnumerable, IList>> ListCache      = new Dictionary<Type, Func<IEnumerable, IList>>();

        /// <summary>
        ///     Changes an <see cref="IEnumerable{T}(object)"/> to a collection of a type.
        /// </summary>
        /// <param name="source"> Collection values. </param>
        /// <param name="containedType"> The type inside of the array or generic. Ex:  <see cref="T:int[]" />, <see cref="List{T}(int)"/>. </param>
        /// <param name="collectionType"> The actual collection type. Ex: Array, List, IEnumerable. </param>
        /// <returns> A boxed representation of the output collection. </returns>
        internal static object Convert(this IEnumerable<object> source, Type containedType, Type collectionType)
        {
            if (source is IEnumerable<string> itemsAsStrings)
            {
                source = itemsAsStrings.Select(containedType.AdvancedStringConvert);
            }

            return ToIList(source, containedType, collectionType.IsArray);
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
        ///     Gets the base type of an array or generic collection.
        /// </summary>
        /// <param name="property"> The property being searched. </param>
        /// <returns> The contained type. </returns>
        internal static Type GetContainedType(this PropertyInfo property) =>
            property.PropertyType.IsArray
                ? property.PropertyType.GetElementType()
                : property.PropertyType.GetGenericArguments()[0];

        private static IList ToIList(IEnumerable source, Type type, bool toArray)
        {
            if (toArray && ArrayCache.TryGetValue(type, out var arrayFunc))
            {
                return arrayFunc(source);
            }

            if (!toArray && ListCache.TryGetValue(type, out var listFunc))
            {
                return listFunc(source);
            }

            Type[]                   typeArgs   = { type };
            MethodCallExpression     castExp    = Expression.Call(EnumerableType, "Cast", typeArgs, Param);
            MethodCallExpression     toIListExp = Expression.Call(EnumerableType, toArray ? "ToArray" : "ToList", typeArgs, castExp);
            Func<IEnumerable, IList> lambdaExp  = Expression.Lambda<Func<IEnumerable, IList>>(toIListExp, Param).Compile();

            if (toArray)
            {
                ArrayCache.Add(type, lambdaExp);
            }
            else
            {
                ListCache.Add(type, lambdaExp);
            }

            return lambdaExp(source);
        }
    }
}