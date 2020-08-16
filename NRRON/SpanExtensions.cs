using System;
using System.Collections.Generic;

namespace NRRON
{
    public static class SpanExtensions
    {
        public static IReadOnlyList<string> Split(this ReadOnlySpan<char> stringAsSpan, char separator = ',', bool skipWhitespace = true, int commaCount = 0)
        {
            if (commaCount == 0)
            {
                // ReSharper disable once ForCanBeConvertedToForeach
                for (var i = 0; i < stringAsSpan.Length; i++)
                {
                    if (stringAsSpan[i] == separator)
                    {
                        commaCount++;
                    }
                }

                commaCount++;
            }

            var output = new string[commaCount];
            commaCount = 0;
            var currentIndex = 0;

            for (var i = 0; i < stringAsSpan.Length; i++)
            {
                if (stringAsSpan[i] == separator)
                {
                    output[commaCount] = stringAsSpan.Slice(currentIndex, i - currentIndex).ToString();
                    currentIndex = ++i;
                    commaCount++;
                }

                if (skipWhitespace)
                {
                    while (char.IsWhiteSpace(stringAsSpan[i]))
                    {
                        currentIndex = ++i;
                    }
                }
            }

            output[commaCount] = stringAsSpan.Slice(currentIndex).ToString();

            return output;
        }
    }
}