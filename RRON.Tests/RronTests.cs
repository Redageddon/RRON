using System.Collections.Generic;
using Inflex.Rron;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RRON.Tests
{
    [TestClass]
    public class RronTests
    {
        private const string Path = "Test.rron";

        private readonly TestClass _testClass = new TestClass(1, "one", true, 10.2f, 20.4d, new List<ClassInClassTest>
            {
                new ClassInClassTest(1, "one", true, 10.0f, 20.4d, new List<int> {1, 2, 3}, new List<string> {"hello", "there"}, TestEnum.Name1),
                new ClassInClassTest(1, "one", true, 10.0f, 20.4d, new List<int> {1, 2, 3}, new List<string> {"hello", "there"}, TestEnum.Name1)
            },
            new ClassInClassTest(1, "one", true, 10.0f, 20.4d, new List<int> {1, 2, 3}, new List<string> {"hello", "there"}, TestEnum.Name1),
            new List<int> {1, 2, 3},
            new List<string> {"hello", "there"}, TestEnum.Name1);

        [TestMethod]
        public void SerializeObjectToFile()
        {
            TestAll(_testClass);
            RronConvert.SerializeObjectToFile(_testClass, Path);
            Assert.Fail();
        }

        [TestMethod]
        public void DeserializeObjectFromFile()
        {
            //TestClass postTest = RronConvert.DeserializeObjectFromFile<TestClass>(Path);
            //TestAll(postTest);
        }

        private static void TestAll(TestClass test)
        {
            Assert.AreEqual(Path, "Test.rron");
            Assert.AreEqual(test.Number, 1);
            Assert.AreEqual(test.Word, "one");
            Assert.AreEqual(test.Boolean, true);
            Assert.AreEqual(test.Float, 10.2f);
            Assert.AreEqual(test.Double, 20.4d);
            Assert.AreEqual(test.Enum, TestEnum.Name1);
            Assert.AreEqual(test.ClassInClassList[0].InNumber, 1);
            Assert.AreEqual(test.ClassInClassList[0].InWord, "one");
            Assert.AreEqual(test.ClassInClassList[0].InBoolean, true);
            Assert.AreEqual(test.ClassInClassList[0].InFloat, 10.0f);
            Assert.AreEqual(test.ClassInClassList[0].InDouble, 20.4d);
            Assert.AreEqual(test.ClassInClassList[0].InNonStringList[0], 1);
            Assert.AreEqual(test.ClassInClassList[0].InNonStringList[1], 2);
            Assert.AreEqual(test.ClassInClassList[0].InNonStringList[2], 3);
            Assert.AreEqual(test.ClassInClassList[0].InStringList[0], "hello");
            Assert.AreEqual(test.ClassInClassList[0].InStringList[1], "there");
            Assert.AreEqual(test.ClassInClassList[0].Enum, TestEnum.Name1);
            Assert.AreEqual(test.ClassInClassList[1].InNumber, 1);
            Assert.AreEqual(test.ClassInClassList[1].InWord, "one");
            Assert.AreEqual(test.ClassInClassList[1].InBoolean, true);
            Assert.AreEqual(test.ClassInClassList[1].InFloat, 10.0f);
            Assert.AreEqual(test.ClassInClassList[1].InDouble, 20.4d);
            Assert.AreEqual(test.ClassInClassList[1].InNonStringList[0], 1);
            Assert.AreEqual(test.ClassInClassList[1].InNonStringList[1], 2);
            Assert.AreEqual(test.ClassInClassList[1].InNonStringList[2], 3);
            Assert.AreEqual(test.ClassInClassList[1].InStringList[0], "hello");
            Assert.AreEqual(test.ClassInClassList[1].InStringList[1], "there");
            Assert.AreEqual(test.ClassInClassList[1].Enum, TestEnum.Name1);
            Assert.AreEqual(test.ClassInClass.InNumber, 1);
            Assert.AreEqual(test.ClassInClass.InWord, "one");
            Assert.AreEqual(test.ClassInClass.InBoolean, true);
            Assert.AreEqual(test.ClassInClass.InFloat, 10.0f);
            Assert.AreEqual(test.ClassInClass.InDouble, 20.4d);
            Assert.AreEqual(test.ClassInClass.InNonStringList[0], 1);
            Assert.AreEqual(test.ClassInClass.InNonStringList[1], 2);
            Assert.AreEqual(test.ClassInClass.InNonStringList[2], 3);
            Assert.AreEqual(test.ClassInClass.InStringList[0], "hello");
            Assert.AreEqual(test.ClassInClass.InStringList[1], "there");
            Assert.AreEqual(test.NonStringList[0], 1);
            Assert.AreEqual(test.NonStringList[1], 2);
            Assert.AreEqual(test.NonStringList[2], 3);
            Assert.AreEqual(test.StringList[0], "hello");
            Assert.AreEqual(test.StringList[1], "there");
        }
    }
}