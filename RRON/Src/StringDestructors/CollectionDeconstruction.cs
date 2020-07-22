namespace RRON.StringDestructors
{
    public static partial class StringDestructor
    {
        public static void CollectionDeconstruction(this string input, out string name, out string[] values)
        {
            string[] rows = input.GetRowsWithName(out name);
            values = rows[1].Split(", ");
        }
    }
}