using System;
using System.IO;
using System.Text.Json.Serialization;

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
            InflexSerializer<Test> inflexSerializer = new InflexSerializer<Test>();
            if (File.Exists(path))File.Delete(path);
            FileStream fileStream = File.Create(path);
            inflexSerializer.Serialize(fileStream, data);
            fileStream.Close();
        }
        
        private static object CustomDeserialize(Type type, string path)
        {
            object obj = null;
            InflexSerializer<Test> inflexSerializer = new InflexSerializer<Test>();
            if (File.Exists(path))
            {
                FileStream fileStream = File.OpenRead(path);
                obj = inflexSerializer.Deserialize(fileStream);
                fileStream.Close();
            }

            return obj;
        }
    }
}