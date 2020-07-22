using System.IO;
using RronTests;

namespace RRON
{
    public static class Program
    {
        private static void Main()
        {
            TestClass testClass = RronDeserializer.Deserialize<TestClass>(File.ReadAllText("data.rron"));   
        }
    }
}