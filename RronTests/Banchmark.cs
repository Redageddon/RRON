using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using NUnit.Framework;
using RRON;

namespace RronTests
{
    public class BanchmarkTest
    {
        [Test]
        public void GetBenchmark() => BenchmarkRunner.Run<Benchmark>();
    }

    [MemoryDiagnoser]
    public class Benchmark
    {
        private readonly string[] text = File.ReadAllLines("data.rron");
        
        [Benchmark]
        public void Mark() => 
            RronConvert.DeserializeObjectFromString<TestClass>(this.text);
    }
}