using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using FastMember;

namespace RRON.Helpers
{
    internal static class SetterHelper
    {
        internal static object CreateComplex(this Type propertyType, IEnumerable<string> propertyNames, IEnumerable<string> propertyValues)
        {
            object semiInstance = TypeInstanceFactory.GetInstanceOf(propertyType);

            TypeAccessor semiAccessor = TypeAccessor.Create(propertyType);

            for (int i = 1; i < propertyNames.Count(); i++)
            {
                string       propertyNameAtIndex = propertyNames.ElementAt(i);
                PropertyInfo semiProperty        = propertyType.GetProperty(propertyNameAtIndex);

                object value = semiProperty.PropertyType.StringTypeConverter(propertyValues.ElementAt(i - 1));

                semiAccessor[semiInstance, propertyNameAtIndex] = value;
            }

            return semiInstance;
        }

        internal static Type GetContainedType(this Type propertyType) =>
            propertyType.IsArray
                ? propertyType.GetElementType()
                : propertyType.GetGenericArguments()[0];
    }
}