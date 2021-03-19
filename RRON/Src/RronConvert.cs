using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using FastMember;
using RRON.Deserialize;
using RRON.Serialize;
using RRON.SpanAddons;

namespace RRON
{
    /// <summary>
    ///     The class responsible for serializing or deserializing objects.
    /// </summary>
    public static class RronConvert
    {
        /// <summary>
        ///     Deserializes an object, by its type, from rron data.
        /// </summary>
        /// <param name="value">The rron data being deserialized.</param>
        /// <param name="obj">An instance of the type.</param>
        /// <returns>A new object of type <typeparamref name="T"/>.</returns>
        public static T DeserializeObject<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)]
            T>(ReadOnlySpan<char> value, T? obj = default)
            where T : new()
        {
            obj ??= new T();

            ObjectAccessor accessor = ObjectAccessor.Create(obj);
            Dictionary<string, Type> typeNameMap = TypeNameMap<T>.Map;

            ValueStringReader valueStringReader = new(value);
            RronDeserializer rronDeserializer = new(accessor, typeNameMap);

            rronDeserializer.DataRead(valueStringReader);

            return (T)accessor.Target;
        }

        /// <summary>
        ///     Serializes an object into rron data.
        /// </summary>
        /// <param name="value">The object being serialized.</param>
        /// <param name="ignoreOptions">The property names being skipped in serialization.</param>
        /// <returns>A string in the format of rron data.</returns>
        public static string SerializeObject<T>(T value, string[]? ignoreOptions = null)
            where T : notnull
        {
            RronSerializer<T> serializer = new(value, ignoreOptions ?? Array.Empty<string>());

            return serializer.Serialize();
        }
    }
}