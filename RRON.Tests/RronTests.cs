using System.Collections.Generic;
using System.IO;
using Inflex.Rron;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RRON.Tests
{
    [TestClass]
    public class RronTests
    {
        private const string Path = "Test.rron";

        private readonly TestClass _test = new TestClass(1, "one", true, 10.2f, 20.4d, new List<ClassInClassTest>
            {
                new ClassInClassTest(1, 2, 3),
                new ClassInClassTest(4, 5, 6)
            }, new ClassInClassTest(1, 1, 1), 
            new List<int> {1,2,3});

        [TestMethod]
        public void SerializeObjectToFile()
        {
            TestAll(_test);
            RronConvert.SerializeObjectToFile(_test, Path);
        }

        [TestMethod]
        public void DeserializeObjectFromFile()
        {
            TestClass postTest = RronConvert.DeserializeObjectFromFile<TestClass>(Path);
            TestAll(postTest);
        }

        [TestMethod]
        public void DeserializeObjectFromString()
        {
            TestClass postTest = RronConvert.DeserializeObjectFromString<TestClass>(File.ReadAllText(Path));
            TestAll(postTest);
        }

        private static void TestAll(TestClass test)
        {
            Assert.AreEqual(Path, "Test.rron");
            Assert.AreEqual(test.Number, 1);
            Assert.AreEqual(test.Word, "one");
            Assert.AreEqual(test.Boolean, true);
            Assert.AreEqual(test.Float, 10.2f);
            Assert.AreEqual(test.Double, 20.4d);
            Assert.AreEqual(test.Test[0].Test1, 1);
            Assert.AreEqual(test.Test[0].Test2, 2);
            Assert.AreEqual(test.Test[0].Test3, 3);
            Assert.AreEqual(test.Test[1].Test1, 4);
            Assert.AreEqual(test.Test[1].Test2, 5);
            Assert.AreEqual(test.Test[1].Test3, 6);
            Assert.AreEqual(test.Test2.Test1, 1);
            Assert.AreEqual(test.Test2.Test2, 1);
            Assert.AreEqual(test.Test2.Test3, 1);
            Assert.AreEqual(test.List[0], 1);
            Assert.AreEqual(test.List[1], 2);
            Assert.AreEqual(test.List[2], 3);
        }
    }
}