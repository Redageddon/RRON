namespace RRON_new
{
    public static partial class StringDeconstructor
    {
        public static void PropertyDeconstruction(this string input, out string name, out string value)
        {
            string[] temp = input.Trim().Split(": ");
            name  = temp[0];
            value = temp[1];
        }
    }
}