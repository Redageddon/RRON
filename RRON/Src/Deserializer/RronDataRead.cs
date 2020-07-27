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

                string[] test = currentLine.AdvancedSplit(out bool isClass, out bool isCollection);
                if (isClass && isCollection)
                {
                    List<string[]> columns = new List<string[]>();

                    currentLine = lines[++i];
                    while (!currentLine.Contains("]"))
                    {
                        columns.Add(currentLine.AdvancedSplit());
                        currentLine = lines[++i];
                    }
                    ValueSetter.SetComplexCollection(instance, test[0], test[1..], columns);
                }
                else if (isClass)
                {
                    ValueSetter.SetComplex(instance, test[0], test[1..], lines[++i].AdvancedSplit());
                }
                else if (isCollection)
                {
                    ValueSetter.SetCollection(instance, test[0], lines[++i].AdvancedSplit());
                }
                else
                {
                    ValueSetter.SetProperty(instance, test[0], test[1]);
                }
            }
        }
    }
}