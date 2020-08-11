using System;
using System.Collections.Generic;
using RRON.Deserializer.Setters;
using RRON.Helpers;

namespace RRON.Deserializer
{
    /// <summary>
    ///     The class responsible for converting rron lines into actual data.
    /// </summary>
    internal static class RronDataRead
    {
        /// <summary>
        ///     The method responsible for converting rron lines into actual data.
        /// </summary>
        /// <param name="lines"> All lines of an rron file. </param>
        internal static void DataRead(string[] lines)
        {
            for (int i = 0; i < lines.Length; i++)
            {
                string currentLine = lines[i];

                if (string.IsNullOrWhiteSpace(currentLine))
                {
                    continue;
                }

                Span<string> value = currentLine.AdvancedSplit(out bool isClass, out bool isCollection);
                string       name  = value[0];

                Span<string> propertyNames = value.Slice(1, value.Length - 1);

                if (isClass && isCollection)
                {
                    List<string[]> columns = new List<string[]>();

                    while ((currentLine = lines[++i]) != "]")
                    {
                        columns.Add(currentLine.AdvancedSplit().ToArray());
                    }

                    ValueSetter.SetComplexCollection(name, propertyNames, columns);
                }
                else if (isClass)
                {
                    ValueSetter.SetComplex(name, propertyNames, lines[++i].AdvancedSplit());
                }
                else if (isCollection)
                {
                    ValueSetter.SetCollection(name, lines[++i].AdvancedSplit());
                }
                else
                {
                    ValueSetter.SetProperty(name, value[1]);
                }
            }
        }
    }
}