using System;

namespace RRON
{
    public ref struct ValueStringReader
    {
        private readonly ReadOnlySpan<char> value;
        private readonly int length;
        private int pos;

        public ValueStringReader(string value)
        {
            this.length = value.Length;
            this.value = value.AsSpan();
            this.pos = 0;
        }

        public ReadOnlySpan<char> ReadLine()
        {
            var i = this.pos;
            for (; i < this.length; i++)
            {
                var currentChar = this.value[i];

                if (currentChar == '\r' ||
                    currentChar == '\n')
                {
                    var result = this.value.Slice(this.pos, i - this.pos);
                    this.pos = 1 + i;

                    if (currentChar == '\r' &&
                        this.pos < this.length &&
                        this.value[this.pos] == '\n')
                    {
                        this.pos++;
                    }

                    return result;
                }
            }

            if (i > this.pos)
            {
                var result = this.value.Slice(this.pos, i - this.pos);
                this.pos = i;

                return result;
            }

            return null;
        }
    }
}