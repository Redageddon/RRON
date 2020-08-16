using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace NRRON
{
    public class Program
    {
        public static void Main() => BenchmarkRunner.Run<BenchmarkClass>();
    }

    [MemoryDiagnoser]
    public class BenchmarkClass
    {
        private readonly string data = File.ReadAllText("data.rron");

        [Benchmark]
        public void Test() => RronTextReader.DataRead(new ValueStringReader(this.data));
    }
}