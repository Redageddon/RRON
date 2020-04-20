using System.IO;
using Inflex.Rron;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RRON.Tests
{
    [TestClass]
    public class RronTests
    {
        private const string Path = "Test.rron";
        private readonly TestClass _test = new TestClass(1, "one", true, 10.2f, 20.4d);
        
        [TestMethod]
        public void SerializeObjectToFile()
        {
            Assert.AreEqual(Path, "Test.rron");
            Assert.AreEqual(_test.Number, 1);
            Assert.AreEqual(_test.Word, "one");
            Assert.AreEqual(_test.Boolean, true);
            Assert.AreEqual(_test.Float, 10.2f);
            Assert.AreEqual(_test.Double, 20.4d);

            RronConvert.SerializeObjectToFile<TestClass>(_test, Path);
        }

        [TestMethod]
        public void DeserializeObjectFromFile()
        {
            TestClass postTest = RronConvert.DeserializeObjectFromFile<TestClass>("Test.rron");
            
            Assert.AreEqual(Path, "Test.rron");
            Assert.AreEqual(postTest.Number, 1);
            Assert.AreEqual(postTest.Word, "one");
            Assert.AreEqual(postTest.Boolean, true);
            Assert.AreEqual(postTest.Float, 10.2f);
            Assert.AreEqual(postTest.Double, 20.4d);
        }

        [TestMethod]
        public void DeserializeObjectFromString()
        {
            TestClass postTest = RronConvert.DeserializeObjectFromString<TestClass>(File.ReadAllText(Path));
         
            Assert.AreEqual(Path, "Test.rron");
            Assert.AreEqual(postTest.Number, 1);
            Assert.AreEqual(postTest.Word, "one");
            Assert.AreEqual(postTest.Boolean, true);
            Assert.AreEqual(postTest.Float, 10.2f);
            Assert.AreEqual(postTest.Double, 20.4d);
        }
    }
}