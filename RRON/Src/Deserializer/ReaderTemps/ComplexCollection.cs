namespace RRON.Deserializer.ReaderTemps
{
    public readonly struct ComplexCollection
    {
        public string   Name           { get; }
        public string[] PropertyNames  { get; }
        public string[][] PropertyValues { get; }
        
        public ComplexCollection(string name, string[] propertyNames, string[][] propertyValues)
        {
            this.Name           = name;
            this.PropertyNames  = propertyNames;
            this.PropertyValues = propertyValues;
        }
    }
}