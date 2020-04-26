using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Inflex.Rron
{
    public class RronSerializer
    {
        /// <summary>
        /// Deserializes the RRON to the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="serializationStream">The RRON stream to deserialize.</param>
        /// <param name="ignoreOptions">The properties, by name, that are to be ignored.</param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public T Deserialize<T>(Stream serializationStream, string[] ignoreOptions = null)
        {
            Type type = typeof(T);
            T instance = (T) Activator.CreateInstance(type);

            using (StreamReader reader = new StreamReader(serializationStream))
            {
                string currentLine = reader.ReadLine();
                foreach (PropertyInfo property in type.GetProperties())
                {
                    // Ignores all empty spaces at the beginning of the file
                    while (string.IsNullOrWhiteSpace(currentLine))
                    {
                        currentLine = reader.ReadLine();
                    }

                    if (ignoreOptions != null && ignoreOptions.Any(property.Name.Contains))
                    {
                        continue;
                    }
                    
                    // Check if property is list
                    if (typeof(ICollection).IsAssignableFrom(property.PropertyType))
                    {
                        currentLine = reader.ReadLine();
                        // Create an empty list of the type to hold
                        IList objects = (IList) Activator.CreateInstance(property.PropertyType);
                        if (property.PropertyType.GetGenericArguments()[0].Namespace == "System")
                        {
                            
                            string[] strArray = currentLine.Split(new[]{ ", " }, StringSplitOptions.None);
                            foreach (string @string in strArray)
                            {
                                objects.Add(TypeDescriptor.GetConverter(property.PropertyType.GetGenericArguments()[0]).ConvertFromString(@string));
                            }
                        }
                        else
                        {
                            // Checks for "[...]" or ": " or empty line, loops if those aren't found
                            while (!string.IsNullOrWhiteSpace(currentLine) && !Regex.IsMatch(currentLine, "^\\[.*?\\]$") && !currentLine.Contains(": "))
                            {
                                object properties = StringToProperties(currentLine, property.PropertyType.GetGenericArguments()[0]);
                                objects.Add(properties);
                                currentLine = reader.ReadLine();
                            }
                        }

                        // Fills the instance list with things from file
                        property.SetValue(instance, objects);
                    }
                    // Checks for a custom class
                    else if (property.PropertyType.Namespace != "System")
                    {
                        currentLine = reader.ReadLine();
                        object item = StringToProperties(currentLine, property.PropertyType);
                        type.GetProperty(property.Name).SetValue(instance, item);
                        currentLine = reader.ReadLine();
                    }
                    // Default property
                    else
                    {
                        object item = TypeDescriptor.GetConverter(property.PropertyType).ConvertFromString(currentLine.Split(new[]{ ": " }, StringSplitOptions.None).Last());
                        type.GetProperty(property.Name).SetValue(instance, item);
                        currentLine = reader.ReadLine();
                    }
                }
            }
            
            return instance;
        }

        // Takes a string and converts it to the specified type
        private static object StringToProperties(string line, Type type)
        {
            string[] strArray = line.Split(new[]{ ", " }, StringSplitOptions.None);
            object[] objArray = new object[strArray.Length];
            for (int index = 0; index < type.GetProperties().Length; ++index)
                objArray[index] = TypeDescriptor.GetConverter(type.GetProperties()[index].PropertyType).ConvertFromString(strArray[index]);
            return Activator.CreateInstance(type, objArray);
        }

        /// <summary>
        /// Serializes the specified object to a JSON string.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="ignoreOptions">The properties, by name, that are to be ignored.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public string Serialize(object value, string[] ignoreOptions = null)
        {
            using (TextWriter textWriter = new StringWriter())
            {
                foreach (PropertyInfo property in value.GetType().GetProperties())
                {
                    if (ignoreOptions != null && ignoreOptions.Any(property.Name.Contains))
                    {
                        //currentLine = reader.ReadLine();
                    }
                    
                    if (ignoreOptions != null && ignoreOptions.Any(property.Name.Contains))
                    {
                        continue;
                    }
                    
                    // Check if property is list
                    if (typeof(ICollection).IsAssignableFrom(property.PropertyType))
                    {
                        if (property.PropertyType.GetGenericArguments()[0].Namespace == "System")
                        {
                            textWriter.WriteLine();
                            IList item = (IList) property.GetValue(value);
                            textWriter.WriteLine($"[{property.Name}]");
                            textWriter.WriteLine(string.Join(", ", item.Cast<object>()));
                        }
                        else
                        {
                            IEnumerator item = (property.GetValue(value) as IEnumerable).GetEnumerator();
                            item.MoveNext();
                            textWriter.WriteLine();
                            textWriter.WriteLine(
                                $"[{property.Name}: {string.Join(", ", item.Current.GetType().GetProperties().Select(propertyInfo => propertyInfo.Name))}]");
                            do
                            {
                                textWriter.WriteLine(string.Join(", ",
                                    item.Current.GetType().GetProperties().Select(propertyInfo => propertyInfo.GetValue(item.Current))));
                            } while (item.MoveNext());
                        }
                    }
                    // Checks for a custom class
                    else if (property.PropertyType.Namespace != "System")
                    {
                        textWriter.WriteLine();
                        textWriter.WriteLine(
                            $"[{property.Name}: {string.Join(", ", property.PropertyType.GetProperties().Select(propertyInfo => propertyInfo.Name))}]");
                        textWriter.WriteLine(string.Join(", ",
                            property.PropertyType.GetProperties().Select(propertyInfo => propertyInfo.GetValue(property.GetValue(value)))));
                    }
                    // Default property
                    else
                    {
                        textWriter.WriteLine($"{property.Name}: {property.GetValue(value)}");
                    }
                }

                return textWriter.ToString();
            }
        }
    }
}