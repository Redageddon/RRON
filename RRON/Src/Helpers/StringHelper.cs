using System;
using System.Collections.Generic;

namespace RRON.Helpers
{
    public static class StringHelper
    {
        private static readonly List<string> AdvancedSplitStorage = new List<string>();

        public static Span<string> AdvancedSplit(this string line)
        {
            return AdvancedSplit(line, out bool _, out bool _);
        }

        public static Span<string> AdvancedSplit(this string line, out bool isClass, out bool isCollection)
        {
            AdvancedSplitStorage.Clear();
            isClass = false;
            isCollection = false;
            bool maybeClass = false;
            
            int startRange = 0;
            if (line[startRange] == '[')
            {
                if (line[++startRange] == '[')
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
                    
                    AdvancedSplitStorage.Add(line.Substring(startRange, i - startRange));
                    while (char.IsWhiteSpace(line[++i]));
                    startRange = i;
                }
            }
            
            if (line[line.Length - 1] == ']')
            {
                if (maybeClass)
                {
                    isClass = true;
                }
                else
                {
                    isCollection = true;
                }
                
                AdvancedSplitStorage.Add(line.Substring(startRange, line.Length - startRange - 1));
            }
            else
            {
                AdvancedSplitStorage.Add(line.Substring(startRange, line.Length - startRange));
            }

            
            return AdvancedSplitStorage.ToArray();
        }
    }
}