using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace CustomInflexSerializer
{
    public class InflexSerializer : IFormatter
    {
        private readonly Type _type;
        public InflexSerializer(Type type) => _type = type;

        public object Deserialize(Stream serializationStream)
        {
            object obj = Activator.CreateInstance(_type);
            using (StreamReader reader = new StreamReader(serializationStream))
            { 
                reader.ReadLine();
                string contents = reader.ReadToEnd();
                List<string> pairs = contents.Split(new[] {"\n", "\r\n"}, StringSplitOptions.RemoveEmptyEntries).ToList();
                foreach (string pair in pairs)
                {
                    string[] keyValue = pair.Split(':');

                    string key = keyValue.First();
                    string value = keyValue.Last();
                    
                    PropertyInfo propertyInfo = _type.GetProperty(key);
                    
                    if (propertyInfo != null) break;
                    TypeConverter typeConverter = TypeDescriptor.GetConverter(propertyInfo.PropertyType);
                    
                    propertyInfo.SetValue(obj, typeConverter.ConvertFromString(value), null);
                    
                }
            }
            return obj;
        }

        public void Serialize(Stream serializationStream, object graph)
        {
            List<PropertyInfo> properties = _type.GetProperties().ToList();
            StreamWriter streamWriter = new StreamWriter(serializationStream);
            streamWriter.WriteLine($"[{_type.Name}]");
            foreach (PropertyInfo propertyInfo in properties)
            {
                streamWriter.WriteLine(string.Format($"{propertyInfo.Name}:{propertyInfo.GetValue(graph)}"));
            }
            
            streamWriter.Flush();
        }

        public SerializationBinder Binder { get; set; }
        public StreamingContext Context { get; set; }
        public ISurrogateSelector SurrogateSelector { get; set; }
    }
}