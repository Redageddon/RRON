using System.IO;
using NUnit.Framework;
using RRON;

namespace RronTests
{
    internal class DataReadTest
    {
        private readonly string text = File.ReadAllText("data.rron");

        [Test]
        public void DataRead()
        {
            RronConvert.DeserializeObject<TestClass>(this.text);
        }
    }
}