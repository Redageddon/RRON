public class TestClass
{
    public TestClass(int number, string word, bool boolean, float f, double d)
    {
        Number = number;
        Word = word;
        Boolean = boolean;
        Float = f;
        Double = d;
    }
    
    public TestClass() { }
    

    public int Number { get; set; }
    public string Word { get; set; }
    public bool Boolean { get; set; }
    public float Float { get; set; }
    public double Double { get; set; }
}