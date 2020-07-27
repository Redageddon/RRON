using RRON.Deserializer.Setters;

namespace RRON.Deserializer
{
    internal static class RronDeserializer
    {
        internal static T Deserialize<T>(string[] lines)
            where T : class, new()
        {
            T instance = new T();
            ValueSetter.Type = typeof(T);
            
            RronDataRead.DataRead(lines, instance);
            
            return instance;
        }
    }
}