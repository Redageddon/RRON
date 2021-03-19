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
            string text = File.ReadAllText("data.rron");
            RronConvert.DeserializeObject<TestClass>(text);
        }
    }
}