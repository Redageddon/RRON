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
            Assert.AreEqual(0L, "0".AsSpan().ParseInt64());
            Assert.AreEqual(1L, "1".AsSpan().ParseInt64());
            Assert.AreEqual(1L, "0000000001".AsSpan().ParseInt64());
            Assert.AreEqual(int.MaxValue, int.MaxValue.ToString().AsSpan().ParseInt64());

            Assert.AreEqual(0L, "-0".AsSpan().ParseInt64());
            Assert.AreEqual(-1L, "-1".AsSpan().ParseInt64());
            Assert.AreEqual(-1L, "-0000000001".AsSpan().ParseInt64());
            Assert.AreEqual(int.MinValue, int.MinValue.ToString().AsSpan().ParseInt64());
        }

        [Test]
        public void ParseSByte()
        {
            Assert.AreEqual(0L, "0".AsSpan().ParseInt64());
            Assert.AreEqual(1L, "1".AsSpan().ParseInt64());
            Assert.AreEqual(1L, "0000000001".AsSpan().ParseInt64());
            Assert.AreEqual(sbyte.MaxValue, sbyte.MaxValue.ToString().AsSpan().ParseInt64());

            Assert.AreEqual(0L, "-0".AsSpan().ParseInt64());
            Assert.AreEqual(-1L, "-1".AsSpan().ParseInt64());
            Assert.AreEqual(-1L, "-0000000001".AsSpan().ParseInt64());
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
    }
}