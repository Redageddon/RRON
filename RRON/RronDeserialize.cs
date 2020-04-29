using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace Inflex.Rron
{
    public partial class RronSerializer
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
            // Gets the type of the generic
            Type type = typeof(T);
            
            // Create an instance of the generic
            T instance = (T) Activator.CreateInstance(type);

            // Next two lines remove blank lines from the file
            StreamReader streamReader = new StreamReader(serializationStream);
            string noBlankLines = Regex.Replace(streamReader.ReadToEnd(), @"^\s+[\r\n]*", string.Empty, RegexOptions.Multiline);
            
            using (StringReader reader = new StringReader(noBlankLines))
            {
                // Reads first line
                string currentLine = reader.ReadLine();
                
                // iterates through all properties, skips all properties that are in ignoreOptions
                foreach (PropertyInfo property in type.GetProperties().Where(property => ignoreOptions == null || !ignoreOptions.Any(property.Name.Contains)))
                {
                    // The value that the current property is going to be set to
                    object value;
                    
                    // Only for the while loop, but it cuts down on a lot of repeated code
                    bool throughWhile = false;
                    
                    // The current properties type
                    Type propertyType = property.PropertyType;

                    // If the current property is a collection
                    if (typeof(ICollection).IsAssignableFrom(propertyType))
                    {
                        // reads a new line
                        currentLine = reader.ReadLine();
                        
                        // all values that are to be set to the current property
                        List<object> objects = new List<object>();
                        
                        // Gets the type that the list is holding. EX: List<int>, it gets int
                        Type listType = propertyType.GetGenericArguments()[0];
                        
                        // Checks if listType is a custom class and isn't an enum
                        if (listType.Namespace != "System" && !listType.IsEnum)
                        {
                            // loops through all lines as long as the current line isn't null, ends in [], and contains a colon
                            while (currentLine != null && !Regex.IsMatch(currentLine, "^\\[.*?\\]$") && !currentLine.Contains(": "))
                            {
                                // adds the value of the current row to objects
                                objects.Add(StringToProperties(currentLine, listType));
                                
                                // reads a new line
                                currentLine = reader.ReadLine();
                            }
                            // like i said above, this is used just to shorten code repetition
                            throughWhile = true;
                        }
                        // Executes if is not custom class or enum
                        else
                        {
                            // Splits the current line into chunks
                            string[] strArray = listType == typeof(string)
                                ? currentLine.Split(new[] {"\\,"}, StringSplitOptions.None)
                                : currentLine.Split(new[] {", "}, StringSplitOptions.None);
                            
                            // converts each string value to the correct type and adds it to objects
                            objects.AddRange(strArray.Select(@string => TypeDescriptor.GetConverter(listType).ConvertFromString(@string)));
                        }
                        
                        // Adds objects to value in the form of listType
                        value = ObjectListToTypeList(objects, listType);
                    }
                    // Checks if listType is a custom class and isn't an enum
                    else if (propertyType.Namespace != "System" && !propertyType.IsEnum)
                    {
                        // reads a new line
                        currentLine = reader.ReadLine();
                        
                        // sets value as the current line
                        value = StringToProperties(currentLine, propertyType);
                    }
                    // Executes if is not custom class or enum
                    else
                    {
                        // Gets value of line and sets value
                        value = TypeDescriptor.GetConverter(propertyType).ConvertFromString(currentLine.Split(new[] {": "}, StringSplitOptions.None).Last());
                    }
                    
                    // Injects value into the current property
                    property.SetValue(instance, value);
                    
                    // reads a new line
                    if (!throughWhile) currentLine = reader.ReadLine();
                }
            }
            // because you were freaking out about this
            streamReader.Dispose();
            return instance;
        }
        
        // converts an iEnumberable<object> to a specific type
        private static object ObjectListToTypeList(IEnumerable<object> items, Type type)
        {
            MethodInfo castMethod   = typeof(Enumerable).GetMethod(nameof(Enumerable.Cast))  .MakeGenericMethod(type);
            MethodInfo toListMethod = typeof(Enumerable).GetMethod(nameof(Enumerable.ToList)).MakeGenericMethod(type);

            IEnumerable<object> itemsToCast = items.Select(item => Convert.ChangeType(item, type));
            object castedItems = castMethod.Invoke(null, new object[] { itemsToCast });
            return toListMethod.Invoke(null, new[] { castedItems });
        }
        
        // converts a sting into a value of type
        private static object StringToProperties(string line, Type type)
        {
            List<string> strList = StringSplitter(line);

            object[] objArray = new object[strList.Count];
            for (int index = 0; index < type.GetProperties().Length; ++index)
            {
                if (typeof(ICollection).IsAssignableFrom(type.GetProperties()[index].PropertyType))
                {
                    Type listType = type.GetProperties()[index].PropertyType.GetGenericArguments()[0];
                    string[] strArray = listType == typeof(string) 
                        ? strList[index].Split(new[] {"\\,"}, StringSplitOptions.None) 
                        : strList[index].Split(new[] {","}, StringSplitOptions.None);
                    IEnumerable<object> asd = strArray.Select(@string => TypeDescriptor.GetConverter(listType).ConvertFromString(@string));
                    objArray[index] = ObjectListToTypeList(asd, listType);
                }
                else
                {
                    objArray[index] = TypeDescriptor.GetConverter(type.GetProperties()[index].PropertyType).ConvertFromString(strList[index]);
                }
            }
            return Activator.CreateInstance(type, objArray);
        }

        // your code, you know what it does
        private static List<string> StringSplitter(string line)
        {
            List<string> list = new List<string>();
            StringBuilder builder = new StringBuilder();
            bool listStart = false;

            foreach (char @char in line)
            {
                switch (@char)
                {
                    case '<':
                        listStart = true;
                        break;
                    case '>':
                        listStart = false;
                        break;
                    case ' ':
                        break;
                    case ',':
                        if (!listStart)
                        {
                            list.Add(builder.ToString());
                            builder.Clear();
                        }
                        else goto default;

                        break;
                    default:
                        builder.Append(@char);
                        break;
                }
            }
            
            if (builder.Length > 0)
            {
                list.Add(builder.ToString());
            }

            return list;
        }
    }
}