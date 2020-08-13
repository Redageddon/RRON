using System;
using System.Collections.Generic;
using FastMember;

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

            foreach (KeyValuePair<string, Func<Type, object>> typeAccessible in dataReader.Dictionary)
            {
                accessor[typeAccessible.Key] = typeAccessible.Value(typeof(T).GetProperty(typeAccessible.Key).PropertyType);
            }

            return instance;
        }
    }
}