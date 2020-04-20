using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace RRON.Tests
{
    [TestClass]
    public class RronConvertTest
    {
        [TestMethod]
        public void SerializeObjectToFile()
        {
            TestClass test = new TestClass(1, "one", true);

            Assert.AreEqual(test.Number, 1);
            Assert.AreEqual(test.Word, "one");
            Assert.AreEqual(test.Boolean, true);

            /*RronConvert.SerializeObjectToFile<Test>(test, "Test.rron");
            TestClass postTest = RronConvert.DeserializeObjectFromFile<TestClass>("Test.rron");
            Console.WriteLine($"{postTest.Number}, {postTest.Word}, {postTest.Boolean}");*/
        }
    }
}