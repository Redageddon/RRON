using System.Collections.Generic;
using System.Linq;
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

                IEnumerable<string> split = currentLine.AdvancedSplit(out bool isClass, out bool isCollection);
                string       name  = split.ElementAt(0);

                if (isClass && isCollection)
                {
                    List<IEnumerable<string>> columns = new List<IEnumerable<string>>();

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
                    ValueSetter.SetProperty(name, split.ElementAt(1));
                }
            }
        }
    }
}