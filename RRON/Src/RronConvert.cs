using System.IO;
using RRON.Deserializer;
using RRON.Serializer;

namespace RRON
{
    public static class RronConvert
    {
        public static void SerializeObjectToFile(object value, string path, string[] ignoreOptions = null!) =>
            File.WriteAllText(path, RronSerializer.Serialize(value, ignoreOptions));

        public static string SerializeObjectToString(object value, string[] ignoreOptions = null!) => RronSerializer.Serialize(value, ignoreOptions);

        public static T DeserializeObjectFromFile<T>(string path)
            where T : class, new() =>
            new RronDeserializer<T>().Deserialize(File.ReadAllLines(path));

        public static T DeserializeObjectFromString<T>(string[] lines)
            where T : class, new() =>
            new RronDeserializer<T>().Deserialize(lines);
    }
}