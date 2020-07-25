using System.IO;
using NUnit.Framework;
using RRON;
using RRON.Deserializer;

namespace RronTests
{
    internal class DataReadTest
    {
        [Test]
        public void DataRead()
        {
            string text = File.ReadAllText("data.rron");
            RronConvert.DeserializeObjectFromString<TestClass>(text);
        }
    }
}