using System;

namespace RRON.Extensions
{
    public static partial class Extensions
    {
        public static bool ParseBool(ReadOnlySpan<char> value)
        {
            // true or yes
            if (value.Length is 4 or 3)
            {
                return true;
            }

            // false or no
            if (value.Length is 5 or 2)
            {
                return false;
            }

            switch (value[0])
            {
                case 'T':
                case 't':
                case 'Y':
                case 'y':
                case '1': return true;
                default: return false;
            }
        }
    }
}