using System.Collections.Generic;
using RRON.Deserializer.Setters;
using RRON.Helpers;

namespace RRON.Deserializer
{
    internal static class RronDataRead
    {
        internal static void DataRead<T>(string[] lines, T instance)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                string currentLine = lines[i];
                if (string.IsNullOrWhiteSpace(currentLine))
                {
                    continue;
                }

                if (currentLine[0] == '[')
                {
                    if (currentLine[^1] == ']' && !currentLine.Contains(":"))
                    {
                        ValueSetter.SetCollection(instance, currentLine[1..^1], lines[++i].SplitOnComma());
                    }
                    else if (currentLine[1] != '[')
                    {
                        string[] decelerators = currentLine[1..^1].SplitOnColon();

                        string   name           = decelerators[0];
                        string[] propertyNames  = decelerators[1].SplitOnComma();
                        string[] propertyValues = lines[++i].SplitOnComma();
                        ValueSetter.SetComplex(instance, name, propertyNames, propertyValues);
                    }
                    else 
                    {
                        string[]       decelerators  = currentLine[2..^1].SplitOnColon();  
                        string         name          = decelerators[0];
                        string[]       propertyNames = decelerators[1].SplitOnComma();
                        List<string[]> columns       = new List<string[]>();

                        currentLine = lines[++i];
                        while (!currentLine.Contains("]"))
                        {
                            columns.Add(currentLine.SplitOnComma());
                            currentLine = lines[++i];
                        }

                        ValueSetter.SetComplexCollection(instance, name, propertyNames, columns);
                    }
                }
                else
                {
                    string[] property = currentLine.SplitOnColon();
                    ValueSetter.SetProperty(instance, property[0], property[1]);
                }
            }
        }
    }
}