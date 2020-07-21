namespace RRON_new
{
    public static partial class ValueSetter
    {
        public static void SetClass<T>(this string match, ref T instance)
        {
            match.ClassDeconstruction(out string name, out string[] propertyNames, out string[] propertyValues);
            property = Type.GetProperty(name);
            
            property.SetValue(instance, property.PropertyType.CreateClass(propertyNames, propertyValues));
        }
    }
}