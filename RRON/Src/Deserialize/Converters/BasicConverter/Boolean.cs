using System;

namespace RRON.Deserialize.Converters
{
    public static partial class Converter
    {
        public static bool ParseBool(this ReadOnlySpan<char> value)
        {
            switch (value.Length)
            {
                case 1 when value[0] is 'T' or 't' or 'Y' or 'y' or '1':
                case 3 when char.ToLower(value[0]) == 'y' &&
                            char.ToLower(value[1]) == 'e' &&
                            char.ToLower(value[2]) == 's':
                case 4 when char.ToLower(value[0]) == 't' &&
                            char.ToLower(value[1]) == 'r' &&
                            char.ToLower(value[2]) == 'u' &&
                            char.ToLower(value[3]) == 'e': return true;
                default: return false;
            }
        }
    }
}