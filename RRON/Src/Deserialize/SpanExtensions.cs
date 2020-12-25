namespace RRON.Deserialize
{
    using System;

    /// <summary>
    ///      Houses Split.
    /// </summary>
    internal static class SpanExtensions
    {
        /// <summary>
        ///     Splits a ReadOnlySpan{char} by a char.
        /// </summary>
        /// <param name="stringAsSpan"> The span being split. </param>
        /// <param name="separator"> The character that delimits the span. </param>
        /// <param name="skipWhitespace"> If you want to skip whitespace if you only want raw values. </param>
        /// <param name="commaCount"> If you already know the amount that is going to be split into. </param>
        /// <returns> A collection of strings. </returns>
        internal static string[] Split(this ReadOnlySpan<char> stringAsSpan, char separator = ',', bool skipWhitespace = true, int commaCount = 0)
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

            string[] output = new string[commaCount];
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

        internal static object[] SplitWithType(this ReadOnlySpan<char> stringAsSpan, Type type, char separator = ',', bool skipWhitespace = true, int commaCount = 0)
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

            object[] output = new object[commaCount];
            commaCount = 0;
            var currentIndex = 0;

            for (var i = 0; i < stringAsSpan.Length; i++)
            {
                if (stringAsSpan[i] == separator)
                {
                    output[commaCount] = type.ConvertSpan(stringAsSpan.Slice(currentIndex, i - currentIndex));
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

            output[commaCount] = type.ConvertSpan(stringAsSpan.Slice(currentIndex).ToString());

            return output;
        }
    }
}