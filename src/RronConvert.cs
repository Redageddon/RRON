using System.IO;
using System.Text;
using Cis;

namespace CustomInflexSerializer
{
    public static class RronConvert
    {
        /// <summary>
        /// Serializes the specified object to a RRON file.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// /// <param name="path">The serialized object's save path.</param>
        public static void SerializeObjectToFile<T>(object value, string path) => File.WriteAllText(path,  new RronSerializer().Serialize<T>(value));

        /// <summary>
        /// Deserializes the RRON to the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="path">The path to deserialize from.</param>
        /// <returns>The deserialized object from the path.</returns>
        public static T DeserializeObjectFromFile<T>(string path) => new RronSerializer().Deserialize<T>(File.OpenRead(path));

        /// <summary>
        /// Deserializes the RRON to the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="value">The RRON to deserialize.</param>
        /// <returns>The deserialized object from the RRON string.</returns>
        public static T DeserializeObjectFromString<T>(string value) => new RronSerializer().Deserialize<T>(new MemoryStream(Encoding.ASCII.GetBytes(value)));
    }
}