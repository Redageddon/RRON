using System;
using System.Collections.Generic;

namespace RRON.Helpers
{
    public static class StringHelper
    {
        private static readonly string[] ColonSplitStorage = new string[2];
        private static readonly List<string> CommaSplitStorage = new List<string>();
        
        public static string[] SplitOnColon(this string line)
        {
            for (int i = 0; i < line.Length; i++)
            {
                if (!line[i].IsAlphanumeric())
                {
                    ColonSplitStorage[0] = line[..i];
                    while (!line[i].IsAlphanumeric())
                    {
                        i++;
                    }

                    ColonSplitStorage[1] = line[i..];
                    break;
                }
            }
            return ColonSplitStorage;
        }
        
        public static string[] SplitOnComma(this string line)
        {
            CommaSplitStorage.Clear();
            int currentLineStart = 0;
            for (int i = 0; i < line.Length; i++)
            {
                if (!line[i].IsAlphanumeric())
                {
                    CommaSplitStorage.Add(line[currentLineStart..i]);
                    while (!line[i].IsAlphanumeric())
                    {
                        i++;
                    }

                    currentLineStart = i;
                }
            }
            CommaSplitStorage.Add(line[currentLineStart..]);
            return CommaSplitStorage.ToArray();
        }
        
        private static bool IsAlphanumeric(this char c)
        {
            return c == '0' || c == '1' || c == '2' || c == '3' || c == '4' || c == '5' || c == '6' || c == '7' || c == '8' ||
                   c == '9' || c == 'a' || c == 'b' || c == 'c' || c == 'd' || c == 'e' || c == 'f' || c == 'g' || c == 'h' ||
                   c == 'i' || c == 'j' || c == 'k' || c == 'l' || c == 'm' || c == 'n' || c == 'o' || c == 'p' || c == 'q' ||
                   c == 'r' || c == 's' || c == 't' || c == 'u' || c == 'v' || c == 'w' || c == 'x' || c == 'y' || c == 'z' ||
                   c == 'A' || c == 'B' || c == 'C' || c == 'D' || c == 'E' || c == 'F' || c == 'G' || c == 'H' || c == 'I' ||
                   c == 'J' || c == 'K' || c == 'L' || c == 'M' || c == 'N' || c == 'O' || c == 'P' || c == 'Q' || c == 'R' ||
                   c == 'S' || c == 'T' || c == 'U' || c == 'V' || c == 'W' || c == 'X' || c == 'Y' || c == 'Z';
        }
    }
}