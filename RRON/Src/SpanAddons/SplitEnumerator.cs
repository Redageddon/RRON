using System;

namespace RRON.SpanAddons
{
    /// <summary>
    ///     Splits a <see cref="ReadOnlySpan{T}"/> where T is <see cref="char"/>, by a comma.
    ///     <see cref="ReadOnlySpan{T}"/>s cannot be passed as a collection, so the closest thing to a <see cref="ReadOnlySpan{T}"/> collection is an
    ///     enumerator with a foreach loop.
    /// </summary>
    public ref struct SplitEnumerator
    {
        private ReadOnlySpan<char> value;

        public SplitEnumerator(in ReadOnlySpan<char> value)
        {
            this.value = value;
            this.Current = default;
        }

        public readonly SplitEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (this.value.Length == 0)
            {
                return false;
            }

            int index = this.value.IndexOf(',');

            if (index == -1)
            {
                this.Current = this.value;
                this.value = ReadOnlySpan<char>.Empty;

                return true;
            }

            this.Current = this.value[..index];

            // skips all whitespace
            while (char.IsWhiteSpace(this.value[++index])) {}

            this.value = this.value[index..];

            return true;
        }

        public ReadOnlySpan<char> Current { get; private set; }
    }
}