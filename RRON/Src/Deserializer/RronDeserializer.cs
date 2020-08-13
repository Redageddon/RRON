using FastMember;
using RRON.Deserializer.Chunks;

namespace RRON.Deserializer
{
    public class RronDeserializer<T>
        where T : new()
    {
        internal T Deserialize(string[] lines)
        {
            RronDataReader dataRead = new RronDataReader();
            dataRead.SetValues(lines);

            T instance = new T();
            T accessor = SetValues(instance, dataRead);

            return accessor;
        }

        private static T SetValues(T instance, RronDataReader dataReader)
        {
            ObjectAccessor accessor = ObjectAccessor.Create(instance);

            foreach (ITypeAcessable complexCollection in dataReader.AccessableTypes)
            {
                accessor[complexCollection.Name] = complexCollection.GetObject<T>();
            }

            return instance;
        }
    }
}