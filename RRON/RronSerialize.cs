using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Inflex.Rron
{
    public partial class RronSerializer
    {
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
                // iterates through all properties, skips all properties that are in ignoreOptions
                foreach (PropertyInfo property in value.GetType().GetProperties().Where(property => ignoreOptions == null || !ignoreOptions.Any(property.Name.Contains)))
                {
                    // The Type of the current property
                    Type propertyType = property.PropertyType;
                    
                    // The Value of the current property
                    object propertyValue = property.GetValue(value);
                    
                    // The Name of the current property
                    string propertyName = property.Name;
                    
                    // The String that separates list values from each other
                    string separator = ", ";
                    
                    // The header that is going to be written, only used for lists and custom classes
                    object header = null;
                    
                    // The follower/row that it to be written, used for everything
                    List<object> followerObjects = new List<object>();

                    // If the current property is a collection
                    if (typeof(ICollection).IsAssignableFrom(propertyType))
                    {
                        // Create an object list filled with the current properties values
                        List<object> itemList = (propertyValue as IEnumerable).Cast<object>().ToList();
                        
                        // Gets the type that the list is holding. EX: List<int>, it gets int
                        Type listType = propertyType.GetGenericArguments()[0];
                        
                        // Checks if listType is a custom class and isn't an enum
                        if (listType.Namespace != "System" && !listType.IsEnum)
                        {
                            // Adds a header using an obsolete filler value
                            header = itemList[0].GetType().GetProperties().Select(propertyInfo => propertyInfo.Name);
                            
                            // Adds a list of follower objects for every item in a list
                            followerObjects.AddRange(itemList.Select(obj => obj.GetType().GetProperties().Select(info => GetNestedValues(info, obj))));
                        }
                        // Executes if is not custom class or enum
                        else
                        {
                            // Writes a pseudo header for type, EX: [int]
                            textWriter.WriteLine("\n[" + propertyName + "]");
                            
                            // Changes separator to \, if type is string
                            if (listType == typeof(string)) separator = "\\,";
                            
                            // Adds the system values to followerObjects
                            followerObjects.Add(itemList);
                        }
                    }
                    // Checks if listType is a custom class and isn't an enum
                    else if (propertyType.Namespace != "System" && !propertyType.IsEnum)
                    {
                        // Adds all of the current properties' types' properties' names to the header
                        header = propertyType.GetProperties().Select(propertyInfo => propertyInfo.Name);
                        
                        // Adds all of the current properties' types' properties' values to the follower
                        followerObjects.Add(propertyType.GetProperties().Select(info => GetNestedValues(info, propertyValue)));
                    }
                    // Executes if is not custom class or enum
                    else
                    {
                        // Just writes the name and value, doesnt require header or follower
                        textWriter.WriteLine("{0}: {1}", propertyName, propertyValue);
                    }

                    // Writes the header
                    if (header != null) textWriter.WriteLine("\n[" + propertyName + ": " + string.Join(separator, (IEnumerable<object>) header) + "]");
                    
                    // Iterates through the follower list
                    foreach (object follower in followerObjects)
                    {
                        // Writes the follower
                        textWriter.WriteLine(string.Join(separator, (IEnumerable<object>)follower));
                    }
                }
                
                // Returns the full serialized string
                return textWriter.ToString();
            }
        }

        // Gets values inside of values
        private static object GetNestedValues(PropertyInfo info, object propertyValue)
        {
            // If the property info property type isn't a collection, return its value, aka pass the value back to caller
            if (!typeof(ICollection).IsAssignableFrom(info.PropertyType)) return info.GetValue(propertyValue);
            
            // Create a collection with the values of the list
            IEnumerable<object> innerItemList = (info.GetValue(propertyValue) as IEnumerable).Cast<object>();
            
            // Returns a concatenated string of all values from innerItemList
            return info.PropertyType.GetGenericArguments()[0] == typeof(string)
                ? "<" + string.Join("\\,", innerItemList) + ">"
                : "<" + string.Join(", ", innerItemList) + ">";
        }
    }
}