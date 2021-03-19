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
        private const int Offset = 2;

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

                if (currentLine[0] == Opening)
                {
                    currentLine = currentLine[1..^(Offset - 1)];

                    if (indexOfColon == -1)
                    {
                        string name = GetName(currentLine, currentLine.Length);
                        this.accessor[name] = ValueSetter.GetCollection(this.map[name], valueStringReader.ReadLine());
                    }
                    else if (currentLine[0] == Opening)
                    {
                        currentLine = currentLine[1..];
                        string name = GetName(currentLine, indexOfColon - 2);
                        SplitEnumerator nameEnumerator = GetPropertyNameEnumerator(currentLine, indexOfColon - 1);

                        this.accessor[name] = ValueSetter.GetComplexCollection(nameEnumerator,
                                                                               this.map[name],
                                                                               currentLine.Count(',') + 1,
                                                                               ref valueStringReader);
                    }
                    else
                    {
                        string name = GetName(currentLine, indexOfColon - 1);

                        this.accessor[name] = ValueSetter.GetComplex(this.map[name],
                                                                     currentLine[indexOfColon..].Trim(),
                                                                     valueStringReader.ReadLine());
                    }
                }
                else
                {
                    string name = GetName(currentLine, indexOfColon);
                    ReadOnlySpan<char> value = currentLine[(indexOfColon + Offset)..];
                    this.accessor[name] = ValueSetter.GetSingle(this.map[name], value);
                }
            }
        }

        private static SplitEnumerator GetPropertyNameEnumerator(ReadOnlySpan<char> currentLine, int indexOfColon) =>
            currentLine.Slice(indexOfColon + 1, currentLine.Length - indexOfColon - 1).GetSplitEnumerator();

        private static string GetName(ReadOnlySpan<char> currentLine, int offset) => currentLine[..offset].ToString();
    }
}