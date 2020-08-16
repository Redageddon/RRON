using System.IO;
using NUnit.Framework;
using RRON;

namespace RronTests.Tests
{
    public class DeserializerTests
    {
        private TestClass testClass;

        [SetUp]
        public void Setup()
        {
            var text = File.ReadAllLines("data.rron");
            this.testClass = RronConvert.DeserializeObjectFromString<TestClass>(text);
        }

        [Test]
        public void Singles()
        {
            Assert.AreEqual(true, this.testClass.Bool);
            Assert.AreEqual(1, this.testClass.Byte);
            Assert.AreEqual(2, this.testClass.Sbyte);
            Assert.AreEqual('3', this.testClass.Char);
            Assert.AreEqual(4.0m, this.testClass.Decimal);
            Assert.AreEqual(5.0d, this.testClass.Double);
            Assert.AreEqual(6.0f, this.testClass.Float);
            Assert.AreEqual(7, this.testClass.Int);
            Assert.AreEqual(8, this.testClass.Uint);
            Assert.AreEqual(9, this.testClass.Long);
            Assert.AreEqual(10, this.testClass.Ulong);
            Assert.AreEqual(11, this.testClass.Short);
            Assert.AreEqual(12, this.testClass.Ushort);
            Assert.AreEqual("13", this.testClass.String);
            Assert.AreEqual(Enum.A, this.testClass.Enum);
        }

        [Test]
        public void Classes()
        {
            Assert.AreEqual(14.0f, this.testClass.Struct.A);
            Assert.AreEqual(15.0f, this.testClass.Struct.B);

            Assert.AreEqual(16f, this.testClass.Class.I);
            Assert.AreEqual(17f, this.testClass.Class.E);
        }

        [Test]
        public void Collections()
        {
            Assert.AreEqual(18, this.testClass.IntArray[0]);
            Assert.AreEqual(19, this.testClass.IntArray[1]);

            Assert.AreEqual(20, this.testClass.IntList[0]);
            Assert.AreEqual(21, this.testClass.IntList[1]);

            Assert.AreEqual(Enum.A, this.testClass.EnumArray[0]);
            Assert.AreEqual(Enum.B, this.testClass.EnumArray[1]);

            Assert.AreEqual(Enum.C, this.testClass.EnumList[0]);
            Assert.AreEqual(Enum.D, this.testClass.EnumList[1]);
        }

        [Test]
        public void ComplexCollections()
        {
            Assert.AreEqual(25.0, this.testClass.StructArray[0].A);
            Assert.AreEqual(26.0, this.testClass.StructArray[0].B);
            Assert.AreEqual(27.0, this.testClass.StructArray[1].A);
            Assert.AreEqual(28.0, this.testClass.StructArray[1].B);

            Assert.AreEqual(29.0, this.testClass.StructList[0].A);
            Assert.AreEqual(30.0, this.testClass.StructList[0].B);
            Assert.AreEqual(31.0, this.testClass.StructList[1].A);
            Assert.AreEqual(32.0, this.testClass.StructList[1].B);

            Assert.AreEqual(33, this.testClass.ClassArray[0].I);
            Assert.AreEqual(34, this.testClass.ClassArray[0].E);
            Assert.AreEqual(35, this.testClass.ClassArray[1].I);
            Assert.AreEqual(36, this.testClass.ClassArray[1].E);

            Assert.AreEqual(37, this.testClass.ClassList[0].I);
            Assert.AreEqual(38, this.testClass.ClassList[0].E);
            Assert.AreEqual(39, this.testClass.ClassList[1].I);
            Assert.AreEqual(40, this.testClass.ClassList[1].E);
        }
    }
}