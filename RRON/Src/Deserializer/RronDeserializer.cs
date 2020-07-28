using System;
using FastMember;
using RRON.Deserializer.Setters;

namespace RRON.Deserializer
{
    internal static class RronDeserializer
    {
        internal static T Deserialize<T>(string[] lines)
            where T : class, new()
        {
            T instance = new T();
            Type type = typeof(T);
            
            if (ValueSetter.Type != type)
            {
                ValueSetter.Type = type;
                ValueSetter.Accessor = TypeAccessor.Create(type);
                ValueSetter.propertyTypeAccessor.Clear();
                
                foreach (var propertyInfo in type.GetProperties())
                {
                    ValueSetter.propertyTypeAccessor.Add(propertyInfo.Name, propertyInfo);
                }
            }

            ValueSetter.Instance = instance;

            RronDataRead.DataRead(lines);
            return instance;
        }
    }
}