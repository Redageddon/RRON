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
        private enum LineType
        {
            CustomClassCollection,
            CustomClass,
            Collection,
            Value
        }

        /// <summary>
        /// Deserializes the RRON to the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="serializationStream">The RRON stream to deserialize.</param>
        /// <param name="ignoreOptions">The properties, by name, that are to be ignored.</param>
        /// <returns>The deserialized object from the JSON string.</returns>
        public T Deserialize<T>(Stream serializationStream) where T : new()
        {
            T instance = new T();

            StreamReader streamReader = new StreamReader(serializationStream);
            string noBlankLines = Regex.Replace(streamReader.ReadToEnd(), @"^\s+[\r\n]*", string.Empty, RegexOptions.Multiline);

            using (StringReader reader = new StringReader(noBlankLines))
            {
                string currentLine;
                while ((currentLine = reader.ReadLine()) != null)
                {
                    switch (GetLineType(currentLine))
                    {
                        case LineType.CustomClassCollection:
                            DeserializeCustomClassCollection(currentLine, reader, ref instance);
                            break;
                        case LineType.CustomClass:
                            DeserializeCustomClass(currentLine, reader, ref instance);
                            break;
                        case LineType.Collection:
                            DeserializeCollection(currentLine, reader, ref instance);
                            break;
                        case LineType.Value:
                            DeserializeValue(currentLine, ref instance);
                            break;
                    }
                }
            }

            return instance;
        }

        private static void DeserializeCustomClassCollection<T>(string currentLine, TextReader reader, ref T instance)
        {
            int index = 2;
            string propertyName = FromIndexToChar(currentLine, ref index, ':');
            index += 2;
            string[] propertyProperties = FromIndexToChar(currentLine, ref index, ']').Split(new[] {", "}, StringSplitOptions.None);

            PropertyInfo listStart = typeof(T).GetProperty(propertyName);
            Type currentLineInfoType = listStart.PropertyType.GetGenericArguments()[0];
            
            object setter = typeof(T).GetProperty(propertyName).GetValue(instance);

            while ((currentLine = reader.ReadLine()) != null && currentLine != "]")
            {
                object semiInstance = Activator.CreateInstance(currentLineInfoType);
                object valToSet = SetByLine(currentLine, propertyProperties, currentLineInfoType, semiInstance);
                listStart.PropertyType.GetMethod("Add").Invoke(setter, new[] {valToSet});
            }
        }

        private static void DeserializeCustomClass<T>(string currentLine, TextReader reader, ref T instance)
        {
            int index = 1;
            string propertyName = FromIndexToChar(currentLine, ref index, ':');
            index += 2;
            string[] propertyProperties = FromIndexToChar(currentLine, ref index, ']').Split(new[] {", "}, StringSplitOptions.None);

            PropertyInfo currentLineInfo = typeof(T).GetProperty(propertyName);
            object semiInstance = Activator.CreateInstance(currentLineInfo.PropertyType);

            currentLine = reader.ReadLine();
            object valToSet = SetByLine(currentLine, propertyProperties, currentLineInfo.PropertyType, semiInstance);
            currentLineInfo.SetValue(instance, valToSet);
        }

        private static void DeserializeCollection<T>(string currentLine, TextReader reader, ref T instance)
        {
            int index = 1;
            string propertyName = FromIndexToChar(currentLine, ref index, ']');
            object setter = typeof(T).GetProperty(propertyName).GetValue(instance);
            PropertyInfo listStart = typeof(T).GetProperty(propertyName);


            currentLine = reader.ReadLine();
            Type listType = listStart.PropertyType.GetGenericArguments()[0];
            string[] values = StringSplitter(currentLine, listType == typeof(string));
            foreach (string item in values)
            {
                listStart.PropertyType.GetMethod("Add").Invoke(setter, new[] {TypeDescriptor.GetConverter(listType).ConvertFromString(item)});
            }
        }

        private static void DeserializeValue<T>(string currentLine, ref T instance)
        {
            int index = 0;
            string name = FromIndexToChar(currentLine, ref index, ':');
            string value = currentLine.Replace(name + ": ", "");
            typeof(T).GetProperty(name).SetValue(instance, TypeDescriptor.GetConverter(typeof(T).GetProperty(name).PropertyType).ConvertFromString(value));
        }

        private static object SetByLine(string currentLine, IReadOnlyList<string> propertyProperties, Type currentLineInfoType, object semiInstance)
        {
            string[] values = StringSplitter(currentLine);

            for (int i = 0; i < propertyProperties.Count; i++)
            {
                PropertyInfo currentProperty = currentLineInfoType.GetProperty(propertyProperties[i]);
                if (typeof(ICollection).IsAssignableFrom(currentProperty.PropertyType))
                {
                    Type listType = currentProperty.PropertyType.GetGenericArguments()[0];
                    string[] innerValues = StringSplitter(values[i], listType == typeof(string));
                    object setter = semiInstance.GetType().GetProperty(currentProperty.Name).GetValue(semiInstance);
                    foreach (string val in innerValues)
                    {
                        setter.GetType().GetMethod("Add").Invoke(setter, new[] {TypeDescriptor.GetConverter(listType).ConvertFromString(val)});
                    }
                }
                else
                {
                    object value = TypeDescriptor.GetConverter(currentProperty.PropertyType).ConvertFromString(values[i]);
                    semiInstance.GetType().GetProperty(currentProperty.Name).SetValue(semiInstance, value);
                }
            }

            return semiInstance;
        }

        private static LineType GetLineType(string input)
        {
            if (input[0] == '[' && input[input.Length - 1] == ']')
            {
                if (input[1] == '[')
                {
                    return LineType.CustomClassCollection;
                }

                return input.Contains(':') ? LineType.CustomClass : LineType.Collection;
            }

            return LineType.Value;
        }

        private static string FromIndexToChar(string line, ref int index, char readTo)
        {
            StringBuilder builder = new StringBuilder();

            for (; index < line.Length; index++)
            {
                if (line[index] == readTo) break;
                builder.Append(line[index]);
            }

            return builder.ToString();
        }

        private static string[] StringSplitter(string line, bool isString = false)
        {
            List<string> list = new List<string>();
            StringBuilder builder = new StringBuilder();
            bool listOpen = false;

            for (int index = 0; index < line.Length; index++)
            {
                char character = line[index];
                switch (character)
                {
                    case '<':
                        listOpen = true;
                        break;
                    case '>':
                        listOpen = false;
                        break;
                    case ' ':
                        break;
                    case ',':
                        if (isString)
                        {
                            if (line[index - 1] == '\\')
                            {
                                if (!listOpen)
                                {
                                    builder.Length--;
                                    list.Add(builder.ToString());
                                    builder.Clear();
                                }
                                else goto default;
                            }
                            else
                            {
                                goto default;
                            }
                        }
                        else
                        {
                            if (!listOpen)
                            {
                                list.Add(builder.ToString());
                                builder.Clear();
                            }
                            else goto default;
                        }

                        break;

                    default:
                        builder.Append(character);
                        break;
                }
            }

            if (builder.Length > 0) list.Add(builder.ToString());
            return list.ToArray();
        }
    }
}