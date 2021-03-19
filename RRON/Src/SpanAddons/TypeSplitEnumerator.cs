using System;

namespace RRON.SpanAddons
{
    public ref struct TypeSplitEnumerator
    {
        private ReadOnlySpan<char> value;
        private readonly Type type;

        public TypeSplitEnumerator(ReadOnlySpan<char> value, Type type)
        {
            this.value = value;
            this.Current = default;
            this.type = type;
        }

        public readonly TypeSplitEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (this.value.Length == 0)
            {
                return false;
            }

            int index = this.value.IndexOf(',');

            if (index == -1)
            {
                this.Current = this.type.ConvertSpan(this.value);
                this.value = ReadOnlySpan<char>.Empty;

                return true;
            }

            this.Current = this.type.ConvertSpan(this.value[..index]);

            while (char.IsWhiteSpace(this.value[++index])) {}

            this.value = this.value[index..];

            return true;
        }

        public object? Current { get; private set; }
    }
}