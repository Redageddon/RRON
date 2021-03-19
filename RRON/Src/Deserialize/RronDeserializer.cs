using System;
using System.Collections.Generic;
using FastMember;
using RRON.SpanAddons;

namespace RRON.Deserialize
{
    /// <summary>
    ///     The class responsible for deserializing rron data.
    /// </summary>
    public readonly struct RronDeserializer
    {
        private const char Opening = '[';
        private const char OpeningCollectionCount = '(';
        private const char ClosingCollectionCount = ')';

        private readonly ObjectAccessor accessor;
        private readonly Dictionary<string, Type> map;

        /// <summary>
        ///     Initializes a new instance of the <see cref="RronDeserializer"/> struct.
        /// </summary>
        /// <param name="accessor"> The accessor being acted upon. </param>
        /// <param name="map"> The type name cache being pulled from. </param>
        public RronDeserializer(ObjectAccessor accessor, Dictionary<string, Type> map)
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

            while ((currentLine = valueStringReader.ReadLine()) != ReadOnlySpan<char>.Empty)
            {
                if (currentLine.IsEmpty)
                {
                    continue;
                }

                int indexOfColon = currentLine.IndexOf(':');
                int preColonIndex = indexOfColon - 1;
                int postColonIndex = indexOfColon + 2;

                if (currentLine[0] == Opening)
                {
                    currentLine = currentLine[1..^1];

                    if (indexOfColon == -1)
                    {
                        // line is BasicCollection
                        (string name, int count) = GetCollectionNameAndCount(currentLine);

                        this.accessor[name] = ValueSetter.GetCollection(this.map[name], valueStringReader.ReadLine(), count);
                    }
                    else if (currentLine[0] == Opening)
                    {
                        // line is ComplexCollection
                        currentLine = currentLine[1..];
                        (string name, int count) = GetCollectionNameAndCount(currentLine);

                        this.accessor[name] = ValueSetter.GetComplexCollection(currentLine[indexOfColon..],
                                                                               this.map[name],
                                                                               ref valueStringReader,
                                                                               count);
                    }
                    else
                    {
                        // line is Complex
                        string name = currentLine[..preColonIndex].ToString();

                        this.accessor[name] = ValueSetter.GetComplex(this.map[name],
                                                                     currentLine[indexOfColon..].TrimStart(),
                                                                     valueStringReader.ReadLine());
                    }
                }
                else
                {
                    // line is Basic
                    string name = currentLine[..indexOfColon].ToString();

                    this.accessor[name] = ValueSetter.GetSingle(this.map[name], currentLine[postColonIndex..]);
                }
            }
        }

        private static (string name, int count) GetCollectionNameAndCount(ReadOnlySpan<char> currentLine)
        {
            int collectionCountStart = currentLine.IndexOf(OpeningCollectionCount);
            int collectionCountEnd = currentLine.IndexOf(ClosingCollectionCount);

            return (currentLine[..collectionCountStart].ToString(), int.Parse(currentLine[(collectionCountStart + 1)..collectionCountEnd]));
        }
    }
}