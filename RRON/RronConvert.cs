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
            var accessor = ObjectAccessor.Create(Activator.CreateInstance(type));
            var typeNameMap = TypeNameMap.GetOrCreate(type);
            var valueStringReader = new ValueStringReader(value);
            var rronTextReader = new RronTextReader(accessor, typeNameMap);

            rronTextReader.DataRead(valueStringReader);

            return accessor.Target;
        }
    }
}