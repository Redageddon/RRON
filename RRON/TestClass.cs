using System.Collections.Generic;

namespace RronTests
{
    public enum Enum
    {
        A,
        B,
        C,
        D
    }

    public class ClassInClassTest
    {
        public int I { get; set; }
        public int E { get; set; }
    }

    public struct Vector2
    {
        public float A { get; set; }
        public float B { get; set; }
    }

    public class TestClass
    {
        public TestClass()
        {
        }

        public TestClass(bool                   @bool,
                         byte                   @byte,
                         sbyte                  @sbyte,
                         char                   @char,
                         decimal                @decimal,
                         double                 @double,
                         float                  @float,
                         int                    @int,
                         uint                   @uint,
                         long                   @long,
                         ulong                  @ulong,
                         short                  @short,
                         ushort                 @ushort,
                         string                 @string,
                         Enum                   @enum,
                         Vector2                @struct,
                         ClassInClassTest       @class,
                         int[]                  intArray,
                         List<int>              intList,
                         Enum[]                 enumArray,
                         List<Enum>             enumList,
                         Vector2[]              structArray,
                         List<Vector2>          structList,
                         ClassInClassTest[]     classArray,
                         List<ClassInClassTest> classList)
        {
            this.Bool        = @bool;
            this.Byte        = @byte;
            this.Sbyte       = @sbyte;
            this.Char        = @char;
            this.Decimal     = @decimal;
            this.Double      = @double;
            this.Float       = @float;
            this.Int         = @int;
            this.Uint        = @uint;
            this.Long        = @long;
            this.Ulong       = @ulong;
            this.Short       = @short;
            this.Ushort      = @ushort;
            this.String      = @string;
            this.Enum        = @enum;
            this.Struct      = @struct;
            this.Class       = @class;
            this.IntArray    = intArray;
            this.IntList     = intList;
            this.EnumArray   = enumArray;
            this.EnumList    = enumList;
            this.StructArray = structArray;
            this.StructList  = structList;
            this.ClassArray  = classArray;
            this.ClassList   = classList;
        }

        public bool                   Bool        { get; set; }
        public byte                   Byte        { get; set; }
        public sbyte                  Sbyte       { get; set; }
        public char                   Char        { get; set; }
        public decimal                Decimal     { get; set; }
        public double                 Double      { get; set; }
        public float                  Float       { get; set; }
        public int                    Int         { get; set; }
        public uint                   Uint        { get; set; }
        public long                   Long        { get; set; }
        public ulong                  Ulong       { get; set; }
        public short                  Short       { get; set; }
        public ushort                 Ushort      { get; set; }
        public string                 String      { get; set; }
        public Enum                   Enum        { get; set; }
        public Vector2                Struct      { get; set; }
        public ClassInClassTest       Class       { get; set; }
        public int[]                  IntArray    { get; set; }
        public List<int>              IntList     { get; set; } = new List<int>();
        public Enum[]                 EnumArray   { get; set; }
        public List<Enum>             EnumList    { get; set; } = new List<Enum>();
        public Vector2[]              StructArray { get; set; }
        public List<Vector2>          StructList  { get; set; } = new List<Vector2>();
        public ClassInClassTest[]     ClassArray  { get; set; }
        public List<ClassInClassTest> ClassList   { get; set; } = new List<ClassInClassTest>();
    }
}