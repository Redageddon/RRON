using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Inflex.Rron
{
    public class RronSerializer
    {
        private readonly TextWriter _textWriter = new StringWriter();

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
                string match = Regex.Match(firstRow, $@"\[{typeof(T).Name}: ([^]]*)\]").Groups[1].Value;
                string contents = reader.ReadToEnd();
                IEnumerable<PropertyInfo> keys = match.Split(", ").Select(name => typeof(T).GetProperty(name)).ToList();
                IEnumerable<string> values = contents.Split(", ").ToList();
                for (int i = 0; i < keys.Count(); i++)
                {
                    keys.ElementAt(i).SetValue(instance,
                        TypeDescriptor.GetConverter(keys.ElementAt(i).PropertyType).ConvertFromString(values.ElementAt(i)));
                }
            }

            return (T) instance;
        }

        /// <summary>
        /// Serializes the specified object to a JSON string.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <returns>A JSON string representation of the object.</returns>
        public string Serialize(object value)
        {
            Test(value);
            //WriteHeader(value.GetType().Name, CreateLine(value, true));
            //WriteFollowers(CreateLine(value, false));

            _textWriter.Dispose();
            return _textWriter.ToString();
        }

        /*private IEnumerable<string> CreateLine(object value, bool header)
        {
            List<string> items = new List<string>();
            foreach (PropertyInfo property in value.GetType().GetProperties())
            {
                object propValue = property.GetValue(value);
                string propName  = property.Name;
                Type propType    = property.PropertyType;
                bool isString    = propType == typeof(string);
                
                if (typeof(IEnumerable).IsAssignableFrom(propType) && !isString)
                {
                    items.Add(header ? $"<{propName}>" : $"<{string.Join(", ", (propValue as IEnumerable).Cast<object>())}>");
                }
                else if (propType.IsClass && !isString)
                {
                    items.Add($"[{propName}]");
                }
                else
                {
                    items.Add(header ? propName : propValue.ToString());
                }
            }

            return items;
        }*/

        public void Test(object value)
        {
            List<PropertyInfo> properties = value.GetType().GetProperties().ToList();
            List<object> values = new List<object>();
            List<object> names = new List<object>();
            List<object> classes = new List<object>();

            foreach (PropertyInfo property in properties)
            {
                if (typeof(ICollection).IsAssignableFrom(property.PropertyType))
                {
                    List<object> items = (property.GetValue(value) as IEnumerable).Cast<object>().ToList();
                    values.Add(items[0].GetType().Namespace == "System" ? $"<{string.Join(", ", items)}>" : $"<[{property.GetValue(value)}]>");
                }
                else if (property.GetValue(value).GetType().Namespace != "System")
                {
                    classes.Add(property.GetValue(value));
                    values.Add($"[{property.GetValue(value)}]");
                }
                else
                {
                    values.Add(property.GetValue(value));
                }
                names.Add(property.Name);
            }

            WriteHeader(value.GetType().Name, names);
            WriteFollowers(values);
            _textWriter.WriteLine();
            foreach (object item in classes)
            {
                Test(item);
            }
        }

        private void WriteHeader(string name, IEnumerable<object> list) => _textWriter.WriteLine($"[{name}: {string.Join(", ", list)}]");

        private void WriteFollowers(IEnumerable<object> followers) => _textWriter.WriteLine(string.Join(", ", followers));
    }
}