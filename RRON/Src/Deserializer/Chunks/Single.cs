using RRON.Helpers;

namespace RRON.Deserializer.Chunks
{
    public readonly struct Single : ITypeAcessable
    {
        public Single(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; }

        public string Value { get; }

        public object GetObject<T>() =>
            typeof(T).GetProperty(this.Name).PropertyType.StringTypeConverter(this.Value);
    }
}