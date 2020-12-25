namespace RRON.Deserialize
{
    using System;
    using System.Collections;
    using FastMember;

    internal static class ValueSetter
    {
        // Partial string parsing happening in here to decrease allocations
        internal static object GetComplexCollection(
            Type propertyType,
            string[] propertyNames,
            ref ValueStringReader valueStringReader)
        {
            ReadOnlySpan<char> currentLine;
            var containedType = propertyType.GetContainedType();
            var propertyNamesLength = propertyNames.Length;

            if (propertyType.IsArray)
            {
                var i = 0;
                var array = Array.CreateInstance(containedType, propertyNamesLength);

                while ((currentLine = valueStringReader.ReadLine())[0] != ']')
                {
                    var values = currentLine.Split(commaCount: propertyNamesLength);
                    array.SetValue(containedType.CreateComplex(propertyNames, values), i++);
                }

                return array;
            }
            else
            {
                var list = (IList)Activator.CreateInstance(propertyType)!;

                // ReSharper disable once ForCanBeConvertedToForeach
                while ((currentLine = valueStringReader.ReadLine())[0] != ']')
                {
                    var values = currentLine.Split(commaCount: propertyNamesLength);
                    list.Add(containedType.CreateComplex(propertyNames, values));
                }

                return list;
            }
        }

        internal static object GetCollection(Type propertyType, ReadOnlySpan<char> propertyValues)
        {
            Type containedType = propertyType.GetContainedType();

            return propertyValues
                   .SplitWithType(containedType)
                   .ConvertCollection(containedType, propertyType);
        }

        internal static object GetComplex(Type propertyType, ReadOnlySpan<char> propertyNames, ReadOnlySpan<char> propertyValues) =>
            propertyType.CreateComplex(propertyNames.Split(), propertyValues.Split());

        internal static object GetSingle(Type propertyType, ReadOnlySpan<char> value) =>
            propertyType.ConvertSpan(value);

        internal static Type GetContainedType(this Type propertyType) =>
            (propertyType.IsArray
                ? propertyType.GetElementType()
                : propertyType.GetGenericArguments()[0])!;


        private static object CreateComplex(this Type propertyType, string[] propertyNames, string[] propertyValues)
        {
            var semiAccessor = ObjectAccessor.Create(Activator.CreateInstance(propertyType));

            for (var i = 0; i < propertyNames.Length; i++)
            {
                var propertyNameAtIndex = propertyNames[i];
                var type = propertyType.GetProperty(propertyNameAtIndex).PropertyType;
                semiAccessor[propertyNameAtIndex] = type.ConvertString(propertyValues[i]);
            }

            return semiAccessor.Target!;
        }
    }
}