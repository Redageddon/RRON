using System.Diagnostics.CodeAnalysis;
using RRON.Deserialize;
using RRON.Serialize;
using RRON.SpanAddons;

namespace RRON
{
    using System;
    using FastMember;

    /// <summary>
    ///     The class responsible for serializing or deserializing objects.
    /// </summary>
    public static class RronConvert
    {
        /// <summary>
        ///     Serializes an object into rron data.
        /// </summary>
        /// <param name="value"> The object being serialized. </param>
        /// <param name="ignoreOptions"> The property names being skipped in serialization. </param>
        /// <returns> A string in the format of rron data. </returns>
        public static string SerializeObject(object value, string[]? ignoreOptions = null)
        {
            var serializer = new RronSerializer(value, ignoreOptions ?? Array.Empty<string>());

            return serializer.Serialize();
        }

        /// <summary>
        ///     Deserializes an object, by its generic type, from rron data.
        /// </summary>
        /// <param name="value"> The rron data being deserialized. </param>
        /// <typeparam name="T"> The compile time type to be serialized to. </typeparam>
        /// <returns> A new <typeparam name="T"/> based on <param name="value"/>. </returns>
        public static T DeserializeObject<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>(ReadOnlySpan<char> value)
            where T : new() =>
            (T)DeserializeObject(value, typeof(T), new T());

        /// <summary>
        ///     Deserializes an object, by its type, from rron data.
        /// </summary>
        /// <param name="value">The rron data being deserialized. </param>
        /// <param name="type"> The runtime type to be serialized to. </param>
        /// <returns> A new object of type <param name="type"/>. </returns>
        public static object DeserializeObject(string value, Type type) =>
            DeserializeObject(value, type, Activator.CreateInstance(type));

        /// <summary>
        ///     Deserializes an object, by its type, from rron data.
        /// </summary>
        /// <param name="value">The rron data being deserialized. </param>
        /// <param name="type"> The runtime type to be serialized to. </param>
        /// <param name="obj"> An instance of the type. </param>
        /// <returns> A new object of type <param name="type"/>. </returns>
        public static object DeserializeObject<T>(ReadOnlySpan<char> value, Type type, T obj)
        {
            var accessor = ObjectAccessor.Create(obj!)!;
            var typeNameMap = TypeNameMap.GetOrCreate(type);

            var valueStringReader = new ValueStringReader(value);
            var rronDeserializer = new RronDeserializer(accessor, typeNameMap);

            rronDeserializer.DataRead(valueStringReader);

            return accessor.Target!;
        }
    }
}