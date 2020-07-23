using System.IO;
using NUnit.Framework;
using RRON.Deserializer;

namespace RronTests
{
    public class DataReadTest
    {
        [Test]
        public void DataRead()
        {
            string text = File.ReadAllText("data.rron");
            RronDeserializer.Deserialize<TestClass>(text);
        }
    }
}