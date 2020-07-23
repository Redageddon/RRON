namespace RRON.Deserializer.ReaderTemps
{
    public readonly struct Collection
    {
        public string   Name   { get; }
        public string[] Values { get; }
        
        public Collection(string name, string[] values)
        {
            this.Name   = name;
            this.Values = values;
        }
    }
}