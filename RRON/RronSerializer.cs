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
            WriteHeader(value.GetType().Name, CreateLine(value, true));
            WriteFollowers(CreateLine(value, false));

            _textWriter.Flush();
            return _textWriter.ToString();
        }

        private IEnumerable<string> CreateLine(object value, bool header)
        {
            List<string> items = new List<string>();
            foreach (PropertyInfo property in value.GetType().GetProperties())
            {
                switch (GetPropertyInfoType(property))
                {
                    case true:
                        items.Add(header ? $"<{property.Name}>" : $"<{string.Join(", ", (property.GetValue(value) as IEnumerable).Cast<object>())}>");
                        break;
                    case false:
                        items.Add($"[{property.Name}]");
                        break;
                    case null:
                        items.Add(header ? property.Name : property.GetValue(value).ToString());
                        break;
                }
            }

            return items;
        }

        private bool? GetPropertyInfoType(PropertyInfo property)
        {
            if (typeof(IEnumerable).IsAssignableFrom(property.PropertyType) && property.PropertyType != typeof(string)) return true;

            if (property.PropertyType.IsClass && property.PropertyType != typeof(string)) return false;

            return null;
        }

        private void WriteHeader(string name, IEnumerable<string> list) => _textWriter.WriteLine($"[{name}: {string.Join(", ", list)}]");

        private void WriteFollowers(IEnumerable<string> followers) => _textWriter.Write(string.Join(", ", followers));

        private void WriteFollower(object follower) => _textWriter.Write(follower);
    }
}