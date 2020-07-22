using System;
using System.Reflection;

namespace RRON.Setters
{
    public static partial class ValueSetter
    {
        public static  Type         Type { get; set; } = null!;
        private static PropertyInfo property = null!;
    }
}