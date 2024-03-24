using System.Linq.Expressions;
using System.Reflection;

namespace Phoenix.Services.Helpers
{
    public static class PredicateGenericHelper
    {
        public static MethodInfo? GetMethod(Type type, string methodName)
        {
            return type.GetMethods().Where(x => x.Name == methodName).FirstOrDefault();
        }

        public static Expression<Func<T, bool>> CreateExpressionCall<T>(
            string key,
            string value,
            MethodInfo? method)
        {
            var param = Expression.Parameter(typeof(T));
            MethodCallExpression call = Expression.Call(Expression.Property(param, key),
                method!,
                Expression.Constant(value, typeof(string)));
            return Expression.Lambda<Func<T, bool>>(call, param);
        }

    }
}
