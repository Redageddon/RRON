using System.Collections.Generic;

namespace RRON.Helpers
{
    public static class StringHelper
    {
        public static IEnumerable<string> AdvancedSplit(this string line) => AdvancedSplit(line, out bool _, out bool _);

        public static IEnumerable<string> AdvancedSplit(this string line, out bool isComplex, out bool isCollection)
        {
            List<string> advancedSplitStorage = new List<string>();
            isComplex    = false;
            isCollection = false;
            bool maybeClass = false;

            int startRange = 0;

            if (line[startRange] == '[' &&
                line[++startRange] == '[')
            {
                isComplex    = true;
                isCollection = true;
                startRange++;
            }

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ':' ||
                    line[i] == ',')
                {
                    maybeClass = true;

                    advancedSplitStorage.Add(line.Substring(startRange, i - startRange));

                    while (char.IsWhiteSpace(line[++i])) { }

                    startRange = i;
                }
            }

            if (line[line.Length - 1] == ']')
            {
                if (maybeClass)
                {
                    isComplex = true;
                }
                else
                {
                    isCollection = true;
                }

                advancedSplitStorage.Add(line.Substring(startRange, line.Length - startRange - 1));
            }
            else
            {
                advancedSplitStorage.Add(line.Substring(startRange, line.Length - startRange));
            }

            return advancedSplitStorage;
        }
    }
}