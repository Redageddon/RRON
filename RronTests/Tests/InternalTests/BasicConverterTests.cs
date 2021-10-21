using System;
using NUnit.Framework;
using RRON.Deserialize.Converters;

namespace RronTests.Tests.InternalTests
{
    public class BasicConverterTests
    {
        [Test]
        public void ParseInt64()
        {
            Assert.AreEqual(0L, "0".AsSpan().ParseInt64());
            Assert.AreEqual(1L, "1".AsSpan().ParseInt64());
            Assert.AreEqual(1L, "0000000001".AsSpan().ParseInt64());
            Assert.AreEqual(long.MaxValue, long.MaxValue.ToString().AsSpan().ParseInt64());

            Assert.AreEqual(0L, "-0".AsSpan().ParseInt64());
            Assert.AreEqual(-1L, "-1".AsSpan().ParseInt64());
            Assert.AreEqual(-1L, "-0000000001".AsSpan().ParseInt64());
            Assert.AreEqual(long.MinValue, long.MinValue.ToString().AsSpan().ParseInt64());
        }

        [Test]
        public void ParseInt32()
        {
            Assert.AreEqual(0, "0".AsSpan().ParseInt64());
            Assert.AreEqual(1, "1".AsSpan().ParseInt64());
            Assert.AreEqual(1, "0000000001".AsSpan().ParseInt64());
            Assert.AreEqual(int.MaxValue, int.MaxValue.ToString().AsSpan().ParseInt64());

            Assert.AreEqual(0, "-0".AsSpan().ParseInt64());
            Assert.AreEqual(-1, "-1".AsSpan().ParseInt64());
            Assert.AreEqual(-1, "-0000000001".AsSpan().ParseInt64());
            Assert.AreEqual(int.MinValue, int.MinValue.ToString().AsSpan().ParseInt64());
        }

        [Test]
        public void ParseSByte()
        {
            Assert.AreEqual(0, "0".AsSpan().ParseInt64());
            Assert.AreEqual(1, "1".AsSpan().ParseInt64());
            Assert.AreEqual(1, "0000000001".AsSpan().ParseInt64());
            Assert.AreEqual(sbyte.MaxValue, sbyte.MaxValue.ToString().AsSpan().ParseInt64());

            Assert.AreEqual(0, "-0".AsSpan().ParseInt64());
            Assert.AreEqual(-1, "-1".AsSpan().ParseInt64());
            Assert.AreEqual(-1, "-0000000001".AsSpan().ParseInt64());
            Assert.AreEqual(sbyte.MinValue, sbyte.MinValue.ToString().AsSpan().ParseInt64());
        }

        [Test]
        public void ParseSingle()
        {
            Assert.AreEqual(0, "0".AsSpan().ParseSingle());
            Assert.AreEqual(0, "0.0".AsSpan().ParseSingle());
            Assert.AreEqual(123456F, 123456F.ToString().AsSpan().ParseSingle());
            Assert.AreEqual(0.012345F, 0.012345F.ToString().AsSpan().ParseSingle());
            Assert.AreEqual(111111.123456F, 111111.123456F.ToString().AsSpan().ParseSingle());

            Assert.AreEqual(0, "-0".AsSpan().ParseSingle());
            Assert.AreEqual(0, "-0.0".AsSpan().ParseSingle());
            Assert.AreEqual(-123456F, -123456F.ToString().AsSpan().ParseSingle());
            Assert.AreEqual(-0.012345F, -0.012345F.ToString().AsSpan().ParseSingle());
            Assert.AreEqual(-111111.123456F, -111111.123456F.ToString().AsSpan().ParseSingle());
        }

        [Test]
        public void ParseDouble()
        {
            Assert.AreEqual(0, "0".AsSpan().ParseDouble());
            Assert.AreEqual(0, "0.0".AsSpan().ParseDouble());
            Assert.AreEqual(111111D, 111111D.ToString().AsSpan().ParseDouble());
            Assert.AreEqual(0.012345678901D, 0.012345678901D.ToString().AsSpan().ParseDouble());
            Assert.AreEqual(111111.0123456789012D, 111111.0123456789012D.ToString().AsSpan().ParseDouble());

            Assert.AreEqual(0, "-0".AsSpan().ParseDouble());
            Assert.AreEqual(0, "-0.0".AsSpan().ParseDouble());
            Assert.AreEqual(-111111D, -111111D.ToString().AsSpan().ParseDouble());
            Assert.AreEqual(-0.012345678901D, -0.012345678901D.ToString().AsSpan().ParseDouble());
            Assert.AreEqual(-111111.0123456789012D, -111111.0123456789012D.ToString().AsSpan().ParseDouble());
        }

