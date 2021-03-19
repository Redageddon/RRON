using System.Collections.Generic;
using System.IO;
using NUnit.Framework;
using RRON;

namespace RronTests.Tests
{
    public class SerializerTests
    {
        [Test]
        public void BasicTests()
        {
            TestClass basicTests = new()
            {
                NullableBool = true,
                BaseSingle = 0,
                Bool = true,
                Byte = 1,
                Sbyte = 2,
                Char = '3',
                Decimal = 4m,
                Double = 5d,
                Float = 6f,
                Int = 7,
                Uint = 8,
                Long = 9,
                Ulong = 10,
                Short = 11,
                Ushort = 12,
                String = "13",
                Enum = Enum.A,
            };

            string actual = RronConvert.SerializeObject(basicTests, new[]
            {
                nameof(TestClass.BaseComplex),
                nameof(TestClass.Struct),
            });

            Assert.AreEqual(File.ReadAllText("basic.rron"), actual);
        }

        [Test]
        public void ComplexTests()
        {
            TestClass complexTests = new()
            {
                BaseComplex = new Vector2(10, 20),
                Struct = new Vector2(14.0f, 15.0f),
                Class = new InClass(16, 17),
            };

            string actual = RronConvert.SerializeObject(complexTests, new[]
            {
                nameof(TestClass.BaseSingle),
                nameof(TestClass.Bool),
                nameof(TestClass.Byte),
                nameof(TestClass.Sbyte),
                nameof(TestClass.Char),
                nameof(TestClass.Decimal),
                nameof(TestClass.Double),
                nameof(TestClass.Float),
                nameof(TestClass.Int),
                nameof(TestClass.Uint),
                nameof(TestClass.Long),
                nameof(TestClass.Ulong),
                nameof(TestClass.Short),
                nameof(TestClass.Ushort),
                nameof(TestClass.Enum),
            });

            Assert.AreEqual(File.ReadAllText("complex.rron"), actual);
        }

        [Test]
        public void BasicCollectionTests()
        {
            TestClass collectionTests = new()
            {
                BaseCollection = new[] { 10, 20 },
                IntArray = new[] { 18, 19 },
                IntList = new List<int>
                {
                    20,
                    21,
                },
                EnumArray = new[] { Enum.A, Enum.B },
                EnumList = new List<Enum>
                {
                    Enum.C,
                    Enum.D,
                },
            };

            string actual = RronConvert.SerializeObject(collectionTests, new[]
            {
                nameof(TestClass.BaseSingle),
                nameof(TestClass.Bool),
                nameof(TestClass.Byte),
                nameof(TestClass.Sbyte),
                nameof(TestClass.Char),
                nameof(TestClass.Decimal),
                nameof(TestClass.Double),
                nameof(TestClass.Float),
                nameof(TestClass.Int),
                nameof(TestClass.Uint),
                nameof(TestClass.Long),
                nameof(TestClass.Ulong),
                nameof(TestClass.Short),
                nameof(TestClass.Ushort),
                nameof(TestClass.Enum),
                nameof(TestClass.BaseComplex),
                nameof(TestClass.Struct),
            });

            Assert.AreEqual(File.ReadAllText("basicCollection.rron"), actual);
        }

        [Test]
        public void ComplexCollectionTests()
        {
            TestClass complexCollectionTests1 = new()
            {
                BaseComplexCollection = new Vector2[] { new(10, 20), new(30, 40) },
                StructArray = new Vector2[] { new(25, 26), new(27, 28) },
                StructList = new List<Vector2>
                {
                    new(29, 30),
                    new(31, 32),
                },
                ClassArray = new InClass[] { new(33, 34), new(35, 36) },
                ClassList = new List<InClass>
                {
                    new(37, 38),
                    new(39, 40),
                },
            };

            TestClass complexCollectionTests2 = new()
            {
                BaseComplexCollection = new Vector2[]
                {
                    new(1, 2),
                    new(2, 4),
                    new(3, 6),
                },
                StructArray = new Vector2[]
                {
                    new(1, 2),
                    new(2, 4),
                    new(3, 6),
                },
                StructList = new List<Vector2>
                {
                    new(1, 2),
                    new(2, 4),
                    new(3, 6),
                },
                ClassArray = new InClass[]
                {
                    new(1, 2),
                    new(2, 4),
                    new(3, 6),
                },
                ClassList = new List<InClass>
                {
                    new(1, 2),
                    new(2, 4),
                    new(3, 6),
                },
            };

            string actual1 = RronConvert.SerializeObject(complexCollectionTests1, new[]
            {
                nameof(TestClass.BaseSingle),
                nameof(TestClass.Bool),
                nameof(TestClass.Byte),
                nameof(TestClass.Sbyte),
                nameof(TestClass.Char),
                nameof(TestClass.Decimal),
                nameof(TestClass.Double),
                nameof(TestClass.Float),
                nameof(TestClass.Int),
                nameof(TestClass.Uint),
                nameof(TestClass.Long),
                nameof(TestClass.Ulong),
                nameof(TestClass.Short),
                nameof(TestClass.Ushort),
                nameof(TestClass.Enum),
                nameof(TestClass.BaseComplex),
                nameof(TestClass.Struct),
            });

            string actual2 = RronConvert.SerializeObject(complexCollectionTests2, new[]
            {
                nameof(TestClass.BaseSingle),
                nameof(TestClass.Bool),
                nameof(TestClass.Byte),
                nameof(TestClass.Sbyte),
                nameof(TestClass.Char),
                nameof(TestClass.Decimal),
                nameof(TestClass.Double),
                nameof(TestClass.Float),
                nameof(TestClass.Int),
                nameof(TestClass.Uint),
                nameof(TestClass.Long),
                nameof(TestClass.Ulong),
                nameof(TestClass.Short),
                nameof(TestClass.Ushort),
                nameof(TestClass.Enum),
                nameof(TestClass.BaseComplex),
                nameof(TestClass.Struct),
            });

            Assert.AreEqual(File.ReadAllText("complexCollection1.rron"), actual1);
            Assert.AreEqual(File.ReadAllText("complexCollection2.rron"), actual2);
        }
    }
}