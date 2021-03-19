using System;

namespace RRON.Extensions
{
    public static partial class Extensions
    {
        internal static Type GetContainedType(this Type propertyType) =>
            (propertyType.IsArray
                ? propertyType.GetElementType()
                : propertyType.GetGenericArguments()[0]) ?? throw new Exception("Contained type should not be null.");
    }
}