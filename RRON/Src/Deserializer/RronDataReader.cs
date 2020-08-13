using System.Collections.Generic;
using System.Linq;
using RRON.Deserializer.Chunks;
using RRON.Helpers;

namespace RRON.Deserializer
{
    internal class RronDataReader
    {
        public List<ITypeAcessable> AccessableTypes { get; } = new List<ITypeAcessable>();

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

                    this.AccessableTypes.Add(new ComplexCollection(name, split, columns));
                }
                else if (isClass)
                {
                    this.AccessableTypes.Add(new Complex(name, split, lines[++i].AdvancedSplit()));
                }
                else if (isCollection)
                {
                    this.AccessableTypes.Add(new Collection(name, lines[++i].AdvancedSplit()));
                }
                else
                {
                    this.AccessableTypes.Add(new Single(name, split.ElementAt(1)));
                }
            }
        }
    }
}