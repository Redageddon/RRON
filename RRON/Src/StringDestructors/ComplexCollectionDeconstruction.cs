namespace RRON.StringDestructors
{
    public static partial class StringDestructor
    {
        public static void ComplexCollectionDeconstruction(this string input, out string name, out string[] propertyNames, out string[][] propertyValues)
        {
            string[] rows = input.GetRowsWithName(out name);

            propertyNames  = rows[1].Split(", ");
            propertyValues = new string[rows.Length - 2][];
            for (int i = 0; i < rows.Length - 2; i++)
            {
                propertyValues[i] = rows[i + 2].Split(", ");
            }
        }
    }
}