using System;
using System.Collections.Generic;
using System.Linq;
using RRON.Deserializer.Chunks;
using RRON.Helpers;

namespace RRON.Deserializer
{
    internal class RronDataReader
    {
        public Dictionary<string, Func<Type, object>> Dictionary { get; } = new Dictionary<string, Func<Type, object>>();

        public void SetValues(IReadOnlyList<string> lines)
        {
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

                    this.Dictionary.Add(name, StringTransformers.GetComplexCollection(split, columns));
                }
                else if (isClass)
                {
                    this.Dictionary.Add(name, StringTransformers.GetComplex(split, lines[++i].AdvancedSplit()));
                }
                else if (isCollection)
                {
                    this.Dictionary.Add(name, StringTransformers.GetCollection(lines[++i].AdvancedSplit()));
                }
                else
                {
                    this.Dictionary.Add(name, StringTransformers.GetSingle(split.ElementAt(1)));
                }
            }
        }
    }
}