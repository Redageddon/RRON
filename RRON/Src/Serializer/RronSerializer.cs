using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;
using RRON.Helpers;

namespace RRON.Serializer
{
    internal static class RronSerializer
    {
        internal static string Serialize(object source, string[] ignoreOptions = null!)
        {
            using TextWriter textWriter = new StringWriter();

            PropertyInfo[] properties = source.GetType().GetProperties();

            foreach (PropertyInfo property in properties.OrderBy(info => info.MetadataToken))
            {
                if (ignoreOptions?.Contains(property.Name) != true)
                {
                    Type propertyType = property.PropertyType;

                    object propertyValue = property.GetValue(source);

                    if (typeof(ICollection).IsAssignableFrom(propertyType))
                    {
                        Type containedType = propertyType.IsArray
                            ? propertyType.GetElementType()
                            : propertyType.GetGenericArguments()[0];

                        if (containedType.IsPrimitive ||
                            containedType.IsEnum)
                        {
                            textWriter.WriteLine($"{Environment.NewLine}[{property.Name}]");
                            textWriter.WriteLine(string.Join(", ", propertyValue.GetCollectionValues()));
                        }
                        else
                        {
                            textWriter.WriteLine($"{Environment.NewLine}[[{property.Name}: {string.Join(", ", containedType.GetProperties().Select(e => e.Name))}]");

                            foreach (object? value in (IList)propertyValue)
                            {
                                textWriter.WriteLine(string.Join(", ", containedType.GetPropertyValues(value)));
                            }

                            textWriter.WriteLine("]");
                        }
                    }
                    else if (propertyType.IsPrimitive ||
                             propertyType.IsEnum ||
                             propertyType == typeof(string) ||
                             propertyType == typeof(decimal))
                    {
                        textWriter.WriteLine($"{property.Name}: {property.GetValue(source)}");
                    }
                    else
                    {
                        textWriter.WriteLine($"{Environment.NewLine}[{property.Name}: {string.Join(", ", propertyType.GetProperties().Select(e => e.Name))}]");
                        textWriter.WriteLine($"{string.Join(", ", propertyType.GetPropertyValues(propertyValue))}");
                    }
                }
            }

            return textWriter.ToString() == null
                ? string.Empty
                : textWriter.ToString() !.Trim();
        }
    }
}