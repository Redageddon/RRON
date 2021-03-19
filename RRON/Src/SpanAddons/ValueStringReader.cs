using System;

namespace RRON.SpanAddons
{
    /// <summary>
    ///     A limited string reader based on spans.
    /// </summary>
    public ref struct ValueStringReader
    {
        private readonly ReadOnlySpan<char> value;
        private readonly int length;
        private int pos;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ValueStringReader"/> struct.
        /// </summary>
        /// <param name="value"> The string being read. </param>
        public ValueStringReader(ReadOnlySpan<char> value)
        {
            this.length = value.Length;
            this.value = value;
            this.pos = 0;
        }

        /// <summary>
        ///     Reads a new line.
        /// </summary>
        /// <returns> A new line in the form of a span. </returns>
        public ReadOnlySpan<char> ReadLine()
        {
            int i = this.pos;

            while (i < this.length)
            {
                char currentChar = this.value[i];

                if (currentChar is '\r' or '\n')
                {
                    ReadOnlySpan<char> result = this.value.Slice(this.pos, i - this.pos);
                    this.pos = 1 + i;

                    if (currentChar == '\r'
                     && this.pos < this.length
                     && this.value[this.pos] == '\n')
                    {
                        this.pos++;
                    }

                    return result;
                }

                i++;
            }

            if (i > this.pos)
            {
                ReadOnlySpan<char> result = this.value.Slice(this.pos, i - this.pos);
                this.pos = i;

                return result;
            }

            return ReadOnlySpan<char>.Empty;
        }
    }
}