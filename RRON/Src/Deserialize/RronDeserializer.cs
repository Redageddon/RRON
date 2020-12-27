using System.Linq;
using RRON.SpanAddons;

namespace RRON.Deserialize
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
                        var name = GetName(currentLine, currentLine.Length);
                        this.accessor[name] = ValueSetter.GetCollection(this.map.GetTypeByName(name), valueStringReader.ReadLine());
                    }
                    else if (currentLine[0] == Opening)
                    {
                        currentLine = currentLine.Slice(1);
                        var name = GetName(currentLine, indexOfColon - 2);
                        var nameEnumerator = GetPropertyNameEnumerator(currentLine, indexOfColon - 1);
                        this.accessor[name] = ValueSetter.GetComplexCollection(nameEnumerator, this.map.GetTypeByName(name), currentLine.Count(',') + 1, ref valueStringReader);
                    }
                    else
                    {
                        var name = GetName(currentLine, indexOfColon - 1);
                        this.accessor[name] = ValueSetter.GetComplex(this.map.GetTypeByName(name), currentLine.Slice(indexOfColon).Trim(), valueStringReader.ReadLine());
                    }
                }
                else
                {
                    var name = GetName(currentLine, indexOfColon);
                    var value = currentLine.Slice(indexOfColon + Offset);
                    this.accessor[name] = ValueSetter.GetSingle(this.map.GetTypeByName(name), value);
                }
            }
        }

        private static SplitEnumerator GetPropertyNameEnumerator(ReadOnlySpan<char> currentLine, int indexOfColon) =>
            currentLine.Slice(indexOfColon + 1, currentLine.Length - indexOfColon - 1).Split();

        private static string GetName(ReadOnlySpan<char> currentLine, int offset) =>
            currentLine.Slice(0, offset).ToString();
    }
}