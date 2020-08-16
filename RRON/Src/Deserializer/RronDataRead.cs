using System.Collections.Generic;
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
            for (var i = 0; i < lines.Length; i++)
            {
                string currentLine = lines[i];

                if (string.IsNullOrWhiteSpace(currentLine))
                {
                    continue;
                }

                IReadOnlyList<string> split = currentLine.AdvancedSplit(out var isClass, out var isCollection);
                string name = split[0];

                if (isClass && isCollection)
                {
                    List<IReadOnlyList<string>> columns = new List<IReadOnlyList<string>>();

                    while ((currentLine = lines[++i]) != "]")
                    {
                        columns.Add(currentLine.AdvancedSplit());
                    }

                    ValueSetter.SetComplexCollection(name, split, columns);
                }
                else if (isClass)
                {
                    ValueSetter.SetComplex(name, split, lines[++i].AdvancedSplit());
                }
                else if (isCollection)
                {
                    ValueSetter.SetCollection(name, lines[++i].AdvancedSplit());
                }
                else
                {
                    ValueSetter.SetProperty(name, split[1]);
                }
            }
        }
    }
}