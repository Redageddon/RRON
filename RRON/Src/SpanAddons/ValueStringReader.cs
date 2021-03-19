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

            for (; i < this.length; i++)
            {
                char currentChar = this.value[i];

                if (currentChar == '\r'
                 || currentChar == '\n')
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
            }

            if (i > this.pos)
            {
                ReadOnlySpan<char> result = this.value.Slice(this.pos, i - this.pos);
                this.pos = i;

                return result;
            }

            return ReadOnlySpan<char>.Empty;
        }

        /// <summary>
        ///     Reads to the ends of a complex block.
        /// </summary>
        /// <returns> An iterator of all lines of the block. </returns>
        public unsafe ValueStringReaderEnumerator ReadToBlockEnd
        {
            get
            {
                fixed (int* a = &this.pos)
                {
                    return new ValueStringReaderEnumerator(ref this, a);
                }
            }
        }

        /// <summary>
        ///     A class that iterates through all lines in a complex block.
        /// </summary>
        public ref struct ValueStringReaderEnumerator
        {
            private ValueStringReader reader;
            private readonly unsafe int* pointer;

            public unsafe ValueStringReaderEnumerator(ref ValueStringReader reader, int* ptr)
            {
                this.reader = reader;
                this.Current = default;
                this.pointer = ptr;
            }

            public readonly ValueStringReaderEnumerator GetEnumerator() => this;

            public unsafe bool MoveNext()
            {
                ReadOnlySpan<char> currentLine = this.reader.ReadLine();

                *this.pointer = this.reader.pos;

                if (currentLine[0] == ']')
                {
                    return false;
                }

                this.Current = currentLine.GetSplitEnumerator();

                return true;
            }

            public SplitEnumerator Current { get; private set; }
        }
    }
}