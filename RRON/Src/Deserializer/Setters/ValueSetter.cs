using System;
using System.Reflection;

namespace RRON.Deserializer.Setters
{
    public static partial class ValueSetter
    {
        public static  Type         Type { get; set; } = null!;
        private static PropertyInfo property = null!;
    }
}