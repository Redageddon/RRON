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
            string[] text = File.ReadAllLines("data.rron");
            TestClass testClass = RronConvert.DeserializeObjectFromString<TestClass>(text);
        }
    }
}