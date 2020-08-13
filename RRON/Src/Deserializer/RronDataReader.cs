using System.Collections.Generic;
using System.Linq;
using RRON.Deserializer.Chunks;
using RRON.Helpers;

namespace RRON.Deserializer
{
    internal class RronDataReader
    {
        public void SetValues<T>(IReadOnlyList<string> lines, T instance)
        {
            StringTransformers<T> stringTransformers = new StringTransformers<T>(instance);
            for (int i = 0; i < lines.Count; i++)
            {
                string currentLine = lines[i];

                if (string.IsNullOrWhiteSpace(currentLine))
                {
                    continue;
                }

                IEnumerable<string> split = currentLine.AdvancedSplit(out bool isClass, out bool isCollection);
                string              name  = split.ElementAt(0);

                if (isClass && isCollection)
                {
                    List<IEnumerable<string>> columns = new List<IEnumerable<string>>();

                    while ((currentLine = lines[++i]) != "]")
                    {
                        columns.Add(currentLine.AdvancedSplit());
                    }

                    stringTransformers.SetComplexCollection(name, split, columns);
                }
                else if (isClass)
                {
                    stringTransformers.SetComplex(name, split, lines[++i].AdvancedSplit());
                }
                else if (isCollection)
                {
                    stringTransformers.SetCollection(name, lines[++i].AdvancedSplit());
                }
                else
                {
                    stringTransformers.SetSingle(name, split.ElementAt(1));
                }
            }
        }
    }
}