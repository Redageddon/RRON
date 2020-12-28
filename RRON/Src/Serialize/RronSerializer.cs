using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using RRON.Deserialize;

namespace RRON.Serialize
{
    /// <summary>
    ///     The class responsible for serializing an object to an rron data string.
    /// </summary>
    public class RronSerializer
    {
        private readonly object instance;
        private readonly IEnumerable<PropertyInfo> sortedProperties;
        private readonly TextWriter textWriter = new StringWriter();

        /// <summary>
        ///     Initializes a new instance of the <see cref="RronSerializer" /> class.
        /// </summary>
        /// <param name="instance"> The object the values are pulled from. </param>
        /// <param name="ignoreOptions"> The properties by name being skipped. </param>
        public RronSerializer(object instance, string[] ignoreOptions)
        {
            this.instance = instance;
            this.sortedProperties = this.instance
                                        .GetType()
                                        .GetProperties()
                                        .Where(e => !ignoreOptions.Contains(e.Name))
                                        .OrderBy(info => info.MetadataToken);
        }

        /// <summary>
        ///     Serializes an object into rron data.
        /// </summary>
        /// <returns> A string representing rron data. </returns>
        public string Serialize()
        {
            foreach (var property in this.sortedProperties)
            {
                var name = property.Name;
                var propertyType = property.PropertyType;
                var propertyValue = property.GetValue(this.instance);

                if (IsASingle(propertyType))
                {
                    this.WriteSingle(name, property);
                }
                else if (propertyValue is IEnumerable enumValue)
                {
                    var containedType = propertyType.GetContainedType();
                    var enumerableValue = enumValue.OfType<object>();

                    if (IsASingle(containedType))
                    {
                        this.WriteCollection(name, enumerableValue);
                    }
                    else
                    {
                        this.WriteComplexCollection(name, enumerableValue, containedType);
                    }
                }
                else
                {
                    this.WriteComplex(name, propertyType, propertyValue!);
                }
            }

            return this.textWriter.ToString()?.Trim() ?? string.Empty;
        }

        private static string GetComplexHeader(string name, Type propertyType) => $"[{name}: {string.Join(", ", propertyType.GetProperties().Select(e => e.Name))}]";

        private static bool IsASingle(Type propertyType) =>
            propertyType.IsPrimitive ||
            propertyType.IsEnum ||
            propertyType == typeof(string) ||
            propertyType == typeof(decimal);

        private void WriteSingle(string name, PropertyInfo property) =>
            this.textWriter.WriteLine($"{name}: {property.GetValue(this.instance)}");

        private void WriteCollection(string name, IEnumerable<object> propertyValue)
        {
            this.textWriter.WriteLine();
            this.textWriter.WriteLine($"[{name}]");
            this.textWriter.WriteLine(string.Join(", ", propertyValue.Select(e => e.ToString())));
        }

        private void WriteComplex(string name, Type propertyType, object propertyValue)
        {
            this.textWriter.WriteLine();
            this.textWriter.WriteLine(GetComplexHeader(name, propertyType));
            this.textWriter.WriteLine(string.Join(", ", propertyType.GetProperties().Select(e => e.GetValue(propertyValue))));
        }

        private void WriteComplexCollection(string name, IEnumerable<object> propertyValue, Type containedType)
        {
            this.textWriter.WriteLine();
            this.textWriter.Write("[");
            this.textWriter.WriteLine(GetComplexHeader(name, containedType));

            var values = propertyValue
                .Select(e => string.Join(", ", containedType.GetProperties().Select(f => f.GetValue(e))));

            this.textWriter.WriteLine(string.Join(Environment.NewLine, values));
            this.textWriter.WriteLine("]");
        }
    }
}