namespace Cis
{
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