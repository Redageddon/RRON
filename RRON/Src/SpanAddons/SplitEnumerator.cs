using System;

namespace RRON.SpanAddons
{
    public ref struct SplitEnumerator
    {
        private ReadOnlySpan<char> value;

        public SplitEnumerator(ReadOnlySpan<char> value)
        {
            this.value = value;
            this.Current = default;
        }

        public SplitEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (this.value.Length == 0)
            {
                return false;
            }
            
            var index = this.value.IndexOf(',');

            if (index == -1)
            {
                this.Current = this.value;
                this.value = ReadOnlySpan<char>.Empty;

                return true;
            }

            this.Current = this.value.Slice(0, index);

            while (char.IsWhiteSpace(this.value[++index])) { }

            this.value = this.value.Slice(index);

            return true;
        }

        public ReadOnlySpan<char> Current { get; private set; }
    }
}