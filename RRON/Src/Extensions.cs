using System;
using RRON.SpanAddons;

namespace RRON
{
    public static class Extensions
    {
        public static SplitEnumerator GetSplitEnumerator(this in ReadOnlySpan<char> value) => new(value);

        internal static Type GetContainedType(this Type propertyType) =>
            (propertyType.IsArray
                ? propertyType.GetElementType()
                : propertyType.GetGenericArguments()[0]) ?? throw new Exception("Contained type should not be null.");
    }
}