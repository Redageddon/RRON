using System;

namespace Cis
{
    using CustomInflexSerializer;
    internal static class Program
    {
        private static void Main()
        {
            Test test = new Test(1, "one", true);

            RronConvert.SerializeObjectToFile<Test>(test, "Test.rron");
            Test postTest = RronConvert.DeserializeObjectFromFile<Test>("Test.rron");
            
            Console.WriteLine($"{postTest.Number}, {postTest.Word}, {postTest.Boolean}");
        }
    }
}