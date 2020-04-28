﻿using System.Collections.Generic;

namespace RRON.Tests
{
    public enum TestEnum
    {
        Name1 = 0,
        Name2 = 1,
        Name3 = 2
    }

    public class TestClass
    {
        public TestClass() { }

        public TestClass(int number, string word, bool boolean, float @float, double @double, List<ClassInClassTest> classInClassList, ClassInClassTest classInClass, List<int> nonStringList,
            List<string> stringList, TestEnum @enum)
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
        }

        public int Number { get; set; }
        public string Word { get; set; }
        public bool Boolean { get; set; }
        public float Float { get; set; }
        public double Double { get; set; }
        public List<ClassInClassTest> ClassInClassList { get; set; }
        public ClassInClassTest ClassInClass { get; set; }
        public List<int> NonStringList { get; set; }
        public List<string> StringList { get; set; }
        public TestEnum Enum { get; set; }
    }

    public class ClassInClassTest
    {
        public ClassInClassTest() { }

        public ClassInClassTest(int inNumber, string inWord, bool inBoolean, float inFloat, double inDouble, List<int> inNonStringList, List<string> inStringList, TestEnum @enum)
        {
            InNumber = inNumber;
            InWord = inWord;
            InBoolean = inBoolean;
            InFloat = inFloat;
            InDouble = inDouble;
            InNonStringList = inNonStringList;
            InStringList = inStringList;
            Enum = @enum;
        }

        public int InNumber { get; set; }
        public string InWord { get; set; }
        public bool InBoolean { get; set; }
        public float InFloat { get; set; }
        public double InDouble { get; set; }
        public List<int> InNonStringList { get; set; }
        public List<string> InStringList { get; set; }
        public TestEnum Enum { get; set; }
    }
}