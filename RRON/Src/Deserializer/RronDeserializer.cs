namespace RRON.Deserializer
{
    public class RronDeserializer<T>
        where T : new()
    {
        internal T Deserialize(string[] lines)
        {
            RronDataReader dataRead = new RronDataReader();
            T instance = new T();

            dataRead.SetValues(lines, instance);

            return instance;
        }
    }
}