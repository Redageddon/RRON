using System;
using System.Collections.Generic;
using System.Numerics;

namespace RronTests
{
    [Serializable]
    public enum Enum
    {
        A,
        B,
        C,
        D,
    }

    [Serializable]
    public class InClass
    {
        public InClass() { }

        public InClass(int i, int e)
        {
            this.I = i;
            this.E = e;
        }

        public int I { get; set; }

        public int E { get; set; }
    }

    /*[Serializable]
    public struct Vector2
    {
        public Vector2(float a, float b)
        {
            this.A = a;
            this.B = b;
        }

        public float A { get; set; }

        public float B { get; set; }
    }*/

    [Serializable]
    public class TestClass
    {
        public bool Bool { get; set; }

        public byte Byte { get; set; }

        public sbyte Sbyte { get; set; }

        public char Char { get; set; }

        public decimal Decimal { get; set; }

        public double Double { get; set; }

        public float Float { get; set; }

        public int Int { get; set; }

        public uint Uint { get; set; }

        public long Long { get; set; }

        public ulong Ulong { get; set; }

        public short Short { get; set; }

        public ushort Ushort { get; set; }

        public string String { get; set; }

        public Enum Enum { get; set; }

        public Vector2 Struct { get; set; }

        public InClass Class { get; set; }

        public int[] IntArray { get; set; }

        public List<int> IntList { get; set; } = new List<int>();

        public Enum[] EnumArray { get; set; }

        public List<Enum> EnumList { get; set; } = new List<Enum>();

        public Vector2[] StructArray { get; set; }

        public List<Vector2> StructList { get; set; } = new List<Vector2>();

        public InClass[] ClassArray { get; set; }

        public List<InClass> ClassList { get; set; } = new List<InClass>();
    }
}