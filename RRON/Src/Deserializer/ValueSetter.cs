using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FastMember;
using RRON.Helpers;

namespace RRON.Deserializer
{
    /// <summary>
    ///     The Class responsible for setting values.
    /// </summary>
    internal static class ValueSetter
    {
        /// <summary>
        ///     A cache of all PropertyInfo's by property name.
        /// </summary>
        internal static readonly Dictionary<string, PropertyInfo> PropertyTypeAccessor = new Dictionary<string, PropertyInfo>();

        /// <summary>
        ///     Gets or sets the property <see cref="TypeAccessor"/> of the chosen generic type.
        /// </summary>
        /// Initialised with null suppression because it will always get set to, but the compiler doesn't know that.
        internal static TypeAccessor Accessor { get; set; } = null!;

        /// <summary>
        ///     Gets or sets an instance of the chosen generic type.
        /// </summary>
        /// Initialised with null suppression because it will always get set to, but the compiler doesn't know that.
        internal static object Instance { get; set; } = null!;

        /// <summary>
        ///     Gets or sets the type of the previous inputted generic. This it to tell the cache to refresh or not.
        /// </summary>
        /// Initialised with null suppression because it will always get set to, but the compiler doesn't know that.
        internal static Type PreviousType { get; set; } = null!;

        /// <summary>
        ///     The method responsible for filling a complex collection with rron data.
        /// </summary>
        /// <param name="name"> The name of the Complex collection being set to. </param>
        /// <param name="propertyNames"> All of the property names that this ComplexCollection contains. </param>
        /// <param name="propertyValues"> All of the property names' values being set to. </param>
        internal static void SetComplexCollection(string name, IEnumerable<string> propertyNames, IEnumerable<IEnumerable<string>> propertyValues)
        {
            Type propertyType = PropertyTypeAccessor[name].PropertyType;
            Type containedType = propertyType.GetContainedType();

            int propertyValuesCount = propertyValues.Count();

            object[] classCollection = new object[propertyValuesCount];

            for (int i = 0; i < propertyValuesCount; i++)
            {
                classCollection[i] = containedType.CreateComplex(propertyNames, propertyValues.ElementAt(i));
            }

            Accessor[Instance, name] = classCollection.CollectionConverter(containedType, propertyType);
        }

        /// <summary>
        ///     The method responsible for filling a collection with rron data.
        /// </summary>
        /// <param name="name"> The name of the Collection being set to. </param>
        /// <param name="propertyValues"> All values of the Collection being set to. </param>
        internal static void SetCollection(string name, IEnumerable<string> propertyValues)
        {
            Type propertyType = PropertyTypeAccessor[name].PropertyType;
            Accessor[Instance, name] = propertyValues.CollectionConverter(propertyType.GetContainedType(), propertyType);
        }

        /// <summary>
        ///     The method responsible for filling a Complex with rron data.
        /// </summary>
        /// <param name="name"> The name of the Complex being set to. </param>
        /// <param name="propertyNames"> All of the property names that this Complex contains. </param>
        /// <param name="propertyValues"> All of the property names' values being set to. </param>
        internal static void SetComplex(string name, IEnumerable<string> propertyNames, IEnumerable<string> propertyValues) =>
            Accessor[Instance, name] = PropertyTypeAccessor[name].PropertyType.CreateComplex(propertyNames, propertyValues);

        /// <summary>
        ///     The method responsible for filling a Property with rron data.
        /// </summary>
        /// <param name="name"> The name of the property being set to. </param>
        /// <param name="value"> The value of the property being set to. </param>
        internal static void SetProperty(string name, string value) =>
            Accessor[Instance, name] = PropertyTypeAccessor[name].PropertyType.AdvancedStringConvert(value);
    }
}