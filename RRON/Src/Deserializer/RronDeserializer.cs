using RRON.Deserializer.Setters;

namespace RRON.Deserializer
{
    public static class RronDeserializer
    {
        public static T Deserialize<T>(string text)
            where T : class, new()
        {
            T instance = new T();
            ValueSetter.Type = typeof(T);

            RronDataRead.DataRead(text, instance);
            
            return instance;
        }
    }
}