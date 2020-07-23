using System.Collections.Generic;
using RRON.Deserializer.ReaderTemps;
using RRON.Deserializer.Setters;

namespace RRON.Deserializer
{
    public static class RronDeserializer
    {
        public static T Deserialize<T>(string text)
            where T : new()
        {
            T instance = new T();
            ValueSetter.Type = typeof(T);

            RronDataRead.DataRead(text, out List<Property> properties, out List<Complex> complexes, out List<Collection> collections, out List<ComplexCollection> complexCollections);
            
            foreach (ComplexCollection complexCollection in complexCollections)
            {
                complexCollection.SetComplexCollection(ref instance);
            }

            foreach (Complex complex in complexes)
            {
                complex.SetComplex(ref instance);
            }

            foreach (Collection collection in collections)
            {
                collection.SetCollection(ref instance);
            }

            foreach (Property property in properties)
            {
                property.SetProperty(ref instance);
            }

            return instance;
        }
    }
}