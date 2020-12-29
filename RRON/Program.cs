using System.IO;
using RRON;

namespace RronTests
{
    public class Program
    {
        public static void Main()
        { 
            string text = File.ReadAllText("data.rron"); 
            RronConvert.DeserializeObject<TestClass>(text!);
            
            TestReal(text);
        }

        private static void TestReal(string text)
        {
            JetBrains.Profiler.Api.MeasureProfiler.StartCollectingData();
            RronConvert.DeserializeObject<TestClass>(text!);
            JetBrains.Profiler.Api.MeasureProfiler.StopCollectingData();
        }
    }
}