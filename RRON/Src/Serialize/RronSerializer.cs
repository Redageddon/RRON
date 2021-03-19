using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using RRON.Extensions;

namespace RRON.Serialize
{
    /// <summary>
    ///     The class responsible for serializing an object to an rron data string.
    /// </summary>
    public class RronSerializer<T>
        where T : notnull
    {
        private readonly T instance;
        private readonly IEnumerable<PropertyInfo> sortedProperties;
        private readonly RronWriter rronWriter = new();

        /// <summary>
        ///     Initializes a new instance of the <see cref="RronSerializer{T}"/> class.
        /// </summary>
        /// <param name="instance"> The object the values are pulled from. </param>
        /// <param name="ignoreOptions"> The properties by name being skipped. </param>
        public RronSerializer(T instance, string[] ignoreOptions)
        {
            this.instance = instance;

            Type type = instance is object
                ? instance.GetType()
                : typeof(T);

            this.sortedProperties = type.GetProperties()
                                        .Where(propertyInfo => !ignoreOptions.Contains(propertyInfo.Name))
                                        .OrderBy(propertyInfo => propertyInfo.MetadataToken);
        }

        private static bool IsBasic(Type propertyType) => propertyType.IsPrimitive
                                                       || propertyType.IsEnum
                                                       || propertyType == typeof(string)
                                                       || propertyType == typeof(decimal);

        /// <summary>
        ///     Serializes an object into rron data.
        /// </summary>
        /// <returns> A string representing rron data. </returns>
        public string Serialize()
        {
            foreach (var property in this.sortedProperties)
            {
                string name = property.Name;
                Type propertyType = property.PropertyType;
                object? propertyValue = property.GetValue(this.instance);

                if (propertyValue is null)
                {
                    continue;
                }

                Type? probableNullableType = Nullable.GetUnderlyingType(propertyType);

                if (probableNullableType != null)
                {
                    propertyValue = propertyType.GetProperty("Value")!.GetValue(propertyValue)!;
                    propertyType = probableNullableType;
                }

                if (IsBasic(propertyType))
                {
                    this.rronWriter.WriteBasic(name, propertyValue);
                }
                else if (propertyValue is IEnumerable enumerable)
                {
                    Type containedType = propertyType.GetContainedType();

                    if (IsBasic(containedType))
                    {
                        this.rronWriter.WriteBasicCollection(name, enumerable);
                    }
                    else
                    {
                        this.rronWriter.WriteComplexCollection(name, enumerable, containedType);
                    }
                }
                else
                {
                    this.rronWriter.WriteComplex(name, propertyType, propertyValue);
                }
            }

            return this.rronWriter.ToString().Trim();
        }
    }
}