using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RRON.Deserialize
{
    /// <summary>
    ///     Maps an objects properties names to their types.
    /// </summary>
    public static class TypeNameMap
    {
        public static ReadOnlyDictionary<string, Type> GetOrCreate(Type type)
        {
            if (!TypeMap.TryGetValue(type, out ReadOnlyDictionary<string, Type>? dictionary))
            {
                PropertyInfo[] properties = type.GetProperties();
                Dictionary<string, Type> tempDictionary = new(properties.Length);

                foreach (PropertyInfo propertyInfo in properties)
                {
                    tempDictionary.Add(propertyInfo.Name, propertyInfo.PropertyType);
                }

                dictionary = new ReadOnlyDictionary<string, Type>(tempDictionary);
                TypeMap.Add(type, dictionary);
            }

            return dictionary;
        }

        private static Dictionary<Type, ReadOnlyDictionary<string, Type>> TypeMap { get; } = new();
    }

    /// <summary>
    ///     Maps the property names of <typeparamref name="T"/> to the property types of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type to be mapped.</typeparam>
    public static class TypeNameMap<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>
    {
        static TypeNameMap()
        {
            PropertyInfo[] properties = typeof(T).GetProperties();
            Dictionary<string, Type> map = new(properties.Length);

            foreach (PropertyInfo propertyInfo in properties)
            {
                map.Add(propertyInfo.Name, propertyInfo.PropertyType);
            }

            Map = new ReadOnlyDictionary<string, Type>(map);
        }

        public static ReadOnlyDictionary<string, Type> Map { get; }
    }
}