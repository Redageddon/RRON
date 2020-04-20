public class TestClass
{
    public TestClass() { }

    public TestClass(int number, string word, bool boolean)
    {
        Number = number;
        Word = word;
        Boolean = boolean;
    }

    public int Number { get; set; }
    public string Word { get; set; }
    public bool Boolean { get; set; }
}