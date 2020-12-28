using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NUnit.Framework;
using RRON;

namespace RronTests
{
    public class BenchmarkTest
    {
        [Test]
        public void GetBenchmark() => BenchmarkRunner.Run<Benchmark>();
    }

    [MemoryDiagnoser]
    public class Benchmark
    {
        private readonly string text = File.ReadAllText("data.rron");

        [Benchmark]
        public void Mark() => RronConvert.DeserializeObject<TestClass>(this.text!);
    }
}