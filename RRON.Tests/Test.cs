using System.Collections.Generic;

namespace RRON.Tests
{
    public enum TestEnum
    {
        Name1 = 0,
        Name2 = 1
    }

    public class TestClass
    {
        public TestClass() { }

        public TestClass(int number, string word, bool boolean, float @float, double @double, List<ClassInClassTest> classInClassList, ClassInClassTest classInClass, List<int> nonStringList,
            List<string> stringList,TestEnum @enum , List<TestEnum> enumList)
        {
            Number = number;
            Word = word;
            Boolean = boolean;
            Float = @float;
            Double = @double;
            ClassInClassList = classInClassList;
            ClassInClass = classInClass;
            NonStringList = nonStringList;
            StringList = stringList;
            Enum = @enum;
            EnumList = enumList;
        }

        public int Number { get; set; }
        public string Word { get; set; }
        public bool Boolean { get; set; }
        public float Float { get; set; }
        public double Double { get; set; }
        public List<ClassInClassTest> ClassInClassList { get; set; } = new List<ClassInClassTest>();
        public ClassInClassTest ClassInClass { get; set; }
        public List<int> NonStringList { get; set; } = new List<int>();
        public List<string> StringList { get; set; } = new List<string>();
        public TestEnum Enum { get; set; }
        public List<TestEnum> EnumList { get; set; } = new List<TestEnum>();
    }

    public class ClassInClassTest
    {
        public ClassInClassTest() { }

        public ClassInClassTest(int inNumber, string inWord, bool inBoolean, float inFloat, double inDouble, List<int> inNonStringList, List<string> inStringList, TestEnum @enum, List<TestEnum> enumList)
        {
            InNumber = inNumber;
            InWord = inWord;
            InBoolean = inBoolean;
            InFloat = inFloat;
            InDouble = inDouble;
            InNonStringList = inNonStringList;
            InStringList = inStringList;
            Enum = @enum;
            EnumList = enumList;
        }

        public int InNumber { get; set; }
        public string InWord { get; set; }
        public bool InBoolean { get; set; }
        public float InFloat { get; set; }
        public double InDouble { get; set; }
        public List<int> InNonStringList { get; set; } = new List<int>();
        public List<string> InStringList { get; set; } = new List<string>();
        public TestEnum Enum { get; set; }
        public List<TestEnum> EnumList { get; set; } = new List<TestEnum>();
    }
}