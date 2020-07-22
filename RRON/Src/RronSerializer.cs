using System;
using System.Collections;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RRON
{
    public static class RronSerializer
    {
        public static string Serialize(object source, string[] ignoreOptions = null)
        {
            using TextWriter textWriter = new StringWriter();

            PropertyInfo[] properties = source.GetType().GetProperties();

            foreach (PropertyInfo property in properties.OrderBy(info => info.GetMetadataToken()))
            {
                if (ignoreOptions == null || !property.Name.IsIn(ignoreOptions))
                {
                    Type propertyType = property.PropertyType;
                    object propertyValue = property.GetValue(source) ?? throw new NullReferenceException($"{nameof(propertyValue)} should not be null");

                    if (typeof(ICollection).IsAssignableFrom(propertyType))
                    {
                        Type containedType = (propertyType.IsArray
                            ? propertyType.GetElementType()
                            : propertyType.GetGenericArguments()[0]) ?? throw new NullReferenceException($"{nameof(containedType)} should not be null");
                        
                        if (containedType.IsPrimitive || containedType.IsEnum)
                        {
                            textWriter.WriteLine($"{Environment.NewLine}[{property.Name}]");
                            textWriter.WriteLine(propertyValue.GetValuesDynamically().Join());
                        }
                        else
                        {
                            textWriter.WriteLine($"{Environment.NewLine}[[{property.Name}: {containedType.GetPropertyNames().Join()}]");
                            foreach (object value in (dynamic)propertyValue)
                            {
                                textWriter.WriteLine(containedType.GetPropertyValues(value).Join());
                            }

                            textWriter.WriteLine("]");
                        }
                    }
                    else if (propertyType.IsPrimitive || propertyType.IsEnum || propertyType == typeof(string))
                    {
                        textWriter.WriteLine($"{property.Name}: {property.GetValue(source)}");
                    }
                    else
                    {
                        textWriter.WriteLine($"{Environment.NewLine}[{property.Name}: {propertyType.GetPropertyNames().Join()}]");
                        textWriter.WriteLine($"{propertyType.GetPropertyValues(propertyValue).Join()}");
                    }
                }
            }

            return textWriter.ToString() ?? "";
        }
    }
}