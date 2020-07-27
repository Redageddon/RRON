using System.Collections.Generic;

namespace RRON.Helpers
{
    public static class StringHelper
    {
        private static readonly List<string> AdvancedSplitStorage = new List<string>();

        public static string[] AdvancedSplit(this string line)
        {
            return AdvancedSplit(line, out bool _, out bool _);
        }

        public static string[] AdvancedSplit(this string line, out bool isClass, out bool isCollection)
        {
            AdvancedSplitStorage.Clear();
            isClass = false;
            isCollection = false;
            bool maybeClass = false;
            
            int startRange = 0;
            if (line[startRange] == '[')
            {
                startRange++;
                if (line[startRange] == '[')
                {
                    isClass      = true;
                    isCollection = true;
                    startRange++;
                }
            }

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ':' || line[i] == ',')
                {
                    maybeClass = true;
                    AdvancedSplitStorage.Add(line[startRange..i]);
                    while (char.IsWhiteSpace(line[++i]));
                    startRange = i;
                }
            }
            
            if (line[^1] == ']')
            {
                if (maybeClass)
                {
                    isClass = true;
                }
                else
                {
                    isCollection = true;
                }
                AdvancedSplitStorage.Add(line[startRange..^1]);
            }
            else
            {
                AdvancedSplitStorage.Add(line[startRange..]);
            }

            return AdvancedSplitStorage.ToArray();
        }
    }
}