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
            CustomSerializer(typeof(Test), test, "Test.cis");
            Test postTest = CustomDeserialize(typeof(Test), "Test.cis") as Test;
            Console.WriteLine($"{postTest.Number}, {postTest.Word}, {postTest.Boolean}");
        }

        private static void CustomSerializer(Type type, object data, string path)
        {
            InflexSerializer inflexSerializer = new InflexSerializer(type);
            if (File.Exists(path))File.Delete(path);
            FileStream fileStream = File.Create(path);
            inflexSerializer.Serialize(fileStream, data);
            fileStream.Close();
        }
        private static object CustomDeserialize(Type type, string path)
        {
            object obj = null;
            InflexSerializer inflexSerializer = new InflexSerializer(type);
            if (File.Exists(path))
            {
                FileStream fileStream = File.OpenRead(path);
                obj = inflexSerializer.Deserialize(fileStream);
                fileStream.Close();
            }

            return obj;
        }
    }

    public class Test
    {
        public Test() { }

        public Test(int number, string word, bool boolean)
        {
            Number = number;
            Word = word;
            Boolean = boolean;
        }

        public int Number { get; set; }
        public string Word { get; set; }
        public bool Boolean { get; set; }
    }
}