﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
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
            Type type = typeof(T);
            T instance = (T) Activator.CreateInstance(type);

            StreamReader streamReader = new StreamReader(serializationStream);
            string noBlankLines = Regex.Replace(streamReader.ReadToEnd(), @"^\s+[\r\n]*", string.Empty, RegexOptions.Multiline);
            streamReader.Dispose();
            using (StringReader reader = new StringReader(noBlankLines))
            {
                string currentLine = reader.ReadLine();
                foreach (PropertyInfo property in type.GetProperties().Where(property => ignoreOptions == null || !ignoreOptions.Any(property.Name.Contains)))
                {
                    object value;
                    bool throughWhile = false;
                    Type propertyType = property.PropertyType;
                    
                    if (typeof(ICollection).IsAssignableFrom(propertyType))
                    {
                        currentLine = reader.ReadLine();
                        
                        List<object> objects = new List<object>();
                        Type listType = propertyType.GetGenericArguments()[0];
                        
                        if (listType.Namespace != "System")
                        {
                            while (!Regex.IsMatch(currentLine, "^\\[.*?\\]$") && !currentLine.Contains(": "))
                            {
                                objects.Add(StringToProperties(currentLine, listType));
                                currentLine = reader.ReadLine();
                            }
                            throughWhile = true;
                        }
                        else
                        {
                            string separator = ", ";
                            if (listType == typeof(string)) separator = "\\,";
                            string[] strArray = currentLine.Split(new[] {separator}, StringSplitOptions.None);
                            objects.AddRange(strArray.Select(@string => TypeDescriptor.GetConverter(listType).ConvertFromString(@string)));
                        }
                        value = ObjectListToTypeList(objects, listType);
                    }
                    else if (propertyType.Namespace != "System")
                    {
                        currentLine = reader.ReadLine();
                        value = StringToProperties(currentLine, propertyType);
                    }
                    else
                    {
                        value = TypeDescriptor.GetConverter(propertyType).ConvertFromString(currentLine.Split(new[] {": "}, StringSplitOptions.None).Last());
                    }
                    
                    property.SetValue(instance, value);
                    if (!throughWhile) currentLine = reader.ReadLine();
                }
            }

            return instance;
        }
        
        private static object ObjectListToTypeList(IEnumerable<object> items, Type type)
        {
            MethodInfo castMethod   = typeof(Enumerable).GetMethod(nameof(Enumerable.Cast))  .MakeGenericMethod(type);
            MethodInfo toListMethod = typeof(Enumerable).GetMethod(nameof(Enumerable.ToList)).MakeGenericMethod(type);

            IEnumerable<object> itemsToCast = items.Select(item => Convert.ChangeType(item, type));
            object castedItems = castMethod.Invoke(null, new object[] { itemsToCast });
            return toListMethod.Invoke(null, new[] { castedItems });
        }
        
        private static object StringToProperties(string line, Type type)
        {
            string[] strArray = line.Split(new[] {", "}, StringSplitOptions.None);
            object[] objArray = new object[strArray.Length];
            for (int index = 0; index < type.GetProperties().Length; ++index)
                objArray[index] = TypeDescriptor.GetConverter(type.GetProperties()[index].PropertyType).ConvertFromString(strArray[index]);
            return Activator.CreateInstance(type, objArray);
        }
    }
}