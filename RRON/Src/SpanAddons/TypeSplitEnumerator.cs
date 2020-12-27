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

        public TypeSplitEnumerator GetEnumerator() => this;

        public bool MoveNext()
        {
            if (this.value.Length == 0)
            {
                return false;
            }

            var index = this.value.IndexOf(',');
            if (index == -1)
            {
                this.Current = this.type.ConvertSpan(this.value);
                this.value = ReadOnlySpan<char>.Empty;

                return true;
            }

            this.Current = this.type.ConvertSpan(this.value.Slice(0, index));

            while (char.IsWhiteSpace(this.value[++index])) { }

            this.value = this.value.Slice(index);

            return true;
        }

        public object? Current { get; private set; }
    }
}