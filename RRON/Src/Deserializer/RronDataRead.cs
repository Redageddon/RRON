using System;
using System.Collections.Generic;
using RRON.Deserializer.ReaderTemps;

namespace RRON.Deserializer
{
    public static class RronDataRead
    {
        public static void DataRead(string                      data,
                                    out List<Property>          properties,
                                    out List<Complex>           complexes,
                                    out List<Collection>        collections,
                                    out List<ComplexCollection> complexCollections)
        {
            string[] lines = data.Split(Environment.NewLine);

            properties         = new List<Property>();
            complexes          = new List<Complex>();
            collections        = new List<Collection>();
            complexCollections = new List<ComplexCollection>();

            for (int i = 0; i < lines.Length; i++)
            {
                string currentLine = lines[i];
                if (string.IsNullOrWhiteSpace(currentLine))
                {
                    continue;
                }

                if (currentLine.StartsWith('[') && currentLine.EndsWith(']') && !currentLine.Contains(':'))
                {
                    collections.Add(new Collection(currentLine.Trim('[', ']'), lines[++i].SplitTrimAll(',')));
                }
                else if (currentLine.StartsWith('[') && currentLine[1] != '[')
                {
                    string[] decelerators   = currentLine.Trim('[', ']').SplitTrimAll(':');
                    string   name           = decelerators[0];
                    string[] propertyNames  = decelerators[1].SplitTrimAll(',');
                    string[] propertyValues = lines[++i].SplitTrimAll(',');
                    complexes.Add(new Complex(name, propertyNames, propertyValues));
                }
                else if (currentLine.StartsWith('[') && currentLine[1] == '[')
                {
                    string[]       decelerators  = currentLine.Trim('[', ']').SplitTrimAll(':');
                    string         name          = decelerators[0];
                    string[]       propertyNames = decelerators[1].SplitTrimAll(',');
                    List<string[]> columns       = new List<string[]>();

                    currentLine = lines[++i];
                    while (!currentLine.Contains(']'))
                    {
                        columns.Add(currentLine.SplitTrimAll(','));
                        currentLine = lines[++i];
                    }

                    complexCollections.Add(new ComplexCollection(name, propertyNames, columns.ToArray()));
                }
                else
                {
                    string[] property = currentLine.SplitTrimAll(':');
                    properties.Add(new Property(property[0], property[1]));
                }
            }
        }

        private static string[] SplitTrimAll(this string source, char separator)
        {
            string[] values = source.Split(separator);
            for (int i = 0; i < values.Length; i++)
            {
                values[i] = values[i].Trim();
            }

            return values;
        }
    }
}