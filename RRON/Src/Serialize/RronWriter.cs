using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace RRON.Serialize
{
    public class RronWriter : StringWriter
    {
        private const string Separator = ", ";

        public void WriteBasic(string name, object? propertyValue) => this.WriteLine($"{name}: {propertyValue}");

        public void WriteBasicCollection(string name, IEnumerable propertyValue)
        {
            int collectionSize = 0;

            IEnumerable<string> InternalIterator()
            {
                foreach (object o in propertyValue)
                {
                    yield return o.ToString() ?? string.Empty;
                    collectionSize++;
                }
            }

            this.WriteLine();
            string values = string.Join(Separator, InternalIterator()); // execute values before header so collection size can be efficiently calculated.
            this.WriteLine($"[{name}({collectionSize})]");
            this.WriteLine(values);
        }

        public void WriteComplex(string name, Type propertyType, object propertyValue)
        {
            this.WriteLine();
            this.WriteLine(GetComplexHeader(name, propertyType));

            this.WriteLine(string.Join(Separator, GetPropertyStrings(propertyType, propertyValue)));
        }

        public void WriteComplexCollection(string name, IEnumerable propertyValue, Type containedType)
        {
            List<string> strings = new();

            foreach (object? value in propertyValue)
            {
                strings.Add(string.Join(Separator, GetPropertyStrings(containedType, value)));
            }

            this.WriteLine();
            this.Write("[");
            this.WriteLine(GetComplexCollectionHeader(name, strings.Count, containedType));

            foreach (string complex in strings)
            {
                this.WriteLine(complex);
            }

            this.WriteLine("]");
        }

        private static string GetComplexHeader(string name, Type propertyType) =>
            $"[{name}: {string.Join(Separator, propertyType.GetProperties().Select(e => e.Name))}]";

        private static string GetComplexCollectionHeader(string name, int collectionSize, Type propertyType) =>
            $"[{name}({collectionSize}): {string.Join(Separator, propertyType.GetProperties().Select(e => e.Name))}]";

        private static IEnumerable<string> GetPropertyStrings(Type propertyType, object value)
        {
            foreach (PropertyInfo propertyInfo in propertyType.GetProperties())
            {
                yield return propertyInfo.GetValue(value)?.ToString() ?? string.Empty;
            }
        }
    }
}