        [Test]
        public void ParseDecimal()
        {
            Assert.AreEqual(0, "0".AsSpan().ParseDecimal());
            Assert.AreEqual(0, "0.0".AsSpan().ParseDecimal());
            Assert.AreEqual(111111M, 111111M.ToString().AsSpan().ParseDecimal());
            Assert.AreEqual(0.012345678901234567M, 0.012345678901234567M.ToString().AsSpan().ParseDecimal());
            Assert.AreEqual(111111.012345678901234567M, 111111.012345678901234567M.ToString().AsSpan().ParseDecimal());

            Assert.AreEqual(0, "-0".AsSpan().ParseDecimal());
            Assert.AreEqual(0, "-0.0".AsSpan().ParseDecimal());
            Assert.AreEqual(-111111M, -111111M.ToString().AsSpan().ParseDecimal());
            Assert.AreEqual(-0.012345678901234567M, -0.012345678901234567M.ToString().AsSpan().ParseDecimal());
            Assert.AreEqual(-111111.012345678901234567M, -111111.012345678901234567M.ToString().AsSpan().ParseDecimal());
        }

        [Test]
        public void ParseBoolean()
        {
            Assert.IsTrue("t".AsSpan().ParseBool());
            Assert.IsTrue("T".AsSpan().ParseBool());
            Assert.IsTrue("Y".AsSpan().ParseBool());
            Assert.IsTrue("y".AsSpan().ParseBool());
            Assert.IsTrue("1".AsSpan().ParseBool());

            Assert.IsTrue("yes".AsSpan().ParseBool());
            Assert.IsTrue("yeS".AsSpan().ParseBool());
            Assert.IsTrue("yEs".AsSpan().ParseBool());
            Assert.IsTrue("yES".AsSpan().ParseBool());
            Assert.IsTrue("Yes".AsSpan().ParseBool());
            Assert.IsTrue("YeS".AsSpan().ParseBool());
            Assert.IsTrue("YEs".AsSpan().ParseBool());
            Assert.IsTrue("YES".AsSpan().ParseBool());

            Assert.IsTrue("true".AsSpan().ParseBool());
            Assert.IsTrue("truE".AsSpan().ParseBool());
            Assert.IsTrue("trUe".AsSpan().ParseBool());
            Assert.IsTrue("trUE".AsSpan().ParseBool());
            Assert.IsTrue("tRue".AsSpan().ParseBool());
            Assert.IsTrue("tRuE".AsSpan().ParseBool());
            Assert.IsTrue("tRUe".AsSpan().ParseBool());
            Assert.IsTrue("tRUE".AsSpan().ParseBool());
            Assert.IsTrue("True".AsSpan().ParseBool());
            Assert.IsTrue("TruE".AsSpan().ParseBool());
            Assert.IsTrue("TrUe".AsSpan().ParseBool());
            Assert.IsTrue("TrUE".AsSpan().ParseBool());
            Assert.IsTrue("TRue".AsSpan().ParseBool());
            Assert.IsTrue("TRuE".AsSpan().ParseBool());
            Assert.IsTrue("TRUe".AsSpan().ParseBool());
            Assert.IsTrue("TRUE".AsSpan().ParseBool());
        }

        [Test]
        public void ParseUInt64()
        {
            Assert.AreEqual(0UL, "0".AsSpan().ParseUInt64());
            Assert.AreEqual(1UL, "1".AsSpan().ParseUInt64());
            Assert.AreEqual(1UL, "0000000001".AsSpan().ParseUInt64());
            Assert.AreEqual(ulong.MaxValue, ulong.MaxValue.ToString().AsSpan().ParseUInt64());
        }

        [Test]
        public void ParseUInt32()
        {
            Assert.AreEqual(0U, "0".AsSpan().ParseUInt64());
            Assert.AreEqual(1U, "1".AsSpan().ParseUInt64());
            Assert.AreEqual(1U, "0000000001".AsSpan().ParseUInt64());
            Assert.AreEqual(uint.MaxValue, uint.MaxValue.ToString().AsSpan().ParseUInt64());
        }

        [Test]
        public void ParseByte()
        {
            Assert.AreEqual(0UL, "0".AsSpan().ParseUInt64());
            Assert.AreEqual(1UL, "1".AsSpan().ParseUInt64());
            Assert.AreEqual(1UL, "0000000001".AsSpan().ParseUInt64());
            Assert.AreEqual(byte.MaxValue, byte.MaxValue.ToString().AsSpan().ParseUInt64());
        }
    }
}