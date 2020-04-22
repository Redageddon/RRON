public class TestClass
{
    public TestClass(int number, string word, bool boolean, float f, double d, ClassInClassTest c, int[] t)
    {
        Number = number;
        Word = word;
        Boolean = boolean;
        Float = f;
        Double = d;
        Test = c;
        Tet = t;
    }
    
    public TestClass() { }
    

    public int Number { get; set; }
    public string Word { get; set; }
    public bool Boolean { get; set; }
    public float Float { get; set; }
    public double Double { get; set; }
    public int[] Tet { get; set; }
    public ClassInClassTest Test { get; set; }
}

public class ClassInClassTest
{
    public ClassInClassTest(int s) => Class = s;

    public int Class { get; set; }
}