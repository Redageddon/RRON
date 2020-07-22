using System;

namespace RRON.StringDestructors
{
    public static partial class StringDestructor
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