using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace RRON.Deserialize
{
    /// <summary>
    ///     Maps an objects properties names to their types.
    /// </summary>
    public static class TypeNameMap
    {
        public static Dictionary<string, Type> GetOrCreate(Type type)
        {
            if (!TypeMap.TryGetValue(type, out Dictionary<string, Type>? dictionary))
            {
                dictionary = new Dictionary<string, Type>();

                foreach (PropertyInfo propertyInfo in type.GetProperties())
                {
                    dictionary.Add(propertyInfo.Name, propertyInfo.PropertyType);
                }

                TypeMap.Add(type, dictionary);
            }

            return dictionary;
        }

        private static Dictionary<Type, Dictionary<string, Type>> TypeMap { get; } = new();
    }

    /// <summary>
    ///     Maps the property names of <typeparamref name="T"/> to the property types of <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type to be mapped.</typeparam>
    public static class TypeNameMap<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] T>
    {
        static TypeNameMap()
        {
            foreach (PropertyInfo propertyInfo in typeof(T).GetProperties())
            {
                Map.Add(propertyInfo.Name, propertyInfo.PropertyType);
            }
        }

        public static Dictionary<string, Type> Map { get; } = new();
    }
}