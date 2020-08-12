using System;
using FastMember;
using RRON.Deserializer.Setters;

namespace RRON.Deserializer
{
    /// <summary>
    ///     The class responsible for starting deserialization.
    /// </summary>
    internal static class RronDeserializer
    {
        /// <summary>
        ///     The method responsible for starting deserialization.
        /// </summary>
        /// <param name="lines"> All lines of an rron file. </param>
        /// <typeparam name="T"> The type of object being deserialized into. </typeparam>
        /// <returns> A new instance of <see cref="T"/> with the rron file data. </returns>
        internal static T Deserialize<T>(string[] lines)
        {
            Type type     = typeof(T);
            T instance = (T)TypeInstanceFactory.GetInstanceOf(type);

            if (ValueSetter.PreviousType != type)
            {
                ValueSetter.PreviousType     = type;
                ValueSetter.Accessor = TypeAccessor.Create(type);
                ValueSetter.PropertyTypeAccessor.Clear();

                foreach (var propertyInfo in type.GetProperties())
                {
                    ValueSetter.PropertyTypeAccessor.Add(propertyInfo.Name, propertyInfo);
                }
            }

            ValueSetter.Instance = instance;

            RronDataRead.DataRead(lines);

            return instance;
        }
    }
}