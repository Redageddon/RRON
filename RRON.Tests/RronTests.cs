using System;
using System.Collections.Generic;
using System.Diagnostics;
using Inflex.Rron;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RRON.Tests
{
    [TestClass]
    public class RronTests
    {
        private const string Path = "Test.rron";

        /*[TestMethod]
        public void SerializeObjectToFile()
        {
            List<ClassInClassTest> list = new List<ClassInClassTest>();
            
            for (int i = 0; i < 2; i++)
            {
                list.Add(new ClassInClassTest(1, "one", true, 10.0f, 20.4d, new List<int> {1, 2, 3}, new List<string> {"hello", "there"}, TestEnum.Name1,
                    new List<TestEnum> {TestEnum.Name1, TestEnum.Name2}));
            }

            TestClass testClass = new TestClass(1, "one", true, 10.2f, 20.4d, list,
                new ClassInClassTest(1, "one", true, 10.0f, 20.4d, new List<int> {1, 2, 3}, new List<string> {"hello", "there"}, TestEnum.Name1, new List<TestEnum>{TestEnum.Name1, TestEnum.Name2}),
                new List<int> {1, 2, 3},
                new List<string> {"hello", "there"}, TestEnum.Name1, new List<TestEnum>{TestEnum.Name1, TestEnum.Name2});
        }

        [TestMethod]
        public void DeserializeObjectFromFile()
        {
            TestClass test = RronConvert.DeserializeObjectFromFile<TestClass>(Path);
            TestAll(test);
        }

        private static void TestTime(Action action)
        {
            long shortest = long.MaxValue;
            long longest = long.MinValue;
            long total = 0L;
            Stopwatch stopwatch = new Stopwatch();
            const int iterationCount = 1000;

            for (int i = 0; i < iterationCount; i++)
            {
                stopwatch.Restart();
                action.Invoke();
                stopwatch.Stop();

                long time = stopwatch.ElapsedMilliseconds;
                total += time;
                if (longest < time)
                {
                    longest = time;
                }
                if (shortest > time)
                {
                    shortest = time;
                }
            }

            Debug.WriteLine($"Shortest time: {shortest}ms");
            Debug.WriteLine($"Longest time: {longest}ms");
            Debug.WriteLine($"Average time: {total * 1.0 / iterationCount}ms");
        }

        private static void TestAll(TestClass test)
        {
            Assert.AreEqual(Path, "Test.rron");
            //Assert.AreEqual(test.Number, 1);
            Assert.AreEqual(test.Word, "one");
            Assert.AreEqual(test.Boolean, true);
            Assert.AreEqual(test.Float, 10.2f);
            Assert.AreEqual(test.Double, 20.4d);
            Assert.AreEqual(test.Enum, TestEnum.Name1);
            Assert.AreEqual(test.EnumList[0], TestEnum.Name1);
            Assert.AreEqual(test.EnumList[1], TestEnum.Name2);
            Assert.AreEqual(test.ClassInClassList[0].InNumber, 1);
            Assert.AreEqual(test.ClassInClassList[0].InWord, "one");
            Assert.AreEqual(test.ClassInClassList[1].InNumber, 2);
            Assert.AreEqual(test.ClassInClassList[1].InWord, "two");
            Assert.AreEqual(test.ClassInClassList[0].InBoolean, true);
            Assert.AreEqual(test.ClassInClassList[0].InFloat, 10.0f);
            Assert.AreEqual(test.ClassInClassList[0].InDouble, 20.4d);
            Assert.AreEqual(test.ClassInClassList[0].InNonStringList[0], 1);
            Assert.AreEqual(test.ClassInClassList[0].InNonStringList[1], 2);
            Assert.AreEqual(test.ClassInClassList[0].InNonStringList[2], 3);
            Assert.AreEqual(test.ClassInClassList[0].InStringList[0], "hello");
            Assert.AreEqual(test.ClassInClassList[0].InStringList[1], "there");
            Assert.AreEqual(test.ClassInClassList[0].Enum, TestEnum.Name1);
            Assert.AreEqual(test.ClassInClassList[0].EnumList[0], TestEnum.Name1);
            Assert.AreEqual(test.ClassInClassList[0].EnumList[1], TestEnum.Name2);
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
        }*/
        [TestMethod]
        public void Deserizlize()
        {
            BeatMap beatMap = RronConvert.DeserializeObjectFromFile<BeatMap>("data.rron");
        }
    }
}