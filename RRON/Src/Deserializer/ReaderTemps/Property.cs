namespace RRON.Deserializer.ReaderTemps
{
    public readonly struct Property
    {
        public string Name { get; }
        public string Value { get; }

        public Property(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }
    }
}