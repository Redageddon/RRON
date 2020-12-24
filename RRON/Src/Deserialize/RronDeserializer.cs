namespace RRON
{
    using System;
    using System.Collections.Generic;
    using FastMember;

    /// <summary>
    ///     The class responsible for deserializing rron data.
    /// </summary>
    public readonly struct RronDeserializer
    {
        private const char Opening = '[';
        private const char Closing = ']';
        private const int Offset = 2;

        private readonly ObjectAccessor accessor;
        private readonly TypeNameMap map;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RronDeserializer"/> struct.
        /// </summary>
        /// <param name="accessor"> The accessor being acted upon. </param>
        /// <param name="map"> The type name cache being pulled from. </param>
        public RronDeserializer(ObjectAccessor accessor, TypeNameMap map)
        {
            this.accessor = accessor;
            this.map = map;
        }

        /// <summary>
        ///     Reads the rron data and sets the object accessor accordingly.
        /// </summary>
        /// <param name="valueStringReader"> The span version string reader that reads rron data. </param>
        public void DataRead(ValueStringReader valueStringReader)
        {
            ReadOnlySpan<char> currentLine;

            while ((currentLine = valueStringReader.ReadLine()) != null)
            {
                if (currentLine.IsEmpty)
                {
                    continue;
                }

                var indexOfColon = currentLine.IndexOf(':');

                if (currentLine[0] == Opening)
                {
                    currentLine = currentLine.Slice(1, currentLine.Length - Offset);
                    if (indexOfColon == -1)
                    {
                        var (name, values) = GetCollectionStrings(currentLine, valueStringReader.ReadLine());
                        this.accessor[name] = ValueSetter.GetCollection(this.map.GetTypeByName(name), values);
                    }
                    else if (currentLine[0] == Opening)
                    {
                        var (name, propertyNames, propertyValues) = GetComplexCollectionStrings(ref valueStringReader, currentLine, indexOfColon);
                        this.accessor[name] = ValueSetter.GetComplexCollection(this.map.GetTypeByName(name), propertyNames, propertyValues);
                    }
                    else
                    {
                        var (name, propertyNames, propertyValues) = GetComplexStrings(currentLine, valueStringReader.ReadLine(), indexOfColon);
                        this.accessor[name] = ValueSetter.GetComplex(this.map.GetTypeByName(name), propertyNames, propertyValues);
                    }
                }
                else
                {
                    var (name, value) = GetSingleStrings(currentLine, indexOfColon);
                    this.accessor[name] = ValueSetter.GetSingle(this.map.GetTypeByName(name), value);
                }
            }
        }

        private static (string, string[], IReadOnlyList<string[]>) GetComplexCollectionStrings(ref ValueStringReader valueStringReader, ReadOnlySpan<char> currentLine, int indexOfColon)
        {
            var (name, propertyNames) = GetComplexHeaderStrings(currentLine.Slice(1), indexOfColon - 1);
            var propertyValues = GetComplexCollectionValuesStrings(ref valueStringReader, propertyNames.Length);

            return (name, propertyNames, propertyValues);
        }

        private static IReadOnlyList<string[]> GetComplexCollectionValuesStrings(ref ValueStringReader valueStringReader, int splitCount)
        {
            var tempPropertyValues = new List<string[]>();

            ReadOnlySpan<char> currentLine;
            while ((currentLine = valueStringReader.ReadLine())[0] != Closing)
            {
                tempPropertyValues.Add(currentLine.Split(commaCount: splitCount));
            }

            return tempPropertyValues;
        }

        private static (string, string[], string[]) GetComplexStrings(ReadOnlySpan<char> currentLine, ReadOnlySpan<char> nextLine, int indexOfColon)
        {
            var (name, propertyNames) = GetComplexHeaderStrings(currentLine, indexOfColon);
            var propertyValues = nextLine.Split(commaCount: propertyNames.Length);

            return (name, propertyNames, propertyValues);
        }

        private static (string, string[]) GetComplexHeaderStrings(ReadOnlySpan<char> currentLine, int indexOfColon)
        {
            var name = currentLine.Slice(0, indexOfColon - 1).ToString();
            var propertyNames = currentLine.Slice(indexOfColon + 1, currentLine.Length - indexOfColon - 1).Split();

            return (name, propertyNames);
        }

        private static (string, string[]) GetCollectionStrings(ReadOnlySpan<char> currentLine, ReadOnlySpan<char> nextLine)
        {
            var name = currentLine.Slice(0, currentLine.Length).ToString();
            var values = nextLine.Split();

            return (name, values);
        }

        private static (string, string) GetSingleStrings(ReadOnlySpan<char> currentLine, int indexOfColon)
        {
            var name = currentLine.Slice(0, indexOfColon).ToString();
            var value = currentLine.Slice(indexOfColon + Offset).ToString();

            return (name, value);
        }
    }
}