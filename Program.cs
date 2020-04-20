using System;
using System.IO;

namespace Cis
{
    using CustomInflexSerializer;
    internal static class Program
    {
        private static void Main()
        {
            Test test = new Test(1, "one", true);

            string rron = RronConvert.SerializeObject<Test>(test);
            File.WriteAllText("Test.rron", rron);

            RronConvert.SerializeToFile<Test>(test, "Test.cis");
            Test postTest = (Test) RronConvert.DeserializeFromFile<Test>("Test.cis");
            Console.WriteLine($"{postTest.Number}, {postTest.Word}, {postTest.Boolean}");
        }
    }
}