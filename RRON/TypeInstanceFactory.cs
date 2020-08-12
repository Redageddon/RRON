using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace RRON
{
    public class TypeInstanceFactory
    {
        private static readonly Dictionary<Type, Func<object>> Constructors = new Dictionary<Type, Func<object>>();
        private static readonly Type ObjectType = typeof(object);

        public static object GetInstanceOf(Type type)
        {
            if (!Constructors.TryGetValue(type, out Func<object> constructor))
            {
                constructor = Expression.Lambda<Func<object>>(Expression.Convert(Expression.New(type), ObjectType)).Compile();
                Constructors.Add(type, constructor);
            }

            return constructor();
        }
    }
}