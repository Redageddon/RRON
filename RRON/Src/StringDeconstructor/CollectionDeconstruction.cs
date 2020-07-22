namespace RRON.StringDeconstructor
{
    public static partial class StringDeconstructor
    {
        public static void CollectionDeconstruction(this string input, out string name, out string[] values)
        {
            string[] rows = input.GetRowsWithName(out name);
            values = rows[1].Split(", ");
        }
    }
}