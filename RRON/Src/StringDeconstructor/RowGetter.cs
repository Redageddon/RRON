using System;

namespace RRON.StringDeconstructor
{
    public static partial class StringDeconstructor
    {
        private static string[] GetRowsWithName(this string input, out string name)
        {
            string[] rows = input.Replace("[", "")
                                 .Replace("]",  "")
                                 .Replace(": ", Environment.NewLine)
                                 .Trim()
                                 .Split(Environment.NewLine);
            name = rows[0];
            return rows;
        }
    }
}