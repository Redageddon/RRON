using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;

namespace CustomInflexSerializer
{
    public static class RronConvert
    {
        public static object Deserialize<T>(Stream serializationStream)
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
            return instance;
        }

        public static void Serialize<T>(Stream serializationStream, object graph)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            StreamWriter streamWriter = new StreamWriter(serializationStream);
            streamWriter.WriteLine($"[{typeof(T).Name}: {string.Join(", ", properties.Select(e => e.Name))}]");
            streamWriter.WriteLine($"{string.Join(", ", properties.Select(e => e.GetValue(graph)))}");
            streamWriter.Flush();
        }
        
        public static string SerializeObject<T>(object graph)
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            StringWriter stringWriter = new StringWriter();
            stringWriter.WriteLine($"[{typeof(T).Name}: {string.Join(", ", properties.Select(e => e.Name))}]");
            stringWriter.WriteLine($"{string.Join(", ", properties.Select(e => e.GetValue(graph)))}");
            stringWriter.Flush();
            return stringWriter.ToString();
        }
        
        public static void SerializeToFile<T>(object data, string path)
        {
            if (File.Exists(path))File.Delete(path);
            FileStream fileStream = File.Create(path);
            Serialize<T>(fileStream, data);
            fileStream.Close();
        }
        
        public static object DeserializeFromFile<T>(string path)
        {
            object obj = null;
            if (File.Exists(path))
            {
                FileStream fileStream = File.OpenRead(path);
                obj = Deserialize<T>(fileStream);
                fileStream.Close();
            }
            return obj;
        }
    }
}