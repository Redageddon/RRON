namespace RRON.Deserializer.Chunks
{
    public interface ITypeAcessable
    {
        public string Name { get; }

        public object GetObject<T>();
    }
}