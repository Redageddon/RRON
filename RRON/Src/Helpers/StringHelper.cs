using System;
using System.Collections.Generic;

namespace RRON.Helpers
{
    /// <summary>
    ///     The class responsible for splitting up the rron rows.
    /// </summary>
    public static class StringHelper
    {
        /// <summary>
        ///     Processes a string only rron split.
        /// </summary>
        /// <param name="line"> The line being read. </param>
        /// <returns> A <see cref="Span{T}" /> representation of the current line. </returns>
        public static IReadOnlyList<string> AdvancedSplit(this string line) => AdvancedSplit(line, out var _, out var _);

        /// <summary>
        ///     Processes a fill data split.
        /// </summary>
        /// <param name="line"> The line being read. </param>
        /// <param name="isComplex"> The output that tells if the current line is the start of a complex. </param>
        /// <param name="isCollection"> The output that tells if the current line is the start of a collection. </param>
        /// <returns> A <see cref="Span{T}" /> representation of the current line. </returns>
        public static IReadOnlyList<string> AdvancedSplit(this string line, out bool isComplex, out bool isCollection)
        {
            List<string> advancedSplitStorage = new List<string>();
            isComplex = false;
            isCollection = false;
            var maybeClass = false;

            var startRange = 0;

            // We know that complex collections always start with "[["
            if (line[startRange] == '[' &&
                line[++startRange] == '[')
            {
                isComplex = true;
                isCollection = true;
                startRange++;
            }

            // Splits the line on colons and commas while ignoring whitespace and stores the values in a list.
            for (var i = 0; i < line.Length; i++)
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

            // Checks if the last character is a closing bracket and finishes class/complex calculations
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