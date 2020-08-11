using System;
using System.Collections.Generic;

namespace RRON.Helpers
{
    /// <summary>
    ///     The class responsible for splitting up the rron rows.
    /// </summary>
    public static class StringHelper
    {
        private static readonly List<string> AdvancedSplitStorage = new List<string>();

        /// <summary>
        ///     Processes a string only rron split.
        /// </summary>
        /// <param name="line"> The line being read. </param>
        /// <returns> A <see cref="Span{T}(string)"/> representation of the current line. </returns>
        public static Span<string> AdvancedSplit(this string line) => AdvancedSplit(line, out bool _, out bool _);

        /// <summary>
        ///     Processes a fill data split.
        /// </summary>
        /// <param name="line"> The line being read. </param>
        /// <param name="isComplex"> The output that tells if the current line is the start of a complex. </param>
        /// <param name="isCollection"> The output that tells if the current line is the start of a collection. </param>
        /// <returns> A <see cref="Span{T}(string)"/> representation of the current line. </returns>
        public static Span<string> AdvancedSplit(this string line, out bool isComplex, out bool isCollection)
        {
            AdvancedSplitStorage.Clear();
            isComplex      = false;
            isCollection = false;
            bool maybeClass = false;

            int startRange = 0;

            if (line[startRange] == '[')
            {
                if (line[++startRange] == '[')
                {
                    isComplex      = true;
                    isCollection = true;
                    startRange++;
                }
            }

            for (int i = 0; i < line.Length; i++)
            {
                if (line[i] == ':' ||
                    line[i] == ',')
                {
                    maybeClass = true;

                    AdvancedSplitStorage.Add(line.Substring(startRange, i - startRange));

                    while (char.IsWhiteSpace(line[++i]))
                    {
                    }

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