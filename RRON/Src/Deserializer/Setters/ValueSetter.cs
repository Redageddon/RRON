using System;
using System.Reflection;

namespace RRON.Deserializer.Setters
{
    internal static partial class ValueSetter
    {
        public static  Type         Type { get; set; } = null!;
        internal static PropertyInfo Property = null!;
    }
}