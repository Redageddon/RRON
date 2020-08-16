using System.IO;
using NUnit.Framework;
using RRON;

namespace RronTests
{
    internal class DataReadTest
    {
        [Test]
        public void DataRead()
        {
            var text = File.ReadAllLines("data.rron");
            RronConvert.DeserializeObjectFromString<TestClass>(text);
        }
    }
}