using System;

namespace NRRON
{
    public class RronSerializer
    {
        public object Deserialize(Type objectType)
        {
            return Activator.CreateInstance(objectType);
        }
    }
}