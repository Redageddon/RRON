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
                foreach (PropertyInfo property in value.GetType().GetProperties().Where(property => ignoreOptions == null || !ignoreOptions.Any(property.Name.Contains)))
                {
                    Type propertyType = property.PropertyType;
                    object propertyValue = property.GetValue(value);
                    string propertyName = property.Name;
                    string separator = ", ";
                    object header = null;
                    List<object> followerObjects = new List<object>();
                    bool customList = false;

                    if (typeof(ICollection).IsAssignableFrom(propertyType))
                    {
                        List<object> itemList = (propertyValue as IEnumerable).Cast<object>().ToList();

                        Type listType = propertyType.GetGenericArguments()[0];

                        if (listType.Namespace != "System" && !listType.IsEnum)
                        {
                            header = itemList[0].GetType().GetProperties().Select(propertyInfo => propertyInfo.Name);
                            
                            followerObjects.AddRange(itemList.Select(obj => obj.GetType().GetProperties().Select(info => GetNestedValues(info, obj))));
                            customList = true;
                        }
                        else
                        {
                            textWriter.WriteLine("\n[" + propertyName + "]");
                            if (listType == typeof(string)) separator = "\\,";
                            followerObjects.Add(itemList);
                        }
                    }
                    else if (propertyType.Namespace != "System" && !propertyType.IsEnum)
                    {
                        header = propertyType.GetProperties().Select(propertyInfo => propertyInfo.Name);
                        followerObjects.Add(propertyType.GetProperties().Select(info => GetNestedValues(info, propertyValue)));
                    }
                    else
                    {
                        textWriter.WriteLine("{0}: {1}", propertyName, propertyValue);
                    }

                    if (customList)
                    {
                        if (header != null) textWriter.WriteLine("\n[[" + propertyName + ": " + string.Join(separator, (IEnumerable<object>) header) + "]");
                    }
                    else
                    {
                        if (header != null) textWriter.WriteLine("\n[" + propertyName + ": " + string.Join(separator, (IEnumerable<object>) header) + "]");
                        header = null;
                    }

                    

                    foreach (object follower in followerObjects)
                    {
                        textWriter.WriteLine(string.Join(separator, (IEnumerable<object>) follower));
                    }
                    if(header != null) textWriter.WriteLine("]");
                }

                return textWriter.ToString();
            }
        }

        private static object GetNestedValues(PropertyInfo info, object propertyValue)
        {
            if (!typeof(ICollection).IsAssignableFrom(info.PropertyType)) return info.GetValue(propertyValue);

            IEnumerable<object> innerItemList = (info.GetValue(propertyValue) as IEnumerable).Cast<object>();
            return info.PropertyType.GetGenericArguments()[0] == typeof(string)
                ? "<" + string.Join("\\,", innerItemList) + ">"
                : "<" + string.Join(", ", innerItemList) + ">";
        }
    }
}