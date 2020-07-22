using System.Text.RegularExpressions;
using RRON.Setters;

namespace RRON
{
    public static class RronDeserializer
    {
        private static readonly Regex ComplexCollectionRx = new Regex(@"\[\[.*[^\]]*\]",       RegexOptions.Compiled);
        private static readonly Regex ComplexRx           = new Regex(@"(?<!\[)\[\w*\:.*\n.*", RegexOptions.Compiled);
        private static readonly Regex CollectionRx        = new Regex(@"\[\w*\]\s+[\w, ]*",    RegexOptions.Compiled);
        private static readonly Regex PropertyRx          = new Regex(@"^\w*\:.*",             RegexOptions.Compiled | RegexOptions.Multiline);

        public static T Deserialize<T>(string text)
            where T : new()
        {
            T instance = new T();
            ValueSetter.Type = typeof(T);

            foreach (Match match in ComplexCollectionRx.Matches(text))
            {
                match?.Value.SetComplexCollection(ref instance);
            }

            foreach (Match match in ComplexRx.Matches(text))
            {
                match?.Value.SetComplex(ref instance);
            }

            foreach (Match match in CollectionRx.Matches(text))
            {
                match?.Value.SetCollection(ref instance);
            }

            foreach (Match match in PropertyRx.Matches(text))
            {
                match?.Value.SetProperty(ref instance);
            }

            return instance;
        }
    }
}