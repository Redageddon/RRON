using System.IO;
using System.Text;

namespace Inflex.Rron
{
    public static class RronConvert
    {
        private static readonly RronSerializer Converter = new RronSerializer();
        /// <summary>
        /// Serializes the specified object to a RRON file.
        /// </summary>
        /// <param name="value">The object to serialize.</param>
        /// /// <param name="path">The serialized object's save path.</param>
        public static void SerializeObjectToFile(object value, string path) => File.WriteAllText(path,  Converter.Serialize(value));

        /// <summary>
        /// Deserializes the RRON to the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="path">The path to deserialize from.</param>
        /// <returns>The deserialized object from the path.</returns>
        public static T DeserializeObjectFromFile<T>(string path) => Converter.Deserialize<T>(File.OpenRead(path));

        /// <summary>
        /// Deserializes the RRON to the specified type.
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize to.</typeparam>
        /// <param name="value">The RRON to deserialize.</param>
        /// <returns>The deserialized object from the RRON string.</returns>
        public static T DeserializeObjectFromString<T>(string value) => Converter.Deserialize<T>(new MemoryStream(Encoding.ASCII.GetBytes(value)));
    }
}