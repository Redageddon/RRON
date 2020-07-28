using System;
using System.Collections.Generic;
using System.Reflection;
using FastMember;

namespace RRON.Deserializer.Setters
{
    internal static partial class ValueSetter
    {
        internal static Type         Type { get; set; } = null!;
        internal static Dictionary<string, PropertyInfo> propertyTypeAccessor = new Dictionary<string, PropertyInfo>();
        internal static PropertyInfo Property = null!;
        internal static TypeAccessor Accessor = null!;
        internal static object Instance = null!;
    }
}