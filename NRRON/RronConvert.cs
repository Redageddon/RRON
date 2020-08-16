using System;

namespace NRRON
{
    public static class RronConvert
    {
        public static string SerializeObject(object value, string[] ignoreOptions = null!) => string.Empty;

        public static T DeserializeObject<T>(string value) => (T)DeserializeObject(value, typeof(T));

        public static object DeserializeObject(string value, Type type)
        {
            RronSerializer rronSerializer = new RronSerializer();
            RronTextReader.DataRead(new ValueStringReader(value));

            return rronSerializer.Deserialize(type);
        }
    }
}