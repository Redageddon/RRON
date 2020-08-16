using System;
using System.Collections.Generic;

namespace NRRON
{
    public class RronTextReader
    {
        private const char Opening = '[';
        private const char Closing = ']';
        private const int Offset = 2;

        public static void DataRead(ValueStringReader valueStringReader)
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
                        var (name, values) = GetCollection(currentLine, valueStringReader.ReadLine());
                    }
                    else if (currentLine[0] == Opening)
                    {
                        var (name, propertyNames, propertyValues) = GetComplexCollection(ref valueStringReader, currentLine, indexOfColon);
                    }
                    else
                    {
                        var (name, propertyNames, propertyValues) = GetComplex(currentLine, valueStringReader.ReadLine(), indexOfColon);
                    }
                }
                else
                {
                    var (name, value) = GetSingle(currentLine, indexOfColon);
                }
            }
        }

        private static (string, IReadOnlyList<string>, IReadOnlyList<IReadOnlyList<string>>) GetComplexCollection(ref ValueStringReader valueStringReader, ReadOnlySpan<char> currentLine, int indexOfColon)
        {
            var (name, propertyNames) = GetComplexHeader(currentLine.Slice(1), indexOfColon - 1);
            var propertyValues = GetComplexCollectionValues(ref valueStringReader, propertyNames.Count);

            return (name, propertyNames, propertyValues);
        }

        private static IReadOnlyList<IReadOnlyList<string>> GetComplexCollectionValues(ref ValueStringReader valueStringReader, int splitCount)
        {
            var tempPropertyValues = new List<IReadOnlyList<string>>();

            ReadOnlySpan<char> currentLine;
            while ((currentLine = valueStringReader.ReadLine())[0] != Closing)
            {
                tempPropertyValues.Add(currentLine.Split(commaCount: splitCount));
            }

            return tempPropertyValues;
        }

        private static (string, IReadOnlyList<string>, IReadOnlyList<string>) GetComplex(ReadOnlySpan<char> currentLine, ReadOnlySpan<char> nextLine, int indexOfColon)
        {
            var (name, propertyNames) = GetComplexHeader(currentLine, indexOfColon);
            var propertyValues = nextLine.Split(commaCount: propertyNames.Count);

            return (name, propertyNames, propertyValues);
        }

        private static (string, IReadOnlyList<string>) GetComplexHeader(ReadOnlySpan<char> currentLine, int indexOfColon)
        {
            var name = currentLine.Slice(0, indexOfColon - 1).ToString();
            var propertyNames = currentLine.Slice(indexOfColon + 1, currentLine.Length - indexOfColon - 1).Split();

            return (name, propertyNames);
        }

        private static (string, IReadOnlyList<string>) GetCollection(ReadOnlySpan<char> currentLine, ReadOnlySpan<char> nextLine)
        {
            var name = currentLine.Slice(0, currentLine.Length).ToString();
            var values = nextLine.Split();

            return (name, values);
        }

        private static (string, string) GetSingle(ReadOnlySpan<char> currentLine, int indexOfColon)
        {
            var name = currentLine.Slice(0, indexOfColon).ToString();
            var value = currentLine.Slice(indexOfColon + Offset).ToString();

            return (name, value);
        }
    }
}