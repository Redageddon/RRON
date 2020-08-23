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
            var text = File.ReadAllText("data.rron");
            this.testClass = RronConvert.DeserializeObject<TestClass>(text);
        }

        [Test]
        public void Singles()
        {
            Assert.AreEqual(0, this.testClass.BaseSingle);
            
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
        public void Complexes()
        {
            Assert.AreEqual(new Vector2(10, 20), this.testClass.BaseComplex);
            
            Assert.AreEqual(14.0f, this.testClass.Struct.X);
            Assert.AreEqual(15.0f, this.testClass.Struct.Y);

            Assert.AreEqual(16f, this.testClass.Class.I);
            Assert.AreEqual(17f, this.testClass.Class.E);
        }

        [Test]
        public void Collections()
        {
            Assert.AreEqual(10, this.testClass.BaseCollection[0]);
            Assert.AreEqual(20, this.testClass.BaseCollection[1]);
            
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
            Assert.AreEqual(new Vector2(10, 20), this.testClass.BaseComplexCollection[0]);
            Assert.AreEqual(new Vector2(30, 40), this.testClass.BaseComplexCollection[1]);
            
            Assert.AreEqual(25.0, this.testClass.StructArray[0].X);
            Assert.AreEqual(26.0, this.testClass.StructArray[0].Y);
            Assert.AreEqual(27.0, this.testClass.StructArray[1].X);
            Assert.AreEqual(28.0, this.testClass.StructArray[1].Y);

            Assert.AreEqual(29.0, this.testClass.StructList[0].X);
            Assert.AreEqual(30.0, this.testClass.StructList[0].Y);
            Assert.AreEqual(31.0, this.testClass.StructList[1].X);
            Assert.AreEqual(32.0, this.testClass.StructList[1].Y);

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