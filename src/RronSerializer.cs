using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Cis
{
    public class RronSerializer
    {
        /// <summary>
        /// Deserializes the RRON to the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="serializationStream">The RRON stream to deserialize.</param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public T Deserialize<T>(Stream serializationStream)
        {
            object instance = Activator.CreateInstance(typeof(T));
            using (StreamReader reader = new StreamReader(serializationStream))
            { 
                string firstRow = reader.ReadLine();
                string match    = Regex.Match(firstRow, $@"\[{typeof(T).Name}: ([^]]*)\]").Groups[1].Value;
                string contents = reader.ReadToEnd();
                IEnumerable<PropertyInfo> keys = match.Split(", ").Select(name => typeof(T).GetProperty(name)).ToList();
                IEnumerable<string> values = contents.Split(", ").ToList();
                for (int i = 0; i < keys.Count(); i++)
                {
                    keys.ElementAt(i).SetValue(instance, TypeDescriptor.GetConverter(keys.ElementAt(i).PropertyType).ConvertFromString(values.ElementAt(i)));
                }
            }
            return (T) instance;
        }

        /// <summary>
        /// Serializes the specified object to a JSON string.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public string Serialize<T>(object value)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            StringWriter stringWriter = new StringWriter();
            stringWriter.WriteLine($"[{typeof(T).Name}: {string.Join(", ", properties.Select(e => e.Name))}]");
            stringWriter.WriteLine($"{string.Join(", ", properties.Select(e => e.GetValue(value)))}");
            stringWriter.Flush();
            return stringWriter.ToString();
        }
    }
}