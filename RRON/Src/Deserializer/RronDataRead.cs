using System;
using System.Collections.Generic;
using RRON.Deserializer.Setters;

namespace RRON.Deserializer
{
    public static class RronDataRead
    {
        public static void DataRead<T>(string data, T instance)
        {
            string[] lines = data.Split(Environment.NewLine);

            for (int i = 0; i < lines.Length; i++)
            {
                string currentLine = lines[i];
                if (string.IsNullOrWhiteSpace(currentLine))
                {
                    continue;
                }

                if (currentLine.StartsWith('[') && currentLine.EndsWith(']') && !currentLine.Contains(':'))
                {
                    ValueSetter.SetCollection(instance, currentLine.Trim('[', ']'), lines[++i].SplitTrimAll(','));
                }
                else if (currentLine.StartsWith('[') && currentLine[1] != '[')
                {
                    string[] decelerators   = currentLine.Trim('[', ']').SplitTrimAll(':');
                    string   name           = decelerators[0];
                    string[] propertyNames  = decelerators[1].SplitTrimAll(',');
                    string[] propertyValues = lines[++i].SplitTrimAll(',');
                    ValueSetter.SetComplex(instance, name, propertyNames, propertyValues);
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

                    ValueSetter.SetComplexCollection(instance, name, propertyNames, columns.ToArray());
                }
                else
                {
                    string[] property = currentLine.SplitTrimAll(':');
                    ValueSetter.SetProperty(instance, property[0], property[1]);
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