using System.IO;
using RRON.Deserializer;
using RRON.Serializer;

namespace RRON
{
    /// <summary>
    ///     The class responsible for serializing to or deserializing from rron.
    /// </summary>
    public static class RronConvert
    {
        /// <summary>
        ///     Serializes the specified object to a RRON file.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// <param name="path">The serialized object's save path.</param>
        /// <param name="ignoreOptions">The properties, by name, that are to be ignored.</param>
        public static void SerializeObjectToFile(object value, string path, string[] ignoreOptions = null!) =>
            File.WriteAllText(path, RronSerializer.Serialize(value, ignoreOptions));

        /// <summary>
        ///     Serializes the specified object to a RRON file.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// ///
        /// <param name="ignoreOptions">The properties, by name, that are to be ignored.</param>
        /// <returns>The string that is serialized from the object.</returns>
        public static string SerializeObjectToString(object value, string[] ignoreOptions = null!) => RronSerializer.Serialize(value, ignoreOptions);

        /// <summary>
        ///     Deserializes the RRON to the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="path">The path to deserialize from.</param>
        /// <returns>The deserialized object from the path.</returns>
        public static T DeserializeObjectFromFile<T>(string path)
            where T : class, new() =>
            RronDeserializer.Deserialize<T>(File.ReadAllLines(path));

        /// <summary>
        ///     Deserializes the RRON to the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="lines">The data to deserialize from.</param>
        /// <returns>The deserialized object from the data.</returns>
        public static T DeserializeObjectFromString<T>(string[] lines)
            where T : class, new() =>
            RronDeserializer.Deserialize<T>(lines);
    }
}