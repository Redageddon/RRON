using System;
using FastMember;

namespace RRON
{
    public static class RronConvert
    {
        public static string SerializeObject(object value, string[] ignoreOptions = null!) => string.Empty;

        public static T DeserializeObject<T>(string value) => (T)DeserializeObject(value, typeof(T));

        public static object DeserializeObject(string value, Type type)
        {
            object instance = Activator.CreateInstance(type);

            RronTextReader rronTextReader = new RronTextReader(ObjectAccessor.Create(instance), new TypeNameMap(type));
            rronTextReader.DataRead(new ValueStringReader(value));

            return instance;
        }
    }
}