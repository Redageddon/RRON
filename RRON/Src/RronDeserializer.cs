using System;
using System.Text.RegularExpressions;
using RRON.Setters;

namespace RRON
{
    public static class RronDeserializer
    {
        private static readonly Regex ClassCollectionRx = new Regex(@"\[\[.*[^\]]*\]",       RegexOptions.Compiled);
        private static readonly Regex ClassRx           = new Regex(@"(?<!\[)\[\w*\:.*\n.*", RegexOptions.Compiled);
        private static readonly Regex CollectionRx      = new Regex(@"\[\w*\]\s.*",          RegexOptions.Compiled | RegexOptions.Multiline);
        private static readonly Regex PropertyRx        = new Regex(@"^\w*\:.*",     RegexOptions.Compiled | RegexOptions.Multiline);

        public static T Deserialize<T>(string text) 
            where T : new()
        {
            T instance = new T();
            ValueSetter.Type = typeof(T);

            foreach (Match match in ClassCollectionRx.Matches(text))
            {
                match.Value.SetClassCollection(ref instance);
            }

            foreach (Match match in ClassRx.Matches(text))
            {
                match.Value.SetClass(ref instance);
            }

            foreach (Match match in CollectionRx.Matches(text))
            {
                match.Value.SetCollection(ref instance);
                Console.WriteLine("g: " + match.Value);
            }

            foreach (Match match in PropertyRx.Matches(text))
            {
                match.Value.SetProperty(ref instance);
            }

            return instance;
        }
    }
}