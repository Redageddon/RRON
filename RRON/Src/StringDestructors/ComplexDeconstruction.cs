namespace RRON.StringDestructors
{
    public static partial class StringDestructor
    {
        public static void ComplexDeconstruction(this string input, out string name, out string[] propertyNames, out string[] propertyValues)
        {
            string[] rows = input.GetRowsWithName(out name);

            propertyNames  = rows[1].Split(", ");
            propertyValues = rows[2].Split(", ");
        }
    }
}