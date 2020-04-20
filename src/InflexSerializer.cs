using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;

namespace CustomInflexSerializer
{
    public class InflexSerializer<T> : IFormatter
    {
        public object Deserialize(Stream serializationStream)
        {
            object obj = Activator.CreateInstance(typeof(T));
            using (StreamReader reader = new StreamReader(serializationStream))
            { 
                string firstRow = reader.ReadLine();

                string match    = Regex.Match(firstRow, $@"\[{typeof(T).Name}: ([^]]*)\]").Groups[1].Value;
                string contents = reader.ReadToEnd();
                
                IEnumerable<PropertyInfo> keys = match.Split(", ").Select(name => typeof(T).GetProperty(name)).ToList();
                IEnumerable<string> values = contents.Split(", ").ToList();
                
                for (int i = 0; i < keys.Count(); i++)
                {
                    keys.ElementAt(i).SetValue(obj, TypeDescriptor.GetConverter(keys.ElementAt(i).PropertyType).ConvertFromString(values.ElementAt(i)));
                }
            }
            return obj;
        }

        public void Serialize(Stream serializationStream, object graph)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            StreamWriter streamWriter = new StreamWriter(serializationStream);

            streamWriter.WriteLine($"[{typeof(T).Name}: {string.Join(", ", properties.Select(e => e.Name))}]");
            streamWriter.WriteLine($"{string.Join(", ", properties.Select(e => e.GetValue(graph)))}");

            streamWriter.Flush();
        }

        public SerializationBinder Binder { get; set; }
        public StreamingContext Context { get; set; }
        public ISurrogateSelector SurrogateSelector { get; set; }
    }
}