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
                if (ignoreOptions == null || !property.Name.IsIn(ignoreOptions))
                {
                    Type propertyType = property.PropertyType;
                    object propertyValue = property.GetValue(source) ?? throw new NullReferenceException($"{nameof(Serialize)}: {nameof(propertyValue)} should not be null");

                    if (typeof(ICollection).IsAssignableFrom(propertyType))
                    {
                        Type containedType = (propertyType.IsArray
                                                 ? propertyType.GetElementType()
                                                 : propertyType.GetGenericArguments()[0]) ?? throw new NullReferenceException($"{nameof(Serialize)}: {nameof(containedType)} should not be null");
                        
                        if (containedType.IsPrimitive || containedType.IsEnum)
                        {
                            textWriter.WriteLine($"{Environment.NewLine}[{property.Name}]");
                            textWriter.WriteLine(string.Join(", ", propertyValue.GetCollectionValues()));
                        }
                        else
                        {
                            textWriter.WriteLine($"{Environment.NewLine}[[{property.Name}: {string.Join(", ", containedType.GetPropertyNames())}]");
                            
                            foreach (object? value in (IList)propertyValue)
                            {
                                if (value == null)
                                {
                                    throw new NullReferenceException($"{nameof(Serialize)}: {nameof(value)} should not be null");
                                }

                                textWriter.WriteLine(string.Join(", ", containedType.GetPropertyValues(value)));
                            }

                            textWriter.WriteLine("]");
                        }
                    }
                    else if (propertyType.IsPrimitive || propertyType.IsEnum || propertyType == typeof(string) || propertyType == typeof(decimal))
                    {
                        textWriter.WriteLine($"{property.Name}: {property.GetValue(source)}");
                    }
                    else
                    {
                        textWriter.WriteLine($"{Environment.NewLine}[{property.Name}: {string.Join(", ", propertyType.GetPropertyNames())}]");
                        textWriter.WriteLine($"{string.Join(", ", propertyType.GetPropertyValues(propertyValue))}");
                    }
                }
            }
            
            return textWriter.ToString() == null ? "" : textWriter.ToString()!.Trim();
        }
    }
}