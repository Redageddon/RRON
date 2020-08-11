using System;
using System.Collections.Generic;
using System.Reflection;
using FastMember;

namespace RRON.Deserializer.Setters
{
    /// <summary>
    ///     The part of ValueSetter responsible for holding properties and fields.
    /// </summary>
    internal static partial class ValueSetter
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
    }
}